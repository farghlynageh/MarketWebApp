using MarketWebApp.Data;
using MarketWebApp.Models;
using MarketWebApp.Models.Entity;
using MarketWebApp.Reprository.CategoryReprositry;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace MarketWebApp
{
    public class Program

    {
        public static void Main(string[] args)
        {

            //        var builder = WebApplication.CreateBuilder(args);

            //        // Add services to the container.
            //        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            //        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //            options.UseSqlServer(connectionString));

            //        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
            //            .AddEntityFrameworkStores<ApplicationDbContext>();

            //        builder.Services.AddControllersWithViews();


            //        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            //        builder.Services.AddSession();
            //        builder.Services.AddRazorPages();

            //        builder.Services.ConfigureApplicationCookie(options =>
            //        {

            //            options.LoginPath = "/Identity/Account/Login";
            //            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            //        });


            //        var app = builder.Build();

            //        // Configure the HTTP request pipeline.
            //        if (app.Environment.IsDevelopment())
            //        {
            //            app.UseMigrationsEndPoint();
            //        }
            //        else
            //        {
            //            app.UseExceptionHandler("/Home/Error");
            //            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //            app.UseHsts();
            //        }

            //        app.UseHttpsRedirection();
            //        app.UseStaticFiles();

            //        app.UseRouting();

            //        app.UseAuthorization();

            //        app.UseSession();

            //        app.MapControllerRoute(
            //            name: "default",
            //            pattern: "{controller=Home}/{action=Index}/{id?}");
            //        app.MapRazorPages();

            //        app.Run();
            //    }
            //}
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddSession();
            builder.Services.AddRazorPages();

            builder.Services.ConfigureApplicationCookie(options =>
            {

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
                    app.UseSession();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    } 
}
