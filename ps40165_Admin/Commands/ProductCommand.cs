using System.ComponentModel.DataAnnotations;

namespace ps40165_Admin.Commands;

public class AddProductCommand
{
    [Required(ErrorMessage = "Cần mã loại sản phẩm")]
    public int CategoryId { get; set; }

    public string? SKU { get; set; }

    [Required(ErrorMessage = "Cần tên sản phẩm")]
    [StringLength(5, ErrorMessage = "Tên phải ít nhất trở lên 5")]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string UnderDescription { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải từ 1 trở lên")]
    public int StockLevel { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Giá phải từ 1 trở lên")]
    public decimal Price { get; set; }

    public bool DisableBuyButton { get; set; }
}

public class EditProductCommand
{
    public string? SKU { get; set; }

    [Required(ErrorMessage = "Cần tên sản phẩm")]
    [StringLength(5, ErrorMessage = "Tên phải ít nhất trở lên 5")]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string UnderDescription { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải từ 1 trở lên")]
    public int StockLevel { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Giá phải từ 1 trở lên")]
    public decimal Price { get; set; }

    public bool DisableBuyButton { get; set; }
}
