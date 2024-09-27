using OnlineShop.Core.DTOs.User;
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

}