using MarketWebApp.Data;
using MarketWebApp.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MarketWebApp.Controllers
{
    public class WishListController : BaseController
    {

        IHttpContextAccessor _contx;
        ApplicationDbContext context;

        List<Product> products;

        public WishListController(ApplicationDbContext _context, IHttpContextAccessor contx):base(_context)
        {
            context = _context;
            _contx = contx;
            products = new List<Product>();


        }

        [HttpPost]
        //string Name,double Price,string Image, Guid id,
        public ActionResult AddProductToWish(int id)
        {
            var wish = context.Products.Find(id);

            var existingWishlistData = _contx.HttpContext.Session.GetString("ProductData");
            List<Product> products = new List<Product>();

            if (!string.IsNullOrEmpty(existingWishlistData))
            {
                products = JsonConvert.DeserializeObject<List<Product>>(existingWishlistData);

                if (products.Any(p => p.ID == wish.ID))
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            products.Add(wish);

            var serializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string updatedProductString = JsonConvert.SerializeObject(products, serializerSettings);

            _contx.HttpContext.Session.SetString("ProductData", updatedProductString);

            return RedirectToAction("Index", "Home");

        }
        public IActionResult WishIndex()
        {
            // List<Product> products = new List<Product>();

            string productString = _contx.HttpContext.Session.GetString("ProductData");
            if (!string.IsNullOrEmpty(productString))
            {

                products = JsonConvert.DeserializeObject<List<Product>>(productString);
            }

            return View(products);
        }
    }
}
