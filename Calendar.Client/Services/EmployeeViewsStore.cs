using System.Text.Json;
using Calendar.Client.Models;

namespace Calendar.Client.Services;

public sealed class EmployeeViewsStore
{
    private const string StorageKey = "enable.calendar.employeeViews.v1";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    private readonly BrowserLocalStorage _storage;

    public EmployeeViewsStore(BrowserLocalStorage storage)
    {
        _storage = storage;
    }

    public async Task<List<SavedEmployeeView>> GetAllAsync()
    {
        var views = await _storage.GetJsonAsync<List<SavedEmployeeView>>(StorageKey, JsonOptions) ?? new List<SavedEmployeeView>();
        // Defensive cleanup + stable ordering.
        foreach (var v in views)
        {
            v.Name ??= "";
            v.EmployeeIds ??= new List<int>();
        }

        return views
            .OrderBy(v => v.Name, StringComparer.OrdinalIgnoreCase)
            .ThenByDescending(v => v.UpdatedUtc)
            .ToList();
    }

    public async Task<SavedEmployeeView?> GetByIdAsync(string id)
    {
        var views = await GetAllAsync();
        return views.FirstOrDefault(v => string.Equals(v.Id, id, StringComparison.Ordinal));
    }

    public async Task UpsertAsync(SavedEmployeeView view)
    {
        ArgumentNullException.ThrowIfNull(view);

        var views = await _storage.GetJsonAsync<List<SavedEmployeeView>>(StorageKey, JsonOptions) ?? new List<SavedEmployeeView>();
        var idx = views.FindIndex(v => string.Equals(v.Id, view.Id, StringComparison.Ordinal));
        if (idx >= 0)
        {
            views[idx] = view;
        }
        else
        {
            views.Add(view);
        }

        await _storage.SetJsonAsync(StorageKey, views, JsonOptions);
    }

    public async Task DeleteAsync(string id)
    {
        var views = await _storage.GetJsonAsync<List<SavedEmployeeView>>(StorageKey, JsonOptions) ?? new List<SavedEmployeeView>();
        views.RemoveAll(v => string.Equals(v.Id, id, StringComparison.Ordinal));
        await _storage.SetJsonAsync(StorageKey, views, JsonOptions);
    }
}


