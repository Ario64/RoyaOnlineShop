using OnlineShop.DataLayer.Entities.Product;

namespace OnlineShop.Core.Services.Interfaces;

public interface IProductService
{

    #region Product Group

    List<ProductGroup> GetGroups();

    #endregion
}