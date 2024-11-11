using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using OnlineShop.DataLayer.Entities.User;

namespace OnlineShop.DataLayer.Entities.Order;

public class Discount
{
    public int DiscountId { get; set; }

    [DisplayName("کد تخفیف")]
    [Required(ErrorMessage = "{0} را وارد کنید !")]
    [MaxLength(50,ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد !")]
    public string DiscountCode { get; set; }

    [DisplayName("درصد تخفیف")]
    [Required(ErrorMessage = "{0} را وارد کنید !")]
    public int DiscountPercent { get; set; }

    [DisplayName("تعداد")]
    public int? UsableCount { get; set; }

    [DisplayName("تاریخ شروع")]
    public DateTime? StartDate { get; set; }

    [DisplayName("تاریخ پایان")]
    public DateTime? EndDate { get; set; }

    #region Relations

    public List<UserDiscountCode>? UserDiscountCodes { get; set; }

    #endregion

}