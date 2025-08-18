using FoodOrderingApp.Models;

namespace FoodOrderingApp.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order Add(Order order);
        Order Find(int id);
        Order UpdateOrder(Order order);

        public Order CancelOrder(int orderId);
    }
}
