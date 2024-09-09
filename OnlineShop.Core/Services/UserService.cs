using OnlineShop.Core.Convertors;
using OnlineShop.Core.DTOs.User;
using OnlineShop.Core.Generators;
using OnlineShop.Core.Security;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Contexts;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.Core.Services;

public class UserService : IUserService
{
    private RoyaContext _context;

    public UserService(RoyaContext context)
    {
        _context = context;
    }

    public bool IsUserNameExist(string userName)
    {
        return _context.Users.Any(a => a.UserName == userName);
    }

    public bool IsEmailExist(string email)
    {
        return _context.Users.Any(a => a.Email == email);
    }

    public bool IsPhoneNumberExist(string phoneNumber)
    {
        return _context.Users.Any(a => a.PhoneNumber == phoneNumber);
    }

    public int AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user.UserId;
    }

    public User LoginUser(LoginViewModel login)
    {
        string hashPassword = PasswordHelper.EncodePasswordMd5(login.Password);
        string email = FixedText.FixedEmail(login.Email);
        return _context.Users.SingleOrDefault(a => a.Email == email && a.Password == hashPassword);
    }

    public bool ActiveAccount(string activeCode)
    {
        var user = _context.Users.SingleOrDefault(s => s.ActiveCode == activeCode);
        if (user == null)
        {
            return false;
        }

        if (user.IsActive)
        {
            return false;
        }

        user.IsActive = true;
        user.ActiveCode = NameGenerator.GenerateUniqueName();
        _context.SaveChanges();
        return true;
    }

    public User? GetUserByEmailAddress(string email)
    {
        var newEmail = FixedText.FixedEmail(email);
        var user = _context.Users.SingleOrDefault(w => w.Email == newEmail);
        return user;
    }
}