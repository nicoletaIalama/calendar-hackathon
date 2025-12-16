using System.Reflection;
using System.Text.Json.Serialization;

namespace Enable.Design.Extensions;

public static class EnumExtensions
{
    public static string GetJsonPropertyName(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        if (field == null)
            return value.ToString().ToLowerInvariant();

        var attribute = field.GetCustomAttribute<JsonPropertyNameAttribute>();
        return attribute?.Name ?? value.ToString().ToLowerInvariant();
    }
}

