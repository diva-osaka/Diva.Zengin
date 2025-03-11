using CsvHelper.Configuration.Attributes;
using Diva.Zengin.Converters;

namespace Diva.Zengin.Records;

/// <summary>
/// 振込入金通知のトレーラ・レコードを表すクラス。
/// </summary>
[FluentSetter]
[IndexToLengthMap]
public partial class 振込入金通知Trailer : IRecord
{
    /// <summary>
    /// データ区分 (N(1))
    /// 8: トレーラ・レコード
    /// </summary>
    [Index(0)]
    [TypeConverter(typeof(NumberTypeConverter<データ区分>), 1)]
    public データ区分 データ区分 { get; set; } = データ区分.Trailer;

    /// <summary>
    /// 振込合計件数 (N(6))
    /// 右詰め残り前「0」
    /// </summary>
    [Index(1)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 6)]
    public int 振込合計件数 { get; set; }

    /// <summary>
    /// 振込合計金額 (N(12))
    /// 右詰め残り前「0」
    /// </summary>
    [Index(2)]
    [TypeConverter(typeof(NumberTypeConverter<decimal>), 12)]
    public decimal 振込合計金額 { get; set; }

    /// <summary>
    /// 取消合計件数 (N(6))
    /// 右詰め残り前「0」
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(3)]
    [TypeConverter(typeof(NumberTypeConverter<int?>), 6)]
    public int? 取消合計件数 { get; set; }

    /// <summary>
    /// 取消合計金額 (N(12))
    /// 右詰め残り前「0」
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(4)]
    [TypeConverter(typeof(NumberTypeConverter<decimal?>), 12)]
    public decimal? 取消合計金額 { get; set; }

    /// <summary>
    /// ダミー (C(163))
    /// スペースとする。
    /// </summary>
    [Index(5)]
    [TypeConverter(typeof(CharacterTypeConverter), 163)]
    public string ダミー { get; set; } = "";
}