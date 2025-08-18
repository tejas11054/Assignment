using FoodOrderingApp.Models;
using FoodOrderingApp.Repositories;

namespace FoodOrderingApp.Services
{
    public class MenuService : IMenuService
    {
        private IMenuRepository _repository;
        public MenuService(IMenuRepository menuRepository)
        {
            _repository = menuRepository;
        }
        public Menu Add(Menu menu)
        {
            return _repository.Add(menu);
        }

        public void Delete(Menu menu)
        {
            _repository.Delete(menu);
        }

        public Menu Update(Menu menu)
        {
            return _repository.Update(menu);
        }

        public Menu? Find(int id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<Menu> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<object> GetMenusByRestaurantName(string restaurantName)
        {
            return _repository.GetMenusByRestaurantName(restaurantName);
        }
    }
}
