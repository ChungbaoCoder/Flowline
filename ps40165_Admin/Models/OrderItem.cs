using ps40165_Admin.Shared;

namespace ps40165_Admin.Models;

public class OrderItem : BaseEntity
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}
