using Terraria;
using Terraria.ModLoader;
using TranslateTest2.Core;

namespace TranslateTest2.Systems
{
	/// <summary>
	/// チャットメッセージ翻訳の将来的実装用骨格。
	/// 今後: OnChatMessage などの適切な Hook で DeepLTranslator / TooltipTranslator を適用し、
	/// 言語判定 (TextLangHelper) により必要時のみ翻訳を要求する予定。
	/// </summary>
	public class ChatTranslationSystem : ModSystem
	{
		public override void OnModLoad()
		{
			TranslateTest2.Instance?.Logger?.Debug("ChatTranslationSystem loaded (skeleton)");
		}

		public override void OnModUnload()
		{
			TranslateTest2.Instance?.Logger?.Debug("ChatTranslationSystem unloaded (skeleton)");
		}
	}
}
