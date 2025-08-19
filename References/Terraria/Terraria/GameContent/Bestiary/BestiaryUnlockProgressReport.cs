using System;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x020002EA RID: 746
	public struct BestiaryUnlockProgressReport
	{
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06002367 RID: 9063 RVA: 0x0054EC74 File Offset: 0x0054CE74
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

		// Token: 0x04004828 RID: 18472
		public int EntriesTotal;

		// Token: 0x04004829 RID: 18473
		public float CompletionAmountTotal;
	}
}
