using ps40165_Admin.Shared;

namespace ps40165_Admin.Models;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Alias { get; set; } = string.Empty;
}
