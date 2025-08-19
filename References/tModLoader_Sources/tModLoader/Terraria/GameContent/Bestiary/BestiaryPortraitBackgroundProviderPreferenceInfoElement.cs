using System;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000678 RID: 1656
	public class BestiaryPortraitBackgroundProviderPreferenceInfoElement : IPreferenceProviderElement, IBestiaryInfoElement
	{
		// Token: 0x060047B6 RID: 18358 RVA: 0x00646474 File Offset: 0x00644674
		public BestiaryPortraitBackgroundProviderPreferenceInfoElement(IBestiaryBackgroundImagePathAndColorProvider preferredProvider)
		{
			this._preferredProvider = preferredProvider;
		}

		// Token: 0x060047B7 RID: 18359 RVA: 0x00646483 File Offset: 0x00644683
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x060047B8 RID: 18360 RVA: 0x00646486 File Offset: 0x00644686
		public bool Matches(IBestiaryBackgroundImagePathAndColorProvider provider)
		{
			return provider == this._preferredProvider;
		}

		// Token: 0x060047B9 RID: 18361 RVA: 0x00646491 File Offset: 0x00644691
		public IBestiaryBackgroundImagePathAndColorProvider GetPreferredProvider()
		{
			return this._preferredProvider;
		}

		// Token: 0x04005BEF RID: 23535
		private IBestiaryBackgroundImagePathAndColorProvider _preferredProvider;
	}
}
