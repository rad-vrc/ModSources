using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000412 RID: 1042
	public struct NPCKillAttempt
	{
		// Token: 0x06002B44 RID: 11076 RVA: 0x0059DED7 File Offset: 0x0059C0D7
		public NPCKillAttempt(NPC target)
		{
			this.npc = target;
			this.netId = target.netID;
			this.active = target.active;
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x0059DEF8 File Offset: 0x0059C0F8
		public bool DidNPCDie()
		{
			return !this.npc.active;
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x0059DF08 File Offset: 0x0059C108
		public bool DidNPCDieOrTransform()
		{
			return this.DidNPCDie() || this.npc.netID != this.netId;
		}

		// Token: 0x04004F67 RID: 20327
		public readonly NPC npc;

		// Token: 0x04004F68 RID: 20328
		public readonly int netId;

		// Token: 0x04004F69 RID: 20329
		public readonly bool active;
	}
}
