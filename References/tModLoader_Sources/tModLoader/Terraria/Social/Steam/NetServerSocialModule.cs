using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Steamworks;
using Terraria.Localization;
using Terraria.Net;
using Terraria.Net.Sockets;

namespace Terraria.Social.Steam
{
	// Token: 0x020000EA RID: 234
	public class NetServerSocialModule : NetSocialModule
	{
		// Token: 0x06001808 RID: 6152 RVA: 0x004BA296 File Offset: 0x004B8496
		public NetServerSocialModule() : base(1, 2)
		{
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x004BA2A0 File Offset: 0x004B84A0
		private void BroadcastConnectedUsers()
		{
			List<ulong> list = new List<ulong>();
			foreach (KeyValuePair<CSteamID, NetSocialModule.ConnectionState> item in this._connectionStateMap)
			{
				if (item.Value == NetSocialModule.ConnectionState.Connected)
				{
					list.Add(item.Key.m_SteamID);
				}
			}
			byte[] array = new byte[list.Count * 8 + 1];
			using (MemoryStream output = new MemoryStream(array))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(output))
				{
					binaryWriter.Write(1);
					foreach (ulong item2 in list)
					{
						binaryWriter.Write(item2);
					}
				}
			}
			this._lobby.SendMessage(array);
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x004BA3B0 File Offset: 0x004B85B0
		public override void Initialize()
		{
			base.Initialize();
			this._reader.SetReadEvent(new SteamP2PReader.OnReadEvent(this.OnPacketRead));
			this._p2pSessionRequest = Callback<P2PSessionRequest_t>.Create(new Callback<P2PSessionRequest_t>.DispatchDelegate(this.OnP2PSessionRequest));
			if (Program.LaunchParameters.ContainsKey("-lobby"))
			{
				this._mode |= ServerMode.Lobby;
				string text = Program.LaunchParameters["-lobby"];
				if (!(text == "private"))
				{
					if (text == "friends")
					{
						this._mode |= ServerMode.FriendsCanJoin;
						this._lobby.Create(false, new CallResult<LobbyCreated_t>.APIDispatchDelegate(this.OnLobbyCreated));
					}
					else
					{
						Console.WriteLine(Language.GetTextValue("Error.InvalidLobbyFlag", "private", "friends"));
					}
				}
				else
				{
					this._lobby.Create(true, new CallResult<LobbyCreated_t>.APIDispatchDelegate(this.OnLobbyCreated));
				}
			}
			if (Program.LaunchParameters.ContainsKey("-friendsoffriends"))
			{
				this._mode |= ServerMode.FriendsOfFriends;
			}
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x004BA4B7 File Offset: 0x004B86B7
		public override ulong GetLobbyId()
		{
			return this._lobby.Id.m_SteamID;
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x004BA4C9 File Offset: 0x004B86C9
		public override void OpenInviteInterface()
		{
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x004BA4CB File Offset: 0x004B86CB
		public override void CancelJoin()
		{
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x004BA4CD File Offset: 0x004B86CD
		public override bool CanInvite()
		{
			return false;
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x004BA4D0 File Offset: 0x004B86D0
		public override void LaunchLocalServer(Process process, ServerMode mode)
		{
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x004BA4D2 File Offset: 0x004B86D2
		public override bool StartListening(SocketConnectionAccepted callback)
		{
			this._acceptingClients = true;
			this._connectionAcceptedCallback = callback;
			return true;
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x004BA4E3 File Offset: 0x004B86E3
		public override void StopListening()
		{
			this._acceptingClients = false;
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x004BA4EC File Offset: 0x004B86EC
		public override void Connect(RemoteAddress address)
		{
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x004BA4F0 File Offset: 0x004B86F0
		public override void Close(RemoteAddress address)
		{
			CSteamID user = base.RemoteAddressToSteamId(address);
			this.Close(user);
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x004BA50C File Offset: 0x004B870C
		private void Close(CSteamID user)
		{
			if (this._connectionStateMap.ContainsKey(user))
			{
				SteamUser.EndAuthSession(user);
				SteamNetworking.CloseP2PSessionWithUser(user);
				this._connectionStateMap[user] = NetSocialModule.ConnectionState.Inactive;
				this._reader.ClearUser(user);
				this._writer.ClearUser(user);
			}
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x004BA55C File Offset: 0x004B875C
		private void OnLobbyCreated(LobbyCreated_t result, bool failure)
		{
			if (!failure)
			{
				SteamFriends.SetRichPresence("status", Language.GetTextValue("Social.StatusInGame"));
				Utils.LogAndConsoleInfoMessage("Currently Hosting LobbyID: " + this.GetLobbyId().ToString());
			}
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x004BA5A0 File Offset: 0x004B87A0
		private bool OnPacketRead(byte[] data, int length, CSteamID userId)
		{
			if (!this._connectionStateMap.ContainsKey(userId) || this._connectionStateMap[userId] == NetSocialModule.ConnectionState.Inactive)
			{
				P2PSessionRequest_t result = default(P2PSessionRequest_t);
				result.m_steamIDRemote = userId;
				this.OnP2PSessionRequest(result);
				if (!this._connectionStateMap.ContainsKey(userId) || this._connectionStateMap[userId] == NetSocialModule.ConnectionState.Inactive)
				{
					return false;
				}
			}
			NetSocialModule.ConnectionState connectionState = this._connectionStateMap[userId];
			if (connectionState != NetSocialModule.ConnectionState.Authenticating)
			{
				return connectionState == NetSocialModule.ConnectionState.Connected;
			}
			if (length < 3)
			{
				return false;
			}
			if (((int)data[1] << 8 | (int)data[0]) != length)
			{
				return false;
			}
			if (data[2] != 93)
			{
				return false;
			}
			byte[] array = new byte[data.Length - 3];
			Array.Copy(data, 3, array, 0, array.Length);
			switch (SteamUser.BeginAuthSession(array, array.Length, userId))
			{
			case 0:
				this._connectionStateMap[userId] = NetSocialModule.ConnectionState.Connected;
				this.BroadcastConnectedUsers();
				break;
			case 1:
				this.Close(userId);
				break;
			case 2:
				this.Close(userId);
				break;
			case 3:
				this.Close(userId);
				break;
			case 4:
				this.Close(userId);
				break;
			case 5:
				this.Close(userId);
				break;
			}
			return false;
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x004BA6B8 File Offset: 0x004B88B8
		private void OnP2PSessionRequest(P2PSessionRequest_t result)
		{
			CSteamID steamIDRemote = result.m_steamIDRemote;
			if (this._connectionStateMap.ContainsKey(steamIDRemote) && this._connectionStateMap[steamIDRemote] != NetSocialModule.ConnectionState.Inactive)
			{
				SteamNetworking.AcceptP2PSessionWithUser(steamIDRemote);
				return;
			}
			if (this._acceptingClients && (this._mode.HasFlag(ServerMode.FriendsOfFriends) || SteamFriends.GetFriendRelationship(steamIDRemote) == 3))
			{
				SteamNetworking.AcceptP2PSessionWithUser(steamIDRemote);
				P2PSessionState_t pConnectionState;
				while (SteamNetworking.GetP2PSessionState(steamIDRemote, ref pConnectionState) && pConnectionState.m_bConnecting == 1)
				{
				}
				if (pConnectionState.m_bConnectionActive == 0)
				{
					this.Close(steamIDRemote);
				}
				this._connectionStateMap[steamIDRemote] = NetSocialModule.ConnectionState.Authenticating;
				this._connectionAcceptedCallback(new SocialSocket(new SteamAddress(steamIDRemote)));
				return;
			}
			Utils.LogAndConsoleInfoMessage("User " + SteamFriends.GetFriendPersonaName(steamIDRemote) + "connection rejected. Not a friend of lobby Owner.\nSet allow Friends of Friends to accept non-Friends of Host.");
		}

		// Token: 0x04001345 RID: 4933
		private ServerMode _mode;

		// Token: 0x04001346 RID: 4934
		private Callback<P2PSessionRequest_t> _p2pSessionRequest;

		// Token: 0x04001347 RID: 4935
		private bool _acceptingClients;

		// Token: 0x04001348 RID: 4936
		private SocketConnectionAccepted _connectionAcceptedCallback;
	}
}
