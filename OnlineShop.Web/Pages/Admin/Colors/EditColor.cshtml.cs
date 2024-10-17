using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.Security;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.Product;

namespace OnlineShop.Web.Pages.Admin.Colors
{
    public class EditColorModel : PageModel
    {
        private IProductService _productService;

        public EditColorModel(IProductService productService)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _productService.UpdateColor(Color);
            return RedirectToPage("Index");
        }
    }
}
