using Microsoft.EntityFrameworkCore;
using ps40165_Main.Database;
using ps40165_Main.Dtos.PostDto;
using ps40165_Main.Models;
using ps40165_Main.Shared.Interfaces;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Features.OrderFeature;

public class OrderService : IOrder
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context) => _context = context;

    public async Task<Result<List<Order>>> GetListOrders()
    {
        var orders = await _context.Orders
            .AsNoTracking()
            .Include(o => o.Items)
            .ToListAsync();

        if (orders.Count < 1)
            return Result<List<Order>>.Fail("Không có đơn hàng nào trong danh sách");

        return Result<List<Order>>.Ok(orders, "Lấy danh sách đơn hàng thành công");
    }

    public async Task<Result<Order>> GetOrderById(int orderId)
    {
        var odr = await _context.Orders
            .AsNoTracking()
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (odr is null)
            return Result<Order>.Fail($"Không tìm thấy đơn hàng có mã id {orderId}");

        return Result<Order>.Ok(odr, $"Tìm thấy đơn hàng có mã id {orderId}");
    }

    public async Task<Result<List<Order>>> GetOrderByCustomerId(int customerId)
    {
        var odr = await _context.Orders
            .AsNoTracking()
            .Include(o => o.Items)
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();

        if (odr is null)
            return Result<List<Order>>.Fail($"Không tìm thấy đơn hàng của khách hàng có mã id {customerId}");

        return Result<List<Order>>.Ok(odr, $"Tìm thấy đơn hàng của khách hàng có mã id {customerId}");
    }

    public async Task<Result<Order>> CreateOrder(CreateOrderDto order)
    {
        var odr = new Order();
        var odrItems = MapToModel.Map(order.Items);
        Result<Order> result = odr.CreateOrder(order.CustomerId, odrItems).Bind(r => odr.CalculateTotal());

        if (result.IsSuccess)
        {
            await _context.Orders.AddAsync(odr);
            await _context.SaveChangesAsync();
            result = Result<Order>.Ok("Tạo đơn hàng thành công");
        }

        return result;
    }

    public async Task<Result<Order>> UpdateOrderStatus(int orderId, Order order)
    {
        var odr = await _context.Orders.FindAsync(orderId);

        throw new NotImplementedException();
    }

    public async Task<Result<Order>> CancelOrder(int orderId)
    {
        var odr = await _context.Orders.FindAsync(orderId);

        throw new NotImplementedException();
    }
}
