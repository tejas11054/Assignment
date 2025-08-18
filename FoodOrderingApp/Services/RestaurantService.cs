using FoodOrderingApp.Models;
using FoodOrderingApp.Repositories;

namespace FoodOrderingApp.Services
{
    public class RestaurantService : IRestaurantService
    {

        private IRestaurantRepository _repository;
        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _repository = restaurantRepository;
        }
        public Restaurant Add(Restaurant restaurant)
        {
            return _repository.Add(restaurant);
        }

        public void Delete(Restaurant restaurant)
        {
            _repository.Delete(restaurant);
        }

        public Restaurant Update(Restaurant restaurant)
        {
            return _repository.Update(restaurant);
        }

        public Restaurant? Find(int id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Restaurant> GetRestaurantsByCuisine(string cuisin)
        {
            return _repository.GetRestaurantsByCuisine(cuisin);
        }
    }
}
