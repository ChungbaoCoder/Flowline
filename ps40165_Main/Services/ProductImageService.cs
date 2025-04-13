using Microsoft.EntityFrameworkCore;
using ps40165_Main.Database;
using ps40165_Main.Database.DbResponse;
using ps40165_Main.Database.DbResponse.ErrorDoc;
using ps40165_Main.Models;

namespace ps40165_Main.Services;

public class ProductImageService
{
    private readonly AppDbContext _context;

    public ProductImageService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IDbResponse<string>> AddImage(ProductImage image)
    {
        await _context.ProductImages.AddAsync(image);
        await _context.SaveChangesAsync();

        return DbResponse<string>.Success;
    }

    public async Task<IDbResponse<string>> EditImage(int productImageId)
    {
        var found = await _context.ProductImages.FindAsync(productImageId);

        if (found is null)
            return DbResponse<string>.Failure(new ProductImageError().NotFound());

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

    public async Task<string> GetMainImage(int productId)
    {
        var found = await _context.ProductImages.SingleOrDefaultAsync(pi => pi.ProductId == productId);

        if (found is not null)
        {
            return found.ImagePath ?? "No Image";
        }

        return "No Image";
    }
}
