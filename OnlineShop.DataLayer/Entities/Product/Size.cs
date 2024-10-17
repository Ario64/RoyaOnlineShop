using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OnlineShop.DataLayer.Entities.Product;

public class Size
{
    public int SizeId { get; set; }

    public int ProductId { get; set; }

    [DisplayName("سایز محصول")]
    [Required(ErrorMessage = "{0} را وارد کنید !")]
    [MaxLength(50, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد !")]
    public string SizeName { get; set; }

    [DisplayName("شرح سایز")]
    [Required(ErrorMessage = "{0} را وارد کنید !")]
    [MaxLength(500, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد !")]
    public string Description { get; set; }

    [DisplayName("تعداد")]
    [Required(ErrorMessage = "{0} را وارد کنید !")]
    public int Quantity { get; set; }

    public bool IsDeleted { get; set; }

    #region Relations

    public Product? Product { get; set; }

    #endregion

}