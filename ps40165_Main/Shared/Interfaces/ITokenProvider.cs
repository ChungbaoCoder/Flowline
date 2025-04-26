using ps40165_Main.Models;

namespace ps40165_Main.Shared.Interfaces;

public interface ITokenProvider
{
    string GenerateUserToken(Account account);
}
