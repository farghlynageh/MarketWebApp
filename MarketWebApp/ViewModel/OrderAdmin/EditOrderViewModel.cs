using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MarketWebApp.ViewModel.Order
{
    public class EditOrderViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Start date and time cannot be empty")]
        [DataType(DataType.DateTime)]
        //[Range(typeof(DateTime), DateTime.UtcNow.ToString() , "1/1/2025")]
        [Column(TypeName = "datetime2")]
        public DateTime ResevationDate { get; set; }


        [Required(ErrorMessage = "Start date and time cannot be empty")]
        [DataType(DataType.DateTime)]
        //[Range(typeof(DateTime), DateTime.UtcNow.ToString() , "1/1/2025")]
        [Column(TypeName = "datetime2")]
        public DateTime DeliveryDate { get; set; }


        [Display(Name = "Order Cost")]
        [Range(1, 10000, ErrorMessage = "Order Cost Must Be Between 1 and 10000")]
        public double Cost { get; set; }
        [Display(Name = "Order destination")]
        [MaxLength(200, ErrorMessage = "Order destination Must Be Less Than 50 Char ")]
        [MinLength(10, ErrorMessage = "Order destination Must Be Less Than 50 Char")]
        public string destination { get; set; }
        [Display(Name = "Order Confirm")]
        public bool IsConfirmed { get; set; }

        public string UserId { get; set; }
    }
}
