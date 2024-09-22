using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.DTOs.User;
using OnlineShop.Core.Services.Interfaces;

namespace OnlineShop.Web.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public UsersForAdminPanelViewModel UsersForAdmin { get; set; }

        public void OnGet(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            UsersForAdmin = _userService.GetUsersForAdminPanel(pageId,filterEmail,filterUserName);
        }
    }
}
