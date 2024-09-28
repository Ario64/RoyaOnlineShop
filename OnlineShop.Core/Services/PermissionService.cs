using OnlineShop.Core.DTOs.User;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Contexts;
using OnlineShop.DataLayer.Entities.Permission;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.Core.Services;

public class PermissionService : IPermissionService
{
    private RoyaContext _context;

    public PermissionService(RoyaContext context)
    {
        _context = context;
    }

    public List<Role> GetRoles()
    {
        return _context.Roles.ToList();
    }

    public void AddRolesToUser(int userId, List<int> roleIdList)
    {
        foreach (int roleId in roleIdList)
        {
            _context.UserRoles.Add(new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            });
        }

        _context.SaveChanges();
    }

    public void EditUserRolesByAdmin(int userId, List<int> userRoles)
    {
        _context.UserRoles
            .Where(w => w.UserId == userId)
            .ToList()
            .ForEach(f => _context.UserRoles.Remove(f));
        AddRolesToUser(userId, userRoles);
    }

    public int AddRoleToRoles(Role role)
    {
        _context.Roles.Add(role);
        _context.SaveChanges();
        return role.RoleId;
    }

    public Role GetRoleByRoleId(int roleId)
    {
        return _context.Roles.Find(roleId);
    }

    public void UpdateRoleByAdmin(Role role)
    {
        _context.Roles.Update(role);
        _context.SaveChanges();
    }

    public void DeleteRoleByAdmin(Role role)
    {
        role.IsDeleted = true;
        UpdateRoleByAdmin(role);
    }

    public List<Permission> GetPermissions()
    {
        return _context.Permissions.ToList();
    }

    public void AddPermissionsToRole(int roleId, List<int> permissionIdList)
    {
        foreach (int permissionId in permissionIdList)
        {
            _context.RolePermissions.Add(new RolePermission()
            {
                RoleId = roleId,
                PermissionId = permissionId
            });
        }

        _context.SaveChanges();
    }

    public List<int> GetPermissionsRole(int roleId)
    {
        return _context.RolePermissions
            .Where(w => w.RoleId == roleId)
            .Select(s => s.PermissionId)
            .ToList();
    }

    public void UpdateRolePermissions(int roleId, List<int> permissionIdList)
    {
        _context.RolePermissions
            .Where(w => w.RoleId == roleId)
            .ToList()
            .ForEach(f => _context.RolePermissions.Remove(f));
        AddPermissionsToRole(roleId, permissionIdList);
    }
}
