using System.Net.Http.Json;
using ps40165_User.Models;

namespace ps40165_User.Services;

public class CategoryService
{
    private readonly HttpClient _client;

    public CategoryService(HttpClient client) => _client = client;

    public async Task<Response<List<Category>>> GetListCategories()
    {
        var response = await _client.GetAsync("/categories");

        if (response.IsSuccessStatusCode)
        {
            var product = await response.Content.ReadFromJsonAsync<Response<List<Category>>>();
            return product;
        }
        return new Response<List<Category>>();
    }
}
