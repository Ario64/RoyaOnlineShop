using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.Security;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.Product;

namespace OnlineShop.Web.Pages.Admin.Groups
{
    [PermissionChecker(17)]
    public class CreateGroupModel : PageModel
    {
        private IProductService _productService;

        public CreateGroupModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public ProductGroup Group { set; get; }

        public void OnGet(int? id)
        {
            Group = new ProductGroup()
            {
                ParentId = id
            };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _productService.AddGroup(Group);
            return RedirectToPage("Index");
        }
    }
}
