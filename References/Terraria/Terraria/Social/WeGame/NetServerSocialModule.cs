using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using rail;
using Terraria.Localization;
using Terraria.Net;
using Terraria.Net.Sockets;

namespace Terraria.Social.WeGame
{
	// Token: 0x0200015A RID: 346
	public class NetServerSocialModule : NetSocialModule
	{
		// Token: 0x060019A6 RID: 6566 RVA: 0x004E1E07 File Offset: 0x004E0007
		public NetServerSocialModule()
		{
			this._lobby._lobbyCreatedExternalCallback = new Action<RailID>(this.OnLobbyCreated);
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		private void BroadcastConnectedUsers()
		{
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x004E1E48 File Offset: 0x004E0048
		private bool AcceptAnUserSession(RailID local_peer, RailID remote_peer)
		{
			bool result = false;
			WeGameHelper.WriteDebugString("AcceptAnUserSession server:" + local_peer.id_.ToString() + " remote:" + remote_peer.id_.ToString(), new object[0]);
			IRailNetwork railNetwork = rail_api.RailFactory().RailNetworkHelper();
			if (railNetwork != null)
			{
				result = (railNetwork.AcceptSessionRequest(local_peer, remote_peer) == 0);
			}
			return result;
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x004E1EA4 File Offset: 0x004E00A4
		private void TerminateRemotePlayerSession(RailID remote_id)
		{
			IRailPlayer railPlayer = rail_api.RailFactory().RailPlayer();
			if (railPlayer != null)
			{
				railPlayer.TerminateSessionOfPlayer(remote_id);
			}
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x004E1EC8 File Offset: 0x004E00C8
		private bool CloseNetWorkSession(RailID remote_peer)
		{
			bool result = false;
			IRailNetwork railNetwork = rail_api.RailFactory().RailNetworkHelper();
			if (railNetwork != null)
			{
				result = (railNetwork.CloseSession(this._serverID, remote_peer) == 0);
			}
			return result;
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x004E1EF8 File Offset: 0x004E00F8
		private RailID GetServerID()
		{
			RailID railID = null;
			IRailGameServer server = this._lobby.GetServer();
			if (server != null)
			{
				railID = server.GetGameServerRailID();
			}
			return railID ?? new RailID();
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x004E1F28 File Offset: 0x004E0128
		private void CloseAndUpdateUserState(RailID remote_peer)
		{
			if (!this._connectionStateMap.ContainsKey(remote_peer))
			{
				return;
			}
			WeGameHelper.WriteDebugString("CloseAndUpdateUserState, remote:{0}", new object[]
			{
				remote_peer.id_
			});
			this.TerminateRemotePlayerSession(remote_peer);
			this.CloseNetWorkSession(remote_peer);
			this._connectionStateMap[remote_peer] = NetSocialModule.ConnectionState.Inactive;
			this._reader.ClearUser(remote_peer);
			this._writer.ClearUser(remote_peer);
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x004E1F96 File Offset: 0x004E0196
		public void OnConnected()
		{
			this._serverConnected = true;
			if (this._ipcConnetedAction != null)
			{
				this._ipcConnetedAction();
			}
			this._ipcConnetedAction = null;
			WeGameHelper.WriteDebugString("IPC connected", new object[0]);
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x004E1FCC File Offset: 0x004E01CC
		private void OnCreateSessionRequest(CreateSessionRequest data)
		{
			if (!this._acceptingClients)
			{
				WeGameHelper.WriteDebugString(" - Ignoring connection from " + data.remote_peer.id_ + " while _acceptionClients is false.", new object[0]);
				return;
			}
			if (!this._mode.HasFlag(ServerMode.FriendsOfFriends) && !this.IsWeGameFriend(data.remote_peer))
			{
				WeGameHelper.WriteDebugString("Ignoring connection from " + data.remote_peer.id_ + ". Friends of friends is disabled.", new object[0]);
				return;
			}
			WeGameHelper.WriteDebugString("pass wegame friend check", new object[0]);
			this.AcceptAnUserSession(data.local_peer, data.remote_peer);
			this._connectionStateMap[data.remote_peer] = NetSocialModule.ConnectionState.Authenticating;
			if (this._connectionAcceptedCallback != null)
			{
				this._connectionAcceptedCallback(new SocialSocket(new WeGameAddress(data.remote_peer, "")));
			}
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x004E20BC File Offset: 0x004E02BC
		private void OnCreateSessionFailed(CreateSessionFailed data)
		{
			WeGameHelper.WriteDebugString("CreateSessionFailed, local:{0}, remote:{1}", new object[]
			{
				data.local_peer.id_,
				data.remote_peer.id_
			});
			this.CloseAndUpdateUserState(data.remote_peer);
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x004E210C File Offset: 0x004E030C
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

		// Token: 0x060019B1 RID: 6577 RVA: 0x004E2138 File Offset: 0x004E0338
		private void OnWegameMessage(IPCMessage message)
		{
			IPCMessageType cmd = message.GetCmd();
			if (cmd == IPCMessageType.IPCMessageTypeNotifyFriendList)
			{
				WeGameFriendListInfo friendListInfo;
				message.Parse<WeGameFriendListInfo>(out friendListInfo);
				this.UpdateFriendList(friendListInfo);
			}
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x004E215F File Offset: 0x004E035F
		private void UpdateFriendList(WeGameFriendListInfo friendListInfo)
		{
			this._wegameFriendList = friendListInfo._friendList;
			WeGameHelper.WriteDebugString("On update friend list - " + this.DumpFriendListString(friendListInfo._friendList), new object[0]);
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x004E2190 File Offset: 0x004E0390
		private bool IsWeGameFriend(RailID id)
		{
			bool result = false;
			if (this._wegameFriendList != null)
			{
				using (List<RailFriendInfo>.Enumerator enumerator = this._wegameFriendList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.friend_rail_id == id)
						{
							result = true;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x004E21F8 File Offset: 0x004E03F8
		private string DumpFriendListString(List<RailFriendInfo> list)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (RailFriendInfo railFriendInfo in list)
			{
				stringBuilder.AppendLine(string.Format("friend_id: {0}, type: {1}, online: {2}, playing: {3}", new object[]
				{
					railFriendInfo.friend_rail_id.id_,
					railFriendInfo.friend_type,
					railFriendInfo.online_state.friend_online_state.ToString(),
					railFriendInfo.online_state.game_define_game_playing_state
				}));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x004E22B0 File Offset: 0x004E04B0
		private bool IsActiveUser(RailID user)
		{
			return this._connectionStateMap.ContainsKey(user) && this._connectionStateMap[user] > NetSocialModule.ConnectionState.Inactive;
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x004E22D4 File Offset: 0x004E04D4
		private void UpdateUserStateBySessionAuthResult(GameServerStartSessionWithPlayerResponse data)
		{
			RailID remote_rail_id = data.remote_rail_id;
			RailResult result = data.result;
			if (this._connectionStateMap.ContainsKey(remote_rail_id))
			{
				if (result == null)
				{
					WeGameHelper.WriteDebugString("UpdateUserStateBySessionAuthResult Auth Success", new object[0]);
					this.BroadcastConnectedUsers();
					return;
				}
				WeGameHelper.WriteDebugString("UpdateUserStateBySessionAuthResult Auth Failed", new object[0]);
				this.CloseAndUpdateUserState(remote_rail_id);
			}
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x004E2330 File Offset: 0x004E0530
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

		// Token: 0x060019B8 RID: 6584 RVA: 0x004E2400 File Offset: 0x004E0600
		private bool OnPacketRead(byte[] data, int size, RailID user)
		{
			if (!this.IsActiveUser(user))
			{
				WeGameHelper.WriteDebugString("OnPacketRead IsActiveUser false", new object[0]);
				return false;
			}
			NetSocialModule.ConnectionState connectionState = this._connectionStateMap[user];
			if (connectionState == NetSocialModule.ConnectionState.Authenticating)
			{
				if (!this.TryAuthUserByRecvData(user, data, size))
				{
					this.CloseAndUpdateUserState(user);
				}
				else
				{
					this.OnAuthSuccess(user);
				}
				return false;
			}
			return connectionState == NetSocialModule.ConnectionState.Connected;
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x004E245C File Offset: 0x004E065C
		private void OnAuthSuccess(RailID remote_peer)
		{
			if (!this._connectionStateMap.ContainsKey(remote_peer))
			{
				return;
			}
			this._connectionStateMap[remote_peer] = NetSocialModule.ConnectionState.Connected;
			int num = 3;
			byte[] array = new byte[num];
			array[0] = (byte)(num & 255);
			array[1] = (byte)(num >> 8 & 255);
			array[2] = 93;
			rail_api.RailFactory().RailNetworkHelper().SendReliableData(this._serverID, remote_peer, array, (uint)num);
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x004E24C4 File Offset: 0x004E06C4
		public void OnRailEvent(RAILEventID event_id, EventBase data)
		{
			WeGameHelper.WriteDebugString("OnRailEvent,id=" + event_id.ToString() + " ,result=" + data.result.ToString(), new object[0]);
			if (event_id == 3006)
			{
				this.UpdateUserStateBySessionAuthResult((GameServerStartSessionWithPlayerResponse)data);
				return;
			}
			if (event_id == 16001)
			{
				this.OnCreateSessionRequest((CreateSessionRequest)data);
				return;
			}
			if (event_id != 16002)
			{
				return;
			}
			this.OnCreateSessionFailed((CreateSessionFailed)data);
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x004E2548 File Offset: 0x004E0748
		private void OnLobbyCreated(RailID lobbyID)
		{
			WeGameHelper.WriteDebugString("SetLocalPeer: {0}", new object[]
			{
				lobbyID.id_
			});
			this._reader.SetLocalPeer(lobbyID);
			this._writer.SetLocalPeer(lobbyID);
			this._serverID = lobbyID;
			Action action = delegate()
			{
				ReportServerID t = new ReportServerID
				{
					_serverID = lobbyID.id_.ToString()
				};
				IPCMessage ipcmessage = new IPCMessage();
				ipcmessage.Build<ReportServerID>(IPCMessageType.IPCMessageTypeReportServerID, t);
				WeGameHelper.WriteDebugString("Send serverID to game client - " + this._client.SendMessage(ipcmessage).ToString(), new object[0]);
			};
			if (this._serverConnected)
			{
				action();
				return;
			}
			this._ipcConnetedAction = (Action)Delegate.Combine(this._ipcConnetedAction, action);
			WeGameHelper.WriteDebugString("report server id fail, no connection", new object[0]);
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x004E2600 File Offset: 0x004E0800
		private void RegisterRailEvent()
		{
			foreach (RAILEventID raileventID in new RAILEventID[]
			{
				16001,
				16002,
				3006,
				3005
			})
			{
				this._callbackHelper.RegisterCallback(raileventID, new RailEventCallBackHandler(this.OnRailEvent));
			}
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x004E2660 File Offset: 0x004E0860
		public override void Initialize()
		{
			base.Initialize();
			this._mode |= ServerMode.Lobby;
			this.RegisterRailEvent();
			this._reader.SetReadEvent(new WeGameP2PReader.OnReadEvent(this.OnPacketRead));
			if (Program.LaunchParameters.ContainsKey("-lobby"))
			{
				this._mode |= ServerMode.Lobby;
				string a = Program.LaunchParameters["-lobby"];
				if (!(a == "private"))
				{
					if (!(a == "friends"))
					{
						Console.WriteLine(Language.GetTextValue("Error.InvalidLobbyFlag", "private", "friends"));
					}
					else
					{
						this._mode |= ServerMode.FriendsCanJoin;
						this._lobby.Create(false);
					}
				}
				else
				{
					this._lobby.Create(true);
				}
			}
			if (Program.LaunchParameters.ContainsKey("-friendsoffriends"))
			{
				this._mode |= ServerMode.FriendsOfFriends;
			}
			this._client.Init("WeGame.Terraria.Message.Client", "WeGame.Terraria.Message.Server");
			this._client.OnConnected += this.OnConnected;
			this._client.OnMessage += this.OnWegameMessage;
			CoreSocialModule.OnTick += this._client.Tick;
			this._client.Start();
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x004E27AF File Offset: 0x004E09AF
		public override ulong GetLobbyId()
		{
			return this._serverID.id_;
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void OpenInviteInterface()
		{
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void CancelJoin()
		{
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
		public override bool CanInvite()
		{
			return false;
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void LaunchLocalServer(Process process, ServerMode mode)
		{
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x004E27BC File Offset: 0x004E09BC
		public override bool StartListening(SocketConnectionAccepted callback)
		{
			this._acceptingClients = true;
			this._connectionAcceptedCallback = callback;
			return false;
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x004E27CD File Offset: 0x004E09CD
		public override void StopListening()
		{
			this._acceptingClients = false;
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void Connect(RemoteAddress address)
		{
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x004E27D8 File Offset: 0x004E09D8
		public override void Close(RemoteAddress address)
		{
			RailID remote_peer = base.RemoteAddressToRailId(address);
			this.CloseAndUpdateUserState(remote_peer);
		}

		// Token: 0x04001551 RID: 5457
		private SocketConnectionAccepted _connectionAcceptedCallback;

		// Token: 0x04001552 RID: 5458
		private bool _acceptingClients;

		// Token: 0x04001553 RID: 5459
		private ServerMode _mode;

		// Token: 0x04001554 RID: 5460
		private RailCallBackHelper _callbackHelper = new RailCallBackHelper();

		// Token: 0x04001555 RID: 5461
		private MessageDispatcherClient _client = new MessageDispatcherClient();

		// Token: 0x04001556 RID: 5462
		private bool _serverConnected;

		// Token: 0x04001557 RID: 5463
		private RailID _serverID = new RailID();

		// Token: 0x04001558 RID: 5464
		private Action _ipcConnetedAction;

		// Token: 0x04001559 RID: 5465
		private List<RailFriendInfo> _wegameFriendList;
	}
}
