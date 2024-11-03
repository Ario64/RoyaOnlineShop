using System.Drawing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Convertors;
using OnlineShop.Core.DTOs.Product;
using OnlineShop.Core.Generators;
using OnlineShop.Core.Security;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Contexts;
using OnlineShop.DataLayer.Entities.Product;
using Size = OnlineShop.DataLayer.Entities.Product.Size;

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

        if (imgProduct != null && imgProduct.IsImage())
        {
            product.ProductImageName = NameGenerator.GenerateUniqueName() + Path.GetExtension(imgProduct.FileName);
            string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product/images/", product.ProductImageName);

            using (var stream = new FileStream(imgPath, FileMode.Create))
            {
                imgProduct.CopyTo(stream);
            }

            ImageConvertor imgResizer = new ImageConvertor();
            string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product/thumb/", product.ProductImageName);
            imgResizer.Image_resize(imgPath, thumbPath, 150);
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

    public Product GetProductByProductId(int productId)
    {
        return _context.Products.Find(productId);
    }

    public void UpdateProduct(Product product, IFormFile? file)
    {
        if (file != null && file.IsImage())
        {
            if (product.ProductImageName != "no-photo.jpg")
            {
                string deleteImgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product/images/", product.ProductImageName);
                if (File.Exists(deleteImgPath))
                {
                    File.Delete(deleteImgPath);
                }

                string deleteThumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product/thumb/", product.ProductImageName);
                if (File.Exists(deleteThumbPath))
                {
                    File.Delete(deleteThumbPath);
                }

                product.ProductImageName = NameGenerator.GenerateUniqueName() + Path.GetExtension(file.FileName);
                string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product/images/", product.ProductImageName);

                using (var stream = new FileStream(imgPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/product/thumb/", product.ProductImageName);
                imgResizer.Image_resize(imgPath, thumbPath, 150);

            }
        }

        _context.Products.Update(product);
        _context.SaveChanges();
    }

    public Tuple<List<ShowProductListItemViewModel>, int> GetProducts(int pageId = 1, string filterName = "", string orderDate = "", string price = "", List<int>? selectedGroups = null, int take = 0)
    {
        if (take == 0)
            take = 6;

        int skip = (pageId - 1) * take;

        IQueryable<Product> result = _context.Products;

        if (!string.IsNullOrEmpty(filterName))
        {
            result = result.Where(w => w.ProductName.Contains(filterName) || w.Tags.Contains(filterName));
        }

        switch (orderDate)
        {
            case "lastDate":
                {
                    result = result.OrderBy(o => o.CreateDate);
                    break;
                }
            case "newDate":
                {
                    result = result.OrderByDescending(o => o.CreateDate);
                    break;
                }
        }

        switch (price)
        {
            case "minPrice":
                {
                    result = result.OrderByDescending(o => o.ProductPrice);
                    break;
                }
            case "maxPrice":
                {
                    result = result.OrderBy(o => o.ProductPrice);
                    break;
                }
        }

        if (selectedGroups != null && selectedGroups.Any())
        {
            foreach (int groupId in selectedGroups)
            {
                result = result.Where(w => w.ProductGroupId == groupId || w.SubGroup == groupId);
            }
        }

        int pageCount = result.Select(s => new ShowProductListItemViewModel
        {
            ProductId = s.ProductId,
            ProductName = s.ProductName,
            Price = s.ProductPrice,
            ImageName = s.ProductImageName
        }).Count() / take;

        if (pageCount % 2 != 0)
        {
            pageCount += 1;
        }

        var list = result.Select(s => new ShowProductListItemViewModel
        {
            ProductId = s.ProductId,
            ProductName = s.ProductName,
            Price = s.ProductPrice,
            ImageName = s.ProductImageName
        }).Skip(skip).Take(take).ToList();

        return Tuple.Create(list, pageCount);
    }

    public Product GetProductForShow(int productId)
    {
        return _context.Products.FirstOrDefault(w => w.ProductId == productId);
    }

    public List<Size> GetSizes()
    {
        return _context.Sizes.ToList();
    }

    public List<SelectListItem> GetProductSizeList(int productId)
    {
        return _context.ProductSizes
            .Where(w => w.ProductId == productId)
            .Select(s =>
                new SelectListItem()
                {
                    Text = s.Size!.SizeName,
                    Value = s.SizeId.ToString(),
                }).ToList();
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

    public List<int?> GetProductSizes(int productId)
    {
        return _context.ProductSizes
            .Where(w => w.ProductId == productId)
            .Select(s => s.SizeId)
            .ToList();
    }

    public Tuple<List<Size>, List<GetSizeAndQuantities>> GetProductSizesForShow(int productId)
    {
        List<Size> sizes = _context.Sizes.ToList();

        List<GetSizeAndQuantities> quantities = _context.ProductSizes
            .Where(w => w.ProductId == productId)
            .Select(s =>
                new GetSizeAndQuantities()
                {
                    SizeId = s.SizeId,
                    Quantity = s.Quantity
                }).ToList();

        return Tuple.Create(sizes, quantities);
    }

    public void UpdateSizes(int productId, List<int>? SelectedSizes, List<int>? SizeQuantities)
    {
        if (SelectedSizes is null)
        {
            return;
        }

        _context.ProductSizes
            .Where(w => w.ProductId == productId)
            .ToList()
            .ForEach(f => _context.ProductSizes.Remove(f));

        AddSizeToProductByAdmin(productId, SelectedSizes, SizeQuantities);
    }
}