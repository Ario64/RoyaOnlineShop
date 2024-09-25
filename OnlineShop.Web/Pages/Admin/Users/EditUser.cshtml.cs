using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.Convertors;
using OnlineShop.Core.DTOs.User;
using OnlineShop.Core.Services;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.Web.Pages.Admin.Users
{
    public class EditUserModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;

        public EditUserModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        [BindProperty]
        public EditUserViewModel User { get; set; }

        public void OnGet(int id)
        {
            ViewData["Roles"] = _permissionService.GetRoles();
            User = _userService.GetUserForEditByAdmin(id);
        }

        public IActionResult OnPost(List<int> selectedRoles)
        {
            ViewData["Roles"] = _permissionService.GetRoles();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _userService.EditUserByAdmin(User);
            _permissionService.EditUserRolesByAdmin(User.UserId, selectedRoles);
            return RedirectToPage("Index");

        }
    }
}
