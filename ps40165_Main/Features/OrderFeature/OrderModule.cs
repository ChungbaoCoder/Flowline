using Microsoft.AspNetCore.Mvc;
using ps40165_Main.Models;
using ps40165_Main.Shared.Interfaces;

namespace ps40165_Main.Features.OrderFeature;

public static class OrderModule
{
    public static void AddOrder(this IServiceCollection services)
    {
        services.AddScoped<IOrder, OrderService>();
        services.AddScoped<OrderCommand>();
    }

    public static void ApiOrder(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/orders");

        group.MapGet("/", async (OrderCommand cmd) => await cmd.GetList());

        group.MapGet("/{orderId}", async (OrderCommand cmd, int id) => await cmd.GetById(id));

        group.MapGet("order/{customerId}", async (OrderCommand cmd, int id) => await cmd.GetByCustomerId(id));

        group.MapPost("/", async (OrderCommand cmd, [FromBody] Order order, List<OrderItem> items) => await cmd.Create(order, items));

        //group.MapPut("/{id}", async (OrderCommand cmd, int id, [FromBody] Order order) => await cmd.Update(id, order));

        //group.MapDelete("/{id}", async (OrderCommand cmd, int id) => await cmd.Delete(id));
    }
}
