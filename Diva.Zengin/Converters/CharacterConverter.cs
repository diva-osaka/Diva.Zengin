namespace Diva.Zengin.Converters;

internal static class CharacterConverter
{
    public static string? ConvertFromString(string? text, bool nullable = false)
    {
        // 空白/null 以外の場合は、TrimEnd() して返す。
        var value = text?.TrimEnd();
        if (!string.IsNullOrWhiteSpace(value))
            return value;

        // null 許容の場合は、 null を返す（任意項目を想定）。それ以外の場合は "" を返す。
        return nullable ? null : "";
    }

    public static string ConvertToString(object? value, int count = 1) =>
        value?.ToString()?.PadRight(count) ?? "".PadRight(count);
}