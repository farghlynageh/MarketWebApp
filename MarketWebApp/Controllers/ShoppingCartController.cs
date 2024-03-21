using MarketWebApp.Data;
using MarketWebApp.Models;
using MarketWebApp.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace MarketWebApp.Controllers
{
    public class ShoppingCartController : BaseController
    {

        private readonly ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context):base(context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View(_context.ShoppingCart.ToList());
        }


        public IActionResult Delete(int? id)
        {
            var sh = _context.ShoppingCart.Find(id);
            if (sh == null)
            {
                return NotFound();
            }

            _context.ShoppingCart.Remove(sh);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }




        [HttpPost]
        public IActionResult AddToCart(int id)
        {

            var product = _context.Products.Find(id);

            if (product != null)
            {
                // Create a new ShoppingCart item and populate its properties
                ShoppingCart shoppingCart = new ShoppingCart
                {
                    Name = product.Name,
                    Price = (int)product.Price,
                    Img = product.Img
                };

                // Add the ShoppingCart item to the database
                _context.ShoppingCart.Add(shoppingCart);
                _context.SaveChanges();

                // Redirect to the shopping cart view
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Product not found, return a not found response or handle the error as appropriate
                return NotFound();
            }
        }
    }
}
