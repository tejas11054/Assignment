namespace FoodOrderingApp.Models
{
    public class Review
    {
        public int ReviewId { get; set; }   // Primary Key
        public int UserId { get; set; }     // FK -> User
        public string Comment { get; set; }
        public int Rating { get; set; }     // Example: 1 to 5
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
