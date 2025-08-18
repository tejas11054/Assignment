using FoodOrderingApp.Models;
using FoodOrderingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet("getAll")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}")]
        public IActionResult Get()
        {
            return Ok(_discountService.GetAll());
        }

        [HttpPost("addDiscount")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}")]
        public IActionResult Post([FromBody] Discount discount)
        {
            if (!ModelState.IsValid) return BadRequest(discount);

            var added = _discountService.Add(discount);
            return CreatedAtAction("Get", new { id = added.DiscountId }, added);
        }

        [HttpGet("getById/{id}")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}")]
        public IActionResult Get(int id)
        {
            var discount = _discountService.Find(id);
            if (discount == null) return NotFound();
            return Ok(discount);
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}")]
        public IActionResult Put(int id, [FromBody] Discount discount)
        {
            if (id != discount.DiscountId) return BadRequest();
            var updated = _discountService.Update(discount);
            return Ok(updated);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}")]
        public IActionResult Delete(int id)
        {
            var discount = _discountService.Find(id);
            if (discount == null) return NotFound();
            _discountService.Delete(discount);
            return Ok("Discount deleted successfully!");
        }

        [HttpGet("today")]
        [AllowAnonymous]
        [Authorize(Roles = $"{nameof(Role.ADMIN)},{nameof(Role.CUSTOMER)},{nameof(Role.DELIVERY_PARTNER)}")]
        public IActionResult GetTodaysDiscount()
        {
            var discount = _discountService.GetTodaysDiscount();
            if (discount == null) return NotFound("No discount set for today.");
            return Ok(discount);
        }

        [HttpPost("settoday/{id}")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}")]
        public IActionResult SetTodaysDiscount(int id)
        {
            _discountService.SetTodaysDiscount(id);
            return Ok("Today's discount updated successfully.");
        }
    }
}
