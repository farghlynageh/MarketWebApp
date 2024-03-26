
using DinkToPdf.Contracts;
using MarketWebApp.Data;
using MarketWebApp.Repository.ProductRepository;
using MarketWebApp.Repository.SupplierRepository;
using MarketWebApp.Reprository.CategoryReprositry;
using MarketWebApp.Reprository.OrderReprository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Azure.WebJobs;
using PdfSharp;
using PdfSharp.Pdf;


namespace MarketWebApp.Controllers
{
    public class ReportPDFController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IConverter _pdfConverter;
        private readonly IProductRepository productReprository;
        private readonly ICategoryRepository categoryReprositry;
        private readonly ISupplierRepository supplierRepository;
        private readonly IOrderAdminRepository orderReprository;
        public ReportPDFController(IConverter pdfConverter, ApplicationDbContext context, IProductRepository productReprository,
            ICategoryRepository categoryReprositry, ISupplierRepository supplierRepository
            , IOrderAdminRepository orderReprository)
        {
            this.context = context;
            _pdfConverter = pdfConverter;
            this.productReprository = productReprository;
            this.categoryReprositry = categoryReprositry;
            this.supplierRepository = supplierRepository;
            this.orderReprository = orderReprository;
        }
        [HttpGet]
        public async Task<IActionResult> GeneratePDF(string InvoiceNo)
        {
            ViewBag.productCount = productReprository.GetAll().Count();
            ViewBag.CategoryCount = categoryReprositry.GetAll().Count();
            ViewBag.SupplierCount = supplierRepository.GetAll().Count();
            ViewBag.orderCount = orderReprository.GetAll().Count();

            // Assuming context.Orders include a navigation property to Products
            var monthOrders = context.Orders
                .Where(c => c.Date.Month == DateTime.Now.Month)
                .ToList(); // Retrieve orders for the current month and materialize the query

            float monthCost = monthOrders
                .SelectMany(o => o.OrderProducts) // Flatten the nested collections of products
                .Sum(p => p.Price); // Sum the prices of all products in the orders for the month

          //  ViewBag.yearCost = context.Orders.Where(c => c.Date.Year == DateTime.Now.Year).Sum(c => c.Price);
            ViewBag.monthOrders = context.Orders.Where(c => c.Date.Month == DateTime.Now.Month).Count();
            ViewBag.yearOrders = context.Orders.Where(c => c.Date.Year == DateTime.Now.Year).Count();

            //return View(orderreport);
            var htmlContent = await this.RenderViewToStringAsync("GeneratePDF", null);
            var document = new PdfDocument();
            //string HtmlContent = "<h1>fgdhdhd</h1>";
            // PdfGenerator.AddPdfPages(document, htmlContent, PageSize.A4);

            byte[] response;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }
            string FileName = "Sales Report" + InvoiceNo + ".pdf";
            return File(response, "application/pdf", FileName);
        }
        public async Task<string> RenderViewToStringAsync(string viewName, object model)
        {

            ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                var viewEngineResult = HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;

                var viewContext = new ViewContext(
                    ControllerContext,
                    viewEngineResult.FindView(ControllerContext, viewName, true).View,
                    ViewData,
                    TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewContext.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }

        }
    }
}
