namespace Diva.Zengin.Formats;

public class 総合振込 : ISequence<総合振込Header, 総合振込Data, 総合振込Trailer, 総合振込End>
{
    public 総合振込Header Header { get; set; } = new();
    public List<総合振込Data> DataList { get; set; } = [];
    public 総合振込Trailer Trailer { get; set; } = new();
    public 総合振込End End { get; set; } = new();
    
    public void SetTrailerValues()
    {
        Trailer.データ区分 = データ区分.Trailer;
        Trailer.合計件数 = DataList.Count;
        // TODO: 各項目の計算
    }

    public void SetEndValues()
    {
        End.データ区分 = データ区分.End;
        // エンド・レコードはダミーのみ
    }
}