using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public class Order
{
    public int OrderId { get; set; }

    [Required(ErrorMessage = "Customer ID is required.")]
    public int CustomerId { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Total amount cannot be negative.")]
    public decimal TotalAmount => Items?.Sum(item => item.Price * item.Quantity) ?? 0;

    [Required(ErrorMessage = "Order status is required.")]
    [RegularExpression("Pending|Processing|Shipped|Completed|Cancelled", ErrorMessage = "Invalid order status.")]
    public string Status { get; set; } = "Pending";

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedDate { get; set; }

    [MinLength(1, ErrorMessage = "An order must contain at least one item.")]
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}
