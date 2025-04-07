using CsvHelper.Configuration.Attributes;
using Diva.Zengin.Converters;

namespace Diva.Zengin.Records;

/// <summary>
/// 入出金取引明細のトレーラ・レコードを表すクラス。
/// </summary>
[FluentSetter]
[TypeConverterGetter]
[IndexToLengthMap]
public partial class 入出金取引明細Trailer : IRecord
{
    /// <summary>
    /// データ区分 (N(1))  
    /// 8: トレーラ・レコード
    /// </summary>
    [Index(0)]
    [TypeConverter(typeof(NumberTypeConverter<データ区分>), 1)]
    public データ区分 データ区分 { get; set; } = データ区分.Trailer;

    /// <summary>
    /// 入金件数 (N(6))  
    /// 右詰め・残り前「0」
    /// </summary>
    [Index(1)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 6)]
    public int 入金件数 { get; set; }

    /// <summary>
    /// 入金額合計 (N(13))  
    /// 右詰め・残り前「0」
    /// </summary>
    [Index(2)]
    [TypeConverter(typeof(NumberTypeConverter<decimal>), 13)]
    public decimal 入金額合計 { get; set; }

    /// <summary>
    /// 出金件数 (N(6))  
    /// 右詰め・残り前「0」
    /// </summary>
    [Index(3)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 6)]
    public int 出金件数 { get; set; }

    /// <summary>
    /// 出金額合計 (N(13))  
    /// 右詰め・残り前「0」
    /// </summary>
    [Index(4)]
    [TypeConverter(typeof(NumberTypeConverter<decimal>), 13)]
    public decimal 出金額合計 { get; set; }

    /// <summary>
    /// 貸越区分 (N(1))
    /// 取引後残高の状態を表わす。 
    /// 1: プラス, 2: マイナス
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(5)]
    [TypeConverter(typeof(NumberTypeConverter<int?>), 1)]
    public int? 貸越区分 { get; set; }

    /// <summary>
    /// 取引後残高 (N(14))  
    /// 右詰め・残り前「0」
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(6)]
    [TypeConverter(typeof(NumberTypeConverter<decimal?>), 14)]
    public decimal? 取引後残高 { get; set; }

    /// <summary>
    /// データ・レコード件数 (N(7))  
    /// 右詰め・残り前「0」
    /// </summary>
    [Index(7)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 7)]
    public int データレコード件数 { get; set; }

    /// <summary>
    /// ダミー (C(139))  
    /// スペースとする。
    /// </summary>
    [Index(8)]
    [TypeConverter(typeof(CharacterTypeConverter), 139)]
    public string ダミー  { get; set; } = "";
}