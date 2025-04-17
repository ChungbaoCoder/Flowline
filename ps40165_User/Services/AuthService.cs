using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using ps40165_User.Models;
using ps40165_User.Requests;

namespace ps40165_User.Services;

public class AuthService
{
    private readonly HttpClient _client;
    private readonly AuthenticationStateProvider _auth;

    public AuthService(HttpClient client, AuthenticationStateProvider auth)
    {
        _client = client;
        _auth = auth;
    }

    public async Task<Response<JwtToken>> LoginUser(string route, LoginRequest request)
    {
        try
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync(route, request);

            if (response.IsSuccessStatusCode)
            {
                Response<JwtToken> loginResult = await response.Content.ReadFromJsonAsync<Response<JwtToken>>() ?? new Response<JwtToken>();

                if (loginResult.IsSuccess)
                {
                    var saveToken = (JwtAuthStateProvider)_auth;
                    await saveToken.MarkUserAsAuthenticated(loginResult.Data.Token);
                    return loginResult;
                }
                else
                    return loginResult;
            }
        }
        catch
        {

        }
        return new Response<JwtToken>();
    }
}
