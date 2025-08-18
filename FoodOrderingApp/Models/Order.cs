using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrderingApp.Models
{
    public enum OrderStatus
    {
        Placed,
        Picked,
        Delivered,
        Cancelled
    }

    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        public string PaymentType { get; set; }

        public double TotalAmount { get; set; }
        public double DiscountApplied { get; set; }

        [ForeignKey("DeliveryPartner")]
        public int? DeliveryPartnerId { get; set; }
        public virtual DeliveryPartner DeliveryPartner { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Placed;

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
