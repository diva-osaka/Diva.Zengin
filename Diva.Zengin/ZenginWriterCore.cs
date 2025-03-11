using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Diva.Zengin.Configuration;
using Diva.Zengin.Mappers;
using Diva.Zengin.Records;
using 総合振込Data = Diva.Zengin.Records.総合振込Data;

namespace Diva.Zengin;

internal class ZenginWriterCore<TSequence, THeader, TData, TTrailer, TEnd>
    where TSequence : ISequence<THeader, TData, TTrailer, TEnd>, new()
    where THeader : IRecord, new()
    where TData : IRecord, new()
    where TTrailer : IRecord, new()
    where TEnd : IRecord, new()
{
    private readonly string? _path;
    private readonly Stream? _stream;

    static ZenginWriterCore()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    public ZenginWriterCore(string path)
    {
        _path = path ?? throw new ArgumentNullException(nameof(path));
    }

    public ZenginWriterCore(Stream stream)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
    }

    public async Task WriteAsync(List<TSequence> sequences, ZenginConfiguration config)
    {
        if (sequences == null || sequences.Count == 0)
            throw new ArgumentNullException(nameof(sequences));

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var encoding = Encoding.GetEncoding("shift_jis");

        Stream? streamToClose = null;
        Stream streamToUse;

        if (_stream != null)
        {
            streamToUse = _stream;
        }
        else if (_path != null)
        {
            streamToUse = File.Create(_path);
            streamToClose = streamToUse;
        }
        else
        {
            throw new InvalidOperationException("Both path and stream cannot be null.");
        }

        var csvConfiguration = config.FileFormat switch
        {
            FileFormat.Zengin => new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false, Delimiter = "", ShouldQuote = _ => false,
            },
            FileFormat.Csv => new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false, ShouldQuote = _ => true,
            },
            _ => throw new ArgumentOutOfRangeException(nameof(config), config, null)
        };

        try
        {
            await using var writer = new StreamWriter(streamToUse, encoding, leaveOpen: streamToClose == null);
            await using var csv = new CsvWriter(writer, csvConfiguration);
            RegisterClassMaps(csv);

            foreach (var sequence in sequences)
            {
                await WriteSequenceAsync(csv, sequence).ConfigureAwait(false);
            }
        }
        finally
        {
            if (streamToClose != null)
                await streamToClose.DisposeAsync().ConfigureAwait(false);
        }
    }

    private static async Task WriteSequenceAsync(CsvWriter csv, TSequence sequence)
    {
        // Write header
        csv.WriteRecord(sequence.Header);
        await csv.NextRecordAsync().ConfigureAwait(false);

        // Write data records
        foreach (var data in sequence.DataList)
        {
            if (typeof(TSequence) == typeof(総合振込) && data is 総合振込Data 総合振込Data)
            {
                if (総合振込Data.識別表示)
                {
                    csv.WriteRecord(総合振込Data.ToWriteData1());
                }
                else
                {
                    var data2 = 総合振込Data.ToWriteData2();
                    csv.WriteRecord(data2);
                }
            }
            else
            {
                csv.WriteRecord(data);
            }

            await csv.NextRecordAsync().ConfigureAwait(false);
        }

        // Write trailer
        csv.WriteRecord(sequence.Trailer);
        await csv.NextRecordAsync().ConfigureAwait(false);

        // Write end record
        csv.WriteRecord(sequence.End);
        await csv.NextRecordAsync().ConfigureAwait(false);
    }

    private static void RegisterClassMaps(CsvWriter csv)
    {
    }
}