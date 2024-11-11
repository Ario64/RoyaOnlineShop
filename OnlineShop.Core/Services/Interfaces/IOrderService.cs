using OnlineShop.Core.DTOs.Order;
using OnlineShop.DataLayer.Entities.Order;

namespace OnlineShop.Core.Services.Interfaces;

public interface IOrderService
{
    #region Order

    int AddOrder(string userName, int productId);
    void UpdatePriceOrder(int orderId);
    Order GetOrderForUserPanel(string userName, int orderId);
    List<Order> GetUserOrders(string userName);
    bool FinallyOrder(string userName, int orderId);
    int DeleteOrderDetail(string userName, int detailId);
    Order GetOrderByOrderId(int orderId);
    void UpdateOrder(Order order);
    int GetOrderByUserName(string userName);

    #endregion

    #region Discount

    DiscountUsageType UseDiscount(int orderId, string code);
    List<Discount> GetAllDiscounts();
    void AddDiscount(Discount discount);
    Discount GetDiscountById(int discountId);
    void UpdateDiscount(Discount discount);
    bool IsExistCode(string code);

    #endregion

}
