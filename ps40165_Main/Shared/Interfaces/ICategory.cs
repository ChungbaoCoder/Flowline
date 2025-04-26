using ps40165_Main.Dtos.GetDto;
using ps40165_Main.Dtos.PostDto;
using ps40165_Main.Dtos.PutDto;
using ps40165_Main.Models;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Shared.Interfaces;

public interface ICategory
{
    Task<Result<List<Category>>> GetListCategories();

    Task<Result<Category>> GetCategoryById(int categoryId);

    Task<Result<Category>> CreateCategory(CreateCategoryDto category);

    Task<Result<Category>> UpdateCategory(int categoryId, EditCategoryDto category);

    Task<Result<Category>> DeleteCategory(int categoryId);

    Task<Result<PaginatedList<Category>>> GetPagination(int pageNumber, int pageSize, string? searchText);
}
