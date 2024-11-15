﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using OnlineShop.DataLayer.Entities.Product;

namespace OnlineShop.DataLayer.Entities.User;


public class User
{
    public int UserId { get; set; }

    [DisplayName("نام و نام خانوادگی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(150, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public string FullName { get; set; }

    [DisplayName("نام کاربری")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(30, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public string UserName { get; set; }

    [DisplayName("ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(100, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public string Email { get; set; }

    [DisplayName("تلفن")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(11, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public string PhoneNumber { get; set; }

    [DisplayName("آدرس")]
    [MaxLength(300, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public string Address { get; set; }

    [DisplayName("کد فعال سازی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(50, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public string ActiveCode { get; set; }

    [DisplayName("کلمه عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(50, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public string Password { get; set; }

    [DisplayName("آواتار")]
    [MaxLength(50, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public string? UserAvatar { get; set; }

    [DisplayName("وضعیت")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public bool IsActive { get; set; }

    [DisplayName("تاریخ ثبت نام")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public DateTime RegisterDate { get; set; }

    public bool IsDeleted { get; set; }

    #region Relations

    public List<UserRole>? UserRoles { get; set; }
    public List<Wallet.Wallet>? Wallets{ get; set; }
    public List<UserProduct>? UserProducts { get; set; }
    public List<Order.Order>? Orders { get; set; }
    public List<UserDiscountCode>? UserDiscountCodes { get; set; }
    public List<ProductComment>? ProductComments { get; set; }
    public List<ProductVote>? ProductVotes { get; set; }

    #endregion

}