using FoodOrderingApp.Models;

namespace FoodOrderingApp.Services
{
    public interface IReviewService
    {
        IEnumerable<Review> GetAll();
        Review Find(int id);
        void Add(Review review);
    }
}
