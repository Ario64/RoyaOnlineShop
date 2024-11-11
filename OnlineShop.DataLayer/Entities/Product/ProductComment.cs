using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DataLayer.Entities.Product;

public class ProductComment
{
    public int ProductCommentId { get; set; }
    public int? UserId { get; set; }
    public int ProductId { get; set; }

    [DisplayName("متن")]
    [MaxLength(700,ErrorMessage = "{0} نباید بیشتر از {1} کاراکتر باشد !")]
    public string Comment { get; set; }
    public DateTime CreateDate { get; set; }
    public bool IsDelete { get; set; }
    public bool IsAdminRead { get; set; }

    #region Relations

    public User.User? User { get; set; }
    public Product? Product { get; set; }

    #endregion

}