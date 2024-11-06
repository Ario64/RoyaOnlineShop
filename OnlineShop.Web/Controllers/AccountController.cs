using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Convertors;
using OnlineShop.Core.DTOs.User;
using OnlineShop.Core.Generators;
using OnlineShop.Core.Security;
using OnlineShop.Core.Senders;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IViewRenderService _viewRenderService;

        public AccountController(IUserService userService, IViewRenderService viewRenderService)
        {
            _userService = userService;
            _viewRenderService = viewRenderService;
        }

        #region Register

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            if (_userService.IsUserNameExist(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری وارد شده تکراری است !");
                return View(register);
            }

            if (_userService.IsEmailExist(FixedText.FixedEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل وارد شده قبلا ثبت نام کرده است !");
                return View(register);
            }

            if (_userService.IsPhoneNumberExist(register.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "شماره تلفن وارد شده قبلا ثبت نام کرده است !");
                return View(register);
            }

            var user = new User()
            {
                FullName = register.FullName,
                UserName = register.UserName,
                Email = FixedText.FixedEmail(register.Email),
                PhoneNumber = register.PhoneNumber,
                Address = register.Address,
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                ActiveCode = NameGenerator.GenerateUniqueName(),
                RegisterDate = DateTime.Now,
                UserAvatar = "DefaultAvatar.jpg"
            };

            _userService.AddUser(user);

            #region Send Activation Email

            var body = _viewRenderService.RenderToStringAsync("_ActiveEmail", user);
            SendEmail.Send(user.Email, "فعالسازی حساب", body);

            #endregion

            return View("SuccessRegister", user);
        }

        #endregion

        #region Login

        [HttpGet("login")]
        public IActionResult Login(bool ChangePassword = false, string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.ChangePassword = ChangePassword;
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginViewModel login, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            ViewBag.ReturnUrl = returnUrl;

            var user = _userService.LoginUser(login);

            if (user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.GivenName, user.FullName)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties()
                    {
                        IsPersistent = login.RememberMe
                    };

                    HttpContext.SignInAsync(principal, properties);

                    ViewBag.IsLogin = true;
                    return View("Login");
                }
                else
                {
                    ModelState.AddModelError("Email", " حساب کاربری شما فعال نمی باشد !");
                    return View(login);
                }
            }

            ModelState.AddModelError("Email", "اطلاعات وارد شده صحیح نیست !");
            return View(login);
        }

        #endregion

        #region Active Account

        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userService.ActiveAccount(id);
            return View();
        }

        #endregion

        #region ReSend Activation Email

        [HttpGet("active-account")]
        public IActionResult ReSendActivationEmail()
        {
            return View();
        }

        [HttpPost("active-account")]
        public IActionResult ReSendActivationEmail(ResendActivationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _userService.GetUserByEmailAddress(model.Email);
            if (user != null)
            {
                if (user.IsActive)
                {
                    ModelState.AddModelError("Email", "حساب شما فعال می باشد !");
                    return View(model);
                }
                else
                {
                    var body = _viewRenderService.RenderToStringAsync("_ActiveEmail", user);
                    SendEmail.Send(user.Email, "فعالسازی حساب", body);
                    ViewBag.SendActivationEmail = true;
                    return View("ReSendActivationEmail");
                }
            }
            ModelState.AddModelError("Email", "کاربری یافت نشد !");
            return View(model);
        }

        #endregion

        #region Logout

        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("login");
        }

        #endregion

        #region Forgot Password

        [HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User? user = _userService.GetUserByEmailAddress(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "کاربری با ایمیل وارد شده یافت نشد !");
                return View(model);
            }
            string emailBody = _viewRenderService.RenderToStringAsync("_ForgotPassword", user);
            SendEmail.Send(user.Email, "بازیابی کلمه عبور", emailBody);
            ViewBag.IsSuccess = true;
            return View();
        }

        #endregion

        #region Reset Password

        [HttpGet]
        public IActionResult ResetPassword(string id)
        {
            return View(new ResetPasswordViewModel()
            {
                ActiveCode = id
            });
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.GetUserByActiveCode(model.ActiveCode);
            if (user == null)
            {
                return NotFound();
            }

            string hashPassword = PasswordHelper.EncodePasswordMd5(model.Password);
            user.Password = hashPassword;
            user.ActiveCode = NameGenerator.GenerateUniqueName();
            _userService.UpdateUser(user);
            var body = _viewRenderService.RenderToStringAsync("_ResetPassword", user);
            SendEmail.Send(user.Email, "تغییر کلمه عبور", body);
            ViewBag.EmailSent = true;
            return View("Login");
        }

        #endregion
    }
}
