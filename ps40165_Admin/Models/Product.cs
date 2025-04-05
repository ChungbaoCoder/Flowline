namespace ps40165_Admin.Models;

public class Product
{
    public int ProductId { get; set; }

    public int CategoryId { get; set; }

    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;

    public string UnderDescription { get; set; } = string.Empty;

    public int StockLevel { get; set; }

    public decimal Price { get; set; }
}
