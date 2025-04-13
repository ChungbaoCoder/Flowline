﻿using Microsoft.AspNetCore.Mvc;
using ps40165_Main.Commands;
using ps40165_Main.Database.DbResponse;
using ps40165_Main.Dtos;
using ps40165_Main.Services;

namespace ps40165_Main.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _account;

    public AccountController(AccountService account)
    {
        _account = account;
    }

    [HttpPost("user")]
    public async Task<IActionResult> RegisterUserAccount([FromBody] RegisterUserCommand request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _account.RegisterUser(request);

        if (result.IsSuccess)
        {
            return Created(HttpContext.Request.Path, new CentralResponse<TokenDto>
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data
            });
        }
        else if (!result.IsSuccess)
        {
            return BadRequest(new CentralResponse<TokenDto>
            {
                IsSuccess = result.IsSuccess,
                Error = new Error(result.Errors.Type, result.Errors.Detail)
            });
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpPost("admin")]
    public async Task<IActionResult> RegisterAdminAccount([FromBody] RegisterAdminCommand request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _account.RegisterAdminAsync(request);

        if (result.IsSuccess)
        {
            return Created(HttpContext.Request.Path, new CentralResponse<string>
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data
            });
        }
        else if (!result.IsSuccess)
        {
            return BadRequest(new CentralResponse<string>
            {
                IsSuccess = result.IsSuccess,
            });
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpPost("moderator")]
    public async Task<IActionResult> RegisterModeratorAccount([FromBody] RegisterModeratorCommand request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _account.RegisterModeratorAsync(request);

        if (result.IsSuccess)
        {
            return Created(HttpContext.Request.Path, new CentralResponse<string>
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data
            });
        }
        else if (!result.IsSuccess)
        {
            return BadRequest(new CentralResponse<string>
            {
                IsSuccess = result.IsSuccess,
            });
        }
        else
        {
            return BadRequest(result);
        }
    }
}
