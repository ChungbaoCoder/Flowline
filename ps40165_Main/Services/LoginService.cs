using Microsoft.AspNetCore.Identity;
using ps40165_Main.Commands;
using ps40165_Main.Database.DbResponse;
using ps40165_Main.Database.DbResponse.ErrorDoc;
using ps40165_Main.Dtos;
using ps40165_Main.Models;

namespace ps40165_Main.Services;

public class LoginService
{
    private readonly AccountService _service;
    private readonly PasswordHasher _hasher;
    private readonly JwtGenerateService _jwt;
    private readonly UserManager<IdentityUser> _userManager;

    public LoginService(AccountService service, PasswordHasher hasher, JwtGenerateService jwt, UserManager<IdentityUser> userManager)
    {
        _service = service;
        _hasher = hasher;
        _jwt = jwt;
        _userManager = userManager;
    }

    public async Task<IDbResponse<TokenDto>> LoginUser(LoginUserCommand request)
    {
        Account? account = await _service.GetUserByEmail(request.Email);

        if (account is null)
            return DbResponse<TokenDto>.Failure(new LoginError().NotFound());

        bool verified = _hasher.Verify(request.Password, account.PasswordHash);

        if (!verified)
            return DbResponse<TokenDto>.Failure(new LoginError().PasswordIncorrect());

        TokenDto token = new TokenDto();
        token.Token = _jwt.GenerateUserToken(account);

        return DbResponse<TokenDto>.GiveBack(token);
    }

    public async Task<IDbResponse<TokenDto>> LoginEmployee(LoginEmployeeCommand request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return DbResponse<TokenDto>.Failure(new LoginError().NotFound());

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return DbResponse<TokenDto>.Failure(new LoginError().PasswordIncorrect());
        }

        var roles = await _userManager.GetRolesAsync(user);

        TokenDto token = new TokenDto();
        token.Token = _jwt.GenerateEmployeeToken(user, roles);

        return DbResponse<TokenDto>.GiveBack(token);
    }
}
