using Terraria.ModLoader;

namespace TranslateTest2.Systems
{
	/// <summary>
	/// UI 文字列 (Hover/Tooltip/特定メニュー) 翻訳の骨格。
	/// 実装予定: UI 再描画タイミングでの文字列取得・部分辞書置換・DeepL 非同期要求。
	/// </summary>
	public class UITranslationSystem : ModSystem
	{
		public override void OnModLoad()
		{
			TranslateTest2.Instance?.Logger?.Debug("UITranslationSystem loaded (skeleton)");
		}

		public override void OnModUnload()
		{
			TranslateTest2.Instance?.Logger?.Debug("UITranslationSystem unloaded (skeleton)");
		}
	}
}
