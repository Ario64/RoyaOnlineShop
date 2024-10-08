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

    public Color GetColorByIdForAdmin(int colorId)
    {
        return _context.Colors.Find(colorId);
    }

    public void UpdateColor(Color color)
    {
        _context.Colors.Update(color);
        _context.SaveChanges();
    }

    public void DeleteColor(Color color)
    {
        color.IsDeleted = true;
        UpdateColor(color);
    }

    public List<Size> GetSizes()
    {
        return _context.Sizes.ToList();
    }

    public void AddSize(Size size)
    {
        _context.Sizes.Add(size);
        _context.SaveChanges();
    }

    public Size GetSizeByIdForAdmin(int sizeId)
    {
        return _context.Sizes.Find(sizeId);
    }

    public void UpdateSize(Size size)
    {
        _context.Sizes.Update(size);
        _context.SaveChanges();
    }

    public void DeleteSize(Size size)
    {
        _context.Sizes.Remove(size);
        _context.SaveChanges();
    }
}