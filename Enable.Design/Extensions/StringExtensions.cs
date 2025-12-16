namespace Enable.Design.Extensions;

public static class StringExtensions
{
    public static bool HasCharacters(this string? value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }
}

