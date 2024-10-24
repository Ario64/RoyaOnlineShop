using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Core.DTOs.Product;
using OnlineShop.DataLayer.Entities.Product;

namespace OnlineShop.Core.Services.Interfaces;

public interface IProductService
{

    #region Product

    List<ProductGroup> GetGroups();
    List<SelectListItem> GetMainGroup();
    List<SelectListItem> GetSubMainGroup(int groupId);
    int AddProduct(Product product, IFormFile imgProduct);
    ShowProductsForAdminViewModel GetAllProduct(int pageId = 1, string filterProductName = "");
    Product GetProductByProductId(int productId);
    Tuple<List<Color>, List<GetColorQuantitiesForShow>> GetProductColorsForShow(int productId);
    void UpdateProduct(Product product, IFormFile? file);
    List<ShowProductListItemViewModel> GetProducts(int pageId = 1, string filterName = "", string orderDate = "date", int startPrice = 0, int endPrice = 0, List<int>? selectedGroups = null, int take =0);

    #endregion

    #region Product Color

    List<Color> GetColors();
    int AddColor(Color color);
    Color GetColorByIdForAdmin(int colorId);
    void UpdateColor(Color color);
    void DeleteColor(Color color);
    void AddColorToProductByAdmin(int productId, List<int>? SelectedColor, List<string>? ColorQuantities);
    void UpdateColors(int productId, List<int>? SelectedColors, List<string>? ColorQuantities);

    #endregion

    #region Product Size

    List<Size> GetSizes();
    void AddSize(Size size);
    Size GetSizeByIdForAdmin(int sizeId);
    void UpdateSize(Size size);
    void DeleteSize(Size size);
    void AddSizeToProductByAdmin(int productId, List<int>? SelectedSizes, List<string>? SizeQuantities);
    List<int?> GetProductSizes(int productId);
    Tuple<List<Size>, List<GetSizeAndQuantities>> GetProductSizesForShow(int productId);
    void UpdateSizes(int productId, List<int>? SelectedSizes, List<string>? SizeQuantities);

    #endregion

}