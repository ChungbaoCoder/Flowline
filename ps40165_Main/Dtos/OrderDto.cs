using ps40165_Main.Models;

namespace ps40165_Main.Dtos;

public class OrderDto
{
    public int OrderId { get; set; }

    public CustomerDto Customer { get; set; } = new CustomerDto();

    public DateTime OrderDate { get; set; }

    public OrderStatus Status { get; set; }

    public bool Completed { get; set; }

    public decimal Total => Items.Sum(oi => oi.Total);

    public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();

    public DateTime CreatedOnUtc { get; set; }

    public DateTime UpdatedOnUtc { get; set; }
}
