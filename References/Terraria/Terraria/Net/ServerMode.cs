using System;

namespace Terraria.Net
{
	// Token: 0x020000BE RID: 190
	[Flags]
	public enum ServerMode : byte
	{
		// Token: 0x040011EA RID: 4586
		None = 0,
		// Token: 0x040011EB RID: 4587
		Lobby = 1,
		// Token: 0x040011EC RID: 4588
		FriendsCanJoin = 2,
		// Token: 0x040011ED RID: 4589
		FriendsOfFriends = 4
	}
}
