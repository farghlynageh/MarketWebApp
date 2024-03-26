using MarketWebApp.Repository.ProductRepository;
using MarketWebApp.Repository.SupplierRepository;
using MarketWebApp.Reprository.CategoryReprositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketWebApp.Controllers
{
 //   [Authorize(Roles = "Admin")]
    public class DashBordController : Controller
    {
        private readonly IProductRepository productReprository;
        private readonly ICategoryRepository categoryReprository;
        private readonly ISupplierRepository supplierRepository;
      //  private readonly IOrderRepository orderRepository;


        public DashBordController(IProductRepository productReprository,
            ICategoryRepository categoryReprository, ISupplierRepository supplierRepository
            /*, IOrderReprository orderReprository*/)
        {
            this.productReprository = productReprository;
            this.categoryReprository = categoryReprository;
            this.supplierRepository = supplierRepository;
         //   this.orderReprository = orderReprository;
        }
        public IActionResult Index()
        {
            ViewBag.productCount = productReprository.GetAll().Count();
            ViewBag.CategoryCount = categoryReprository.GetAll().Count();
            ViewBag.SupplierCount = supplierRepository.GetAll().Count();
            //ViewBag.orderCount = orderReprository.GetAll().Count();
            return View();
        }
    }
}
