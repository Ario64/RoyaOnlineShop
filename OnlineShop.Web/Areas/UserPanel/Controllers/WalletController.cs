using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.DTOs.Wallet;
using OnlineShop.Core.Services.Interfaces;
using ZarinpalSandbox;


namespace OnlineShop.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class WalletController : Controller
    {
        private IUserService _userService;

        public WalletController(IUserService userService)
        {
            _userService = userService;
        }

        #region Index

        [HttpGet("UserPanel/Wallet")]
        public IActionResult Index()
        {
            ViewBag.WalletList = _userService.GetUserWallets(User.Identity.Name);
            return View();
        }

        #endregion

        #region Charge Wallet

        [HttpPost("UserPanel/Wallet")]
        public IActionResult Index(ChargeWalletViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.WalletList = _userService.GetUserWallets(User.Identity.Name);
                return View(model);
            }

            if (model.Amount <= 0)
            {
                ViewBag.WalletList = _userService.GetUserWallets(User.Identity.Name);
                ModelState.AddModelError("Amount", "لطفا مبلغی بیشتر از صفر وارد کنید !");
                return View(model);
            }

            ViewBag.WalletList = _userService.GetUserWallets(User.Identity.Name);
            int walletId = _userService.ChargeWallet(User.Identity.Name, model.Amount, "شارژ حساب");

            #region Online Payment

            Payment payment = new Payment(model.Amount);
            var res = payment.PaymentRequest("شارژ حساب", "httpss://localhost:7189/OnlinePayment/" + walletId);
            if (res.Result.Status == 100)
            {
                return Redirect("httpss://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }

            #endregion

            return BadRequest();
        }

        #endregion

    }
}
