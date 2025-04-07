namespace ps40165_Main.Database.DbResponse.ErrorDoc;

public class CategoryError : IError
{
    public List<string> Detail { get; }

    public CategoryError()
    {
        Detail = new List<string>();
    }

    public CategoryError NotFound()
    {
        Detail.Add("Không tìm thấy loại sản phẩm");
        return this;
    }
}
