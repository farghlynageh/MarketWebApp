using MarketWebApp.Data;
using MarketWebApp.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace MarketWebApp.Controllers
{
    public class ShopController : BaseController
    {
        ApplicationDbContext context;
        public ShopController(ApplicationDbContext _context) : base(_context)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            var products = context.Products.Where(Product => Product.Discount == 0).ToList();
            var Model = new
            {
                Products = products,
                Count = products.Count(),
                Discount = context.Products.Where(Product => Product.Discount > 0).ToList(),
            };
            return View(Model);
        }

        [HttpGet]
        public IActionResult Product(int id)
        {
            var products = context.Products.Where(P => P.CategoryId == id && P.Discount == 0);
            var Model = new
            {
                Products = products,
                Count = products.Count(),
                Discount = context.Products.Where(Product => Product.Discount > 0).ToList(),
            };
            return View(Model);

        }
    }
}
