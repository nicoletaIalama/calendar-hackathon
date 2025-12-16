using Blazored.LocalStorage;

namespace Calendar.Client.DesignComponents.Table.LocalStorage;

public class ColumnChoiceLocalStorageProvider(ILocalStorageService localStorage)
{
    private const string StoragePrefix = "columnChoices_";

    /// <summary>
    /// Saves the user's column choices for a specific page.
    /// </summary>
    /// <param name="tableKey">A unique key for the table.</param>
    /// <param name="chosenColumns">Dictionary of column title to show/hide bool.</param>
    public async Task SaveColumnChoicesAsync(string tableKey, Dictionary<string, bool> chosenColumns)
    {
        if (string.IsNullOrWhiteSpace(tableKey) || chosenColumns is null)
            return;

        var storageKey = StoragePrefix + tableKey;
        await localStorage.SetItemAsync(storageKey, chosenColumns);
    }

    /// <summary>
    /// Gets the user's column choices for a specific page.
    /// </summary>
    /// <param name="tableKey">A unique key for the table.</param>
    /// <returns>Dictionary of column title to show/hide bool, or null if not set.</returns>
    public async Task<Dictionary<string, bool>?> GetColumnChoicesAsync(string tableKey)
    {
        if (string.IsNullOrWhiteSpace(tableKey))
            return null;

        var storageKey = StoragePrefix + tableKey;
        return await localStorage.GetItemAsync<Dictionary<string, bool>>(storageKey);
    }
}
