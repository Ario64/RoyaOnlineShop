using OnlineShop.DataLayer.Entities.User;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using OnlineShop.DataLayer.Entities.Order;

namespace OnlineShop.DataLayer.Entities.Product;

public class Product
{
    public int ProductId { get; set; }
    public int ProductGroupId { get; set; }
    public int? SubGroup { get; set; }

    [DisplayName("نام محصول")]
    [Required(ErrorMessage = "{0} را وارد کنید !")]
    [MaxLength(400, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد !")]
    public string ProductName { get; set; }

    [DisplayName("شرح محصول")]
    [Required(ErrorMessage = "{0} را وارد کنید !")]
    public string Description { get; set; }

    [DisplayName("شرح محصول برای سئو")]
    [MaxLength(300, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد !")]
    //[MinLength(200,ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد !")]
    public string? SeoDescription { get; set; }

    [DisplayName("سایز محصول")]
    [Required(ErrorMessage = "{0} را وارد کنید !")]
    [MaxLength(150, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد !")]
    public string ProductSize { get; set; }

    [DisplayName("رنگ محصول")]
    [Required(ErrorMessage = "{0} را وارد کنید !")]
    [MaxLength(150, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد !")]
    public string ProductColor { get; set; }

    [DisplayName("قیمت محصول")]
    [Required(ErrorMessage = "{0} را وارد کنید !")]
    public int ProductPrice { get; set; }

    [DisplayName("تعداد محصول")]
    [Required(ErrorMessage = "{0} را وارد کنید !")]
    public int Quantity { get; set; }

    [DisplayName("برچسب ها")]
    [MaxLength(600, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد !")]
    public string Tags { get; set; }

    [DisplayName("تصویر محصول")]
    [MaxLength(60, ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد !")]
    public string? ProductImageName { get; set; }

    [DisplayName("تاریخ عرضه محصول")]
    [Required(ErrorMessage = "{0} را وارد کنید !")]
    public DateTime CreateDate { get; set; }

    public bool IsDeleted { get; set; }


    #region Relations

    public ProductGroup? ProductGroup { get; set; }
    public ProductGroup? Group { get; set; }
    public List<UserProduct>? UserProducts { get; set; }
    public List<OrderDetail>? OrderDetails { get; set; }

    #endregion

}