using Microsoft.AspNetCore.Mvc.Internal;
using OnlineShop.DataLayer.Entities.Order;

namespace OnlineShop.Core.Services.Interfaces;

public interface IOrderService
{
    int AddOrder(string userName, int productId);
    void UpdatePriceOrder(int orderId);
    Order GetOrderForUserPanel(string userName, int orderId);
    List<Order> GetOrders(string userName);
}