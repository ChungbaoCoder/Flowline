using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ps40165_Main.Commands;
using ps40165_Main.Database;
using ps40165_Main.Database.DbResponse;
using ps40165_Main.Database.DbResponse.ErrorDoc;
using ps40165_Main.Dtos;
using ps40165_Main.Mapper.ModelToDto;
using ps40165_Main.Models;

namespace ps40165_Main.Services;

public class AccountService
{
    private readonly AppDbContext _context;
    private readonly PasswordHasher _hasher;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountService(AppDbContext context, PasswordHasher hasher, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _hasher = hasher;
        _userManager = userManager;
    }

    public async Task<IDbResponse> RegisterUser(RegisterUserCommand request)
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

    public async Task<IDbResponse> RegisterAdminAsync(RegisterAdminCommand request)
    {
        return await RegisterWithRoleAsync(request.Email, request.Password, "Admin");
    }

    public async Task<IDbResponse> RegisterModeratorAsync(RegisterModeratorCommand request)
    {
        return await RegisterWithRoleAsync(request.Email, request.Password, "Moderator");
    }

    private async Task<IDbResponse> RegisterWithRoleAsync(string email, string password, string role)
    {
        if (await _userManager.FindByEmailAsync(email).ConfigureAwait(false) is not null)
            return DbResponse.Failure(new RegisterError().AlreadyExist(email));

        var user = new IdentityUser
        {
            UserName = email,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        return DbResponse.Success;
    }

    public async Task<Account?> GetUserEmail(string email)
    {
        return await _context.Accounts.SingleOrDefaultAsync(a => a.Email == email);
    }
}
