using System.ComponentModel.DataAnnotations;

public class OrderItem
{
    public int OrderItemId { get; set; }

    [Required(ErrorMessage = "Order ID is required.")]
    public int OrderId { get; set; }

    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Product name must be between 2 and 100 characters.")]
    public string ProductName { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }

    public Order Order { get; set; }
}
