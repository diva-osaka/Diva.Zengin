# Diva.Zengin
## 対応レコード

* [振込入金通知（フォーマットA）](https://github.com/diva-osaka/Diva.Zengin/blob/master/Diva.Zengin/Records/%E5%85%A5%E5%87%BA%E9%87%91%E5%8F%96%E5%BC%95%E6%98%8E%E7%B4%B01.cs)
* [入出金取引明細（普通預金・当座預金・貯蓄預金の場合）](https://github.com/diva-osaka/Diva.Zengin/blob/master/Diva.Zengin/Records/%E6%8C%AF%E8%BE%BC%E5%85%A5%E9%87%91%E9%80%9A%E7%9F%A5A.cs)
* [総合振込](https://github.com/diva-osaka/Diva.Zengin/blob/master/Diva.Zengin/Records/%E7%B7%8F%E5%90%88%E6%8C%AF%E8%BE%BC.cs)

## Sample

```cs
// 読み込み
var path = "入出金取引明細.txt";
var config = new ZenginConfiguration
{
    FileFormat = FileFormat.Zengin // or FileFormat.Csv
};
var reader = new ZenginReader<入出金取引明細1>(path, config);
// var reader = new ZenginReader<List<入出金取引明細1>>(stream, config); // List/Array, Stream available

var 明細 = await reader.ReadAsync();

Console.WriteLine(明細.Header.勘定日開始);
Console.WriteLine(明細.Header.勘定日終了);
foreach (var data in 明細.DataList)
{
    Console.WriteLine(data.勘定日);
    Console.WriteLine(data.取引金額);
}
Console.WriteLine(明細.Trailer.データレコード件数);
Console.WriteLine(明細.End.レコード総件数);
```

```cs
// 書き込み
var path = "入出金取引明細.txt";
var config = new ZenginConfiguration
{
    FileFormat = FileFormat.Zengin // or FileFormat.Csv
};
var writer = new ZenginWriter<入出金取引明細1>(path, config);
// var writer = new ZenginWriter<List<入出金取引明細1>>(stream, config); // List/Array, Stream available

var 明細 = new 入出金取引明細1();
明細.Header
    .Set勘定日開始(new DateOnly(2025, 1, 1))
    .Set銀行コード(9999)
    .Set支店コード(999)
    .Set口座番号(1234567);
    // .SetXXX ...
// Non-fluent approach:
// 明細.Header.勘定日開始 = new DateOnly(2025, 1, 1);
// 明細.Header.銀行コード = 9999;
// 明細.Header.支店コード = 999;
// 明細.Header.口座番号 = 1234567;

var data = new 入出金取引明細Data1();
data
    .Set入払区分(1)
    .Set勘定日(new DateOnly(2025, 1, 1))
    .Set取引金額(1000);
    // .SetXXX ...
明細.DataList.Add(data);
明細.SetTrailerValues(); // or 明細.Trailer.データレコード件数 = 1; ...
明細.SetEndValues(); // or 明細.End.レコード総件数 = 1; ...

await writer.WriteAsync(明細);
```
