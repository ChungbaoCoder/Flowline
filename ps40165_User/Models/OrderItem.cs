namespace ps40165_User.Models;

public class OrderItem
{
    public int ProductId { get; set; }

    public string? SKU { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? ImagePath { get; set; }

    public int Amount { get; set; }

    public decimal Price { get; set; }

    public decimal Total => Amount * Price;
}
