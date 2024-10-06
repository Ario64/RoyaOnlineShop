using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OnlineShop.DataLayer.Entities.Product;

public class Size
{
    public int SizeId { get; set; }

    [DisplayName("سایز محصول")]
    [Required(ErrorMessage = "{} را وارد کنید !")]
    [MaxLength(50, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد !")]
    public string SizeName { get; set; }

    #region Relations

    public List<ProductSize>? ProductSizes { get; set; }

    #endregion

}