
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MarketWebApp.Reprository.CategoryReprositry;
using MarketWebApp.ViewModel;

namespace MarketWebApp.Controllers
{
    // sooo
   // [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository repository;

        public CategoriesController(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        // GET: Categories

        public IActionResult Index()
        {
            ViewBag.PageCount = (int)Math.Ceiling((decimal)repository.GetAll().Count() / 5m);
            return View(repository.GetAll());
        }

        public IActionResult GetCategoriess(int pageNumber, int pageSize = 5)
        {
            var Categories = repository.GetAll()
           .OrderBy(p => p.ID)
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();
            return PartialView("_CategoryTable", Categories);
        }


        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddCategoryViewModel categoryViewModel)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    repository.Insert(categoryViewModel);
                    repository.Save();
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(categoryViewModel);

                }
            }
            else
            {
                return View(categoryViewModel);
            }

        }

        public IActionResult CheckCategoryExist(string Name)
        {
            if (repository.CheckCategoryExist(Name))
                return Json(true);
            else
                return Json(false);
        }

        // GET: Categories/Edit/5

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var category = repository.GetCategory(Id);
            EditCategoryViewModel categoryViewModel = new EditCategoryViewModel();
            categoryViewModel.ID = category.ID;
            categoryViewModel.Name = category.Name;
            categoryViewModel.Image = category.Img;
            return View(categoryViewModel);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditCategoryViewModel categoryViewModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    repository.Update(categoryViewModel);
                    repository.Save();
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(categoryViewModel);
                }
            }
            else
            {
                return View(categoryViewModel);
            }
        }


        public IActionResult CheckCategoryExistEdit(string Name, int Id)
        {
            if (repository.CheckCategoryExistEdit(Name, Id))
                return Json(true);
            else
                return Json(false);
        }


        // GET: Categories/Delete/5
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var data = repository.GetCategoryWithProducts(Id);
            ViewBag.flag = data.Products.Count > 0 ? true : false;
            return View(data);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int Id)
        {

            if (ModelState.IsValid)
            {
                repository.Delete(Id);
                repository.Save();
                return RedirectToAction("Index");
            }
            return View("Delete");
        }
        
        //// GET: Categories/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var category = await repository.Categories
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(category);
        //}
    
    }
}
