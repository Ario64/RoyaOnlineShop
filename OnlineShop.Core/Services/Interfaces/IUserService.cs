using OnlineShop.Core.DTOs.User;
using OnlineShop.Core.DTOs.Wallet;
using OnlineShop.DataLayer.Entities.User;
using OnlineShop.DataLayer.Entities.Wallet;

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
    int GetUserIdByUserName(string userName);

    #endregion

    #region User Panel Actions

    UserInformationViewModel GetUserInformationForUserPanel(string userName);
    SideBarUserPanelViewModel GetUserInformationForSideBar(string userName);
    EditUserInformationViewModel GetUserInformationForEditProfile(string userName);
    bool EditProfile(string userName, EditUserInformationViewModel profile);
    bool ChangePassword(string userName, ChangePasswordViewModel model);

    #endregion

    #region Wallet Actions

    int UserBalance(string userName);
    List<WalletViewModel> GetUserWallets(string userName);
    int ChargeWallet(string userName, int amount, string description, bool isPay = false);
    int AddWallet(Wallet wallet);
    Wallet GetWalletByWalletId(int walletId);
    void UpdateWallet(Wallet wallet);

    #endregion

    #region Admin Panel Actions

    UsersForAdminPanelViewModel GetUsersForAdminPanel(int pageId = 1, string filterEmail = "", string filterUserName = "");

    #endregion

}