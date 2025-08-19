using System;
using System.Collections.Generic;

namespace Terraria.Net
{
	// Token: 0x020000B8 RID: 184
	public class NetGroupInfo
	{
		// Token: 0x060013FB RID: 5115 RVA: 0x004A1E94 File Offset: 0x004A0094
		public NetGroupInfo()
		{
			this._infoProviders = new List<NetGroupInfo.INetGroupInfoProvider>();
			this._infoProviders.Add(new NetGroupInfo.IPAddressInfoProvider());
			this._infoProviders.Add(new NetGroupInfo.SteamLobbyInfoProvider());
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x004A1EFC File Offset: 0x004A00FC
		public string ComposeInfo()
		{
			List<string> list = new List<string>();
			foreach (NetGroupInfo.INetGroupInfoProvider netGroupInfoProvider in this._infoProviders)
			{
				if (netGroupInfoProvider.HasValidInfo)
				{
					string text = (int)netGroupInfoProvider.Id + this._separatorBetweenIdAndInfo[0] + netGroupInfoProvider.ProvideInfoNeededToJoin();
					string item = this.ConvertToSafeInfo(text);
					list.Add(item);
				}
			}
			return string.Join(this._separatorBetweenInfos[0], list.ToArray());
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x004A1F9C File Offset: 0x004A019C
		public Dictionary<NetGroupInfo.InfoProviderId, string> DecomposeInfo(string info)
		{
			Dictionary<NetGroupInfo.InfoProviderId, string> dictionary = new Dictionary<NetGroupInfo.InfoProviderId, string>();
			foreach (string text in info.Split(this._separatorBetweenInfos, StringSplitOptions.RemoveEmptyEntries))
			{
				string[] array2 = this.ConvertFromSafeInfo(text).Split(this._separatorBetweenIdAndInfo, StringSplitOptions.RemoveEmptyEntries);
				int key;
				if (array2.Length == 2 && int.TryParse(array2[0], out key))
				{
					dictionary[(NetGroupInfo.InfoProviderId)key] = array2[1];
				}
			}
			return dictionary;
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x004A2005 File Offset: 0x004A0205
		private string ConvertToSafeInfo(string text)
		{
			return Uri.EscapeDataString(text);
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x004A200D File Offset: 0x004A020D
		private string ConvertFromSafeInfo(string text)
		{
			return Uri.UnescapeDataString(text);
		}

		// Token: 0x040011DB RID: 4571
		private readonly string[] _separatorBetweenInfos = new string[]
		{
			", "
		};

		// Token: 0x040011DC RID: 4572
		private readonly string[] _separatorBetweenIdAndInfo = new string[]
		{
			":"
		};

		// Token: 0x040011DD RID: 4573
		private List<NetGroupInfo.INetGroupInfoProvider> _infoProviders;

		// Token: 0x02000559 RID: 1369
		public enum InfoProviderId
		{
			// Token: 0x040058EC RID: 22764
			IPAddress,
			// Token: 0x040058ED RID: 22765
			Steam
		}

		// Token: 0x0200055A RID: 1370
		private interface INetGroupInfoProvider
		{
			// Token: 0x170003A8 RID: 936
			// (get) Token: 0x06003115 RID: 12565
			NetGroupInfo.InfoProviderId Id { get; }

			// Token: 0x170003A9 RID: 937
			// (get) Token: 0x06003116 RID: 12566
			bool HasValidInfo { get; }

			// Token: 0x06003117 RID: 12567
			string ProvideInfoNeededToJoin();
		}

		// Token: 0x0200055B RID: 1371
		private class IPAddressInfoProvider : NetGroupInfo.INetGroupInfoProvider
		{
			// Token: 0x170003AA RID: 938
			// (get) Token: 0x06003118 RID: 12568 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
			public NetGroupInfo.InfoProviderId Id
			{
				get
				{
					return NetGroupInfo.InfoProviderId.IPAddress;
				}
			}

			// Token: 0x170003AB RID: 939
			// (get) Token: 0x06003119 RID: 12569 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
			public bool HasValidInfo
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600311A RID: 12570 RVA: 0x004E2992 File Offset: 0x004E0B92
			public string ProvideInfoNeededToJoin()
			{
				return "";
			}
		}

		// Token: 0x0200055C RID: 1372
		private class SteamLobbyInfoProvider : NetGroupInfo.INetGroupInfoProvider
		{
			// Token: 0x170003AC RID: 940
			// (get) Token: 0x0600311C RID: 12572 RVA: 0x0003266D File Offset: 0x0003086D
			public NetGroupInfo.InfoProviderId Id
			{
				get
				{
					return NetGroupInfo.InfoProviderId.Steam;
				}
			}

			// Token: 0x170003AD RID: 941
			// (get) Token: 0x0600311D RID: 12573 RVA: 0x005E4D55 File Offset: 0x005E2F55
			public bool HasValidInfo
			{
				get
				{
					return Main.LobbyId > 0UL;
				}
			}

			// Token: 0x0600311E RID: 12574 RVA: 0x005E4D60 File Offset: 0x005E2F60
			public string ProvideInfoNeededToJoin()
			{
				return Main.LobbyId.ToString();
			}
		}
	}
}
