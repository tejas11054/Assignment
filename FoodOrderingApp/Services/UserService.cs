using FoodOrderingApp.Models;
using FoodOrderingApp.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodOrderingApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User Add(User user)
        {
            return _userRepository.Add(user);
        }

        public User Update(User user)
        {
            return _userRepository.Update(user);
        }
        public void Delete(User user)
        {
            _userRepository.Delete(user);
        }
        public User Find(int id)
        {
            return _userRepository.Find(id);
        }
        public LoginResponseViewModel Login(LoginViewModel usr)
        {
            var response = _userRepository.Login(usr);
            if (response.isSucess)
            {
                response.Token = GenerateToken(response.User);
            }
            return response;
        }

        private string GenerateToken(User user)
        {
            var config = new ConfigurationManager();
            config.AddJsonFile("appsettings.json");
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["ConnectionStrings:secretkey"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email,user.UserEmail),
                new Claim(ClaimTypes.Role,user.UserRole.Role.ToString()),
                new Claim("MyClaim",user.UserPhone),
            };
            var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:7291",
                    audience: "https://localhost:7291",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signingCredentials
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }
    }
}
