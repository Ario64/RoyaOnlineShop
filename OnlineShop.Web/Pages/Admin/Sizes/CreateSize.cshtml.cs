using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.Product;

namespace OnlineShop.Web.Pages.Admin.Sizes
{
    public class CreateSizeModel : PageModel
    {
        private IProductService _productService;

        public CreateSizeModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Size Size { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _productService.AddSize(Size);
            return RedirectToPage("Index");
        }
    }
}
