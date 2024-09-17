using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NuGet.Protocol;
using OnlineShop.Core.DTOs.Wallet;
using OnlineShop.Core.Services.Interfaces;

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

        [HttpGet("UserPanel/Wallet")]
        public IActionResult Index()
        {
            ViewBag.WalletList = _userService.GetUserWallets(User.Identity.Name);
            return View();
        }

        [HttpPost("UserPanel/Wallet")]
        public IActionResult Index(ChargeWalletViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Amount <= 0)
            {
                ModelState.AddModelError("Amount","لطفا مبلغی بیشتر از صفر وارد کنید !");
                return View(model);
            }
            return View();
        }


    }
}
