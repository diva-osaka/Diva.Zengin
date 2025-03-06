using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Diva.Zengin.Converters;

public class 和暦日付TypeConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text) || text.Length != 6)
            throw new FormatException($"Invalid date format: {text}");

        return 和暦To西暦Converter.Convert和暦日付ToDateOnly(text);
    }

    public override string ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        // CultureInfoを日本のカレンダーに設定
        var culture = new CultureInfo("ja-JP", true)
        {
            DateTimeFormat =
            {
                Calendar = new JapaneseCalendar()
            }
        };

        return value is DateOnly date
            ? date.ToString("yyMMdd", culture)
            : throw new FormatException("Value is not a DateOnly type");
    }
}