using ps40165_Main.Dtos.PostDto;
using ps40165_Main.Models;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Shared.Interfaces;

public interface IUserAccount
{
    Task<Result<Token>> Register(RegisterDto account);

    Task<Result<Token>> Login(LoginDto account);

    Task GetProfile(int accountId);

    Task UpdateProfile(int accountId, Account account);

    Task ChangePassword(string newPassword);
}
