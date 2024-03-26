using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MarketWebApp.ViewModel.Order
{
    public class AddOrderViewModel
    {

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Order request date can't be empty")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }


        //[Required(ErrorMessage = "Start date and time cannot be empty")]
        //[DataType(DataType.DateTime)]
        ////[Range(typeof(DateTime), DateTime.UtcNow.ToString() , "1/1/2025")]
        //[Column(TypeName = "datetime2")]
        //public DateTime DeliveryDate { get; set; }

        //[Display(Name = "Order Cost")]
        //[Range(1, 10000, ErrorMessage = "Order Cost Must Be Between 1 and 10000")]
        //public double Cost { get; set; }


        [Display(Name = "Product Category")]
        public int LocationID { get; set; }

        public string applicationUser { get; set; }
    }
}
