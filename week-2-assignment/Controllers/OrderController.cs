using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace week_2_assignment.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // POST /api/orders → Create a new order
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            if (dto == null || dto.CustomerId <= 0 || dto.Items == null || !dto.Items.Any())
            {
                await HttpContext.LogActionAsync("Failed to create order: Invalid order data.");
                return BadRequest(new { Message = "Invalid order data. Please ensure that CustomerId and Items are provided." });
            }

            await _orderService.CreateOrderAsync(dto);
            await HttpContext.LogActionAsync($"Order created successfully for CustomerId: {dto.CustomerId}");

            return Ok(new { Message = "Order has been successfully created." });
        }

        // GET /api/orders/{orderId} → Get the details of a single order
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int orderId)
        {
            if (orderId <= 0)
            {
                await HttpContext.LogActionAsync($"Failed to retrieve order: Invalid order ID {orderId}.");
                return BadRequest(new { Message = "Invalid order ID." });
            }

            var orderDto = await _orderService.GetOrderByIdAsync(orderId);

            if (orderDto == null)
            {
                await HttpContext.LogActionAsync($"Order not found: {orderId}");
                return NotFound(new { Message = "Order not found." });
            }

            await HttpContext.LogActionAsync($"Retrieved order details for OrderId: {orderId}");
            return Ok(orderDto);
        }

        // GET /api/orders/customer/{customerId} → Get all orders for a customer
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<OrderSummaryDto>>> GetOrdersByCustomerId(int customerId)
        {
            if (customerId <= 0)
            {
                await HttpContext.LogActionAsync($"Failed to retrieve orders: Invalid customer ID {customerId}.");
                return BadRequest(new { Message = "Invalid customer ID." });
            }

            var orders = await _orderService.GetAllOrdersAsync(customerId);

            if (orders == null || !orders.Any())
            {
                await HttpContext.LogActionAsync($"No orders found for CustomerId: {customerId}");
                return NotFound(new { Message = "No orders found for this customer." });
            }

            await HttpContext.LogActionAsync($"Retrieved orders for CustomerId: {customerId}");
            return Ok(orders);
        }

        // DELETE /api/orders/{id} → Delete an order
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (id <= 0)
            {
                await HttpContext.LogActionAsync($"Failed to delete order: Invalid order ID {id}.");
                return BadRequest(new { Message = "Invalid order ID." });
            }

            await _orderService.DeleteOrderAsync(id);
            await HttpContext.LogActionAsync($"Order deleted successfully for OrderId: {id}");

            return Ok(new { Message = "Order has been successfully deleted." });
        }
        // PUT /api/orders/{id} → Update an order
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderDto dto)
        {
            if (dto == null)
            {
                await HttpContext.LogActionAsync($"Failed to update order: Invalid order data for OrderId: {id}.");
                return BadRequest(new { Message = "Invalid order data." });
            }

            var success = await _orderService.UpdateOrderAsync(id, dto);
            if (!success)
            {
                await HttpContext.LogActionAsync($"Order not found for update: {id}");
                return NotFound(new { Message = "Order not found." });
            }

            var updatedOrder = await _orderService.GetOrderByIdAsync(id);
            if (updatedOrder == null)
            {
                await HttpContext.LogActionAsync($"Order not found after update: {id}");
                return NotFound(new { Message = "Order not found after update." });
            }

            await HttpContext.LogActionAsync($"Order updated successfully for OrderId: {id}, TotalAmount: {updatedOrder.TotalAmount}");
            return Ok(new { Message = "Order updated successfully.", TotalAmount = updatedOrder.TotalAmount });
        }
    }
}
