using MarketWebApp.Data;
using MarketWebApp.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace MarketWebApp.Controllers
{
    public class CheckoutController : BaseController
    {

        private readonly ApplicationDbContext _context;

        public CheckoutController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        //public IActionResult Index()
        //{
        //    return View(_context.ShoppingCart.ToList());
        //}

        //[HttpGet]
        //public IActionResult Checkout()
        //{
        //    ViewBag
        //    return View(new Order());
        //}
        //[HttpPost]
        //public IActionResult Checkout(Order order)
        //{
        //   order.Date = DateTime.Now;
        //    order.State = "Offline";

        //    return 
        //}
    }
}
