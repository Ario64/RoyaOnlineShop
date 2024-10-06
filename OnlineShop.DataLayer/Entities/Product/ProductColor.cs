namespace OnlineShop.DataLayer.Entities.Product;

public class ProductColor
{
    public int ProductColorId { get; set; }
    public int? ProductId { get; set; }
    public int? ColorId { get; set; }

    #region Relations

    public Product? Product { get; set; }
    public Color? Color { get; set; }

    #endregion

}