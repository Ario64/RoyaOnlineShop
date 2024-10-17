using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.DTOs.User;
using OnlineShop.Core.Security;
using OnlineShop.Core.Services.Interfaces;

namespace OnlineShop.Web.Pages.Admin.Users
{
    [PermissionChecker(7)]
    public class DeletedUsersModel : PageModel
    {
        private IUserService _userService;

        public DeletedUsersModel(IUserService userService)
        {
            _userService = userService;
        }

        public UsersForAdminPanelViewModel UsersForAdmin { get; set; }

        public void OnGet(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            UsersForAdmin = _userService.GetDeletedUsersForAdminPanel(pageId, filterEmail, filterUserName);
        }
    }
}
