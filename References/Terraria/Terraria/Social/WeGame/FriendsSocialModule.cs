using System;
using rail;
using Terraria.Social.Base;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000156 RID: 342
	public class FriendsSocialModule : FriendsSocialModule
	{
		// Token: 0x06001961 RID: 6497 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void Initialize()
		{
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void Shutdown()
		{
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x004E0D50 File Offset: 0x004DEF50
		public override string GetUsername()
		{
			string text;
			rail_api.RailFactory().RailPlayer().GetPlayerName(ref text);
			WeGameHelper.WriteDebugString("GetUsername by wegame" + text, new object[0]);
			return text;
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x004E0D86 File Offset: 0x004DEF86
		public override void OpenJoinInterface()
		{
			WeGameHelper.WriteDebugString("OpenJoinInterface by wegame", new object[0]);
			rail_api.RailFactory().RailFloatingWindow().AsyncShowRailFloatingWindow(10, "");
		}
	}
}
