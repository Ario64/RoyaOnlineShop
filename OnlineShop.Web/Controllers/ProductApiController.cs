using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DataLayer.Contexts;

namespace OnlineShop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private RoyaContext _context;

        public ProductApiController(RoyaContext context)
        {
            _context = context;
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();

                var productNames = _context.Products
                    .Where(w => w.ProductName.Contains(term))
                    .Select(s=>s.ProductName)
                    .ToList();
                  return Task.FromResult<IActionResult>(Ok(productNames));

            }
            catch (Exception e)
            {
                return Task.FromResult<IActionResult>(BadRequest());
            }
        } 
    }
}
