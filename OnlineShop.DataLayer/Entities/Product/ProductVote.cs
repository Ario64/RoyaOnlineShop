namespace OnlineShop.DataLayer.Entities.Product;

public class ProductVote
{
    public int VoteId { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public bool Vote { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;

    #region Relation

    public Product? Product { get; set; }
    public User.User? User { get; set; }

    #endregion
}