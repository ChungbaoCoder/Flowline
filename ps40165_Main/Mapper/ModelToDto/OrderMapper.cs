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
            Customer = new CustomerDto 
            { 
                CustomerId = src.Customer.Id, 
                CustomerName = src.Customer.Name 
            },
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
            CreatedOnUtc = src.CreatedOnUtc,
            UpdatedOnUtc = src.UpdatedOnUtc
        };
    }
}
