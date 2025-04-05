using ps40165_Admin.Models;

namespace ps40165_Admin.Services;

public class CategoryService
{
    private readonly HttpClient _http;

    public CategoryService(HttpClient http)
    {
        _http = http;
    }

    public async Task<DbQuery<List<Category>>> GetListCategories()
    {
        var result = await _http.GetFromJsonAsync<DbQuery<List<Category>>>("api/Categories?PageNumber=1&PageSize=10");

        if (result == null && result.IsSuccess == false)
            return null;

        return result;
    }

    public async Task CreateCategory(Category data)
    {
        var response = await _http.PostAsJsonAsync("api/Categories", data);
    }

    public async Task EditCategory(Category data)
    {
        var response = await _http.PutAsJsonAsync($"api/Categories/{data.CategoryId}", data);
    }

    public async Task DeleteCategory(int categoryId)
    {
        var response = await _http.DeleteAsync($"api/Categories/{categoryId}");
    }
}
