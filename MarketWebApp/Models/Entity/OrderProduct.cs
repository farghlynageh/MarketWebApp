using System.ComponentModel.DataAnnotations;

namespace MarketWebApp.Models.Entity
{
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; } 
    }
}
