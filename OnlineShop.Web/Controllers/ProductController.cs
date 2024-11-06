using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Core.Services.Interfaces;

namespace OnlineShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IOrderService _orderService;

        public ProductController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        public IActionResult Index(int pageId = 1, string filterName = "", string orderDate = "", string price = "", List<int>? selectedGroups = null, int take = 0)
        {
            ViewBag.SelectedGroups = selectedGroups;
            ViewBag.Groups = _productService.GetGroups();
            ViewBag.PageId = pageId;
            return View(_productService.GetProducts(pageId, filterName, orderDate, price, selectedGroups, take));
        }

        [Route("showproduct/{id}")]
        public IActionResult ShowProduct(int id)
        {
            var product = _productService.GetProductForShow(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [Authorize]
        public IActionResult AddToCart(int id)
        {
            string userName = User.Identity.Name;

            int orderId = _orderService.AddOrder(userName,id);

            return Redirect($"/userpanel/order/showcart/" + orderId);
        }
    }
}
