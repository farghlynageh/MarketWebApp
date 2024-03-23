using MarketWebApp.Data;
using MarketWebApp.Models;
using MarketWebApp.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketWebApp.Controllers
{
    public class ShoppingCartController : BaseController
    {

        private readonly ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        //public IActionResult Index()
        //{
        //    return View(_context.ShoppingCart.ToList());
        //}


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
        // Assuming you have access to your DbContext instance
        public IActionResult AddToCart(int id)
        {

            string userId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            // Find or create a shopping cart associated with the user
            var shoppingCart = _context.ShoppingCart
                                        .FirstOrDefault(cart => cart.ApplicationUserID == userId);

            if (shoppingCart == null)
            {
                // If the shopping cart doesn't exist, create a new one
                shoppingCart = new ShoppingCart
                {
                    ApplicationUserID = userId,
                    ProductCarts = new List<ProductCart>()
                };
                _context.ShoppingCart.Add(shoppingCart);
            }

            // Check if the product is already in the cart
            var existingProductCart = shoppingCart.ProductCarts
                                                  .FirstOrDefault(pc => pc.ProductId == id);

            if (existingProductCart != null)
            {
                // If the product is already in the cart, update the quantity
                existingProductCart.Quantity += 1;
            }
            else
            {
                // If the product is not in the cart, add it
                var newProductCart = new ProductCart
                {
                    ProductId = id,
                    Quantity = 1
                };
                shoppingCart.ProductCarts.Add(newProductCart);
            }

            // Save changes to the database
            _context.SaveChanges();
            return RedirectToAction("Index","Home");
        }


        public IActionResult Index()
        {
            string userId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            // Retrieve the shopping cart associated with the user
            var shoppingCart = _context.ShoppingCart
                                        .Include(cart => cart.ProductCarts)
                                            .ThenInclude(pc => pc.Product)
                                        .FirstOrDefault(cart => cart.ApplicationUserID == userId);

            if (shoppingCart == null)
            {
                // Handle the case where the user's shopping cart is empty
                ViewBag.Message = "Your shopping cart is empty.";
                return View(new List<ProductCart>());
            }

            // Pass the products in the shopping cart to the view
            return View(shoppingCart.ProductCarts);
        }


    }
}
