namespace ps40165_Main.Database.DbResponse;

public class DbQuery<T> : IDbResponse
{
    private DbQuery(bool isSuccess, T data)
    {
        if (!isSuccess && data == null)
        {
            throw new ArgumentNullException("Failure response must include an error.");
        }

        IsSuccess = isSuccess;
        Data = data;
    }

    public bool IsSuccess { get; }

    public T Data { get; }

    public static DbQuery<T> GiveBack(T dto) => new DbQuery<T>(true, dto);
}
