namespace Diva.Zengin.Formats;

/// <summary>
/// 入出金明細（普通預金・当座預金・貯蓄預金の場合）
/// </summary>
public class 入出金取引明細1 : ISequence<入出金取引明細Header, 入出金取引明細Data1, 入出金取引明細Trailer, 入出金取引明細End>
{
    public 入出金取引明細Header Header { get; set; } = new();
    public List<入出金取引明細Data1> DataList { get; set; } = [];
    public 入出金取引明細Trailer Trailer { get; set; } = new();
    public 入出金取引明細End End { get; set; } = new();

    /// <summary>
    /// トレーラー・レコードの項目を設定します。
    /// </summary>
    /// <remarks>貸越区分、取引後残高は設定されない。</remarks>
    public void SetTrailerValues()
    {
        Trailer.データ区分 = データ区分.Trailer;
        Trailer.データレコード件数 = DataList.Count;
        Trailer.入金件数 = DataList.Count(x => x.入払区分 == 1);
        Trailer.入金額合計 = DataList.Where(x => x.入払区分 == 1).Sum(x => x.取引金額);
        Trailer.出金件数 = DataList.Count(x => x.入払区分 == 2);
        Trailer.出金額合計 = DataList.Where(x => x.入払区分 == 2).Sum(x => x.取引金額);
    }

    /// <summary>
    /// エンド・レコードの項目を設定します。
    /// </summary>
    /// <remarks>口座数は設定されない</remarks>
    public void SetEndValues()
    {
        End.データ区分 = データ区分.End;
        End.レコード総件数 = DataList.Count + 3;
    }
}