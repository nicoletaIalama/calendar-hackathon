namespace Enable.Design.Filters;

public record FilterRecord(string Id, string Name, bool Disabled = false, string? DisabledHelpText = null);
