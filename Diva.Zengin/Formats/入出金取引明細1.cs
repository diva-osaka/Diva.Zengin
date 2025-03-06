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

    public void SetTrailerValues()
    {
        Trailer.データ区分 = データ区分.Trailer;
        Trailer.データレコード件数 = DataList.Count;
        // TODO: 各項目の計算
    }

    public void SetEndValues()
    {
        End.データ区分 = データ区分.End;
        End.レコード総件数 = DataList.Count;
        // TODO: 各項目の計算
    }
}