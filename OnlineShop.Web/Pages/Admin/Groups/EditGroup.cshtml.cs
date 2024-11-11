using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.Security;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.Product;

namespace OnlineShop.Web.Pages.Admin.Groups
{
    [PermissionChecker(18)]
    public class EditGroupModel : PageModel
    {
        private IProductService _productService;

        public EditGroupModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public ProductGroup Group { get; set; }

        public void OnGet(int id)
        {
            Group = _productService.GetProductGroupByProductId(id);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _productService.UpdateGroup(Group);
            return RedirectToPage("Index");
        }
    }
}
