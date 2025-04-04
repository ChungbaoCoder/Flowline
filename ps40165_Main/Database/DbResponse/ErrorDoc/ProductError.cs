namespace ps40165_Main.Database.DbResponse.ErrorDoc;

public class ProductError : IError
{
    public List<string> Errors { get; }

    public ProductError()
    {
        Errors = new List<string>();
    }

    public ProductError NotFound()
    {
        Errors.Add("Không tìm thấy sản phẩm");
        return this;
    }
}
