using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.Security;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.Product;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.Web.Pages.Admin.Groups
{
    [PermissionChecker(19)]
    public class DeleteGroupModel : PageModel
    {
        private IProductService _productService;

        public DeleteGroupModel(IProductService productService)
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
            _productService.DeleteGroup(Group);
            return RedirectToPage("Index");
        }
    }
}
