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

public class PaginatedList<T>
{
    public int PageNumber { get; }

    public int PageSize { get; }

    public int TotalCount { get;  }

    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    public List<T> Items { get; }

    public PaginatedList(List<T> items, int pageNumber, int pageSize, int totalCount)
    {
        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
}
