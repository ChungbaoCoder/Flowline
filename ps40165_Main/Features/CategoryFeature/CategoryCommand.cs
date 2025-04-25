using ps40165_Main.Models;
using ps40165_Main.Shared;
using ps40165_Main.Shared.Interfaces;

namespace ps40165_Main.Features.CategoryFeature;

public class CategoryCommand
{
    private readonly ICategory _svc;

    public CategoryCommand(ICategory svc) => _svc = svc;

    public async Task<CentralResponse<List<Category>>> GetList()
    {
        var result = await _svc.GetListCategories();

        if (result.IsSuccess)
        {
            return new CentralResponse<List<Category>>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<List<Category>>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }

    public async Task<CentralResponse<Category>> GetById(int categoryId)
    {
        var result = await _svc.GetCategoryById(categoryId);

        if (result.IsSuccess)
        {
            return new CentralResponse<Category>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<Category>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }

    public async Task<CentralResponse<Category>> Create(Category category)
    {
        var result = await _svc.CreateCategory(category);

        if (result.IsSuccess)
        {
            return new CentralResponse<Category>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<Category>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }

    public async Task<CentralResponse<Category>> Update(int categoryId, Category category)
    {
        var result = await _svc.UpdateCategory(categoryId, category);

        if (result.IsSuccess)
        {
            return new CentralResponse<Category>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<Category>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }

    public async Task<CentralResponse<Category>> Delete(int categoryId)
    {
        var result = await _svc.DeleteCategory(categoryId);

        if (result.IsSuccess)
        {
            return new CentralResponse<Category>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<Category>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }
}
