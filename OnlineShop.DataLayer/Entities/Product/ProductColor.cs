using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OnlineShop.DataLayer.Entities.Product;

public class ProductColor
{
    public int PcId { get; set; }
    public int? ColorId { get; set; }
    public int? ProductId { get; set; }

    [DisplayName("تعداد")]
    public int? Quantity { get; set; } 

    #region Relations

    public Product? Product { get; set; }
    public Color? Color { get; set; }

    #endregion
}