using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MarketWebApp.ViewModel.Product
{
    public class AddProductViewModel
    {
        [Display(Name = "Product Name")]
        [MaxLength(50, ErrorMessage = "Product Name Must Be Less Than 50 Char ")]
        [MinLength(3, ErrorMessage = "Product Name Must Be Greater Than 3 Char")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Product name can only contain letters and spaces")]

        public string? Name { get; set; }

        [Required(ErrorMessage = "Please Choose Product Picture")]
        [Display(Name = "Product Picture")]
        [AllowedExtensions([".jpg", ".jpeg", ".png"], ErrorMessage = "Invalid file format. Only JPG, JPEG, and PNG are allowed.")]

        public IFormFile? Img { get; set; }

        [Display(Name = "Product Price")]
        [Range(1, 10000, ErrorMessage = "Price must be between 1 and 10000.")]
        public float Price { get; set; }
        [Display(Name="Supplier")]
        public int SupplierId { get; set; }
        [Range(0, 99, ErrorMessage = "Discount must be between 0 and 99.")]
        public float Discount { get; set; }

        [Display(Name = "Product Amount")]
        [Range(1, 10000, ErrorMessage = "Product Amount Must Be Between 1 to 1000")]
        public int Stock { get; set; }

        [Display(Name = "Department")]
        public int CategoryID { get; set; }

    }
}
