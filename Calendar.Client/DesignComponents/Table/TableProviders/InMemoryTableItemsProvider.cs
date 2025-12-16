using Calendar.Client.DesignComponents.Table;

namespace Calendar.Client.DesignComponents.Table.TableProviders;

public class InMemoryTableItemsProvider<TTableItem> : ITableItemsProvider<TTableItem> where TTableItem : ITableItem
{
    public bool IsLoading { get; set; }
    public int PageIndex { get; set; } = 1;
    public string? CurrentSortedColumn { get; set; }
    public SortOrder? CurrentSortOrder { get; set; }
    public string? ContinuationToken { get; set; }
    public List<TTableItem> Page { get; set; } = [];
    public List<TTableItem> Items { get; set; } = [];
    public int PageSize { get; set; } = 10;
    public int TotalItemCount => Items.Count;
    public int TotalPages => TotalItemCount < PageSize ? 1 : (int)Math.Ceiling(TotalItemCount / (double)PageSize);
    public bool HasItems => TotalItemCount != 0;
    public bool EnableRowSelection { get; } = true;

    private Dictionary<string, TTableItem> SelectedItems { get; set; } = [];

    private (Func<TTableItem, object> Field, SortOrder SortDirection)? _sort;
 
    public InMemoryTableItemsProvider(List<TTableItem> items, bool enableRowSelection) 
    {
        Items = items;
        EnableRowSelection = enableRowSelection;
    }
    
    public Task SetPage(int pageIndex)
    {
        PageIndex = pageIndex;
        UpdatePaging();
        return Task.CompletedTask;
    }

    public Task SetPageSize(int pageSize)
    {
        PageSize = pageSize;
        PageIndex = 1;
        UpdatePaging();
        return Task.CompletedTask;
    }

    public Task SetOrder(Func<TTableItem, object> field, SortOrder sortDirection, string? columnName)
    {
        _sort = (field, sortDirection);
        UpdateItems(Items);
        return Task.CompletedTask;
    }

    public Task SetItems(List<TTableItem> items)
    {
        Items = items;
        
        if (PageIndex > TotalPages)
        {
            SetPage(1);
        }
        else
        {
            UpdatePaging();
        }
        
        return Task.CompletedTask;
    }

    public Task Init()
    {
        UpdatePaging();
        return Task.CompletedTask;
    }

    public Task Refresh()
    {
        PageIndex = 1;
        UpdatePaging();
        return Task.CompletedTask;
    }

    private void UpdateItems(List<TTableItem> items)
    {
        if (_sort is null)
        {
            Items = items;
            UpdatePaging();
            return;
        }

        var (field, sortDirection) = _sort.Value;

        Items = sortDirection.Equals(SortOrder.Ascending)
            ? items.OrderBy(field).ToList()
            : items.OrderByDescending(field).ToList();
        
        PageIndex = 1;
        UpdatePaging();
    }

    private void UpdatePaging()
    {
        Page = Items.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
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

        return SelectedItems.ContainsKey(item.Id);
    }
}
