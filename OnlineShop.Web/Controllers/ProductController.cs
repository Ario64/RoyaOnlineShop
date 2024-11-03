using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Index(int pageId = 1, string filterName = "", string orderDate = "", string price = "", List<int>? selectedGroups = null, int take = 0)
        {
            ViewBag.SelectedGroups = selectedGroups;
            ViewBag.Groups = _productService.GetGroups();
            ViewBag.PageId = pageId;
            return View(_productService.GetProducts(pageId, filterName, orderDate, price, selectedGroups, take));
        }

        [Route("ShowProduct/{id}")]
        public IActionResult ShowProduct(int id)
        {
            var product = _productService.GetProductForShow(id);
            if (product == null)
            {
                return NotFound();
            }

            var productSizeList = _productService.GetProductSizeList(id);
            ViewData["ProductSizeList"] = new SelectList(productSizeList, "Value", "Text");

            return View(product);
        }
    }
}
