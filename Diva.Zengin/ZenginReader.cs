using Diva.Zengin.Configuration;
using Diva.Zengin.Records;

namespace Diva.Zengin;

public class ZenginReader<T>
{
    private readonly string? _path;
    private readonly Stream? _stream;
    private readonly ZenginConfiguration? _config;

    public ZenginReader(string path)
    {
        _path = path ?? throw new ArgumentNullException(nameof(path));
    }

    public ZenginReader(Stream stream)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
    }

    public ZenginReader(string path, ZenginConfiguration config)
    {
        _path = path ?? throw new ArgumentNullException(nameof(path));
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public ZenginReader(Stream stream, ZenginConfiguration config)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }
    
    public async Task<T?> ReadAsync()
    {
        if (_path == null && _stream == null)
            throw new InvalidOperationException("Both path and stream cannot be null.");

        // Determine the sequence type and container type
        Type sequenceType;
        bool isSingleResult;
        Type? containerType = null;

        if (typeof(T) == typeof(振込入金通知A) ||
            typeof(T) == typeof(List<振込入金通知A>) ||
            typeof(T) == typeof(振込入金通知A[]))
        {
            sequenceType = typeof(振込入金通知A);
            isSingleResult = typeof(T) == typeof(振込入金通知A);
            if (!isSingleResult)
                containerType = typeof(T) == typeof(List<振込入金通知A>) ? typeof(List<>) : typeof(Array);
        }
        else if (typeof(T) == typeof(入出金取引明細1) ||
                 typeof(T) == typeof(List<入出金取引明細1>) ||
                 typeof(T) == typeof(入出金取引明細1[]))
        {
            sequenceType = typeof(入出金取引明細1);
            isSingleResult = typeof(T) == typeof(入出金取引明細1);
            if (!isSingleResult)
                containerType = typeof(T) == typeof(List<入出金取引明細1>) ? typeof(List<>) : typeof(Array);
        }
        else if (typeof(T) == typeof(総合振込) ||
                 typeof(T) == typeof(List<総合振込>) ||
                 typeof(T) == typeof(総合振込[]))
        {
            sequenceType = typeof(総合振込);
            isSingleResult = typeof(T) == typeof(総合振込);
            if (!isSingleResult)
                containerType = typeof(T) == typeof(List<総合振込>) ? typeof(List<>) : typeof(Array);
        }
        else
        {
            throw new NotSupportedException($"Unsupported type: {typeof(T)}");
        }

        // Read the appropriate sequence type
        if (sequenceType == typeof(振込入金通知A))
        {
            var result =
                await CreateReader<振込入金通知A, 振込入金通知Header, 振込入金通知DataA, 振込入金通知Trailer, 振込入金通知End>(_path, _stream,
                    _config).ConfigureAwait(false);

            if (result.Count == 0 && isSingleResult)
                return default; // null

            return isSingleResult
                ? (T)(object)result.First()
                : ConvertToContainer(result, containerType!);
        }

        if (sequenceType == typeof(入出金取引明細1))
        {
            var result =
                await CreateReader<入出金取引明細1, 入出金取引明細Header, 入出金取引明細Data1, 入出金取引明細Trailer, 入出金取引明細End>(_path, _stream,
                    _config).ConfigureAwait(false);

            if (result.Count == 0 && isSingleResult)
                return default; // null

            return isSingleResult
                ? (T)(object)result.First()
                : ConvertToContainer(result, containerType!);
        }

        if (sequenceType == typeof(総合振込))
        {
            var result = await CreateReader<総合振込, 総合振込Header, 総合振込Data, 総合振込Trailer, 総合振込End>(_path, _stream, _config)
                .ConfigureAwait(false);

            if (result.Count == 0 && isSingleResult)
                return default; // null

            return isSingleResult
                ? (T)(object)result.First()
                : ConvertToContainer(result, containerType!);
        }

        throw new NotSupportedException($"Unsupported type: {typeof(T)}");
    }

    private static T ConvertToContainer<TSequence>(List<TSequence> list, Type containerType)
        where TSequence : ISequence
    {
        if (containerType == typeof(List<>))
            return (T)(object)list;
        if (containerType == typeof(Array))
            return (T)(object)list.ToArray();

        throw new ArgumentException($"Unsupported container type: {containerType}");
    }

    private static async Task<List<TSequence>> CreateReader<TSequence, THeader, TData, TTrailer, TEnd>(string? path,
        Stream? stream, ZenginConfiguration? config)
        where TSequence : ISequence<THeader, TData, TTrailer, TEnd>, new()
        where THeader : class, IRecord, IIndexToLengthMap, new()
        where TData : class, IRecord, IIndexToLengthMap, new()
        where TTrailer : class, IRecord, IIndexToLengthMap, new()
        where TEnd : class, IRecord, IIndexToLengthMap, new()
    {
        var reader = stream != null
            ? new ZenginReaderCore<TSequence, THeader, TData, TTrailer, TEnd>(stream)
            : new ZenginReaderCore<TSequence, THeader, TData, TTrailer, TEnd>(path!);

        config ??= new ZenginConfiguration();
        return await reader.ReadAsync(config).ConfigureAwait(false);
    }
}