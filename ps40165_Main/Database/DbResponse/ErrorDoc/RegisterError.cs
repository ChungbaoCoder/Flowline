namespace ps40165_Main.Database.DbResponse.ErrorDoc;

public class RegisterError : IError
{
    public string Type => "Login";
    public List<string> Detail { get; }

    public RegisterError()
    {
        Detail = new List<string>();
    }

    public RegisterError AlreadyExist(string email)
    {
        Detail.Add($"Tài khoản này đã có email {email} tồn tại");
        return this;
    }
}
