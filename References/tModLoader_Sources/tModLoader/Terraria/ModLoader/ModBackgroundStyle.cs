using System;

namespace Terraria.ModLoader
{
	// Token: 0x0200019A RID: 410
	public abstract class ModBackgroundStyle : ModType
	{
		/// <summary>
		/// The ID of this underground background style.
		/// </summary>
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x004E266B File Offset: 0x004E086B
		// (set) Token: 0x06001FCF RID: 8143 RVA: 0x004E2673 File Offset: 0x004E0873
		public int Slot { get; internal set; }
	}
}
