namespace OnlineShop.Core.DTOs.Product;

public class ShowProductsForAdminViewModel
{
    public List<DataLayer.Entities.Product.Product> Products { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }

}
