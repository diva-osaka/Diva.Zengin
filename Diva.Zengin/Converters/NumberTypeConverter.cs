using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Diva.Zengin.Converters;

/// <summary>
/// タイプN（数字）用の TypeConverter
/// 読み込み時、数字 string → T に変換。ただし T が nullable で string が空白の場合は null を返す（任意項目を想定）。
/// 書き込み時、T → string （右詰め残り前「0」）に変換する。ただし T が nullable の場合は空白を返す（任意項目を想定）。
/// </summary>
/// <param name="count">Nの桁数</param>
/// <typeparam name="T"></typeparam>
internal class NumberTypeConverter<T>(int count = 1) : ITypeConverter
{
    public object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        try
        {
            return NumberConverter.ConvertFromString<T>(text, count);
        }
        catch (Exception ex)
        {
            throw new TypeConverterException(this, memberMapData, text, row.Context,
                $"Failed to convert '{text}' to {typeof(T)}.", ex);
        }
    }

    public string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        try
        {
            return NumberConverter.ConvertToString(value, count);
        }
        catch (Exception ex)
        {
            throw new TypeConverterException(this, memberMapData, value, row.Context,
                $"Failed to convert {value} to string.", ex);
        }
    }
}