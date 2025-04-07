namespace ps40165_Main.Database.DbResponse.ErrorDoc;

public class OrderError : IError
{
    public List<string> Errors { get; }

    public OrderError()
    {
        Errors = new List<string>();
    }

    public OrderError NotFound()
    {
        Errors.Add("Không tìm thấy hóa đơn");
        return this;
    }
}
