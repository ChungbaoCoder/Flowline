using ps40165_User.Models;
using ps40165_User.Requests;
using System.Net.Http.Json;

namespace ps40165_User.Services;

public class CategoryService
{
    private readonly HttpClient _client;

    public CategoryService(HttpClient client)
    {
        _client = client;
    }

    public async Task<Response<PaginatedList<Category>>> GetListAsync(int currentPage, int pageSize, string? searchText)
    {
        try
        {
            var response = await _client.GetFromJsonAsync<Response<PaginatedList<Category>>>($"Categories?pageNumber={currentPage}&pageSize={pageSize}&searchTerm={searchText}");
            return response;
        }
        catch
        {

        }
        return new Response<PaginatedList<Category>>();
    }

    public async Task<Response<Category>> GetById(int categoryId)
    {
        try
        {
            var response = await _client.GetFromJsonAsync<Response<Category>>($"Categories/{categoryId}");
            return response;
        }
        catch
        {

        }
        return new Response<Category>();
    }

    public async Task PostCategory(AddCategoryRequest request)
    {
        try
        {
            var response = await _client.PostAsJsonAsync($"Categorys", request);
        }
        catch
        {

        }
    }

    public async Task<Response<Category>> PutCategory(int categoryId, EditCategoryRequest request)
    {
        try
        {
            var response = await _client.PutAsJsonAsync($"Categorys/{categoryId}", request);

            if (response.IsSuccessStatusCode)
            {
                var editCategory = await response.Content.ReadFromJsonAsync<Response<Category>>();
                return editCategory;
            }
        }
        catch
        {

        }
        return new Response<Category>();
    }

    public async Task<Response<string>> DeleteCategory(int categoryId)
    {
        try
        {
            var response = await _client.DeleteFromJsonAsync<Response<string>>($"Categorys/{categoryId}");
            return response;
        }
        catch
        {

        }
        return new Response<string>();
    }
}
