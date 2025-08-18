using FoodOrderingApp.Models;
using FoodOrderingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _service;
        public MenuController(IMenuService menuService)
        {
            _service = menuService;
        }

        [HttpGet("getAll")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}, {nameof(Role.CUSTOMER)}")]
        public IActionResult Get()
        {
            var menu = _service.GetAll();
            if (menu.Any())
                return Ok(new { Message = "Menus retrieved successfully.", Data = menu });

            return NotFound(new { Message = "No menus found." });
        }

        [HttpPost("addMenu")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}")]
        public IActionResult Post(Menu menu)
        {
            if (ModelState.IsValid)
            {
                Menu addedMenu = _service.Add(menu);
                return CreatedAtAction("Get", new { id = addedMenu.MenuId },
                    new { Message = "Menu added successfully.", Data = addedMenu });
            }
            return BadRequest(new { Message = "Invalid menu data.", Data = menu });
        }

        [HttpGet("getById/{id}")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)},{nameof(Role.CUSTOMER)}")]
        public IActionResult Get(int id)
        {
            Menu menu = _service.Find(id);
            if (menu != null)
            {
                return Ok(new { Message = "Menu retrieved successfully.", Data = menu });
            }
            return NotFound(new { Message = $"Menu with ID {id} not found." });
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}")]
        public IActionResult Put(int id, [FromBody] Menu menu)
        {
            if (id == menu.MenuId && ModelState.IsValid)
            {
                Menu updatedMenu = _service.Update(menu);
                return Ok(new { Message = "Menu updated successfully.", Data = updatedMenu });
            }
            return BadRequest(new { Message = "Invalid menu data or ID mismatch.", Data = menu });
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}")]
        public IActionResult Delete(int id)
        {
            Menu menu = _service.Find(id);
            if (menu != null)
            {
                _service.Delete(menu);
                return Ok(new { Message = $"Menu with ID {id} deleted successfully." });
            }
            return NotFound(new { Message = $"Menu with ID {id} not found." });
        }

        [Authorize(Roles = $"{nameof(Role.ADMIN)},{nameof(Role.CUSTOMER)},{nameof(Role.DELIVERY_PARTNER)}")]
        [HttpGet("by-restaurant/{restaurantName}")]
        public IActionResult GetMenusByRestaurantName(string restaurantName)
        {
            var menus = _service.GetMenusByRestaurantName(restaurantName);
            if (menus.Any())
                return Ok(new { Message = $"Menus for restaurant '{restaurantName}' retrieved successfully.", Data = menus });

            return NotFound(new { Message = $"No menus found for restaurant '{restaurantName}'." });
        }
    }
}
