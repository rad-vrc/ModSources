using System;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200030E RID: 782
	public class BestiaryPortraitBackgroundBasedOnWorldEvilProviderPreferenceInfoElement : IPreferenceProviderElement, IBestiaryInfoElement
	{
		// Token: 0x060023FF RID: 9215 RVA: 0x0055886B File Offset: 0x00556A6B
		public BestiaryPortraitBackgroundBasedOnWorldEvilProviderPreferenceInfoElement(IBestiaryBackgroundImagePathAndColorProvider preferredProviderCorrupt, IBestiaryBackgroundImagePathAndColorProvider preferredProviderCrimson)
		{
			this._preferredProviderCorrupt = preferredProviderCorrupt;
			this._preferredProviderCrimson = preferredProviderCrimson;
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x00558881 File Offset: 0x00556A81
		public bool Matches(IBestiaryBackgroundImagePathAndColorProvider provider)
		{
			if (Main.ActiveWorldFileData == null || !WorldGen.crimson)
			{
				return provider == this._preferredProviderCorrupt;
			}
			return provider == this._preferredProviderCrimson;
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x005588A4 File Offset: 0x00556AA4
		public IBestiaryBackgroundImagePathAndColorProvider GetPreferredProvider()
		{
			if (Main.ActiveWorldFileData == null || !WorldGen.crimson)
			{
				return this._preferredProviderCorrupt;
			}
			return this._preferredProviderCrimson;
		}

		// Token: 0x04004862 RID: 18530
		private IBestiaryBackgroundImagePathAndColorProvider _preferredProviderCorrupt;

		// Token: 0x04004863 RID: 18531
		private IBestiaryBackgroundImagePathAndColorProvider _preferredProviderCrimson;
	}
}
