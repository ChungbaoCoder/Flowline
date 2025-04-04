namespace ps40165_Main.Database.DbResponse;

public class DbPagination<T> : IDbResponse
{
    private DbPagination(bool isSuccess, PaginationMetadata metadata, List<T> data)
    {
        if (!isSuccess && metadata == null)
        {
            throw new ArgumentNullException("Failure response must include an error.");
        }

        IsSuccess = isSuccess;
        Metadata = metadata;
        Data = data;
    }

    public bool IsSuccess { get; }

    public PaginationMetadata Metadata { get; }

    public List<T> Data { get; }

    public static DbPagination<T> GiveBack(PaginationMetadata metadata, List<T> data) => new DbPagination<T>(true, metadata, data);
}

public class PaginationMetadata
{
    public int TotalCount { get; set; }

    public int PageSize { get; set; }

    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }
}
