namespace ps40165_Main.Models;

public abstract class BaseEntity
{
    public int Id { get; set; }

    public bool Active { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime UpdatedOnUtc { get; set; }
}
