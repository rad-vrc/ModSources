using System;
using Terraria;
using Terraria.ModLoader;
using TranslateTest2.Core;
using TranslateTest2.Config;

namespace TranslateTest2.Commands
{
    public class DeepLCommand : ModCommand
    {
        public override CommandType Type => CommandType.Chat;
        public override string Command => "deepl";
        public override string Usage => "/deepl status | /deepl test <text>";
        public override string Description => "DeepLの状態確認と簡易翻訳テストを行います";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args.Length == 0)
            {
                Main.NewText("Usage: " + Usage);
                return;
            }

            var cfg = ModContent.GetInstance<ClientConfig>();

            var sub = args[0].ToLowerInvariant();
            if (sub == "status")
            {
                bool enabled = DeepLTranslator.IsEnabled;
                Main.NewText($"DeepL Enabled={enabled}, UseDeepL={cfg.UseDeepL}, HasKey={(string.IsNullOrWhiteSpace(cfg.DeepLApiKey) ? "No" : "Yes")}, TargetLang={cfg.TargetLang}");
                return;
            }
            if (sub == "test")
            {
                string text = args.Length >= 2 ? string.Join(" ", args, 1, args.Length - 1) : "Hello";
                if (!DeepLTranslator.IsEnabled)
                {
                    Main.NewText("DeepLが無効です。ConfigでAPIキーとUseDeepLを確認してください。");
                    return;
                }
                // 非同期で実行し、メインスレッドブロックを避ける
                Main.NewText($"翻訳要求中: '{text}'...");
                System.Threading.Tasks.Task.Run(async () =>
                {
                    try
                    {
                        var cts = new System.Threading.CancellationTokenSource(TimeSpan.FromSeconds(5));
                        var result = await DeepLTranslator.TranslateAsync(text).WaitAsync(cts.Token).ConfigureAwait(false);
                        bool changed = !string.IsNullOrEmpty(result) && !string.Equals(result, text, StringComparison.Ordinal);
                        Main.NewText(changed
                            ? $"DeepL OK: '{text}' -> '{result}'"
                            : "DeepL応答なし/同一結果（キー/エンドポイント/ネットワークをご確認ください）");
                    }
                    catch (OperationCanceledException)
                    {
                        Main.NewText("DeepLタイムアウト (5s)");
                    }
                    catch (Exception ex)
                    {
                        Main.NewText($"DeepLエラー: {ex.Message}");
                    }
                });
                return;
            }

            Main.NewText("Unknown subcommand. " + Usage);
        }
    }
}
