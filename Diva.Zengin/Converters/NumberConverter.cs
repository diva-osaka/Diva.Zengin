using System.Globalization;

namespace Diva.Zengin.Converters;

internal static class NumberConverter
{
    public static T? ConvertFromString<T>(string? text, int count = 1)
    {
        var isNullable = Nullable.GetUnderlyingType(typeof(T)) != null;
        var underlyingType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

        if (string.IsNullOrWhiteSpace(text))
        {
            return isNullable ? default : default(T);
        }

        object convertedValue;
        if (underlyingType.IsEnum)
        {
            convertedValue = int.TryParse(text, out var intValue)
                ? Enum.ToObject(underlyingType, intValue)
                : Enum.Parse(underlyingType, text, true);
        }
        else
        {
            convertedValue = Convert.ChangeType(text, underlyingType, CultureInfo.InvariantCulture);
        }

        return (T?)(isNullable ? Activator.CreateInstance(typeof(T), convertedValue) : convertedValue);
    }

    public static string ConvertToString(object? value, int count = 1)
    {
        if (value == null)
            return new string(' ', count);

        if (value.GetType().IsEnum)
        {
            return Convert.ToInt32(value).ToString($"D{count}", CultureInfo.InvariantCulture);
        }
        
        if (value is decimal decimalValue)
        {
            return decimalValue.ToString(new string('0', count), CultureInfo.InvariantCulture);
        }

        return ((IFormattable)value).ToString($"D{count}", CultureInfo.InvariantCulture);
    }
}