using Microsoft.EntityFrameworkCore;
using ps40165_Main.Database;
using ps40165_Main.Database.DbResponse;
using ps40165_Main.Database.DbResponse.ErrorDoc;
using ps40165_Main.Dtos;
using ps40165_Main.Mapper.ModelToDto;
using ps40165_Main.Models;

namespace ps40165_Main.Services;

public class OrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IDbResponse> AddOrder(int accountId)
    {
        Order order = new Order { AccountId = accountId };

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        return DbResponse.Success;
    }

    public async Task<IDbResponse> AddItemsToOrder(int orderId, List<OrderItem> orderItems)
    {
        var found = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (found == null)
            return DbResponse.Failure(new OrderError().NotFound());

        foreach (var item in orderItems) 
            found.OrderItems.Add(item);

        found.Total = orderItems.Sum(oi => oi.Total);

        await _context.SaveChangesAsync();

        OrderDto data = new OrderMapper().Map(found);

        return DbQuery<OrderDto>.GiveBack(data);
    }
}
