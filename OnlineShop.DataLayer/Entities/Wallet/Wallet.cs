using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DataLayer.Entities.Wallet;

public class Wallet
{
    public int WalletId { get; set; }

    [DisplayName("کاربر")]
    public int? UserId { get; set; }

    [DisplayName("کاربر")]
    public int WalletTypeId { get; set; }

    [DisplayName("مبلغ")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public int Amount { get; set; }

    [DisplayName("وضعیت")]
    public bool IsPayed { get; set; }

    [DisplayName("شرح تراکنش")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(500, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد")]
    public string? Description { get; set; }

    [DisplayName("تاریخ تراکنش")]
    public DateTime CreateDate { get; set; }

    #region Relations

    public WalletType? WalletType { get; set; }
    public User.User? User { get; set; }

    #endregion

}