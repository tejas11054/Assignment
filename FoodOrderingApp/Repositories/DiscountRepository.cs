using FoodOrderingApp.Data;
using FoodOrderingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApp.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly AppDbContext _dbContext;
        public DiscountRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Discount Add(Discount discount)
        {
            _dbContext.Discounts.Add(discount);
            _dbContext.SaveChanges();
            return discount;
        }

        public Discount Update(Discount discount)
        {
            _dbContext.Entry(discount).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return discount;
        }

        public void Delete(Discount discount)
        {
            _dbContext.Discounts.Remove(discount);
            _dbContext.SaveChanges();
        }

        public Discount? Find(int id)
        {
            return _dbContext.Discounts.FirstOrDefault(d => d.DiscountId == id);
        }

        public IEnumerable<Discount> GetAll()
        {
            return _dbContext.Discounts.ToList();
        }

        public Discount? GetTodaysDiscount()
        {
            return _dbContext.Discounts.FirstOrDefault(d => d.IsSetToday);
        }

        public void SetTodaysDiscount(int id)
        {
            // reset all discounts
            var allDiscounts = _dbContext.Discounts.ToList();
            foreach (var discount in allDiscounts)
            {
                discount.IsSetToday = false;
            }

            // set the selected one
            var selected = _dbContext.Discounts.FirstOrDefault(d => d.DiscountId == id);
            if (selected != null)
            {
                selected.IsSetToday = true;
            }

            _dbContext.SaveChanges();
        }

    }
}
