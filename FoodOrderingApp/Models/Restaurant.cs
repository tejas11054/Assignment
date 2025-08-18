using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApp.Models
{
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }
        [Required(ErrorMessage = "Restaurant Name is Required!")]
        public string RestaurantName { get; set; }

        [Required(ErrorMessage = "Restaurant Address is Required!")]
        public string RestaurantAddress { get; set; }

        [Required(ErrorMessage = "Restaurant Cuisin is Required!")]
        public string RestaurantCuisin { get; set; }

        [Required(ErrorMessage = "Restaurant Email is Required!")]
        [EmailAddress(ErrorMessage = "Email address is not in proper format!")]
        public string RestaurantEmail { get; set; }

        [RegularExpression(@"^[0-9]{3}-[0-9]{3}$", ErrorMessage = "The Phone number must be in proper format!")]
        public string RestaurantPhone { get; set; }

        public virtual ICollection<Menu>? Menus { get; set; }
    }
}
