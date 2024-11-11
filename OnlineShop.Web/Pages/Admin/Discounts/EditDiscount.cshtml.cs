using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.Order;
using System.Globalization;
using OnlineShop.Core.Security;

namespace OnlineShop.Web.Pages.Admin.Discounts
{
    [PermissionChecker(22)]
    public class EditDiscountModel : PageModel
    {
        private IOrderService _orderService;

        public EditDiscountModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [BindProperty]
        public Discount Discount { get; set; }

        public void OnGet(int id)
        {
            Discount = _orderService.GetDiscountById(id);
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

            _orderService.UpdateDiscount(Discount);
            return RedirectToPage("Index");
        }

        public IActionResult OnGetCheckDiscountCode(string code)
        {
            return Content(_orderService.IsExistCode(code).ToString());
        }

    }
}
