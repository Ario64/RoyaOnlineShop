using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.DTOs.User;
using OnlineShop.Core.Services.Interfaces;

namespace OnlineShop.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        #region Index

        public IActionResult Index()
        {
            return View(_userService.GetUserInformationForUserPanel(User.Identity.Name));
        }

        #endregion

        #region Edit Profile

        [HttpGet("UserPanel/EditProfile")]
        public IActionResult EditProfile()
        {
            return View(_userService.GetUserInformationForEditProfile(User.Identity.Name));
        }

        [HttpPost("UserPanel/EditProfile")]
        public IActionResult EditProfile(EditUserInformationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ViewBag.IsSuccess = _userService.EditProfile(User.Identity.Name, model);
            return View(_userService.GetUserInformationForEditProfile(User.Identity.Name));
        }

        #endregion

    }
}
