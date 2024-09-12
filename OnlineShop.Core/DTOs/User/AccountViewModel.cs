using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OnlineShop.Core.DTOs.User;

#region Register And Login

public class RegisterViewModel
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

    [DisplayName("کلمه عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(50, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public required string Password { get; set; }

    [DisplayName("تکرار کلمه عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [Compare("Password", ErrorMessage = "{0} وارد شده با کلمه عبور مطابقت ندارد")]
    public required string ConfirmedPassword { get; set; }
}

public class LoginViewModel
{
    [DisplayName("ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(100, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    [EmailAddress(ErrorMessage = "فرمت وارد شده صحیح نمی باشد")]
    public required string Email { get; set; }

    [DisplayName("کلمه عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(50, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public required string Password { get; set; }

    [DisplayName("مرا بخاطر بسپار")]
    public bool RememberMe { get; set; }
}

public class ResendActivationViewModel
{
    [DisplayName("ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(100, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    [EmailAddress(ErrorMessage = "فرمت وارد شده صحیح نمی باشد")]
    public required string Email { get; set; }
}

public class ForgotPasswordViewModel
{
    [DisplayName("ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(100, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    [EmailAddress(ErrorMessage = "فرمت وارد شده صحیح نمی باشد")]
    public required string Email { get; set; }
}

public class ResetPasswordViewModel
{
    public required string ActiveCode { get; set; }

    [DisplayName("کلمه عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(50, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public string? Password { get; set; }

    [DisplayName("تکرار کلمه عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [Compare("Password", ErrorMessage = "{0} وارد شده با کلمه عبور مطابقت ندارد")]
    public string? ConfirmedPassword { get; set; }
}

#endregion
