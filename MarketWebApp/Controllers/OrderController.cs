using Microsoft.AspNetCore.Mvc;

namespace MarketWebApp.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
