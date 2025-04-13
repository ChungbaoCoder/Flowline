using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ps40165_Main.Commands;
using ps40165_Main.Dtos;
using ps40165_Main.Services;

namespace ps40165_Main.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderService _order;

    public OrderController(OrderService order)
    {
        _order = order;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetOrders ([FromQuery] QueryPageCommand request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _order.GetListOrder(request);

        if (result.IsSuccess)
        {
            return Ok(new CentralResponse<PaginatedList<OrderDto>>
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data
            });
        }
        else if (!result.IsSuccess)
        {
            return BadRequest(new CentralResponse<PaginatedList<OrderDto>>
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

    [HttpPost]
    public async Task<IActionResult> MakeOrder([FromBody] MakeOrderCommand request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _order.MakeOrder(request);

        if (result.IsSuccess)
        {
            return Created(HttpContext.Request.Path, new CentralResponse<OrderDto>
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data
            });
        }
        else if (!result.IsSuccess)
        {
            return BadRequest(new CentralResponse<OrderDto>
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
}
