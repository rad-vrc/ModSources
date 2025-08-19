using System;
using Steamworks;

namespace Terraria.Net
{
	// Token: 0x020000BC RID: 188
	public class SteamAddress : RemoteAddress
	{
		// Token: 0x06001409 RID: 5129 RVA: 0x004A2071 File Offset: 0x004A0271
		public SteamAddress(CSteamID steamId)
		{
			this.Type = AddressType.Steam;
			this.SteamId = steamId;
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x004A2088 File Offset: 0x004A0288
		public override string ToString()
		{
			string str = (this.SteamId.m_SteamID % 2UL).ToString();
			string str2 = ((this.SteamId.m_SteamID - (76561197960265728UL + this.SteamId.m_SteamID % 2UL)) / 2UL).ToString();
			return "STEAM_0:" + str + ":" + str2;
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x004A2069 File Offset: 0x004A0269
		public override string GetIdentifier()
		{
			return this.ToString();
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x004A20F0 File Offset: 0x004A02F0
		public override bool IsLocalHost()
		{
			return Program.LaunchParameters.ContainsKey("-localsteamid") && Program.LaunchParameters["-localsteamid"].Equals(this.SteamId.m_SteamID.ToString());
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x004A2137 File Offset: 0x004A0337
		public override string GetFriendlyName()
		{
			if (this._friendlyName == null)
			{
				this._friendlyName = SteamFriends.GetFriendPersonaName(this.SteamId);
			}
			return this._friendlyName;
		}

		// Token: 0x040011E5 RID: 4581
		public readonly CSteamID SteamId;

		// Token: 0x040011E6 RID: 4582
		private string _friendlyName;
	}
}
