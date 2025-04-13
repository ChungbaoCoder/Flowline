using ps40165_Main.Database.DbResponse.ErrorDoc;

namespace ps40165_Main.Database.DbResponse;

public interface IDbResponse<T>
{
    public bool IsSuccess { get; }

    public IError Errors { get; }

    public T Data { get; }
}
