using System.ComponentModel.DataAnnotations;

namespace FoodOrderingApp.Models
{
    public enum Role { ADMIN, CUSTOMER, DELIVERY_PARTNER }
    public class UserRole
    {
        [Key]
        public int RoleID { get; set; }
        [Required(ErrorMessage = "Role is Required!")]
        public Role Role { get; set; }
        public virtual IEnumerable<User>? Users { get; set; }
    }
}
