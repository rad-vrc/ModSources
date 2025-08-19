using System;

namespace Terraria.ModLoader
{
	// Token: 0x02000178 RID: 376
	public interface IEntityWithGlobals<TGlobal> where TGlobal : GlobalType<TGlobal>
	{
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06001DF2 RID: 7666
		int Type { get; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06001DF3 RID: 7667
		RefReadOnlyArray<TGlobal> EntityGlobals { get; }
	}
}
