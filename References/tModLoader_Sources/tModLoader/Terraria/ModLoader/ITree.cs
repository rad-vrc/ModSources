using System;
using Terraria.Enums;

namespace Terraria.ModLoader
{
	// Token: 0x020001BC RID: 444
	public interface ITree : IPlant, ILoadable
	{
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06002284 RID: 8836
		TreeTypes CountsAsTreeType { get; }

		// Token: 0x06002285 RID: 8837
		int TreeLeaf();

		// Token: 0x06002286 RID: 8838
		bool Shake(int x, int y, ref bool createLeaves);
	}
}
