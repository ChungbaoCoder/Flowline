namespace ps40165_Admin.Models;

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

public enum OrderStatus
{
    Pending = 0,
    Processing = 1,
    Complete = 2,
    Cancelled = 3
}
