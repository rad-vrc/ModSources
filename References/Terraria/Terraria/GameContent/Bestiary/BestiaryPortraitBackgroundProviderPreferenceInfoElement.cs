using System;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200030D RID: 781
	public class BestiaryPortraitBackgroundProviderPreferenceInfoElement : IPreferenceProviderElement, IBestiaryInfoElement
	{
		// Token: 0x060023FB RID: 9211 RVA: 0x00558849 File Offset: 0x00556A49
		public BestiaryPortraitBackgroundProviderPreferenceInfoElement(IBestiaryBackgroundImagePathAndColorProvider preferredProvider)
		{
			this._preferredProvider = preferredProvider;
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x00558858 File Offset: 0x00556A58
		public bool Matches(IBestiaryBackgroundImagePathAndColorProvider provider)
		{
			return provider == this._preferredProvider;
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x00558863 File Offset: 0x00556A63
		public IBestiaryBackgroundImagePathAndColorProvider GetPreferredProvider()
		{
			return this._preferredProvider;
		}

		// Token: 0x04004861 RID: 18529
		private IBestiaryBackgroundImagePathAndColorProvider _preferredProvider;
	}
}
