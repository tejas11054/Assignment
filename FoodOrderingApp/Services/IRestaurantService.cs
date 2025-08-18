using FoodOrderingApp.Models;

namespace FoodOrderingApp.Services
{
    public interface IRestaurantService
    {
        public Restaurant Add(Restaurant restaurant);
        public void Delete(Restaurant restaurant);
        public Restaurant Update(Restaurant restaurant);
        public Restaurant? Find(int id);
        public IEnumerable<Restaurant> GetAll();

        IEnumerable<Restaurant> GetRestaurantsByCuisine(string cuisine);
    }
}
