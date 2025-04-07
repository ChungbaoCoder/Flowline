namespace ps40165_Main.Database.DbResponse.ErrorDoc;

public class ProductImageError : IError
{
    public List<string> Errors { get; }


    public ProductImageError()
    {
        Errors = new List<string>();
    }

    public ProductImageError NotFound()
    {
        Errors.Add("Không tìm thấy hình ảnh sản phẩm");
        return this;
    }
}
