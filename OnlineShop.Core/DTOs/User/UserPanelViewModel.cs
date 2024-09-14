using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

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

public class EditUserInformationViewModel
{
    
    [DisplayName("موبایل")]
    [MaxLength(11, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    [MinLength(11, ErrorMessage = "{0} نباید کمتر از {1} کاراکتر باشد")]
    public  string? PhoneNumber { get; set; }

    [DisplayName("آدرس")]
    [MaxLength(500, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public  string? Address { get; set; }

    [DisplayName("آواتار")]
    public IFormFile? UserAvatar { get; set; }

    public string? AvatarName { get; set; }
}