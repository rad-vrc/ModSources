using System;
using Terraria.DataStructures;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000689 RID: 1673
	public interface IBestiaryEntryFilter : IEntryFilter<BestiaryEntry>
	{
		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x060047F1 RID: 18417
		bool? ForcedDisplay { get; }
	}
}
