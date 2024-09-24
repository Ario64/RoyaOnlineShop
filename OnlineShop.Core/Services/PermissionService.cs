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
}