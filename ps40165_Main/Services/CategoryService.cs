using Microsoft.EntityFrameworkCore;
using ps40165_Main.Commands;
using ps40165_Main.Database;
using ps40165_Main.Database.DbResponse;
using ps40165_Main.Database.DbResponse.ErrorDoc;
using ps40165_Main.Dtos;
using ps40165_Main.Mapper.ModelToDto;
using ps40165_Main.Models;

namespace ps40165_Main.Services;

public class CategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IDbResponse<PaginatedList<CategoryDto>>> GetListCategories(QueryPageCommand request)
    {
        int pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
        int pageSize = request.PageSize < 1 ? 10 : request.PageSize;

        var categories = await _context.Categories
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    Alias = c.Alias,
                    Description = c.Description
                })
            .ToListAsync();

        if (categories.Count < 1)
        {
            return DbResponse<PaginatedList<CategoryDto>>.Failure(new CategoryError().NotFound());
        }

        int totalCount = await _context.Categories.CountAsync();
        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        List<CategoryDto> data = new List<CategoryDto>();

        foreach (Category category in categories)
            data.Add(new CategoryMapper().Map(category));

        PaginatedList<CategoryDto> meta = new PaginatedList<CategoryDto>(data, pageNumber, pageSize, totalCount);

        return DbResponse<PaginatedList<CategoryDto>>.GiveBack(meta);
    }

    public async Task<IDbResponse<CategoryDto>> GetCategoryById(int categoryId)
    {
        var found = await _context.Categories.FindAsync(categoryId);

        if (found is null)
            return DbResponse<CategoryDto>.Failure(new CategoryError().NotFound());

        CategoryDto data = new CategoryMapper().Map(found);

        return DbResponse<CategoryDto>.GiveBack(data);
    }

    public async Task<IDbResponse<CategoryDto>> AddCategory(AddCategoryCommand request)
    {
        Category category = new Category { Name = request.Name, Alias = request.Alias, Description = request.Description };

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        CategoryDto data = new CategoryMapper().Map(category);

        return DbResponse<CategoryDto>.GiveBack(data);
    }

    public async Task<IDbResponse<CategoryDto>> EditCategory(int categoryId, EditCategoryCommand request)
    {
        var found = await _context.Categories.FindAsync(categoryId);

        if (found is null)
            return DbResponse<CategoryDto>.Failure(new CategoryError().NotFound());

        found.Name = request.Name;
        found.Alias = request.Alias;
        found.Description = request.Description;

        await _context.SaveChangesAsync();

        CategoryDto data = new CategoryMapper().Map(found);

        return DbResponse<CategoryDto>.GiveBack(data);
    }

    public async Task<IDbResponse<string>> DeleteCategory(int categoryId)
    {
        var found = await _context.Categories.FindAsync(categoryId);

        if (found == null)
            return DbResponse<string>.Failure(new CategoryError().NotFound());

        _context.Categories.Remove(found);

        await _context.SaveChangesAsync();

        return DbResponse<string>.GiveBack($"Loại sản phẩm với mã số {categoryId}  đã được xóa đi");
    }
}
