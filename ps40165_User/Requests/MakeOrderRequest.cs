using ps40165_User.Models;

namespace ps40165_User.Requests;

public class MakeOrderRequest
{
    public int AccountId { get; set; }

    public DateTime OrderDate { get; set; }

    public OrderStatus Status { get; set; }

    public decimal Total { get; set; }

    public List<OrderedItem> Items { get; set; } = new List<OrderedItem>();
}

public class OrderedItem
{
    public int ProductId { get; set; }

    public string? SKU { get; set; }

    public string? ImagePath { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}
