    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace FoodOrderingApp.Models
    {
        public class Menu
        {
            [Key]
            public int MenuId { get; set; }
            [Required(ErrorMessage = "Menu Name is Required!")]
            public string MenuName { get; set; }

            [Required(ErrorMessage = "Menu Price is Required!")]
            public double MenuPrice { get; set; }

            [Required(ErrorMessage = "Menu Category is Required!")]
            public string MenuCategory { get; set; }

            [ForeignKey("Restaurant")]
            public int? RestaurantId { get; set; }
            public virtual Restaurant? Restaurant { get; set; }
        }
    }
