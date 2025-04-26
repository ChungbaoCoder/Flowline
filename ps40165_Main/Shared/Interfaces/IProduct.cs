using ps40165_Main.Dtos.GetDto;
using ps40165_Main.Dtos.PostDto;
using ps40165_Main.Dtos.PutDto;
using ps40165_Main.Models;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Shared.Interfaces;

public interface IProduct
{
    Task<Result<List<Product>>> GetListProducts();

    Task<Result<Product>> GetProductById(int productId);

    Task<Result<Product>> CreateProduct(CreateProductDto product);

    Task<Result<Product>> UpdateProduct(int productId, EditProductDto product);

    Task<Result<Product>> DeleteProduct(int productId);

    Task<Result<ProductDto>> GetDetail(int productId);

    Task<Result<PaginatedList<Product>>> GetPagination(int pageNumber, int pageSize, string? searchText);
}
