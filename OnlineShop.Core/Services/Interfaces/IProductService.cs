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
    void UpdateProduct(Product product, IFormFile? file);
    Tuple<List<ShowProductListItemViewModel>, int > GetProducts(int pageId = 1, string filterName = "", string orderDate = "", string price = "", List<int>? selectedGroups = null, int take = 0);
    Product GetProductForShow(int productId);

    #endregion

    #region Product Size

    List<Size> GetSizes();
    List<SelectListItem> GetProductSizeList(int productId);
    void AddSize(Size size);
    Size GetSizeByIdForAdmin(int sizeId);
    void UpdateSize(Size size);
    void DeleteSize(Size size);
    void AddSizeToProductByAdmin(int productId, List<int>? SelectedSizes, List<int>? SizeQuantities);
    List<int?> GetProductSizes(int productId);
    Tuple<List<Size>, List<GetSizeAndQuantities>> GetProductSizesForShow(int productId);
    void UpdateSizes(int productId, List<int>? SelectedSizes, List<int>? SizeQuantities);

    #endregion

}