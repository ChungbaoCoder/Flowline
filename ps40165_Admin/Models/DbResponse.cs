namespace ps40165_Admin.Models;

public class DbResponse : IResponse
{
    public bool IsSuccess { get; set; }

    public ErrorResponse Errors { get; set; }
}
