using FoodOrderingApp.Models;

namespace FoodOrderingApp.Repositories
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetAll();
        Review Find(int id);
        void Add(Review review);
    }
}
