using Microsoft.EntityFrameworkCore;
using ps40165_Main.Database;
using ps40165_Main.Dtos.PostDto;
using ps40165_Main.Models;
using ps40165_Main.Shared.Interfaces;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Features.UserAccountFeature;

public class UserAccountService : IUserAccount
{
    private readonly AppDbContext _context;
    private readonly ICustomer _customer;
    private readonly ITokenProvider _token;
    private readonly PasswordHasher _hasher;

    public UserAccountService(AppDbContext context, ICustomer customer, ITokenProvider token, PasswordHasher hasher)
    {
        _context = context;
        _customer = customer;
        _token = token;
        _hasher = hasher;
    }

    public async Task<Result<Token>> Register(RegisterDto account)
    {
        if (await CheckAccountExist(account))
            return Result<Token>.Fail($"Tài khoản với địa chỉ email {account.Email} đã tồn tại");

        var cusExist = await _customer.GetCustomerByEmail(account.Email);

        var acc = new Account();
        string hashed = _hasher.Hash(account.Password);

        if (!cusExist.IsSuccess)
        {
            Result<Customer> cus = await _customer.CreateCustomer(new CreateCustomerDto { Name = account.Name, Email = account.Email });
            Result<Account> result = acc.CreateAccount(account.Name, account.Email, hashed).Bind(r => acc.SignAsCustomer(cus.Data.Id));

            if (result.IsSuccess)
            {
                await _context.Accounts.AddAsync(acc);
                await _context.SaveChangesAsync();
                Token token = new Token(_token.GenerateUserToken(acc));
                return Result<Token>.Ok(token, "Tạo tài khoản thành công");
            }
        }
        else
        {
            Result<Account> result = acc.CreateAccount(account.Name, account.Email, hashed);

            await _context.Accounts.AddAsync(acc);
            await _context.SaveChangesAsync();

            Token token = new Token(_token.GenerateUserToken(acc));
            return Result<Token>.Ok(token, "Tạo tài khoản thành công");
        }
        return Result<Token>.Fail("");
    }

    public async Task<Result<Token>> Login(LoginDto account)
    {
        var acc = await _context.Accounts
            .AsNoTracking()
            .SingleOrDefaultAsync(a => a.Email == account.Email);

        if (acc is null)
            return Result<Token>.Fail($"Không thể tìm thấy tài khoản người dùng có email {account.Email}");

        if (!_hasher.Verify(account.Password, acc.PasswordHash))
            return Result<Token>.Fail("Không đúng mật khẩu của tài khoản");

        Token token = new Token(_token.GenerateUserToken(acc));
        return Result<Token>.Ok(token, "Đăng nhập thành công");
    }

    public Task GetProfile(int accountId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProfile(int accountId, Account account)
    {
        throw new NotImplementedException();
    }

    public Task ChangePassword(string newPassword)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CheckAccountExist(RegisterDto account)
    {
        var acc = await _context.Accounts
            .AsNoTracking()
            .SingleOrDefaultAsync(a => a.Email == account.Email);

        if (acc is not null)
            return true;

        return false;
    }
}
