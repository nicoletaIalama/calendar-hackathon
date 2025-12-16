using Enable.Design.Grids.Shared;

namespace Enable.Design.Grids.PagedListing;

public abstract class ListingProvider<TGridItem>
{
    public bool Loading { get; protected set; } = false;
    public int CurrentPageIndex { get; private set; } = 1;
    public string? CurrentSortedColumn { get; private set; }
    public SortOrder? CurrentSortOrder { get; private set; }
    public string? ContinuationToken;

    public IList<TGridItem> CurrentPage =>
        Items.Skip((CurrentPageIndex - 1) * PageSize).Take(PageSize).ToList();

    public int PageSize { get; private set; } = 10;
    public List<TGridItem> Items { get; protected set; } = [];
    public bool MoreAvailable { get; protected set; } = false;
    public int TotalItemCount = 0;
    public int LoadedItemCount => Items.Count;
    public int PageCount => (int)Math.Ceiling((double)TotalItemCount / PageSize);

    public abstract Task GetMore();

    public async Task SetPage(int page)
    {
        CurrentPageIndex = page;
        if (MoreAvailable && LoadedItemCount < page * PageSize)
        {
            await GetMore();
        }
    }

    public async Task SetPageSize(int pageSize)
    {
        PageSize = pageSize;
        CurrentPageIndex = CurrentPageIndex > PageCount
            ? PageCount
            : CurrentPageIndex;

        if (MoreAvailable && LoadedItemCount < Math.Min(CurrentPageIndex * PageSize, TotalItemCount))
        {
            await GetMore();
        }
    }


    public async Task SetSort(string field, SortOrder order)
    {
        CurrentSortedColumn = field;
        CurrentSortOrder = order;
        await FreshSearch();
    }

    public string? GetSortedColumn()
    {
        return CurrentSortedColumn;
    }

    public SortOrder? GetSortOrder()
    {
        return CurrentSortOrder;
    }

    public async Task ClearSort()
    {
        CurrentSortedColumn = null;
        CurrentSortOrder = null;
        await FreshSearch();
    }

    public async Task FreshSearch()
    {
        ContinuationToken = null;
        MoreAvailable = true;
        Items.Clear();
        await SetPage(1);
    }
}
