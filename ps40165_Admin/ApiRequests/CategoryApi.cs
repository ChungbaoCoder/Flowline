using System.Text.Json;
using System.Text;
using ps40165_Admin.Models;
using ps40165_Admin.Dtos;

namespace ps40165_Admin.ApiRequests;

public class CategoryApi
{
    private readonly HttpClient _client;

    public CategoryApi(HttpClient client)
    {
        _client = client;
    }

    public async Task<Response<PaginatedList<Category>>> GetList(int currentPage, int pageSize, string searchText)
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync($"categories/paginate?pageNumber={currentPage}&pageSize={pageSize}&searchText={searchText}");

            if (response.IsSuccessStatusCode)
            {
                Response<PaginatedList<Category>> res = await response.Content.ReadFromJsonAsync<Response<PaginatedList<Category>>>();

                return res;
            }
        }
        catch
        {

        }
        return new Response<PaginatedList<Category>>();
    }

    public async Task AddCategory(CreateCategoryDto request)
    {
        string jsonString = JsonSerializer.Serialize(request);

        HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PostAsync("categories", content);

        if (response.IsSuccessStatusCode)
        {

        }
    }

    public async Task EditCategory(int categoryId, EditCategoryDto request)
    {
        string jsonString = JsonSerializer.Serialize(request);

        HttpContent content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PutAsync($"categories/{categoryId}", content);

        if (response.IsSuccessStatusCode)
        {

        }
    }

    public async Task DeleteCategory(int categoryId)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"categories/{categoryId}");

        if (response.IsSuccessStatusCode)
        {

        }
    }
}
