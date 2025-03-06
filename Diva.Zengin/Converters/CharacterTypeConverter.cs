using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Diva.Zengin.Converters;

/// <summary>
/// タイプC（キャラクター）用の TypeConverter
/// 読み込み時、右側の空白を削除。空白のみの場合、string? → null を返す（任意項目を想定）。string → "" を返す。
/// 書き込み時、左詰め残りスペースを付与。
/// </summary>
/// <param name="count">Cの桁数</param>
internal class CharacterTypeConverter(int count = 1) : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        var targetType = memberMapData.Member?.MemberType();
        return CharacterConverter.ConvertFromString(text, targetType != typeof(string));
    }

    public override string ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData) =>
        CharacterConverter.ConvertToString(value, count);
}