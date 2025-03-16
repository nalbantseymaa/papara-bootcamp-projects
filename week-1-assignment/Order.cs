using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Order
{
    [Required(ErrorMessage = "OrderId is required.")]
    public int OrderId { get; set; }

    [Required(ErrorMessage = "UserId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "UserId must be greater than 0.")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Items cannot be null.")]
    [MinLength(1, ErrorMessage = "At least one item is required in the order.")]
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();

    public decimal TotalAmount
    {
        get
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.Price * item.Quantity;
            }
            return total;
        }
    }
    public string Status { get; set; } = "Pending";

    [Required(ErrorMessage = "OrderDate is required.")]
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedDate { get; set; }
}
