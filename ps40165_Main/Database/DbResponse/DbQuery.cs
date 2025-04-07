using ps40165_Main.Database.DbResponse.ErrorDoc;

namespace ps40165_Main.Database.DbResponse;

public class DbQuery<TDto> : IDbResponse
{
    private DbQuery(bool isSuccess, TDto data)
    {
        if (!isSuccess && data == null)
        {
            throw new ArgumentNullException("Failure response must include an error.");
        }

        IsSuccess = isSuccess;
        Data = data;
    }

    public bool IsSuccess { get; }

    public IError Error { get; }

    public TDto Data { get; }

    public static DbQuery<TDto> GiveBack(TDto data) => new DbQuery<TDto>(true, data);
}
