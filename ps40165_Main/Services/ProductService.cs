using Microsoft.EntityFrameworkCore;
using ps40165_Main.Commands;
using ps40165_Main.Database;
using ps40165_Main.Database.DbResponse.ErrorDoc;
using ps40165_Main.Database.DbResponse;
using ps40165_Main.Dtos;
using ps40165_Main.Models;
using ps40165_Main.Mapper.ModelToDto;

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
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .Select(p => new Product 
                { 
                    Id = p.Id,
                    SKU = p.SKU,
                    Name = p.Name,
                    Description = p.Description,
                    UnderDescription = p.UnderDescription,
                    StockLevel = p.StockLevel,
                    Price = p.Price,
                    DisableBuyButton = p.DisableBuyButton,
                    Category = p.Category,
                    ProductImages = p.ProductImages
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

        List<ProductDto> data = new List<ProductDto>();

        foreach (var product in products)
            data.Add(new ProductMapper().Map(product));

        return DbPagination<ProductDto>.GiveBack(meta, data);
    }

    public async Task<IDbResponse> GetProductById(int productId)
    {
        var result = await _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .Where(p => p.Id == productId)
            .Select(p => new Product 
                {
                    Id = p.Id,
                    SKU = p.SKU,
                    Name = p.Name,
                    Description = p.Description,
                    UnderDescription = p.UnderDescription,
                    StockLevel = p.StockLevel,
                    Price = p.Price,
                    DisableBuyButton = p.DisableBuyButton,
                    Category = p.Category,
                    ProductImages = p.ProductImages
                })
            .FirstOrDefaultAsync();

        if (result == null)
            return DbResponse.Failure(new ProductError().NotFound());

        ProductDto product = new ProductMapper().Map(result);

        return DbQuery<ProductDto>.GiveBack(product);
    }

    public async Task<IDbResponse> AddProduct(AddProductCommand request)
    {
        Product product = new Product
        {
<<<<<<< HEAD
            CategoryId = request.CategoryId,
=======
            SKU = request.SKU,
>>>>>>> 2b0e6ac (Mass update models, add Order, OrderItem, Account model and fix some services. Add hand mapper)
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

<<<<<<< HEAD
        found.CategoryId = request.CategoryId;
=======
        found.SKU = request.SKU;
>>>>>>> 2b0e6ac (Mass update models, add Order, OrderItem, Account model and fix some services. Add hand mapper)
        found.Name = request.Name;
        found.Description = request.Description;
        found.UnderDescription = request.UnderDescription;
        found.StockLevel = request.StockLevel;
        found.Price = request.Price;

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
