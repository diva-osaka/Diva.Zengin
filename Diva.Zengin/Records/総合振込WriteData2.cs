using CsvHelper.Configuration.Attributes;
using Diva.Zengin.Converters;

namespace Diva.Zengin.Records;

/// <summary>
/// 総合振込のデータ・レコードを表すクラス。
/// 顧客コードを持つ。
/// </summary>
[IndexToLengthMap]
public partial class 総合振込WriteData2 : IRecord
{
    /// <summary>
    /// データ区分 (N(1))  
    /// 2: データ・レコード
    /// </summary>
    [Index(0)]
    [TypeConverter(typeof(NumberTypeConverter<データ区分>), 1)]
    public データ区分 データ区分 { get; set; } = データ区分.Data;

    /// <summary>
    /// 被仕向銀行番号 (N(4))  
    /// 統一金融機関番号
    /// </summary>
    [Index(1)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 4)]
    public int 被仕向銀行番号 { get; set; }

    /// <summary>
    /// 被仕向銀行名 (C(15))  
    /// 左詰め・残りスペース
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(2)]
    [TypeConverter(typeof(CharacterTypeConverter), 15)]
    public string? 被仕向銀行名 { get; set; }

    /// <summary>
    /// 被仕向支店番号 (N(3))  
    /// 統一店番号
    /// </summary>
    [Index(3)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 3)]
    public int 被仕向支店番号 { get; set; }

    /// <summary>
    /// 被仕向支店名 (C(15))  
    /// 左詰め・残りスペース
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(4)]
    [TypeConverter(typeof(CharacterTypeConverter), 15)]
    public string? 被仕向支店名 { get; set; }

    /// <summary>
    /// 手形交換所番号 (N(4))  
    /// 統一手形交換所番号
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(5)]
    [TypeConverter(typeof(NumberTypeConverter<int?>), 4)]
    public int? 手形交換所番号 { get; set; }

    /// <summary>
    /// 預金種目 (N(1))  
    /// 1: 普通預金, 2: 当座預金, 4: 貯蓄預金, 9: その他
    /// </summary>
    [Index(6)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 1)]
    public int 預金種目 { get; set; }

    /// <summary>
    /// 口座番号 (N(7))  
    /// 右詰め・残り前「0」
    /// </summary>
    [Index(7)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 7)]
    public int 口座番号 { get; set; }

    /// <summary>
    /// 受取人名 (C(30))  
    /// 左詰め・残りスペース
    /// </summary>
    [Index(8)]
    [TypeConverter(typeof(CharacterTypeConverter), 30)]
    public string 受取人名 { get; set; } = "";

    /// <summary>
    /// 振込金額 (N(10))  
    /// 右詰め・残り前「0」
    /// </summary>
    [Index(9)]
    [TypeConverter(typeof(NumberTypeConverter<decimal>), 10)]
    public decimal 振込金額 { get; set; }

    /// <summary>
    /// 新規コード (N(1))  
    /// 1: 第1回振込分, 2: 変更分, 0: その他
    /// </summary>
    [Index(10)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 1)]
    public int 新規コード { get; set; }

    /// <summary>
    /// 顧客コード1 (N(10))  
    /// 依頼人が定めた受取人識別のための顧客コードを表わす。識別表示が空白の場合のみ。
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(11)]
    [TypeConverter(typeof(NumberTypeConverter<decimal?>), 10)]
    public decimal? 顧客コード1 { get; set; }

    /// <summary>
    /// 顧客コード2 (N(10))  
    /// 依頼人が定めた受取人識別のための顧客コードを表わす。識別表示が空白の場合のみ。
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(12)]
    [TypeConverter(typeof(NumberTypeConverter<decimal?>), 10)]
    public decimal? 顧客コード2 { get; set; }

    /// <summary>
    /// 振込指定区分 (N(1))  
    /// 7: テレ振込, 8: 文書振込
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(13)]
    [TypeConverter(typeof(NumberTypeConverter<int?>), 1)]
    public int? 振込指定区分 { get; set; }

    /// <summary>
    /// 識別表示 (C(1))  
    /// 「Y」またはスペース
    /// 本欄に「Y」表示を付した場合は、「EDI情報」を含む。 
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(14)]
    [BooleanTrueValues("Y")]
    [BooleanFalseValues(" ")]
    public bool 識別表示 { get; set; }

    /// <summary>
    /// ダミー (C(7))  
    /// スペース
    /// </summary>
    [Index(15)]
    [TypeConverter(typeof(CharacterTypeConverter), 7)]
    public string ダミー { get; set; } = "";
}