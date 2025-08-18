using FoodOrderingApp.Models;

namespace FoodOrderingApp.Services
{
    public interface IMenuService
    {
        public Menu Add(Menu menu);
        public void Delete(Menu menu);
        public Menu Update(Menu menu);
        public Menu? Find(int id);
        public IEnumerable<Menu> GetAll();
        public IEnumerable<object>? GetMenusByRestaurantName(string name);
    }
}
