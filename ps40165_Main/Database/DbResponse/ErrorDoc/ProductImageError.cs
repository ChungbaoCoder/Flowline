namespace ps40165_Main.Database.DbResponse.ErrorDoc;

public class ProductImageError : IError
{
    public List<string> Detail { get; }


    public ProductImageError()
    {
        Detail = new List<string>();
    }

    public ProductImageError NotFound()
    {
        Detail.Add("Không tìm thấy hình ảnh sản phẩm");
        return this;
    }
}
