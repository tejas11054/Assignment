using FoodOrderingApp.Data;
using FoodOrderingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApp.Repositories
{
    public class DeliveryPartnerRepository : IDeliveryPartnerRepository
    {
        private readonly AppDbContext _context;

        public DeliveryPartnerRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DeliveryPartner> GetAll()
        {
            return _context.DeliveryPartners.ToList();
        }

        public DeliveryPartner Find(int id)
        {
            return _context.DeliveryPartners.FirstOrDefault(dp => dp.UserId == id);
        }

        public DeliveryPartner Add(DeliveryPartner deliveryPartner)
        {
            _context.DeliveryPartners.Add(deliveryPartner);
            _context.SaveChanges();
            return deliveryPartner;
        }

        public DeliveryPartner Update(DeliveryPartner deliveryPartner)
        {
            _context.Entry(deliveryPartner).State = EntityState.Modified;
            _context.SaveChanges();
            return deliveryPartner;
        }

        public void Delete(DeliveryPartner deliveryPartner)
        {
            _context.DeliveryPartners.Remove(deliveryPartner);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetOrdersByDeliveryPartner(int deliveryPartnerId)
        {
            return _context.Orders
                .Include(o => o.User)
                .Include(o => o.Restaurant)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Menu)
                .Where(o => o.DeliveryPartnerId == deliveryPartnerId)
                .ToList();
        }

    }
}
