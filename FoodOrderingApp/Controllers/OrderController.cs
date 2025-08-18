using FoodOrderingApp.Models;
using FoodOrderingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FoodOrderingApp.Controllers
{
    
    public class PlaceOrderRequest
    {
        public int UserId { get; set; }
        public int RestaurantId { get; set; }
        public string PaymentType { get; set; }
        public List<OrderItemRequest> Items { get; set; }
    }

    public class OrderItemRequest
    {
        public int MenuId { get; set; }
        public int Quantity { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrderController(IOrderService service) => _service = service;

        [HttpGet("getAll")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}")]
        public IActionResult GetAll()
        {
            var orders = _service.GetAll().Select(o => new
            {
                OrderId = o.OrderId,
                User = o.User.UserFullName,
                UserAddress = o.User.UserEmail,
                Restaurant = o.Restaurant.RestaurantName,
                o.PaymentType,
                o.TotalAmount,
                o.DiscountApplied,
                DeliveryPartner = o.DeliveryPartner.UserFullName,
                o.Status,
                o.OrderDate,
                Items = o.OrderItems.Select(oi => new
                {
                    oi.Menu.MenuName,
                    oi.Quantity,
                    oi.ItemTotal
                })
            });

            return Ok(new { message = "Orders retrieved successfully.", data = orders });
        }

        [HttpPost("placeOrder")]
        [Authorize(Roles = $"{nameof(Role.CUSTOMER)}")]
        public IActionResult PlaceOrder([FromBody] PlaceOrderRequest request)
        {
            if (request.Items == null || !request.Items.Any())
                return BadRequest(new { Message = "No menu items selected." });

            var order = new Order
            {
                UserId = request.UserId,
                RestaurantId = request.RestaurantId,
                PaymentType = request.PaymentType,
                Status = OrderStatus.Placed
            };

            var items = request.Items.Select(i => (i.MenuId, i.Quantity)).ToList();
            var savedOrder = _service.PlaceOrder(order, items);

            var result = new
            {
                savedOrder.OrderId,
                User = savedOrder.User.UserFullName,
                UserAddress = savedOrder.User.UserEmail,
                Restaurant = savedOrder.Restaurant.RestaurantName,
                savedOrder.PaymentType,
                savedOrder.TotalAmount,
                savedOrder.DiscountApplied,
                DeliveryPartner = savedOrder.DeliveryPartner.UserFullName,
                savedOrder.Status,
                savedOrder.OrderDate,
                Items = savedOrder.OrderItems.Select(oi => new
                {
                    oi.Menu.MenuName,
                    oi.Quantity,
                    oi.ItemTotal
                })
            };

            return Ok(new { message = "Order placed successfully.", order = result });
        }

        [HttpGet("getById/{id}")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)}, {nameof(Role.DELIVERY_PARTNER)}, {nameof(Role.CUSTOMER)}")]
        public IActionResult GetById(int id)
        {
            var o = _service.GetById(id);
            if (o == null)
                return NotFound(new { message = $"Order with ID {id} not found." });

            var order = new
            {
                o.OrderId,
                User = o.User.UserFullName,
                UserAddress = o.User.UserEmail,
                Restaurant = o.Restaurant.RestaurantName,
                o.PaymentType,
                o.TotalAmount,
                o.DiscountApplied,
                DeliveryPartner = o.DeliveryPartner.UserFullName,
                o.Status,
                o.OrderDate,
                Items = o.OrderItems.Select(oi => new
                {
                    oi.Menu.MenuName,
                    oi.Quantity,
                    oi.ItemTotal
                })
            };

            return Ok(new { message = $"Order {id} retrieved successfully.", order });
        }

        [HttpPut("cancel/{orderId}")]
        [Authorize(Roles = $"{nameof(Role.CUSTOMER)}")]
        public IActionResult CancelOrder(int orderId)
        {
            var order = _service.CancelOrder(orderId);

            if (order == null)
                return NotFound(new { message = $"Order {orderId} not found." });

            if (order.Status != OrderStatus.Cancelled)
                return BadRequest(new { message = $"Order cannot be cancelled. Current status: {order.Status}" });

            return Ok(new { message = $"Order {orderId} cancelled successfully.", orderId });
        }

    }
}
