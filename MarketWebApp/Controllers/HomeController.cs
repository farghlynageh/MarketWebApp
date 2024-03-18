using MarketWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketWebApp.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context;
        public HomeController(ApplicationDbContext _context)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            var Model = new
            {
                Catagory = context.Categories.ToList(),
                Products = context.Products.ToList(),
            };
            return View(Model);
        }


        //[HttpPost]
        //public IActionResult Search(string searchString)
        //{
        //    // Perform search based on the provided searchString
        //    var searchResults = context.Products
        //        .Where(m => m.Name.Contains(searchString))
        //        .ToList();

        //    var Model = new
        //    {
        //        Catagory = context.Categories.ToList(),
        //        SearchResults = searchResults,
        //    };
        //    return View(Model);
        //}


    }
}
