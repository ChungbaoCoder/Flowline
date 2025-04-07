using ps40165_Main.Dtos;
using ps40165_Main.Models;

namespace ps40165_Main.Mapper.ModelToDto;

public class AccountMapper : IMapper<Account, AccountDto>
{
    public AccountDto Map(Account src)
    {
        return new AccountDto
        {
            Name = src.Name,
            Email = src.Email,
            PhoneNumber = src.PhoneNumber,
            PasswordHash = src.PasswordHash,
            GoogleUserId = src.GoogleUserId,
            LastLogin = src.LastLogin
        };
    }
}
