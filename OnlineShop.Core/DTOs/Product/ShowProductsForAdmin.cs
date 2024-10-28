namespace OnlineShop.Core.DTOs.Product;

public class ShowProductsForAdminViewModel
{
    public List<DataLayer.Entities.Product.Product> Products { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }

}

public class GetColorQuantitiesForShow
{
    public int? ColorId { get; set; }
    public string? Quantity { get; set; }
}

public class GetSizeAndQuantities
{
    public int? SizeId { get; set; }
    public string? Quantity { get; set; }
}

public class ShowProductListItemViewModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string? ImageName { get; set; }
    public int Price { get; set; }

}
