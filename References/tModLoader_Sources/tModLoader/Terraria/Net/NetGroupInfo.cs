using System;
using System.Collections.Generic;

namespace Terraria.Net
{
	// Token: 0x0200011E RID: 286
	public class NetGroupInfo
	{
		// Token: 0x06001A13 RID: 6675 RVA: 0x004CAAC0 File Offset: 0x004C8CC0
		public NetGroupInfo()
		{
			this._infoProviders = new List<NetGroupInfo.INetGroupInfoProvider>();
			this._infoProviders.Add(new NetGroupInfo.IPAddressInfoProvider());
			this._infoProviders.Add(new NetGroupInfo.SteamLobbyInfoProvider());
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x004CAB28 File Offset: 0x004C8D28
		public string ComposeInfo()
		{
			List<string> list = new List<string>();
			foreach (NetGroupInfo.INetGroupInfoProvider infoProvider in this._infoProviders)
			{
				if (infoProvider.HasValidInfo)
				{
					string text = ((int)infoProvider.Id).ToString() + this._separatorBetweenIdAndInfo[0] + infoProvider.ProvideInfoNeededToJoin();
					string item = this.ConvertToSafeInfo(text);
					list.Add(item);
				}
			}
			return string.Join(this._separatorBetweenInfos[0], list.ToArray());
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x004CABCC File Offset: 0x004C8DCC
		public Dictionary<NetGroupInfo.InfoProviderId, string> DecomposeInfo(string info)
		{
			Dictionary<NetGroupInfo.InfoProviderId, string> dictionary = new Dictionary<NetGroupInfo.InfoProviderId, string>();
			foreach (string text in info.Split(this._separatorBetweenInfos, StringSplitOptions.RemoveEmptyEntries))
			{
				string[] array2 = this.ConvertFromSafeInfo(text).Split(this._separatorBetweenIdAndInfo, StringSplitOptions.RemoveEmptyEntries);
				int result;
				if (array2.Length == 2 && int.TryParse(array2[0], out result))
				{
					dictionary[(NetGroupInfo.InfoProviderId)result] = array2[1];
				}
			}
			return dictionary;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x004CAC35 File Offset: 0x004C8E35
		private string ConvertToSafeInfo(string text)
		{
			return Uri.EscapeDataString(text);
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x004CAC3D File Offset: 0x004C8E3D
		private string ConvertFromSafeInfo(string text)
		{
			return Uri.UnescapeDataString(text);
		}

		// Token: 0x04001414 RID: 5140
		private readonly string[] _separatorBetweenInfos = new string[]
		{
			", "
		};

		// Token: 0x04001415 RID: 5141
		private readonly string[] _separatorBetweenIdAndInfo = new string[]
		{
			":"
		};

		// Token: 0x04001416 RID: 5142
		private List<NetGroupInfo.INetGroupInfoProvider> _infoProviders;

		// Token: 0x0200089E RID: 2206
		public enum InfoProviderId
		{
			// Token: 0x04006A38 RID: 27192
			IPAddress,
			// Token: 0x04006A39 RID: 27193
			Steam
		}

		// Token: 0x0200089F RID: 2207
		private interface INetGroupInfoProvider
		{
			// Token: 0x170008C6 RID: 2246
			// (get) Token: 0x060051F4 RID: 20980
			NetGroupInfo.InfoProviderId Id { get; }

			// Token: 0x170008C7 RID: 2247
			// (get) Token: 0x060051F5 RID: 20981
			bool HasValidInfo { get; }

			// Token: 0x060051F6 RID: 20982
			string ProvideInfoNeededToJoin();
		}

		// Token: 0x020008A0 RID: 2208
		private class IPAddressInfoProvider : NetGroupInfo.INetGroupInfoProvider
		{
			// Token: 0x170008C8 RID: 2248
			// (get) Token: 0x060051F7 RID: 20983 RVA: 0x006987DF File Offset: 0x006969DF
			public NetGroupInfo.InfoProviderId Id
			{
				get
				{
					return NetGroupInfo.InfoProviderId.IPAddress;
				}
			}

			// Token: 0x170008C9 RID: 2249
			// (get) Token: 0x060051F8 RID: 20984 RVA: 0x006987E2 File Offset: 0x006969E2
			public bool HasValidInfo
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060051F9 RID: 20985 RVA: 0x006987E5 File Offset: 0x006969E5
			public string ProvideInfoNeededToJoin()
			{
				return "";
			}
		}

		// Token: 0x020008A1 RID: 2209
		private class SteamLobbyInfoProvider : NetGroupInfo.INetGroupInfoProvider
		{
			// Token: 0x170008CA RID: 2250
			// (get) Token: 0x060051FB RID: 20987 RVA: 0x006987F4 File Offset: 0x006969F4
			public NetGroupInfo.InfoProviderId Id
			{
				get
				{
					return NetGroupInfo.InfoProviderId.Steam;
				}
			}

			// Token: 0x170008CB RID: 2251
			// (get) Token: 0x060051FC RID: 20988 RVA: 0x006987F7 File Offset: 0x006969F7
			public bool HasValidInfo
			{
				get
				{
					return Main.LobbyId > 0UL;
				}
			}

			// Token: 0x060051FD RID: 20989 RVA: 0x00698802 File Offset: 0x00696A02
			public string ProvideInfoNeededToJoin()
			{
				return Main.LobbyId.ToString();
			}
		}
	}
}
