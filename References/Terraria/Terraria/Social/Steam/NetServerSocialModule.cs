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
	// Token: 0x0200017A RID: 378
	public class NetServerSocialModule : NetSocialModule
	{
		// Token: 0x06001AB3 RID: 6835 RVA: 0x004E5827 File Offset: 0x004E3A27
		public NetServerSocialModule() : base(1, 2)
		{
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x004E5834 File Offset: 0x004E3A34
		private void BroadcastConnectedUsers()
		{
			List<ulong> list = new List<ulong>();
			foreach (KeyValuePair<CSteamID, NetSocialModule.ConnectionState> keyValuePair in this._connectionStateMap)
			{
				if (keyValuePair.Value == NetSocialModule.ConnectionState.Connected)
				{
					list.Add(keyValuePair.Key.m_SteamID);
				}
			}
			byte[] array = new byte[list.Count * 8 + 1];
			using (MemoryStream memoryStream = new MemoryStream(array))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(1);
					foreach (ulong value in list)
					{
						binaryWriter.Write(value);
					}
				}
			}
			this._lobby.SendMessage(array);
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x004E5944 File Offset: 0x004E3B44
		public override void Initialize()
		{
			base.Initialize();
			this._reader.SetReadEvent(new SteamP2PReader.OnReadEvent(this.OnPacketRead));
			this._p2pSessionRequest = Callback<P2PSessionRequest_t>.Create(new Callback<P2PSessionRequest_t>.DispatchDelegate(this.OnP2PSessionRequest));
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
						this._lobby.Create(false, new CallResult<LobbyCreated_t>.APIDispatchDelegate(this.OnLobbyCreated));
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

		// Token: 0x06001AB6 RID: 6838 RVA: 0x004E5A4D File Offset: 0x004E3C4D
		public override ulong GetLobbyId()
		{
			return this._lobby.Id.m_SteamID;
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void OpenInviteInterface()
		{
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void CancelJoin()
		{
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
		public override bool CanInvite()
		{
			return false;
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void LaunchLocalServer(Process process, ServerMode mode)
		{
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x004E5A5F File Offset: 0x004E3C5F
		public override bool StartListening(SocketConnectionAccepted callback)
		{
			this._acceptingClients = true;
			this._connectionAcceptedCallback = callback;
			return true;
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x004E5A70 File Offset: 0x004E3C70
		public override void StopListening()
		{
			this._acceptingClients = false;
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void Connect(RemoteAddress address)
		{
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x004E5A7C File Offset: 0x004E3C7C
		public override void Close(RemoteAddress address)
		{
			CSteamID user = base.RemoteAddressToSteamId(address);
			this.Close(user);
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x004E5A98 File Offset: 0x004E3C98
		private void Close(CSteamID user)
		{
			if (!this._connectionStateMap.ContainsKey(user))
			{
				return;
			}
			SteamUser.EndAuthSession(user);
			SteamNetworking.CloseP2PSessionWithUser(user);
			this._connectionStateMap[user] = NetSocialModule.ConnectionState.Inactive;
			this._reader.ClearUser(user);
			this._writer.ClearUser(user);
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x004E5AE6 File Offset: 0x004E3CE6
		private void OnLobbyCreated(LobbyCreated_t result, bool failure)
		{
			if (failure)
			{
				return;
			}
			SteamFriends.SetRichPresence("status", Language.GetTextValue("Social.StatusInGame"));
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x004E5B04 File Offset: 0x004E3D04
		private bool OnPacketRead(byte[] data, int length, CSteamID userId)
		{
			if (!this._connectionStateMap.ContainsKey(userId) || this._connectionStateMap[userId] == NetSocialModule.ConnectionState.Inactive)
			{
				P2PSessionRequest_t result;
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

		// Token: 0x06001AC2 RID: 6850 RVA: 0x004E5C14 File Offset: 0x004E3E14
		private void OnP2PSessionRequest(P2PSessionRequest_t result)
		{
			CSteamID steamIDRemote = result.m_steamIDRemote;
			if (this._connectionStateMap.ContainsKey(steamIDRemote) && this._connectionStateMap[steamIDRemote] != NetSocialModule.ConnectionState.Inactive)
			{
				SteamNetworking.AcceptP2PSessionWithUser(steamIDRemote);
				return;
			}
			if (!this._acceptingClients)
			{
				return;
			}
			if (!this._mode.HasFlag(ServerMode.FriendsOfFriends) && SteamFriends.GetFriendRelationship(steamIDRemote) != 3)
			{
				return;
			}
			SteamNetworking.AcceptP2PSessionWithUser(steamIDRemote);
			P2PSessionState_t p2PSessionState_t;
			while (SteamNetworking.GetP2PSessionState(steamIDRemote, ref p2PSessionState_t) && p2PSessionState_t.m_bConnecting == 1)
			{
			}
			if (p2PSessionState_t.m_bConnectionActive == 0)
			{
				this.Close(steamIDRemote);
			}
			this._connectionStateMap[steamIDRemote] = NetSocialModule.ConnectionState.Authenticating;
			this._connectionAcceptedCallback(new SocialSocket(new SteamAddress(steamIDRemote)));
		}

		// Token: 0x040015BC RID: 5564
		private ServerMode _mode;

		// Token: 0x040015BD RID: 5565
		private Callback<P2PSessionRequest_t> _p2pSessionRequest;

		// Token: 0x040015BE RID: 5566
		private bool _acceptingClients;

		// Token: 0x040015BF RID: 5567
		private SocketConnectionAccepted _connectionAcceptedCallback;
	}
}
