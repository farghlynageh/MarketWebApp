using System.ComponentModel.DataAnnotations;

namespace MarketWebApp.Models.Entity
{
    public class ShoppingCart
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Img { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }

    }
}

