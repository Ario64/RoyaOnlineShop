﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Core.Security;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.Product;

namespace OnlineShop.Web.Pages.Admin.Products
{
    [PermissionChecker(13)]
    public class AddProductModel : PageModel
    {
        private IProductService _productService;

        public AddProductModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Product Product { get; set; }

        public void OnGet()
        {
            var mainGroups = _productService.GetMainGroup();
            ViewData["MainGroups"] = new SelectList(mainGroups, "Value", "Text");

            var subGroup = _productService.GetSubMainGroup(int.Parse(mainGroups.First().Value));
            ViewData["SubMainGroups"] = new SelectList(subGroup, "Value", "Text");


            ViewData["Sizes"] = _productService.GetSizes();
        }

        public IActionResult OnPost(IFormFile? imgProductUp, List<int>? SelectedSizes, List<int>? SizeQuantities)
        {
            
            int productId = _productService.AddProduct(Product, imgProductUp);
            _productService.AddSizeToProductByAdmin(productId, SelectedSizes, SizeQuantities);
            return RedirectToPage("Index");
        }
    }
}
