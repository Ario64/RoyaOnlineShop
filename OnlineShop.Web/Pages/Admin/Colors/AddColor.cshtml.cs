using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.Product;

namespace OnlineShop.Web.Pages.Admin.Colors
{
    public class AddColorModel : PageModel
    {
        private IProductService _productService;

        public AddColorModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Color Color { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _productService.AddColor(Color);
            return RedirectToPage("Index");
        }
    }
}
