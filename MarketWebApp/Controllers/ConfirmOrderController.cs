using MarketWebApp.Data;
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


        //public IActionResult UsersAndOrders()
        //{
        //    // Retrieve the list of users with their orders
        //    var usersWithOrders = _context.Users
        //        .Include(u => u.Or)
        //            .ThenInclude(o => o.OrderProducts)
        //                .ThenInclude(op => op.Product)
        //        .ToList();

        //    return View(usersWithOrders);
        //}


        [HttpPost]
        public IActionResult ConfirmOrder(int orderId)
        {
            // Retrieve the order from the database
            var order = _context.Orders.FirstOrDefault(o => o.ID == orderId);

            if (order != null)
            {
                // Update the order state to "Confirmed"
                order.State = "Confirmed";
                _context.SaveChanges();
            }

            return RedirectToAction("UsersAndOrders");
        }

    }
}
