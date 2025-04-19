using System.Net.Http.Json;
using ps40165_User.Models;
using ps40165_User.Requests;

namespace ps40165_User.Services;

public class ProductService
{
    private readonly HttpClient _client;

    public ProductService(HttpClient client)
    {
        _client = client;
    }

    public async Task<Response<PaginatedList<Product>>> GetListAsync(int currentPage, int pageSize, string? searchText)
    {
        try
        {
            var response = await _client.GetFromJsonAsync<Response<PaginatedList<Product>>>($"Products?pageNumber={currentPage}&pageSize={pageSize}&searchTerm={searchText}");
            return response;
        }
        catch
        {
            
        }
        return new Response<PaginatedList<Product>>();
    }

    public async Task<Response<Product>> GetById(int productId)
    {
        try
        {
            var response = await _client.GetFromJsonAsync<Response<Product>>($"Products/{productId}");
            return response;
        }
        catch
        {

        }
        return new Response<Product>();
    }

    public async Task PostProduct(AddProductRequest request)
    {
        try
        {
            var response = await _client.PostAsJsonAsync($"Products", request);
        }
        catch
        {

        }
    }

    public async Task<Response<Product>> PutProduct(int productId, EditProductRequest request)
    {
        try
        {
            var response = await _client.PutAsJsonAsync($"Products/{productId}", request);

            if (response.IsSuccessStatusCode)
            {
                var editProduct = await response.Content.ReadFromJsonAsync<Response<Product>>();
                return editProduct;
            }
        }
        catch
        {

        }
        return new Response<Product>();
    }

    public async Task<Response<string>> DeleteProduct(int productId)
    {
        try
        {
            var response = await _client.DeleteFromJsonAsync<Response<string>>($"Products/{productId}");
            return response;
        }
        catch
        {

        }
        return new Response<string>();
    }
}
