using ps40165_User.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace ps40165_User.Services;

public class OrderService
{
    private readonly HttpClient _client;

    public OrderService(HttpClient client) => _client = client;

    public async Task<Response<Order>> CreateOrder(Order order)
    {
        try
        {
            var json = JsonSerializer.Serialize(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"/api/orders/", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<Order>>();
                return result;
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
        return new Response<Order>();
    }
}
