using ps40165_Main.Dtos;
using ps40165_Main.Models;

namespace ps40165_Main.Mapper.ModelToDto;

public class ProductMapper : IMapper<Product, ProductDto>
{
    public ProductDto Map(Product src)
    {
        return new ProductDto
        {
            ProductId = src.Id,
            CategoryId = src.CategoryId,
            SKU = src.SKU,
            Name = src.Name,
            Description = src.Description,
            UnderDescription = src.UnderDescription,
            StockLevel = src.StockLevel,
            Price = src.Price,
            DisableBuyButton = src.DisableBuyButton,
            ProductImages = new ProductImageMapper().Map(src.ProductImages).ToList()
        };
    }
}
