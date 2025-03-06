using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Diva.Zengin.Converters;

/// <summary>
/// 和暦 (N(6)) TypeConverter
/// 読み込み時、DateOnly に変換。
/// 書き込み時、和暦yyMMddに変換。
/// <remarks>任意項目は DateOnly? とすること</remarks>
/// </summary>
internal class JapaneseEraDateTypeConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        var fieldName = memberMapData.Names.FirstOrDefault() ?? "Unknown Field";
        var targetType = memberMapData.Member?.MemberType();

        if (string.IsNullOrWhiteSpace(text) || text == "000000")
        {
            if (targetType == typeof(DateOnly?))
                return null;

            // DateOnly? ではないのに空白の場合
            throw new FormatException(
                $"[{fieldName}] Invalid empty date format. Expected 6-digit Japanese era date for type {targetType?.Name ?? "Unknown"}.");
        }

        if (text.Length != 6)
            throw new FormatException(
                $"[{fieldName}] Invalid date format: '{text}'. Expected 6-digit Japanese era date (yyMMdd).");

        return 和暦To西暦Converter.Convert和暦日付ToDateOnly(text);
    }

    public override string ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        var culture = new CultureInfo("ja-JP", true)
        {
            DateTimeFormat =
            {
                Calendar = new JapaneseCalendar()
            }
        };

        return value is DateOnly date
            ? date.ToString("yyMMdd", culture)
            : "000000";
    }
}