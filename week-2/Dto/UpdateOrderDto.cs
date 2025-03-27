using System.Collections.Generic;

public class UpdateOrderDto
{
    public List<OrderItemUpdateDto> Items { get; set; }

    public UpdateOrderDto()
    {
        Items = new List<OrderItemUpdateDto>();
    }
}


public class OrderItemUpdateDto
{
    public int OrderItemId { get; set; }
    public int Quantity { get; set; }
}

