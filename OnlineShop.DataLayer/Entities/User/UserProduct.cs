namespace OnlineShop.DataLayer.Entities.User;

public class UserProduct
{
    public int UserProductId { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }

    #region Relations

    public User? User { get; set; }
    public Product.Product? Product { get; set; }

    #endregion

}