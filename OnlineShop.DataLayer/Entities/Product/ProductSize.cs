namespace OnlineShop.DataLayer.Entities.Product;

public class ProductSize
{
    public int PsId { get; set; }
    public int? SizeId { get; set; }
    public int? ProductId { get; set; }
    public int? Quantity { get; set; }


    #region Relations

    public Product? Product { get; set; }
    public Size? Size { get; set; }

    #endregion
}