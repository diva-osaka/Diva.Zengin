using CsvHelper.Configuration.Attributes;
using Diva.Zengin.Converters;

namespace Diva.Zengin.Formats;

/// <summary>
/// 総合振込のトレーラ・レコードを表すクラス。
/// </summary>
public class 総合振込Trailer : IRecord
{
    /// <summary>
    /// データ区分 (N(1))  
    /// 8: トレーラ・レコード
    /// </summary>
    [Index(0)]
    [TypeConverter(typeof(NumberTypeConverter<データ区分>), 1)]
    public データ区分 データ区分 { get; set; } = データ区分.Trailer;

    /// <summary>
    /// 合計件数 (N(6))  
    /// 右詰め・残り前「0」
    /// </summary>
    [Index(1)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 6)]
    public int 合計件数 { get; set; }

    /// <summary>
    /// 合計金額 (N(12))  
    /// 右詰め・残り前「0」
    /// </summary>
    [Index(2)]
    [TypeConverter(typeof(NumberTypeConverter<decimal>), 12)]
    public decimal 合計金額 { get; set; }

    /// <summary>
    /// ダミー (C(101))  
    /// スペースとする。
    /// </summary>
    [Index(3)]
    [TypeConverter(typeof(CharacterTypeConverter), 101)]
    public string ダミー  { get; set; } = "";
}