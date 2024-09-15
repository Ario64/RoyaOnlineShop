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

    public User? GetUserByActiveCode(string activeCode)
    {
        return _context.Users.SingleOrDefault(s => s.ActiveCode == activeCode);
    }

    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public User GetUserByUserName(string userName)
    {
        return _context.Users.SingleOrDefault(s => s.UserName == userName);
    }

    public int GetUserIdByUserName(string userName)
    {
        return _context.Users.SingleOrDefault(s => s.UserName == userName).UserId;
    }

    public UserInformationViewModel GetUserInformationForUserPanel(string userName)
    {
        var user = GetUserByUserName(userName);
        UserInformationViewModel information = new UserInformationViewModel();
        information.FullName = user.FullName;
        information.UserName = user.UserName;
        information.Email = user.Email;
        information.Phone = user.PhoneNumber;
        information.Address = user.Address;
        information.RegisterDate = user.RegisterDate;
        information.Wallet = 0;
        return information;
    }

    public SideBarUserPanelViewModel GetUserInformationForSideBar(string userName)
    {
        return _context.Users
            .Where(w => w.UserName == userName)
            .Select(s =>
                new SideBarUserPanelViewModel()
                {
                    FullName = s.FullName,
                    UserImage = s.UserAvatar,
                    RegisterDate = s.RegisterDate
                }).SingleOrDefault();
    }

    public EditUserInformationViewModel GetUserInformationForEditProfile(string userName)
    {
        return _context.Users.Where(w => w.UserName == userName)
            .Select(s =>
                new EditUserInformationViewModel()
                {
                    PhoneNumber = s.PhoneNumber,
                    Address = s.Address,
                    AvatarName = s.UserAvatar
                }).Single();
    }

    public bool EditProfile(string userName, EditUserInformationViewModel profile)
    {
        if (profile.UserAvatar != null)
        {
            string imagePath = "";
            if (profile.AvatarName != "DefaultAvatar.jpg")
            {
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/useravatar/", profile.AvatarName);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
            profile.AvatarName = NameGenerator.GenerateUniqueName() + Path.GetExtension(profile.UserAvatar.FileName);
            imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/useravatar/", profile.AvatarName);
            using (FileStream stream = new FileStream(imagePath, FileMode.Create))
            {
                profile.UserAvatar.CopyTo(stream);
            }
        }

        var user = GetUserByUserName(userName);
        if (user.PhoneNumber != profile.PhoneNumber)
        {
            if (IsPhoneNumberExist(profile.PhoneNumber))
            {
                return false;
            }
        }

        user.PhoneNumber = profile.PhoneNumber;
        user.Address = profile.Address;
        user.UserAvatar = profile.AvatarName;
        UpdateUser(user);
        return true;
    }

    public bool ChangePassword(string userName, ChangePasswordViewModel model)
    {
        var user = _context.Users.SingleOrDefault(s => s.UserName == userName);
        if (user == null)
        {
            return false;
        }

        if (user.Password != PasswordHelper.EncodePasswordMd5(model.CurrentPassword))
        {
            return false;
        }

        user.Password = PasswordHelper.EncodePasswordMd5(model.Password);
        UpdateUser(user);
        return true;
    }
}