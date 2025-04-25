using ps40165_Main.Models;

namespace ps40165_Main.Dtos;

public class ProductDto
{
    public int ProductId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public Category Category { get; set; } = new Category();
}

public class MapToDto
{
    public static ProductDto Map(Product product)
    {
        return new ProductDto
        {
            ProductId = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Category = product.Category
        };
    }
}
