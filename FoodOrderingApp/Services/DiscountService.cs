using FoodOrderingApp.Models;
using FoodOrderingApp.Repositories;

namespace FoodOrderingApp.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _repository;
        public DiscountService(IDiscountRepository repository)
        {
            _repository = repository;
        }

        public Discount Add(Discount discount) => _repository.Add(discount);

        public Discount Update(Discount discount) => _repository.Update(discount);

        public void Delete(Discount discount) => _repository.Delete(discount);

        public Discount? Find(int id) => _repository.Find(id);

        public IEnumerable<Discount> GetAll() => _repository.GetAll();

        public Discount? GetTodaysDiscount() => _repository.GetTodaysDiscount();

        public void SetTodaysDiscount(int id) => _repository.SetTodaysDiscount(id);
    }
}
