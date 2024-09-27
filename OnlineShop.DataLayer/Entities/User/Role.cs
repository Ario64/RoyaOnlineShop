using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using OnlineShop.DataLayer.Entities.Permission;

namespace OnlineShop.DataLayer.Entities.User;

public class Role
{
    public int RoleId { get; set; }

    [DisplayName("عنوان نقش")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(30, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public string RoleTitle { get; set; }

    public bool IsDeleted { get; set; }

    #region Relations

    public List<UserRole>? UserRoles { get; set; }
    public List<RolePermission>? RolePermissions { get; set; }

    #endregion
}