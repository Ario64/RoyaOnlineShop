using Microsoft.EntityFrameworkCore;
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

}