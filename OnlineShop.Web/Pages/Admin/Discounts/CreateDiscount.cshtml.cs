using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.Security;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.Order;

namespace OnlineShop.Web.Pages.Discounts
{
    [PermissionChecker(21)]
    public class CreateDiscountModel : PageModel
    {
        private IOrderService _orderService;

        public CreateDiscountModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [BindProperty]
        public Discount Discount { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost(string? startDate = "", string? endDate = "")
        {
            if (startDate != "")
            {
                string[] stD = startDate.Split('/');
                Discount.StartDate = new DateTime(int.Parse(stD[0])
                    , int.Parse(stD[1])
                    , int.Parse(stD[2])
                    , new PersianCalendar());
            }

            if (endDate != "")
            {
                string[] endD = endDate.Split('/');
                Discount.EndDate = new DateTime(int.Parse(endD[0])
                    , int.Parse(endD[1])
                    , int.Parse(endD[2])
                    , new PersianCalendar());
            }

            if (!ModelState.IsValid || _orderService.IsExistCode(Discount.DiscountCode))
                return Page();

            _orderService.AddDiscount(Discount);
            return RedirectToPage("Index");
        }

        //admin/discounts/creatediscount?handler=checkcode
        //admin/discounts/creatediscount/checkcode

        public IActionResult OnGetCheckDiscountCode(string code)
        {
            return Content(_orderService.IsExistCode(code).ToString());
        }

    }
}
