using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.Product;

namespace OnlineShop.Web.Pages.Admin.Products
{
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
            ViewData["MainGroups"] = new SelectList(mainGroups, "Value", "Text", Product.ProductId);

            var subGroup = _productService.GetSubMainGroup(int.Parse(mainGroups.First().Value));
            ViewData["SubMainGroups"] = new SelectList(subGroup, "Value", "Text", Product.SubGroup ?? 0);

            ViewData["ColorsAndQuantities"] = _productService.GetProductColorsForShow(id);

            ViewData["SizesAndQuantities"] = _productService.GetProductSizesForShow(id);
        }

        public IActionResult OnPost(IFormFile? imgProductUp, List<int>? SelectedColors, List<string>? ColorQuantities, List<int>? SelectedSizes, List<string>? SizeQuantities)
        {
            if (!ModelState.IsValid)
                return Page();

            _productService.UpdateProduct(Product,imgProductUp);
            _productService.UpdateColors(Product.ProductId, SelectedColors, ColorQuantities);
            _productService.UpdateSizes(Product.ProductId, SelectedSizes, SizeQuantities);

            return RedirectToPage("Index");
        }
    }
}