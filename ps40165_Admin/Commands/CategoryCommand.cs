using System.ComponentModel.DataAnnotations;

namespace ps40165_Admin.Commands;

public class AddCategoryCommand
{
    [Required(ErrorMessage = "Cần tên loại sản phẩm")]
    [StringLength(5, ErrorMessage = "Tên cần ít nhất 5 trở lên")]
    public string Name { get; set; } = string.Empty;

    public string? Alias { get; set; }

    public string Description { get; set; } = string.Empty;
}

public class EditCategoryCommand
{
    [Required(ErrorMessage = "Cần tên loại sản phẩm")]
    [StringLength(5, ErrorMessage = "Tên cần ít nhất 5 trở lên")]
    public string Name { get; set; } = string.Empty;

    public string? Alias { get; set; }

    public string Description { get; set; } = string.Empty;
}
