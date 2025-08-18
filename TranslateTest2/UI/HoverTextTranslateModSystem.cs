using Terraria.ModLoader;

namespace TranslateTest2.UI
{
    /// <summary>
    /// ホバー時のテキスト（看板、チェスト名、NPC名等）を翻訳するModSystemです。
    /// 左Shiftキー押下時にMain.hoverItemNameを翻訳対象として処理します。
    /// </summary>
    // 原点回帰のため、ホバー翻訳は無効化（必要になれば復活させます）
    public class HoverTextTranslateModSystem : ModSystem { }
}
