namespace ps40165_User.Models;

public class Product
{
    public int ProductId { get; set; }

    public string? SKU { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string UnderDescription { get; set; } = string.Empty;

    public int StockLevel { get; set; }

    public decimal Price { get; set; }

    public bool DisableBuyButton { get; set; }

    public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public string GetMainImage => ProductImages.FirstOrDefault(pi => pi.MainImage).ImagePath;
}
