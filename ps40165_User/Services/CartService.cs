using Microsoft.JSInterop;
using ps40165_User.Models;
using System.Text.Json;

namespace ps40165_User.Services;

public class CartService
{
    private readonly IJSRuntime _js;

    public event Action? OnChange;

    public CartService(IJSRuntime js) => _js = js;

    private void NotifyStateChanged() => OnChange?.Invoke();

    public async Task<List<Product>> GetCartAsync()
    {
        var json = await _js.InvokeAsync<string>("cartStorage.getCart");

        return string.IsNullOrEmpty(json)
            ? new List<Product>()
            : JsonSerializer.Deserialize<List<Product>>(json);
    }

    public async Task AddToCartAsync(Product newItem, bool overrideQuantity = false)
    {
        var cart = await GetCartAsync();

        var existing = cart.FirstOrDefault(i => i.Id == newItem.Id);

        if (existing != null)
        {
            if (overrideQuantity)
                existing.Quantity = newItem.Quantity;
            else
                existing.Quantity += newItem.Quantity;
        }
        else
        {
            if (newItem.Quantity < 1)
                newItem.Quantity = 1;

            cart.Add(newItem);
        }

        await SaveCartAsync(cart);
        NotifyStateChanged();
    }

    public async Task UpdateCartAsync(int id, int quantity)
    {
        var cart = await GetCartAsync();

        var existing = cart.FirstOrDefault(i => i.Id == id);

        if (existing != null)
        {
            existing.Quantity = quantity;
            await SaveCartAsync(cart);
            NotifyStateChanged();
        }
    }

    public async Task<int> CountAsync()
    {
        var cart = await GetCartAsync();
        return cart.Count;
    }

    public async Task SaveCartAsync(List<Product> cart)
    {
        var json = JsonSerializer.Serialize(cart);
        await _js.InvokeVoidAsync("cartStorage.setCart", json);
    }

    public async Task RemoveFromCartAsync(int id)
    {
        var cart = await GetCartAsync();

        var item = cart.FirstOrDefault(i => i.Id == id);

        if (item != null)
        {
            cart.Remove(item);
            await SaveCartAsync(cart);
            NotifyStateChanged();
        }
    }

    public async Task ClearCartAsync()
    {
        await _js.InvokeVoidAsync("cartStorage.clearCart");
        NotifyStateChanged();
    }
}
