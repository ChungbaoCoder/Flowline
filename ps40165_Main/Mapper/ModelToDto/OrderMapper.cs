using ps40165_Main.Dtos;
using ps40165_Main.Models;

namespace ps40165_Main.Mapper.ModelToDto;

public class OrderMapper : IMapper<Order, OrderDto>
{
    public OrderDto Map(Order src)
    {
        return new OrderDto
        {
            OrderId = src.Id,
            AccountId = src.AccountId,
            OrderDate = src.OrderDate,
            Status = src.Status,
            Completed = src.Completed,
            Items = src.OrderItems.Select(oi => new OrderItemDto
            {
                ProductId = oi.ProductId,
                SKU = oi.SKU,
                ImagePath = oi.ImagePath,
                Quantity = oi.Quantity,
                Price = oi.Price
            })
            .ToList(),
        };
    }
}
