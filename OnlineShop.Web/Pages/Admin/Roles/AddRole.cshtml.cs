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
            ViewData["PermissionList"] = _permissionService.GetPermissions();
        }

        public IActionResult OnPost(List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
                return Page();
            int roleId = _permissionService.AddRoleToRoles(Role);
            _permissionService.AddPermissionsToRole(Role.RoleId, SelectedPermission);
            return RedirectToPage("Index");
        }

    }
}
