using ps40165_Main.Database.DbResponse.ErrorDoc;

namespace ps40165_Main.Database.DbResponse;

public class DbResponse<T> : IDbResponse<T>
{
    public bool IsSuccess { get; }

    public IError Errors { get; }

    public T Data { get; }

    private DbResponse()
    {
        IsSuccess = true;
    }

    private DbResponse(T data)
    {
        IsSuccess = true;
        Data = data;
    }

    private DbResponse(IError error)
    {
        IsSuccess = false;
        Errors = error;
    }

    public static DbResponse<T> Success => new DbResponse<T>();

    public static DbResponse<T> GiveBack(T data) => new DbResponse<T>(data);

    public static DbResponse<T> Failure(IError error) => new DbResponse<T>(error);
}
