namespace OnlineShop.Core.DTOs.User;

public class UserInformationViewModel
{
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public DateTime RegisterDate { get; set; }
    public int Wallet { get; set; }
}

public class SideBarUserPanelViewModel
{
    public required string FullName { get; set; }
    public required DateTime RegisterDate { get; set; }
    public required string UserImage { get; set; }
}