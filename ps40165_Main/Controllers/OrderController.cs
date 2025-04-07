using Microsoft.AspNetCore.Mvc;
using ps40165_Main.Commands;
using ps40165_Main.Database.DbResponse;
using ps40165_Main.Dtos;
using ps40165_Main.Services;

namespace ps40165_Main.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderService _order;

    public OrderController(OrderService order)
    {
        _order = order;
    }

    [HttpPost]
    public async Task<IActionResult> MakeOrder([FromBody] MakeOrderCommand request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _order.MakeOrder(request);

        if (result.IsSuccess && result is DbQuery<OrderDto> success)
        {
            return Created(HttpContext.Request.Path, new CentralResponse<OrderDto>
            {
                IsSuccess = result.IsSuccess,
                Data = success.Data
            });
        }
        else if (!result.IsSuccess && result is DbResponse failure)
        {
            return BadRequest(new CentralResponse
            {
                IsSuccess = !result.IsSuccess,
                Error = new Error(failure.Error.Type, failure.Error.Detail)
            });
        }
        else
        {
            return BadRequest(result);
        }
    }
}
