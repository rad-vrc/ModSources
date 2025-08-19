using System;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200067A RID: 1658
	public struct BestiaryUnlockProgressReport
	{
		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x060047BA RID: 18362 RVA: 0x00646499 File Offset: 0x00644699
		public float CompletionPercent
		{
			get
			{
				if (this.EntriesTotal == 0)
				{
					return 1f;
				}
				return this.CompletionAmountTotal / (float)this.EntriesTotal;
			}
		}

		// Token: 0x04005BF2 RID: 23538
		public int EntriesTotal;

		// Token: 0x04005BF3 RID: 23539
		public float CompletionAmountTotal;
	}
}
