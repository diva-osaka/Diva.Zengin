using CsvHelper.Configuration.Attributes;
using Diva.Zengin.Converters;

namespace Diva.Zengin.Formats;

/// <summary>
/// 振込入金通知のヘッダー・レコードを表すクラス。
/// </summary>
[FluentSetter]
[IndexToLengthMap]
public partial class 振込入金通知Header : IRecord
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
    /// 01: 振込入金通知
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
    /// 預金種目 (N(1))
    /// 1: 普通預金, 2: 当座預金, 4: 貯蓄預金
    /// </summary>
    [Index(10)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 1)]
    public int 預金種目 { get; set; }

    /// <summary>
    /// 口座番号 (N(7))
    /// 右詰め残り前「0」
    /// </summary>
    [Index(11)]
    [TypeConverter(typeof(NumberTypeConverter<int>), 7)]
    public int 口座番号 { get; set; }

    /// <summary>
    /// 口座名 (C(40))
    /// 左詰め残りスペース
    /// </summary>
    [Index(12)]
    [TypeConverter(typeof(CharacterTypeConverter), 40)]
    public string 口座名 { get; set; }

    /// <summary>
    /// ダミー (C(93))
    /// スペースとする。
    /// </summary>
    [Index(13)]
    [TypeConverter(typeof(CharacterTypeConverter), 93)]
    public string ダミー { get; set; } = "";
}