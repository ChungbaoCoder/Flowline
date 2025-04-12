using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using ps40165_Main.Commands;
using ps40165_Main.Database.DbResponse;
using ps40165_Main.Dtos;
using ps40165_Main.Services;

namespace ps40165_Main.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly LoginService _login;

    public AuthController(LoginService login)
    {
        _login = login;
    }

    [HttpPost("user")]
    public async Task<IActionResult> LoginUserAccount([FromBody] LoginUserCommand request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _login.LoginUser(request);

        if (result.IsSuccess && result is DbQuery<TokenDto> query)
        {
            return Ok(new CentralResponse<TokenDto>
            {
                IsSuccess = true,
                Data = query.Data
            });
        }
        else if (!result.IsSuccess && result is DbResponse response)
        {
            return BadRequest(new CentralResponse
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

    [HttpPost("employee")]
    public async Task<IActionResult> LoginEmployeeAccount([FromBody] LoginEmployeeCommand request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _login.LoginEmployee(request);

        if (result.IsSuccess && result is DbQuery<TokenDto> query)
        {
            return Ok(new CentralResponse<TokenDto>
            {
                IsSuccess = true,
                Data = query.Data
            });
        }
        else if (!result.IsSuccess && result is DbResponse response)
        {
            return BadRequest(new CentralResponse
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
}
