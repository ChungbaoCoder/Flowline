using ps40165_Main.Database.DbResponse.ErrorDoc;

namespace ps40165_Main.Database.DbResponse;

public class DbPagination<TDto> : IDbResponse
{
    private DbPagination(bool isSuccess, PaginationMetadata metadata, List<TDto> data)
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

    public IError Error { get; }

    public PaginationMetadata Metadata { get; }

    public List<TDto> Data { get; }

    public static DbPagination<TDto> GiveBack(PaginationMetadata metadata, List<TDto> data) => new DbPagination<TDto>(true, metadata, data);
}

public class PaginationMetadata
{
    public int TotalCount { get; set; }

    public int PageSize { get; set; }

    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }
}
