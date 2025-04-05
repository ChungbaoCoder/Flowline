using Microsoft.AspNetCore.Mvc;
using ps40165_Main.Commands;
using ps40165_Main.Services;

namespace ps40165_Main.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductService _service;

    public ProductsController(ProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] QueryPageCommand request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _service.GetListProducts(request);

        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return NotFound(result);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        if (id < 1)
        {
            return BadRequest();
        }

        var result = await _service.GetProductById(id);

        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return NotFound(result);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] AddProductCommand request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _service.AddProduct(request);

        if (result.IsSuccess)
        {
            return Created(HttpContext.Request.Path, result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditProduct(int id, [FromBody] EditProductCommand request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _service.EditProduct(id, request);

        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return NotFound(result);
        }
    }
}
