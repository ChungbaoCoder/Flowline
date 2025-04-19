using Microsoft.EntityFrameworkCore;
using ps40165_Main.Commands;
using ps40165_Main.Database;
using ps40165_Main.Database.DbResponse;
using ps40165_Main.Database.DbResponse.ErrorDoc;
using ps40165_Main.Dtos;
using ps40165_Main.Mapper.ModelToDto;
using ps40165_Main.Models;

namespace ps40165_Main.Services;

public class ProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IDbResponse<PaginatedList<ProductDto>>> GetListProducts(QueryPageCommand request)
    {
        int pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
        int pageSize = request.PageSize < 1 ? 10 : request.PageSize;

        var query = _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var keyword = $"%{request.SearchTerm?.Trim()}%";

            query = query.Where(p =>
                EF.Functions.Like(p.Name, keyword) ||
                EF.Functions.Like(p.Description, keyword) ||
                EF.Functions.Like(p.Category.Name, keyword));
        }

        var products = await query
            .OrderBy(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new Product
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
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
            return DbResponse<PaginatedList<ProductDto>>.Failure(new ProductError().NotFound());
        }

        int totalCount = await query.CountAsync();
        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        List<ProductDto> data = new List<ProductDto>();

        foreach (Product product in products)
            data.Add(new ProductMapper().Map(product));

        PaginatedList<ProductDto> meta = new PaginatedList<ProductDto>(data, pageNumber, pageSize, totalCount);

        return DbResponse<PaginatedList<ProductDto>>.GiveBack(meta);
    }

    public async Task<IDbResponse<ProductDto>> GetProductById(int productId)
    {
        var result = await _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .Where(p => p.Id == productId)
            .Select(p => new Product
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
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

        if (result is null)
            return DbResponse<ProductDto>.Failure(new ProductError().NotFound());

        ProductDto data = new ProductMapper().Map(result);

        return DbResponse<ProductDto>.GiveBack(data);
    }

    public async Task<IDbResponse<ProductDto>> AddProduct(AddProductCommand request)
    {
        Product product = new Product
        {
            CategoryId = request.CategoryId,
            SKU = request.SKU,
            Name = request.Name,
            Description = request.Description,
            UnderDescription = request.UnderDescription,
            StockLevel = request.StockLevel,
            Price = request.Price,
            DisableBuyButton = request.DisableBuyButton,
            ProductImages = request.ProductImages   
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        ProductDto data = new ProductMapper().Map(product);

        return DbResponse<ProductDto>.GiveBack(data);
    }

    public async Task<IDbResponse<ProductDto>> EditProduct(int productId, EditProductCommand request)
    {
        var found = await _context.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == productId);

        if (found is null)
            return DbResponse<ProductDto>.Failure(new ProductError().NotFound());

        found.CategoryId = request.CategoryId;
        found.SKU = request.SKU;
        found.Name = request.Name;
        found.Description = request.Description;
        found.UnderDescription = request.UnderDescription;
        found.StockLevel = request.StockLevel;
        found.Price = request.Price;
        found.DisableBuyButton = request.DisableBuyButton;

        await _context.SaveChangesAsync();

        ProductDto data = new ProductMapper().Map(found);

        return DbResponse<ProductDto>.GiveBack(data);
    }

    public async Task<IDbResponse<string>> DeleteProduct(int productId)
    {
        var found = await _context.Products.FindAsync(productId);

        if (found == null)
            return DbResponse<string>.Failure(new ProductError().NotFound());

        _context.Products.Remove(found);

        await _context.SaveChangesAsync();

        return DbResponse<string>.GiveBack($"Sản phẩm với mã số {productId} đã được xóa đi");
    }
}
