using System.Text.Json;
using System.Text;
using ps40165_Admin.Models;
using ps40165_Admin.Dtos.PostDto;

namespace ps40165_Admin.ApiRequests;

public class AccountApi
{
    private readonly HttpClient _client;

    public AccountApi(HttpClient client)
    {
        _client = client;
    }

    public async Task<Response<PaginatedList<Account>>> GetList(int currentPage, int pageSize, string? searchText)
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync($"accounts?pageNumber={currentPage}&pageSize={pageSize}&searchText={searchText}");

            if (response.IsSuccessStatusCode)
            {
                Response<PaginatedList<Account>> res = await response.Content.ReadFromJsonAsync<Response<PaginatedList<Account>>>();

                return res;
            }
        }
        catch
        {

        }
        return new Response<PaginatedList<Account>>();
    }

    public async Task AddAcount(RegisterDto request)
    {
        try
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"/api/accounts/register", content);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Network or connection error: {ex.Message}");
        }
    }
}
