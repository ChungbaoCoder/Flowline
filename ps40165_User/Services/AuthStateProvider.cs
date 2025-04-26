using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace ps40165_User.Services;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _js;
    private readonly HttpClient _client;
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
    private const string TokenKey = "authToken";

    private readonly JwtSecurityTokenHandler _jwtHandler = new JwtSecurityTokenHandler();

    public AuthStateProvider(IJSRuntime js, HttpClient client)
    {
        _js = js;
        _client = client;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await GetTokenAsync();

        if (string.IsNullOrWhiteSpace(token))
        {
            // No token, user is anonymous
            return new AuthenticationState(_anonymous);
        }

        // Token exists, create the ClaimsPrincipal from it
        var claimsPrincipal = CreateClaimsPrincipalFromToken(token);

        // Set default authorization header for subsequent HttpClient requests
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return new AuthenticationState(claimsPrincipal);
    }

    // Called when the user logs in successfully
    public async Task MarkUserAsAuthenticated(string token)
    {
        await SetTokenAsync(token); // Store the token
        var claimsPrincipal = CreateClaimsPrincipalFromToken(token); // Create principal from new token
                                                                     // Notify Blazor that the authentication state has changed
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    // Called when the user logs out
    public async Task MarkUserAsLoggedOut()
    {
        await RemoveTokenAsync(); // Remove the token from storage
        _client.DefaultRequestHeaders.Authorization = null; // Clear default auth header
                                                            // Notify Blazor that the authentication state has changed (user is now anonymous)
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }

    // --- Uses System.IdentityModel.Tokens.Jwt ---
    private ClaimsPrincipal CreateClaimsPrincipalFromToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return _anonymous; // Return anonymous if token is empty
        }

        try
        {
            // Use the library handler to read the token
            // Note: ReadJwtToken just parses, it DOES NOT VALIDATE the signature or expiry.
            // Validation (signature, expiry, issuer, audience) MUST happen on your API server.
            var jwtToken = _jwtHandler.ReadJwtToken(token);

            // Check if token was parsed and has claims
            if (jwtToken == null || !jwtToken.Claims.Any())
            {
                Console.WriteLine("Invalid JWT token or no claims found.");
                return _anonymous;
            }

            // The library automatically extracts claims (including roles)
            // and maps common JWT claim types (like 'sub', 'name', 'role')
            // to the standard .NET ClaimTypes (like NameIdentifier, Name, Role).
            var identity = new ClaimsIdentity(jwtToken.Claims, "jwtAuthType"); // "jwtAuthType" is the authentication scheme name

            return new ClaimsPrincipal(identity);
        }
        catch (Exception ex)
        {
            // Handle exceptions if the token is malformed
            Console.WriteLine($"Error reading JWT token: {ex.Message}");
            return _anonymous; // Return anonymous if token is invalid
        }
    }
    // --- End JWT Library Usage ---


    // Helper methods for LocalStorage (these remain the same)
    private async Task SetTokenAsync(string token)
    {
        await _js.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
    }

    private async Task<string?> GetTokenAsync()
    {
        try
        {
            return await _js.InvokeAsync<string?>("localStorage.getItem", TokenKey);
        }
        catch
        {
            // Handle cases where localStorage might not be available (e.g., prerendering)
            return null;
        }
    }

    private async Task RemoveTokenAsync()
    {
        try
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        }
        catch
        {
            // Handle cases where localStorage might not be available
        }
    }
}
