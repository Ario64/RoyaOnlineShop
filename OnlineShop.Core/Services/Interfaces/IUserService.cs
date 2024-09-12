using OnlineShop.Core.DTOs.User;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.Core.Services.Interfaces;

public interface IUserService
{
    #region Register And Login Actions

    bool IsUserNameExist(string userName);
    bool IsEmailExist(string email);
    bool IsPhoneNumberExist(string phoneNumber);
    int AddUser(User user);
    User LoginUser(LoginViewModel login);
    bool ActiveAccount(string activeCode);
    User? GetUserByEmailAddress(string email);
    User? GetUserByActiveCode(string activeCode);
    void UpdateUser(User user);
    User GetUserByUserName(string userName);
    #endregion

    #region User Panel Actions

    UserInformationViewModel GetUserInformationForUserPanel(string userName);

    #endregion

}