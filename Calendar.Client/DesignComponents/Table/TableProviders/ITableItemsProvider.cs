using Calendar.Client.DesignComponents.Table;

namespace Calendar.Client.DesignComponents.Table.TableProviders;

public interface ITableItemsProvider<TTableItem> where TTableItem : ITableItem
{
    public bool IsLoading { get; protected set; }
    public int PageIndex { get; protected set; }
    public string? CurrentSortedColumn { get; protected set; }
    public SortOrder? CurrentSortOrder { get; protected set; }
    public string? ContinuationToken { get; protected set; }
    public List<TTableItem> Page { get; protected set; }
    public List<TTableItem> Items { get; protected set; }
    public int PageSize { get; protected set; }
    public int TotalItemCount { get; }
    public int TotalPages { get; }
    public bool HasItems { get; }
    public bool EnableRowSelection { get; }
    public Task SetPage(int pageIndex);
    public Task SetPageSize(int pageSize);
    public Task SetOrder(Func<TTableItem, object> field, SortOrder sortDirection, string? columnName);
    public Task SetItems(List<TTableItem> items);
    public Task Init();
    public Task Refresh();
    public void SelectItem(TTableItem item);
    public void SelectItems(IEnumerable<TTableItem> items);
    public void DeselectItem(TTableItem item);
    public void DeselectAll();
    public void SelectPage();
    public void DeselectPage();
    public bool AreAllItemsOnPageSelected();
    public bool IsAnyItemOnPageSelected();
    public List<string> GetSelectedItemIds();
    public List<TTableItem> GetSelectedItems();
    public bool IsItemSelected(TTableItem item);
}
