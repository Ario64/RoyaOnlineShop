using OnlineShop.Core.DTOs.User;
using OnlineShop.DataLayer.Entities.Permission;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.Core.Services.Interfaces;

public interface IPermissionService
{
    #region Roles Actions

    List<Role> GetRoles();
    void AddRolesToUser(int userId, List<int> roleIdList);
    void EditUserRolesByAdmin(int userId, List<int> userRoles);
    int AddRoleToRoles(Role role);
    Role GetRoleByRoleId(int roleId);
    void UpdateRoleByAdmin(Role role);
    void DeleteRoleByAdmin(Role role);

    #endregion

    #region Permission Actions

    List<Permission> GetPermissions();
    void AddPermissionsToRole(int roleId, List<int> permissionIdList);
    List<int> GetPermissionsRole(int roleId);
    void UpdateRolePermissions(int roleId, List<int> permissionIdList);
    bool CheckPermission(string userName, int permissionId);

    #endregion
}