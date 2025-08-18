using FoodOrderingApp.Models;

namespace FoodOrderingApp.Repositories
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAll();
        public User Add(User user);
        public User Update(User user);
        public void Delete(User user);
        public User Find(int id);
        public LoginResponseViewModel Login(LoginViewModel usr);
    }
}
