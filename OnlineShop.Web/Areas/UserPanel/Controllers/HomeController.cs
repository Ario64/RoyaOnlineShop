using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Convertors;
using OnlineShop.Core.DTOs.User;
using OnlineShop.Core.Security;
using OnlineShop.Core.Senders;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private IUserService _userService;
        private IViewRenderService _viewRenderService;

        public HomeController(IUserService userService, IViewRenderService viewRenderService)
        {
            _userService = userService;
            _viewRenderService = viewRenderService;
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

        #region Change Password

        [HttpGet("UserPanel/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost("UserPanel/ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            var user = _userService.GetUserByUserName(User.Identity.Name);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ViewBag.IsSuccess = _userService.ChangePassword(User.Identity.Name,model);
            if (ViewBag.IsSuccess == true)
            {
                var body = _viewRenderService.RenderToStringAsync("_ChangePassword", user);
                SendEmail.Send(user.Email, "تغییر کلمه عبور", body);
                return Redirect("/login?ChangePassword=true");
            }
            return View(model);
        }

        #endregion 

    }
}
