using ps40165_Main.Models;

namespace ps40165_Main.Dtos;

public class OrderDto
{
    public int OrderId { get; set; }

    public int AccountId { get; set; }

    public DateTime OrderDate { get; set; }

    public OrderStatus Status { get; set; }

    public bool Completed { get; set; }

    public decimal Total => Items.Sum(oi => oi.Total);

    public List<OrderItemDto> Items { get; set; }
}
