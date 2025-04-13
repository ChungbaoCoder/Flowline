using System.Text;
using System.Text.Json;
using ps40165_Admin.Commands;
using ps40165_Admin.Models;

namespace ps40165_Admin.ApiRequests;

public class AccountApi
{
    private readonly HttpClient _client;

    public AccountApi(HttpClient client)
    {
        _client = client;
    }

    public async Task<Response<List<AccountDto>>> GetList(int currentPage, int pageSize)
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync($"Accounts?pageNumber={currentPage}&pageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                Response<List<AccountDto>> res = await response.Content.ReadFromJsonAsync<Response<List<AccountDto>>>();

                if (res.IsSuccess)
                {
                    return res;
                }
            }
        }
        catch
        {

        }
        return new Response<List<AccountDto>>();
    }

    public async Task AddAcount(RegisterUserCommand request)
    {
        string jsonString = JsonSerializer.Serialize(request);

        HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _client.PostAsync("Accounts", content);

        if (response.IsSuccessStatusCode)
        {

        }
    }
}
