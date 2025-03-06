namespace Diva.Zengin.Formats;

public interface ISequence;

public interface ISequence<THeader, TData, TTrailer, TEnd> : ISequence
{
    THeader Header { get; set; }
    List<TData> DataList { get; set; }
    TTrailer Trailer { get; set; }
    TEnd End { get; set; }

    void SetTrailerValues();
    void SetEndValues();
}