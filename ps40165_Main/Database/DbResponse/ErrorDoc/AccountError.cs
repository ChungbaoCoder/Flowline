namespace ps40165_Main.Database.DbResponse.ErrorDoc;

public class AccountError : IError
{
    public string Type => "Account error";

    public List<string> Detail { get; }

    public AccountError()
    {
        Detail = new List<string>();
    }
}
