using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Terraria.ModLoader;

namespace TranslateTest2.Core
{
    /// <summary>
    /// ツールチップ文言の簡易置換辞書を管理します。
    /// 優先順: Assets/tooltip_dict.txt -> tooltip_dict.txt -> Content/tooltip_dict.txt -> 埋め込みリソース
    /// フォーマット: 1行に1エントリ、"原文 => 訳文" 形式。#で始まる行はコメント。
    /// </summary>
    public static class TooltipTranslator
    {
        private static Dictionary<string, string> _map = new(StringComparer.Ordinal);
    // 大文字小文字無視の完全一致用（線形探索削減）
    private static Dictionary<string, string> _mapIgnoreCase = new(StringComparer.OrdinalIgnoreCase);
        // 置換性能向上用: キー長の降順で事前インデックス
        private static List<KeyValuePair<string, string>> _sortedEntries = new();

        public static bool IsLoaded => _map.Count > 0;

        public static void Load(Mod mod)
        {
            _map.Clear();
            _mapIgnoreCase.Clear();
            _sortedEntries.Clear();

            // Mod内ファイルの探索候補
            string[] candidates = new[]
            {
                "Assets/tooltip_dict.txt",
                "tooltip_dict.txt",
                "Content/tooltip_dict.txt",
            };

            foreach (var path in candidates)
            {
                try
                {
                    using var s = mod.GetFileStream(path);
                    if (s != null)
                    {
                        using var sr = new StreamReader(s, Encoding.UTF8, true);
                        ParseIntoMap(sr);
                        RebuildIndex();
                        try { global::TranslateTest2.TranslateTest2.Instance?.Logger?.Info($"Tooltip dictionary loaded from {path} with {_map.Count} entries"); } catch { }
                        return;
                    }
                }
                catch
                {
                    // 次の候補へ
                }
            }

            // 埋め込みリソースからのフォールバック
            try
            {
                var asm = Assembly.GetExecutingAssembly();
                using var s = asm.GetManifestResourceStream("TranslateTest2.Assets.tooltip_dict.txt")
                             ?? asm.GetManifestResourceStream("TranslateTest2.tooltip_dict.txt")
                             ?? asm.GetManifestResourceStream("TranslateTest2.Content.tooltip_dict.txt");
                if (s != null)
                {
                    using var sr = new StreamReader(s, Encoding.UTF8, true);
                    ParseIntoMap(sr);
                    RebuildIndex();
                    try { global::TranslateTest2.TranslateTest2.Instance?.Logger?.Info($"Tooltip dictionary loaded from embedded resource with {_map.Count} entries"); } catch { }
                }
            }
            catch
            {
                // 読み込めない場合は空のまま
            }
        }

        public static void Unload()
        {
            _map.Clear();
            _mapIgnoreCase.Clear();
            _sortedEntries.Clear();
        }

        private static void ParseIntoMap(StreamReader sr)
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.Length == 0 || line.StartsWith("#"))
                    continue;

                // フォーマット: 原文 => 訳文
                int sep = line.IndexOf("=>", StringComparison.Ordinal);
                if (sep <= 0)
                    continue; // 不正行はスキップ

                var left = line.Substring(0, sep).Trim();
                var right = line.Substring(sep + 2).Trim();
                if (left.Length == 0)
                    continue;

                _map[left] = right;
                _mapIgnoreCase[left] = right; // 同期登録
            }
        }

        /// <summary>
        /// 1行のツールチップ文字列に対して辞書置換を適用します。
        /// 完全一致があればそれを優先。なければ全キーの部分一致置換を行います。
        /// </summary>
        public static string TranslateLine(string input)
        {
            if (_map.Count == 0 || string.IsNullOrEmpty(input))
                return input;

            // まずは大小区別での完全一致
            if (_map.TryGetValue(input, out var exact))
            {
#if DEBUG
                try { global::TranslateTest2.TranslateTest2.Instance?.Logger?.Debug($"Tooltip exact hit: '{input}' -> '{exact}'"); } catch { }
#endif
                return exact;
            }
            // 次に大小無視での完全一致（O(1)）
            if (_mapIgnoreCase.TryGetValue(input, out var ciValue))
            {
#if DEBUG
                try { global::TranslateTest2.TranslateTest2.Instance?.Logger?.Debug($"Tooltip exact (ci) hit: '{input}' -> '{ciValue}'"); } catch { }
#endif
                return ciValue;
            }

            string output = input;
            // 長いキーほど優先して置換する
            foreach (var kv in _sortedEntries)
            {
                output = ReplaceIgnoreCase(output, kv.Key, kv.Value);
            }
            
#if DEBUG
            if (!ReferenceEquals(output, input) && output != input)
            {
                try { global::TranslateTest2.TranslateTest2.Instance?.Logger?.Debug($"Tooltip substr hit: '{input}' -> '{output}'"); } catch { }
            }
#endif
            return output;
        }

        private static void RebuildIndex()
        {
            _sortedEntries = new List<KeyValuePair<string, string>>(_map.Count);
            foreach (var kv in _map)
                _sortedEntries.Add(kv);
            _sortedEntries.Sort((a, b) => b.Key.Length.CompareTo(a.Key.Length));
        }

        private static string ReplaceIgnoreCase(string input, string pattern, string replacement)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(pattern)) return input;
            // まず一度だけ探索し、ヒットが無ければ元文字列をそのまま返す（不要な StringBuilder 回避）
            int first = input.IndexOf(pattern, StringComparison.OrdinalIgnoreCase);
            if (first < 0) return input;
            int idx = 0;
            var sb = new System.Text.StringBuilder(input.Length + Math.Min(32, replacement?.Length ?? 0));
            // 先頭ヒット位置までコピー
            sb.Append(input, 0, first);
            sb.Append(replacement);
            idx = first + pattern.Length;
            while (true)
            {
                int hit = input.IndexOf(pattern, idx, StringComparison.OrdinalIgnoreCase);
                if (hit < 0)
                {
                    sb.Append(input, idx, input.Length - idx);
                    break;
                }
                sb.Append(input, idx, hit - idx);
                sb.Append(replacement);
                idx = hit + pattern.Length;
            }
            return sb.ToString();
        }
    }
}
