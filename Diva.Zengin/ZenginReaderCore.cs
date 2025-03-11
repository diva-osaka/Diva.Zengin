using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Diva.Zengin.Formats;

namespace Diva.Zengin;

internal class ZenginReaderCore<TSequence, THeader, TData, TTrailer, TEnd>
    where TSequence : ISequence<THeader, TData, TTrailer, TEnd>, new()
    where THeader : class, IRecord, IIndexToLengthMap, new()
    where TData : class, IRecord, IIndexToLengthMap, new()
    where TTrailer : class, IRecord, IIndexToLengthMap, new()
    where TEnd : class, IRecord, IIndexToLengthMap, new()
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

    public async Task<List<TSequence>> ReadAsync(FileFormat format)
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
            if (format == FileFormat.Zengin)
            {
                // When format is Zengin, convert to CSV first
                using var zenginReader = new StreamReader(streamToUse, encoding, leaveOpen: streamToClose == null);
                using var convertedStream = new MemoryStream();
                await using var csvWriter = new StreamWriter(convertedStream, encoding, leaveOpen: true);

                // Read Zengin format and write as CSV
                while (await zenginReader.ReadLineAsync().ConfigureAwait(false) is { } line)
                {
                    // Convert Zengin format line to CSV format
                    // This will need the specific Zengin-to-CSV conversion logic
                    var csvLine = ConvertZenginLineToCsv(line);
                    await csvWriter.WriteLineAsync(csvLine).ConfigureAwait(false);
                }

                await csvWriter.FlushAsync().ConfigureAwait(false);
                convertedStream.Position = 0;

                // Read from the converted CSV stream
                using var csvReader = new StreamReader(convertedStream, encoding);
                return await ReadFromStreamReaderAsync(csvReader).ConfigureAwait(false);
            }
            else
            {
                // Original CSV processing
                using var reader = new StreamReader(streamToUse, encoding, leaveOpen: streamToClose == null);
                return await ReadFromStreamReaderAsync(reader).ConfigureAwait(false);
            }
        }
        finally
        {
            if (streamToClose != null)
                await streamToClose.DisposeAsync().ConfigureAwait(false);
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

        while (await csv.ReadAsync().ConfigureAwait(false))
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

    /// <summary>
    /// 全銀テキスト形式をCSV形式に変換します。
    /// </summary>
    /// <param name="zenginLine"></param>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    private static string ConvertZenginLineToCsv(string zenginLine)
    {
        var dataTypeValue = int.Parse(zenginLine[0].ToString());
        if (!Enum.IsDefined(typeof(データ区分), dataTypeValue))
        {
            throw new FormatException($"Unknown data type: {dataTypeValue}");
        }

        var dataType = (データ区分)dataTypeValue;
        SortedDictionary<int, int> indexToLengthMap;

        // Get the appropriate index-to-length map based on record type
        switch (dataType)
        {
            case データ区分.Header:
                indexToLengthMap = THeader.GetIndexToLengthMap();
                break;
            case データ区分.Data:
                if (typeof(TData) == typeof(総合振込Data))
                {
                    indexToLengthMap = zenginLine[112] == 'Y'
                        ? 総合振込WriteData1.GetIndexToLengthMap()
                        : 総合振込WriteData2.GetIndexToLengthMap();
                }
                else
                {
                    indexToLengthMap = TData.GetIndexToLengthMap();
                }
                break;
            case データ区分.Trailer:
                indexToLengthMap = TTrailer.GetIndexToLengthMap();
                break;
            case データ区分.End:
                indexToLengthMap = TEnd.GetIndexToLengthMap();
                break;
            default:
                throw new FormatException();
        }

        // Parse the Zengin line into fields using the index-to-length map
        var fields = new List<string>();
        var startPos = 0;

        foreach (var index in indexToLengthMap.Keys.OrderBy(k => k).ToList())
        {
            var length = indexToLengthMap[index];
            var field = zenginLine.Substring(startPos, length);
            fields.Add(field);
            startPos += length;
        }

        // Convert to CSV format
        return string.Join(",", fields.Select(EscapeCsvField));

        string EscapeCsvField(string field)
        {
            if (field.Contains(',') || field.Contains('"') || field.Contains('\n'))
            {
                return $"\"{field.Replace("\"", "\"\"")}\"";
            }

            return field;
        }
    }
}