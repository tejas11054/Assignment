using FoodOrderingApp.Models;

namespace FoodOrderingApp.Repositories
{
    public interface IDeliveryPartnerRepository
    {
        IEnumerable<DeliveryPartner> GetAll();
        DeliveryPartner Find(int id);
        DeliveryPartner Add(DeliveryPartner deliveryPartner);
        DeliveryPartner Update(DeliveryPartner deliveryPartner);
        void Delete(DeliveryPartner deliveryPartner);
        IEnumerable<Order> GetOrdersByDeliveryPartner(int deliveryPartnerId);

    }
}
