using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OnlineShop.Core.Services.Interfaces;

namespace OnlineShop.Web.ViewComponents;

public class ProductGroupComponent : ViewComponent
{
    private IProductService _productService;

    public ProductGroupComponent(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult((IViewComponentResult) View("ProductGroup",_productService.GetGroups()));
    }
}