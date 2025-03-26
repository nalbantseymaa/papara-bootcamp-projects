using System.Collections.Generic;
using System.Threading.Tasks;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(CreateOrderDto dto);
    Task<OrderDto> GetOrderByIdAsync(int orderId);
    Task<List<OrderSummaryDto>> GetAllOrdersAsync(int? customerId);
    Task<bool> UpdateOrderAsync(int orderId, UpdateOrderDto dto);
    Task DeleteOrderAsync(int orderId);
}

