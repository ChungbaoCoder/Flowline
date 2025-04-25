using Microsoft.AspNetCore.Mvc;
using ps40165_Main.Models;
using ps40165_Main.Shared.Interfaces;

namespace ps40165_Main.Features.ProductFeature;

public static class ProductModule
{
    public static void AddProduct(this IServiceCollection services)
    {
        services.AddScoped<IProduct, ProductService>();
        services.AddScoped<ProductCommand>();
    }

    public static void ApiProduct(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/products");

        group.MapGet("/", async (ProductCommand cmd) => await cmd.GetList());

        group.MapGet("/{id}", async (ProductCommand cmd, int id) => await cmd.GetById(id));

        group.MapPost("/", async (ProductCommand cmd, [FromBody] Product product) => await cmd.Create(product));

        group.MapPut("/{id}", async (ProductCommand cmd, int id, [FromBody] Product product) => await cmd.Update(id, product));

        group.MapDelete("/{id}", async (ProductCommand cmd, int id) => await cmd.Delete(id));

        group.MapGet("/{id}/detail", async (ProductCommand cmd, int id) => await cmd.GetDetail(id));
    }
}
