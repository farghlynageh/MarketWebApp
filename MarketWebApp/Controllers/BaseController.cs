using MarketWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MarketWebApp.Controllers
{
    public class BaseController : Controller
    {
        ApplicationDbContext context;

        public BaseController(ApplicationDbContext _context)
        {
            context = _context;
        }
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            // Fetch data from the database
            ViewBag.Categories = context.Categories.ToList();
        }
    }
}
