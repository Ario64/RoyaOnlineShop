using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DataLayer.Entities.Product;

public class ProductGroup
{
    public int ProductGroupId { get; set; }

    [DisplayName("عنوان گروه")]
    [MaxLength(150,ErrorMessage ="{0} نباید بیشتر از {1} کاراکتر باشد !")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید !")]
    public string GroupTitle { get; set; }

    [DisplayName("گروه اصلی")]
    public int? ParentId { get; set; }

    [DisplayName("حذف شده؟")]
    public bool IsDeleted { get; set; }

    #region Relations

    public List<ProductGroup>? ProductGroups { get; set; }
    public ProductGroup? Groups { get; set; }
    public List<Product>? Products { get; set; }
    public List<Product>? ProductList { get; set; }

    #endregion

}