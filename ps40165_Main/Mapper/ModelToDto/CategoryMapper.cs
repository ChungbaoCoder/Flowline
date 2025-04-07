using ps40165_Main.Dtos;
using ps40165_Main.Models;

namespace ps40165_Main.Mapper.ModelToDto;

public class CategoryMapper : IMapper<Category, CategoryDto>
{
    public CategoryDto Map(Category src)
    {
        return new CategoryDto 
        { 
            CategoryId = src.Id,
            Name = src.Name,
            Alias = src.Alias ?? "",
            Description = src.Description
        };
    }
}
