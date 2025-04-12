using Microsoft.AspNetCore.Mvc;
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

        if (result.IsSuccess && result is DbQuery<AccountDto> query)
        {
            return Created(HttpContext.Request.Path, new CentralResponse<AccountDto>
            {
                IsSuccess = true,
                Data = query.Data
            });
        }
        else if (!result.IsSuccess && result is DbResponse response)
        {
            return BadRequest(new CentralResponse<AccountDto>
            {
                IsSuccess = true,
                Error = new Error(response.Error.Type, response.Error.Detail)
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
            return Created(HttpContext.Request.Path, new CentralResponse
            {
                IsSuccess = true
            });
        }
        else if (!result.IsSuccess)
        {
            return BadRequest(new CentralResponse
            {
                IsSuccess = true,
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
            return Created(HttpContext.Request.Path, new CentralResponse
            {
                IsSuccess = true
            });
        }
        else if (!result.IsSuccess)
        {
            return BadRequest(new CentralResponse
            {
                IsSuccess = true,
            });
        }
        else
        {
            return BadRequest(result);
        }
    }
}
