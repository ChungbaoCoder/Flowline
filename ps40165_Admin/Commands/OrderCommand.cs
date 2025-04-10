using ps40165_Admin.Models;

namespace ps40165_Admin.Commands;

public class OrderedItemCommand
{
    public int ProductId { get; set; }

    public string? SKU { get; set; }

    public string? ImagePath { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}

public class MakeOrderCommand
{
    public int AccountId { get; set; }

    public DateTime OrderDate { get; set; }

    public OrderStatus Status { get; set; }

    public decimal Total { get; set; }

    public List<OrderedItemCommand> Items { get; set; } = new List<OrderedItemCommand>();
}