using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Core.Security;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.Product;

namespace OnlineShop.Web.Pages.Admin.Products
{
    [PermissionChecker(14)]
    public class EditProductModel : PageModel
    {
        private IProductService _productService;

        public EditProductModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Product Product { get; set; }

        public void OnGet(int id)
        {
            Product = _productService.GetProductByProductId(id);

            var mainGroups = _productService.GetMainGroup();
            ViewData["MainGroups"] = new SelectList(mainGroups, "Value", "Text", Product.ProductGroupId);

            List<SelectListItem> subGroups = new List<SelectListItem>()
            {
                new() { Text = "انتخاب کنید !", Value = "" }
            };
            subGroups.AddRange(_productService.GetSubMainGroup(Product.ProductGroupId));
            string? selectedSubGroup = null;
            if (Product.SubGroup != null)
            {
                selectedSubGroup = Product.SubGroup.ToString();
            }
            ViewData["SubMainGroups"] = new SelectList(subGroups, "Value", "Text", selectedSubGroup);
        }

        public IActionResult OnPost(IFormFile? imgProductUp)
        {
            _productService.UpdateProduct(Product, imgProductUp);

            return RedirectToPage("Index");
        }
    }
}
