using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.DTOs.Product;
using OnlineShop.Core.Security;
using OnlineShop.Core.Services.Interfaces;

namespace OnlineShop.Web.Pages.Admin.Products
{
    [PermissionChecker(12)]
    public class IndexModel : PageModel
    {
        private IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public ShowProductsForAdminViewModel Product { get; set; }

        public void OnGet(int pageId = 1, string filterProductName = "")
        {
            Product = _productService.GetAllProduct(pageId,filterProductName);
        }
    }
}
