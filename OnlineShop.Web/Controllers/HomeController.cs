using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Index

        public IActionResult Index()
        {
            return View();
        }

        #endregion

    }
}
