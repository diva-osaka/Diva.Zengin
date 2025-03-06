using System.Globalization;

namespace Diva.Zengin.Converters;

internal static class 和暦To西暦Converter
{
    /// <summary>
    /// 和暦日付をDateOnlyに変換
    /// </summary>
    /// <param name="eraDate">yyMMdd</param>
    /// <returns></returns>
    /// <remarks>
    /// 未来の日付（現時刻より翌年）には未対応。
    /// </remarks>
    public static DateOnly Convert和暦日付ToDateOnly(string eraDate)
    {
        var culture = new CultureInfo("ja-JP", true)
        {
            DateTimeFormat =
            {
                Calendar = new JapaneseCalendar()
            }
        };

        var jstZone = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
        var currentDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, jstZone);

        var eraYear = int.Parse(eraDate[..2]);
        var month = int.Parse(eraDate[2..4]);
        var day = int.Parse(eraDate[4..]);
        var eras = culture.DateTimeFormat.Calendar.Eras;

        // 元号を順に試す
        foreach (var era in eras)
        {
            try
            {
                var s = $"{culture.DateTimeFormat.GetEraName(era)}{eraYear}/{month}/{day}";
                var date = DateTime.Parse(s, culture);

                // 未来の日付になった場合は採用しない
                if (date.Year > currentDate.Year)
                    continue;

                // 和暦に変換して一致しない場合は採用しない
                var d = date.ToString("ggy/M/d", culture);
                if (d != s)
                    continue;

                return DateOnly.FromDateTime(date);
            }
            catch (Exception)
            {
                // Do nothing
            }
        }

        throw new ArgumentException("Failed to convert Japanese era date.", nameof(eraDate));
    }
}