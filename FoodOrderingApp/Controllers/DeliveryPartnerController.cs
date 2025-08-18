using FoodOrderingApp.Models;
using FoodOrderingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPartnerController : ControllerBase
    {
        private readonly IDeliveryPartnerService _deliveryService;
        private readonly IOrderService _orderService;

        public DeliveryPartnerController(IDeliveryPartnerService deliveryService, IOrderService orderService)
        {
            _deliveryService = deliveryService;
            _orderService = orderService;
        }

        [HttpGet("getAll")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}")]
        public ActionResult<IEnumerable<DeliveryPartner>> GetAll()
        {
            var partners = _deliveryService.GetAll();
            return Ok(new { message = "Delivery partners retrieved successfully.", data = partners });
        }

        [HttpPost("add")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}")]
        public ActionResult<DeliveryPartner> Create(DeliveryPartner deliveryPartner)
        {
            var created = _deliveryService.Add(deliveryPartner);
            return CreatedAtAction(nameof(GetById), new { id = created.UserId },
                new { message = "Delivery partner added successfully.", data = created });
        }

        [HttpGet("getById/{id}")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}")]
        public ActionResult<DeliveryPartner> GetById(int id)
        {
            var partner = _deliveryService.Find(id);
            if (partner == null)
                return NotFound(new { message = $"Delivery partner with ID {id} not found." });

            return Ok(new { message = "Delivery partner retrieved successfully.", data = partner });
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}, {nameof(Role.DELIVERY_PARTNER)}")]
        public ActionResult<DeliveryPartner> Update(int id, DeliveryPartner deliveryPartner)
        {
            if (id != deliveryPartner.UserId)
                return BadRequest(new { message = "ID mismatch between route and body." });

            var updated = _deliveryService.Update(deliveryPartner);
            return Ok(new { message = "Delivery partner updated successfully.", data = updated });
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}")]
        public IActionResult Delete(int id)
        {
            var partner = _deliveryService.Find(id);
            if (partner == null)
                return NotFound(new { message = $"Delivery partner with ID {id} not found." });

            _deliveryService.Delete(id);
            return Ok(new { message = "Delivery partner deleted successfully." });
        }

        // Get all orders assigned to a delivery partner
        [HttpGet("orders/{deliveryPartnerId}")]
        [Authorize(Roles = $"{nameof(Role.DELIVERY_PARTNER)}, {nameof(Role.ADMIN)}")]
        public IActionResult GetOrdersByDeliveryPartner(int deliveryPartnerId)
        {
            var orders = _deliveryService.GetOrdersByDeliveryPartner(deliveryPartnerId);
            if (orders == null || !orders.Any())
                return NotFound(new { message = "No orders assigned to this delivery partner." });

            var mappedOrders = orders.Select(order => new
            {
                order.OrderId,
                User = order.User != null ? new { Name = order.User.UserFullName, Address = order.User.UserAddress } : null,
                Restaurant = order.Restaurant?.RestaurantName,
                order.PaymentType,
                order.TotalAmount,
                order.DiscountApplied,
                order.OrderDate,
                Status = order.Status,
                OrderItems = order.OrderItems?.Select(oi => new
                {
                    oi.Menu?.MenuName,
                    oi.Menu?.MenuCategory,
                    oi.Menu?.MenuPrice,
                    oi.Quantity,
                    oi.ItemTotal
                }).ToList()
            }).ToList();

            return Ok(new { message = "Orders fetched successfully.", Orders = mappedOrders });
        }

        // Update order status by delivery partner
        [HttpPut("updateOrderStatus")]
        [Authorize(Roles = $"{nameof(Role.DELIVERY_PARTNER)}")]
        public IActionResult UpdateOrderStatus(int orderId, OrderStatus status)
        {
            var order = _orderService.GetById(orderId); // use OrderService
            if (order == null)
                return NotFound(new { message = $"Order {orderId} not found." });

            order.Status = status;
            _orderService.UpdateOrder(order); // make sure this method exists in IOrderService

            return Ok(new { message = $"Order {orderId} status updated to {status}.", orderId, newStatus = status });
        }
    }
}
