namespace ps40165_Main.Dtos;

public class ProductDto
{
    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;

    public string UnderDescription { get; set; } = string.Empty;

    public int StockLevel { get; set; }

    public decimal Price { get; set; }

    public bool Active { get; set; }

    public bool DisableBuyButton { get; set; }
}
