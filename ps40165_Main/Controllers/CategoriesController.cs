using Microsoft.AspNetCore.Mvc;
using ps40165_Main.Commands;
using ps40165_Main.Services;

namespace ps40165_Main.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly CategoryService _service;

    public CategoriesController(CategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories([FromQuery] QueryPageCommand request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _service.GetListCategories(request);

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
    public async Task<IActionResult> GetCategoryById(int id)
    {
        if (id < 1)
        {
            return BadRequest();
        }

        var result = await _service.GetCategoryById(id);

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
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryCommand request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _service.AddCategory(request);

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
    public async Task<IActionResult> EditCategory(int id, [FromBody] EditCategoryCommand request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _service.EditCategory(id, request);

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
