using CsvHelper.Configuration.Attributes;
using Diva.Zengin.Converters;

namespace Diva.Zengin.Formats;

/// <summary>
/// 入出金取引明細のエンド・レコードを表すクラス。
/// </summary>
[FluentSetter]
[IndexToLengthMap]
public partial class 入出金取引明細End : IRecord
{
    /// <summary>
    /// データ区分 (N(1))  
    /// 9: エンド・レコード
    /// </summary>
    [Index(0)]
    [TypeConverter(typeof(NumberTypeConverter<データ区分>), 1)]
    public データ区分 データ区分 { get; set; } = データ区分.End;

    /// <summary>
    /// レコード総件数 (N(10))  
    /// 右詰め・残り前「0」
    /// </summary>
    [Index(1)]
    [TypeConverter(typeof(NumberTypeConverter<decimal>), 10)]
    public decimal レコード総件数 { get; set; }

    /// <summary>
    /// 口座数 (N(5))  
    /// 右詰め・残り前「0」
    /// </summary>
    [Index(2)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 5)]
    public int 口座数 { get; set; }

    /// <summary>
    /// ダミー (C(184))  
    /// スペース
    /// </summary>
    [Index(3)]
    [TypeConverter(typeof(CharacterTypeConverter), 184)]
    public string ダミー { get; set; } = "";
}