using OnlineShop.Core.DTOs.User;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Contexts;
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
}