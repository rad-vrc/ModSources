using System;
using rail;
using Terraria.Social.Base;

namespace Terraria.Social.WeGame
{
	// Token: 0x020000CE RID: 206
	public class FriendsSocialModule : FriendsSocialModule
	{
		// Token: 0x060016DC RID: 5852 RVA: 0x004B5FD3 File Offset: 0x004B41D3
		public override void Initialize()
		{
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x004B5FD5 File Offset: 0x004B41D5
		public override void Shutdown()
		{
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x004B5FD8 File Offset: 0x004B41D8
		public override string GetUsername()
		{
			string name;
			rail_api.RailFactory().RailPlayer().GetPlayerName(ref name);
			WeGameHelper.WriteDebugString("GetUsername by wegame" + name, Array.Empty<object>());
			return name;
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x004B600D File Offset: 0x004B420D
		public override void OpenJoinInterface()
		{
			WeGameHelper.WriteDebugString("OpenJoinInterface by wegame", Array.Empty<object>());
			rail_api.RailFactory().RailFloatingWindow().AsyncShowRailFloatingWindow(10, "");
		}
	}
}
