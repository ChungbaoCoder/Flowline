using ps40165_Admin.Dtos;
using ps40165_Admin.Models;

namespace ps40165_Admin.Services;

public class ProductService
{
    private readonly HttpClient _http;

    public ProductService(HttpClient http)
    {
        _http = http;
    }

    public async Task<DbQuery<List<Product>>> GetListProducts()
    {
        var result = await _http.GetFromJsonAsync<DbQuery<List<Product>>>("api/Products?PageNumber=1&PageSize=10");

        if (result == null && result.IsSuccess == false)
            return null;

        return result;
    }

    public async Task<DbQuery<Product>> GetProductById(int productId)
    {
        var result = await _http.GetFromJsonAsync<DbQuery<Product>>($"api/Products/{productId}");

        if (result == null && result.IsSuccess == false)
            return null;

        return result;
    }

    public async Task CreateProduct(PostProduct data)
    {
        var response = await _http.PostAsJsonAsync("api/Products", data);
    }

    public async Task EditProduct(int productId, PutProduct data)
    {
        var response = await _http.PutAsJsonAsync($"api/Products/{productId}", data);
    }

    public async Task DeleteProduct(int productId)
    {
        var response = await _http.DeleteAsync($"api/Products/{productId}");
    }
}
