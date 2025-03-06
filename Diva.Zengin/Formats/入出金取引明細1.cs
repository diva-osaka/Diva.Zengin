namespace Diva.Zengin.Formats;

public class 入出金取引明細
{
    public 入出金取引明細Header Header { get; set; }
    public List<入出金取引明細Data1> DataList { get; set; }
    public 入出金取引明細Trailer Trailer { get; set; }
    public 入出金取引明細End End { get; set; }
}
