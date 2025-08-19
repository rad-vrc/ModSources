using System;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000692 RID: 1682
	public interface IPreferenceProviderElement : IBestiaryInfoElement
	{
		// Token: 0x060047FE RID: 18430
		IBestiaryBackgroundImagePathAndColorProvider GetPreferredProvider();

		// Token: 0x060047FF RID: 18431
		bool Matches(IBestiaryBackgroundImagePathAndColorProvider provider);
	}
}
