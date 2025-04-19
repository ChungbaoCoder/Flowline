using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ps40165_Main.Commands;
using ps40165_Main.Dtos;
using ps40165_Main.Services;

namespace ps40165_Main.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductService _product;

    public ProductsController(ProductService product)
    {
        _product = product;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] QueryPageCommand request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _product.GetListProducts(request);

        if (result.IsSuccess)
        {
            return Ok(new CentralResponse<PaginatedList<ProductDto>>
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data
            });
        }
        else if (!result.IsSuccess)
        {
            return BadRequest(new CentralResponse<PaginatedList<ProductDto>>
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

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        if (id < 1)
        {
            return BadRequest();
        }

        var result = await _product.GetProductById(id);

        if (result.IsSuccess)
        {
            return Ok(new CentralResponse<ProductDto>
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data
            });
        }
        else if (!result.IsSuccess)
        {
            return BadRequest(new CentralResponse<ProductDto>
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
    public async Task<IActionResult> AddProduct([FromBody] AddProductCommand request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _product.AddProduct(request);

        if (result.IsSuccess)
        {
            return Created(HttpContext.Request.Path, new CentralResponse<ProductDto>
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data
            });
        }
        else if (!result.IsSuccess)
        {
            return BadRequest(new CentralResponse<ProductDto>
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

    [HttpPut("{id}")]
    public async Task<IActionResult> EditProduct(int id, [FromBody] EditProductCommand request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _product.EditProduct(id, request);

        if (result.IsSuccess)
        {
            return Ok(new CentralResponse<ProductDto>
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data
            });
        }
        else if (!result.IsSuccess)
        {
            return BadRequest(new CentralResponse<ProductDto>
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        if (id < 0)
            return BadRequest();

        var result = await _product.DeleteProduct(id);

        if (result.IsSuccess)
        {
            return Ok(new CentralResponse<string>
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
                Error = new Error(result.Errors.Type, result.Errors.Detail)
            });
        }
        else
        {
            return BadRequest(result);
        }
    }
}
