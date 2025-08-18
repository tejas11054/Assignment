
using FoodOrderingApp.Models;
using FoodOrderingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace API_SmartLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewController(IReviewService service)
        {
            _service = service;
        }

        // GET: api/review
        [HttpGet("getAll")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}, {nameof(Role.CUSTOMER)}, {nameof(Role.DELIVERY_PARTNER)}")]
        public IActionResult GetAll()
        {
            var reviews = _service.GetAll();
            if (!reviews.Any())
                return NotFound(new { message = "No reviews found." });

            return Ok(reviews);
        }


        // POST: api/review
        [HttpPost("add")]
        [Authorize(Roles = "CUSTOMER")]
        public IActionResult Add([FromBody] Review review)
        {
            if (review == null)
                return BadRequest(new { message = "Invalid review data." });

            _service.Add(review);
            return Ok(new { message = "Review added successfully.", review });
        }
    }
}
