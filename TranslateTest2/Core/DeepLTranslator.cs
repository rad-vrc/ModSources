using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TranslateTest2.Config;
using System.Threading;

namespace TranslateTest2.Core
{
    public static class DeepLTranslator
    {
    private static HttpClient _client;
    private static HttpClientHandler _handler;
    private static readonly object _initLock = new();
        private static string _apiKey = string.Empty;
        private static string _targetLang = "ja";
        private static bool _use = true;
        private static bool _cacheEnabled = true;
    private static int _timeoutMs = 1500;
    private static int _batchIntervalMs = 150;
    private static int _batchMax = 10;
    private static string _endpointPref = "auto";
    private static int _retryCount = 2;
    private static int _retryDelayMs = 200;
    private static string _proxyUrl = string.Empty;

    private static readonly ConcurrentDictionary<string, string> _cache = new();
    // 処理中のテキスト（重複送信抑止）
    private static readonly ConcurrentDictionary<string, byte> _pending = new();
    // 直近の送信時刻（簡易クールダウン）
    private static readonly ConcurrentDictionary<string, long> _lastReqTicks = new();
    private static readonly ConcurrentQueue<string> _queue = new();
    private static Task _batchWorker;
    private static readonly object _workerLock = new();
    private static CancellationTokenSource _workerCts;
    // リクエスト間の最小間隔（全体スロットリング）
    private static int _minInterRequestDelayMs = 250;
    // 安全策: キュー長と文字列長の上限
    private static int _queueMax = 2000;
    private static int _textMaxLen = 4000;
    private static int _queueCount = 0;
    // 簡易キャッシュ上限（近似的に古いキーから削除）
    private static int _cacheMaxEntries = 10000;

    private static void PruneCacheIfNeeded()
    {
        try
        {
            int count = _cache.Count;
            if (count <= _cacheMaxEntries) return;
            int toRemove = Math.Max(0, count - _cacheMaxEntries);
            // 近似的に先頭から削除（ConcurrentDictionaryの列挙はスナップショット）
            foreach (var k in _cache.Keys.Take(toRemove))
            {
                _cache.TryRemove(k, out _);
            }
        }
        catch { }
    }

        public static void ApplyConfig(ClientConfig cfg)
        {
            _apiKey = cfg?.DeepLApiKey ?? string.Empty;
            _targetLang = NormalizeLang(string.IsNullOrWhiteSpace(cfg?.TargetLang) ? "ja" : cfg.TargetLang.Trim());
            _use = cfg?.UseDeepL ?? true;
            _cacheEnabled = cfg?.CacheDeepL ?? true;
            _timeoutMs = Math.Max(250, cfg?.DeepLTimeoutMs ?? 1500);
            _batchIntervalMs = Math.Max(50, cfg?.DeepLBatchIntervalMs ?? 150);
            _batchMax = Math.Clamp(cfg?.DeepLBatchMax ?? 10, 1, 50);
            _endpointPref = (cfg?.DeepLEndpointPreference ?? "auto").Trim().ToLowerInvariant();
            _retryCount = Math.Clamp(cfg?.DeepLRetryCount ?? 2, 0, 5);
            _retryDelayMs = Math.Max(0, cfg?.DeepLRetryInitialDelayMs ?? 200);
            _proxyUrl = cfg?.ProxyUrl ?? string.Empty;

            if (_client == null)
            {
                lock (_initLock)
                {
                    if (_client == null)
                    {
                        _handler = new HttpClientHandler();
                        try
                        {
                            // 軽量化のため自動圧縮を有効化（DeepLはgzip対応）
                            _handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                        }
                        catch { }
                        try
                        {
                            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
                        }
                        catch { }
                        if (!string.IsNullOrWhiteSpace(_proxyUrl))
                        {
                            try
                            {
                                _handler.Proxy = new System.Net.WebProxy(_proxyUrl);
                                _handler.UseProxy = true;
                            }
                            catch { }
                        }
                        _client = new HttpClient(_handler, disposeHandler: true);
                    }
                }
            }

            try
            {
                bool hasKey = !string.IsNullOrWhiteSpace(_apiKey);
                global::TranslateTest2.TranslateTest2.Instance?.Logger?.Info(
                    $"DeepL configured: use={_use}, hasKey={hasKey}, target={_targetLang}, endpointPref={_endpointPref}, timeout={_timeoutMs}ms, retry={_retryCount}, delay={_retryDelayMs}ms, cache={_cacheEnabled}, proxy={( _handler != null && _handler.UseProxy && _handler.Proxy != null)}");
            }
            catch { }

            EnsureWorker();
        }

        public static void Unload()
        {
            try { _workerCts?.Cancel(); } catch { }
            _client?.Dispose();
            _client = null;
            _handler = null;
            _cache.Clear();
            _batchWorker = null;
            _pending.Clear();
            _lastReqTicks.Clear();
            // キューを完全排出し、カウントをリセット
            while (_queue.TryDequeue(out _)) { }
            Interlocked.Exchange(ref _queueCount, 0);
            try { _workerCts?.Dispose(); } catch { }
            _workerCts = null;
        }

        public static bool IsEnabled => _use && !string.IsNullOrWhiteSpace(_apiKey);

        public static bool TryGetCached(string text, out string translated)
        {
            if (string.IsNullOrEmpty(text)) { translated = text; return true; }
            return _cache.TryGetValue(text, out translated);
        }

    public static void RequestIfMissing(string text)
        {
            if (!IsEnabled || string.IsNullOrWhiteSpace(text)) return;
            if (text.Length > _textMaxLen) return;
            if (_cache.ContainsKey(text)) return;

            // 直近送信からのクールダウン（同一テキストを短時間で連投しない）
            if (_lastReqTicks.TryGetValue(text, out var lastTicks))
            {
                var deltaMs = (int)TimeSpan.FromTicks(DateTime.UtcNow.Ticks - lastTicks).TotalMilliseconds;
                if (deltaMs < Math.Max(_timeoutMs, 1000)) // タイムアウト相当の間は再送しない
                    return;
            }

            // 既にキューに乗っている/処理中ならスキップ
            if (!_pending.TryAdd(text, 0))
                return;

            // 厳密カウントで上限管理
            int current = Interlocked.CompareExchange(ref _queueCount, 0, 0);
            if (current >= _queueMax)
            {
                // キュー満杯なら pending ロールバック（ゴースト状態防止）
                _pending.TryRemove(text, out _);
                return;
            }
            _queue.Enqueue(text);
            Interlocked.Increment(ref _queueCount);
            EnsureWorker();
        }

        public static async Task<string> TranslateAsync(string text)
        {
            if (!IsEnabled || string.IsNullOrWhiteSpace(text)) return text;
            if (text.Length > _textMaxLen) return text;

            if (_cacheEnabled && _cache.TryGetValue(text, out var hit))
                return hit;

            try
            {
                var result = await TranslateCoreAsync(text).ConfigureAwait(false);

                if (string.IsNullOrEmpty(result))
                    return text;

                if (_cacheEnabled)
                {
                    _cache[text] = result;
                    PruneCacheIfNeeded();
                }

                return result;
            }
            catch
            {
                return text;
            }
        }

        private static async Task<string> TranslateCoreAsync(string text)
        {
            var endpoints = GetPreferredEndpoints();
            foreach (var ep in endpoints)
            {
                var r = await PostTranslateWithRetry(text, ep).ConfigureAwait(false);
                if (!string.IsNullOrEmpty(r)) return r;
            }
            return null;
        }

        private static async Task<Dictionary<string, string>> TranslateCoreBatchAsync(List<string> texts)
        {
            if (texts == null || texts.Count == 0) return new Dictionary<string, string>();
            // 異常な長文は除外
            texts = texts.Where(t => !string.IsNullOrEmpty(t) && t.Length <= _textMaxLen).Distinct().ToList();
            if (texts.Count == 0) return new Dictionary<string, string>();
            var endpoints = GetPreferredEndpoints();
            foreach (var ep in endpoints)
            {
                var r = await PostTranslateBatchWithRetry(texts, ep).ConfigureAwait(false);
                if (r != null && r.Count > 0) return r;
            }
            return new Dictionary<string, string>();
        }

        private static readonly string[] EndpointsFree = { "https://api-free.deepl.com/v2/translate" };
        private static readonly string[] EndpointsPaid = { "https://api.deepl.com/v2/translate" };
        private static readonly string[] EndpointsBoth = { "https://api-free.deepl.com/v2/translate", "https://api.deepl.com/v2/translate" };

        private static string[] GetPreferredEndpoints() => _endpointPref switch
        {
            "free" => EndpointsFree,
            "paid" => EndpointsPaid,
            _ => EndpointsBoth
        };

        private static async Task<string> PostTranslate(string text, string endpoint)
        {
            if (_client == null) return null;

            // DeepLの推薦はapplication/x-www-form-urlencoded
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("auth_key", _apiKey),
                new KeyValuePair<string, string>("text", text),
                new KeyValuePair<string, string>("target_lang", _targetLang.ToUpperInvariant()),
            });

            using var cts = new System.Threading.CancellationTokenSource(_timeoutMs);
        using var resp = await _client.PostAsync(endpoint, content, cts.Token).ConfigureAwait(false);
            if (!resp.IsSuccessStatusCode)
            {
                try
                {
            var body = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
            global::TranslateTest2.TranslateTest2.Instance?.Logger?.Warn($"DeepL error {resp.StatusCode} at {endpoint}: {body}");
                }
                catch { }
                return null;
            }

            var json = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
            using var doc = JsonDocument.Parse(json);
            if (!doc.RootElement.TryGetProperty("translations", out var arr) || arr.GetArrayLength() == 0)
                return null;

            var first = arr[0];
            if (!first.TryGetProperty("text", out var textProp))
                return null;

            return textProp.GetString();
        }

        private static async Task<Dictionary<string, string>> PostTranslateBatch(List<string> texts, string endpoint)
        {
            var result = new Dictionary<string, string>();
            if (_client == null || texts == null || texts.Count == 0) return result;

            // DeepL: 同じキー"text"を複数付与で一括翻訳
            var payload = new List<KeyValuePair<string, string>>(texts.Count + 2)
            {
                new KeyValuePair<string, string>("auth_key", _apiKey),
                new KeyValuePair<string, string>("target_lang", _targetLang.ToUpperInvariant()),
            };
            foreach (var t in texts)
                payload.Add(new KeyValuePair<string, string>("text", t));

            var content = new FormUrlEncodedContent(payload);
            using var cts = new System.Threading.CancellationTokenSource(_timeoutMs);
        using var resp = await _client.PostAsync(endpoint, content, cts.Token).ConfigureAwait(false);
            if (!resp.IsSuccessStatusCode)
            {
                try
                {
            var body = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
            global::TranslateTest2.TranslateTest2.Instance?.Logger?.Warn($"DeepL error {resp.StatusCode} at {endpoint} (batch {texts.Count}): {body}");
                }
                catch { }
                return result;
            }

            var json = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
            using var doc = JsonDocument.Parse(json);
            if (!doc.RootElement.TryGetProperty("translations", out var arr))
                return result;
            int n = Math.Min(arr.GetArrayLength(), texts.Count);
            for (int i = 0; i < n; i++)
            {
                var el = arr[i];
                if (el.TryGetProperty("text", out var textProp))
                {
                    var translated = textProp.GetString();
                    if (!string.IsNullOrEmpty(translated))
                        result[texts[i]] = translated;
                }
            }
            return result;
        }

        private static async Task<Dictionary<string, string>> PostTranslateBatchWithRetry(List<string> texts, string endpoint)
        {
            int attempt = 0;
            int delay = _retryDelayMs;
            while (true)
            {
                try
                {
                    try
                    {
                        global::TranslateTest2.TranslateTest2.Instance?.Logger?.Debug($"DeepL BATCH attempt {attempt + 1}/{_retryCount + 1} -> {endpoint}, timeout={_timeoutMs}ms, size={texts.Count}");
                    }
                    catch { }
                    var map = await PostTranslateBatch(texts, endpoint).ConfigureAwait(false);
                    if (map != null && map.Count > 0) return map;
                }
                catch (TaskCanceledException tce)
                {
                    global::TranslateTest2.TranslateTest2.Instance?.Logger?.Warn($"DeepL timeout after {_timeoutMs}ms at {endpoint} (batch attempt {attempt + 1}): {tce.Message}");
                }
                catch (OperationCanceledException oce)
                {
                    global::TranslateTest2.TranslateTest2.Instance?.Logger?.Warn($"DeepL operation canceled at {endpoint} (batch attempt {attempt + 1}): {oce.Message}");
                }
                catch (Exception ex)
                {
                    global::TranslateTest2.TranslateTest2.Instance?.Logger?.Warn($"DeepL transport error at {endpoint} (batch attempt {attempt + 1}): {ex.Message}");
                }
                if (attempt++ >= _retryCount)
                    return new Dictionary<string, string>();
                if (delay > 0)
                    await Task.Delay(delay).ConfigureAwait(false);
                delay = Math.Min(delay * 2, 3000);
            }
        }

        private static async Task<string> PostTranslateWithRetry(string text, string endpoint)
        {
            int attempt = 0;
            int delay = _retryDelayMs;
            while (true)
            {
                try
                {
                    try
                    {
                        global::TranslateTest2.TranslateTest2.Instance?.Logger?.Debug($"DeepL POST attempt {attempt + 1}/{_retryCount + 1} -> {endpoint}, timeout={_timeoutMs}ms, textLen={text?.Length ?? 0}");
                    }
                    catch { }
                    var r = await PostTranslate(text, endpoint).ConfigureAwait(false);
                    if (!string.IsNullOrEmpty(r)) return r;
                }
                catch (TaskCanceledException tce)
                {
                    global::TranslateTest2.TranslateTest2.Instance?.Logger?.Warn($"DeepL timeout after {_timeoutMs}ms at {endpoint} (attempt {attempt + 1}): {tce.Message}");
                }
                catch (OperationCanceledException oce)
                {
                    global::TranslateTest2.TranslateTest2.Instance?.Logger?.Warn($"DeepL operation canceled at {endpoint} (attempt {attempt + 1}): {oce.Message}");
                }
                catch (Exception ex)
                {
                    global::TranslateTest2.TranslateTest2.Instance?.Logger?.Warn($"DeepL transport error at {endpoint} (attempt {attempt + 1}): {ex.Message}");
                }
                if (attempt++ >= _retryCount)
                    return null;
                if (delay > 0)
                    await Task.Delay(delay).ConfigureAwait(false);
                delay = Math.Min(delay * 2, 3000);
            }
        }

        private static string NormalizeLang(string input)
        {
                if (string.IsNullOrWhiteSpace(input)) return "JA";
                var s = input.Trim().Replace('_', '-').ToUpperInvariant();

                if (AllowedLangs.Contains(s)) return s;

            // よくある地域付き→言語コードへの簡易正規化
            if (s.StartsWith("EN-")) return s == "EN-US" || s == "EN-GB" ? s : "EN";
            if (s.StartsWith("PT-")) return (s == "PT-BR" || s == "PT-PT") ? s : "PT";
            if (s.StartsWith("ZH-")) return "ZH";
            if (s.StartsWith("JA-")) return "JA";
            if (s.StartsWith("KO-")) return "KO";
            if (s.Length >= 2 && AllowedLangs.Contains(s.AsSpan(0,2).ToString()))
                return s.Substring(0,2);
            return "JA"; // 無効値は日本語へフォールバック
        }

        // 許可言語セット（イミュータブル）
        private static readonly HashSet<string> AllowedLangs = new(StringComparer.OrdinalIgnoreCase)
        {
            "BG","CS","DA","DE","EL","EN","EN-GB","EN-US","ES","ET","FI","FR","HU","ID",
            "IT","JA","KO","LT","LV","NB","NL","PL","PT","PT-BR","PT-PT","RO","RU","SK",
            "SL","SV","TR","UK","ZH"
        };

        private static void EnsureWorker()
        {
            if (_batchWorker != null && !_batchWorker.IsCompleted) return;
            lock (_workerLock)
            {
                if (_batchWorker != null && !_batchWorker.IsCompleted) return;
                try
                {
                    global::TranslateTest2.TranslateTest2.Instance?.Logger?.Debug("DeepL batch worker started");
                }
                catch { }
                _workerCts?.Dispose();
                _workerCts = new CancellationTokenSource();
                var token = _workerCts.Token;
                _batchWorker = Task.Run(async () =>
                {
                    var list = new List<string>(_batchMax);
                    long lastSentTicks = 0;
                    while (!token.IsCancellationRequested)
                    {
                        // デキュー（空なら終了）
                        if (_queue.TryDequeue(out var item))
                        {
                            list.Add(item);
                            Interlocked.Decrement(ref _queueCount);
                        }
                        else
                        {
                            break; // 現設計では空で終了（必要時にEnsureWorkerで再起動）
                        }

                        // バッチ満杯 or 待ち時間経過
                        var start = DateTime.UtcNow;
                        while (!token.IsCancellationRequested && list.Count < _batchMax)
                        {
                            try { await Task.Delay(_batchIntervalMs, token).ConfigureAwait(false); } catch { }
                            while (list.Count < _batchMax && _queue.TryDequeue(out var nxt))
                            {
                                list.Add(nxt);
                                Interlocked.Decrement(ref _queueCount);
                            }
                            if ((DateTime.UtcNow - start).TotalMilliseconds >= _batchIntervalMs)
                                break;
                        }

                        try
                        {
                            // 全体スロットリング間隔を確保
                            if (lastSentTicks != 0)
                            {
                                var gapMs = (int)TimeSpan.FromTicks(DateTime.UtcNow.Ticks - lastSentTicks).TotalMilliseconds;
                                if (gapMs < _minInterRequestDelayMs)
                                {
                                    var delay = _minInterRequestDelayMs - gapMs;
                                    if (delay > 0) { try { await Task.Delay(delay, token).ConfigureAwait(false); } catch { } }
                                }
                            }

                            // バッチは重複を除いて送信（結果は順序対応）
                            var uniq = list.Where(t => !string.IsNullOrEmpty(t) && t.Length <= _textMaxLen).Distinct().ToList();
                            Dictionary<string, string> map;
                            if (uniq.Count > 1)
                            {
                                map = await TranslateCoreBatchAsync(uniq).ConfigureAwait(false);
                            }
                            else
                            {
                                map = new Dictionary<string, string>();
                                string single = null;
                                if (uniq.Count == 1)
                                    single = await TranslateCoreAsync(uniq[0]).ConfigureAwait(false);
                                if (!string.IsNullOrEmpty(single)) map[uniq[0]] = single;
                            }

                            foreach (var kv in map)
                            {
                                if (!string.IsNullOrEmpty(kv.Value))
                                {
                                    _cache[kv.Key] = kv.Value;
                                }
                            }
                            PruneCacheIfNeeded();
                            lastSentTicks = DateTime.UtcNow.Ticks;
                        }
                        catch { /* 無視 */ }
                        finally
                        {
                            var nowTicks = DateTime.UtcNow.Ticks;
                            foreach (var t in list)
                            {
                                _lastReqTicks[t] = nowTicks;
                                _pending.TryRemove(t, out _);
                            }
                            list.Clear();
                        }
                    }
                    // 最終的なカウンタ補正（負値防止）
                    if (Interlocked.CompareExchange(ref _queueCount, 0, int.MinValue) < 0)
                        Interlocked.Exchange(ref _queueCount, 0);
                }, token);
            }
        }
    }
}
