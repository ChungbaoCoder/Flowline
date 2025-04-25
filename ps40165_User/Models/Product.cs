using ps40165_User.Shared;

namespace ps40165_User.Models;

public class Product : BaseEntity
{
    public int CategoryId { get;  set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public Category Category { get; set; } = new Category();
}
