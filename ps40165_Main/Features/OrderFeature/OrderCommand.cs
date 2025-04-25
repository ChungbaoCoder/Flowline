using ps40165_Main.Models;
using ps40165_Main.Shared;
using ps40165_Main.Shared.Interfaces;

namespace ps40165_Main.Features.OrderFeature;

public class OrderCommand
{
    private readonly IOrder _svc;

    public OrderCommand(IOrder svc) => _svc = svc;

    public async Task<CentralResponse<List<Order>>> GetList()
    {
        var result = await _svc.GetListOrders();

        if (result.IsSuccess)
        {
            return new CentralResponse<List<Order>>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<List<Order>>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }

    public async Task<CentralResponse<Order>> GetById(int orderId)
    {
        var result = await _svc.GetOrderById(orderId);

        if (result.IsSuccess)
        {
            return new CentralResponse<Order>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<Order>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }

    public async Task<CentralResponse<List<Order>>> GetByCustomerId(int customerId)
    {
        var result = await _svc.GetOrderByCustomerId(customerId);

        if (result.IsSuccess)
        {
            return new CentralResponse<List<Order>>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<List<Order>>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }

    public async Task<CentralResponse<Order>> Create(Order order, List<OrderItem> items)
    {
        var result = await _svc.CreateOrder(order, items);

        if (result.IsSuccess)
        {
            return new CentralResponse<Order>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<Order>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }
}
