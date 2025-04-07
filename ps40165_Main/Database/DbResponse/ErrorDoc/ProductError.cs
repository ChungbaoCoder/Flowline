namespace ps40165_Main.Database.DbResponse.ErrorDoc;

public class ProductError : IError
{
    public string Type => "Product";
    public List<string> Detail { get; }

    public ProductError()
    {
        Detail = new List<string>();
    }

    public ProductError NotFound()
    {
        Detail.Add("Không tìm thấy sản phẩm");
        return this;
    }
}
