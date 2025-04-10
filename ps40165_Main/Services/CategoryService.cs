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

    public async Task<IDbResponse> GetListCategories(QueryPageCommand request)
    {
        int pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
        int pageSize = request.PageSize < 1 ? 10 : request.PageSize;

        var categories = await _context.Categories
            .AsNoTracking()
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
            return DbResponse.Failure(new CategoryError().NotFound());
        }

        int totalCount = await _context.Categories.CountAsync();
        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        PaginationMetadata meta = new PaginationMetadata
        {
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };

        List<CategoryDto> data = new List<CategoryDto>();

        foreach (Category category in categories)
            data.Add(new CategoryMapper().Map(category));

        return DbPagination<CategoryDto>.GiveBack(meta, data);
    }

    public async Task<IDbResponse> GetCategoryById(int categoryId)
    {
        var found = await _context.Categories.FindAsync(categoryId);

        if (found == null)
            return DbResponse.Failure(new CategoryError().NotFound());

        CategoryDto category = new CategoryDto { Name = found.Name, Description = found.Description };

        return DbQuery<CategoryDto>.GiveBack(category);
    }

    public async Task<IDbResponse> AddCategory(AddCategoryCommand request)
    {
        Category category = new Category { Name = request.Name, Alias = request.Alias, Description = request.Description };

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        return DbResponse.Success;
    }

    public async Task<IDbResponse> EditCategory(int categoryId, EditCategoryCommand request)
    {
        var found = await _context.Categories.FindAsync(categoryId);

        if (found == null)
            return DbResponse.Failure(new CategoryError().NotFound());

        found.Name = request.Name;
        found.Alias = request.Alias;
        found.Description = request.Description;

        await _context.SaveChangesAsync();

        return DbResponse.Success;
    }

    public async Task<IDbResponse> DeleteCategory(int categoryId)
    {
        var found = await _context.Categories.FindAsync(categoryId);

        if (found == null)
            return DbResponse.Failure(new CategoryError().NotFound());

        _context.Categories.Remove(found);

        await _context.SaveChangesAsync();

        return DbResponse.Success;
    }
}
