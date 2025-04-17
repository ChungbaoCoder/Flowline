namespace ps40165_User.Models;

public class Order
{
    public int Id { get; set; }

    public Customer Customer { get; set; } = new Customer();

    public DateTime OrderDate { get; set; }

    public OrderStatus Status { get; set; }

    public bool Completed { get; set; }

    public decimal Total => Items.Sum(oi => oi.Total);

    public List<OrderItem> Items { get; set; } = new List<OrderItem>();

    public DateTime CreatedOnUtc { get; set; }

    public DateTime UpdatedOnUtc { get; set; }
}

public enum OrderStatus
{
    Pending = 0,
    Processing = 1,
    Complete = 2,
    Cancelled = 3
}
