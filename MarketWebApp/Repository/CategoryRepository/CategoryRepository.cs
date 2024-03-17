using MarketWebApp.Data;
using MarketWebApp.Models.Entity;
using MarketWebApp.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace MarketWebApp.Reprository.CategoryReprositry
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CategoryRepository(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public object Categories => throw new NotImplementedException();

        public bool CheckCategoryExist(string Name)
        {
            return context.Categories.SingleOrDefault(c => c.Name.ToLower() == Name.ToLower()) == null;
        }

        public bool CheckCategoryExistEdit(string Name, int Id)
        {
            return context.Categories.SingleOrDefault(c => c.ID != Id && c.Name.ToLower() == Name.ToLower()) == null;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public Category GetCategory(int Id)
        {
            return context.Categories.SingleOrDefault(c => c.ID == Id);
        }

        public Category GetCategoryWithProducts(int Id)
        {
            return context.Categories.Where(c => c.ID == Id).Include(c => c.Products).SingleOrDefault();
        }

        public void Insert(AddCategoryViewModel addCategoryViewModel)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(EditCategoryViewModel editCategoryViewModel)
        {
            throw new NotImplementedException();
        }

        public string UploadedFile(IFormFile model, string CatName)
        {
            throw new NotImplementedException();
        }
    }
}
