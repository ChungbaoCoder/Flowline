using ps40165_Main.Models;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Shared.Interfaces;

public interface ICategory
{
    public Task<Result<List<Category>>> GetListCategories();

    public Task<Result<Category>> GetCategoryById(int categoryId);

    public Task<Result<Category>> CreateCategory(Category category);

    public Task<Result<Category>> UpdateCategory(int categoryId, Category category);

    public Task<Result<Category>> DeleteCategory(int categoryId);
}
