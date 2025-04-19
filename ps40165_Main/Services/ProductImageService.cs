using Microsoft.EntityFrameworkCore;
using ps40165_Main.Database;
using ps40165_Main.Database.DbResponse;
using ps40165_Main.Database.DbResponse.ErrorDoc;
using ps40165_Main.Dtos;
using ps40165_Main.Mapper.ModelToDto;
using ps40165_Main.Models;

namespace ps40165_Main.Services;

public class ProductImageService
{
    private readonly AppDbContext _context;

    public ProductImageService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IDbResponse<List<ProductImageDto>>> GetListImages(int productId)
    {
        var found = await _context.ProductImages.Where(pi => pi.ProductId == productId).ToListAsync();

        if (found is null)
            return DbResponse<List<ProductImageDto>>.Failure(new ProductImageError().NotFound());

        List<ProductImageDto> data = new List<ProductImageDto>();

        data = new ProductImageMapper().Map(found);

        return DbResponse<List<ProductImageDto>>.GiveBack(data);
    }

    public async Task<IDbResponse<List<ProductImageDto>>> AddImage(ProductImage image)
    {
        var found = await _context.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == image.ProductId);

        if (found is null)
            return DbResponse<List<ProductImageDto>>.Failure(new ProductImageError());

        found.ProductImages.Add(image);

        await _context.ProductImages.AddAsync(image);
        await _context.SaveChangesAsync();

        List<ProductImageDto> data = new ProductImageMapper().Map(found.ProductImages);

        return DbResponse<List<ProductImageDto>>.GiveBack(data);
    }

    public async Task<IDbResponse<string>> EditImage(int productImageId, ProductImage image)
    {
        var found = await _context.ProductImages.FindAsync(productImageId);

        if (found is null)
            return DbResponse<string>.Failure(new ProductImageError().NotFound());

        found.ImagePath = image.ImagePath;
        found.MainImage = image.MainImage;
        found.Width = image.Width;
        found.Height = image.Height;

        await _context.SaveChangesAsync();

        return DbResponse<string>.Success;
    }

    public async Task<IDbResponse<string>> SetDefaultImage(int productId, int imageId)
    {
        var found = await _context.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == productId);

        if (found is null)
            return DbResponse<string>.Failure(new ProductImageError().NotFound());

        foreach (var image in found.ProductImages)
        {
            if (image.Id == imageId)
                image.MainImage = true;
            else
                image.MainImage = false;
        }

        await _context.SaveChangesAsync();

        return DbResponse<string>.Success;
    }

    public async Task<IDbResponse<string>> DeleteImage(int productImageId)
    {
        var found = await _context.ProductImages.FindAsync(productImageId);

        if (found is null)
            return DbResponse<string>.Failure(new ProductImageError().NotFound());

        return DbResponse<string>.Success;
    }

    public async Task<ProductImage> GetImage(int imageId)
    {
        var found = await _context.ProductImages.SingleOrDefaultAsync(pi => pi.Id == imageId);

        if (found is null)
        {
            return new ProductImage();
        }

        return found;
    }
}
