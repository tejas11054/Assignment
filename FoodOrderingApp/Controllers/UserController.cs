using FoodOrderingApp.Models;
using FoodOrderingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        // GET all users
        [HttpGet("getAll")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}")]
        public IActionResult Get()
        {
            var users = _service.GetAll();
            if (users == null || !users.Any())
                return Ok(new { Message = "No users found." });

            return Ok(new { Message = "Users retrieved successfully.", Users = users });
        }

        // POST - Add a user
        [HttpPost("add")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)},{nameof(Role.CUSTOMER)},{nameof(Role.DELIVERY_PARTNER)}")]
        public IActionResult Post(User user)
        {
            if (ModelState.IsValid)
            {
                var addedUser = _service.Add(user);
                return CreatedAtAction("Get", new { id = addedUser.UserId },
                    new { Message = "User added successfully.", User = addedUser });
            }
            return BadRequest(new { Message = "Invalid user details provided.", User = user });
        }

        // GET by id
        [HttpGet("getById/{id}")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}")]
        public IActionResult Get(int id)
        {
            var user = _service.Find(id);
            if (user == null)
                return NotFound(new { Message = $"User with ID {id} not found." });

            return Ok(new { Message = "User retrieved successfully.", User = user });
        }

        // PUT - Update
        [HttpPut("update/{id}")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)},{nameof(Role.CUSTOMER)},{nameof(Role.DELIVERY_PARTNER)}")]
        public IActionResult Put(int id, [FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                var modifiedUser = _service.Update(user);
                if (modifiedUser != null)
                {
                    return Ok(new { Message = "User updated successfully.", User = modifiedUser });
                }
                return NotFound(new { Message = $"User with ID {id} not found." });
            }
            return BadRequest(new { Message = "Invalid user details provided.", User = user });
        }

        // DELETE
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}")]
        public IActionResult Delete(int id)
        {
            var user = _service.Find(id);
            if (user != null)
            {
                _service.Delete(user);
                return Ok(new { Message = "User deleted successfully." });
            }
            return NotFound(new { Message = $"User with ID {id} not found." });
        }
    }
}
