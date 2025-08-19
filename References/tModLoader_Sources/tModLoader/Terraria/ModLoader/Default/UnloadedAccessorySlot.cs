using System;

namespace Terraria.ModLoader.Default
{
	// Token: 0x020002CE RID: 718
	[Autoload(false)]
	public class UnloadedAccessorySlot : ModAccessorySlot
	{
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06002DCF RID: 11727 RVA: 0x0053030A File Offset: 0x0052E50A
		public override string Name { get; }

		// Token: 0x06002DD0 RID: 11728 RVA: 0x00530312 File Offset: 0x0052E512
		internal UnloadedAccessorySlot(int slot, string oldName)
		{
			base.Type = slot;
			this.Name = oldName;
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x00530328 File Offset: 0x0052E528
		public override bool IsEnabled()
		{
			return false;
		}
	}
}
