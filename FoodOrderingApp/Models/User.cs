using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "UserFullName is Required!")]
        public string UserFullName { get; set; }

        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [RegularExpression(@"^[0-9]{3}-[0-9]{3}$", ErrorMessage = "The Phone number must be in proper format!")]
        public string UserPhone { get; set; }

        [Required(ErrorMessage = "User Email is Required!")]
        [EmailAddress(ErrorMessage = "Email is not in proper format")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "User Address is Required!")]
        public string UserAddress { get; set; }

        [DataType(DataType.Date)]
        public DateTime UserJoinDate { get; set; }

        [ForeignKey("UserRole")]
        public int? RoleId { get; set; }
        public virtual UserRole? UserRole { get; set; }
    }
}
