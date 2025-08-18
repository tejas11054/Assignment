using FoodOrderingApp.Models;
using FoodOrderingApp.Repositories;

namespace FoodOrderingApp.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;

        public ReviewService(IReviewRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Review> GetAll()
        {
            return _repository.GetAll();
        }

        public Review Find(int id)
        {
            return _repository.Find(id);
        }

        public void Add(Review review)
        {
            _repository.Add(review);
        }
    }
}
