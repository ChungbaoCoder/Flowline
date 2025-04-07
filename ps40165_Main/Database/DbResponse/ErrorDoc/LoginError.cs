namespace ps40165_Main.Database.DbResponse.ErrorDoc;

public class LoginError : IError
{
    public string Type => "Login";
    public List<string> Detail { get; }

    public LoginError()
    {
        Detail = new List<string>();
    }

    public LoginError AlreadyExist(string email)
    {
        Detail.Add($"Tài khoản này đã có email {email} tồn tại");
        return this;
    }

    public LoginError PasswordIncorrect()
    {
        Detail.Add("Mật khẩu không đúng với mật khẩu của tài khoản");
        return this;
    }

    public LoginError NotFound()
    {
        Detail.Add("Người dùng không tìm thấy");
        return this;
    }
}
