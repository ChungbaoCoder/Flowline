using Microsoft.AspNetCore.Mvc;
using ps40165_Main.Dtos.PostDto;
using ps40165_Main.Dtos.PutDto;
using ps40165_Main.Shared.Interfaces;

namespace ps40165_Main.Features.CustomerFeature;

public static class CustomerModule
{
    public static void AddCustomer(this IServiceCollection services)
    {
        services.AddScoped<ICustomer, CustomerService>();
        services.AddScoped<CustomerCommand>();
    }

    public static void ApiCustomer(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/customers");

        group.MapGet("/", async (CustomerCommand cmd) => await cmd.GetList());

        group.MapGet("/{id}", async (CustomerCommand cmd, int id) => await cmd.GetById(id));

        group.MapPost("/", async (CustomerCommand cmd, [FromBody] CreateCustomerDto product) => await cmd.Create(product));

        group.MapPut("/{id}", async (CustomerCommand cmd, int id, [FromBody] EditCustomerDto product) => await cmd.Update(id, product));

        group.MapDelete("/{id}", async (CustomerCommand cmd, int id) => await cmd.Delete(id));

        group.MapGet("/paginate", async (CustomerCommand cmd, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5, [FromQuery] string? searchText = null) => await cmd.Paginate(pageNumber, pageSize, searchText));
    }
}
