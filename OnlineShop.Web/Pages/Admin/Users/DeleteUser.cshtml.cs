using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.DTOs.User;
using OnlineShop.Core.Security;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.Web.Pages.Admin.Users
{
    [PermissionChecker(5)]
    public class DeleteUserModel : PageModel
    {
        private IUserService _userService;

        public DeleteUserModel(IUserService userService)
        {
            _userService = userService;
        }

        public UserInformationViewModel User { get; set; }

        public void OnGet(int id)
        {
            ViewData["UserId"] = id;
            User = _userService.GetUserInformationForUserPanel(id);
        }

        public IActionResult OnPost(int UserId)
        {
            _userService.DeleteUserByAdmin(UserId);
            return RedirectToPage("Index");
        }
    }
}
