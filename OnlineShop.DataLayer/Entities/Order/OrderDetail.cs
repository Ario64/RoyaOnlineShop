namespace OnlineShop.DataLayer.Entities.Order;

public class OrderDetail
{
    public int OrderDetailId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }

    #region Relations

    public Order? Order { get; set; }
    public Product.Product? Product { get; set; }

    #endregion

}