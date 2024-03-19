using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MarketWebApp.ViewModel
{
    public class AddCategoryViewModel
    {
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category Name is required")]
        [MaxLength(50, ErrorMessage = "Category Name Must Be Less Than 50 Char ")]
        [MinLength(3, ErrorMessage = "Category Name Must Be Greater Than 3 Char")]
        [Remote(action: "CheckCategoryExistEdit", controller: "Category", AdditionalFields = "Id", ErrorMessage = "Category Name oready Exists ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please choose Category Picture ")]
        [Display(Name = "Category Picture")]
        public IFormFile CategoryImage { get; set; }
    }

    public class CategoryViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IFormFile CategoryImage { get; set; }

    }

}
