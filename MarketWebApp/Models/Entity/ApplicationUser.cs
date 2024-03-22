using Microsoft.AspNetCore.Identity;

namespace MarketWebApp.Models.Entity
{
    public class ApplicationUser:IdentityUser<string>
    {
      //  public string ApplicationUserId { get; set; }
        public string Address { get; set; }
        //public virtual List<ShoppingCart>  ShoppingCart { get; set; }
        //public virtual List<Order>? Order { get; set; }
    }

}
