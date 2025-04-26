using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using ps40165_User.Models;
using ps40165_User.Shared;

namespace ps40165_User.Services;

public class UserAccountService
{
    private readonly HttpClient _client;

    public UserAccountService(HttpClient client) => _client = client;

    public async Task<Response<Token>> RegisterAccount(RegisterUser user)
    {
        try
        {
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"/api/account/register", content);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadFromJsonAsync<Response<Token>>();
                return token;
            }
            else
            {
                Console.WriteLine($"Server returned error: {(int)response.StatusCode} {response.ReasonPhrase}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Network or connection error: {ex.Message}");
        }
        return new Response<Token>();
    }

    public async Task<Response<Token>> LoginAccount(LoginUser user)
    {
        try
        {
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"/api/account/login", content);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadFromJsonAsync<Response<Token>>();
                return token;
            }
            else
            {
                Console.WriteLine($"Server returned error: {(int)response.StatusCode} {response.ReasonPhrase}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Network or connection error: {ex.Message}");
        }
        return new Response<Token>();
    }
}
