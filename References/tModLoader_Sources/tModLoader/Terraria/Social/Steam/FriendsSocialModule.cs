using System;
using Steamworks;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x020000E6 RID: 230
	public class FriendsSocialModule : FriendsSocialModule
	{
		// Token: 0x060017E0 RID: 6112 RVA: 0x004B994E File Offset: 0x004B7B4E
		public override void Initialize()
		{
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x004B9950 File Offset: 0x004B7B50
		public override void Shutdown()
		{
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x004B9952 File Offset: 0x004B7B52
		public override string GetUsername()
		{
			return SteamFriends.GetPersonaName();
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x004B9959 File Offset: 0x004B7B59
		public override void OpenJoinInterface()
		{
			SteamFriends.ActivateGameOverlay("Friends");
		}
	}
}
