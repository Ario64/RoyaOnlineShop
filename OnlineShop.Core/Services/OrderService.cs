using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.DTOs.Order;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Contexts;
using OnlineShop.DataLayer.Entities.Order;
using OnlineShop.DataLayer.Entities.User;
using OnlineShop.DataLayer.Entities.Wallet;

namespace OnlineShop.Core.Services;

public class OrderService : IOrderService
{
    private readonly RoyaContext _context;
    private readonly IUserService _userService;

    public OrderService(RoyaContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public int AddOrder(string userName, int productId)
    {
        int userId = _userService.GetUserIdByUserName(userName);

        Order order = _context.Orders.FirstOrDefault(f => f.UserId == userId && !f.IsFinally);

        var product = _context.Products.Find(productId);

        if (order == null)
        {
            order = new Order()
            {
                CreateDate = DateTime.Now,
                IsFinally = false,
                UserId = userId,
                OrderSum = product.ProductPrice,
                OrderDetails = new List<OrderDetail>
                {
                    new()
                    {
                        ProductId = productId,
                        Quantity = 1,
                        Price = product.ProductPrice
                    }
                }
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        else
        {
            OrderDetail detail = _context.OrderDetails.FirstOrDefault(f => f.OrderId == order.OrderId && f.ProductId == productId);

            if (detail != null)
            {
                detail.Quantity += 1;
                _context.OrderDetails.Update(detail);
            }
            else
            {
                detail = new OrderDetail()
                {
                    ProductId = productId,
                    Quantity = 1,
                    Price = product.ProductPrice,
                    OrderId = order.OrderId
                };
                _context.OrderDetails.Add(detail);
            }
            _context.SaveChanges();
            UpdatePriceOrder(order.OrderId);
        }
        return order.OrderId;
    }

    public void UpdatePriceOrder(int orderId)
    {
        var order = _context.Orders.Find(orderId);

        order.OrderSum = _context.OrderDetails
            .Where(d => d.OrderId == orderId)
            .Sum(d => d.Price * d.Quantity);

        _context.Orders.Update(order);
        _context.SaveChanges();
    }

    public Order GetOrderForUserPanel(string userName, int orderId)
    {
        int userId = _userService.GetUserIdByUserName(userName);

        return _context.Orders
           .Include(i => i.OrderDetails)
           .ThenInclude(t => t.Product)
           .FirstOrDefault(f => f.UserId == userId && f.OrderId == orderId);

    }

    public List<Order> GetUserOrders(string userName)
    {
        int userId = _userService.GetUserIdByUserName(userName);

        return _context.Orders
            .Include(i => i.OrderDetails)
            .OrderByDescending(o => o.CreateDate)
            .Where(w => w.UserId == userId)
            .ToList();
    }

    public bool FinallyOrder(string userName, int orderId)
    {
        int userId = _userService.GetUserIdByUserName(userName);

        var order = _context.Orders
            .Include(i => i.OrderDetails)
            .ThenInclude(th => th.Product)
            .FirstOrDefault(w => w.UserId == userId && w.OrderId == orderId);

        if (order == null || order.IsFinally)
            return false;

        foreach (var detail in order.OrderDetails)
        {
            var product = _context.Products.Find(detail.ProductId);
            if (product.Quantity < detail.Quantity)
            {
                return false;
            }
        }

        if (_userService.UserBalance(userName) >= order.OrderSum)
        {
            order.IsFinally = true;
            _userService.AddWallet(new Wallet()
            {
                CreateDate = DateTime.Now,
                Description = "فاکتور شماره #" + order.OrderId,
                Amount = order.OrderSum,
                IsPayed = true,
                UserId = userId,
                WalletTypeId = 2
            });
            _context.Orders.Update(order);
            foreach (var detail in order.OrderDetails)
            {
                _context.UserProducts.Add(new UserProduct()
                {
                    ProductId = detail.ProductId,
                    UserId = userId
                });
                var product = _context.Products.Find(detail.ProductId);
                product.Quantity -= detail.Quantity;
            }
            _context.SaveChanges();

            return true;
        }

        return false;
    }

    public int DeleteOrderDetail(string userName, int detailId)
    {
        int userId = _userService.GetUserIdByUserName(userName);

        var order = _context.Orders
            .Include(i => i.OrderDetails)
            .FirstOrDefault(f => f.UserId == userId && !f.IsFinally);

        if (order != null)
        {
            foreach (var detail in order.OrderDetails)
            {
                _context.OrderDetails
                    .Where(w => w.OrderDetailId == detailId)
                    .ToList()
                    .ForEach(f => _context.OrderDetails.Remove(f));
            }
        }
        _context.SaveChanges();
        if (!order.OrderDetails.Any())
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
        return order.OrderId;
    }

    public Order GetOrderByOrderId(int orderId)
    {
        return _context.Orders.Find(orderId);
    }

    public void UpdateOrder(Order order)
    {
        _context.Orders.Update(order);
        _context.SaveChanges();
    }

    public int GetOrderByUserName(string userName)
    {
        int userId = _userService.GetUserIdByUserName(userName);
        var order = _context.Orders.SingleOrDefault(w => w.UserId == userId && !w.IsFinally);
        return order.OrderId;
    }

    public DiscountUsageType UseDiscount(int orderId, string code)
    {
        var discount = _context.Discounts.SingleOrDefault(s => s.DiscountCode == code);

        if (discount == null)
        {
            return DiscountUsageType.NotFound;
        }

        if (discount.StartDate != null && discount.StartDate < DateTime.Now)
        {
            return DiscountUsageType.Expired;
        }

        if (discount.StartDate != null && discount.EndDate >= DateTime.Now)
        {
            return DiscountUsageType.Expired;
        }

        if (discount.UsableCount != null && discount.UsableCount < 1)
        {
            return DiscountUsageType.Finished;
        }

        var order = GetOrderByOrderId(orderId);

        if (_context.UserDiscountCodes.Any(a => a.UserId == order.UserId && a.DiscountId == discount.DiscountId))
        {
            return DiscountUsageType.Used;
        }

        var percent = (order.OrderSum * discount.DiscountPercent) / 100;

        order.OrderSum -= percent;
        UpdateOrder(order);
        if (discount.UsableCount != null)
        {
            discount.UsableCount -= 1;
        }

        _context.Discounts.Update(discount);
        _context.UserDiscountCodes.Add(new UserDiscountCode()
        {
            UserId = order.UserId,
            DiscountId = discount.DiscountId
        });

        _context.SaveChanges();
        return DiscountUsageType.Success;
    }

    public List<Discount> GetAllDiscounts()
    {
        return _context.Discounts.ToList();
    }

    public void AddDiscount(Discount discount)
    {
        _context.Discounts.Add(discount);
        _context.SaveChanges();
    }

    public Discount GetDiscountById(int discountId)
    {
        return _context.Discounts.Find(discountId);
    }

    public void UpdateDiscount(Discount discount)
    {
        _context.Discounts.Update(discount);
        _context.SaveChanges();
    }

    public bool IsExistCode(string code)
    {
        return _context.Discounts.Any(a => a.DiscountCode == code);
    }
}