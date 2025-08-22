using Terraria.ModLoader;

namespace TranslateTest2.Common.Systems
{
	/// <summary>
	/// メニュー(UIロビー/タイトル/設定) テキスト翻訳の将来拡張用骨格。
	/// 実装時: ロード済み文字列テーブル巡回し TooltipTranslator/DeepLTranslator を適用する仕組みを検討。
	/// </summary>
	public class MenuTranslationSystem : ModSystem
	{
		public override void OnModLoad()
		{
			TranslateTest2.Instance?.Logger?.Debug("MenuTranslationSystem loaded (skeleton)");
		}

		public override void OnModUnload()
		{
			TranslateTest2.Instance?.Logger?.Debug("MenuTranslationSystem unloaded (skeleton)");
		}
	}
}
