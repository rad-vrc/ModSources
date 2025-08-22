using Terraria.ModLoader;

namespace TranslateTest2.Common.Systems
{
	/// <summary>
	/// 今後、ゲーム内で発生する様々な動的文字列 (NPC会話 / ステータス / UI) に対する統合的翻訳エントリポイントを提供する予定の骨格。
	/// 現状は初期化ログのみ。必要なイベントが判明次第、適切な Hook / コールバックを追加する。
	/// </summary>
	public class UniversalTextTranslateSystem : ModSystem
	{
		public override void OnModLoad()
		{
			TranslateTest2.Instance?.Logger?.Debug("UniversalTextTranslateSystem loaded (skeleton)");
		}

		public override void OnModUnload()
		{
			TranslateTest2.Instance?.Logger?.Debug("UniversalTextTranslateSystem unloaded (skeleton)");
		}
	}
}
