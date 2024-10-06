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

    #endregion

}