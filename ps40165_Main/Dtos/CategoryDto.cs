namespace ps40165_Main.Dtos;

public class CategoryDto
{
    public int CategoryId { get; set; }

    public required string Name { get; set; }

    public string Alias { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool Active { get; set; }
}
