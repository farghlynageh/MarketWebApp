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


        public IActionResult Index(int page = 1)
        {
            int pageSize = 2; // Number of items per page
            var totalItems = context.Products.Where(Product => Product.Discount >= 0).Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var products = context.Products
                                    .Where(Product => Product.Discount >= 0)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            var viewModel = new 
            {
                Products = products,
                CurrentPage = page,
                TotalPages = totalPages
            };
           
            return View(viewModel);
        }


        [HttpPost]
        [HttpGet]
        public IActionResult Search(string searchString, int page = 1)
        {
            int pageSize = 8; // Number of items per page
            var totalItems = context.Products.Where(p => p.Name.Contains(searchString)).Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var products = context.Products
                                    .Where(p => p.Name.Contains(searchString))
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            var viewModel = new 
            {
                Products = products,
                CurrentPage = page,
                searchString=searchString,
                TotalPages = totalPages
            };

            return View(viewModel);
        }

        public ActionResult Contact()
        {
            return View();
        }


       
    }
}
