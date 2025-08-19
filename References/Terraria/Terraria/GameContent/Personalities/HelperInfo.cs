using System;
using System.Collections.Generic;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020003D5 RID: 981
	public struct HelperInfo
	{
		// Token: 0x04004D3C RID: 19772
		public Player player;

		// Token: 0x04004D3D RID: 19773
		public NPC npc;

		// Token: 0x04004D3E RID: 19774
		public List<NPC> NearbyNPCs;

		// Token: 0x04004D3F RID: 19775
		public bool[] nearbyNPCsByType;
	}
}
