using System.Text.Json;
using System.Text;
using ps40165_Admin.Models;
using ps40165_Admin.Dtos;

namespace ps40165_Admin.ApiRequests;

public class OrderApi
{
    private readonly HttpClient _client;

    public OrderApi(HttpClient client)
    {
        _client = client;
    }

    public async Task<Response<PaginatedList<Order>>> GetList(int currentPage, int pageSize)
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync($"Orders?pageNumber={currentPage}&pageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                Response<PaginatedList<Order>> res = await response.Content.ReadFromJsonAsync<Response<PaginatedList<Order>>>();

                return res;
            }
        }
        catch
        {

        }
        return new Response<PaginatedList<Order>>();
    }

    public async Task MakeOrder(CreateOrderDto request)
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(request);

            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("Orders", content);

            if (response.IsSuccessStatusCode)
            {

            }
        }
        catch
        {

        }
    }
}
