using FoodOrderingApp.Models;

namespace FoodOrderingApp.Repositories
{
    public interface IMenuRepository
    {
        public Menu Add(Menu menu);
        public void Delete(Menu menu);
        public Menu Update(Menu menu);
        public Menu? Find(int id);
        public IEnumerable<Menu> GetAll();
        public IEnumerable<object> GetMenusByRestaurantName(string restaurantName);

    }
}
