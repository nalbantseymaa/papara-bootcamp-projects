using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class FakeOrderService : IOrderService
{
    private readonly List<Order> _orders; // List of orders
    private int _nextId; // Order ID
    private int _nextItemId; // Order item ID

    public FakeOrderService(List<Order> orders)
    {
        _orders = orders;

        // If there are orders, get the maximum OrderId
        _nextId = _orders.Any() ? _orders.Max(o => o.OrderId) + 1 : 1;

        // If there are order items, get the maximum OrderItemId
        _nextItemId = _orders.SelectMany(o => o.Items).Any() ? _orders.SelectMany(o => o.Items).Max(i => i.OrderItemId) + 1 : 1;
    }

    public async Task<Order> CreateOrderAsync(CreateOrderDto dto)
    {
        var order = new Order
        {
            OrderId = _nextId++, // Increment ID for each new order
            CustomerId = dto.CustomerId,
            Items = dto.Items.Select(item => new OrderItem
            {
                OrderId = _nextId, // Assigning the OrderId of the order
                OrderItemId = _nextItemId++, // Giving a unique ID for each OrderItem
                ProductName = item.ProductName,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList(),
            Status = "Pending", // Initial status of the order
            OrderDate = DateTime.UtcNow // Set the order date to now
        };

        _orders.Add(order); // Add the new order to the list
        return await Task.FromResult(order); // Return the created order
    }

    public async Task<OrderDto> GetOrderByIdAsync(int orderId)
    {
        var order = _orders.FirstOrDefault(o => o.OrderId == orderId);

        if (order == null)
        {
            return null; // Return null if the order is not found
        }

        var orderDto = new OrderDto
        {
            Status = order.Status,
            OrderDate = order.OrderDate,
            Items = order.Items.Select(item => new OrderItemDto
            {
                ProductName = item.ProductName,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList()
        };
        return await Task.FromResult(orderDto); // Return the order DTO
    }

    public async Task<List<OrderSummaryDto>> GetAllOrdersAsync(int? customerId)
    {
        var filteredOrders = _orders
            .Where(o => o.CustomerId == customerId)
            .Select(o => new OrderSummaryDto
            {
                Id = o.OrderId,
                Status = o.Status,
                OrderDate = o.OrderDate
            })
            .ToList();

        return await Task.FromResult(filteredOrders); // Return the list of filtered orders
    }

    public async Task DeleteOrderAsync(int orderId)
    {
        var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order != null)
        {
            _orders.Remove(order); // Remove the order if it exists
        }
        await Task.CompletedTask; // Complete the task
    }

    public async Task<bool> UpdateOrderAsync(int orderId, UpdateOrderDto dto)
    {
        var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order == null)
        {
            return false; // Return false if the order is not found
        }

        foreach (var item in dto.Items)
        {
            var orderItem = order.Items.FirstOrDefault(i => i.OrderItemId == item.OrderItemId);
            if (orderItem != null)
            {
                orderItem.Quantity = item.Quantity; // Update the quantity of the order item
            }

            order.Status = "Shipped"; // Update the order status to "Shipped"
        }
        return true; // Return true if the update is successful
    }
}
