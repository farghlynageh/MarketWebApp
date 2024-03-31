using MarketWebApp.Data;
using MarketWebApp.Models.Entity;
using MarketWebApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
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
                ViewBag.Message = "Your shopping cart is empty.";
                return View(new List<ProductCart>());
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

        }


        //confirm
        [HttpPost]
        public IActionResult ConfirmOrder(PlaceOrderViewModel viewModel)
        {
            // Retrieve the selected location ID and other order details from the view model
            int selectedLocationId = viewModel.SelectedLocationId;
            string userId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // Get the shopping cart for the current user
            var shoppingCart = _context.ShoppingCart
                                        .Include(sc => sc.ProductCarts)
                                            .ThenInclude(pc => pc.Product)
                                        .FirstOrDefault(sc => sc.ApplicationUserID == userId);

            // Create the order entity
            Order order = new Order
            {
                Date = DateTime.Now,
                State = "Pending", // or any other initial state
                LocationId = selectedLocationId,
                ApplicationUserID = userId
                // You may need to set other properties of the order based on your requirements
            };

            // Add the order to the context
            _context.Orders.Add(order);

            // Save changes to generate the order ID
            _context.SaveChanges();

            // Create order products and store them in the OrderProduct table
            foreach (var productCart in shoppingCart.ProductCarts)
            {
                // Create an OrderProduct instance
                OrderProduct orderProduct = new OrderProduct
                {
                    OrderId = order.ID, // Assign the order ID generated above
                    ProductId = productCart.ProductId,
                    Quantity = productCart.Quantity,
                    Price = productCart.Product.Price // You may need to adjust the price if there are discounts or other considerations
                };

                // Add the order product to the context
                _context.OrderProduct.Add(orderProduct);
            }

            // Save changes to store order products
            _context.SaveChanges();

            // Clear the shopping cart after placing the order
            _context.ShoppingCart.Remove(shoppingCart);
            _context.SaveChanges();

            // Redirect to a thank you page or order details page
            // return RedirectToAction("OrderConfirmation", new { orderId = order.ID });
            return RedirectToAction("Index", "Home");
        }



        public IActionResult OrderHistory()
        {
            // Get the current user's ID
            string userId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // Retrieve order products for the current user from the database
            var order = _context.Orders
                .Where(op => op.ApplicationUserID == userId)
                .ToList();

            // Pass order products to the view
            return View(order);
        }
        public IActionResult OrderInfo(int id)
        {
            var orderproduct = _context.OrderProduct.Include(p => p.Product).Where(o => o.OrderId == id).ToList();
            return View(orderproduct);
        }
    }
}
