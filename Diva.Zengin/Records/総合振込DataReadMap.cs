using CsvHelper;
using CsvHelper.Configuration;
using Diva.Zengin.Converters;

namespace Diva.Zengin.Records;

/// <summary>
/// 総合振込データの列マッピング定義（読み込み時）
/// </summary>
public sealed class 総合振込DataReadMap : ClassMap<総合振込Data>
{
    public 総合振込DataReadMap()
    {
        // 基本列のインデックスマッピング（型変換は属性から使われる）
        Map(m => m.データ区分)
            .Index(0)
            .TypeConverter(new NumberTypeConverter<データ区分>());
        Map(m => m.被仕向銀行番号)
            .Index(1)
            .TypeConverter(new NumberTypeConverter<int>(4));
        Map(m => m.被仕向銀行名)
            .Index(2)
            .TypeConverter(new CharacterTypeConverter(15));
        Map(m => m.被仕向支店番号)
            .Index(3)
            .TypeConverter(new NumberTypeConverter<int>(3));
        Map(m => m.被仕向支店名)
            .Index(4)
            .TypeConverter(new CharacterTypeConverter(15));
        Map(m => m.手形交換所番号)
            .Index(5)
            .TypeConverter(new NumberTypeConverter<int?>(4));
        Map(m => m.預金種目)
            .Index(6)
            .TypeConverter(new NumberTypeConverter<int>());
        Map(m => m.口座番号)
            .Index(7)
            .TypeConverter(new NumberTypeConverter<int>(7));
        Map(m => m.受取人名)
            .Index(8)
            .TypeConverter(new CharacterTypeConverter(30));
        Map(m => m.振込金額)
            .Index(9)
            .TypeConverter(new NumberTypeConverter<decimal>(10));
        Map(m => m.新規コード)
            .Index(10)
            .TypeConverter(new NumberTypeConverter<int>());

        // 識別表示 C(1) は常に末尾から2番目
        // "Y" または " "
        Map(m => m.識別表示).Convert(Get識別表示Value);

        // 顧客コード1 - 識別表示がY以外の場合のみ使用
        Map(m => m.顧客コード1).Convert(args =>
            !Get識別表示Value(args)
                ? NumberConverter.ConvertFromString<decimal?>(args.Row.GetField(11), 10)
                : null);

        // 顧客コード2 - 識別表示がY以外の場合のみ使用
        Map(m => m.顧客コード2).Convert(args =>
            !Get識別表示Value(args)
                ? NumberConverter.ConvertFromString<decimal?>(args.Row.GetField(12), 10)
                : null);

        // EDI情報 - 識別表示がYの場合のみ使用
        Map(m => m.EDI情報).Convert(args =>
            Get識別表示Value(args) ? CharacterConverter.ConvertFromString(args.Row.GetField(11), true) : null);

        // 振込指定区分 - 列位置が動的
        Map(m => m.振込指定区分).Convert(args =>
        {
            // 振込指定区分のインデックスは識別表示によって変わる
            var index = Get識別表示Value(args) ? 12 : 13;
            return NumberConverter.ConvertFromString<int?>(args.Row.GetField(index));
        });

        // ダミーは常に末尾 - 動的位置決定
        Map(m => m.ダミー).Convert(args =>
        {
            var field = args.Row.GetField(args.Row.ColumnCount - 1);
            return CharacterConverter.ConvertFromString(field);
        });
    }

    /// <summary>
    /// 識別表示の値を取得
    /// </summary>
    private static bool Get識別表示Value(ConvertFromStringArgs args)
    {
        var field = args.Row.GetField(args.Row.ColumnCount - 2);
        return field == "Y";
    }
}