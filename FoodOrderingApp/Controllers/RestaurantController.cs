using FoodOrderingApp.Models;
using FoodOrderingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("getAll")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)},{nameof(Role.CUSTOMER)},{nameof(Role.DELIVERY_PARTNER)}")]
        public IActionResult Get()
        {
            var restaurants = _restaurantService.GetAll();
            return Ok(new { message = "Restaurants fetched successfully", data = restaurants });
        }

        [HttpPost("add")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}")]
        public IActionResult Post(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                Restaurant addedRestaurant = _restaurantService.Add(restaurant);
                return CreatedAtAction("Get", new { id = addedRestaurant.RestaurantId },
                    new { message = "Restaurant added successfully", data = addedRestaurant });
            }
            return BadRequest(new { message = "Invalid restaurant details", data = restaurant });
        }

        [HttpGet("getById/{id}")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}, {nameof(Role.CUSTOMER)}, {nameof(Role.DELIVERY_PARTNER)}")]
        public IActionResult Get(int id)
        {
            Restaurant restaurant = _restaurantService.Find(id);
            if (restaurant != null)
            {
                return Ok(new { message = "Restaurant fetched successfully", data = restaurant });
            }
            return NotFound(new { message = $"Restaurant with ID {id} not found" });
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}")]
        public IActionResult Put(int id, [FromBody] Restaurant restaurant)
        {
            if (id == restaurant.RestaurantId && ModelState.IsValid)
            {
                Restaurant updatedRestaurant = _restaurantService.Update(restaurant);
                return Ok(new { message = "Restaurant updated successfully", data = updatedRestaurant });
            }
            return BadRequest(new { message = "Invalid restaurant update request", data = restaurant });
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}")]
        public IActionResult Delete(int id)
        {
            Restaurant restaurant = _restaurantService.Find(id);
            if (restaurant != null)
            {
                _restaurantService.Delete(restaurant);
                return Ok(new { message = "Restaurant deleted successfully" });
            }
            return NotFound(new { message = $"Restaurant with ID {id} not found" });
        }

        [HttpGet("by-cuisine/{cuisine}")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)},{nameof(Role.CUSTOMER)},{nameof(Role.DELIVERY_PARTNER)}")]
        public IActionResult GetRestaurantsByCuisine(string cuisine)
        {
            var restaurants = _restaurantService.GetRestaurantsByCuisine(cuisine);

            if (restaurants == null || !restaurants.Any())
                return NotFound(new { message = $"No restaurants found serving {cuisine} cuisine." });

            var result = restaurants.Select(r => new
            {
                r.RestaurantName,
                r.RestaurantAddress,
                r.RestaurantPhone,
                r.RestaurantEmail
            });

            return Ok(new { message = $"{restaurants.Count()} restaurants found serving {cuisine} cuisine", data = result });
        }
    }
}
