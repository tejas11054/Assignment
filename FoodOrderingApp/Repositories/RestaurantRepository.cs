using FoodOrderingApp.Data;
using FoodOrderingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApp.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private AppDbContext _dbContext;
        public RestaurantRepository(AppDbContext appContext)
        {
            _dbContext = appContext;
        }

        public Restaurant Add(Restaurant restaurant)
        {
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
            return restaurant;
        }

        public void Delete(Restaurant restaurant)
        {
            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
        }

        public Restaurant Update(Restaurant restaurant)
        {
            _dbContext.Entry(restaurant).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
            return restaurant;
        }

        public Restaurant? Find(int id)
        {
            Restaurant restaurant = _dbContext.Restaurants.FirstOrDefault((a) => a.RestaurantId == id);
            if (restaurant != null)
            {
                return restaurant;
            }

            return null;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _dbContext.Restaurants.Include(a => a.Menus).ToList();
        }

        public IEnumerable<Restaurant> GetRestaurantsByCuisine(string cuisine)
        {
            return _dbContext.Restaurants
                .Where(r => r.RestaurantCuisin != null &&
                            r.RestaurantCuisin.ToLower().Contains(cuisine.ToLower()));
        }
    }
}
