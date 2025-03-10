using System.IO;
using System.Threading.Tasks;
using Diva.Zengin.Formats;
using Xunit;

namespace Diva.Zengin.Tests;

/// <summary>
/// レコードの各プロパティの値を乱数で生成し、ZenginWriterで書き込み、ZenginReaderで読み込み、元の値と一致することを確認する。
/// </summary>
public class WriteReadTest
{
    private static async Task WriteAsync<T>(T data, Stream stream, FileFormat format) where T : ISequence
    {
        var writer = new ZenginWriter<T>(stream);
        await writer.WriteAsync(data, format);
    }

    private static async Task<T?> ReadAsync<T>(Stream stream, FileFormat format) where T : ISequence
    {
        var reader = new ZenginReader<T>(stream);
        return await reader.ReadAsync(format);
    }

    [Theory]
    [InlineData(FileFormat.Zengin)]
    [InlineData(FileFormat.Csv)]
    public async Task 振込入金通知A_WriteReadTest(FileFormat format)
    {
        var originalData = Generator.GenerateRandom振込入金通知A();
        using var memoryStream = new MemoryStream();
        await WriteAsync(originalData, memoryStream, format);

        memoryStream.Seek(0, SeekOrigin.Begin); // Reset stream position for reading
        var readData = await ReadAsync<振込入金通知A>(memoryStream, format);

        Assert.NotNull(readData);

        // Compare Header properties
        Assert.Equal(originalData.Header.種別コード, readData.Header.種別コード);
        Assert.Equal(originalData.Header.コード区分, readData.Header.コード区分);
        Assert.Equal(originalData.Header.作成日, readData.Header.作成日);
        Assert.Equal(originalData.Header.勘定日開始, readData.Header.勘定日開始);
        Assert.Equal(originalData.Header.勘定日終了, readData.Header.勘定日終了);
        Assert.Equal(originalData.Header.銀行コード, readData.Header.銀行コード);
        Assert.Equal(originalData.Header.銀行名, readData.Header.銀行名);
        Assert.Equal(originalData.Header.支店コード, readData.Header.支店コード);
        Assert.Equal(originalData.Header.支店名, readData.Header.支店名);
        Assert.Equal(originalData.Header.預金種目, readData.Header.預金種目);
        Assert.Equal(originalData.Header.口座番号, readData.Header.口座番号);
        Assert.Equal(originalData.Header.口座名, readData.Header.口座名);
        Assert.Equal(originalData.Header.ダミー.Trim(), readData.Header.ダミー);

        // Compare DataList properties
        for (var i = 0; i < originalData.DataList.Count; i++)
        {
            Assert.Equal(originalData.DataList[i].データ区分, readData.DataList[i].データ区分);
            Assert.Equal(originalData.DataList[i].照会番号, readData.DataList[i].照会番号);
            Assert.Equal(originalData.DataList[i].勘定日, readData.DataList[i].勘定日);
            Assert.Equal(originalData.DataList[i].起算日, readData.DataList[i].起算日);
            Assert.Equal(originalData.DataList[i].金額, readData.DataList[i].金額);
            Assert.Equal(originalData.DataList[i].うち他店券金額, readData.DataList[i].うち他店券金額);
            Assert.Equal(originalData.DataList[i].振込依頼人コード, readData.DataList[i].振込依頼人コード);
            Assert.Equal(originalData.DataList[i].振込依頼人名, readData.DataList[i].振込依頼人名);
            Assert.Equal(originalData.DataList[i].仕向銀行名, readData.DataList[i].仕向銀行名);
            Assert.Equal(originalData.DataList[i].仕向店名, readData.DataList[i].仕向店名);
            Assert.Equal(originalData.DataList[i].取消区分, readData.DataList[i].取消区分);
            Assert.Equal(originalData.DataList[i].EDI情報, readData.DataList[i].EDI情報);
            Assert.Equal(originalData.DataList[i].ダミー.Trim(), readData.DataList[i].ダミー);
        }

        // Compare Trailer properties
        Assert.Equal(originalData.Trailer.データ区分, readData.Trailer.データ区分);
        Assert.Equal(originalData.Trailer.振込合計件数, readData.Trailer.振込合計件数);
        Assert.Equal(originalData.Trailer.振込合計金額, readData.Trailer.振込合計金額);
        Assert.Equal(originalData.Trailer.取消合計件数, readData.Trailer.取消合計件数);
        Assert.Equal(originalData.Trailer.取消合計金額, readData.Trailer.取消合計金額);
        Assert.Equal(originalData.Trailer.ダミー.Trim(), readData.Trailer.ダミー);

        // Compare End properties
        Assert.Equal(originalData.End.データ区分, readData.End.データ区分);
        Assert.Equal(originalData.End.ダミー.Trim(), readData.End.ダミー);
    }


    [Theory]
    [InlineData(FileFormat.Zengin)]
    [InlineData(FileFormat.Csv)]
    public async Task 入出金取引明細1_WriteReadTest(FileFormat format)
    {
        var originalData = Generator.GenerateRandom入出金取引明細1();
        using var memoryStream = new MemoryStream();
        await WriteAsync(originalData, memoryStream, format);

        memoryStream.Seek(0, SeekOrigin.Begin); // Reset stream position for reading
        var readData = await ReadAsync<入出金取引明細1>(memoryStream, format);

        Assert.NotNull(readData);

        // Compare Header properties
        Assert.Equal(originalData.Header.種別コード区分, readData.Header.種別コード区分);
        Assert.Equal(originalData.Header.コード区分, readData.Header.コード区分);
        Assert.Equal(originalData.Header.作成日, readData.Header.作成日);
        Assert.Equal(originalData.Header.勘定日開始, readData.Header.勘定日開始);
        Assert.Equal(originalData.Header.勘定日終了, readData.Header.勘定日終了);
        Assert.Equal(originalData.Header.銀行コード, readData.Header.銀行コード);
        Assert.Equal(originalData.Header.銀行名, readData.Header.銀行名);
        Assert.Equal(originalData.Header.支店コード, readData.Header.支店コード);
        Assert.Equal(originalData.Header.支店名, readData.Header.支店名);
        Assert.Equal(originalData.Header.預金種目, readData.Header.預金種目);
        Assert.Equal(originalData.Header.口座番号, readData.Header.口座番号);
        Assert.Equal(originalData.Header.口座名, readData.Header.口座名);
        Assert.Equal(originalData.Header.ダミー.Trim(), readData.Header.ダミー.Trim());

        // Compare DataList properties
        for (var i = 0; i < originalData.DataList.Count; i++)
        {
            Assert.Equal(originalData.DataList[i].データ区分, readData.DataList[i].データ区分);
            Assert.Equal(originalData.DataList[i].照会番号, readData.DataList[i].照会番号);
            Assert.Equal(originalData.DataList[i].勘定日, readData.DataList[i].勘定日);
            Assert.Equal(originalData.DataList[i].預入払出日, readData.DataList[i].預入払出日);
            Assert.Equal(originalData.DataList[i].入払区分, readData.DataList[i].入払区分);
            Assert.Equal(originalData.DataList[i].取引区分, readData.DataList[i].取引区分);
            Assert.Equal(originalData.DataList[i].取引金額, readData.DataList[i].取引金額);
            Assert.Equal(originalData.DataList[i].うち他店券金額, readData.DataList[i].うち他店券金額);
            Assert.Equal(originalData.DataList[i].交換呈示日, readData.DataList[i].交換呈示日);
            Assert.Equal(originalData.DataList[i].不渡返還日, readData.DataList[i].不渡返還日);
            Assert.Equal(originalData.DataList[i].手形小切手区分, readData.DataList[i].手形小切手区分);
            Assert.Equal(originalData.DataList[i].手形小切手番号, readData.DataList[i].手形小切手番号);
            Assert.Equal(originalData.DataList[i].僚店番号, readData.DataList[i].僚店番号);
            Assert.Equal(originalData.DataList[i].振込依頼人コード, readData.DataList[i].振込依頼人コード);
            Assert.Equal(originalData.DataList[i].振込依頼人名または契約者番号, readData.DataList[i].振込依頼人名または契約者番号);
            Assert.Equal(originalData.DataList[i].仕向銀行名, readData.DataList[i].仕向銀行名);
            Assert.Equal(originalData.DataList[i].仕向店名, readData.DataList[i].仕向店名);
            Assert.Equal(originalData.DataList[i].摘要内容, readData.DataList[i].摘要内容);
            Assert.Equal(originalData.DataList[i].EDI情報, readData.DataList[i].EDI情報);
            Assert.Equal(originalData.DataList[i].ダミー.Trim(), readData.DataList[i].ダミー.Trim());
        }

        // Compare Trailer properties
        Assert.Equal(originalData.Trailer.データ区分, readData.Trailer.データ区分);
        Assert.Equal(originalData.Trailer.入金件数, readData.Trailer.入金件数);
        Assert.Equal(originalData.Trailer.入金額合計, readData.Trailer.入金額合計);
        Assert.Equal(originalData.Trailer.出金件数, readData.Trailer.出金件数);
        Assert.Equal(originalData.Trailer.出金額合計, readData.Trailer.出金額合計);
        Assert.Equal(originalData.Trailer.貸越区分, readData.Trailer.貸越区分);
        Assert.Equal(originalData.Trailer.取引後残高, readData.Trailer.取引後残高);
        Assert.Equal(originalData.Trailer.データレコード件数, readData.Trailer.データレコード件数);
        Assert.Equal(originalData.Trailer.ダミー.Trim(), readData.Trailer.ダミー.Trim());

        // Compare End properties
        Assert.Equal(originalData.End.データ区分, readData.End.データ区分);
        Assert.Equal(originalData.End.レコード総件数, readData.End.レコード総件数);
        Assert.Equal(originalData.End.口座数, readData.End.口座数);
        Assert.Equal(originalData.End.ダミー.Trim(), readData.End.ダミー.Trim());
    }

    [Theory]
    [InlineData(FileFormat.Zengin)]
    [InlineData(FileFormat.Csv)]
    public async Task 総合振込_WriteReadTest(FileFormat format)
    {
        var originalData = Generator.GenerateRandom総合振込();
        using var memoryStream = new MemoryStream();
        await WriteAsync(originalData, memoryStream, format);

        memoryStream.Seek(0, SeekOrigin.Begin); // Reset stream position for reading
        var readData = await ReadAsync<総合振込>(memoryStream, format);

        Assert.NotNull(readData);

        // Compare Header properties
        Assert.Equal(originalData.Header.種別コード, readData.Header.種別コード);
        Assert.Equal(originalData.Header.コード区分, readData.Header.コード区分);
        Assert.Equal(originalData.Header.振込依頼人コード, readData.Header.振込依頼人コード);
        Assert.Equal(originalData.Header.振込依頼人名, readData.Header.振込依頼人名);
        Assert.Equal(originalData.Header.取組日, readData.Header.取組日);
        Assert.Equal(originalData.Header.仕向銀行番号, readData.Header.仕向銀行番号);
        Assert.Equal(originalData.Header.仕向銀行名, readData.Header.仕向銀行名);
        Assert.Equal(originalData.Header.仕向支店番号, readData.Header.仕向支店番号);
        Assert.Equal(originalData.Header.仕向支店名, readData.Header.仕向支店名);
        Assert.Equal(originalData.Header.預金種目, readData.Header.預金種目);
        Assert.Equal(originalData.Header.口座番号, readData.Header.口座番号);
        Assert.Equal(originalData.Header.ダミー.Trim(), readData.Header.ダミー.Trim());

        // Compare DataList properties
        for (var i = 0; i < originalData.DataList.Count; i++)
        {
            Assert.Equal(originalData.DataList[i].データ区分, readData.DataList[i].データ区分);
            Assert.Equal(originalData.DataList[i].被仕向銀行番号, readData.DataList[i].被仕向銀行番号);
            Assert.Equal(originalData.DataList[i].被仕向銀行名, readData.DataList[i].被仕向銀行名);
            Assert.Equal(originalData.DataList[i].被仕向支店番号, readData.DataList[i].被仕向支店番号);
            Assert.Equal(originalData.DataList[i].被仕向支店名, readData.DataList[i].被仕向支店名);
            Assert.Equal(originalData.DataList[i].手形交換所番号, readData.DataList[i].手形交換所番号);
            Assert.Equal(originalData.DataList[i].預金種目, readData.DataList[i].預金種目);
            Assert.Equal(originalData.DataList[i].口座番号, readData.DataList[i].口座番号);
            Assert.Equal(originalData.DataList[i].受取人名, readData.DataList[i].受取人名);
            Assert.Equal(originalData.DataList[i].振込金額, readData.DataList[i].振込金額);
            Assert.Equal(originalData.DataList[i].新規コード, readData.DataList[i].新規コード);
            Assert.Equal(originalData.DataList[i].識別表示, readData.DataList[i].識別表示);
            Assert.Equal(originalData.DataList[i].EDI情報, readData.DataList[i].EDI情報);
            Assert.Equal(originalData.DataList[i].顧客コード1, readData.DataList[i].顧客コード1);
            Assert.Equal(originalData.DataList[i].顧客コード2, readData.DataList[i].顧客コード2);
            Assert.Equal(originalData.DataList[i].振込指定区分, readData.DataList[i].振込指定区分);
            Assert.Equal(originalData.DataList[i].ダミー.Trim(), readData.DataList[i].ダミー.Trim());
        }

        // Compare Trailer properties
        Assert.Equal(originalData.Trailer.データ区分, readData.Trailer.データ区分);
        Assert.Equal(originalData.Trailer.合計件数, readData.Trailer.合計件数);
        Assert.Equal(originalData.Trailer.合計金額, readData.Trailer.合計金額);
        Assert.Equal(originalData.Trailer.ダミー.Trim(), readData.Trailer.ダミー.Trim());

        // Compare End properties
        Assert.Equal(originalData.End.データ区分, readData.End.データ区分);
        Assert.Equal(originalData.End.ダミー.Trim(), readData.End.ダミー.Trim());
    }
}