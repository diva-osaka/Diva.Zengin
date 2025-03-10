namespace Diva.Zengin.Formats;

/// <summary>
/// 振込入金通知の全体を表すクラス。
/// フォーマットA
/// </summary>
public class 振込入金通知A : ISequence<振込入金通知Header, 振込入金通知DataA, 振込入金通知Trailer, 振込入金通知End>
{
    public 振込入金通知Header Header { get; set; } = new();
    public List<振込入金通知DataA> DataList { get; set; } = new();
    public 振込入金通知Trailer Trailer { get; set; } = new();
    public 振込入金通知End End { get; set; } = new();

    /// <summary>
    /// トレーラー・レコードの項目を設定します。
    /// </summary>
    public void SetTrailerValues()
    {
        Trailer.データ区分 = データ区分.Trailer;
        Trailer.振込合計件数 = DataList.Count;
        Trailer.振込合計金額 = DataList.Sum(x => x.金額);
        Trailer.取消合計件数 = DataList.Count(x => x.取消区分 == 1);
        Trailer.取消合計金額 = DataList.Where(x => x.取消区分 == 1).Sum(x => x.金額);
    }

    /// <summary>
    /// エンド・レコードの項目を設定します。
    /// </summary>
    public void SetEndValues()
    {
        End.データ区分 = データ区分.End;
        // エンド・レコードはダミーのみ
    }
}