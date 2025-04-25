using System.Net.Http.Json;
using ps40165_User.Models;

namespace ps40165_User.Services;

public class ProductService
{
    private readonly HttpClient _client;

    public ProductService(HttpClient client) => _client = client;

    public async Task<Response<List<Product>>> GetList(int currentPage, int pageSize, string? searchText)
    {
        var response = await _client.GetAsync($"Products");

        if (response.IsSuccessStatusCode)
        {
            var product = await response.Content.ReadFromJsonAsync<Response<List<Product>>>();
            return product;
        }
        return new Response<List<Product>>();
    }

    public async Task<Response<Product>> GetById(int productId)
    {
        var response = await _client.GetAsync($"Products/{productId}");

        if (response.IsSuccessStatusCode)
        {
            var product = await response.Content.ReadFromJsonAsync<Response<Product>>();
            return product;
        }
        return new Response<Product>();
    }

    public async Task<Response<Product>> GetDetail(int productId)
    {
        var response = await _client.GetAsync($"Products/{productId}/detail");

        if (response.IsSuccessStatusCode)
        {
            var product = await response.Content.ReadFromJsonAsync<Response<Product>>();
            return product;
        }
        return new Response<Product>();
    }
}
