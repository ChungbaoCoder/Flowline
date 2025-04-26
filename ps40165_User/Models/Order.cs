using ps40165_User.Shared.Enums;

namespace ps40165_User.Models;

public class Order
{
    public int CustomerId { get; set; }

    public string Address1 { get; set; } = string.Empty;

    public string Address2 { get; set; } = string.Empty;

    public List<OrderItem> Items { get; set; } = new();

    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

    public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
}
