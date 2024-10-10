namespace OnlineShop.DataLayer.Entities.Product;

public class SizeColorQuantity
{
    public int SizeColorQuantityId { get; set; }
    public int ColorId { get; set; }
    public int SizeId { get; set; }
    public int Quantity { get; set; }

    #region Relations

    public Color? Color { get; set; }
    public Size? Size { get; set; }

    #endregion
}