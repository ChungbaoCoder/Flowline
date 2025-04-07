using Microsoft.EntityFrameworkCore;
using ps40165_Main.Database;
using ps40165_Main.Models;

namespace ps40165_Main.Services;

public class AccountService
{
    private readonly AppDbContext _context;

    public AccountService(AppDbContext context)
    {
        _context = context;
    }

    public async Task Register(Account account)
    {
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
    }

    public async Task<Account?> GetUserEmail(string email)
    {
        return await _context.Accounts.SingleOrDefaultAsync(a => a.Email == email);
    }
}
