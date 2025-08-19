using System;
using Terraria.Utilities;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x020005F4 RID: 1524
	public struct DropAttemptInfo
	{
		// Token: 0x04005A37 RID: 23095
		public NPC npc;

		// Token: 0x04005A38 RID: 23096
		public int item;

		// Token: 0x04005A39 RID: 23097
		public Player player;

		// Token: 0x04005A3A RID: 23098
		public UnifiedRandom rng;

		// Token: 0x04005A3B RID: 23099
		public bool IsInSimulation;

		// Token: 0x04005A3C RID: 23100
		public bool IsExpertMode;

		// Token: 0x04005A3D RID: 23101
		public bool IsMasterMode;
	}
}
