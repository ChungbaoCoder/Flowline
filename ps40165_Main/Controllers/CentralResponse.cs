using ps40165_Main.Database.DbResponse;

namespace ps40165_Main.Controllers;

public class CentralResponse
{
    public bool IsSuccess { get; set; }

    public Error? Error { get; set; }
}

public class CentralResponse<T>
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
