using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DataLayer.Entities.Product;

public class Color
{
    public int ColorId { get; set; }

    [DisplayName("رنگ محصول")]
    [Required(ErrorMessage = "{0} را وارد کنید !")]
    [MaxLength(100,ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد !")]
    public string ColorName { get; set; }

    public bool IsDeleted { get; set; }

    #region Relations

    public List<ProductColor>? ProductColors { get; set; }
    public List<SizeColorQuantity>? SizeColorQuantities { get; set; }

    #endregion

}