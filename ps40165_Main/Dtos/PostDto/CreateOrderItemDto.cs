using ps40165_Main.Models;

namespace ps40165_Main.Dtos;

public class CreateOrderItemDto
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}

public class MapToModel
{
    public static List<OrderItem> Map(List<CreateOrderItemDto> items)
    {
        List<OrderItem> orderedItems = new List<OrderItem>();

        foreach (var item in items)
        {
            OrderItem orderItem = new OrderItem();
            orderItem.AddOrderItem(item.ProductId, item.ProductName, item.Quantity, item.Price);
            orderedItems.Add(orderItem);
        }
        return orderedItems;
    }
}
