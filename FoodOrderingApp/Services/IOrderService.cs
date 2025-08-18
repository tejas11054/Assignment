using FoodOrderingApp.Models;

namespace FoodOrderingApp.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAll();
        Order PlaceOrder(Order order, List<(int MenuId, int Quantity)> items);
        Order GetById(int id);

        Order UpdateOrder(Order order);

        public Order CancelOrder(int orderId);
    }
}
