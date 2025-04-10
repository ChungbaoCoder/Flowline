using System.Text.Json;
using ps40165_Admin.Models;
using ps40165_Admin.Commands;

namespace ps40165_Admin.ApiRequests;

public class CategoryApi
{
    private readonly HttpClient _client;

    public CategoryApi(HttpClient client)
    {
        _client = client;
    }

    public async Task<Response<List<CategoryDto>>> GetList(int currentPage, int pageSize)
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync($"Categories?pageNumber={currentPage}&pageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                Response<List<CategoryDto>> res = await response.Content.ReadFromJsonAsync<Response<List<CategoryDto>>>();

                if (res.IsSuccess)
                {
                    return res;
                }
            }
        }
        catch
        {

        }
        return new Response<List<CategoryDto>>();
    }

    public async Task AddCategory(AddCategoryCommand request)
    {
        string jsonString = JsonSerializer.Serialize(request);

        HttpContent content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PostAsync("Categories", content);

        if (response.IsSuccessStatusCode)
        {

        }
    }

    public async Task EditCategory(int categoryId, EditCategoryCommand request)
    {
        string jsonString = JsonSerializer.Serialize(request);

        HttpContent content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PutAsync($"Categories/{categoryId}", content);

        if (response.IsSuccessStatusCode)
        {

        }
    }

    public async Task DeleteCategory(int categoryId)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"Categories/{categoryId}");

        if (response.IsSuccessStatusCode)
        {

        }
    }
}
