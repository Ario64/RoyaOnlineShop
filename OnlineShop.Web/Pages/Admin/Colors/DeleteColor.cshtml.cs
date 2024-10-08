using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.Security;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.Product;

namespace OnlineShop.Web.Pages.Admin.Colors
{
    [PermissionChecker(18)]
    public class DeleteColorModel : PageModel
    {
        private IProductService _productService;

        public DeleteColorModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Color Color { get; set; }

        public void OnGet(int id)
        {
            Color = _productService.GetColorByIdForAdmin(id);
        }

        public IActionResult OnPost()
        {
            _productService.DeleteColor(Color);
            return RedirectToPage("Index");
        }
    }
}
