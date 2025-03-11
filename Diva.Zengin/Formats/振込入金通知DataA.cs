using CsvHelper.Configuration.Attributes;
using Diva.Zengin.Converters;

namespace Diva.Zengin.Formats;

/// <summary>
/// 振込入金通知のデータ・レコードを表すクラス。
/// フォーマットA
/// </summary>
[FluentSetter]
[IndexToLengthMap]
public partial class 振込入金通知DataA : IRecord
{
    /// <summary>
    /// データ区分 (N(1))
    /// 2: データ・レコード
    /// </summary>
    [Index(0)]
    [TypeConverter(typeof(NumberTypeConverter<データ区分>), 1)]
    public データ区分 データ区分 { get; set; } = データ区分.Data;

    /// <summary>
    /// 照会番号 (N(6))
    /// 銀行が採番した照会用番号
    /// 右詰め残り前「0」
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(1)]
    [TypeConverter(typeof(NumberTypeConverter<int?>), 6)]
    public int? 照会番号 { get; set; }

    /// <summary>
    /// 勘定日 (N(6))
    /// YYMMDD (和暦)
    /// </summary>
    [Index(2)]
    [TypeConverter(typeof(JapaneseEraDateTypeConverter))]
    public DateOnly 勘定日 { get; set; }

    /// <summary>
    /// 起算日 (N(6))
    /// 入金の起算日を表わす。 YYMMDD (和暦)
    /// </summary>
    [Index(3)]
    [TypeConverter(typeof(JapaneseEraDateTypeConverter))]
    public DateOnly 起算日 { get; set; }

    /// <summary>
    /// 金額 (N(10))
    /// 右詰め残り前「0」
    /// </summary>
    [Index(4)]
    [TypeConverter(typeof(NumberTypeConverter<decimal>), 10)]
    public decimal 金額 { get; set; }

    /// <summary>
    /// うち他店券金額 (N(10))
    /// 入金額中の他店券金額。 右詰め残り前「0」
    /// </summary>
    [Index(5)]
    [TypeConverter(typeof(NumberTypeConverter<decimal>), 10)]
    public decimal うち他店券金額 { get; set; }

    /// <summary>
    /// 振込依頼人コード (N(10))
    /// 仕向銀行からの為替通知に記載された振込依頼人の識別コード。 右詰め残り前「0」
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(6)]
    [TypeConverter(typeof(NumberTypeConverter<decimal?>), 10)]
    public decimal? 振込依頼人コード { get; set; }

    /// <summary>
    /// 振込依頼人名 (C(48))
    /// 左詰め残りスペース
    /// </summary>
    [Index(7)]
    [TypeConverter(typeof(CharacterTypeConverter), 48)]
    public string 振込依頼人名 { get; set; }

    /// <summary>
    /// 仕向銀行名 (C(15))
    /// 左詰め残りスペース
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(8)]
    [TypeConverter(typeof(CharacterTypeConverter), 15)]
    public string? 仕向銀行名 { get; set; }

    /// <summary>
    /// 仕向店名 (C(15))
    /// 左詰め残りスペース
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(9)]
    [TypeConverter(typeof(CharacterTypeConverter), 15)]
    public string? 仕向店名 { get; set; }

    /// <summary>
    /// 取消区分 (N(1))
    /// 振込入金通知を取り消す場合に使用する。 1: 取消
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(10)]
    [TypeConverter(typeof(NumberTypeConverter<int?>), 1)]
    public int? 取消区分 { get; set; }

    /// <summary>
    /// EDI情報 (C(20))
    /// 仕向銀行からの為替通知に記載されたEDI情報。 左詰め残りスペース
    /// </summary>
    /// <remarks>任意項目</remarks>
    [Index(11)]
    [TypeConverter(typeof(CharacterTypeConverter), 20)]
    public string? EDI情報 { get; set; }

    /// <summary>
    /// ダミー (C(52))
    /// スペースとする。
    /// </summary>
    [Index(12)]
    [TypeConverter(typeof(CharacterTypeConverter), 52)]
    public string ダミー { get; set; } = "";
}