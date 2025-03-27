using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Customer
{
    public int Id { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number format.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(200, MinimumLength = 10, ErrorMessage = "Address must be between 10 and 200 characters.")]
    public string Address { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Customer number must be greater than zero.")]
    public int CustomerNumber { get; set; }

    public List<Order> Orders { get; set; } = new List<Order>();
}
