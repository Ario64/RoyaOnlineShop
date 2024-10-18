using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Core.DTOs.Product;
using OnlineShop.DataLayer.Entities.Product;

namespace OnlineShop.Core.Services.Interfaces;

public interface IProductService
{

    #region Product Group

    List<ProductGroup> GetGroups();
    List<SelectListItem> GetMainGroup();
    List<SelectListItem> GetSubMainGroup(int groupId);
    int AddProduct(Product product, IFormFile imgProduct);
    ShowProductsForAdminViewModel GetAllProduct(int pageId = 1, string filterProductName = "");
    

    #endregion

    #region Product Color

    List<Color> GetColors();
    int AddColor(Color color);
    Color GetColorByIdForAdmin(int colorId);
    void UpdateColor(Color color);
    void DeleteColor(Color color);
    void AddColorToProductByAdmin(int productId, List<int>? SelectedColor, List<int>? ColorQuantities);

    #endregion

    #region Product Size

    List<Size> GetSizes();
    void AddSize(Size size);
    Size GetSizeByIdForAdmin(int sizeId);
    void UpdateSize(Size size);
    void DeleteSize(Size size);
    void AddSizeToProductByAdmin(int productId, List<int>? SelectedSizes, List<int>? SizeQuantities);

    #endregion

}