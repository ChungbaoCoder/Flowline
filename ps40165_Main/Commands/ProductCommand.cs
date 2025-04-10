namespace ps40165_Main.Commands;

public class AddProductCommand
{
    public int CategoryId { get; set; }
    
    public string? SKU { get; set; }

    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;

    public string UnderDescription { get; set; } = string.Empty;

    public int StockLevel { get; set; }

    public decimal Price { get; set; }

    public bool DisableBuyButton { get; set; }
}

public class EditProductCommand
{
    public int CategoryId { get; set; }
    
    public string? SKU { get; set; }

    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;

    public string UnderDescription { get; set; } = string.Empty;

    public int StockLevel { get; set; }

    public decimal Price { get; set; }

    public bool DisableBuyButton { get; set; }
}
