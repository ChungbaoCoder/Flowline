namespace ps40165_Main.Dtos;

public class OrderItemDto
{
    public int ProductId { get; set; }

    public string? SKU { get; set; }

    public string? ImagePath { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Total => Quantity * Price;
}
