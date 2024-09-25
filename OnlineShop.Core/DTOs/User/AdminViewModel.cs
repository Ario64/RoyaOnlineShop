using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Core.DTOs.User;

public class UsersForAdminPanelViewModel
{
    public List<DataLayer.Entities.User.User>? Users { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
}

public class CreateUserViewModel
{
    [DisplayName("نام و نام خانوادگی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(150, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public required string FullName { get; set; }

    [DisplayName("نام کاربری")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(30, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public required string UserName { get; set; }

    [DisplayName("ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(100, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    [EmailAddress(ErrorMessage = "فرمت وارد شده صحیح نمی باشد")]
    public required string Email { get; set; }

    [DisplayName("موبایل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(11, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    [MinLength(11, ErrorMessage = "{0} نباید کمتر از {1} کاراکتر باشد")]
    public required string PhoneNumber { get; set; }

    [DisplayName("آدرس")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(500, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public required string Address { get; set; }

    [DisplayName("کلمه عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(50, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public required string Password { get; set; }

    public IFormFile? UserAvatar { get; set; }

}

public class EditUserViewModel
{
    public int UserId { get; set; }

    public string FullName { get; set; }

    public string UserName { get; set; }

    [Remote("IsEmailExist", "Home", ErrorMessage = "ایمیل وارد شده معتبر نیست !")]
    [DisplayName("ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(100, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    [EmailAddress(ErrorMessage = "فرمت وارد شده صحیح نمی باشد")]
    public string Email { get; set; }

    [DisplayName("موبایل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(11, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    [MinLength(11, ErrorMessage = "{0} نباید کمتر از {1} کاراکتر باشد")]
    public string PhoneNumber { get; set; }

    [DisplayName("آدرس")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(500, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public string Address { get; set; }

    [DisplayName("کلمه عبور")]
    [MaxLength(50, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public string? Password { get; set; }

    public IFormFile? UserAvatar { get; set; }

    public List<int>? UserRoles { get; set; }

    public string? AvatarName { get; set; }
}