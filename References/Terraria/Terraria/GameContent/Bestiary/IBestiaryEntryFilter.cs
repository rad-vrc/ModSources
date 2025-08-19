using System;
using Terraria.DataStructures;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020002EC RID: 748
	public interface IBestiaryEntryFilter : IEntryFilter<BestiaryEntry>
	{
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600237D RID: 9085
		bool? ForcedDisplay { get; }
	}
}
