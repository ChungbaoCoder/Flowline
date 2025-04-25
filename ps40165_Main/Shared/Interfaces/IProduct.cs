using ps40165_Main.Dtos;
using ps40165_Main.Models;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Shared.Interfaces;

public interface IProduct
{
    public Task<Result<List<Product>>> GetListProducts();

    public Task<Result<Product>> GetProductById(int productId);

    public Task<Result<Product>> CreateProduct(Product product);

    public Task<Result<Product>> UpdateProduct(int productId, Product product);

    public Task<Result<Product>> DeleteProduct(int productId);

    public Task<Result<ProductDto>> GetDetail(int productId);
}
