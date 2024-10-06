using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OnlineShop.DataLayer.Entities.Product;

public class ProductPicture
{
    public int ProductPictureId { get; set; }
    public int ProductId { get; set; }

    [DisplayName("تصویر محصول")]
    [Required(ErrorMessage = "{} را وارد کنید !")]
    [MaxLength(50, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد !")]
    public string PictureName { get; set; }

    #region Relations

    public Product? Product { get; set; }

    #endregion 
}