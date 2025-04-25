using Microsoft.EntityFrameworkCore;
using ps40165_Main.Database;
using ps40165_Main.Models;
using ps40165_Main.Shared.Interfaces;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Features.CategoryFeature;

public class CategoryService : ICategory
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context) => _context = context;

    public async Task<Result<List<Category>>> GetListCategories()
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .ToListAsync();

        if (categories.Count < 1)
            return Result<List<Category>>.Fail("Không có loại sản phẩm trong danh sách");

        return Result<List<Category>>.Ok(categories, "Lấy danh sách loại sản phẩm thành công");
    }

    public async Task<Result<Category>> GetCategoryById(int categoryId)
    {
        var cate = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == categoryId);

        if (cate is null)
            return Result<Category>.Fail($"Không tìm thấy loại sản phẩm có mã id {categoryId}");

        return Result<Category>.Ok(cate, $"Tìm thấy loại sản phẩm có mã id {categoryId}");
    }

    public async Task<Result<Category>> CreateCategory(Category category)
    {
        var cate = new Category();
        Result<Category> result = cate.UpdateName(category.Name).Bind(r => cate.UpdateAlias(category.Alias));

        if (result.IsSuccess)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            result = Result<Category>.Ok("Thêm loại sản phẩm thành công");
        }

        return result;
    }

    public async Task<Result<Category>> UpdateCategory(int categoryId, Category category)
    {
        var cate = await _context.Categories.FindAsync(categoryId);

        if (cate is null)
            return Result<Category>.Fail($"Không tìm thấy loại sản phẩm có mã id {categoryId}");

        Result<Category> result = cate.UpdateName(category.Name).Bind(r => cate.UpdateAlias(category.Alias));

        if (result.IsSuccess)
        {
            await _context.SaveChangesAsync();
            result = Result<Category>.Ok($"Cập nhật loại sản phẩm có mã id {categoryId} thành công");
        }

        return result;
    }

    public async Task<Result<Category>> DeleteCategory(int categoryId)
    {
        var cate = await _context.Categories.FindAsync(categoryId);

        if (cate is null)
            return Result<Category>.Fail($"Không tìm thấy loại sản phẩm có mã id {categoryId}");

        _context.Categories.Remove(cate);
        await _context.SaveChangesAsync();
        return Result<Category>.Ok($"Xóa loại sản phẩm có mã id {categoryId} thành công");
    }
}
