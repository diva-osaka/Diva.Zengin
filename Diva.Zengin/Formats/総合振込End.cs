using CsvHelper.Configuration.Attributes;
using Diva.Zengin.Converters;

namespace Diva.Zengin.Formats;

/// <summary>
/// 総合振込のエンド・レコードを表すクラス。
/// </summary>
[FluentSetter]
[IndexToLengthMap]
public partial class 総合振込End : IRecord
{
    /// <summary>
    /// データ区分 (N(1))  
    /// 9: エンド・レコード
    /// </summary>
    [Index(0)]
    [TypeConverter(typeof(NumberTypeConverter<データ区分>), 1)]
    public データ区分 データ区分 { get; set; } = データ区分.End;

    /// <summary>
    /// ダミー (C(119))  
    /// スペース
    /// </summary>
    [Index(1)]
    [TypeConverter(typeof(CharacterTypeConverter), 119)]
    public string ダミー { get; set; } = "";
}