using FoodOrderingApp.Models;

namespace FoodOrderingApp.Services
{
    public interface IDeliveryPartnerService
    {
        IEnumerable<DeliveryPartner> GetAll();
        DeliveryPartner Find(int id);
        DeliveryPartner Add(DeliveryPartner deliveryPartner);
        DeliveryPartner Update(DeliveryPartner deliveryPartner);
        void Delete(int id);
        IEnumerable<Order> GetOrdersByDeliveryPartner(int deliveryPartnerId);


    }
}
