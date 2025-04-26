using ps40165_Admin.Shared;
using ps40165_Admin.Shared.Enums;

namespace ps40165_Admin.Models;

public class Order : BaseEntity
{
    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public string Address1 { get; set; } = string.Empty;

    public string Address2 { get; set; } = string.Empty;

    public List<OrderItem> Items { get; set; } = new();

    public OrderStatus OrderStatus { get; set; }

    public PaymentStatus PaymentStatus { get; set; }

    public decimal Total { get; set; }
}
