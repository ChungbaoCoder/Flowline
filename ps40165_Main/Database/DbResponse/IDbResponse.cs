using ps40165_Main.Database.DbResponse.ErrorDoc;

namespace ps40165_Main.Database.DbResponse;

public interface IDbResponse
{
    public bool IsSuccess { get; }

    public IError Error { get; }
}
