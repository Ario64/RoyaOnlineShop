using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Core.DTOs.User;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.Web.Pages.Admin.Roles
{
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
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _permissionService.UpdateRoleByAdmin(Role);
            return RedirectToPage("Index");
        }
    }
}
