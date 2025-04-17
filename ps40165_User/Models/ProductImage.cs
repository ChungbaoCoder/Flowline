namespace ps40165_User.Models;

public class ProductImage
{
    public int Id { get; set; }

    public string? ImagePath { get; set; }

    public bool MainImage { get; set; }

    public int? Height { get; set; }

    public int? Width { get; set; }
}
