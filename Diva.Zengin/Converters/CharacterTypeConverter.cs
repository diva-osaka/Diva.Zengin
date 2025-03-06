using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Diva.Zengin.Converters;

public class NullableDecimalTypeConverter(int count = 1) : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;

        return decimal.Parse(text);
    }

    public override string ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        return value?.ToString()?.PadLeft(count) ?? "".PadLeft(count);
    }
}