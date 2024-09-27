namespace OnlineShop.DataLayer.Entities.Permission;

public class Permission
{
    public int PermissionId { get; set; }
    public int ParentId { get; set; }
    public string PermissionTitle { get; set; }

    #region Relations

    public Permission? PermissionList { get; set; }
    public List<RolePermission>? RolePermissions { get; set; }

    #endregion
}