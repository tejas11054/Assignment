using FoodOrderingApp.Models;
using FoodOrderingApp.Repositories;

namespace FoodOrderingApp.Services
{
    public class DeliveryPartnerService : IDeliveryPartnerService
    {
        private readonly IDeliveryPartnerRepository _repository;

        public DeliveryPartnerService(IDeliveryPartnerRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<DeliveryPartner> GetAll() => _repository.GetAll();

        public DeliveryPartner Find(int id) => _repository.Find(id);

        public DeliveryPartner Add(DeliveryPartner deliveryPartner) => _repository.Add(deliveryPartner);

        public DeliveryPartner Update(DeliveryPartner deliveryPartner) => _repository.Update(deliveryPartner);

        public void Delete(int id)
        {
            var dp = _repository.Find(id);
            if (dp != null)
            {
                _repository.Delete(dp);
            }
        }

        public IEnumerable<Order> GetOrdersByDeliveryPartner(int deliveryPartnerId)
        {
            return _repository.GetOrdersByDeliveryPartner(deliveryPartnerId);
        }

    }
}

