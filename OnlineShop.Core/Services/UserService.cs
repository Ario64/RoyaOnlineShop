﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using OnlineShop.Core.Convertors;
using OnlineShop.Core.DTOs.User;
using OnlineShop.Core.DTOs.Wallet;
using OnlineShop.Core.Generators;
using OnlineShop.Core.Security;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Contexts;
using OnlineShop.DataLayer.Entities.User;
using OnlineShop.DataLayer.Entities.Wallet;

namespace OnlineShop.Core.Services;

public class UserService : IUserService
{
    private readonly RoyaContext _context;

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

    public User GetUserByUserId(int userId)
    {
        return _context.Users.Find(userId);
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
        information.Wallet = UserBalance(user.UserName);
        return information;
    }

    public UserInformationViewModel GetUserInformationForUserPanel(int userId)
    {
        var user = GetUserByUserId(userId);
        UserInformationViewModel information = new UserInformationViewModel();
        information.FullName = user.FullName;
        information.UserName = user.UserName;
        information.Email = user.Email;
        information.Phone = user.PhoneNumber;
        information.Address = user.Address;
        information.RegisterDate = user.RegisterDate;
        information.Wallet = UserBalance(user.UserName);
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
        var user = GetUserByUserName(userName);
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

    public int UserBalance(string userName)
    {
        int userId = GetUserIdByUserName(userName);
        var deposit = _context.Wallets
            .Where(w => w.UserId == userId && w.WalletTypeId == 1 && w.IsPayed)
            .Select(s => s.Amount)
            .ToList();
        var withdraw = _context.Wallets
            .Where(w => w.UserId == userId && w.WalletTypeId == 2 && w.IsPayed)
            .Select(s => s.Amount)
            .ToList();
        return (deposit.Sum() - withdraw.Sum());
    }

    public List<WalletViewModel> GetUserWallets(string userName)
    {
        int userId = GetUserIdByUserName(userName);
        return _context.Wallets
            .Where(w => w.UserId == userId && w.IsPayed)
            .Select(s =>
                new WalletViewModel()
                {
                    CreateDate = s.CreateDate,
                    Amount = s.Amount,
                    TypeId = s.WalletTypeId,
                    Description = s.Description
                })
            .ToList();
    }

    public int ChargeWallet(string userName, int amount, string description, bool isPayed = false)
    {
        int userId = GetUserIdByUserName(userName);
        Wallet wallet = new Wallet()
        {
            Amount = amount,
            CreateDate = DateTime.Now,
            UserId = userId,
            WalletTypeId = 1,
            Description = description,
            IsPayed = isPayed
        };
        return AddWallet(wallet);
    }

    public int AddWallet(Wallet wallet)
    {
        _context.Wallets.Add(wallet);
        _context.SaveChanges();
        return wallet.WalletId;
    }

    public Wallet GetWalletByWalletId(int walletId)
    {
        return _context.Wallets.Find(walletId);
    }

    public void UpdateWallet(Wallet wallet)
    {
        _context.Wallets.Update(wallet);
        _context.SaveChanges();
    }

    public UsersForAdminPanelViewModel GetUsersForAdminPanel(int pageId = 1, string filterEmail = "", string filterUserName = "")
    {
        IQueryable<User> result = _context.Users;

        if (!string.IsNullOrEmpty(filterEmail))
        {
            result = result.Where(w => w.Email.Contains(filterEmail));
        }

        if (!string.IsNullOrEmpty(filterUserName))
        {
            result = result.Where(w => w.UserName.Contains(filterUserName));
        }

        int take = 20;
        int skip = (pageId - 1) * take;
        var list = new UsersForAdminPanelViewModel();
        list.CurrentPage = pageId;
        int quantity = result.OrderByDescending(o => o.RegisterDate).Count();
        if (quantity % 2 == 0)
        {
            list.PageCount = quantity / take;
        }
        else
        {
            list.PageCount = (quantity / take) + 1;
        }
        list.Users = result.OrderByDescending(o => o.RegisterDate).Skip(skip).Take(take).ToList();
        return list;
    }

    public int AddUserByAdmin(CreateUserViewModel user)
    {
        User newUser = new User();
        if (user.UserAvatar != null)
        {
            newUser.UserAvatar = NameGenerator.GenerateUniqueName() + Path.GetExtension(user.UserAvatar.FileName);
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/useravatar/", newUser.UserAvatar);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                user.UserAvatar.CopyTo(stream);
            }
        }
        else
        {
            newUser.UserAvatar = "DefaultAvatar.jpg";
        }
        newUser.FullName = user.FullName;
        newUser.UserName = user.UserName;
        newUser.Email = user.Email;
        newUser.PhoneNumber = user.PhoneNumber;
        newUser.Address = user.Address;
        newUser.Password = PasswordHelper.EncodePasswordMd5(user.Password);
        newUser.ActiveCode = NameGenerator.GenerateUniqueName();
        newUser.IsActive = true;
        newUser.RegisterDate = DateTime.Now;
        return AddUser(newUser);
    }

    public EditUserViewModel GetUserForEditByAdmin(int userId)
    {
        return _context.Users
            .Where(w => w.UserId == userId)
            .Select(s => new EditUserViewModel()
            {
                UserId = s.UserId,
                UserName = s.UserName,
                FullName = s.FullName,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                Address = s.Address,
                AvatarName = s.UserAvatar,
                UserRoles = s.UserRoles.Select(s => s.RoleId).ToList()
            }).Single();
    }

    public void EditUserByAdmin(EditUserViewModel editUser)
    {
        User user = GetUserByUserId(editUser.UserId);

        #region Save Image

        if (editUser.UserAvatar != null)
        {
            string imagePath = "";
            if (editUser.AvatarName != "DefaultAvatar.jpg")
            {
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/useravatar/", editUser.AvatarName);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }

            user.UserAvatar = NameGenerator.GenerateUniqueName() + Path.GetExtension(editUser.UserAvatar.FileName);
            imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/useravatar/", user.UserAvatar);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                editUser.UserAvatar.CopyTo(stream);
            }
        }

        #endregion

        if (FixedText.FixedEmail(editUser.Email) != user.Email)
        {
            if (IsEmailExist(FixedText.FixedEmail(editUser.Email)))
            {
                return;
            }
        }
        user.Email = FixedText.FixedEmail(editUser.Email);

        if (editUser.PhoneNumber != user.PhoneNumber)
        {
            if (IsPhoneNumberExist(editUser.PhoneNumber))
            {
                return;
            }
        }
        user.PhoneNumber = editUser.PhoneNumber;

        if (!string.IsNullOrEmpty(editUser.Password))
        {
            user.Password = PasswordHelper.EncodePasswordMd5(editUser.Password);
        }

        user.Address = editUser.Address;
        _context.Update(user);
        _context.SaveChanges();
    }

    public UsersForAdminPanelViewModel GetDeletedUsersForAdminPanel(int pageId = 1, string filterEmail = "", string filterUserName = "")
    {
        IQueryable<User> result = _context.Users.IgnoreQueryFilters().Where(w => w.IsDeleted);

        if (!string.IsNullOrEmpty(filterEmail))
        {
            result = result.Where(w => w.Email.Contains(filterEmail));
        }

        if (!string.IsNullOrEmpty(filterUserName))
        {
            result = result.Where(w => w.UserName.Contains(filterUserName));
        }

        int take = 20;
        int skip = (pageId - 1) * take;
        var list = new UsersForAdminPanelViewModel();
        list.CurrentPage = pageId;
        int quantity = result.OrderByDescending(o => o.RegisterDate).Count();
        if (quantity % 2 == 0)
        {
            list.PageCount = quantity / take;
        }
        else
        {
            list.PageCount = (quantity / take) + 1;
        }
        list.Users = result.OrderByDescending(o => o.RegisterDate).Skip(skip).Take(take).ToList();
        return list;
    }

    public void DeleteUserByAdmin(int userId)
    {
        var user = GetUserByUserId(userId);
        user.IsActive = false;
        user.IsDeleted = true;
        UpdateUser(user);
    }
}