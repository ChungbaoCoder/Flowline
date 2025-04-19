namespace ps40165_Main.Commands;

public class AddProductImageCommand
{
    public int ProductId { get; set; }

    public string? ImagePath { get; set; }

    public bool MainImage { get; set; }

    public int? Height { get; set; }

    public int? Width { get; set; }
}

public class EditProductImageCommand
{
    public int ImageId { get; set; }

    public string? ImagePath { get; set; }

    public bool MainImage { get; set; }

    public int? Height { get; set; }

    public int? Width { get; set; }
}
