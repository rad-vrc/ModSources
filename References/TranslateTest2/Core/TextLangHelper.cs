using System;

namespace TranslateTest2.Core
{
    public static class TextLangHelper
    {
        // 日本語(ひらがな/カタカナ/半角カナ/漢字)かどうか
        public static bool IsJapaneseChar(char c)
        {
            // ひらがな
            if (c >= '\u3040' && c <= '\u309F') return true;
            // カタカナ
            if (c >= '\u30A0' && c <= '\u30FF') return true;
            // カタカナ拡張
            if (c >= '\u31F0' && c <= '\u31FF') return true;
            // CJK統合漢字
            if (c >= '\u4E00' && c <= '\u9FFF') return true;
            // 半角カナ
            if (c >= '\uFF66' && c <= '\uFF9D') return true;
            // 長音符
            if (c == '\u30FC') return true;
            return false;
        }

        // 空白/記号/数字などの中立文字か
        public static bool IsNeutral(char c)
        {
            if (char.IsWhiteSpace(c)) return true;
            if (char.IsDigit(c)) return true;
            // 一般的な記号は中立扱い
            if (char.IsPunctuation(c) || char.IsSymbol(c)) return true;
            return false;
        }

        // 文字列全体が日本語(と中立)のみで構成されるか
        public static bool IsJapaneseString(string s)
        {
            if (string.IsNullOrEmpty(s)) return true;
            foreach (var ch in s)
            {
                if (IsJapaneseChar(ch)) continue;
                if (IsNeutral(ch)) continue;
                return false; // 非日本語の文字を含む
            }
            return true;
        }

        // DeepLの対象にすべきか: 非日本語の文字(英字等)を含む
        public static bool NeedsTranslation(string s) => !IsJapaneseString(s);
    }
}
