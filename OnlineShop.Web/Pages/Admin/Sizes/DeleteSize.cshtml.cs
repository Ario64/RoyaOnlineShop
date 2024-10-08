using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.Product;

namespace OnlineShop.Web.Pages.Admin.Sizes
{
    public class DeleteSizeModel : PageModel
    {
        private IProductService _productService;

        public DeleteSizeModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty] public Size Size { get; set; }

        public void OnGet(int id)
        {
            Size = _productService.GetSizeByIdForAdmin(id);
        }

        public IActionResult OnPost()
        {
            _productService.DeleteSize(Size);
            return RedirectToPage("Index");
        }
    }
}