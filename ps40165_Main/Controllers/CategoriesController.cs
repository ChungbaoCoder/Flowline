using Microsoft.AspNetCore.Mvc;
using ps40165_Main.Commands;
using ps40165_Main.Database.DbResponse;
using ps40165_Main.Dtos;
using ps40165_Main.Services;

namespace ps40165_Main.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly CategoryService _category;

    public CategoriesController(CategoryService category)
    {
        _category = category;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories([FromQuery] QueryPageCommand request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _category.GetListCategories(request);

        if (result.IsSuccess && result is DbPagination<CategoryDto> query)
        {
            return Created(HttpContext.Request.Path, new CentralResponse<List<CategoryDto>>
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
    public async Task<IActionResult> GetCategoryById(int id)
    {
        if (id < 1)
        {
            return BadRequest();
        }

        var result = await _category.GetCategoryById(id);

        if (result.IsSuccess && result is DbQuery<CategoryDto> query)
        {
            return Created(HttpContext.Request.Path, new CentralResponse<CategoryDto>
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
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryCommand request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _category.AddCategory(request);

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
    public async Task<IActionResult> EditCategory(int id, [FromBody] EditCategoryCommand request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _category.EditCategory(id, request);

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
