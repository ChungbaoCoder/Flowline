﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ps40165_Main.Commands;
using ps40165_Main.Database;
using ps40165_Main.Database.DbResponse;
using ps40165_Main.Database.DbResponse.ErrorDoc;
using ps40165_Main.Dtos;
using ps40165_Main.Models;

namespace ps40165_Main.Services;

public class AccountService
{
    private readonly AppDbContext _context;
    private readonly PasswordHasher _hasher;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtGenerateService _jwt;

    public AccountService(AppDbContext context, PasswordHasher hasher, UserManager<IdentityUser> userManager, JwtGenerateService jwt)
    {
        _context = context;
        _hasher = hasher;
        _userManager = userManager;
        _jwt = jwt;
    }

    public async Task<IDbResponse<TokenDto>> RegisterUser(RegisterUserCommand request)
    {
        if (await _context.Accounts.FirstOrDefaultAsync(a => a.Email == request.Email) is not null)
        {
            return DbResponse<TokenDto>.Failure(new RegisterError().AlreadyExist(request.Email));
        }

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

        TokenDto data = new TokenDto();
        data.Token = _jwt.GenerateUserToken(account);

        return DbResponse<TokenDto>.GiveBack(data);
    }

    public async Task<IDbResponse<string>> RegisterAdminAsync(RegisterAdminCommand request)
    {
        return await RegisterWithRoleAsync(request.Email, request.Password, "Admin");
    }

    public async Task<IDbResponse<string>> RegisterModeratorAsync(RegisterModeratorCommand request)
    {
        return await RegisterWithRoleAsync(request.Email, request.Password, "Moderator");
    }

    private async Task<IDbResponse<string>> RegisterWithRoleAsync(string email, string password, string role)
    {
        if (await _userManager.FindByEmailAsync(email) is not null)
            return DbResponse<string>.Failure(new RegisterError().AlreadyExist(email));

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

        return DbResponse<string>.GiveBack("Đăng kí tài khoản thành công, hãy đăng nhập bên cổng đăng nhập");
    }

    public async Task<Account?> GetUserByEmail(string email)
    {
        return await _context.Accounts.SingleOrDefaultAsync(a => a.Email == email);
    }
}
