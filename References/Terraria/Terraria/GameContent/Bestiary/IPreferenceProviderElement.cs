using System;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200030C RID: 780
	public interface IPreferenceProviderElement : IBestiaryInfoElement
	{
		// Token: 0x060023F9 RID: 9209
		IBestiaryBackgroundImagePathAndColorProvider GetPreferredProvider();

		// Token: 0x060023FA RID: 9210
		bool Matches(IBestiaryBackgroundImagePathAndColorProvider provider);
	}
}
