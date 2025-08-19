using System;
using Terraria.Utilities;

namespace Terraria.GameContent.ItemDropRules
{
	// Token: 0x0200027F RID: 639
	public struct DropAttemptInfo
	{
		// Token: 0x040046B7 RID: 18103
		public NPC npc;

		// Token: 0x040046B8 RID: 18104
		public Player player;

		// Token: 0x040046B9 RID: 18105
		public UnifiedRandom rng;

		// Token: 0x040046BA RID: 18106
		public bool IsInSimulation;

		// Token: 0x040046BB RID: 18107
		public bool IsExpertMode;

		// Token: 0x040046BC RID: 18108
		public bool IsMasterMode;
	}
}
