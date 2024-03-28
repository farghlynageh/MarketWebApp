﻿using Microsoft.AspNetCore.Identity;

namespace MarketWebApp.Models.Entity
{
    public class ApplicationUser: IdentityUser
    {
        //public UserManager<ApplicationUser> userManager;
        //public ApplicationUser(UserManager<ApplicationUser> _userManager)
        //{
        //    userManager = _userManager;
        //}

        //  public string ApplicationUserId { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        //public virtual List<ShoppingCart>  ShoppingCart { get; set; }
        //public virtual List<Order>? Order { get; set; }

    }

}
