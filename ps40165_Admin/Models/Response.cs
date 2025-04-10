namespace ps40165_Admin.Models;

public class Response<T>
{
    public bool IsSuccess { get; set; }

    public Error? Error { get; set; }

    public PaginationMetadata? Pagination { get; set; }

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

public class PaginationMetadata
{
    public int TotalCount { get; set; }

    public int PageSize { get; set; }

    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }
}

