using System;

namespace Terraria.Social.Base
{
	// Token: 0x020000FC RID: 252
	public abstract class FriendsSocialModule : ISocialModule
	{
		// Token: 0x060018C8 RID: 6344
		public abstract string GetUsername();

		// Token: 0x060018C9 RID: 6345
		public abstract void OpenJoinInterface();

		// Token: 0x060018CA RID: 6346
		public abstract void Initialize();

		// Token: 0x060018CB RID: 6347
		public abstract void Shutdown();
	}
}
