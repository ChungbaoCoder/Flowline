namespace ps40165_Main.Database.DbResponse.ErrorDoc;

public class CategoryError : IError
{
    public List<string> Errors { get; }

    public CategoryError()
    {
        Errors = new List<string>();
    }

    public CategoryError NotFound()
    {
        Errors.Add("Không tìm thấy loại sản phẩm");
        return this;
    }
}
