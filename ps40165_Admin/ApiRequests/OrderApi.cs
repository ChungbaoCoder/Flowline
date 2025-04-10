using System.Text.Json;
using ps40165_Admin.Models;
using ps40165_Main.Commands;

namespace ps40165_Admin.ApiRequests;

public class OrderApi
{
    private readonly HttpClient _client;

    public OrderApi(HttpClient client)
    {
        _client = client;
    }

    public async Task<Response<List<OrderDto>>> GetList(int currentPage, int pageSize)
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync($"Orders?pageNumber={currentPage}&pageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                Response<List<OrderDto>> res = await response.Content.ReadFromJsonAsync<Response<List<OrderDto>>>();

                if (res.IsSuccess)
                {
                    return res;
                }
            }
        }
        catch
        {

        }
        return new Response<List<OrderDto>>();
    }

    public async Task MakeOrder(MakeOrderCommand request)
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(request);

            HttpContent content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

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
