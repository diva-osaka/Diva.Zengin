using System.Text;

namespace Diva.Zengin.Helpers;

public static class StringConverter
{
    public static string? ToHalfWidth(this string? input)
    {
        if (input == null)
            return null;

        var normalized = input.Normalize(NormalizationForm.FormKD);
        var sb = new StringBuilder();

        foreach (var n in normalized)
        {
            var c = n;

            if (c is >= 'ぁ' and <= 'ゖ')
            {
                // Convert Hiragana to Katakana
                c = (char)(c + 0x60);
            }

            switch (c)
            {
                case '\'' or '(' or ')' or '+' or ',' or '-' or '.' or '/' or ':' or '?' or '\\':
                case >= 'A' and <= 'Z':
                case >= '0' and <= '9':
                case >= '｢' and <= '｣':
                case >= 'ｦ' and <= 'ﾟ':
                    sb.Append(c);
                    break;
                case >= 'ァ' and <= 'ヶ':
                    sb.Append(ConvertHalfKatakana(c));
                    break;
                case 'ー': 
                    // 長音→マイナス
                    sb.Append('-');
                    break;
                case '\u309B':
                case '\u3099':
                    // 濁点
                    sb.Append('ﾞ');
                    break;
                case '\u309C':
                case '\u309A':
                    // 半濁点
                    sb.Append('ﾟ');
                    break;
                case >= 'a' and <= 'z':
                    sb.Append((char)(c - 'a' + 'A'));
                    break;
                case >= 'Ａ' and <= 'Ｚ':
                    sb.Append((char)(c - 'Ａ' + 'A'));
                    break;
                case >= 'ａ' and <= 'ｚ':
                    sb.Append((char)(c - 'ａ' + 'A'));
                    break;
                case >= '０' and <= '９':
                    sb.Append((char)(c - '０' + '0'));
                    break;
                case '＇' or '（' or '）' or '＋' or '，' or '－' or '．' or '／' or '：' or '？' or '￥' or '¥': // Normalize で '￥'　にはならない
                case '「' or '」':
                    sb.Append(ConvertHalfWidthSymbol(c));
                    break;
                default:
                    // その他
                    sb.Append(' ');
                    break;
            }
        }

        return sb.ToString();
    }

    private static char ConvertHalfKatakana(char c)
    {
        return c switch
        {
            'ア' => 'ｱ',
            'イ' => 'ｲ',
            'ウ' => 'ｳ',
            'エ' => 'ｴ',
            'オ' => 'ｵ',
            'カ' => 'ｶ',
            'キ' => 'ｷ',
            'ク' => 'ｸ',
            'ケ' => 'ｹ',
            'コ' => 'ｺ',
            'サ' => 'ｻ',
            'シ' => 'ｼ',
            'ス' => 'ｽ',
            'セ' => 'ｾ',
            'ソ' => 'ｿ',
            'タ' => 'ﾀ',
            'チ' => 'ﾁ',
            'ツ' => 'ﾂ',
            'テ' => 'ﾃ',
            'ト' => 'ﾄ',
            'ナ' => 'ﾅ',
            'ニ' => 'ﾆ',
            'ヌ' => 'ﾇ',
            'ネ' => 'ﾈ',
            'ノ' => 'ﾉ',
            'ハ' => 'ﾊ',
            'ヒ' => 'ﾋ',
            'フ' => 'ﾌ',
            'ヘ' => 'ﾍ',
            'ホ' => 'ﾎ',
            'マ' => 'ﾏ',
            'ミ' => 'ﾐ',
            'ム' => 'ﾑ',
            'メ' => 'ﾒ',
            'モ' => 'ﾓ',
            'ヤ' => 'ﾔ',
            'ユ' => 'ﾕ',
            'ヨ' => 'ﾖ',
            'ラ' => 'ﾗ',
            'リ' => 'ﾘ',
            'ル' => 'ﾙ',
            'レ' => 'ﾚ',
            'ロ' => 'ﾛ',
            'ワ' => 'ﾜ',
            'ヲ' => 'ｦ',
            'ン' => 'ﾝ',
            'ァ' => 'ｱ',
            'ィ' => 'ｲ',
            'ゥ' => 'ｳ',
            'ェ' => 'ｴ',
            'ォ' => 'ｵ',
            'ャ' => 'ﾔ',
            'ュ' => 'ﾕ',
            'ョ' => 'ﾖ',
            'ッ' => 'ﾂ',
            'ヮ' => 'ﾜ',
            'ヵ' => 'ｶ',
            'ヶ' => 'ｹ',
            'ヰ' => 'ｲ',
            'ヱ' => 'ｴ',
            _ => c
        };
    }

    private static char ConvertHalfWidthSymbol(char c)
    {
        return c switch
        {
            '（' => '(',
            '）' => ')',
            '＋' => '+',
            '，' => ',',
            '－' => '-',
            '．' => '.',
            '／' => '/',
            '：' => ':',
            '？' => '?',
            '￥' => '\\',
            '¥' => '\\',
            '「' => '｢',
            '」' => '｣',
            _ => c
        };
    }
}