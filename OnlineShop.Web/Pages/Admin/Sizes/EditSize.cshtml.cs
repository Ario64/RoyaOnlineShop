using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.Product;

namespace OnlineShop.Web.Pages.Admin.Sizes
{
    public class EditSizeModel : PageModel
    {
        private IProductService _productService;

        public EditSizeModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Size Size { get; set; }

        public void OnGet(int id)
        {
            Size = _productService.GetSizeByIdForAdmin(id);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            _productService.UpdateSize(Size);
            return RedirectToPage("Index");
        }
    }
}
