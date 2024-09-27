using System;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.DataLayer.Entities.Permission;

public class RolePermission
{
    public int RpId { get; set; }
    public int RoleId { get; set; }
    public int PermissionId { get; set; }

    #region Relations

    public Role? Role { get; set; }
    public Permission? Permission { get; set; }

    #endregion
}