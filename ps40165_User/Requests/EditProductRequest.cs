namespace ps40165_User.Requests;

public class EditProductRequest
{
    public int CategoryId { get; set; }

    public string? SKU { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string UnderDescription { get; set; } = string.Empty;

    public int StockLevel { get; set; }

    public decimal Price { get; set; }

    public bool DisableBuyButton { get; set; }
}

