using MarketWebApp.Models;
using MarketWebApp.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarketWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet properties and other configurations...

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCart { get; set; }
        public virtual DbSet<OrderProduct> OrderProduct { get; set; }
        public virtual DbSet<ProductCart> ProductCart { get; set; }
       // public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }

       // public virtual DbSet<ApplicationRole> ApplicationRole { get; set; }
    }
}
