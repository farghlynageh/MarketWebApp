using MarketWebApp.Models.Entity;
using MarketWebApp.ViewModel;

namespace MarketWebApp.Reprository.CategoryReprositry
{
    public interface ICategoryRepository
    {

        IEnumerable<Category> GetAll();
        Category GetCategory(int Id);
        Category GetCategoryWithProducts(int Id);
        void Insert(AddCategoryViewModel addCategoryViewModel);
        void Update(EditCategoryViewModel editCategoryViewModel);
        void Delete(int id);
        void Save();
        string UploadedFile(IFormFile model, string CatName);
        bool CheckCategoryExist(string Name);
        bool CheckCategoryExistEdit(string Name, int Id);
    }
}
