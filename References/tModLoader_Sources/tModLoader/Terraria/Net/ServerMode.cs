using System;

namespace Terraria.Net
{
	// Token: 0x02000123 RID: 291
	[Flags]
	public enum ServerMode : byte
	{
		// Token: 0x04001420 RID: 5152
		None = 0,
		// Token: 0x04001421 RID: 5153
		Lobby = 1,
		// Token: 0x04001422 RID: 5154
		FriendsCanJoin = 2,
		// Token: 0x04001423 RID: 5155
		FriendsOfFriends = 4
	}
}
