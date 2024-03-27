using MarketWebApp.Models.Entity;

namespace MarketWebApp.ViewModel
{
    public class UserOrdersViewModel
    {
        public ApplicationUser User { get; set; }
        public List<Order> Orders { get; set; }
    }
}
