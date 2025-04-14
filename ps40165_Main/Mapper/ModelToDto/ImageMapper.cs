using ps40165_Main.Dtos;
using ps40165_Main.Models;

namespace ps40165_Main.Mapper.ModelToDto;

public class ImageMapper : IMapper<ProductImage, ProductImageDto>
{
    public ProductImageDto Map(ProductImage src)
    {
        return new ProductImageDto
        {
            MainImage = src.MainImage,
            Height = src.Height,
            Width = src.Width,
            ImagePath = src.ImagePath
        };
    }
}

public class ListImageMapper : IMapper<ICollection<ProductImage>, List<ProductImageDto>>
{
    public List<ProductImageDto> Map(ICollection<ProductImage> src)
    {
        List<ProductImageDto> images = new List<ProductImageDto>();
        foreach (ProductImage image in src)
        {
            images.Add(new ProductImageDto
            {
                MainImage = image.MainImage,
                Height = image.Height,
                Width = image.Width,
                ImagePath = image.ImagePath
            });
        }
        return images;
    }
}
