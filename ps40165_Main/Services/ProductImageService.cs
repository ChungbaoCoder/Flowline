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

    public async Task<IDbResponse> AddImage(ProductImage image)
    {
        await _context.ProductImages.AddAsync(image);
        await _context.SaveChangesAsync();

        return DbResponse.Success;
    }

    public async Task<IDbResponse> EditImage(int productImageId)
    {
        var found = await _context.ProductImages.FindAsync(productImageId);

        if (found == null)
            return DbResponse.Failure(new ProductImageError().NotFound());


        await _context.SaveChangesAsync();

        return DbResponse.Success;
    }

    public async Task<IDbResponse> DeleteImage(int productImageId)
    {
        var found = await _context.ProductImages.FindAsync(productImageId);

        if (found == null)
            return DbResponse.Failure(new ProductImageError().NotFound());

        return DbResponse.Success;
    }
}
