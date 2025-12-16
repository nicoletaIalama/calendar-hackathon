namespace Calendar.Client.DesignComponents.Table;

public class TableItemResponse<TTableItem>
{
    public List<TTableItem> Items { get; set; } = [];
    public string? ContinuationToken { get; set; }
    public int? TotalItemCount { get; set; }
    public bool Success { get; set; }

    public TableItemResponse(List<TTableItem> items, string? continuationToken = null, int? totalItemCount = null)
    {
        Items = items;
        ContinuationToken = continuationToken;
        TotalItemCount = totalItemCount;
        Success = true;
    }    
    
    public TableItemResponse(bool success = false)
    {
        Items = [];
        ContinuationToken = null;
        TotalItemCount = null;
        Success = success;
    }
};
