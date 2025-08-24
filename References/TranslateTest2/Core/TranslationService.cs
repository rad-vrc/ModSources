using System;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TranslateTest2.Config;

namespace TranslateTest2.Core
{
    // 翻訳プロバイダの最小インターフェース（低リスク導入）
    public interface ITranslationProvider
    {
        bool IsEnabled { get; }
        bool TryGetCached(string text, out string translated);
        void RequestIfMissing(string text);
        Task<string> TranslateAsync(string text);
        void ApplyConfig(ClientConfig cfg);
        void Unload();
    }

    // 現行 DeepL 実装への薄いアダプタ
    internal sealed class DeepLProviderAdapter : ITranslationProvider
    {
        public bool IsEnabled => DeepLTranslator.IsEnabled;
        public bool TryGetCached(string text, out string translated) => DeepLTranslator.TryGetCached(text, out translated);
        public void RequestIfMissing(string text) => DeepLTranslator.RequestIfMissing(text);
        public Task<string> TranslateAsync(string text) => DeepLTranslator.TranslateAsync(text);
        public void ApplyConfig(ClientConfig cfg) => DeepLTranslator.ApplyConfig(cfg);
        public void Unload() => DeepLTranslator.Unload();
    }

    // オフライン・無効時用のNOPプロバイダ
    internal sealed class NoopTranslationProvider : ITranslationProvider
    {
        public bool IsEnabled => false;
        public bool TryGetCached(string text, out string translated) { translated = text; return false; }
        public void RequestIfMissing(string text) { }
        public Task<string> TranslateAsync(string text) => Task.FromResult(text);
        public void ApplyConfig(ClientConfig cfg) { }
        public void Unload() { }
    }

    // ローカル辞書プロバイダ（TooltipTranslator を利用）
    internal sealed class LocalDictTranslationProvider : ITranslationProvider
    {
        private readonly System.Collections.Concurrent.ConcurrentDictionary<string, string> _cache = new();
        public bool IsEnabled => TooltipTranslator.IsLoaded;

        public bool TryGetCached(string text, out string translated)
        {
            if (string.IsNullOrEmpty(text)) { translated = text; return true; }
            if (_cache.TryGetValue(text, out translated)) return true;
            var t = TooltipTranslator.TranslateLine(text);
            if (!string.IsNullOrEmpty(t) && t != text)
            {
                _cache[text] = t;
                translated = t;
                return true;
            }
            translated = null;
            return false;
        }

        public void RequestIfMissing(string text)
        {
            // 何もしない（同期翻訳のみ）
        }

        public Task<string> TranslateAsync(string text)
        {
            if (string.IsNullOrEmpty(text)) return Task.FromResult(text);
            if (TryGetCached(text, out var t)) return Task.FromResult(t);
            return Task.FromResult(text);
        }

        public void ApplyConfig(ClientConfig cfg) { }
        public void Unload() { _cache.Clear(); }
    }

    // 呼び出し側のファサード
    public static class TranslationService
    {
        private static ITranslationProvider _provider = new DeepLProviderAdapter();
    // 正規化キーに対する薄いオーバーレイキャッシュ（プロバイダ間で共有）
    private static readonly ConcurrentDictionary<string, string> _cache = new();

        public static ITranslationProvider Provider => _provider;

        // 既存呼び出し置換を最小化するためのパススルー
        public static bool IsEnabled => _provider?.IsEnabled == true;

        public static bool TryGetCached(string text, out string translated)
        {
            translated = null;
            if (string.IsNullOrEmpty(text)) { translated = text; return true; }
            var key = NormalizeKey(text);
            if (_cache.TryGetValue(key, out translated))
                return true;
            if (_provider.TryGetCached(text, out var hit))
            {
                if (!string.IsNullOrEmpty(hit))
                {
                    _cache[key] = hit;
                    translated = hit;
                    return true;
                }
            }
            return false;
        }

        public static void RequestIfMissing(string text)
        {
            if (string.IsNullOrEmpty(text)) return;
            var key = NormalizeKey(text);
            if (_cache.ContainsKey(key)) return;
            _provider.RequestIfMissing(text);
        }

        public static async Task<string> TranslateAsync(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            var key = NormalizeKey(text);
            if (_cache.TryGetValue(key, out var hit))
                return hit;
            var result = await _provider.TranslateAsync(text).ConfigureAwait(false);
            if (!string.IsNullOrEmpty(result) && !string.Equals(result, text, StringComparison.Ordinal))
            {
                _cache[key] = result;
            }
            return result;
        }

        // キャッシュキー正規化
        private static string NormalizeKey(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            // 行末: CRLF/CR を LF に統一
            s = s.Replace("\r\n", "\n").Replace("\r", "\n");
            // Terrariaカラータグ [c/HEX:... ] を中身に展開
            try { s = Regex.Replace(s, "\\[c/[0-9A-Fa-f]{3,8}:(.*?)\\]", m => m.Groups[1].Value, RegexOptions.Singleline); } catch { }
            // ゼロ幅・BOM 除去
            s = s.Replace("\u200B", string.Empty).Replace("\uFEFF", string.Empty);
            // 連続スペース/タブの縮約（改行は保持）
            try { s = Regex.Replace(s, "[ \t]+", " "); } catch { }
            // 両端トリム
            s = s.Trim();
            return s;
        }

        public static void Configure(ClientConfig cfg)
        {
            var mode = cfg?.TranslationMode ?? TranslatorMode.Auto;
            ITranslationProvider next = _provider;
            switch (mode)
            {
                case TranslatorMode.DeepL:
                    next = (!string.IsNullOrWhiteSpace(cfg?.DeepLApiKey) && cfg?.UseDeepL == true)
                        ? new DeepLProviderAdapter()
                        : new NoopTranslationProvider();
                    break;
                case TranslatorMode.Dict:
                    next = new LocalDictTranslationProvider();
                    break;
                case TranslatorMode.Off:
                    next = new NoopTranslationProvider();
                    break;
                case TranslatorMode.Auto:
                default:
                    if (cfg?.UseDeepL == true && !string.IsNullOrWhiteSpace(cfg?.DeepLApiKey))
                        next = new DeepLProviderAdapter();
                    else if (TooltipTranslator.IsLoaded)
                        next = new LocalDictTranslationProvider();
                    else
                        next = new NoopTranslationProvider();
                    break;
            }
            if (_provider?.GetType() != next.GetType())
                _provider = next;

            try { _provider.ApplyConfig(cfg); } catch { }
        }

        public static void Unload()
        {
            try { _provider?.Unload(); } catch { }
            _provider = new NoopTranslationProvider();
            _cache.Clear();
        }

        // 簡易ステータス（UI/コマンド用）
        public static string GetStatus(ClientConfig cfg)
        {
            string providerName = _provider switch
            {
                DeepLProviderAdapter => "DeepL",
                LocalDictTranslationProvider => "Dict",
                _ => "Noop"
            };
            var hasKey = !string.IsNullOrWhiteSpace(cfg?.DeepLApiKey);
            return $"Provider={providerName}, Mode={cfg?.TranslationMode}, Enabled={IsEnabled}, HasKey={hasKey}, TargetLang={cfg?.TargetLang}, Cache(overlay)={_cache.Count}";
        }
    }
}
