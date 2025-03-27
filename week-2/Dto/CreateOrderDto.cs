using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class CreateOrderDto
{
    [Required(ErrorMessage = "Customer ID is required.")]
    public int CustomerId { get; set; }


    [Required(ErrorMessage = "At least one item is required.")]
    public List<OrderItemDto> Items { get; set; }
}