using System;
using Terraria.ID;

namespace Terraria.DataStructures
{
	// Token: 0x02000410 RID: 1040
	public class NPCDebuffImmunityData
	{
		// Token: 0x06002B35 RID: 11061 RVA: 0x0059DC08 File Offset: 0x0059BE08
		public void ApplyToNPC(NPC npc)
		{
			if (this.ImmuneToWhips || this.ImmuneToAllBuffsThatAreNotWhips)
			{
				for (int i = 1; i < BuffID.Count; i++)
				{
					bool flag = BuffID.Sets.IsAnNPCWhipDebuff[i];
					bool flag2 = false;
					flag2 |= (flag && this.ImmuneToWhips);
					flag2 |= (!flag && this.ImmuneToAllBuffsThatAreNotWhips);
					npc.buffImmune[i] = flag2;
				}
			}
			if (this.SpecificallyImmuneTo != null)
			{
				for (int j = 0; j < this.SpecificallyImmuneTo.Length; j++)
				{
					int num = this.SpecificallyImmuneTo[j];
					npc.buffImmune[num] = true;
				}
			}
		}

		// Token: 0x04004F61 RID: 20321
		public bool ImmuneToWhips;

		// Token: 0x04004F62 RID: 20322
		public bool ImmuneToAllBuffsThatAreNotWhips;

		// Token: 0x04004F63 RID: 20323
		public int[] SpecificallyImmuneTo;
	}
}
