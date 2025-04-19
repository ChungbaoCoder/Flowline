using Microsoft.AspNetCore.Mvc;
using ps40165_Main.Commands;
using ps40165_Main.Dtos;
using ps40165_Main.Models;
using ps40165_Main.Services;

namespace ps40165_Main.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
    private readonly ProductImageService _image;

    public ImagesController(ProductImageService image)
    {
        _image = image;
    }

    [HttpGet("product/{productId}")]
    public async Task<IActionResult> GetImages(int productId)
    {
        var result = await _image.GetListImages(productId);

        if (result.IsSuccess)
        {
            return Ok(new CentralResponse<List<ProductImageDto>>
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data
            });
        }
        else if (!result.IsSuccess)
        {
            return BadRequest(new CentralResponse<List<ProductImageDto>>
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
    public async Task<IActionResult> UploadImage([FromForm] AddProductImageCommand request, IFormFile file)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (file is null || file.Length == 0)
            return BadRequest(ModelState);

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        ProductImage imageEntity = new ProductImage
        {
            ProductId = request.ProductId,
            ImagePath = Path.Combine("uploads", uniqueFileName).Replace("\\", "/"), // For web use
            MainImage = request.MainImage,
            Height = request.Height,
            Width = request.Width
        };

        var result = await _image.AddImage(imageEntity);

        if (result.IsSuccess)
        {
            return Ok(new CentralResponse<List<ProductImageDto>>
            {
                IsSuccess = result.IsSuccess,
                Data = result.Data
            });
        }
        else if (!result.IsSuccess)
        {
            return BadRequest(new CentralResponse<List<ProductImageDto>>
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

    [HttpPut]
    public async Task<IActionResult> EditImage([FromForm] EditProductImageCommand request, IFormFile file)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var image = await _image.GetImage(request.ImageId);

        if (image.ImagePath is null)
        {
            return NotFound();
        }

        if (file != null && file.Length > 0)
        {
            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.ImagePath);

            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var newFilePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(newFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            image.ImagePath = Path.Combine("uploads", uniqueFileName).Replace("\\", "/");
            image.MainImage = request.MainImage;
            image.Height = request.Height;
            image.Width = request.Width;
        }

        var result = await _image.EditImage(request.ImageId, image);

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

    [HttpDelete]
    public async Task DeleteImage()
    {

    }
}
