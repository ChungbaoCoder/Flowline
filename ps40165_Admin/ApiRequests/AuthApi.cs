using System.Text;
using System.Text.Json;
using ps40165_Admin.Commands;
using ps40165_Admin.Models;

namespace ps40165_Admin.ApiRequests;

public class AuthApi
{
    private readonly HttpClient _client;

    public AuthApi(HttpClient client)
    {
        _client = client;
    }

    public async Task<Response<TokenDto>> Login(LoginUserCommand request)
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(request);

            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("Auth/employee", content);

            if (response.IsSuccessStatusCode)
            {
                Response<TokenDto> res = await response.Content.ReadFromJsonAsync<Response<TokenDto>>();

                if (res.IsSuccess)
                {
                    return res;
                }
            }
        }
        catch
        {

        }
        return new Response<TokenDto>();
    }
}
