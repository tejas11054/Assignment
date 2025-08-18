using FoodOrderingApp.Data;
using FoodOrderingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApp.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly AppDbContext _context;
        public OrderItemRepository(AppDbContext context) => _context = context;

        public OrderItem Add(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();
            return orderItem;
        }

        public IEnumerable<OrderItem> GetByOrderId(int orderId)
        {
            return _context.OrderItems
                .Include(oi => oi.Menu)
                .Where(oi => oi.OrderId == orderId)
                .ToList();
        }
    }
}
