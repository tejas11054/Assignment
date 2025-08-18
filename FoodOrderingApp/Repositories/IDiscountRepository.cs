using FoodOrderingApp.Models;

namespace FoodOrderingApp.Repositories
{
    public interface IDiscountRepository
    {
        Discount Add(Discount discount);
        Discount Update(Discount discount);
        void Delete(Discount discount);
        Discount? Find(int id);
        IEnumerable<Discount> GetAll();
        Discount? GetTodaysDiscount();
        void SetTodaysDiscount(int id);
    }
}
