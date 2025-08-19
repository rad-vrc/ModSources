using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000720 RID: 1824
	public struct NPCKillAttempt
	{
		// Token: 0x060049F8 RID: 18936 RVA: 0x0064EADB File Offset: 0x0064CCDB
		public NPCKillAttempt(NPC target)
		{
			this.npc = target;
			this.netId = target.netID;
			this.active = target.active;
		}

		// Token: 0x060049F9 RID: 18937 RVA: 0x0064EAFC File Offset: 0x0064CCFC
		public bool DidNPCDie()
		{
			return !this.npc.active;
		}

		// Token: 0x060049FA RID: 18938 RVA: 0x0064EB0C File Offset: 0x0064CD0C
		public bool DidNPCDieOrTransform()
		{
			return this.DidNPCDie() || this.npc.netID != this.netId;
		}

		// Token: 0x04005F1D RID: 24349
		public readonly NPC npc;

		// Token: 0x04005F1E RID: 24350
		public readonly int netId;

		// Token: 0x04005F1F RID: 24351
		public readonly bool active;
	}
}
