using ps40165_Main.Dtos;
using ps40165_Main.Models;

namespace ps40165_Main.Mapper.ModelToDto;

public class ProductImageMapper : IMapper<ICollection<ProductImage>, List<ProductImageDto>>
{
    public List<ProductImageDto> Map(ICollection<ProductImage> src)
    {
        List<ProductImageDto> images = new List<ProductImageDto>();

        foreach (ProductImage img in src)
        {
            images.Add(new ProductImageDto
            {
                Id = img.Id,
                ProductId = img.ProductId,
                ImagePath = img.ImagePath,
                MainImage = img.MainImage,
                Width = img.Width,
                Height = img.Height
            });
        }
        return images;
    }
}
