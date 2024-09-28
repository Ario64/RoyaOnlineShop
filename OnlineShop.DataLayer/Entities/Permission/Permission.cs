using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DataLayer.Entities.Permission;

public class Permission
{
    public int PermissionId { get; set; }

    public int? ParentId { get; set; }

    [DisplayName("عنوان نقش")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public string PermissionTitle { get; set; }

    #region Relations

    public Permission? _Permission { get; set; }
    public List<Permission>? Permissions { get; set; }
    public List<RolePermission>? RolePermissions { get; set; }

    #endregion
}