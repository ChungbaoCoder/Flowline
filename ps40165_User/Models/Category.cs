using ps40165_User.Shared;

namespace ps40165_User.Models;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Alias { get; set; } = string.Empty;
}
