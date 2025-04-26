using System.Net.Http.Json;
using ps40165_User.Models;

namespace ps40165_User.Services;

public class ProductService
{
    private readonly HttpClient _client;

    public ProductService(HttpClient client) => _client = client;

    public async Task<Response<List<Product>>> GetList()
    {
        try
        {
            var response = await _client.GetAsync($"/api/products");

            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadFromJsonAsync<Response<List<Product>>>();
                return product;
            }
            else
            {
                Console.WriteLine($"Server returned error: {(int)response.StatusCode} {response.ReasonPhrase}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Network or connection error: {ex.Message}");
        }
        return new Response<List<Product>>();
    }

    public async Task<Response<Product>> GetById(int productId)
    {
        try
        {
            var response = await _client.GetAsync($"/api/products/{productId}");

            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadFromJsonAsync<Response<Product>>();
                return product;
            }
            else
            {
                Console.WriteLine($"Server returned error: {(int)response.StatusCode} {response.ReasonPhrase}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Network or connection error: {ex.Message}");
        }
        return new Response<Product>();
    }

    public async Task<Response<Product>> GetDetail(int productId)
    {
        try
        {
            var response = await _client.GetAsync($"/api/products/{productId}/detail");

            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadFromJsonAsync<Response<Product>>();
                return product;
            }
            else
            {
                Console.WriteLine($"Server returned error: {(int)response.StatusCode} {response.ReasonPhrase}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Network or connection error: {ex.Message}");
        }
        return new Response<Product>();
    }

    public async Task<Response<PaginatedList<Product>>> GetPagination(int currentPage, int pageSize, string? searchText)
    {
        try
        {
            var response = await _client.GetAsync($"/api/products/paginate?pageNumber={currentPage}&pageSize={pageSize}&searchText={searchText}");

            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadFromJsonAsync<Response<PaginatedList<Product>>>();
                return product;
            }
            else
            {
                Console.WriteLine($"Server returned error: {(int)response.StatusCode} {response.ReasonPhrase}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Network or connection error: {ex.Message}");
        }
        return new Response<PaginatedList<Product>>();
    }
}
