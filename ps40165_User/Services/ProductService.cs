using System.Net.Http.Json;
using ps40165_User.Models;

namespace ps40165_User.Services;

public class ProductService
{
    private readonly HttpClient _client;

    public ProductService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<Product>> GetListAsync(int currentPage, int pageSize)
    {
        try
        {
            var response = await _client.GetFromJsonAsync<Response<PaginatedList<Product>>>($"Products?pageNumber={currentPage}&pageSize={pageSize}");

            if (response.IsSuccess)
            {
                if (response.Data is not null)
                    return response.Data.Items;
            }
        }
        catch
        {
            
        }
        return new List<Product>();
    }
}
