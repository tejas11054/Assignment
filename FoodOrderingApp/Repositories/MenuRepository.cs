using FoodOrderingApp.Data;
using FoodOrderingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApp.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private AppDbContext _dbContext;
        public MenuRepository(AppDbContext appContext)
        {
            _dbContext = appContext;
        }

        public Menu Add(Menu menu)
        {
            _dbContext.Menus.Add(menu);
            _dbContext.SaveChanges();
            return menu;
        }

        public void Delete(Menu menu)
        {
            _dbContext.Menus.Remove(menu);
            _dbContext.SaveChanges();
        }

        public Menu Update(Menu menu)
        {
            _dbContext.Entry(menu).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
            return menu;
        }

        public Menu? Find(int id)
        {
            Menu menu = _dbContext.Menus.Include(b => b.Restaurant).FirstOrDefault((a) => a.MenuId == id);
            if (menu != null)
            {
                return menu;
            }

            return menu;
        }

        public IEnumerable<Menu> GetAll()
        {
            return _dbContext.Menus.Include(b => b.Restaurant).ToList();
        }

        public IEnumerable<object> GetMenusByRestaurantName(string restaurantName)
        {
            return _dbContext.Menus
                 .Where(m => m.Restaurant != null && m.Restaurant.RestaurantName == restaurantName)
                 .Select(m => new
                 {
                     menuId = m.MenuId,
                     menuName = m.MenuName,
                     menuPrice = m.MenuPrice,
                     menuCategory = m.MenuCategory
                 })
                 .ToList();

        }

    }
}
