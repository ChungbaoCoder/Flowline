namespace ps40165_Main.Database.DbResponse.ErrorDoc;

public class LoginError : IError
{
    public List<string> Errors { get; }

    public LoginError()
    {
        Errors = new List<string>();
    }

    public LoginError AlreadyExist(string email)
    {
        Errors.Add($"Tài khoản này đã có email {email} tồn tại");
        return this;
    }

    public LoginError PasswordIncorrect()
    {
        Errors.Add("Mật khẩu không đúng với mật khẩu của tài khoản");
        return this;
    }

    public LoginError NotFound()
    {
        Errors.Add("Người dùng không tìm thấy");
        return this;
    }
}
