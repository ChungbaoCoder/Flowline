namespace ps40165_Main.Dtos.PutDto;

public class EditProductDto
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}
