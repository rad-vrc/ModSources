using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TranslateTest2.Config;
using Terraria.ModLoader;

namespace TranslateTest2.Core
{
    /// <summary>
    /// 英語風のアイテム名 (prefixes + base + of + suffixes) を日本語風の語順に再構成します。
    /// 例: "Legendary Impeding ムラマサ of fending" → "伝説 妨害 受け流しのムラマサ"
    /// </summary>
    public static class NameComposer
    {
        // シンプル辞書（必要に応じてAssets辞書で拡張可能）
    private static readonly Dictionary<string, string> PrefixMap = new(StringComparer.OrdinalIgnoreCase)
        {
            {"Legendary", "伝説"},
            {"Impeding", "妨害"},
            {"Godly", "神"},
            {"Rare", "レア"},
            {"Common", "コモン"},
            {"Uncommon", "アンコモン"},
            {"Epic", "エピック"},
        };

    private static readonly Dictionary<string, string> SuffixMap = new(StringComparer.OrdinalIgnoreCase)
        {
            {"fending", "受け流し"},
            {"warding", "防護"},
        };

        // 固有名詞・ゲーム用語の優先辞書（DeepL前に適用）
        private static readonly Dictionary<string, string> ProperNouns = new(StringComparer.OrdinalIgnoreCase)
        {
            {"Hades", "ハデス"},
            {"terra", "テラ"},
        };

        /// <summary>
        /// 名前行を分析し、英語接頭辞群＋日本語基底名＋of 接尾辞群 の構造なら日本語語順に再構成。
        /// それ以外は原文を返します。
        /// </summary>
        public static bool TryCompose(string input, out string output)
        {
            output = input;
            if (string.IsNullOrWhiteSpace(input)) return false;
            try
            {
                // 末尾のスター囲み（*...* / ＊...＊）セグメントを抽出（空白を含んでもOK）。原文のまま再付与するため翻訳対象から除外する。
                var trailingStarSegments = ExtractTrailingStarSegments(ref input);

                // 基本パターン: [prefix*] [baseName (多言語可)] [" of "] [suffix*]
                int ofIdx = input.IndexOf(" of ", StringComparison.OrdinalIgnoreCase);
                string left = input;
                string right = null;
                if (ofIdx >= 0)
                {
                    left = input.Substring(0, ofIdx);
                    right = input.Substring(ofIdx + 4);
                }

                // 左側をトークナイズ（空白区切り）
                var tokens = left.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                // 末尾に (15) や *ModName* のようなトークンを取り除く
                var trailingMarkers = new List<string>();
                while (tokens.Count > 0 && IsTrailingMarkerToken(tokens[^1]))
                {
                    trailingMarkers.Insert(0, tokens[^1]);
                    tokens.RemoveAt(tokens.Count - 1);
                }
                if (tokens.Count == 0) return false;

                // 基底名
                string baseName = tokens[^1];
                string jpBase = baseName;
                if (TextLangHelper.NeedsTranslation(baseName))
                {
                    jpBase = ToJapaneseToken(baseName, isPrefix: false);
                }

                // 接頭辞
                var prefixes = tokens.Take(tokens.Count - 1).ToList();
                var jpPrefixes = new List<string>();
                foreach (var p in prefixes)
                {
                    jpPrefixes.Add(ToJapaneseToken(p, isPrefix: true));
                }

                // 右側（接尾辞群）
                var jpSuffixes = new List<string>();
                if (!string.IsNullOrEmpty(right))
                {
                    var suffixTokens = right.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var s in suffixTokens)
                    {
                        jpSuffixes.Add(ToJapaneseToken(s, isPrefix: false));
                    }
                }

                // 再構成
                string prefixPart = string.Join(' ', jpPrefixes);
                bool hasJoinerToken = prefixes.Any(IsJoinerToken);
                bool allNumericLikePrefixes = prefixes.Count > 0 && prefixes.All(IsNumericLikeToken);
                string result;
                if (jpSuffixes.Count > 0)
                {
                    string suffixPart = string.Join(' ', jpSuffixes);
                    bool suffixEndsWithNo = suffixPart.EndsWith("の") || suffixPart.EndsWith("ノ");
                    string suffixJoined = suffixEndsWithNo ? $"{suffixPart}{jpBase}" : $"{suffixPart}の{jpBase}";
                    result = string.IsNullOrEmpty(prefixPart) ? suffixJoined : $"{prefixPart} {suffixJoined}";
                }
                else
                {
                    if (string.IsNullOrEmpty(prefixPart))
                    {
                        result = jpBase;
                    }
                    else
                    {
                        if (TextLangHelper.IsJapaneseString(jpBase) && !hasJoinerToken && !allNumericLikePrefixes)
                        {
                            string trimmedPrefix = prefixPart.TrimEnd();
                            bool endsWithNo = trimmedPrefix.EndsWith("の") || trimmedPrefix.EndsWith("ノ");
                            if (endsWithNo)
                            {
                                result = $"{trimmedPrefix}{jpBase}";
                            }
                            else
                            {
                                string lastJpPrefix = jpPrefixes.Count > 0 ? jpPrefixes[^1] : string.Empty;
                                if (IsAdjLikeJapanesePrefix(lastJpPrefix))
                                    result = $"{prefixPart}{jpBase}";
                                else
                                    result = $"{prefixPart}の{jpBase}";
                            }
                        }
                        else
                        {
                            result = $"{prefixPart} {jpBase}";
                        }
                    }
                }

                var reattach = new List<string>();
                if (trailingMarkers.Count > 0) reattach.AddRange(trailingMarkers);
                if (trailingStarSegments.Count > 0) reattach.AddRange(trailingStarSegments);
                if (reattach.Count > 0)
                    result = result + " " + string.Join(" ", reattach);

                output = result;
                return !string.Equals(output, input, StringComparison.Ordinal);
            }
            catch (Exception ex)
            {
                global::TranslateTest2.TranslateTest2.Instance?.Logger?.Warn($"NameComposer error: {ex.Message}");
                output = input;
                return false;
            }
        }

        private static bool IsJoinerToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;
            // 「の」を避けたい連結記号
            switch (token)
            {
                case "-":
                case "–":
                case "—":
                case "－":
                case ":":
                case "：":
                case "・":
                case "/":
                case "／":
                    return true;
            }
            return false;
        }

        // 数値様のトークン（+1, -2, 10%, 1.5x など）かどうか
        private static bool IsNumericLikeToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return false;
            var s = token.Trim();
            // 単純高速判定：先頭が+/-か数字で、かつ英字が含まれない
            bool startsOk = s[0] == '+' || s[0] == '-' || char.IsDigit(s[0]);
            if (!startsOk) return false;
            bool hasLetter = s.Any(ch => (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z'));
            if (hasLetter)
            {
                // 末尾の x/X は倍率記号として許容
                if (!(s.EndsWith("x") || s.EndsWith("X"))) return false;
                // 先頭部は英字なし
                var middle = s.Substring(0, s.Length - 1);
                if (middle.Any(ch => (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z'))) return false;
            }
            // 数字/符号/小数点/カンマ/パーセント/倍率記号のみ
            foreach (var ch in s)
            {
                if (char.IsDigit(ch)) continue;
                if (ch == '+' || ch == '-' || ch == '.' || ch == ',' || ch == '%' || ch == 'x' || ch == 'X') continue;
                // スペースは許容しない（トークンは空白分割済）
                return false;
            }
            return true;
        }

        // 連体修飾的な和語プレフィックス（「献身的な」「延びる」「軽い」「速く」など）かどうか
        // 条件: 日本語文字列で、末尾が「な/い/る/く/的」「やすい/にくい」などの場合に true
        private static bool IsAdjLikeJapanesePrefix(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return false;
            var s = token.Trim();
            // 記号はこの時点で概ね除去済みだが、簡易に日本語かを確認
            if (!TextLangHelper.IsJapaneseString(s)) return false;

            // 複合語尾の先に評価
            if (s.EndsWith("やすい") || s.EndsWith("にくい")) return true;
            if (s.EndsWith("的")) return true;

            // 一文字語尾での簡易判定
            char last = s[s.Length - 1];
            switch (last)
            {
                case 'な':
                case 'い':
                case 'る':
                case 'く':
                case 'た':
                    return true;
                default:
                    return false;
            }
        }

        // 文字列末尾にある *...* / ＊...＊ のセグメント（空白含む）を右端から順に切り出し、
        // input から取り除いたうえでリストとして返す（先頭がより左側の順序）。
        private static List<string> ExtractTrailingStarSegments(ref string input)
        {
            var result = new List<string>();
            if (string.IsNullOrEmpty(input)) return result;

            string working = input;
            while (true)
            {
                if (string.IsNullOrWhiteSpace(working)) break;
                // 末尾の空白を除去して判定（後でスペースは付け直す）
                int endTrim = working.Length - 1;
                while (endTrim >= 0 && char.IsWhiteSpace(working[endTrim])) endTrim--;
                if (endTrim < 0) { working = string.Empty; break; }

                char last = working[endTrim];
                if (last != '*' && last != '＊') break;

                // 同じ種類のスターの開始位置を探す
                char star = last;
                int open = working.LastIndexOf(star, endTrim - 1);
                if (open < 0) break;

                // 開始直前は空白か行頭であること（単語の一部ではない）
                int pre = open - 1;
                while (pre >= 0 && char.IsWhiteSpace(working[pre])) pre--;
                int segmentStart = (pre < 0) ? 0 : pre + 1;
                // segmentStart が空白位置なら1つ進めてスター直前の空白を含めない
                while (segmentStart < working.Length && char.IsWhiteSpace(working[segmentStart])) segmentStart++;

                // セグメントは segmentStart 以降にあるスター囲み全体
                string head = working.Substring(0, segmentStart).TrimEnd();
                string seg = working.Substring(segmentStart, endTrim - segmentStart + 1);
                if (!(seg.Length >= 2 && seg[0] == star && seg[seg.Length - 1] == star)) break;

                // 収集（前側のものが先になるよう先頭に挿入）
                result.Insert(0, seg);
                working = head;
            }

            input = working;
            return result;
        }

        private static bool IsTrailingMarkerToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;
            var s = token.Trim();
            if (s.Length < 2) return false;
            char first = s[0];
            char last = s[s.Length - 1];
            // ASCII/全角の丸括弧
            if ((first == '(' && last == ')') || (first == '（' && last == '）')) return true;
            // ASCII/全角アスタリスク
            bool isStarFirst = (first == '*' || first == '＊');
            bool isStarLast = (last == '*' || last == '＊');
            if (isStarFirst && isStarLast) return true;
            return false;
        }

        private static string ToJapaneseToken(string token, bool isPrefix)
        {
            if (string.IsNullOrWhiteSpace(token)) return token;

            // 句読点などを分離
            var (lead, core, trail) = TrimPunct(token);

            // 設定で角括弧/スター内は翻訳しない
            try
            {
                var cfg = ModContent.GetInstance<ClientConfig>();
                if (cfg?.SkipBracketContentInNames == true)
                {
                    // トークンが [ ... ] / * ... * / ＊ ... ＊ で囲われている場合はそのまま返す
                    if (token.Length >= 2 && token[0] == '[' && token[token.Length - 1] == ']')
                        return token;
                    if (token.Length >= 2 && (token[0] == '*' || token[0] == '＊') && (token[token.Length - 1] == '*' || token[token.Length - 1] == '＊'))
                        return token;
                    // もしくはリード/トレイルが括弧を含む（TrimPunctで外れたケース）
                    if (lead.IndexOf('[') >= 0 && trail.IndexOf(']') >= 0)
                        return lead + core + trail;
                    // リード/トレイルがスターを含む（*...* が分離されたケース）
                    bool leadStar = lead.IndexOf('*') >= 0 || lead.IndexOf('＊') >= 0;
                    bool trailStar = trail.IndexOf('*') >= 0 || trail.IndexOf('＊') >= 0;
                    if (leadStar && trailStar)
                        return lead + core + trail;
                }
            }
            catch { }

            // 1) 固定辞書
            if (ProperNouns.TryGetValue(core, out var proper))
                return lead + proper + trail;

            if (isPrefix && PrefixMap.TryGetValue(core, out var fixedP))
                return lead + fixedP + trail;
            if (!isPrefix && SuffixMap.TryGetValue(core, out var fixedS))
                return lead + fixedS + trail;

            // 2) 非日本語トークンはDeepL対象
            if (TextLangHelper.NeedsTranslation(core))
            {
                // 接頭辞/接尾辞ともに、まずDeepLキャッシュを試み、未命中なら非同期要求のみ投げて原文維持
                if (DeepLTranslator.IsEnabled)
                {
                    if (DeepLTranslator.TryGetCached(core, out var cached))
                        return lead + cached + trail;
                    DeepLTranslator.RequestIfMissing(core);
                }
                return lead + core + trail;
            }

            // 3) そのまま
            return token;
        }

        private static (string lead, string core, string trail) TrimPunct(string s)
        {
            if (string.IsNullOrEmpty(s)) return (string.Empty, string.Empty, string.Empty);
            int start = 0;
            int end = s.Length - 1;
            while (start <= end && char.IsPunctuation(s[start])) start++;
            while (end >= start && char.IsPunctuation(s[end])) end--;
            // 防御的チェック
            if (start < 0) start = 0;
            if (end < start - 1) end = start - 1; // all punctuation case → empty core
            if (start > s.Length) start = s.Length; // 異常防護
            if (end >= s.Length) end = s.Length - 1;
            int coreLen = end - start + 1;
            if (coreLen < 0) coreLen = 0;
            string lead;
            string core;
            string trail;
            try { lead = (start <= s.Length) ? s.Substring(0, start) : string.Empty; }
            catch { lead = string.Empty; }
            try { core = (start <= s.Length && start + coreLen <= s.Length) ? s.Substring(start, coreLen) : string.Empty; }
            catch { core = string.Empty; }
            try { trail = (end + 1 <= s.Length) ? s.Substring(end + 1) : string.Empty; }
            catch { trail = string.Empty; }
            return (lead, core, trail);
        }
    }
}
