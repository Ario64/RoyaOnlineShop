using Microsoft.AspNetCore.Mvc.Rendering;
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

    public List<SelectListItem> GetMainGroup()
    {
        return _context.ProductGroups
            .Where(w => w.ParentId == null)
            .Select(s =>
                new SelectListItem()
                {
                    Text = s.GroupTitle,
                    Value = s.ProductGroupId.ToString()
                }).ToList();
    }

    public List<SelectListItem> GetSubMainGroup(int groupId)
    {
       return _context.ProductGroups
            .Where(w => w.ParentId == groupId)
            .Select(s =>
                new SelectListItem()
                {
                    Text = s.GroupTitle,
                    Value = s.ProductGroupId.ToString()
                }).ToList();
    }

    public List<Color> GetColors()
    {
        return _context.Colors.ToList();
    }

    public int AddColor(Color color)
    {
        _context.Colors.Add(color);
        _context.SaveChanges();
        return color.ColorId;
    }
}