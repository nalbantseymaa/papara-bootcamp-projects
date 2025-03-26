using System;
using System.Collections.Generic;
using System.Linq;

public class OrderDto
{
    public string Status { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemDto> Items { get; set; }

    public decimal TotalAmount
    {
        get
        {
            return Items.Sum(item => item.Price * item.Quantity);
        }
    }

}

