using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.DTOs.User;
using OnlineShop.Core.Security;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.Web.Pages.Admin.Roles
{
    [PermissionChecker(13)]
    public class EditRoleModel : PageModel
    {
        private IPermissionService _permissionService;

        public EditRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public Role Role { get; set; }

        public void OnGet(int id)
        {
            Role = _permissionService.GetRoleByRoleId(id);
            ViewData["PermissionList"] = _permissionService.GetPermissions();
            ViewData["SelectedPermissions"] = _permissionService.GetPermissionsRole(id);
        }

        public IActionResult OnPost(List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _permissionService.UpdateRoleByAdmin(Role);
            _permissionService.UpdateRolePermissions(Role.RoleId, SelectedPermission);
            return RedirectToPage("Index");
        }
    }
}
