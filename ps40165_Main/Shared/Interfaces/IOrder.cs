using ps40165_Main.Models;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Shared.Interfaces;

public interface IOrder
{
    public Task<Result<List<Order>>> GetListOrders();

    public Task<Result<Order>> GetOrderById(int orderId);

    public Task<Result<List<Order>>> GetOrderByCustomerId(int customerId);

    public Task<Result<Order>> CreateOrder(Order order, List<OrderItem> items);

    public Task<Result<Order>> UpdateOrderStatus(int orderId, Order order);

    public Task<Result<Order>> CancelOrder(int orderId);
}
