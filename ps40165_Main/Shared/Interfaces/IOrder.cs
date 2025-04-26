using ps40165_Main.Dtos.PostDto;
using ps40165_Main.Models;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Shared.Interfaces;

public interface IOrder
{
    Task<Result<List<Order>>> GetListOrders();

    Task<Result<Order>> GetOrderById(int orderId);

    Task<Result<List<Order>>> GetOrderByCustomerId(int customerId);

    Task<Result<Order>> CreateOrder(CreateOrderDto order);

    Task<Result<Order>> UpdateOrderStatus(int orderId, Order order);

    Task<Result<Order>> CancelOrder(int orderId);
}
