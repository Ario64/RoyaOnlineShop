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
        return _context.ProductGroups.Include(i=>i.ProductGroups).ToList();
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
            take = 8;

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
            ImageName = s.ProductImageName,
            CreateDate = s.CreateDate
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
            ImageName = s.ProductImageName,
            CreateDate = s.CreateDate
        }).OrderByDescending(o => o.CreateDate).Skip(skip).Take(take).ToList();

        return Tuple.Create(list, pageCount);
    }

    public Product GetProductForShow(int productId)
    {
        return _context.Products.Find(productId);
    }

    public List<ShowProductListItemViewModel> GetPopularProducts()
    {
        return _context.Products
            .Include(i => i.OrderDetails)
            .Where(w => w.OrderDetails.Any())
            .OrderByDescending(o => o.OrderDetails.Count)
            .Select(s =>
                new ShowProductListItemViewModel()
                {
                    ProductId = s.ProductId,
                    ImageName = s.ProductImageName,
                    CreateDate = s.CreateDate,
                    Price = s.ProductPrice,
                    ProductName = s.ProductName
                })
            .Take(6)
            .ToList();
    }

    public void AddComment(ProductComment comment)
    {

        _context.ProductComments.Add(comment);
        _context.SaveChanges();
    }

    public Tuple<List<ProductComment>, int> GetProductComments(int productId, int pageId = 1)
    {
        int take = 4;
        int skip = (pageId - 1) * take;
        int pageCount = 0;
        int quantity;

        quantity = _context.ProductComments
            .Where(w => w.ProductId == productId && !w.IsDelete)
            .ToList().Count;

        if (quantity % 2 == 0)
        {
            pageCount = quantity / take;
        }
        else
        {
            pageId = (quantity / take + 1);
        }

        var list = _context.ProductComments
            .Where(w => w.ProductId == productId && !w.IsDelete)
            .Skip(skip).Take(take).OrderByDescending(o => o.CreateDate).ToList();

        return Tuple.Create(list, pageCount);
    }

    public void AddGroup(ProductGroup group)
    {
        _context.ProductGroups.Add(group);
        _context.SaveChanges();
    }

    public void UpdateGroup(ProductGroup group)
    {
        _context.ProductGroups.Update(group);
        _context.SaveChanges();
    }

    public ProductGroup GetProductGroupByProductId(int groupId)
    {
        return _context.ProductGroups.Find(groupId);
    }
}