using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ps40165_Main.Commands;
using ps40165_Main.Dtos;
using ps40165_Main.Services;

namespace ps40165_Main.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly CategoryService _category;

    public CategoriesController(CategoryService category)
    {
        _category = category;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetCategories([FromQuery] QueryPageCommand request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _category.GetListCategories(request);

        if (result.IsSuccess)
        {
            return Ok(new CentralResponse<PaginatedList<CategoryDto>>
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data
            });
        }
        else if (!result.IsSuccess)
        {
            return BadRequest(new CentralResponse<PaginatedList<CategoryDto>>
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
    public async Task<IActionResult> GetCategoryById(int id)
    {
        if (id < 1)
        {
            return BadRequest();
        }

        var result = await _category.GetCategoryById(id);

        if (result.IsSuccess)
        {
            return Ok(new CentralResponse<CategoryDto>
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data
            });
        }
        else if (!result.IsSuccess)
        {
            return BadRequest(new CentralResponse<CategoryDto>
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
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryCommand request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _category.AddCategory(request);

        if (result.IsSuccess)
        {
            return Created(HttpContext.Request.Path, new CentralResponse<CategoryDto>
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data
            });
        }
        else if (!result.IsSuccess)
        {
            return BadRequest(new CentralResponse<CategoryDto>
            {
                IsSuccess = !result.IsSuccess,
                Error = new Error(result.Errors.Type, result.Errors.Detail)
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
            return Ok(new CentralResponse<CategoryDto>
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data
            });
        }
        else if (!result.IsSuccess)
        {
            return BadRequest(new CentralResponse<CategoryDto>
            {
                IsSuccess = !result.IsSuccess,
                Error = new Error(result.Errors.Type, result.Errors.Detail)
            });
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _category.DeleteCategory(id);

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
                IsSuccess = !result.IsSuccess,
                Error = new Error(result.Errors.Type, result.Errors.Detail)
            });
        }
        else
        {
            return BadRequest(result);
        }
    }
}
