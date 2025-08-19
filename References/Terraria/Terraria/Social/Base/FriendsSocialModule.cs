using System;

namespace Terraria.Social.Base
{
	// Token: 0x02000191 RID: 401
	public abstract class FriendsSocialModule : ISocialModule
	{
		// Token: 0x06001B47 RID: 6983
		public abstract string GetUsername();

		// Token: 0x06001B48 RID: 6984
		public abstract void OpenJoinInterface();

		// Token: 0x06001B49 RID: 6985
		public abstract void Initialize();

		// Token: 0x06001B4A RID: 6986
		public abstract void Shutdown();
	}
}
