namespace ps40165_Admin.Models;

public class CategoryDto
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Alias { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}
