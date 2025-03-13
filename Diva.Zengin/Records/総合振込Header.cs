using CsvHelper.Configuration.Attributes;
using Diva.Zengin.Converters;

namespace Diva.Zengin.Records;

/// <summary>
/// 総合振込のヘッダー・レコードを表すクラス。
/// </summary>
[FluentSetter]
[IndexToLengthMap]
public partial class 総合振込Header : IRecord
{
    /// <summary>
    /// データ区分 (N(1))  
    /// 1: ヘッダーレコード
    /// </summary>
    [Index(0)]
    [TypeConverter(typeof(NumberTypeConverter<データ区分>), 1)]
    public データ区分 データ区分 { get; set; } = データ区分.Header;

    /// <summary>
    /// 種別コード (N(2))  
    /// 21: 総合振込
    /// </summary>
    [Index(1)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 2)]
    public int 種別コード { get; set; }

    /// <summary>
    /// コード区分 (N(1))  
    /// 0: JIS, 1: EBCDIC
    /// </summary>
    [Index(2)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 1)]
    public int コード区分 { get; set; }

    /// <summary>
    /// 振込依頼人コード（取引企業コード） (N(10))
    /// 振込依頼人識別のため銀行が採番したコードを表わす。 
    /// 右詰め・残り前「0」
    /// </summary>
    [Index(3)]
    [TypeConverter(typeof(NumberTypeConverter<decimal>), 10)]
    public decimal 振込依頼人コード { get; set; }

    /// <summary>
    /// 振込依頼人名 (C(40))  
    /// 左詰め・残りスペース
    /// </summary>
    [Index(4)]
    [TypeConverter(typeof(CharacterTypeConverter), 40)]
    public string 振込依頼人名 { get; set; } = "";

    /// <summary>
    /// 取組日 (N(4))  
    /// 振込日を表わす。 MMDD (月-日)
    /// </summary>
    [Index(5)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 4)]
    public int 取組日 { get; set; }

    /// <summary>
    /// 仕向銀行番号 (N(4))  
    /// 統一金融機関番号 
    /// </summary>
    [Index(6)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 4)]
    public int 仕向銀行番号 { get; set; }

    /// <summary>
    /// 仕向銀行名 (C(15))  
    /// 左詰め・残りスペース
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(7)]
    [TypeConverter(typeof(CharacterTypeConverter), 15)]
    public string? 仕向銀行名 { get; set; }

    /// <summary>
    /// 仕向支店番号 (N(3))  
    /// 統一店番号
    /// </summary>
    [Index(8)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 3)]
    public int 仕向支店番号 { get; set; }

    /// <summary>
    /// 仕向支店名 (C(15))  
    /// 左詰め・残りスペース
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(9)]
    [TypeConverter(typeof(CharacterTypeConverter), 15)]
    public string? 仕向支店名 { get; set; }

    /// <summary>
    /// 預金種目（依頼人） (N(1))  
    /// 1: 普通預金, 2: 当座預金, 9: その他
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(10)]
    [TypeConverter(typeof(NumberTypeConverter<int?>), 1)]
    public int? 預金種目 { get; set; }

    /// <summary>
    /// 口座番号（依頼人） (N(7))  
    /// 右詰め・残り前「0」
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(11)]
    [TypeConverter(typeof(NumberTypeConverter<int?>), 7)]
    public int? 口座番号 { get; set; }

    /// <summary>
    /// ダミー (C(17))  
    /// スペースとする
    /// </summary>
    [Index(12)]
    [TypeConverter(typeof(CharacterTypeConverter), 17)]
    public string ダミー { get; set; } = "";
}