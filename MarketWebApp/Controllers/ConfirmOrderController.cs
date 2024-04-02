using MarketWebApp.Data;
using MarketWebApp.Models.Entity;
using MarketWebApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketWebApp.Controllers
{
    public class ConfirmOrderController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;

        public ConfirmOrderController(ApplicationDbContext context, EmailService emailService) : base(context)
        {
            _context = context;
            _emailService = emailService;
        }
       

        [Authorize(Roles = "Admin,Cashier")]
        public IActionResult Index()
        {
            var usersWithOrders = _context.Users
                .Include(u => u.Orders.Where(o => o.State == "Pending"))
                    .ThenInclude(o => o.OrderProducts)
                        .ThenInclude(op => op.Product)
                .ToList();

            if (usersWithOrders == null || usersWithOrders.Count == 0)
            {
                return NotFound();
            }

            var viewModelList = new List<UserOrdersViewModel>();

            foreach (var user in usersWithOrders)
            {
                var userOrdersViewModel = new UserOrdersViewModel
                {
                    User = user,
                    Orders = user.Orders.ToList()
                };
                viewModelList.Add(userOrdersViewModel);
            }

            return View(viewModelList);
        }




        [HttpGet]
        [Authorize(Roles = "Admin,Cashier")]

        public IActionResult AcceptOrder(int Id)
        {
            // Retrieve the order from the database
            var order = _context.Orders.Include(o => o.applicationUser).Include(o => o.OrderProducts).ThenInclude(op => op.Product)
                               .FirstOrDefault(o => o.ID == Id);

            if (order != null)
            {
                // Check if the order state is not already "Shipping" and if there are products in the order
                if (order.State != "Shipping" && order.OrderProducts.Any())
                {
                    // Set the order state to "Shipping"
                    order.State = "Shipping";

                    // Now check if the order is in "Shipping" state
                    if (order.State == "Shipping")
                    {
                        foreach (var orderProduct in order.OrderProducts)
                        {
                            // Retrieve the corresponding product
                            var product = orderProduct.Product;

                            // Check if quantity is 0 or greater than the available stock
                            if (orderProduct.Quantity <= 0)
                            {
                                // Quantity is 0
                                TempData["ErrorMessage"] = "Quantity cannot be 0.";
                                return RedirectToAction("GetUserList", "ConfirmOrder");
                            }
                            else if (orderProduct.Quantity > product.Stock)
                            {
                                // Quantity ordered is greater than available stock
                                TempData["ErrorMessage"] = $"Insufficient stock for product: {product.Name}. Available stock: {product.Stock}.";
                                return RedirectToAction("GetUserList", "ConfirmOrder");
                            }

                            // Reduce the stock quantity by the quantity in the order
                            product.Stock -= orderProduct.Quantity;

                            // Update the product in the database
                            _context.Products.Update(product);
                        }

                        // Save changes to update the stock levels
                        _context.SaveChanges();
                    }
                    _emailService.SendEmail(order.applicationUser.UserName, "Order Accepted", "Dear, Your order has been accepted and is now being shipped.");
                }
                else
                {
                    TempData["ErrorMessage"] = "Order is already shipping or no products in the order.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Order not found.";
            }

            return RedirectToAction("Index", "ConfirmOrder");
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Cashier")]

        public IActionResult RejectOrder(int Id)
        {
            // Retrieve the order from the database
            var order = _context.Orders.Include(o => o.applicationUser).Include(o => o.OrderProducts).ThenInclude(op => op.Product)
                                          .FirstOrDefault(o => o.ID == Id);
            if (order != null)
            {
                // Update the order state to "Confirmed"
                order.State = "Rejected";
                _context.SaveChanges();
                _emailService.SendEmail(order.applicationUser.UserName, "Order Rejected", "Dear , We regret to inform you that your order has been rejected.");
            }

            return RedirectToAction("Index", "ConfirmOrder");
        }

        [Authorize(Roles = "Admin,Cashier")]

        public IActionResult DetailsOfOrder(int id)
        {
            var orderitem = _context.OrderProduct.Include(s => s.Order).Include(p => p.Product).Where(o => o.OrderId == id).ToList();

            return View(orderitem);
        }


    }
}
