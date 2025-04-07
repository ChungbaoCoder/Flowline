using ps40165_Main.Commands;
using ps40165_Main.Database.DbResponse;
using ps40165_Main.Database.DbResponse.ErrorDoc;
using ps40165_Main.Models;

namespace ps40165_Main.Services;

public class LoginService
{
    private readonly AccountService _service;
    private readonly PasswordHasher _hasher;
    private readonly JwtGenerateService _jwt;

    public LoginService(AccountService service, PasswordHasher hasher, JwtGenerateService jwt)
    {
        _service = service;
        _hasher = hasher;
        _jwt = jwt;
    }

    public async Task<IDbResponse> LoginByPassword(LoginUserCommand request)
    {
        Account? account = await _service.GetUserEmail(request.Email);

        if (account is null)
            return DbResponse.Failure(new LoginError().NotFound());

        bool verified = _hasher.Verify(account.Email, account.PasswordHash);

        if (!verified)
            return DbResponse.Failure(new LoginError().PasswordIncorrect());

        string token = _jwt.GenerateToken(account);

        return DbQuery<object>.GiveBack(new { Token = token });
    }
}
