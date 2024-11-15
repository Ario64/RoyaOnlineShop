﻿using Microsoft.AspNetCore.Http;
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
    Tuple<List<ShowProductListItemViewModel>, int> GetProducts(int pageId = 1, string filterName = "", string orderDate = "", string price = "", List<int>? selectedGroups = null, int take = 0);
    Product GetProductForShow(int productId);
    List<ShowProductListItemViewModel> GetPopularProducts();
    bool IsUserInProduct(string userName, int productId);

    #endregion

    #region Comments

    void AddComment(ProductComment comment);
    Tuple<List<ProductComment>, int> GetProductComments(int productId, int pageId = 1);

    #endregion

    #region Groups

    void AddGroup(ProductGroup group);
    void UpdateGroup(ProductGroup group);
    ProductGroup GetProductGroupByProductId(int groupId);
    void DeleteGroup(ProductGroup group);

    #endregion

    #region Vote

    void AddVote(int userId, int productId, bool vote);
    int GetProductVote(int productId);

    #endregion

}