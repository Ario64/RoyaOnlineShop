using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.DTOs.Order;
using OnlineShop.Core.Services.Interfaces;

namespace OnlineShop.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            string userName = User.Identity.Name;
            if (userName == null)
            {
                return NotFound();
            }
            return View(_orderService.GetUserOrders(userName));
        }

        public IActionResult ShowCart(int id, bool finaly = false, bool quantity = false, string type = "")
        {
            string userName = User.Identity.Name;

            var order = _orderService.GetOrderForUserPanel(userName, id);
            if (order == null)
            {
                return RedirectToAction("EmptyCart");
            }

            ViewBag.finaly = finaly;
            ViewBag.quantity = quantity;
            ViewBag.discounttype = type;

            return View(order);
        }

        public IActionResult EmptyCart()
        {
            return View();
        }

        public IActionResult FinallyOrder(int id)
        {
            if (_orderService.FinallyOrder(User.Identity.Name, id))
            {
                return Redirect($"/UserPanel/Order/ShowCart/" + id + "?finaly=true");
            }

            return Redirect("/UserPanel/Order/ShowCart/" + id + "?quantity=true");
        }

        public IActionResult DeleteOrderDetail(int id)
        {
            int orderId = _orderService.DeleteOrderDetail(User.Identity.Name, id);
            return Redirect("/UserPanel/Order/ShowCart/" + orderId);
        }

        public IActionResult UseDiscount(int orderId, string code)
        {
            DiscountUsageType type = _orderService.UseDiscount(orderId, code);
            return Redirect($"/userpanel/Order/ShowCart/" + orderId + "?type=" + type);
        }
    }
}
