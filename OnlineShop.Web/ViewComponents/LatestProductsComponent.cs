using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Services.Interfaces;

namespace OnlineShop.Web.ViewComponents;

public class LatestProductsComponent : ViewComponent
{
    private IProductService _productService;

    public LatestProductsComponent(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult((IViewComponentResult)View("LatestProducts",_productService.GetProducts().Item1));
    }
}