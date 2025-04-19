using System.Net.Http.Json;
using ps40165_User.Models;

namespace ps40165_User.Services;

public class ProductImageService
{
    private readonly HttpClient _client;

    public ProductImageService(HttpClient client)
    {
        _client = client;
    }

    public async Task<Response<List<ProductImage>>> GetListAsync(int productId)
    {
        try
        {
            var response = await _client.GetFromJsonAsync<Response<List<ProductImage>>>($"Images/product/{productId}");
            return response;
        }
        catch
        {

        }
        return new Response<List<ProductImage>>();
    }

    public async Task<Response<List<ProductImage>>> PostImage(MultipartFormDataContent content)
    {
        try
        {
            var response = await _client.PostAsync($"Images", content);

            if (response.IsSuccessStatusCode)
            {
                var listImages = await response.Content.ReadFromJsonAsync<Response<List<ProductImage>>>();
                return listImages;
            }
        }
        catch
        {

        }
        return new Response<List<ProductImage>>();
    }
}
