using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.DataLayer.Entities.Product;

namespace OnlineShop.Core.Services.Interfaces;

public interface IProductService
{

    #region Product Group

    List<ProductGroup> GetGroups();
    List<SelectListItem> GetMainGroup();
    List<SelectListItem> GetSubMainGroup(int groupId);

    #endregion

    #region Product Color

    List<Color> GetColors();
    int AddColor(Color color);
    Color GetColorByIdForAdmin(int colorId);
    void UpdateColor(Color color);
    void DeleteColor(Color color);

    #endregion

    #region Product Size

    List<Size> GetSizes();
    void AddSize(Size size);
    Size GetSizeByIdForAdmin(int sizeId);
    void UpdateSize(Size size);
    void DeleteSize(Size size);

    #endregion

}