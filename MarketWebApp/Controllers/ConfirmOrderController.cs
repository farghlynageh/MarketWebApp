using MarketWebApp.Data;
using MarketWebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketWebApp.Controllers
{
    public class ConfirmOrderController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public ConfirmOrderController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IActionResult GetUserList()
        {
            // Retrieve users who have placed orders
            var usersWithOrders = _context.Orders
                .Select(o => o.applicationUser) // Select the ApplicationUser associated with each order
                .Distinct() // Ensure unique users
                .ToList();

            return View(usersWithOrders);
        }



        public IActionResult Index(string userId)
        {
            if (userId == null)
            {
                // Handle case where userId is not provided
                return BadRequest();
            }

            // Retrieve the user by ID
            var user = _context.Users
                               .Include(u => u.Orders)
                                   .ThenInclude(o => o.OrderProducts)
                                       .ThenInclude(op => op.Product)
                               .FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                // Handle case where user is not found
                return NotFound();
            }

            // Create a list with a single UserOrdersViewModel instance
            var viewModelList = new List<UserOrdersViewModel>
    {
        new UserOrdersViewModel
        {
            User = user,
            Orders = user.Orders.ToList()
        }
    };

            return View(viewModelList);
        }



        [HttpGet]
        public IActionResult AcceptOrder(int Id)
        {
            // Retrieve the order from the database
            var order = _context.Orders.FirstOrDefault(o => o.ID == Id);

            if (order != null)
            {
                // Update the order state to "Confirmed"
                order.State = "Shipping";
                _context.SaveChanges();
            }
            return RedirectToAction("GetUserList", "ConfirmOrder");
        }

        [HttpGet]
        public IActionResult RejectOrder(int Id)
        {
            // Retrieve the order from the database
            var order = _context.Orders.FirstOrDefault(o => o.ID == Id);

            if (order != null)
            {
                // Update the order state to "Confirmed"
                order.State = "Rejected";
                _context.SaveChanges();
            }

            return RedirectToAction("GetUserList", "ConfirmOrder");
        }
        public IActionResult DetailsOfOrder(int id)
        {
            var orderitem = _context.OrderProduct.Include(s => s.Order).Include(p => p.Product).Where(o => o.OrderId == id).ToList();

            return View(orderitem);
        }


    }
}
