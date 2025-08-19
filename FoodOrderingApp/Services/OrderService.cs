using FoodOrderingApp.Data;
using FoodOrderingApp.Models;
using FoodOrderingApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderItemRepository _orderItemRepo;
        private readonly AppDbContext _context;

        public OrderService(IOrderRepository orderRepo, IOrderItemRepository orderItemRepo, AppDbContext context)
        {
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
            _context = context;
        }

        public IEnumerable<Order> GetAll() => _context.Orders
            .Include(o => o.User)
            .Include(o => o.Restaurant)
            .Include(o => o.DeliveryPartner)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Menu)
            .ToList();

        public Order GetById(int id) => _context.Orders
            .Include(o => o.User)
            .Include(o => o.Restaurant)
            .Include(o => o.DeliveryPartner)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Menu)
            .FirstOrDefault(o => o.OrderId == id);

        public Order PlaceOrder(Order order, List<(int MenuId, int Quantity)> items)
        {
            order.OrderItems = new List<OrderItem>();
            double total = 0;

            foreach (var (menuId, qty) in items)
            {
                var menu = _context.Menus.Find(menuId);
                if (menu != null)
                {
                    double itemTotal = menu.MenuPrice * qty;
                    total += itemTotal;

                    var orderItem = new OrderItem
                    {
                        MenuId = menuId,
                        Quantity = qty,
                        ItemTotal = itemTotal,
                        OrderItemId = 0
                    };
                    order.OrderItems.Add(orderItem);
                }
            }

            var discount = _context.Discounts.FirstOrDefault(d => d.IsSetToday);
            double discountAmount = discount != null ? discount.Percentage * total / 100 : 0;

            order.TotalAmount = total - discountAmount;
            order.DiscountApplied = discountAmount;

            // Assign random delivery partner
            var partners = _context.DeliveryPartners.ToList();
            if (partners.Any())
            {
                var random = new Random();
                order.DeliveryPartnerId = partners[random.Next(partners.Count)].UserId;
            }

            // Set initial status to Placed
            order.Status = OrderStatus.Placed;

            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var oi in order.OrderItems)
            {
                oi.OrderId = order.OrderId;
                oi.OrderItemId = 0;
                _context.OrderItems.Add(oi);
            }
            _context.SaveChanges();

            return GetById(order.OrderId); // fetch with all navigation props
        }

        // New method to update an existing order
        public Order UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
            return GetById(order.OrderId); // fetch with navigation props
        }

        public Order CancelOrder(int orderId)
        {
            return _orderRepo.CancelOrder(orderId);
        }
    }
}
