using System.ComponentModel.DataAnnotations;

public class OrderItemDto
{
    [Required(ErrorMessage = "Product Name is required.")]
    [StringLength(100, ErrorMessage = "Product Name cannot be longer than 100 characters.")]
    public string ProductName { get; set; }

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }
}
