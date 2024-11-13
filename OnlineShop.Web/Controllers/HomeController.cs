using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Core.Services.Interfaces;

namespace OnlineShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _userService;
        private IProductService _productService;

        public HomeController(IUserService userService, IProductService productService)
        {
            _userService = userService;
            _productService = productService;
        }

        #region Index

        public IActionResult Index()
        {
            ViewBag.PopularProduct = _productService.GetPopularProducts();
            return View(_productService.GetProducts().Item1);
        }

        #endregion

        #region Online Payment

        [Route("OnlinePayment/{id}")]
        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];
                var wallet = _userService.GetWalletByWalletId(id);
                var payment = new ZarinpalSandbox.Payment(wallet.Amount);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    ViewBag.Code = res.RefId;
                    ViewBag.IsSucces = true;
                    wallet.IsPayed = true;
                    _userService.UpdateWallet(wallet);
                }
            }
            return View();
        }

        #endregion

        #region Check Email Exist  Ajax

        public void IsEmailExist(string email)
        {
            _userService.IsEmailExist(email);
        }

        #endregion

        #region Return Sub Main Grpoups Ajax

        public IActionResult GetSubGroups(int id)
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = "انتخاب کنید !", Value = "0" }
            };
            list.AddRange(_productService.GetSubMainGroup(id));
            return Json(new SelectList(list, "Value", "Text"));
        }

        #endregion

        #region ContactUs
        [Route("contactus")]
        public IActionResult ContactUs()
        {
            return View();
        }

        #endregion

    }
}
