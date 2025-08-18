using FoodOrderingApp.Models;

namespace FoodOrderingApp.Repositories
{
    public interface IOrderItemRepository
    {
        OrderItem Add(OrderItem orderItem);
        IEnumerable<OrderItem> GetByOrderId(int orderId);
    }
}
