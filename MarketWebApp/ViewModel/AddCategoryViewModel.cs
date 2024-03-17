using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MarketWebApp.ViewModel
{
    public class AddCategoryViewModel
    {
        [Display(Name = "Category Name")] // Change the display name to match the Category class
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name length must be between 3 and 100 characters")] // Update the length constraints
        [Remote(action: "CheckCategoryExist", controller: "Category", ErrorMessage = "Category Name oready Exists ")]

        public string Name { get; set; }


    }

    public class CategoryViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

}
