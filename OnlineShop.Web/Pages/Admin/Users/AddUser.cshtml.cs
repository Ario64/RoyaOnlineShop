using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.DTOs.User;
using OnlineShop.Core.Security;
using OnlineShop.Core.Services.Interfaces;

namespace OnlineShop.Web.Pages.Admin.Users
{
    [PermissionChecker(3)]
    public class AddUserModel : PageModel
    {
        private IPermissionService _permissionService;
        private IUserService _userService;

        public AddUserModel(IPermissionService permissionService, IUserService userService)
        {
            _permissionService = permissionService;
            _userService = userService;
        }

        [BindProperty]
        public CreateUserViewModel CreateUser { get; set; }

        public void OnGet()
        {
            ViewData["Roles"] = _permissionService.GetRoles();
        }

        public IActionResult OnPost(List<int> selectedRoles)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Roles"] = _permissionService.GetRoles();
                return Page();
            }
            if (_userService.IsUserNameExist(CreateUser.UserName))
            {
                ViewData["Roles"] = _permissionService.GetRoles();
                ModelState.AddModelError("", "نام کاربری وارد شده معتبر نیست !");
                return Page();
            }
            if (_userService.IsEmailExist(CreateUser.Email))
            {
                ViewData["Roles"] = _permissionService.GetRoles();
                ModelState.AddModelError("", " ایمیل وارد شده معتبر نیست !");
                return Page();
            }
            if (_userService.IsPhoneNumberExist(CreateUser.PhoneNumber))
            {
                ViewData["Roles"] = _permissionService.GetRoles();
                ModelState.AddModelError("", " موبایل وارد شده معتبر نیست !");
                return Page();
            }
            ViewData["Roles"] = _permissionService.GetRoles();
            int userId = _userService.AddUserByAdmin(CreateUser);
            _permissionService.AddRolesToUser(userId, selectedRoles);
            return Redirect("/admin/users");
        }

    }
}
