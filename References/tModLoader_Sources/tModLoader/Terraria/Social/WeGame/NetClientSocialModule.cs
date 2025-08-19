using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using rail;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Net;
using Terraria.Net.Sockets;

namespace Terraria.Social.WeGame
{
	// Token: 0x020000DA RID: 218
	public class NetClientSocialModule : NetSocialModule
	{
		// Token: 0x06001742 RID: 5954 RVA: 0x004B6FC5 File Offset: 0x004B51C5
		private void OnIPCClientAccess()
		{
			WeGameHelper.WriteDebugString("IPC client access", Array.Empty<object>());
			this.SendFriendListToLocalServer();
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x004B6FE0 File Offset: 0x004B51E0
		private void LazyCreateWeGameMsgServer()
		{
			if (this._msgServer == null)
			{
				this._msgServer = new MessageDispatcherServer();
				this._msgServer.Init("WeGame.Terraria.Message.Server");
				this._msgServer.OnMessage += this.OnWegameMessage;
				this._msgServer.OnIPCClientAccess += this.OnIPCClientAccess;
				CoreSocialModule.OnTick += this._msgServer.Tick;
				this._msgServer.Start();
			}
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x004B7060 File Offset: 0x004B5260
		private void OnWegameMessage(IPCMessage message)
		{
			if (message.GetCmd() == IPCMessageType.IPCMessageTypeReportServerID)
			{
				ReportServerID value;
				message.Parse<ReportServerID>(out value);
				this.OnReportServerID(value);
			}
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x004B7084 File Offset: 0x004B5284
		private void OnReportServerID(ReportServerID reportServerID)
		{
			WeGameHelper.WriteDebugString("OnReportServerID - " + reportServerID._serverID, Array.Empty<object>());
			this.AsyncSetMyMetaData(this._serverIDMedataKey, reportServerID._serverID);
			this.AsyncSetInviteCommandLine(reportServerID._serverID);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x004B70C0 File Offset: 0x004B52C0
		public override void Initialize()
		{
			base.Initialize();
			this.RegisterRailEvent();
			this.AsyncGetFriendsInfo();
			this._reader.SetReadEvent(new WeGameP2PReader.OnReadEvent(this.OnPacketRead));
			this._reader.SetLocalPeer(base.GetLocalPeer());
			this._writer.SetLocalPeer(base.GetLocalPeer());
			Main.OnEngineLoad += this.CheckParameters;
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x004B7129 File Offset: 0x004B5329
		private void AsyncSetInviteCommandLine(string cmdline)
		{
			rail_api.RailFactory().RailFriends().AsyncSetInviteCommandLine(cmdline, "");
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x004B7144 File Offset: 0x004B5344
		private void AsyncSetMyMetaData(string key, string value)
		{
			List<RailKeyValue> list = new List<RailKeyValue>();
			list.Add(new RailKeyValue
			{
				key = key,
				value = value
			});
			rail_api.RailFactory().RailFriends().AsyncSetMyMetadata(list, "");
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x004B7188 File Offset: 0x004B5388
		private bool TryAuthUserByRecvData(RailID user, byte[] data, int length)
		{
			WeGameHelper.WriteDebugString("TryAuthUserByRecvData user:{0}", new object[]
			{
				user.id_
			});
			if (length < 3)
			{
				WeGameHelper.WriteDebugString("Failed to validate authentication packet: Too short. (Length: " + length.ToString() + ")", Array.Empty<object>());
				return false;
			}
			int num = (int)data[1] << 8 | (int)data[0];
			if (num != length)
			{
				WeGameHelper.WriteDebugString(string.Concat(new string[]
				{
					"Failed to validate authentication packet: Packet size mismatch. (",
					num.ToString(),
					"!=",
					length.ToString(),
					")"
				}), Array.Empty<object>());
				return false;
			}
			if (data[2] != 93)
			{
				WeGameHelper.WriteDebugString("Failed to validate authentication packet: Packet type is not correct. (Type: " + data[2].ToString() + ")", Array.Empty<object>());
				return false;
			}
			return true;
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x004B725C File Offset: 0x004B545C
		private bool OnPacketRead(byte[] data, int size, RailID user)
		{
			if (!this._connectionStateMap.ContainsKey(user))
			{
				return false;
			}
			NetSocialModule.ConnectionState connectionState = this._connectionStateMap[user];
			if (connectionState == NetSocialModule.ConnectionState.Authenticating)
			{
				if (!this.TryAuthUserByRecvData(user, data, size))
				{
					WeGameHelper.WriteDebugString(" Auth Server Ticket Failed", Array.Empty<object>());
					this.Close(user);
				}
				else
				{
					WeGameHelper.WriteDebugString("OnRailAuthSessionTicket Auth Success..", Array.Empty<object>());
					this.OnAuthSuccess(user);
				}
				return false;
			}
			return connectionState == NetSocialModule.ConnectionState.Connected;
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x004B72CC File Offset: 0x004B54CC
		private void OnAuthSuccess(RailID remote_peer)
		{
			if (this._connectionStateMap.ContainsKey(remote_peer))
			{
				this._connectionStateMap[remote_peer] = NetSocialModule.ConnectionState.Connected;
				this.AsyncSetPlayWith(this._inviter_id);
				this.AsyncSetMyMetaData("status", Language.GetTextValue("Social.StatusInGame"));
				this.AsyncSetMyMetaData(this._serverIDMedataKey, remote_peer.id_.ToString());
				Main.clrInput();
				Netplay.ServerPassword = "";
				Main.GetInputText("", false);
				Main.autoPass = false;
				Main.netMode = 1;
				Netplay.OnConnectedToSocialServer(new SocialSocket(new WeGameAddress(remote_peer, this.GetFriendNickname(this._inviter_id))));
				WeGameHelper.WriteDebugString("OnConnectToSocialServer server:" + remote_peer.id_.ToString(), Array.Empty<object>());
			}
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x004B7394 File Offset: 0x004B5594
		private bool GetRailConnectIDFromCmdLine(RailID server_id)
		{
			foreach (string text in Environment.GetCommandLineArgs())
			{
				string text2 = "--rail_connect_cmd=";
				int num = text.IndexOf(text2);
				if (num != -1)
				{
					ulong result = 0UL;
					if (ulong.TryParse(text.Substring(num + text2.Length), out result))
					{
						server_id.id_ = result;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x004B73F4 File Offset: 0x004B55F4
		private void CheckParameters()
		{
			RailID server_id = new RailID();
			if (!this.GetRailConnectIDFromCmdLine(server_id))
			{
				return;
			}
			if (server_id.IsValid())
			{
				Main.OpenPlayerSelect(delegate(PlayerFileData playerData)
				{
					Main.ServerSideCharacter = false;
					playerData.SetAsActive();
					Main.menuMode = 882;
					Main.statusText = Language.GetTextValue("Social.Joining");
					WeGameHelper.WriteDebugString(" CheckParameters， lobby.join", Array.Empty<object>());
					this.JoinServer(server_id);
				});
				return;
			}
			WeGameHelper.WriteDebugString("Invalid RailID passed to +connect_lobby", Array.Empty<object>());
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x004B7458 File Offset: 0x004B5658
		public override void LaunchLocalServer(Process process, ServerMode mode)
		{
			if (this._lobby.State != LobbyState.Inactive)
			{
				this._lobby.Leave();
			}
			this.LazyCreateWeGameMsgServer();
			ProcessStartInfo startInfo = process.StartInfo;
			startInfo.Arguments = startInfo.Arguments + " -wegame -localwegameid " + base.GetLocalPeer().id_.ToString();
			if (mode.HasFlag(ServerMode.Lobby))
			{
				this._hasLocalHost = true;
				if (mode.HasFlag(ServerMode.FriendsCanJoin))
				{
					ProcessStartInfo startInfo2 = process.StartInfo;
					startInfo2.Arguments += " -lobby friends";
				}
				else
				{
					ProcessStartInfo startInfo3 = process.StartInfo;
					startInfo3.Arguments += " -lobby private";
				}
				if (mode.HasFlag(ServerMode.FriendsOfFriends))
				{
					ProcessStartInfo startInfo4 = process.StartInfo;
					startInfo4.Arguments += " -friendsoffriends";
				}
			}
			string parameter;
			rail_api.RailFactory().RailUtils().GetLaunchAppParameters(2, ref parameter);
			ProcessStartInfo startInfo5 = process.StartInfo;
			startInfo5.Arguments = startInfo5.Arguments + " " + parameter;
			WeGameHelper.WriteDebugString("LaunchLocalServer,cmd_line:" + process.StartInfo.Arguments, Array.Empty<object>());
			this.AsyncSetMyMetaData("status", Language.GetTextValue("Social.StatusInGame"));
			Netplay.OnDisconnect += this.OnDisconnect;
			process.Start();
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x004B75BF File Offset: 0x004B57BF
		public override void Shutdown()
		{
			this.AsyncSetInviteCommandLine("");
			this.CleanMyMetaData();
			this.UnRegisterRailEvent();
			base.Shutdown();
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x004B75DE File Offset: 0x004B57DE
		public override ulong GetLobbyId()
		{
			return 0UL;
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x004B75E2 File Offset: 0x004B57E2
		public override bool StartListening(SocketConnectionAccepted callback)
		{
			return false;
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x004B75E5 File Offset: 0x004B57E5
		public override void StopListening()
		{
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x004B75E8 File Offset: 0x004B57E8
		public override void Close(RemoteAddress address)
		{
			this.CleanMyMetaData();
			RailID remote_peer = base.RemoteAddressToRailId(address);
			this.Close(remote_peer);
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x004B760A File Offset: 0x004B580A
		public override bool CanInvite()
		{
			return (this._hasLocalHost || this._lobby.State == LobbyState.Active || Main.LobbyId != 0UL) && Main.netMode != 0;
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x004B7633 File Offset: 0x004B5833
		public override void OpenInviteInterface()
		{
			this._lobby.OpenInviteOverlay();
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x004B7640 File Offset: 0x004B5840
		private void Close(RailID remote_peer)
		{
			if (this._connectionStateMap.ContainsKey(remote_peer))
			{
				WeGameHelper.WriteDebugString("CloseRemotePeer, remote:{0}", new object[]
				{
					remote_peer.id_
				});
				rail_api.RailFactory().RailNetworkHelper().CloseSession(base.GetLocalPeer(), remote_peer);
				this._connectionStateMap[remote_peer] = NetSocialModule.ConnectionState.Inactive;
				this._lobby.Leave();
				this._reader.ClearUser(remote_peer);
				this._writer.ClearUser(remote_peer);
			}
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x004B76C0 File Offset: 0x004B58C0
		public override void Connect(RemoteAddress address)
		{
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x004B76C2 File Offset: 0x004B58C2
		public override void CancelJoin()
		{
			if (this._lobby.State != LobbyState.Inactive)
			{
				this._lobby.Leave();
			}
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x004B76DC File Offset: 0x004B58DC
		private void RegisterRailEvent()
		{
			RAILEventID[] array = new RAILEventID[7];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.F5B6CD281943791DEA07CEF44DAAFB8820A1A72B3E7374CE3C6E5CDFEAA7A25A).FieldHandle);
			foreach (RAILEventID event_id in array)
			{
				this._callbackHelper.RegisterCallback(event_id, new RailEventCallBackHandler(this.OnRailEvent));
			}
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x004B7725 File Offset: 0x004B5925
		private void UnRegisterRailEvent()
		{
			this._callbackHelper.UnregisterAllCallback();
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x004B7734 File Offset: 0x004B5934
		public void OnRailEvent(RAILEventID id, EventBase data)
		{
			WeGameHelper.WriteDebugString("OnRailEvent,id=" + id.ToString() + " ,result=" + data.result.ToString(), Array.Empty<object>());
			if (id <= 12010)
			{
				if (id == 12002)
				{
					this.OnRailSetMetaData((RailFriendsSetMetadataResult)data);
					return;
				}
				if (id == 12003)
				{
					this.OnGetFriendMetaData((RailFriendsGetMetadataResult)data);
					return;
				}
				if (id != 12010)
				{
					return;
				}
				this.OnFriendlistChange((RailFriendsListChanged)data);
				return;
			}
			else if (id <= 13503)
			{
				if (id == 13501)
				{
					this.OnRailGetUsersInfo((RailUsersInfoData)data);
					return;
				}
				if (id != 13503)
				{
					return;
				}
				this.OnRailRespondInvation((RailUsersRespondInvitation)data);
				return;
			}
			else
			{
				if (id == 16001)
				{
					this.OnRailCreateSessionRequest((CreateSessionRequest)data);
					return;
				}
				if (id != 16002)
				{
					return;
				}
				this.OnRailCreateSessionFailed((CreateSessionFailed)data);
				return;
			}
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x004B7820 File Offset: 0x004B5A20
		private string DumpMataDataString(List<RailKeyValueResult> list)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (RailKeyValueResult item in list)
			{
				stringBuilder.Append("key: " + item.key + " value: " + item.value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x004B7898 File Offset: 0x004B5A98
		private string GetValueByKey(string key, List<RailKeyValueResult> list)
		{
			string result = null;
			foreach (RailKeyValueResult item in list)
			{
				if (item.key == key)
				{
					return item.value;
				}
			}
			return result;
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x004B78FC File Offset: 0x004B5AFC
		private bool SendFriendListToLocalServer()
		{
			bool result = false;
			if (this._hasLocalHost)
			{
				List<RailFriendInfo> list = new List<RailFriendInfo>();
				if (this.GetRailFriendList(list))
				{
					WeGameFriendListInfo t = new WeGameFriendListInfo
					{
						_friendList = list
					};
					IPCMessage iPCMessage = new IPCMessage();
					iPCMessage.Build<WeGameFriendListInfo>(IPCMessageType.IPCMessageTypeNotifyFriendList, t);
					result = this._msgServer.SendMessage(iPCMessage);
					WeGameHelper.WriteDebugString("NotifyFriendListToServer: " + result.ToString(), Array.Empty<object>());
				}
			}
			return result;
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x004B7968 File Offset: 0x004B5B68
		private bool GetRailFriendList(List<RailFriendInfo> list)
		{
			bool result = false;
			IRailFriends railFriends = rail_api.RailFactory().RailFriends();
			if (railFriends != null)
			{
				result = (railFriends.GetFriendsList(list) == 0);
			}
			return result;
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x004B7994 File Offset: 0x004B5B94
		private void OnGetFriendMetaData(RailFriendsGetMetadataResult data)
		{
			if (data.result != null || data.friend_kvs.Count <= 0)
			{
				return;
			}
			WeGameHelper.WriteDebugString("OnGetFriendMetaData - " + this.DumpMataDataString(data.friend_kvs), Array.Empty<object>());
			string valueByKey = this.GetValueByKey(this._serverIDMedataKey, data.friend_kvs);
			if (valueByKey == null)
			{
				return;
			}
			if (valueByKey.Length <= 0)
			{
				WeGameHelper.WriteDebugString("can not find server id key", Array.Empty<object>());
				return;
			}
			RailID railID = new RailID();
			railID.id_ = ulong.Parse(valueByKey);
			if (railID.IsValid())
			{
				this.JoinServer(railID);
				return;
			}
			WeGameHelper.WriteDebugString("JoinServer failed, invalid server id", Array.Empty<object>());
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x004B7A3C File Offset: 0x004B5C3C
		private void JoinServer(RailID server_id)
		{
			WeGameHelper.WriteDebugString("JoinServer:{0}", new object[]
			{
				server_id.id_
			});
			this._connectionStateMap[server_id] = NetSocialModule.ConnectionState.Authenticating;
			int num = 3;
			byte[] array = new byte[num];
			array[0] = (byte)(num & 255);
			array[1] = (byte)(num >> 8 & 255);
			array[2] = 93;
			rail_api.RailFactory().RailNetworkHelper().SendReliableData(base.GetLocalPeer(), server_id, array, (uint)num);
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x004B7AB4 File Offset: 0x004B5CB4
		private string GetFriendNickname(RailID rail_id)
		{
			if (this._player_info_list != null)
			{
				foreach (PlayerPersonalInfo item in this._player_info_list)
				{
					if (item.rail_id == rail_id)
					{
						return item.rail_name;
					}
				}
			}
			return "";
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x004B7B28 File Offset: 0x004B5D28
		private void OnRailGetUsersInfo(RailUsersInfoData data)
		{
			this._player_info_list = data.user_info_list;
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x004B7B36 File Offset: 0x004B5D36
		private void OnFriendlistChange(RailFriendsListChanged data)
		{
			if (this._hasLocalHost)
			{
				this.SendFriendListToLocalServer();
			}
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x004B7B48 File Offset: 0x004B5D48
		private void AsyncGetFriendsInfo()
		{
			IRailFriends railFriends = rail_api.RailFactory().RailFriends();
			if (railFriends == null)
			{
				return;
			}
			List<RailFriendInfo> list = new List<RailFriendInfo>();
			railFriends.GetFriendsList(list);
			List<RailID> list2 = new List<RailID>();
			foreach (RailFriendInfo item in list)
			{
				list2.Add(item.friend_rail_id);
			}
			railFriends.AsyncGetPersonalInfo(list2, "");
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x004B7BD0 File Offset: 0x004B5DD0
		private void AsyncSetPlayWith(RailID rail_id)
		{
			List<RailUserPlayedWith> list = new List<RailUserPlayedWith>();
			list.Add(new RailUserPlayedWith
			{
				rail_id = rail_id
			});
			IRailFriends railFriends = rail_api.RailFactory().RailFriends();
			if (railFriends == null)
			{
				return;
			}
			railFriends.AsyncReportPlayedWithUserList(list, "");
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x004B7C12 File Offset: 0x004B5E12
		private void OnRailSetMetaData(RailFriendsSetMetadataResult data)
		{
			WeGameHelper.WriteDebugString("OnRailSetMetaData - " + data.result.ToString(), Array.Empty<object>());
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x004B7C3C File Offset: 0x004B5E3C
		private void OnRailRespondInvation(RailUsersRespondInvitation data)
		{
			WeGameHelper.WriteDebugString(" request join game", Array.Empty<object>());
			if (this._lobby.State != LobbyState.Inactive)
			{
				this._lobby.Leave();
			}
			this._inviter_id = data.inviter_id;
			Main.OpenPlayerSelect(delegate(PlayerFileData playerData)
			{
				Main.ServerSideCharacter = false;
				playerData.SetAsActive();
				Main.menuMode = 882;
				Main.statusText = Language.GetTextValue("Social.JoiningFriend", this.GetFriendNickname(data.inviter_id));
				this.AsyncGetServerIDByOwener(data.inviter_id);
				WeGameHelper.WriteDebugString("inviter_id: " + data.inviter_id.id_.ToString(), Array.Empty<object>());
			});
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x004B7CA8 File Offset: 0x004B5EA8
		private void AsyncGetServerIDByOwener(RailID ownerID)
		{
			List<string> list = new List<string>();
			list.Add(this._serverIDMedataKey);
			IRailFriends railFriends = rail_api.RailFactory().RailFriends();
			if (railFriends == null)
			{
				return;
			}
			railFriends.AsyncGetFriendMetadata(ownerID, list, "");
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x004B7CE4 File Offset: 0x004B5EE4
		private void OnRailCreateSessionRequest(CreateSessionRequest result)
		{
			WeGameHelper.WriteDebugString("OnRailCreateSessionRequest", Array.Empty<object>());
			if (this._connectionStateMap.ContainsKey(result.remote_peer) && this._connectionStateMap[result.remote_peer] != NetSocialModule.ConnectionState.Inactive)
			{
				WeGameHelper.WriteDebugString("AcceptSessionRequest, local{0}, remote:{1}", new object[]
				{
					result.local_peer.id_,
					result.remote_peer.id_
				});
				rail_api.RailFactory().RailNetworkHelper().AcceptSessionRequest(result.local_peer, result.remote_peer);
			}
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x004B7D78 File Offset: 0x004B5F78
		private void OnRailCreateSessionFailed(CreateSessionFailed result)
		{
			WeGameHelper.WriteDebugString("OnRailCreateSessionFailed, CloseRemote: local:{0}, remote:{1}", new object[]
			{
				result.local_peer.id_,
				result.remote_peer.id_
			});
			this.Close(result.remote_peer);
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x004B7DC7 File Offset: 0x004B5FC7
		private void CleanMyMetaData()
		{
			IRailFriends railFriends = rail_api.RailFactory().RailFriends();
			if (railFriends == null)
			{
				return;
			}
			railFriends.AsyncClearAllMyMetadata("");
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x004B7DE3 File Offset: 0x004B5FE3
		private void OnDisconnect()
		{
			this.CleanMyMetaData();
			this._hasLocalHost = false;
			Netplay.OnDisconnect -= this.OnDisconnect;
		}

		// Token: 0x040012FB RID: 4859
		private RailCallBackHelper _callbackHelper = new RailCallBackHelper();

		// Token: 0x040012FC RID: 4860
		private bool _hasLocalHost;

		// Token: 0x040012FD RID: 4861
		private IPCServer server = new IPCServer();

		// Token: 0x040012FE RID: 4862
		private readonly string _serverIDMedataKey = "terraria.serverid";

		// Token: 0x040012FF RID: 4863
		private RailID _inviter_id = new RailID();

		// Token: 0x04001300 RID: 4864
		private List<PlayerPersonalInfo> _player_info_list;

		// Token: 0x04001301 RID: 4865
		private MessageDispatcherServer _msgServer;
	}
}
