using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Core.Services.Interfaces;
using OnlineShop.DataLayer.Entities.Product;

namespace OnlineShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IOrderService _orderService;
        private IUserService _userService;

        public ProductController(IProductService productService, IOrderService orderService, IUserService userService)
        {
            _productService = productService;
            _orderService = orderService;
            _userService = userService;
        }

        public IActionResult Index(int pageId = 1, string filterName = "", string orderDate = "", string price = "", List<int>? selectedGroups = null, int take = 0)
        {
            ViewBag.SelectedGroups = selectedGroups;
            ViewBag.Groups = _productService.GetGroups();
            ViewBag.PageId = pageId;
            return View(_productService.GetProducts(pageId, filterName, orderDate, price, selectedGroups, take));
        }

        [Route("showproduct/{id}")]
        public IActionResult ShowProduct(int id)
        {
            var product = _productService.GetProductForShow(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [Authorize]
        public IActionResult AddToCart(int id)
        {
            string userName = User.Identity.Name;

            int orderId = _orderService.AddOrder(userName, id);

            return Redirect($"/userpanel/order/showcart/" + orderId);
        }

        [Authorize]
        public IActionResult CreateComment(ProductComment comment)
        {
            comment.CreateDate = DateTime.Now;
            comment.IsDelete = false;
            comment.UserId = _userService.GetUserIdByUserName(User.Identity.Name);
            _productService.AddComment(comment);

            return View("ShowComments", _productService.GetProductComments(comment.ProductId));
        }

        public IActionResult ShowComments(int id, int pageId = 1)
        {
            return View(_productService.GetProductComments(id, pageId));
        }

        public IActionResult Vote(int id)
        {
            return PartialView(_productService.GetProductVote(id));
        }

        [Authorize]
        public IActionResult CreateVote(int id, bool vote)
        {
            int userId = _userService.GetUserIdByUserName(User.Identity.Name);
            _productService.AddVote(userId, id, vote);
            return PartialView("Vote", _productService.GetProductVote(id));
        }
    }
}
