namespace ps40165_Main.Dtos;

public class CategoryDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;
}
