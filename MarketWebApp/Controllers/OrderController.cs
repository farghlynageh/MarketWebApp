﻿using MarketWebApp.Data;
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