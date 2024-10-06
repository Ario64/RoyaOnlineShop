using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DataLayer.Entities.Product;

public class ProductSize
{
    public int ProductSizeId { get; set; }
    public int ProductId { get; set; }
    public int SizeId { get; set; }

    #region Relations

    public Product? Product { get; set; }
    public Size? Size { get; set; }

    #endregion
}