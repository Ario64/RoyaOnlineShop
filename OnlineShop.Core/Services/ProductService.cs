using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Core.DTOs.Product;
using OnlineShop.Core.Generators;
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

    public int AddProduct(Product product, IFormFile imgProduct)
    {
        product.CreateDate = DateTime.Now;
        product.ProductImageName = "no-photo.jpg";

        if (imgProduct != null)
        {
            product.ProductImageName = NameGenerator.GenerateUniqueName() + Path.GetExtension(imgProduct.FileName);
            string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product/images/", product.ProductImageName);

            using (var stream = new FileStream(imgPath, FileMode.Create))
            {
                imgProduct.CopyTo(stream);
            }
        }

        _context.Products.Add(product);
        _context.SaveChanges();
        return product.ProductId;
    }

    public ShowProductsForAdminViewModel GetAllProduct(int pageId = 1, string filterProductName = "")
    {
        int take = 20;
        int skip = (pageId - 1) * take;

        IQueryable<Product> products = _context.Products;

        if (!string.IsNullOrEmpty(filterProductName))
        {
            products = products.Where(w => w.ProductName.Contains(filterProductName));
        }

        var list = new ShowProductsForAdminViewModel();
        list.CurrentPage = pageId;
        int Quantity = products.OrderByDescending(o => o.CreateDate).Count();

        if (Quantity % 2 == 0)
        {
            list.PageCount = Quantity / take;
        }
        else
        {
            list.PageCount = (Quantity / take) + 1;
        }

        list.Products = products.OrderByDescending(o => o.CreateDate)
            .Skip(skip)
            .Take(take)
            .ToList();

        return list;
    }

    public void AddColorToProductByAdmin(int productId, List<int>? SelectedColor, List<int>? ColorQuantities)
    {
        if (SelectedColor is null)
            return; // there are no colors

        for (int i = 0; i < SelectedColor.Count; i++)
        {
            _context.ProductColors.Add(new ProductColor()
            {
                ProductId = productId,
                ColorId = SelectedColor[i],
                Quantity = ColorQuantities?[i]
            });
        }
        _context.SaveChanges();
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
        size.IsDeleted = true;
        UpdateSize(size);
    }

    public void AddSizeToProductByAdmin(int productId, List<int>? SelectedSizes, List<int>? SizeQuantities)
    {
        if (SelectedSizes is null)
        {
            return;
        }

        for (int i = 0; i < SelectedSizes.Count; i++)
        {
            _context.ProductSizes.Add(new ProductSize()
            {
                ProductId = productId,
                SizeId = SelectedSizes[i],
                Quantity = SizeQuantities?[i]
            });
        }

        _context.SaveChanges();
    }
}