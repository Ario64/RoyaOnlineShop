using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize]
        public IActionResult ShowCart(int id, bool finaly = false, bool quantity = false)
        {
            string userName = User.Identity.Name;

            var order = _orderService.GetOrderForUserPanel(userName, id);
            if (order == null)
            {
                return RedirectToAction("EmptyCart");
            }

            ViewBag.finaly = finaly;
            ViewBag.quantity = quantity;

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
    }
}
