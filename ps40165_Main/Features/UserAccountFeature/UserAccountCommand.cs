using ps40165_Main.Dtos.PostDto;
using ps40165_Main.Models;
using ps40165_Main.Shared;
using ps40165_Main.Shared.Interfaces;

namespace ps40165_Main.Features.UserAccountFeature;

public class UserAccountCommand
{
    private IUserAccount _svc;

    public UserAccountCommand(IUserAccount svc) => _svc = svc;

    public async Task<CentralResponse<Token>> Register(RegisterDto acc)
    {
        var result = await _svc.Register(acc);

        if (result.IsSuccess)
        {
            return new CentralResponse<Token>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<Token>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }

    public async Task<CentralResponse<Token>> Login(LoginDto acc)
    {
        var result = await _svc.Login(acc);

        if (result.IsSuccess)
        {
            return new CentralResponse<Token>
            {
                Messages = result.Messages,
                Data = result.Data
            };
        }
        else
        {
            return new CentralResponse<Token>
            {
                Errors = result.Errors,
                Data = result.Data
            };
        }
    }
}
