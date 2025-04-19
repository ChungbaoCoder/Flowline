namespace ps40165_Main.Dtos;

public class ProductDto
{
    public int ProductId { get; set; }

    public int CategoryId { get; set; }

    public string? SKU { get; set; }

    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;

    public string UnderDescription { get; set; } = string.Empty;

    public int StockLevel { get; set; }

    public decimal Price { get; set; }

    public bool DisableBuyButton { get; set; }

    public List<ProductImageDto> ProductImages { get; set; } = new List<ProductImageDto>();
}

public class ProductImageDto
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string? ImagePath { get; set; }

    public bool MainImage { get; set; }

    public int? Height { get; set; }

    public int? Width { get; set; }
}
