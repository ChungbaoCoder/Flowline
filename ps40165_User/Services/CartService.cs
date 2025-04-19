using System.Text.Json;
using Microsoft.JSInterop;
using ps40165_User.Models;

namespace ps40165_User.Services;

public class CartService
{
    private readonly IJSRuntime _js;
    private const string CartKey = "cart";

    public CartService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<List<Product>> GetCartAsync()
    {
        var json = await _js.InvokeAsync<string>("cartStorage.getCart");

        return string.IsNullOrEmpty(json)
            ? new List<Product>()
            : JsonSerializer.Deserialize<List<Product>>(json);
    }

    public async Task SaveCartAsync(List<Product> cart)
    {
        var json = JsonSerializer.Serialize(cart);
        await _js.InvokeVoidAsync("cartStorage.setCart", json);
    }

    public async Task AddToCartAsync(Product newItem)
    {
        var cart = await GetCartAsync();

        var existing = cart.FirstOrDefault(i => i.ProductId == newItem.ProductId);

        if (existing != null)
        {
            existing.StockLevel += newItem.StockLevel;
        }
        else
        {
            cart.Add(newItem);
        }

        await SaveCartAsync(cart);
    }

    public async Task RemoveFromCartAsync(int id)
    {
        var cart = await GetCartAsync();

        var item = cart.FirstOrDefault(i => i.ProductId == id);

        if (item != null)
        {
            cart.Remove(item);
            await SaveCartAsync(cart);
        }
    }

    public async Task ClearCartAsync()
    {
        await _js.InvokeVoidAsync("cartStorage.clearCart");
    }
}
