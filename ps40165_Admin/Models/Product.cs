using ps40165_Admin.Shared;

namespace ps40165_Admin.Models;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public Category Category { get; set; } = new Category();
}
