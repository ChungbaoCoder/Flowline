using Microsoft.AspNetCore.Mvc;
using ps40165_Main.Dtos.PostDto;
using ps40165_Main.Dtos.PutDto;
using ps40165_Main.Shared.Interfaces;

namespace ps40165_Main.Features.CategoryFeature;

public static class CategoryModule
{
    public static void AddCategory(this IServiceCollection services)
    {
        services.AddScoped<ICategory, CategoryService>();
        services.AddScoped<CategoryCommand>();
    }

    public static void ApiCategory(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/categories");

        group.MapGet("/", async (CategoryCommand cmd) => await cmd.GetList());

        group.MapGet("/{id}", async (CategoryCommand cmd, int id) => await cmd.GetById(id));

        group.MapPost("/", async (CategoryCommand cmd, [FromBody] CreateCategoryDto category) => await cmd.Create(category));

        group.MapPut("/{id}", async (CategoryCommand cmd, int id, [FromBody] EditCategoryDto category) => await cmd.Update(id, category));

        group.MapDelete("/{id}", async (CategoryCommand cmd, int id) => await cmd.Delete(id));

        group.MapGet("/paginate", async (CategoryCommand cmd, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5, [FromQuery] string? searchText = null) => await cmd.Paginate(pageNumber, pageSize, searchText));
    }
}
