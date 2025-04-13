using System.Text.Json;
using ps40165_Admin.Models;
using ps40165_Admin.Commands;
using System.Text;

namespace ps40165_Admin.ApiRequests;

public class ProductApi
{
    private readonly HttpClient _client;

    public ProductApi(HttpClient client)
    {
        _client = client;
    }

    public async Task<Response<List<ProductDto>>> GetList(int currentPage, int pageSize)
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync($"Products?pageNumber={currentPage}&pageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                Response<List<ProductDto>> res = await response.Content.ReadFromJsonAsync<Response<List<ProductDto>>>();

                if (res.IsSuccess)
                {
                    return res;
                }
            }
        }
        catch
        {

        }
        return new Response<List<ProductDto>>();
    }

    public async Task AddProduct(AddProductCommand request)
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(request);

            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("Products", content);

            if (response.IsSuccessStatusCode)
            {

            }
        }
        catch
        {

        }
    }

    public async Task EditProduct(int productId, EditProductCommand request)
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(request);

            HttpContent content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync($"Products/{productId}", content);

            if (response.IsSuccessStatusCode)
            {

            }
        }
        catch
        {

        }
    }

    public async Task DeleteProduct(int productId)
    {
        try
        {
            HttpResponseMessage response = await _client.DeleteAsync($"Products/{productId}");

            if (response.IsSuccessStatusCode)
            {

            }
        }
        catch
        {

        }
    }
}
