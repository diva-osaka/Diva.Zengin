using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Diva.Zengin.Formats;

namespace Diva.Zengin;

internal class ZenginReaderCore<TSequence, THeader, TData, TTrailer, TEnd>
    where TSequence : ISequence<THeader, TData, TTrailer, TEnd>, new()
    where THeader : IRecord, new()
    where TData : IRecord, new()
    where TTrailer : IRecord, new()
    where TEnd : IRecord, new()
{
    private readonly string? _path;
    private readonly Stream? _stream;

    static ZenginReaderCore()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    public ZenginReaderCore(string path)
    {
        _path = path ?? throw new ArgumentNullException(nameof(path));
    }

    public ZenginReaderCore(Stream stream)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
    }

    public async Task<List<TSequence>> ReadAsync()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var encoding = Encoding.GetEncoding("shift_jis");

        // 内部で作成したストリームのみを閉じる
        Stream? streamToClose = null;
        Stream streamToUse;

        if (_stream != null)
        {
            streamToUse = _stream; // 外部から提供されたストリームを使用
        }
        else if (_path != null)
        {
            streamToUse = File.OpenRead(_path); // 内部でストリームを作成
            streamToClose = streamToUse; // このストリームは閉じる必要がある
        }
        else
        {
            throw new InvalidOperationException("Both path and stream cannot be null.");
        }

        try
        {
            using var reader = new StreamReader(streamToUse, encoding, leaveOpen: streamToClose == null);
            return await ReadFromStreamReaderAsync(reader);
        }
        finally
        {
            if (streamToClose != null)
                await streamToClose.DisposeAsync();
        }
    }

    private static async Task<List<TSequence>> ReadFromStreamReaderAsync(TextReader reader)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false
        };

        using var csv = new CsvReader(reader, config);
        RegisterClassMaps(csv);

        var sequences = new List<TSequence>();
        TSequence? currentSequence = default;

        while (await csv.ReadAsync())
        {
            if (!int.TryParse(csv.GetField(0), out var dataTypeValue))
            {
                throw new FormatException("The first field is not a numeric data type identifier.");
            }

            if (!Enum.IsDefined(typeof(データ区分), dataTypeValue))
            {
                throw new FormatException($"Unknown data type: {dataTypeValue}");
            }

            var dataType = (データ区分)dataTypeValue;
            
            currentSequence ??= new TSequence();
            
            ProcessRecord(csv, dataType, currentSequence);

            if (dataType != データ区分.End) 
                continue;
            
            // Add the current sequence to the list
            sequences.Add(currentSequence);
            currentSequence = default;
        }

        return sequences;
    }

    private static void RegisterClassMaps(CsvReader csv)
    {
        if (typeof(TSequence) == typeof(総合振込))
        {
            csv.Context.RegisterClassMap<総合振込DataReadMap>();
        }
    }

    private static void ProcessRecord(CsvReader csv, データ区分 dataType, TSequence sequence)
    {
        switch (dataType)
        {
            case データ区分.Header:
                sequence.Header = csv.GetRecord<THeader>();
                break;
            case データ区分.Data:
                sequence.DataList.Add(csv.GetRecord<TData>());
                break;
            case データ区分.Trailer:
                sequence.Trailer = csv.GetRecord<TTrailer>();
                break;
            case データ区分.End:
                sequence.End = csv.GetRecord<TEnd>();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(dataType), dataType, null);
        }
    }
}