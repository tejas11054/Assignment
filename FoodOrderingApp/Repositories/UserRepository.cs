using FoodOrderingApp.Data;
using FoodOrderingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<User> GetAll()
        {
            return _context.Users.Include(u => u.UserRole).ToList();
        }
        public User Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        public User Update(User user)
        {
            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return user;
        }
        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        public User Find(int id)
        {
            User user = _context.Users.Include(u => u.UserRole).FirstOrDefault(u => u.UserId == id);
            return user;
        }

        public LoginResponseViewModel Login(LoginViewModel usr)
        {
            var user = _context.Users.Include(u => u.UserRole).FirstOrDefault((u) => (u.UserName.Equals(usr.UserName) && u.Password.Equals(usr.Password)));
            LoginResponseViewModel response;
            if (user != null)
            {
                response = new LoginResponseViewModel
                {
                    isSucess = true,
                    User = user,
                    Token = ""
                };
                return response;
            }
            response = new LoginResponseViewModel
            {
                isSucess = false,
                User = null,
                Token = ""

            };
            return response;
        }
    }
}
