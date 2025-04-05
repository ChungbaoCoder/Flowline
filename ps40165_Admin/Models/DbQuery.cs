namespace ps40165_Admin.Models;

public class DbQuery<T> : IResponse
{
    public bool IsSuccess { get; }

    public T Data { get; }

    public DbQuery(bool isSuccess, T data)
    {
        IsSuccess = isSuccess;
        Data = data;
    }
}
