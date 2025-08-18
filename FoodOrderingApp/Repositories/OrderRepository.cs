using FoodOrderingApp.Data;
using FoodOrderingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApp.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) => _context = context;

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders
                .Include(o => o.User)
                .Include(o => o.DeliveryPartner)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Menu)
                .ToList();
        }

        public Order Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        public Order Find(int id)
        {
            return _context.Orders
                .Include(o => o.User)
                .Include(o => o.DeliveryPartner)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Menu)
                .FirstOrDefault(o => o.OrderId == id);
        }

        public Order UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
            return Find(order.OrderId); // fetch with navigation props
        }

        public Order CancelOrder(int orderId)
        {
            var order = Find(orderId);
            if (order == null) return null;

            if (order.Status != OrderStatus.Placed)
                return order; // return as-is (not cancellable)

            order.Status = OrderStatus.Cancelled;
            _context.Orders.Update(order);
            _context.SaveChanges();

            return Find(orderId);
        }
    }
}
