using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using rail;
using Terraria.Localization;
using Terraria.Net;
using Terraria.Net.Sockets;

namespace Terraria.Social.WeGame
{
	// Token: 0x020000DB RID: 219
	public class NetServerSocialModule : NetSocialModule
	{
		// Token: 0x0600176F RID: 5999 RVA: 0x004B7E37 File Offset: 0x004B6037
		public NetServerSocialModule()
		{
			this._lobby._lobbyCreatedExternalCallback = new Action<RailID>(this.OnLobbyCreated);
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x004B7E77 File Offset: 0x004B6077
		private void BroadcastConnectedUsers()
		{
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x004B7E7C File Offset: 0x004B607C
		private bool AcceptAnUserSession(RailID local_peer, RailID remote_peer)
		{
			bool result = false;
			WeGameHelper.WriteDebugString("AcceptAnUserSession server:" + local_peer.id_.ToString() + " remote:" + remote_peer.id_.ToString(), Array.Empty<object>());
			IRailNetwork railNetwork = rail_api.RailFactory().RailNetworkHelper();
			if (railNetwork != null)
			{
				result = (railNetwork.AcceptSessionRequest(local_peer, remote_peer) == 0);
			}
			return result;
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x004B7ED5 File Offset: 0x004B60D5
		private void TerminateRemotePlayerSession(RailID remote_id)
		{
			IRailPlayer railPlayer = rail_api.RailFactory().RailPlayer();
			if (railPlayer == null)
			{
				return;
			}
			railPlayer.TerminateSessionOfPlayer(remote_id);
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x004B7EEC File Offset: 0x004B60EC
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

		// Token: 0x06001774 RID: 6004 RVA: 0x004B7F1C File Offset: 0x004B611C
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

		// Token: 0x06001775 RID: 6005 RVA: 0x004B7F4C File Offset: 0x004B614C
		private void CloseAndUpdateUserState(RailID remote_peer)
		{
			if (this._connectionStateMap.ContainsKey(remote_peer))
			{
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
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x004B7FB9 File Offset: 0x004B61B9
		public void OnConnected()
		{
			this._serverConnected = true;
			if (this._ipcConnetedAction != null)
			{
				this._ipcConnetedAction();
			}
			this._ipcConnetedAction = null;
			WeGameHelper.WriteDebugString("IPC connected", Array.Empty<object>());
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x004B7FEC File Offset: 0x004B61EC
		private void OnCreateSessionRequest(CreateSessionRequest data)
		{
			if (!this._acceptingClients)
			{
				WeGameHelper.WriteDebugString(" - Ignoring connection from " + data.remote_peer.id_.ToString() + " while _acceptionClients is false.", Array.Empty<object>());
				return;
			}
			if (!this._mode.HasFlag(ServerMode.FriendsOfFriends) && !this.IsWeGameFriend(data.remote_peer))
			{
				WeGameHelper.WriteDebugString("Ignoring connection from " + data.remote_peer.id_.ToString() + ". Friends of friends is disabled.", Array.Empty<object>());
				return;
			}
			WeGameHelper.WriteDebugString("pass wegame friend check", Array.Empty<object>());
			this.AcceptAnUserSession(data.local_peer, data.remote_peer);
			this._connectionStateMap[data.remote_peer] = NetSocialModule.ConnectionState.Authenticating;
			if (this._connectionAcceptedCallback != null)
			{
				this._connectionAcceptedCallback(new SocialSocket(new WeGameAddress(data.remote_peer, "")));
			}
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x004B80D8 File Offset: 0x004B62D8
		private void OnCreateSessionFailed(CreateSessionFailed data)
		{
			WeGameHelper.WriteDebugString("CreateSessionFailed, local:{0}, remote:{1}", new object[]
			{
				data.local_peer.id_,
				data.remote_peer.id_
			});
			this.CloseAndUpdateUserState(data.remote_peer);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x004B8128 File Offset: 0x004B6328
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

		// Token: 0x0600177A RID: 6010 RVA: 0x004B8154 File Offset: 0x004B6354
		private void OnWegameMessage(IPCMessage message)
		{
			if (message.GetCmd() == IPCMessageType.IPCMessageTypeNotifyFriendList)
			{
				WeGameFriendListInfo value;
				message.Parse<WeGameFriendListInfo>(out value);
				this.UpdateFriendList(value);
			}
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x004B8179 File Offset: 0x004B6379
		private void UpdateFriendList(WeGameFriendListInfo friendListInfo)
		{
			this._wegameFriendList = friendListInfo._friendList;
			WeGameHelper.WriteDebugString("On update friend list - " + this.DumpFriendListString(friendListInfo._friendList), Array.Empty<object>());
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x004B81A8 File Offset: 0x004B63A8
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
							return true;
						}
					}
				}
				return result;
			}
			return result;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x004B8214 File Offset: 0x004B6414
		private string DumpFriendListString(List<RailFriendInfo> list)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (RailFriendInfo item in list)
			{
				StringBuilder stringBuilder2 = stringBuilder;
				StringBuilder stringBuilder3 = stringBuilder2;
				StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(40, 4, stringBuilder2);
				appendInterpolatedStringHandler.AppendLiteral("friend_id: ");
				appendInterpolatedStringHandler.AppendFormatted<ulong>(item.friend_rail_id.id_);
				appendInterpolatedStringHandler.AppendLiteral(", type: ");
				appendInterpolatedStringHandler.AppendFormatted<EnumRailFriendType>(item.friend_type);
				appendInterpolatedStringHandler.AppendLiteral(", online: ");
				appendInterpolatedStringHandler.AppendFormatted(item.online_state.friend_online_state.ToString());
				appendInterpolatedStringHandler.AppendLiteral(", playing: ");
				appendInterpolatedStringHandler.AppendFormatted<uint>(item.online_state.game_define_game_playing_state);
				stringBuilder3.AppendLine(ref appendInterpolatedStringHandler);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x004B8304 File Offset: 0x004B6504
		private bool IsActiveUser(RailID user)
		{
			return this._connectionStateMap.ContainsKey(user) && this._connectionStateMap[user] > NetSocialModule.ConnectionState.Inactive;
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x004B8328 File Offset: 0x004B6528
		private void UpdateUserStateBySessionAuthResult(GameServerStartSessionWithPlayerResponse data)
		{
			RailID remote_rail_id = data.remote_rail_id;
			RailResult result = data.result;
			if (this._connectionStateMap.ContainsKey(remote_rail_id))
			{
				if (result == null)
				{
					WeGameHelper.WriteDebugString("UpdateUserStateBySessionAuthResult Auth Success", Array.Empty<object>());
					this.BroadcastConnectedUsers();
					return;
				}
				WeGameHelper.WriteDebugString("UpdateUserStateBySessionAuthResult Auth Failed", Array.Empty<object>());
				this.CloseAndUpdateUserState(remote_rail_id);
			}
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x004B8380 File Offset: 0x004B6580
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

		// Token: 0x06001781 RID: 6017 RVA: 0x004B8454 File Offset: 0x004B6654
		private bool OnPacketRead(byte[] data, int size, RailID user)
		{
			if (!this.IsActiveUser(user))
			{
				WeGameHelper.WriteDebugString("OnPacketRead IsActiveUser false", Array.Empty<object>());
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

		// Token: 0x06001782 RID: 6018 RVA: 0x004B84B0 File Offset: 0x004B66B0
		private void OnAuthSuccess(RailID remote_peer)
		{
			if (this._connectionStateMap.ContainsKey(remote_peer))
			{
				this._connectionStateMap[remote_peer] = NetSocialModule.ConnectionState.Connected;
				int num = 3;
				byte[] array = new byte[num];
				array[0] = (byte)(num & 255);
				array[1] = (byte)(num >> 8 & 255);
				array[2] = 93;
				rail_api.RailFactory().RailNetworkHelper().SendReliableData(this._serverID, remote_peer, array, (uint)num);
			}
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x004B8518 File Offset: 0x004B6718
		public void OnRailEvent(RAILEventID event_id, EventBase data)
		{
			WeGameHelper.WriteDebugString("OnRailEvent,id=" + event_id.ToString() + " ,result=" + data.result.ToString(), Array.Empty<object>());
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

		// Token: 0x06001784 RID: 6020 RVA: 0x004B859C File Offset: 0x004B679C
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
				IPCMessage iPCMessage = new IPCMessage();
				iPCMessage.Build<ReportServerID>(IPCMessageType.IPCMessageTypeReportServerID, t);
				WeGameHelper.WriteDebugString("Send serverID to game client - " + this._client.SendMessage(iPCMessage).ToString(), Array.Empty<object>());
			};
			if (this._serverConnected)
			{
				action();
				return;
			}
			this._ipcConnetedAction = (Action)Delegate.Combine(this._ipcConnetedAction, action);
			WeGameHelper.WriteDebugString("report server id fail, no connection", Array.Empty<object>());
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x004B8650 File Offset: 0x004B6850
		private void RegisterRailEvent()
		{
			RAILEventID[] array = new RAILEventID[4];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.BF3F5D039D4F51BF263F1547CEE28AB6FC87E02F04DBD3AFA17C24E5E46AEEAD).FieldHandle);
			foreach (RAILEventID event_id in array)
			{
				this._callbackHelper.RegisterCallback(event_id, new RailEventCallBackHandler(this.OnRailEvent));
			}
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x004B869C File Offset: 0x004B689C
		public override void Initialize()
		{
			base.Initialize();
			this._mode |= ServerMode.Lobby;
			this.RegisterRailEvent();
			this._reader.SetReadEvent(new WeGameP2PReader.OnReadEvent(this.OnPacketRead));
			if (Program.LaunchParameters.ContainsKey("-lobby"))
			{
				this._mode |= ServerMode.Lobby;
				string text = Program.LaunchParameters["-lobby"];
				if (!(text == "private"))
				{
					if (text == "friends")
					{
						this._mode |= ServerMode.FriendsCanJoin;
						this._lobby.Create(false);
					}
					else
					{
						Console.WriteLine(Language.GetTextValue("Error.InvalidLobbyFlag", "private", "friends"));
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

		// Token: 0x06001787 RID: 6023 RVA: 0x004B87E9 File Offset: 0x004B69E9
		public override ulong GetLobbyId()
		{
			return this._serverID.id_;
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x004B87F6 File Offset: 0x004B69F6
		public override void OpenInviteInterface()
		{
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x004B87F8 File Offset: 0x004B69F8
		public override void CancelJoin()
		{
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x004B87FA File Offset: 0x004B69FA
		public override bool CanInvite()
		{
			return false;
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x004B87FD File Offset: 0x004B69FD
		public override void LaunchLocalServer(Process process, ServerMode mode)
		{
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x004B87FF File Offset: 0x004B69FF
		public override bool StartListening(SocketConnectionAccepted callback)
		{
			this._acceptingClients = true;
			this._connectionAcceptedCallback = callback;
			return false;
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x004B8810 File Offset: 0x004B6A10
		public override void StopListening()
		{
			this._acceptingClients = false;
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x004B8819 File Offset: 0x004B6A19
		public override void Connect(RemoteAddress address)
		{
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x004B881C File Offset: 0x004B6A1C
		public override void Close(RemoteAddress address)
		{
			RailID remote_peer = base.RemoteAddressToRailId(address);
			this.CloseAndUpdateUserState(remote_peer);
		}

		// Token: 0x04001302 RID: 4866
		private SocketConnectionAccepted _connectionAcceptedCallback;

		// Token: 0x04001303 RID: 4867
		private bool _acceptingClients;

		// Token: 0x04001304 RID: 4868
		private ServerMode _mode;

		// Token: 0x04001305 RID: 4869
		private RailCallBackHelper _callbackHelper = new RailCallBackHelper();

		// Token: 0x04001306 RID: 4870
		private MessageDispatcherClient _client = new MessageDispatcherClient();

		// Token: 0x04001307 RID: 4871
		private bool _serverConnected;

		// Token: 0x04001308 RID: 4872
		private RailID _serverID = new RailID();

		// Token: 0x04001309 RID: 4873
		private Action _ipcConnetedAction;

		// Token: 0x0400130A RID: 4874
		private List<RailFriendInfo> _wegameFriendList;
	}
}
