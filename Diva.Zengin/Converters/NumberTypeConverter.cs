using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Diva.Zengin.Converters;

public class CharacterTypeConverter(int count = 1) : DefaultTypeConverter
{
    public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        return text?.TrimEnd() ?? "";
    }

    public override string ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        return value?.ToString()?.PadRight(count) ?? "".PadRight(count);
    }
}