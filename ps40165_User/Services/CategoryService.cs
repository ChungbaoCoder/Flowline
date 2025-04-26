using System.Net.Http.Json;
using ps40165_User.Models;

namespace ps40165_User.Services;

public class CategoryService
{
    private readonly HttpClient _client;

    public CategoryService(HttpClient client) => _client = client;

    public async Task<Response<List<Category>>> GetList(int currentPage, int pageSize, string? searchText)
    {
        try
        {
            var response = await _client.GetAsync("/api/categories");

            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadFromJsonAsync<Response<List<Category>>>();
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
        return new Response<List<Category>>();
    }
}
