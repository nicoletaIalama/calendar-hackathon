namespace Calendar.Client.DesignComponents.Table;

public record TableItemRequest(string? ContinuationToken, string? SortColumn, SortOrder? SortOrder, int PageSize);
