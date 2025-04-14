namespace ps40165_User.Models;

public class Response<T>
{
    public bool IsSuccess { get; set; }

    public Error? Error { get; set; }

    public T? Data { get; set; }
}

public class Error
{
    public string Type { get; set; }

    public List<string> Detail { get; set; }

    public Error(string type, List<string> detail)
    {
        Type = type;
        Detail = detail;
    }
}

public class PaginatedList<T>
{
    public int PageNumber { get; }

    public int PageSize { get; }

    public int TotalCount { get; }

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
