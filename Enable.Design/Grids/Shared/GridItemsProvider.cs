using System.Diagnostics.CodeAnalysis;

namespace Enable.Design.Grids.Shared;

public class GridItemsProvider<TGridItem>
{
    private readonly Func<TGridItem, string>? _groupingFunc;
    private readonly IList<TGridItem> _cachedItems;
    private (Func<TGridItem, object> Field, SortOrder SortDirection)? _sort;

    public int TotalPages => TotalItems < PageSize ? 1 : (int)Math.Ceiling(TotalItems / (double)PageSize);
    public int StartCount => PageIndex == 1 ? 1 : PageSize * (PageIndex - 1) + 1;
    public int EndCount => TotalItems < PageSize ? TotalItems : PageSize * (PageIndex - 1) + Page.Count;
    
    public IList<TGridItem> Page { get; private set; } = default!;

    private int _pageIndex = 1;
    public int PageSize
    {
        get => _pageSize;
        set
        {
            _pageSize = value;
            _pageIndex = 1;
            UpdatePaging();
        }
    }

    private int _pageSize = 10;
    public int PageIndex
    {
        get => _pageIndex;
        set
        {
            if (value <= TotalPages || value > 0)
            {
                _pageIndex = value;
                UpdatePaging();
            }
        }
    }
    
    private IList<TGridItem> Items { get; set; }
    public bool HasItems => Items.Any();
    
    [MemberNotNullWhen(true, nameof(_groupingFunc))]
    public bool HasGroups => _groupingFunc is not null;

    public int TotalItems => Items.Count;

    public GridItemsProvider(IList<TGridItem> items, Func<TGridItem, string>? groupingFunc)
    {
        _cachedItems = items;
        _groupingFunc = groupingFunc;
        Items = HasGroups ? items.OrderBy(_groupingFunc).ToList() : items;
        UpdatePaging();
    }

    public void SetOrder(Func<TGridItem, object> field, SortOrder sortDirection)
    {
        _sort = (field, sortDirection);
        UpdateItems(Items);
    }

    public void SetItems(IList<TGridItem> items) 
        => UpdateItems(items);

    private void UpdatePaging() 
        => Page = Items.Skip((_pageIndex - 1) * PageSize).Take(PageSize).ToList();

    private void UpdateItems(IList<TGridItem> items)
    {
        if (_sort is null)
        {
            Items = HasGroups ? items.OrderBy(_groupingFunc).ToList() : items;
            UpdatePaging();
            return;
        }
        
        var (field, sortDirection) = _sort.Value;
        
        if (HasGroups)
        {
            Items = sortDirection.Equals(SortOrder.Ascending)
                    ? items.OrderBy(_groupingFunc).ThenBy(field).ToList()
                    : items.OrderBy(_groupingFunc).ThenByDescending(field).ToList();
            
            UpdatePaging();
        }
        else
        {
            Items = sortDirection.Equals(SortOrder.Ascending)
                    ? items.OrderBy(field).ToList()
                    : items.OrderByDescending(field).ToList();

            UpdatePaging();
        }
    }
}
