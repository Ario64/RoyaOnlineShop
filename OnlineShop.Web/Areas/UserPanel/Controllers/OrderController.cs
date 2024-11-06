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
            return View(_orderService.GetOrders(userName));
        }

        [Authorize]
        public IActionResult ShowCart(int id)
        {
            string userName = User.Identity.Name;
            
            var order = _orderService.GetOrderForUserPanel(userName,id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
    }
}
