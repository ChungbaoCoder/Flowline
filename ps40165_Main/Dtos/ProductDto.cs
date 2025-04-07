namespace ps40165_Main.Dtos;

public class ProductDto
{
    public int ProductId { get; set; }

    public string? SKU { get; set; }

    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;

    public string UnderDescription { get; set; } = string.Empty;

    public int StockLevel { get; set; }

    public decimal Price { get; set; }

    public bool DisableBuyButton { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public List<string> ProductImages { get; set; } = new List<string>();
}
