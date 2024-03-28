using MarketWebApp.Data;
using MarketWebApp.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MarketWebApp.Controllers
{
    public class HomeController : BaseController
    {
        ApplicationDbContext context;



        public HomeController(ApplicationDbContext _context) : base(_context) {
            context = _context;
        }
        

        public IActionResult Index()
        {
            var Products = context.Products.Where(Product => Product.Discount >= 0).ToList();
            return View(Products);
        }

        [HttpPost]
        public IActionResult Search(string searchString)
        {
            // Filter categories and products based on search query
            var productsQuery = context.Products.Where(p => p.Name.Contains(searchString));
            var Products = productsQuery.ToList();
            return View("Index", Products);
        }
        public ActionResult Contact()
        {
            return View();
        }


       
    }
}
