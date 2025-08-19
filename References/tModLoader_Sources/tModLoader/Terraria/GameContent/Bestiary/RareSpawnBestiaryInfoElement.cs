using System;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020006A3 RID: 1699
	public class RareSpawnBestiaryInfoElement : IBestiaryInfoElement, IProvideSearchFilterString
	{
		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06004855 RID: 18517 RVA: 0x00648F08 File Offset: 0x00647108
		// (set) Token: 0x06004856 RID: 18518 RVA: 0x00648F10 File Offset: 0x00647110
		public int RarityLevel { get; private set; }

		// Token: 0x06004857 RID: 18519 RVA: 0x00648F19 File Offset: 0x00647119
		public RareSpawnBestiaryInfoElement(int rarityLevel)
		{
			this.RarityLevel = rarityLevel;
		}

		// Token: 0x06004858 RID: 18520 RVA: 0x00648F28 File Offset: 0x00647128
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x06004859 RID: 18521 RVA: 0x00648F2B File Offset: 0x0064712B
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
