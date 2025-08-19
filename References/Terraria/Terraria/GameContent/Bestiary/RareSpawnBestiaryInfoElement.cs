using System;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000308 RID: 776
	public class RareSpawnBestiaryInfoElement : IBestiaryInfoElement, IProvideSearchFilterString
	{
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060023E4 RID: 9188 RVA: 0x00558586 File Offset: 0x00556786
		// (set) Token: 0x060023E5 RID: 9189 RVA: 0x0055858E File Offset: 0x0055678E
		public int RarityLevel { get; private set; }

		// Token: 0x060023E6 RID: 9190 RVA: 0x00558597 File Offset: 0x00556797
		public RareSpawnBestiaryInfoElement(int rarityLevel)
		{
			this.RarityLevel = rarityLevel;
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x005585A6 File Offset: 0x005567A6
		public string GetSearchString(ref BestiaryUICollectionInfo info)
		{
			if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				return null;
			}
			return Language.GetText("BestiaryInfo.IsRare").Value;
		}
	}
}
