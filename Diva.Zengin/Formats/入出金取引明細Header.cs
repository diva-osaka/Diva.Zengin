using CsvHelper.Configuration.Attributes;
using Diva.Zengin.Converters;

namespace Diva.Zengin.Formats;

/// <summary>
/// 入出金取引明細のヘッダー・レコードを表すクラス。
/// </summary>
public class 入出金取引明細Header : IRecord
{
    /// <summary>
    /// データ区分 (N(1))  
    /// 1: ヘッダーレコード
    /// </summary>
    [Index(0)]
    [TypeConverter(typeof(NumberTypeConverter<データ区分>), 1)]
    public データ区分 データ区分 { get; set; } = データ区分.Header;

    /// <summary>
    /// 種別コード区分 (N(2))  
    /// 03: 入出金取引明細
    /// </summary>
    [Index(1)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 2)]
    public int 種別コード区分 { get; set; }

    /// <summary>
    /// コード区分 (N(1))  
    /// 0: JIS, 1: EBCDIC
    /// </summary>
    [Index(2)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 1)]
    public int コード区分 { get; set; }

    /// <summary>
    /// 作成日 (N(6))  
    /// YYMMDD (和暦)
    /// </summary>
    [Index(3)]
    [TypeConverter(typeof(JapaneseEraDateTypeConverter))]
    public DateOnly 作成日 { get; set; }

    /// <summary>
    /// 勘定日（自） (N(6))  
    /// YYMMDD (和暦)
    /// </summary>
    [Index(4)]
    [TypeConverter(typeof(JapaneseEraDateTypeConverter))]
    public DateOnly 勘定日開始 { get; set; }

    /// <summary>
    /// 勘定日（至） (N(6))  
    /// YYMMDD (和暦)
    /// </summary>
    [Index(5)]
    [TypeConverter(typeof(JapaneseEraDateTypeConverter))]
    public DateOnly 勘定日終了 { get; set; }

    /// <summary>
    /// 銀行コード (N(4))  
    /// 統一金融機関番号
    /// </summary>
    [Index(6)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 4)]
    public int 銀行コード { get; set; }

    /// <summary>
    /// 銀行名 (C(15))  
    /// 左詰め残りスペース
    /// </summary>
    [Index(7)]
    [TypeConverter(typeof(CharacterTypeConverter), 15)]
    public string 銀行名 { get; set; }

    /// <summary>
    /// 支店コード (N(3))  
    /// 統一店番号
    /// </summary>
    [Index(8)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 3)]
    public int 支店コード { get; set; }

    /// <summary>
    /// 支店名 (C(15))  
    /// 左詰め残りスペース
    /// </summary>
    [Index(9)]
    [TypeConverter(typeof(CharacterTypeConverter), 15)]
    public string 支店名 { get; set; }

    /// <summary>
    /// ダミー (N(3))  
    /// 予備エリア (0固定)
    /// </summary>
    [Index(10)]
    [TypeConverter(typeof(CharacterTypeConverter), 3)]
    public string ダミー { get; set; } = "000";

    /// <summary>
    /// 預金種目 (N(1))  
    /// 1: 普通預金, 2: 当座預金, 4: 貯蓄預金, 5: 通知預金, 6: 定期預金, 7: 積立定期預金
    /// </summary>
    [Index(11)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 1)]
    public int 預金種目 { get; set; }

    /// <summary>
    /// 口座番号 (N(10))  
    /// 右詰め・前0埋め
    /// </summary>
    [Index(12)]
    [TypeConverter(typeof(NumberTypeConverter<decimal>), 10)]
    public decimal 口座番号 { get; set; }

    /// <summary>
    /// 口座名 (C(40))  
    /// 左詰め残りスペース
    /// </summary>
    [Index(13)]
    [TypeConverter(typeof(CharacterTypeConverter), 40)]
    public string 口座名 { get; set; }

    /// <summary>
    /// 貸越区分 (N(1))  
    /// 1: プラス, 2: マイナス
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(14)]
    [TypeConverter(typeof(NumberTypeConverter<int?>), 1)]
    public int? 貸越区分 { get; set; }

    /// <summary>
    /// 通帳・証書区分 (N(1))  
    /// 1: 通帳, 2: 証書
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(15)]
    [TypeConverter(typeof(NumberTypeConverter<int?>), 1)]
    public int? 通帳証書区分 { get; set; }

    /// <summary>
    /// 取引前残高 (N(14))  
    /// 右詰め・前0埋め
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(16)]
    [TypeConverter(typeof(NumberTypeConverter<decimal?>), 14)]
    public decimal? 取引前残高 { get; set; }

    /// <summary>
    /// ダミー (C(71))  
    /// スペースとする。
    /// </summary>
    [Index(17)]
    [TypeConverter(typeof(CharacterTypeConverter), 71)]
    public string ダミー2 { get; set; }
}