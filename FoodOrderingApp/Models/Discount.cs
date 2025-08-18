using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApp.Models
{
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }

        [Range(1, 100, ErrorMessage = "Percentage must be between 1 and 100")]
        public double Percentage { get; set; }

        public bool IsSetToday { get; set; } = false;
    }
}
