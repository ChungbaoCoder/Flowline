using Microsoft.EntityFrameworkCore;
using ps40165_Main.Database;
using ps40165_Main.Dtos.GetDto;
using ps40165_Main.Dtos.PostDto;
using ps40165_Main.Dtos.PutDto;
using ps40165_Main.Models;
using ps40165_Main.Shared;
using ps40165_Main.Shared.Interfaces;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Features.ProductFeature;

public class ProductService : IProduct
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context) => _context = context;

    public async Task<Result<List<Product>>> GetListProducts()
    {
        var products = await _context.Products
            .AsNoTracking()
            .ToListAsync();

        if (products.Count < 1)
            return Result<List<Product>>.Fail("Không có sản phẩm trong danh sách");

        return Result<List<Product>>.Ok(products, "Lấy danh sách sản phẩm thành công");
    }

    public async Task<Result<Product>> GetProductById(int productId)
    {
        var pro = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == productId);

        if (pro is null)
            return Result<Product>.Fail($"Không tìm thấy sản phẩm có mã id {productId}");

        return Result<Product>.Ok(pro, $"Tìm thấy sản phẩm có mã id {productId}");
    }

    public async Task<Result<Product>> CreateProduct(CreateProductDto product)
    {
        var pro = new Product();
        Result<Product> result = pro.Create(product.CategoryId).Bind(r => pro.UpdateName(product.Name))
            .Bind(r => pro.UpdateDescription(product.Description))
            .Bind(r => pro.UpdatePrice(product.Price));

        if (result.IsSuccess)
        {
            await _context.Products.AddAsync(pro);
            await _context.SaveChangesAsync();
            result = Result<Product>.Ok("Thêm sản phẩm thành công");
        }

        return result;
    }

    public async Task<Result<Product>> UpdateProduct(int productId, EditProductDto product)
    {
        var pro = await _context.Products.FindAsync(productId);

        if (pro is null)
            return Result<Product>.Fail($"Không tìm thấy sản phẩm có mã id {productId}");

        Result<Product> result = pro.UpdateName(product.Name)
            .Bind(r => pro.UpdateDescription(product.Description))
            .Bind(r => pro.UpdatePrice(product.Price));

        if (result.IsSuccess)
        {
            await _context.SaveChangesAsync();
            result = Result<Product>.Ok($"Cập nhật sản phẩm có mã id {productId} thành công");
        }

        return result;
    }

    public async Task<Result<Product>> DeleteProduct(int productId)
    {
        var pro = await _context.Products.FindAsync(productId);

        if (pro is null)
            return Result<Product>.Fail($"Không tìm thấy sản phẩm có mã id {productId}");

        _context.Products.Remove(pro);
        await _context.SaveChangesAsync();
        return Result<Product>.Ok($"Xóa sản phẩm có mã id {productId} thành công");
    }

    public async Task<Result<ProductDto>> GetDetail(int productId)
    {
        var proDetail = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == productId);

        if (proDetail is null)
            return Result<ProductDto>.Fail($"Không tìm thấy sản phẩm có mã id {productId}");

        ProductDto dto = MapToDto.Map(proDetail);

        return Result<ProductDto>.Ok(dto, $"Lấy chi tiết sản phẩm có id {productId} thành công");
    }

    public async Task<Result<PaginatedList<Product>>> GetPagination(int pageNumber, int pageSize, string? searchText)
    {
        var products = await _context.Products
            .AsNoTracking()
            .OrderBy(c => c.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        if (products.Count < 1)
            return Result<PaginatedList<Product>>.Fail("Không có sản phẩm trong danh sách");

        int totalCount = await _context.Products.CountAsync();
        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        PaginatedList<Product> list = new PaginatedList<Product>(products, pageNumber, pageSize, totalCount);
        return Result<PaginatedList<Product>>.Ok(list, "Lấy danh sách sản phẩm thành công");
    }
}
