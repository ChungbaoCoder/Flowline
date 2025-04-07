using Microsoft.AspNetCore.Mvc;
using ps40165_Main.Commands;
using ps40165_Main.Database.DbResponse;
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

        if (result.IsSuccess && result is DbPagination<ProductDto> query)
        {
            return Created(HttpContext.Request.Path, new CentralResponse<List<ProductDto>>
            {
                IsSuccess = result.IsSuccess,
                Pagination = query.Metadata,
                Data = query.Data
            });
        }
        else if (!result.IsSuccess && result is DbResponse response)
        {
            return BadRequest(new CentralResponse
            {
                IsSuccess = !result.IsSuccess,
                Error = new Error(response.Error.Type, response.Error.Detail)
            });
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        if (id < 1)
        {
            return BadRequest();
        }

        var result = await _product.GetProductById(id);

        if (result.IsSuccess && result is DbQuery<ProductDto> query)
        {
            return Created(HttpContext.Request.Path, new CentralResponse<ProductDto>
            {
                IsSuccess = result.IsSuccess,
                Data = query.Data
            });
        }
        else if (!result.IsSuccess && result is DbResponse response)
        {
            return BadRequest(new CentralResponse
            {
                IsSuccess = !result.IsSuccess,
                Error = new Error(response.Error.Type, response.Error.Detail)
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

        if (result.IsSuccess && result is DbResponse)
        {
            return Created(HttpContext.Request.Path, new CentralResponse
            {
                IsSuccess = result.IsSuccess
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

    [HttpPut("{id}")]
    public async Task<IActionResult> EditProduct(int id, [FromBody] EditProductCommand request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _product.EditProduct(id, request);

        if (result.IsSuccess && result is DbResponse)
        {
            return Created(HttpContext.Request.Path, new CentralResponse
            {
                IsSuccess = result.IsSuccess
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        if (id < 0)
            return BadRequest();

        var result = await _product.DeleteProduct(id);

        if (result.IsSuccess && result is DbResponse)
        {
            return Created(HttpContext.Request.Path, new CentralResponse
            {
                IsSuccess = result.IsSuccess
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
