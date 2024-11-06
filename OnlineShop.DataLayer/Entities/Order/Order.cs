namespace OnlineShop.DataLayer.Entities.Order;

public class Order
{
    public int OrderId  { get; set; }
    public int UserId { get; set; }
    public int OrderSum { get; set; }
    public bool IsFinally { get; set; }
    public DateTime CreateDate { get; set; }

    #region Relations

    public User.User? User { get; set; }
    public List<OrderDetail>? OrderDetails { get; set; }

    #endregion
}