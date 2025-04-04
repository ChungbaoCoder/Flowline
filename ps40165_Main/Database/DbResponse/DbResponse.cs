using ps40165_Main.Database.DbResponse.ErrorDoc;

namespace ps40165_Main.Database.DbResponse;

public class DbResponse : IDbResponse
{
    private DbResponse(bool isSuccess, IError error = null!)
    {
        if (!isSuccess && error == null)
        {
            throw new ArgumentNullException(nameof(error), "Failure response must include an error.");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public IError Error { get; }

    public static DbResponse Success => new DbResponse(true);

    public static DbResponse Failure(IError error) => new DbResponse(false, error);
}
