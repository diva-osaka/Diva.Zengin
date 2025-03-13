using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Diva.Zengin.Records;

namespace Diva.Zengin.Tests;

public static class Generator
{
    private static DateOnly ToDateOnly(this DateTime dateTime) => DateOnly.FromDateTime(dateTime);

    public static 振込入金通知A GenerateRandom振込入金通知A()
    {
        var headerFaker = new Faker<振込入金通知Header>()
            .RuleFor(x => x.種別コード, f => 1)
            .RuleFor(x => x.コード区分, f => 0)
            .RuleFor(x => x.作成日, f => f.Date.Past().ToDateOnly())
            .RuleFor(x => x.勘定日開始, f => f.Date.Past().ToDateOnly())
            .RuleFor(x => x.勘定日終了, f => f.Date.Past().ToDateOnly())
            .RuleFor(x => x.銀行コード, f => f.Random.Int(0, 9999))
            .RuleFor(x => x.銀行名, f => f.Random.String2(1, 15))
            .RuleFor(x => x.支店コード, f => f.Random.Int(0, 999))
            .RuleFor(x => x.支店名, f => f.Random.String2(1, 15))
            .RuleFor(x => x.預金種目, f => f.Random.Int(0, 9))
            .RuleFor(x => x.口座番号, f => f.Random.Int(0, 9999999))
            .RuleFor(x => x.口座名, f => f.Random.String2(1, 40))
            .RuleFor(x => x.ダミー, f => new string(' ', 93));

        var dataFaker = new Faker<振込入金通知DataA>()
            .RuleFor(x => x.照会番号, f => f.Random.Int(0, 999999))
            .RuleFor(x => x.勘定日, f => f.Date.Past().ToDateOnly())
            .RuleFor(x => x.起算日, f => f.Date.Past().ToDateOnly())
            .RuleFor(x => x.金額, f => f.Random.Long(0, 9999999999))
            .RuleFor(x => x.うち他店券金額, f => f.Random.Long(0, 9999999999))
            .RuleFor(x => x.振込依頼人コード, f => f.Random.Long(0, 9999999999))
            .RuleFor(x => x.振込依頼人名, f => f.Random.String2(1, 48))
            .RuleFor(x => x.仕向銀行名, f => f.Random.String2(1, 15))
            .RuleFor(x => x.仕向店名, f => f.Random.String2(1, 15))
            .RuleFor(x => x.取消区分, f => f.Random.Int(0, 9))
            .RuleFor(x => x.EDI情報, f => f.Random.String2(1, 20))
            .RuleFor(x => x.ダミー, f => new string(' ', 52));

        var trailerFaker = new Faker<振込入金通知Trailer>()
            .RuleFor(x => x.データ区分, f => データ区分.Trailer)
            .RuleFor(x => x.振込合計件数, f => f.Random.Int(0, 999999))
            .RuleFor(x => x.振込合計金額, f => f.Random.Long(0, 999999999999))
            .RuleFor(x => x.取消合計件数, f => f.Random.Int(0, 999999))
            .RuleFor(x => x.取消合計金額, f => f.Random.Long(0, 999999999999))
            .RuleFor(x => x.ダミー, f => new string(' ', 163));

        var endFaker = new Faker<振込入金通知End>()
            .RuleFor(x => x.データ区分, f => データ区分.End)
            .RuleFor(x => x.ダミー, f => new string(' ', 199));

        var 振込入金通知A = new 振込入金通知A
        {
            Header = headerFaker.Generate(),
            DataList = dataFaker.Generate(10), // Generate multiple DataList items
            Trailer = trailerFaker.Generate(),
            End = endFaker.Generate()
        };

        return 振込入金通知A;
    }

    public static 入出金取引明細1 GenerateRandom入出金取引明細1()
    {
        var headerFaker = new Faker<入出金取引明細Header>()
            .RuleFor(x => x.種別コード区分, f => 3)
            .RuleFor(x => x.コード区分, f => 0)
            .RuleFor(x => x.作成日, f => f.Date.Past().ToDateOnly())
            .RuleFor(x => x.勘定日開始, f => f.Date.Past().ToDateOnly())
            .RuleFor(x => x.勘定日終了, f => f.Date.Past().ToDateOnly())
            .RuleFor(x => x.銀行コード, f => f.Random.Int(0, 9999))
            .RuleFor(x => x.銀行名, f => f.Random.String2(1, 15))
            .RuleFor(x => x.支店コード, f => f.Random.Int(0, 999))
            .RuleFor(x => x.支店名, f => f.Random.String2(1, 15))
            .RuleFor(x => x.ダミー, f => "000")
            .RuleFor(x => x.預金種目, f => f.Random.Int(0, 9))
            .RuleFor(x => x.口座番号, f => f.Random.Long(0, 9999999999))
            .RuleFor(x => x.口座名, f => f.Random.String2(1, 40))
            .RuleFor(x => x.貸越区分, f => f.Random.Int(0, 9))
            .RuleFor(x => x.通帳証書区分, f => f.Random.Int(0, 9))
            .RuleFor(x => x.取引前残高, f => f.Random.Long(0, 99999999999999))
            .RuleFor(x => x.ダミー2, f => new string(' ', 71));

        var dataFaker = new Faker<入出金取引明細Data1>()
            .RuleFor(x => x.データ区分, f => データ区分.Data)
            .RuleFor(x => x.照会番号, f => f.Random.Int(0, 99999999))
            .RuleFor(x => x.勘定日, f => f.Date.Past().ToDateOnly())
            .RuleFor(x => x.預入払出日, f => f.Date.Past().ToDateOnly())
            .RuleFor(x => x.入払区分, f => f.Random.Int(1, 2))
            .RuleFor(x => x.取引区分, f => f.Random.Int(0, 99))
            .RuleFor(x => x.取引金額, f => f.Random.Long(0, 999999999999))
            .RuleFor(x => x.うち他店券金額, f => f.Random.Long(0, 999999999999))
            .RuleFor(x => x.交換呈示日, f => f.Date.Past().ToDateOnly())
            .RuleFor(x => x.不渡返還日, f => f.Date.Past().ToDateOnly())
            .RuleFor(x => x.手形小切手区分, f => f.Random.Int(0, 9))
            .RuleFor(x => x.手形小切手番号, f => f.Random.Int(0, 9999999))
            .RuleFor(x => x.僚店番号, f => f.Random.Int(0, 999))
            .RuleFor(x => x.振込依頼人コード, f => f.Random.Long(0, 9999999999))
            .RuleFor(x => x.振込依頼人名または契約者番号, f => f.Random.String2(1, 48))
            .RuleFor(x => x.仕向銀行名, f => f.Random.String2(1, 15))
            .RuleFor(x => x.仕向店名, f => f.Random.String2(1, 15))
            .RuleFor(x => x.摘要内容, f => f.Random.String2(1, 20))
            .RuleFor(x => x.EDI情報, f => f.Random.String2(1, 20))
            .RuleFor(x => x.ダミー, f => " ");

        var trailerFaker = new Faker<入出金取引明細Trailer>()
            .RuleFor(x => x.データ区分, f => データ区分.Trailer)
            .RuleFor(x => x.入金件数, f => f.Random.Int(0, 999999))
            .RuleFor(x => x.入金額合計, f => f.Random.Long(0, 9999999999999))
            .RuleFor(x => x.出金件数, f => f.Random.Int(0, 999999))
            .RuleFor(x => x.出金額合計, f => f.Random.Long(0, 9999999999999))
            .RuleFor(x => x.貸越区分, f => f.Random.Int(0, 9))
            .RuleFor(x => x.取引後残高, f => f.Random.Long(0, 99999999999999))
            .RuleFor(x => x.データレコード件数, f => f.Random.Int(0, 9999999))
            .RuleFor(x => x.ダミー, f => new string(' ', 139));

        var endFaker = new Faker<入出金取引明細End>()
            .RuleFor(x => x.データ区分, f => データ区分.End)
            .RuleFor(x => x.レコード総件数, f => f.Random.Long(0, 9999999999))
            .RuleFor(x => x.口座数, f => f.Random.Int(0, 99999))
            .RuleFor(x => x.ダミー, f => new string(' ', 184));

        var 入出金取引明細1 = new 入出金取引明細1
        {
            Header = headerFaker.Generate(),
            DataList = dataFaker.Generate(10), // Generate multiple DataList items
            Trailer = trailerFaker.Generate(),
            End = endFaker.Generate()
        };

        return 入出金取引明細1;
    }

    public static 総合振込 GenerateRandom総合振込()
    {
        var headerFaker = new Faker<総合振込Header>()
            .RuleFor(h => h.データ区分, f => データ区分.Header)
            .RuleFor(h => h.種別コード, f => 21)
            .RuleFor(h => h.コード区分, f => 0)
            .RuleFor(h => h.振込依頼人コード, f => f.Random.Long(1000000000, 9999999999))
            .RuleFor(h => h.振込依頼人名, f => f.Random.String2(1, 40))
            .RuleFor(h => h.取組日, f => f.Random.Int(11, 1231))
            .RuleFor(h => h.仕向銀行番号, f => f.Random.Int(1000, 9999))
            .RuleFor(h => h.仕向銀行名, f => f.Random.String2(1, 15))
            .RuleFor(h => h.仕向支店番号, f => f.Random.Int(100, 999))
            .RuleFor(h => h.仕向支店名, f => f.Random.String2(1, 15))
            .RuleFor(h => h.預金種目, f => f.Random.Int(1, 9))
            .RuleFor(h => h.口座番号, f => f.Random.Int(1000000, 9999999))
            .RuleFor(h => h.ダミー, f => new string(' ', 17));

        var dataFaker = new Faker<総合振込Data>()
            .RuleFor(d => d.データ区分, f => データ区分.Data)
            .RuleFor(d => d.被仕向銀行番号, f => f.Random.Int(1000, 9999))
            .RuleFor(d => d.被仕向銀行名, f => f.Random.String2(1, 15))
            .RuleFor(d => d.被仕向支店番号, f => f.Random.Int(100, 999))
            .RuleFor(d => d.被仕向支店名, f => f.Random.String2(1, 15))
            .RuleFor(d => d.手形交換所番号, f => f.Random.Int(1000, 9999))
            .RuleFor(d => d.預金種目, f => f.Random.Int(1, 9))
            .RuleFor(d => d.口座番号, f => f.Random.Int(1000000, 9999999))
            .RuleFor(d => d.受取人名, f => f.Random.String2(1, 30))
            .RuleFor(d => d.振込金額, f => f.Random.Long(1000, 1000000))
            .RuleFor(d => d.新規コード, f => f.Random.Int(0, 2))
            .RuleFor(d => d.識別表示, f => f.Random.Bool())
            .RuleFor(d => d.EDI情報, (f, d) => d.識別表示 ? f.Random.String2(1, 20) : null)
            .RuleFor(d => d.顧客コード1, (f, d) => d.識別表示 ? null : f.Random.Long(1000000000, 9999999999))
            .RuleFor(d => d.顧客コード2, (f, d) => d.識別表示 ? null : f.Random.Long(1000000000, 9999999999))
            .RuleFor(d => d.振込指定区分, f => f.Random.Int(7, 8))
            .RuleFor(d => d.ダミー, f => new string(' ', 7));

        var trailerFaker = new Faker<総合振込Trailer>()
            .RuleFor(t => t.データ区分, f => データ区分.Trailer)
            .RuleFor(t => t.合計件数, f => f.Random.Int(1, 100))
            .RuleFor(t => t.合計金額, f => f.Random.Long(1000, 1000000))
            .RuleFor(t => t.ダミー, f => new string(' ', 101));

        var endFaker = new Faker<総合振込End>()
            .RuleFor(e => e.データ区分, f => データ区分.End)
            .RuleFor(e => e.ダミー, f => new string(' ', 119));

        var 総合振込 = new 総合振込
        {
            Header = headerFaker.Generate(),
            DataList = dataFaker.Generate(10),
            Trailer = trailerFaker.Generate(),
            End = endFaker.Generate()
        };

        return 総合振込;
    }
}