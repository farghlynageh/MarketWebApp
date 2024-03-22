using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        //  public DateTime CreateTime { get; set; }

        //[ForeignKey("ApplicationUser")]
        //public string ApplicationUserId { get; set; }
        //public virtual ApplicationUser ApplicationUser{ get; set; }


        // public virtual List <Order> Orders { get; set; }

        //[ForeignKey("Order")]
        //public int? OrderId { get; set; }
        //public virtual Order Order { get; set; }

        //[ForeignKey("Product")]
        //public int? ProductId { get; set; }
        //public virtual Product Product{ get; set; }

    } 

}

