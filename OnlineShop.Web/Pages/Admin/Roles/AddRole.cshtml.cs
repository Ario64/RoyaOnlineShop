using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.Web.Pages.Admin.Roles
{
    public class AddRoleModel : PageModel
    {
        private IPermissionService _permissionService;

        public AddRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public Role Role { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            int roleId = _permissionService.AddRoleToRoles(Role);

            return RedirectToPage("Index");
        }

    }
}
