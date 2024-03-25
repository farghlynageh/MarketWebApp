using MarketWebApp.Data;
using MarketWebApp.Models.Entity;
using MarketWebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketWebApp.Controllers
{
    public class OrderController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PlaceOrder()
        {
            string userId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // Get the shopping cart for the current user
            var shoppingCart = _context.ShoppingCart
                                        .Include(sc => sc.ProductCarts)
                                            .ThenInclude(pc => pc.Product)
                                        .FirstOrDefault(sc => sc.ApplicationUserID == userId);

            if (shoppingCart == null || shoppingCart.ProductCarts.Count == 0)
            {
                // If the shopping cart is empty, handle accordingly (e.g., display a message)
                // You can redirect to a different action or return a view to inform the user.
                return RedirectToAction("EmptyCart");
            }

            // Retrieve available locations from the database
            var locations = _context.Locations.ToList();


           // Create a view model to pass data to the view
            var viewModel = new PlaceOrderViewModel
            {
                ShoppingCart = shoppingCart,
                Locations = locations
            };

            return View(viewModel);

            //return View();
        }



        //confirm

        [HttpPost]
        public IActionResult ConfirmOrder(PlaceOrderViewModel viewModel)
        {
            // Retrieve the selected location ID and other order details from the view model
            int selectedLocationId = viewModel.SelectedLocationId;

            // Create the order entity and save it to the database
            Order order = new Order
            {
                Date = DateTime.Now,
                State = "Pending", // or any other initial state
                LocationId = selectedLocationId,
                ApplicationUserID = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                // You may need to set other properties of the order based on your requirements
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            // Clear the shopping cart after placing the order
            _context.ShoppingCart.Remove(viewModel.ShoppingCart);
            _context.SaveChanges();

            // Redirect to a thank you page or order details page
            return RedirectToAction("OrderConfirmation", new { orderId = order.ID });
        }


        public IActionResult OrderHistory()
        {
            // Retrieve the user's ID
            string userId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // Find orders associated with the user
            var orders = _context.Orders
                                .Include(o => o.OrderProducts)
                                    .ThenInclude(oi => oi.Product)
                                .Where(o => o.ApplicationUserID == userId)
                                .ToList();

            // You can pass orders to a view model or directly to the view for displaying
            return View(orders);
        }

    }
}
