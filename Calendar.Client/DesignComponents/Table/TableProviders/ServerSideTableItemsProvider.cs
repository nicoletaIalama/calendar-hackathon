using Calendar.Client.DesignComponents.Table;

namespace Calendar.Client.DesignComponents.Table.TableProviders;

public class ServerSideTableItemsProvider<TTableItem> : ITableItemsProvider<TTableItem> where TTableItem : ITableItem
{
    public bool IsLoading { get; set; }
    public int PageIndex { get; set; } = 1;
    public string? CurrentSortedColumn { get; set; }
    public SortOrder? CurrentSortOrder { get; set; }
    public string? ContinuationToken { get; set; }
    public List<TTableItem> Page { get; set; } = [];
    public List<TTableItem> Items { get; set; } = [];
    public int PageSize { get; set; } = 10;
    public int TotalItemCount { get; set; }
    public int TotalPages => TotalItemCount < PageSize ? 1 : (int)Math.Ceiling(TotalItemCount / (double)PageSize);
    public bool HasItems => TotalItemCount != 0;
    public bool EnableRowSelection { get; } = true;

    private Dictionary<string, TTableItem> SelectedItems { get; set; } = [];
    
    // Prevent concurrent requests
    private readonly SemaphoreSlim _requestSemaphore = new(1, 1);

    private int LoadedItemCount => Items.Count;
    private bool MoreAvailable => !string.IsNullOrEmpty(ContinuationToken);

    public Func<TableItemRequest, Task<TableItemResponse<TTableItem>>> OnGetMoreItems
    {
        get;
        set;
    } = default!;

    public ServerSideTableItemsProvider(Func<TableItemRequest, Task<TableItemResponse<TTableItem>>> getMoreItems, bool enableRowSelection)
    {
        OnGetMoreItems = getMoreItems;
        EnableRowSelection = enableRowSelection;
    }

    public async Task Init()
    {
        await GetMoreItems();
    }

    public async Task SetPage(int pageIndex)
    {
        PageIndex = pageIndex;
        await UpdatePaging();
    }

    public async Task SetPageSize(int pageSize)
    {
        PageSize = pageSize;
        PageIndex = 1;
        await UpdatePaging();
    }

    public async Task SetOrder(Func<TTableItem, object> field, SortOrder sortDirection, string? columnName)
    {
        CurrentSortOrder = sortDirection;
        CurrentSortedColumn = columnName;
        await Refresh();
    }

    public async Task SetItems(List<TTableItem> items)
    {
        if (items.Count != 0)
        {
            await UpdatePaging();
        }
    }

    private async Task UpdatePaging()
    {
        if (MoreAvailable && LoadedItemCount < PageIndex * PageSize)
        {
            await GetMoreItems();
        }
        else
        {
            Page = Items.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
    }

    private async Task GetMoreItems()
    {
        // Ensure only one request executes at a time
        await _requestSemaphore.WaitAsync();
        try
        {
            var tableItemRequest = new TableItemRequest(ContinuationToken, CurrentSortedColumn, CurrentSortOrder, PageSize);
            IsLoading = true;
            
            try
            {
                // If we don't have a continuation token, this is a fresh query
                // Clear existing items to prevent duplication
                var isInitialOrFreshQuery = string.IsNullOrEmpty(ContinuationToken);
                if (isInitialOrFreshQuery)
                {
                    Items.Clear();
                }
                
                var request = await OnGetMoreItems(tableItemRequest);
                if (request.Success)
                {
                    if (isInitialOrFreshQuery)
                    {
                        TotalItemCount = request.TotalItemCount ?? 0;
                    }
                    
                    ContinuationToken = request.ContinuationToken;
                    Items.AddRange(request.Items);
                    Page = Items.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
                }
            }
            finally
            {
                IsLoading = false;
            }
        }
        finally
        {
            _requestSemaphore.Release();
        }
    }
    
    public async Task Refresh()
    {
        PageIndex = 1;
        TotalItemCount = 0;
        ContinuationToken = null;
        Items.Clear();
        await GetMoreItems();
    }

    public void SelectItem(TTableItem itemToSelect)
    {
        if (!EnableRowSelection) return;
        SelectedItems[itemToSelect.Id] = itemToSelect;
    }

    public void SelectItems(IEnumerable<TTableItem> itemsToSelect)
    {
        foreach (var itemToSelect in itemsToSelect)
        {
            SelectItem(itemToSelect);
        }
    }

    public void DeselectItem(TTableItem itemToDeselect)
    {
        if (!EnableRowSelection) return;
        SelectedItems.Remove(itemToDeselect.Id);
    }

    public void DeselectAll()
    {
        if (!EnableRowSelection) return;
        SelectedItems.Clear();
    }

    public void SelectPage()
    {
        if (!EnableRowSelection) return;
        foreach (var item in Page)
        {
            SelectItem(item);
        }
    }

    public void DeselectPage()
    {
        if (!EnableRowSelection) return;
        foreach (var item in Page)
        {
            DeselectItem(item);
        }
    }

    public bool AreAllItemsOnPageSelected()
    {
        if (!EnableRowSelection) return false;
        return Page.All(i => SelectedItems.ContainsKey(i.Id));
    }

    public bool IsAnyItemOnPageSelected()
    {
        if (!EnableRowSelection) return false;
        return Page.Any(i => SelectedItems.ContainsKey(i.Id));
    }

    public List<string> GetSelectedItemIds()
    {
        if (!EnableRowSelection) return [];
        return SelectedItems.Keys.ToList();
    }

    public List<TTableItem> GetSelectedItems()
    {
        if (!EnableRowSelection) return [];
        return SelectedItems.Values.ToList();
    }

    public bool IsItemSelected(TTableItem item)
    {
        if (!EnableRowSelection) return false;

        var uniqueId = item.Id;
        if (uniqueId == null)
        {
            return false;
        }
        return SelectedItems.ContainsKey(uniqueId);
    }
}
