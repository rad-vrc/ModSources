using System;
using Steamworks;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x02000176 RID: 374
	public class FriendsSocialModule : FriendsSocialModule
	{
		// Token: 0x06001A8B RID: 6795 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void Initialize()
		{
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void Shutdown()
		{
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x004E4F21 File Offset: 0x004E3121
		public override string GetUsername()
		{
			return SteamFriends.GetPersonaName();
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x004E4F28 File Offset: 0x004E3128
		public override void OpenJoinInterface()
		{
			SteamFriends.ActivateGameOverlay("Friends");
		}
	}
}
