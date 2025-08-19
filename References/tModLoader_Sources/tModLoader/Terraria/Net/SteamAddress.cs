using System;
using Steamworks;

namespace Terraria.Net
{
	// Token: 0x02000124 RID: 292
	public class SteamAddress : RemoteAddress
	{
		// Token: 0x06001A37 RID: 6711 RVA: 0x004CB0C8 File Offset: 0x004C92C8
		public SteamAddress(CSteamID steamId)
		{
			this.Type = AddressType.Steam;
			this.SteamId = steamId;
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x004CB0E0 File Offset: 0x004C92E0
		public override string ToString()
		{
			string text = (this.SteamId.m_SteamID % 2UL).ToString();
			string text2 = ((this.SteamId.m_SteamID - (76561197960265728UL + this.SteamId.m_SteamID % 2UL)) / 2UL).ToString();
			return "STEAM_0:" + text + ":" + text2;
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x004CB145 File Offset: 0x004C9345
		public override string GetIdentifier()
		{
			return this.ToString();
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x004CB14D File Offset: 0x004C934D
		public override bool IsLocalHost()
		{
			return Program.LaunchParameters.ContainsKey("-localsteamid") && Program.LaunchParameters["-localsteamid"].Equals(this.SteamId.m_SteamID.ToString());
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x004CB186 File Offset: 0x004C9386
		public override string GetFriendlyName()
		{
			if (this._friendlyName == null)
			{
				this._friendlyName = SteamFriends.GetFriendPersonaName(this.SteamId);
			}
			return this._friendlyName;
		}

		// Token: 0x04001424 RID: 5156
		public readonly CSteamID SteamId;

		// Token: 0x04001425 RID: 5157
		private string _friendlyName;
	}
}
