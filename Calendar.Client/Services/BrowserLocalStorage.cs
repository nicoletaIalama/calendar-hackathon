using System.Text.Json;
using Microsoft.JSInterop;

namespace Calendar.Client.Services;

public sealed class BrowserLocalStorage
{
    private readonly IJSRuntime _js;

    public BrowserLocalStorage(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<string?> GetStringAsync(string key)
    {
        return await _js.InvokeAsync<string?>("enableCalendar.storage.get", key);
    }

    public async Task SetStringAsync(string key, string value)
    {
        await _js.InvokeVoidAsync("enableCalendar.storage.set", key, value);
    }

    public async Task RemoveAsync(string key)
    {
        await _js.InvokeVoidAsync("enableCalendar.storage.remove", key);
    }

    public async Task<T?> GetJsonAsync<T>(string key, JsonSerializerOptions? options = null)
    {
        var raw = await GetStringAsync(key);
        if (string.IsNullOrWhiteSpace(raw))
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(raw, options);
    }

    public async Task SetJsonAsync<T>(string key, T value, JsonSerializerOptions? options = null)
    {
        var raw = JsonSerializer.Serialize(value, options);
        await SetStringAsync(key, raw);
    }
}


