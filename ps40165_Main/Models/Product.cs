namespace ps40165_Main.Models;

public class Product : BaseEntity
{
    public int CategoryId { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? UnderDescription { get; set; }

    public int StockLevel { get; set; }

    public decimal Price { get; set; }

    public bool DisableBuyButton { get; set; }

    //RelationShip
    public Category Category { get; set; }

    public ICollection<ProductImage> ProductImages { get; set; }
}
