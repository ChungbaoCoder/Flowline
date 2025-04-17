using ps40165_User.Models;
using System.Net.Http.Json;

namespace ps40165_User.Services;

public class CategoryService
{
    private readonly HttpClient _client;

    public CategoryService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<Category>> GetListAsync(int currentPage, int pageSize)
    {
        try
        {
            var response = await _client.GetFromJsonAsync<Response<PaginatedList<Category>>>($"Categories?pageNumber={currentPage}&pageSize={pageSize}");

            if (response.IsSuccess)
            {
                if (response.Data is not null)
                    return response.Data.Items;
            }
        }
        catch
        {

        }
        return new List<Category>();
    }
}
