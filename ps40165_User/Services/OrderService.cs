using ps40165_User.Models;
using ps40165_User.Requests;
using System.Net.Http.Json;

namespace ps40165_User.Services;

public class OrderService
{
    private readonly HttpClient _client;

    public OrderService(HttpClient client)
    {
        _client = client;
    }

    public async Task<Response<PaginatedList<Order>>> GetListAsync(int currentPage, int pageSize, string? searchText)
    {
        try
        {
            var response = await _client.GetFromJsonAsync<Response<PaginatedList<Order>>>($"Orders?pageNumber={currentPage}&pageSize={pageSize}&searchTerm={searchText}");
            return response;
        }
        catch
        {

        }
        return new Response<PaginatedList<Order>>();
    }

    public async Task<Response<Order>> MakeOrder(MakeOrderRequest request)
    {
        try
        {
            var response = await _client.PostAsJsonAsync($"Orders", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<Order>>();

                if (result is not null && result.IsSuccess)
                {
                    return result;
                }
            }
        }
        catch
        {

        }
        return new Response<Order>();
    }

    public async Task<Response<Order>> DeleteOrder(int orderId)
    {
        try
        {
            var response = await _client.DeleteFromJsonAsync<Response<Order>>($"Orders/{orderId}");

            if (response.IsSuccess)
            {
                if (response.Data is not null)
                    return response;
            }
        }
        catch
        {

        }
        return new Response<Order>();
    }
}
