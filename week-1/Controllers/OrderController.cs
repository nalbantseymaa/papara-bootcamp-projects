using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace week_1_assignment.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class OrderController : ControllerBase
    {
        private static List<Order> Orders = new List<Order>()
        {
            new Order()
            {
                OrderId = 1,
                UserId = 101,
                Items = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        OrderItemId = 1,
                        ProductId = 201,
                        Quantity = 2,
                        Price = 15.5m,
                    },
                    new OrderItem()
                    {
                        OrderItemId = 2,
                        ProductId = 202,
                        Quantity = 1,
                        Price = 50.0m
                    }
                },
                Status = "Pending",
                OrderDate = DateTime.Now
            },
            new Order()
            {
                OrderId = 2,
                UserId = 101,
                Items = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        OrderItemId = 3,
                        ProductId = 203,
                        Quantity = 1,
                        Price = 20.0m,
                    },
                    new OrderItem()
                    {
                        OrderItemId = 4,
                        ProductId = 204,
                        Quantity = 3,
                        Price = 10.0m
                    }
                },
                Status = "Shipped",
                OrderDate = DateTime.Now.AddDays(-1)
            },
            new Order()
            {
                OrderId = 3,
                UserId = 102,
                Items = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        OrderItemId = 5,
                        ProductId = 205,
                        Quantity = 5,
                        Price = 5.0m,
                    }
                },
                Status = "Completed",
                OrderDate = DateTime.Now.AddDays(-2)
            },
            new Order()
            {
                OrderId = 4,
                UserId = 103,
                Items = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        OrderItemId = 6,
                        ProductId = 206,
                        Quantity = 2,
                        Price = 30.0m
                    }
                },
                Status = "Pending",
                OrderDate = DateTime.Now.AddDays(-3)
            },
            new Order()
            {
                OrderId = 5,
                UserId = 101,
                Items = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        OrderItemId = 7,
                        ProductId = 207,
                        Quantity = 4,
                        Price = 12.5m,
                    }
                },
                Status = "Canceled",
                OrderDate = DateTime.Now.AddDays(-4)
            }
        };

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                if (Orders == null || !Orders.Any())
                {
                    return NotFound(new { Message = "Orders not found." });
                }

                return Ok(Orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var order = Orders.FirstOrDefault(o => o.OrderId == id);

                if (order == null)
                {
                    return NotFound(new { Message = "Order not found." });
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest(new { Message = "Order is null." });
                }

                Orders.Add(order);

                return CreatedAtAction(nameof(Get), new { id = order.OrderId }, order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Order order)
        {
            try
            {
                var existingOrder = Orders.FirstOrDefault(o => o.OrderId == id);

                if (existingOrder == null)
                {
                    return NotFound(new { Message = "Order not found." });
                }

                // If the order status is "Shipped", no updates allowed
                if (existingOrder.Status == "Shipped")
                {
                    return BadRequest(new { Message = "The order has been shipped; updates cannot be made." });
                }

                // Allow updates only if the status is "Pending"
                if (existingOrder.Status == "Pending")
                {
                    existingOrder.Items = order.Items; // Update the items
                    existingOrder.Status = order.Status; // Update the status

                    return Ok(existingOrder);
                }
                else
                {
                    return BadRequest(new { Message = "No further updates can be made in this status." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the order: " + ex.Message });
            }
        }

        [HttpPatch("{orderId}/items")]
        public IActionResult UpdateOrderItems(int orderId, [FromBody] List<OrderItemDto> orderItemsDto)
        {

            var order = Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                return NotFound(new { Message = "Order not found." });
            }

            foreach (var item in orderItemsDto)
            {
                try
                {
                    var existingItem = order.Items.FirstOrDefault(i => i.OrderItemId == item.OrderItemId);
                    if (existingItem != null)
                    {
                        existingItem.Quantity = item.Quantity;
                    }
                    else
                    {
                        return NotFound(new { Message = $"Order item with ID {item.OrderItemId} not found." });
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Message = "Error updating order item: " + ex.Message });
                }
            }
            return Ok(order);
        }

        public class OrderItemDto
        {
            public int OrderItemId { get; set; }
            public int Quantity { get; set; }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var order = Orders.FirstOrDefault(o => o.OrderId == id);

                if (order == null)
                {
                    return NotFound(new { Message = "Order not found." });
                }

                Orders.Remove(order);

                return Ok(new { Message = "Order deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("filtered")]
        public IActionResult GetOrders([FromQuery] string status = null, [FromQuery] string sortOrder = null)
        {
            try
            {
                var orders = Orders.AsQueryable();

                if (!string.IsNullOrEmpty(status))
                {
                    orders = orders.Where(o => o.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrEmpty(sortOrder))
                {
                    orders = sortOrder.ToLower() switch
                    {
                        "totalasc" => orders.OrderBy(o => o.TotalAmount),
                        "totaldesc" => orders.OrderByDescending(o => o.TotalAmount),
                        _ => orders
                    };
                }

                if (!orders.Any())
                {
                    return NotFound(new { Message = "No orders found matching the specified criteria." });
                }

                return Ok(orders.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving orders: " + ex.Message });
            }
        }

    }
}