using System;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000677 RID: 1655
	public class BestiaryPortraitBackgroundBasedOnWorldEvilProviderPreferenceInfoElement : IPreferenceProviderElement, IBestiaryInfoElement
	{
		// Token: 0x060047B2 RID: 18354 RVA: 0x0064641B File Offset: 0x0064461B
		public BestiaryPortraitBackgroundBasedOnWorldEvilProviderPreferenceInfoElement(IBestiaryBackgroundImagePathAndColorProvider preferredProviderCorrupt, IBestiaryBackgroundImagePathAndColorProvider preferredProviderCrimson)
		{
			this._preferredProviderCorrupt = preferredProviderCorrupt;
			this._preferredProviderCrimson = preferredProviderCrimson;
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x00646431 File Offset: 0x00644631
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x060047B4 RID: 18356 RVA: 0x00646434 File Offset: 0x00644634
		public bool Matches(IBestiaryBackgroundImagePathAndColorProvider provider)
		{
			if (Main.ActiveWorldFileData == null || !WorldGen.crimson)
			{
				return provider == this._preferredProviderCorrupt;
			}
			return provider == this._preferredProviderCrimson;
		}

		// Token: 0x060047B5 RID: 18357 RVA: 0x00646457 File Offset: 0x00644657
		public IBestiaryBackgroundImagePathAndColorProvider GetPreferredProvider()
		{
			if (Main.ActiveWorldFileData == null || !WorldGen.crimson)
			{
				return this._preferredProviderCorrupt;
			}
			return this._preferredProviderCrimson;
		}

		// Token: 0x04005BED RID: 23533
		private IBestiaryBackgroundImagePathAndColorProvider _preferredProviderCorrupt;

		// Token: 0x04005BEE RID: 23534
		private IBestiaryBackgroundImagePathAndColorProvider _preferredProviderCrimson;
	}
}
