using System;
using System.Collections.Generic;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020005BA RID: 1466
	public struct HelperInfo
	{
		// Token: 0x040059C8 RID: 22984
		public Player player;

		// Token: 0x040059C9 RID: 22985
		public NPC npc;

		// Token: 0x040059CA RID: 22986
		public List<NPC> NearbyNPCs;

		// Token: 0x040059CB RID: 22987
		public bool[] nearbyNPCsByType;
	}
}
