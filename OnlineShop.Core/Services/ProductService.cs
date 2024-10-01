using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Contexts;
using OnlineShop.DataLayer.Entities.Product;

namespace OnlineShop.Core.Services;

public class ProductService : IProductService
{
    private RoyaContext _context;

    public ProductService(RoyaContext context)
    {
        _context = context;
    }

    public List<ProductGroup> GetGroups()
    {
        return _context.ProductGroups.ToList();
    }
}