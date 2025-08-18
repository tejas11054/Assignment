using FoodOrderingApp.Data;
using FoodOrderingApp.Models;

namespace FoodOrderingApp.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Review> GetAll()
        {
            return _context.Reviews.ToList();
        }

        public Review Find(int id)
        {
            return _context.Reviews.FirstOrDefault(r => r.ReviewId == id);
        }

        public void Add(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }
    }
}
