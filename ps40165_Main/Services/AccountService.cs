using Microsoft.EntityFrameworkCore;
using ps40165_Main.Commands;
using ps40165_Main.Database;
using ps40165_Main.Database.DbResponse;
using ps40165_Main.Dtos;
using ps40165_Main.Mapper.ModelToDto;
using ps40165_Main.Models;

namespace ps40165_Main.Services;

public class AccountService
{
    private readonly AppDbContext _context;
    private readonly PasswordHasher _hasher;

    public AccountService(AppDbContext context, PasswordHasher hasher)
    {
        _context = context;
        _hasher = hasher;
    }

    public async Task<IDbResponse> Register(RegisterUserCommand request)
    {
        string hashedPassword = _hasher.Hash(request.Password);

        Account account = new Account
        {
            Name = request.Name,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = hashedPassword
        };

        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();

        AccountDto data = new AccountMapper().Map(account);

        return DbQuery<AccountDto>.GiveBack(data);
    }

    public async Task<Account?> GetUserEmail(string email)
    {
        return await _context.Accounts.SingleOrDefaultAsync(a => a.Email == email);
    }
}
