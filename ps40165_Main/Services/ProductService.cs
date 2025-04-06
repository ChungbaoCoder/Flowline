using Microsoft.EntityFrameworkCore;
using ps40165_Main.Commands;
using ps40165_Main.Database;
using ps40165_Main.Database.DbResponse.ErrorDoc;
using ps40165_Main.Database.DbResponse;
using ps40165_Main.Dtos;
using ps40165_Main.Models;

namespace ps40165_Main.Services;

public class ProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IDbResponse> GetListProducts(QueryPageCommand request)
    {
        int pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
        int pageSize = request.PageSize < 1 ? 10 : request.PageSize;

        var products = await _context.Products
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new ProductDto
            {
                ProductId = c.Id,
                Name = c.Name,
                Description = c.Description,
                UnderDescription = c.UnderDescription,
                StockLevel = c.StockLevel,
                Price = c.Price,
                DisableBuyButton = c.DisableBuyButton
            })
            .ToListAsync();

        if (products.Count < 1)
        {
            return DbResponse.Failure(new ProductError().NotFound());
        }

        int totalCount = await _context.Products.CountAsync();
        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        PaginationMetadata meta = new PaginationMetadata
        {
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };

        return DbPagination<ProductDto>.GiveBack(meta, products);
    }

    public async Task<IDbResponse> GetProductById(int productId)
    {
        var result = await _context.Products.FindAsync(productId);

        if (result == null)
            return DbResponse.Failure(new ProductError().NotFound());

        ProductDto product = new ProductDto 
        { 
            Name = result.Name, 
            Description = result.Description,
            UnderDescription = result.UnderDescription,
            StockLevel = result.StockLevel,
            Price = result.Price
        };

        return DbQuery<ProductDto>.GiveBack(product);
    }

    public async Task<IDbResponse> AddProduct(AddProductCommand request)
    {
        Product product = new Product
        {
            CategoryId = request.CategoryId,
            Name = request.Name,
            Description = request.Description,
            UnderDescription = request.UnderDescription,
            StockLevel = request.StockLevel,
            Price = request.Price,
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return DbResponse.Success;
    }

    public async Task<IDbResponse> EditProduct(int productId, EditProductCommand request)
    {
        var found = await _context.Products.FindAsync(productId);

        if (found == null)
            return DbResponse.Failure(new ProductError().NotFound());

        found.CategoryId = request.CategoryId;
        found.Name = request.Name;
        found.Description = request.Description;
        found.UnderDescription = request.UnderDescription;
        found.StockLevel = request.StockLevel;
        found.Price = request.Price;

        return DbResponse.Success;
    }

    public async Task<IDbResponse> MakeActive(int productId)
    {
        var found = await _context.Products.FindAsync(productId);

        if (found == null)
            return DbResponse.Failure(new ProductError().NotFound());

        await _context.SaveChangesAsync();
        return DbResponse.Success;
    }

    public async Task<IDbResponse> MakeInactive(int productId)
    {
        var found = await _context.Products.FindAsync(productId);

        if (found == null)
            return DbResponse.Failure(new ProductError().NotFound());

        await _context.SaveChangesAsync();
        return DbResponse.Success;
    }

    public async Task<IDbResponse> DeleteProduct(int productId)
    {
        var found = await _context.Products.FindAsync(productId);

        if (found == null)
            return DbResponse.Failure(new ProductError().NotFound());

        _context.Products.Remove(found);

        await _context.SaveChangesAsync();
        return DbResponse.Success;
    }
}
