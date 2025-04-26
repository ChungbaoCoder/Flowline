using ps40165_Main.Shared.Enums;

namespace ps40165_Main.Dtos.PostDto;

public class CreateOrderDto
{
    public int CustomerId { get; set; }

    public string Address1 { get; set; } = string.Empty;

    public string Address2 { get; set; } = string.Empty;

    public List<CreateOrderItemDto> Items { get; set; } = new();

    public OrderStatus OrderStatus { get; set; }

    public PaymentStatus PaymentStatus { get; set; }
}
