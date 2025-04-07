namespace ps40165_Main.Database.DbResponse.ErrorDoc;

public class OrderError : IError
{
    public List<string> Detail { get; }

    public OrderError()
    {
        Detail = new List<string>();
    }

    public OrderError NotFound()
    {
        Detail.Add("Không tìm thấy hóa đơn");
        return this;
    }
}
