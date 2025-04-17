using ps40165_User.Models;
using System.Net.Http.Json;

namespace ps40165_User.Services;

public class OrderService
{
    private readonly HttpClient _client;

    public OrderService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<Order>> GetListAsync(int currentPage, int pageSize)
    {
        try
        {
            var response = await _client.GetFromJsonAsync<Response<PaginatedList<Order>>>($"Orders?pageNumber={currentPage}&pageSize={pageSize}");

            if (response.IsSuccess)
            {
                if (response.Data is not null)
                    return response.Data.Items;
            }
        }
        catch
        {

        }
        return new List<Order>();
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
