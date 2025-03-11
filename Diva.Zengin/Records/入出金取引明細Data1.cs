using CsvHelper.Configuration.Attributes;
using Diva.Zengin.Converters;

namespace Diva.Zengin.Records;

/// <summary>
/// 入出金取引明細のデータ・レコードを表すクラス。
/// （普通預金・当座預金・貯蓄預金の場合）
/// </summary>
[FluentSetter]
[IndexToLengthMap]
public partial class 入出金取引明細Data1 : IRecord
{
    /// <summary>
    /// データ区分 (N(1))
    /// 2：データ・レコード 
    /// </summary>
    [Index(0)]
    [TypeConverter(typeof(NumberTypeConverter<データ区分>), 1)]
    public データ区分 データ区分 { get; set; } = データ区分.Data;

    /// <summary>
    /// 照会番号 (N(8))
    /// 銀行が採番した照会用番号。
    /// </summary>
    [Index(1)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 8)]
    public int 照会番号 { get; set; }

    /// <summary>
    /// 勘定日 (N(6))
    /// YYMMDD（和暦）
    /// </summary>
    [Index(2)]
    [TypeConverter(typeof(JapaneseEraDateTypeConverter))]
    public DateOnly 勘定日 { get; set; }

    /// <summary>
    /// 預入・払出日 (N(6))
    /// 入金・出金の起算日を表す。 YYMMDD（和暦)
    /// </summary>
    /// <remarks>通常は勘定日と同日であるが、その場合には勘定日と同一年月日を記入する。</remarks>
    [Index(3)]
    [TypeConverter(typeof(JapaneseEraDateTypeConverter))]
    public DateOnly 預入払出日 { get; set; }

    /// <summary>
    /// 入払区分 (N(1))
    /// 1: 入金, 2: 出金
    /// </summary>
    [Index(4)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 1)]
    public int 入払区分 { get; set; }

    /// <summary>
    /// 取引区分 (N(2))
    /// 10: 現金,  11: 振込,  12: 他店券入金, 13: 交換（取立入金および交換払）, 14: 振替, 18: その他, 19: 訂正 
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(5)]
    [TypeConverter(typeof(NumberTypeConverter<int?>), 2)]
    public int? 取引区分 { get; set; }

    /// <summary>
    /// 取引金額 (N(12))
    /// 右詰め残り前「0」
    /// </summary>
    [Index(6)]
    [TypeConverter(typeof(NumberTypeConverter<decimal>), 12)]
    public decimal 取引金額 { get; set; }

    /// <summary>
    /// うち他店券金額 (N(12))
    /// 取引金額中の他店券金額。
    /// </summary>
    [Index(7)]
    [TypeConverter(typeof(NumberTypeConverter<decimal>), 12)]
    public decimal うち他店券金額 { get; set; }

    /// <summary>
    /// 交換呈示日 (N(6))
    /// 証券類の交換呈示日を表す。YYMMDD（和暦)
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(8)]
    [TypeConverter(typeof(JapaneseEraDateTypeConverter))]
    public DateOnly? 交換呈示日 { get; set; }

    /// <summary>
    /// 不渡返還日 (N(6))
    /// 証券類の不渡返還日を表す。YYMMDD（和暦)
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(9)]
    [TypeConverter(typeof(JapaneseEraDateTypeConverter))]
    public DateOnly? 不渡返還日 { get; set; }

    /// <summary>
    /// 手形・小切手区分 (N(1))
    /// 1: 小切手, 2: 約束手形, 3: 為替手形
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(10)]
    [TypeConverter(typeof(NumberTypeConverter<int?>), 1)]
    public int? 手形小切手区分 { get; set; }

    /// <summary>
    /// 手形・小切手番号 (N(7))
    /// 右詰め残り前「0」
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(11)]
    [TypeConverter(typeof(NumberTypeConverter<int?>), 7)]
    public int? 手形小切手番号 { get; set; }

    /// <summary>
    /// 僚店番号 (N(3))
    /// 取引のあった店を表わす (統一店番号)
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(12)]
    [TypeConverter(typeof(NumberTypeConverter<int?>), 3)]
    public int? 僚店番号 { get; set; }

    /// <summary>
    /// 振込依頼人コード (N(10))
    /// 仕向銀行からの為替通知に記載された振込依頼人の識別コード。
    /// 右詰め残り前「0」
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(13)]
    [TypeConverter(typeof(NumberTypeConverter<decimal?>), 10)]
    public decimal? 振込依頼人コード { get; set; }

    /// <summary>
    /// 振込依頼人名または契約者番号 (C(48))
    /// 入払区分が「1」(入金) の場合: 振込依頼人名
    /// 入払区分が「2」(出金) の場合: 預金口座振替の契約者番号 (左20桁)
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(14)]
    [TypeConverter(typeof(CharacterTypeConverter), 48)]
    public string? 振込依頼人名または契約者番号 { get; set; }

    /// <summary>
    /// 仕向銀行名 (C(15))
    /// 左詰め残りスペース
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(15)]
    [TypeConverter(typeof(CharacterTypeConverter), 15)]
    public string? 仕向銀行名 { get; set; }

    /// <summary>
    /// 仕向店名 (C(15))
    /// 左詰め残りスペース
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(16)]
    [TypeConverter(typeof(CharacterTypeConverter), 15)]
    public string? 仕向店名 { get; set; }

    /// <summary>
    /// 摘要内容 (C(20))
    /// 左詰め残りスペース
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(17)]
    [TypeConverter(typeof(CharacterTypeConverter), 20)]
    public string? 摘要内容 { get; set; }

    /// <summary>
    /// EDI情報 (C(20))
    /// 仕向銀行からの為替通知に記載されたEDI情報。
    /// 左詰め残りスペース
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(18)]
    [TypeConverter(typeof(CharacterTypeConverter), 20)]
    public string? EDI情報 { get; set; }
    
    /// <summary>
    /// ダミー (C(1))
    /// スペースとする。
    /// </summary>
    [Index(19)]
    [TypeConverter(typeof(CharacterTypeConverter), 1)]
    public string ダミー { get; set; } = "";
}
