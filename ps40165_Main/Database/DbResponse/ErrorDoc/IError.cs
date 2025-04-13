namespace ps40165_Main.Database.DbResponse.ErrorDoc;

public interface IError
{
    public string Type { get; }

    public List<string> Detail { get; }
}
