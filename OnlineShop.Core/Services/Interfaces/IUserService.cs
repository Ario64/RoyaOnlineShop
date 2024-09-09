using OnlineShop.Core.DTOs.User;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.Core.Services.Interfaces;

public interface IUserService
{
    #region User Account

    bool IsUserNameExist(string userName);
    bool IsEmailExist(string email);
    bool IsPhoneNumberExist(string phoneNumber);
    int AddUser(User user);
    User LoginUser(LoginViewModel login);
    bool ActiveAccount(string activeCode);
    User? GetUserByEmailAddress(string email);

    #endregion

}