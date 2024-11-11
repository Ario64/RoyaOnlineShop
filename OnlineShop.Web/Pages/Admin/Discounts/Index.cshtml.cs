using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.Security;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.Order;

namespace OnlineShop.Web.Pages.Discounts
{
    [PermissionChecker(20)]
    public class IndexModel : PageModel
    {
        private IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public List<Discount> Discounts { get; set; }

        public void OnGet()
        {
            Discounts = _orderService.GetAllDiscounts();
        }
    }
}
