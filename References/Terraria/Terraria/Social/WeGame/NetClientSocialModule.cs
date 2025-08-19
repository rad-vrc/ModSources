using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using rail;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Net;
using Terraria.Net.Sockets;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000159 RID: 345
	public class NetClientSocialModule : NetSocialModule
	{
		// Token: 0x0600197A RID: 6522 RVA: 0x004E0F94 File Offset: 0x004DF194
		private void OnIPCClientAccess()
		{
			WeGameHelper.WriteDebugString("IPC client access", new object[0]);
			this.SendFriendListToLocalServer();
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x004E0FB0 File Offset: 0x004DF1B0
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

		// Token: 0x0600197C RID: 6524 RVA: 0x004E1030 File Offset: 0x004DF230
		private void OnWegameMessage(IPCMessage message)
		{
			if (message.GetCmd() == IPCMessageType.IPCMessageTypeReportServerID)
			{
				ReportServerID reportServerID;
				message.Parse<ReportServerID>(out reportServerID);
				this.OnReportServerID(reportServerID);
			}
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x004E1056 File Offset: 0x004DF256
		private void OnReportServerID(ReportServerID reportServerID)
		{
			WeGameHelper.WriteDebugString("OnReportServerID - " + reportServerID._serverID, new object[0]);
			this.AsyncSetMyMetaData(this._serverIDMedataKey, reportServerID._serverID);
			this.AsyncSetInviteCommandLine(reportServerID._serverID);
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x004E1094 File Offset: 0x004DF294
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

		// Token: 0x0600197F RID: 6527 RVA: 0x004E10FD File Offset: 0x004DF2FD
		private void AsyncSetInviteCommandLine(string cmdline)
		{
			rail_api.RailFactory().RailFriends().AsyncSetInviteCommandLine(cmdline, "");
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x004E1118 File Offset: 0x004DF318
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

		// Token: 0x06001981 RID: 6529 RVA: 0x004E115C File Offset: 0x004DF35C
		private bool TryAuthUserByRecvData(RailID user, byte[] data, int length)
		{
			WeGameHelper.WriteDebugString("TryAuthUserByRecvData user:{0}", new object[]
			{
				user.id_
			});
			if (length < 3)
			{
				WeGameHelper.WriteDebugString("Failed to validate authentication packet: Too short. (Length: " + length + ")", new object[0]);
				return false;
			}
			int num = (int)data[1] << 8 | (int)data[0];
			if (num != length)
			{
				WeGameHelper.WriteDebugString(string.Concat(new object[]
				{
					"Failed to validate authentication packet: Packet size mismatch. (",
					num,
					"!=",
					length,
					")"
				}), new object[0]);
				return false;
			}
			if (data[2] != 93)
			{
				WeGameHelper.WriteDebugString("Failed to validate authentication packet: Packet type is not correct. (Type: " + data[2] + ")", new object[0]);
				return false;
			}
			return true;
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x004E122C File Offset: 0x004DF42C
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
					WeGameHelper.WriteDebugString(" Auth Server Ticket Failed", new object[0]);
					this.Close(user);
				}
				else
				{
					WeGameHelper.WriteDebugString("OnRailAuthSessionTicket Auth Success..", new object[0]);
					this.OnAuthSuccess(user);
				}
				return false;
			}
			return connectionState == NetSocialModule.ConnectionState.Connected;
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x004E129C File Offset: 0x004DF49C
		private void OnAuthSuccess(RailID remote_peer)
		{
			if (!this._connectionStateMap.ContainsKey(remote_peer))
			{
				return;
			}
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
			WeGameHelper.WriteDebugString("OnConnectToSocialServer server:" + remote_peer.id_.ToString(), new object[0]);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x004E1360 File Offset: 0x004DF560
		private bool GetRailConnectIDFromCmdLine(RailID server_id)
		{
			foreach (string text in Environment.GetCommandLineArgs())
			{
				string text2 = "--rail_connect_cmd=";
				int num = text.IndexOf(text2);
				if (num != -1)
				{
					ulong id_ = 0UL;
					if (ulong.TryParse(text.Substring(num + text2.Length), out id_))
					{
						server_id.id_ = id_;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x004E13C0 File Offset: 0x004DF5C0
		private void CheckParameters()
		{
			RailID server_id = new RailID();
			if (this.GetRailConnectIDFromCmdLine(server_id))
			{
				if (server_id.IsValid())
				{
					Main.OpenPlayerSelect(delegate(PlayerFileData playerData)
					{
						Main.ServerSideCharacter = false;
						playerData.SetAsActive();
						Main.menuMode = 882;
						Main.statusText = Language.GetTextValue("Social.Joining");
						WeGameHelper.WriteDebugString(" CheckParameters， lobby.join", new object[0]);
						this.JoinServer(server_id);
					});
					return;
				}
				WeGameHelper.WriteDebugString("Invalid RailID passed to +connect_lobby", new object[0]);
			}
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x004E1424 File Offset: 0x004DF624
		public override void LaunchLocalServer(Process process, ServerMode mode)
		{
			if (this._lobby.State != LobbyState.Inactive)
			{
				this._lobby.Leave();
			}
			this.LazyCreateWeGameMsgServer();
			ProcessStartInfo startInfo = process.StartInfo;
			startInfo.Arguments = startInfo.Arguments + " -wegame -localwegameid " + base.GetLocalPeer().id_;
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
			string str;
			rail_api.RailFactory().RailUtils().GetLaunchAppParameters(2, ref str);
			ProcessStartInfo startInfo5 = process.StartInfo;
			startInfo5.Arguments = startInfo5.Arguments + " " + str;
			WeGameHelper.WriteDebugString("LaunchLocalServer,cmd_line:" + process.StartInfo.Arguments, new object[0]);
			this.AsyncSetMyMetaData("status", Language.GetTextValue("Social.StatusInGame"));
			Netplay.OnDisconnect += this.OnDisconnect;
			process.Start();
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x004E158C File Offset: 0x004DF78C
		public override void Shutdown()
		{
			this.AsyncSetInviteCommandLine("");
			this.CleanMyMetaData();
			this.UnRegisterRailEvent();
			base.Shutdown();
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x004E15AB File Offset: 0x004DF7AB
		public override ulong GetLobbyId()
		{
			return 0UL;
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
		public override bool StartListening(SocketConnectionAccepted callback)
		{
			return false;
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void StopListening()
		{
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x004E15B0 File Offset: 0x004DF7B0
		public override void Close(RemoteAddress address)
		{
			this.CleanMyMetaData();
			RailID remote_peer = base.RemoteAddressToRailId(address);
			this.Close(remote_peer);
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x004E15D2 File Offset: 0x004DF7D2
		public override bool CanInvite()
		{
			return (this._hasLocalHost || this._lobby.State == LobbyState.Active || Main.LobbyId != 0UL) && Main.netMode != 0;
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x004E15FB File Offset: 0x004DF7FB
		public override void OpenInviteInterface()
		{
			this._lobby.OpenInviteOverlay();
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x004E1608 File Offset: 0x004DF808
		private void Close(RailID remote_peer)
		{
			if (!this._connectionStateMap.ContainsKey(remote_peer))
			{
				return;
			}
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

		// Token: 0x0600198F RID: 6543 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void Connect(RemoteAddress address)
		{
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x004E1689 File Offset: 0x004DF889
		public override void CancelJoin()
		{
			if (this._lobby.State != LobbyState.Inactive)
			{
				this._lobby.Leave();
			}
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x004E16A4 File Offset: 0x004DF8A4
		private void RegisterRailEvent()
		{
			foreach (RAILEventID raileventID in new RAILEventID[]
			{
				16001,
				16002,
				13503,
				13501,
				12003,
				12002,
				12010
			})
			{
				this._callbackHelper.RegisterCallback(raileventID, new RailEventCallBackHandler(this.OnRailEvent));
			}
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x004E171A File Offset: 0x004DF91A
		private void UnRegisterRailEvent()
		{
			this._callbackHelper.UnregisterAllCallback();
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x004E1728 File Offset: 0x004DF928
		public void OnRailEvent(RAILEventID id, EventBase data)
		{
			WeGameHelper.WriteDebugString("OnRailEvent,id=" + id.ToString() + " ,result=" + data.result.ToString(), new object[0]);
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

		// Token: 0x06001994 RID: 6548 RVA: 0x004E1814 File Offset: 0x004DFA14
		private string DumpMataDataString(List<RailKeyValueResult> list)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (RailKeyValueResult railKeyValueResult in list)
			{
				stringBuilder.Append("key: " + railKeyValueResult.key + " value: " + railKeyValueResult.value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x004E188C File Offset: 0x004DFA8C
		private string GetValueByKey(string key, List<RailKeyValueResult> list)
		{
			string result = null;
			foreach (RailKeyValueResult railKeyValueResult in list)
			{
				if (railKeyValueResult.key == key)
				{
					result = railKeyValueResult.value;
					break;
				}
			}
			return result;
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x004E18F0 File Offset: 0x004DFAF0
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
					IPCMessage ipcmessage = new IPCMessage();
					ipcmessage.Build<WeGameFriendListInfo>(IPCMessageType.IPCMessageTypeNotifyFriendList, t);
					result = this._msgServer.SendMessage(ipcmessage);
					WeGameHelper.WriteDebugString("NotifyFriendListToServer: " + result.ToString(), new object[0]);
				}
			}
			return result;
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x004E195C File Offset: 0x004DFB5C
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

		// Token: 0x06001998 RID: 6552 RVA: 0x004E1988 File Offset: 0x004DFB88
		private void OnGetFriendMetaData(RailFriendsGetMetadataResult data)
		{
			if (data.result == null && data.friend_kvs.Count > 0)
			{
				WeGameHelper.WriteDebugString("OnGetFriendMetaData - " + this.DumpMataDataString(data.friend_kvs), new object[0]);
				string valueByKey = this.GetValueByKey(this._serverIDMedataKey, data.friend_kvs);
				if (valueByKey != null)
				{
					if (valueByKey.Length > 0)
					{
						RailID railID = new RailID();
						railID.id_ = ulong.Parse(valueByKey);
						if (railID.IsValid())
						{
							this.JoinServer(railID);
							return;
						}
						WeGameHelper.WriteDebugString("JoinServer failed, invalid server id", new object[0]);
						return;
					}
					else
					{
						WeGameHelper.WriteDebugString("can not find server id key", new object[0]);
					}
				}
			}
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x004E1A34 File Offset: 0x004DFC34
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

		// Token: 0x0600199A RID: 6554 RVA: 0x004E1AAC File Offset: 0x004DFCAC
		private string GetFriendNickname(RailID rail_id)
		{
			if (this._player_info_list != null)
			{
				foreach (PlayerPersonalInfo playerPersonalInfo in this._player_info_list)
				{
					if (playerPersonalInfo.rail_id == rail_id)
					{
						return playerPersonalInfo.rail_name;
					}
				}
			}
			return "";
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x004E1B20 File Offset: 0x004DFD20
		private void OnRailGetUsersInfo(RailUsersInfoData data)
		{
			this._player_info_list = data.user_info_list;
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x004E1B2E File Offset: 0x004DFD2E
		private void OnFriendlistChange(RailFriendsListChanged data)
		{
			if (this._hasLocalHost)
			{
				this.SendFriendListToLocalServer();
			}
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x004E1B40 File Offset: 0x004DFD40
		private void AsyncGetFriendsInfo()
		{
			IRailFriends railFriends = rail_api.RailFactory().RailFriends();
			if (railFriends != null)
			{
				List<RailFriendInfo> list = new List<RailFriendInfo>();
				railFriends.GetFriendsList(list);
				List<RailID> list2 = new List<RailID>();
				foreach (RailFriendInfo railFriendInfo in list)
				{
					list2.Add(railFriendInfo.friend_rail_id);
				}
				railFriends.AsyncGetPersonalInfo(list2, "");
			}
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x004E1BC4 File Offset: 0x004DFDC4
		private void AsyncSetPlayWith(RailID rail_id)
		{
			List<RailUserPlayedWith> list = new List<RailUserPlayedWith>();
			list.Add(new RailUserPlayedWith
			{
				rail_id = rail_id
			});
			IRailFriends railFriends = rail_api.RailFactory().RailFriends();
			if (railFriends != null)
			{
				railFriends.AsyncReportPlayedWithUserList(list, "");
			}
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x004E1C06 File Offset: 0x004DFE06
		private void OnRailSetMetaData(RailFriendsSetMetadataResult data)
		{
			WeGameHelper.WriteDebugString("OnRailSetMetaData - " + data.result.ToString(), new object[0]);
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x004E1C30 File Offset: 0x004DFE30
		private void OnRailRespondInvation(RailUsersRespondInvitation data)
		{
			WeGameHelper.WriteDebugString(" request join game", new object[0]);
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
				WeGameHelper.WriteDebugString("inviter_id: " + data.inviter_id.id_, new object[0]);
			});
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x004E1C9C File Offset: 0x004DFE9C
		private void AsyncGetServerIDByOwener(RailID ownerID)
		{
			List<string> list = new List<string>();
			list.Add(this._serverIDMedataKey);
			IRailFriends railFriends = rail_api.RailFactory().RailFriends();
			if (railFriends != null)
			{
				railFriends.AsyncGetFriendMetadata(ownerID, list, "");
			}
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x004E1CD8 File Offset: 0x004DFED8
		private void OnRailCreateSessionRequest(CreateSessionRequest result)
		{
			WeGameHelper.WriteDebugString("OnRailCreateSessionRequest", new object[0]);
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

		// Token: 0x060019A3 RID: 6563 RVA: 0x004E1D70 File Offset: 0x004DFF70
		private void OnRailCreateSessionFailed(CreateSessionFailed result)
		{
			WeGameHelper.WriteDebugString("OnRailCreateSessionFailed, CloseRemote: local:{0}, remote:{1}", new object[]
			{
				result.local_peer.id_,
				result.remote_peer.id_
			});
			this.Close(result.remote_peer);
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x004E1DC0 File Offset: 0x004DFFC0
		private void CleanMyMetaData()
		{
			IRailFriends railFriends = rail_api.RailFactory().RailFriends();
			if (railFriends != null)
			{
				railFriends.AsyncClearAllMyMetadata("");
			}
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x004E1DE7 File Offset: 0x004DFFE7
		private void OnDisconnect()
		{
			this.CleanMyMetaData();
			this._hasLocalHost = false;
			Netplay.OnDisconnect -= this.OnDisconnect;
		}

		// Token: 0x0400154A RID: 5450
		private RailCallBackHelper _callbackHelper = new RailCallBackHelper();

		// Token: 0x0400154B RID: 5451
		private bool _hasLocalHost;

		// Token: 0x0400154C RID: 5452
		private IPCServer server = new IPCServer();

		// Token: 0x0400154D RID: 5453
		private readonly string _serverIDMedataKey = "terraria.serverid";

		// Token: 0x0400154E RID: 5454
		private RailID _inviter_id = new RailID();

		// Token: 0x0400154F RID: 5455
		private List<PlayerPersonalInfo> _player_info_list;

		// Token: 0x04001550 RID: 5456
		private MessageDispatcherServer _msgServer;
	}
}
