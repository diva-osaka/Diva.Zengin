using Diva.Zengin.Configuration;
using Diva.Zengin.Records;
using 入出金取引明細Data1 = Diva.Zengin.Records.入出金取引明細Data1;
using 入出金取引明細End = Diva.Zengin.Records.入出金取引明細End;
using 入出金取引明細Header = Diva.Zengin.Records.入出金取引明細Header;
using 入出金取引明細Trailer = Diva.Zengin.Records.入出金取引明細Trailer;
using 振込入金通知DataA = Diva.Zengin.Records.振込入金通知DataA;
using 振込入金通知End = Diva.Zengin.Records.振込入金通知End;
using 振込入金通知Header = Diva.Zengin.Records.振込入金通知Header;
using 振込入金通知Trailer = Diva.Zengin.Records.振込入金通知Trailer;
using 総合振込Data = Diva.Zengin.Records.総合振込Data;
using 総合振込End = Diva.Zengin.Records.総合振込End;
using 総合振込Header = Diva.Zengin.Records.総合振込Header;
using 総合振込Trailer = Diva.Zengin.Records.総合振込Trailer;

namespace Diva.Zengin;

public class ZenginWriter<T>
{
    private readonly string? _path;
    private readonly Stream? _stream;
    private readonly ZenginConfiguration? _config;

    public ZenginWriter(string path)
    {
        _path = path ?? throw new ArgumentNullException(nameof(path));
    }

    public ZenginWriter(Stream stream)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
    }
    
    public ZenginWriter(string path, ZenginConfiguration config)
    {
        _path = path ?? throw new ArgumentNullException(nameof(path));
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public ZenginWriter(Stream stream, ZenginConfiguration config)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }
    
    public async Task WriteAsync(T input)
    {
        if (_path == null && _stream == null)
            throw new InvalidOperationException("Both path and stream cannot be null.");

        if (input == null)
            throw new ArgumentNullException(nameof(input));

        // Determine the sequence type based on T
        Type sequenceType;
        List<ISequence> sequences;

        switch (input)
        {
            case ISequence singleSequence:
                sequenceType = singleSequence.GetType();
                sequences = [singleSequence];
                break;
            case IEnumerable<ISequence> sequenceList:
            {
                var list = sequenceList.ToList();
                if (list.Count == 0)
                    return;

                sequenceType = list[0].GetType();
                sequences = list.ToList();
                break;
            }
            default:
                throw new NotSupportedException($"Unsupported type: {typeof(T)}");
        }

        // Handle specific sequence types
        if (sequenceType == typeof(振込入金通知A))
        {
            await CreateWriter<振込入金通知A, 振込入金通知Header, 振込入金通知DataA, 振込入金通知Trailer, 振込入金通知End>(
                _path, _stream, _config, sequences.Cast<振込入金通知A>().ToList());
            return;
        }

        if (sequenceType == typeof(入出金取引明細1))
        {
            await CreateWriter<入出金取引明細1, 入出金取引明細Header, 入出金取引明細Data1, 入出金取引明細Trailer, 入出金取引明細End>(
                _path, _stream, _config, sequences.Cast<入出金取引明細1>().ToList());
            return;
        }

        if (sequenceType == typeof(総合振込))
        {
            await CreateWriter<総合振込, 総合振込Header, 総合振込Data, 総合振込Trailer, 総合振込End>(
                _path, _stream, _config, sequences.Cast<総合振込>().ToList());
            return;
        }

        throw new NotSupportedException($"Unsupported type: {sequenceType}");
    }

    private static async Task CreateWriter<TSequence, THeader, TData, TTrailer, TEnd>(
        string? path, Stream? stream, ZenginConfiguration? config, List<TSequence> sequences)
        where TSequence : ISequence<THeader, TData, TTrailer, TEnd>, new()
        where THeader : IRecord, new()
        where TData : IRecord, new()
        where TTrailer : IRecord, new()
        where TEnd : IRecord, new()
    {
        var writer = stream != null
            ? new ZenginWriterCore<TSequence, THeader, TData, TTrailer, TEnd>(stream)
            : new ZenginWriterCore<TSequence, THeader, TData, TTrailer, TEnd>(path!);

        config ??= new ZenginConfiguration();
        await writer.WriteAsync(sequences, config).ConfigureAwait(false);
    }
}