using FoodOrderingApp.Models;
using FoodOrderingApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult PostLogin([FromBody] LoginViewModel usr)
        {
            if (ModelState.IsValid)
            {
                var response = _userService.Login(usr);
                if (response.isSucess)
                {
                    return Ok(response.Token);
                }
                return Unauthorized();
            }
            return BadRequest();
        }
    }
}
