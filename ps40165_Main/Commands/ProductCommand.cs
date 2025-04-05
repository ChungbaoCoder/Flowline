namespace ps40165_Main.Commands;

public class AddProductCommand
{
    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;

    public string UnderDescription { get; set; } = string.Empty;

    public int StockLevel { get; set; }

    public decimal Price { get; set; }
}

public class EditProductCommand
{
    public int ProductId { get; set; }

    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;

    public string UnderDescription { get; set; } = string.Empty;

    public int StockLevel { get; set; }

    public decimal Price { get; set; }
}
