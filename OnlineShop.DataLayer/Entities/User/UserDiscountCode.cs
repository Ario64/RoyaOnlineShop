using OnlineShop.DataLayer.Entities.Order;

namespace OnlineShop.DataLayer.Entities.User;

public class UserDiscountCode
{
    public int UserDiscountId { get; set; }
    public int UserId { get; set; }
    public int DiscountId { get; set; }

    #region Relations

    public User? User { get; set; }
    public Discount? Discount { get; set; }

    #endregion
}