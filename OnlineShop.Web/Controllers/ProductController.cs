using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Services.Interfaces;

namespace OnlineShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index(int pageId = 1, string filterName = "", string orderDate = "lastDate", string price = "minPrice", List<int>? selectedGroups = null, int take = 0)
        {
            ViewBag.SelectedGroups = selectedGroups;
            ViewBag.Groups = _productService.GetGroups();
            ViewBag.PageId = pageId;
            return View(_productService.GetProducts(pageId, filterName, orderDate, price, selectedGroups, take));
        }
    }
}
