using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApp.Models
{
    public class DeliveryPartner : User
    {

        [Required(ErrorMessage = "Vehicle Number is required!")]
        public string VehicleNumber { get; set; }

        [Required(ErrorMessage = "License Number is required!")]
        public string LicenseNumber { get; set; }
    }
}
