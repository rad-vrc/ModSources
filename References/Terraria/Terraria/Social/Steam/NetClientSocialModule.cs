using System;
using System.Diagnostics;
using Steamworks;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Net;
using Terraria.Net.Sockets;
using Terraria.Social.WeGame;

namespace Terraria.Social.Steam
{
	// Token: 0x02000179 RID: 377
	public class NetClientSocialModule : NetSocialModule
	{
		// Token: 0x06001A9E RID: 6814 RVA: 0x004E51E1 File Offset: 0x004E33E1
		public NetClientSocialModule() : base(2, 1)
		{
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x004E5208 File Offset: 0x004E3408
		public override void Initialize()
		{
			base.Initialize();
			this._gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(new Callback<GameLobbyJoinRequested_t>.DispatchDelegate(this.OnLobbyJoinRequest));
			this._p2pSessionRequest = Callback<P2PSessionRequest_t>.Create(new Callback<P2PSessionRequest_t>.DispatchDelegate(this.OnP2PSessionRequest));
			this._p2pSessionConnectfail = Callback<P2PSessionConnectFail_t>.Create(new Callback<P2PSessionConnectFail_t>.DispatchDelegate(this.OnSessionConnectFail));
			Main.OnEngineLoad += this.CheckParameters;
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x004E5274 File Offset: 0x004E3474
		private void CheckParameters()
		{
			ulong lobbyId;
			if (Program.LaunchParameters.ContainsKey("+connect_lobby") && ulong.TryParse(Program.LaunchParameters["+connect_lobby"], out lobbyId))
			{
				this.ConnectToLobby(lobbyId);
			}
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x004E52B4 File Offset: 0x004E34B4
		public void ConnectToLobby(ulong lobbyId)
		{
			CSteamID lobbySteamId = new CSteamID(lobbyId);
			if (lobbySteamId.IsValid())
			{
				Main.OpenPlayerSelect(delegate(PlayerFileData playerData)
				{
					Main.ServerSideCharacter = false;
					playerData.SetAsActive();
					Main.menuMode = 882;
					Main.statusText = Language.GetTextValue("Social.Joining");
					WeGameHelper.WriteDebugString(" CheckParameters， lobby.join", new object[0]);
					this._lobby.Join(lobbySteamId, new CallResult<LobbyEnter_t>.APIDispatchDelegate(this.OnLobbyEntered));
				});
			}
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x004E52F8 File Offset: 0x004E34F8
		public override void LaunchLocalServer(Process process, ServerMode mode)
		{
			WeGameHelper.WriteDebugString("LaunchLocalServer", new object[0]);
			if (this._lobby.State != LobbyState.Inactive)
			{
				this._lobby.Leave();
			}
			ProcessStartInfo startInfo = process.StartInfo;
			startInfo.Arguments = startInfo.Arguments + " -steam -localsteamid " + SteamUser.GetSteamID().m_SteamID;
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
			SteamFriends.SetRichPresence("status", Language.GetTextValue("Social.StatusInGame"));
			Netplay.OnDisconnect += this.OnDisconnect;
			process.Start();
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x004E15AB File Offset: 0x004DF7AB
		public override ulong GetLobbyId()
		{
			return 0UL;
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
		public override bool StartListening(SocketConnectionAccepted callback)
		{
			return false;
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void StopListening()
		{
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x004E541C File Offset: 0x004E361C
		public override void Close(RemoteAddress address)
		{
			SteamFriends.ClearRichPresence();
			CSteamID user = base.RemoteAddressToSteamId(address);
			this.Close(user);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x004E543D File Offset: 0x004E363D
		public override bool CanInvite()
		{
			return (this._hasLocalHost || this._lobby.State == LobbyState.Active || Main.LobbyId != 0UL) && Main.netMode != 0;
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x004E5466 File Offset: 0x004E3666
		public override void OpenInviteInterface()
		{
			this._lobby.OpenInviteOverlay();
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x004E5474 File Offset: 0x004E3674
		private void Close(CSteamID user)
		{
			if (!this._connectionStateMap.ContainsKey(user))
			{
				return;
			}
			SteamNetworking.CloseP2PSessionWithUser(user);
			this.ClearAuthTicket();
			this._connectionStateMap[user] = NetSocialModule.ConnectionState.Inactive;
			this._lobby.Leave();
			this._reader.ClearUser(user);
			this._writer.ClearUser(user);
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public override void Connect(RemoteAddress address)
		{
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x004E54CD File Offset: 0x004E36CD
		public override void CancelJoin()
		{
			if (this._lobby.State != LobbyState.Inactive)
			{
				this._lobby.Leave();
			}
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x004E54E8 File Offset: 0x004E36E8
		private void OnLobbyJoinRequest(GameLobbyJoinRequested_t result)
		{
			WeGameHelper.WriteDebugString(" OnLobbyJoinRequest", new object[0]);
			if (this._lobby.State != LobbyState.Inactive)
			{
				this._lobby.Leave();
			}
			string friendName = SteamFriends.GetFriendPersonaName(result.m_steamIDFriend);
			Main.OnPlayerSelected <>9__1;
			Main.QueueMainThreadAction(delegate
			{
				Main.OnPlayerSelected method;
				if ((method = <>9__1) == null)
				{
					method = (<>9__1 = delegate(PlayerFileData playerData)
					{
						Main.ServerSideCharacter = false;
						playerData.SetAsActive();
						Main.menuMode = 882;
						Main.statusText = Language.GetTextValue("Social.JoiningFriend", friendName);
						this._lobby.Join(result.m_steamIDLobby, new CallResult<LobbyEnter_t>.APIDispatchDelegate(this.OnLobbyEntered));
					});
				}
				Main.OpenPlayerSelect(method);
			});
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x004E5558 File Offset: 0x004E3758
		private void OnLobbyEntered(LobbyEnter_t result, bool failure)
		{
			WeGameHelper.WriteDebugString(" OnLobbyEntered", new object[0]);
			SteamNetworking.AllowP2PPacketRelay(true);
			this.SendAuthTicket(this._lobby.Owner);
			int num = 0;
			P2PSessionState_t p2PSessionState_t;
			while (SteamNetworking.GetP2PSessionState(this._lobby.Owner, ref p2PSessionState_t) && p2PSessionState_t.m_bConnectionActive != 1)
			{
				switch (p2PSessionState_t.m_eP2PSessionError)
				{
				case 1:
					this.ClearAuthTicket();
					return;
				case 2:
					this.ClearAuthTicket();
					return;
				case 3:
					this.ClearAuthTicket();
					return;
				case 4:
					if (++num > 5)
					{
						this.ClearAuthTicket();
						return;
					}
					SteamNetworking.CloseP2PSessionWithUser(this._lobby.Owner);
					this.SendAuthTicket(this._lobby.Owner);
					break;
				case 5:
					this.ClearAuthTicket();
					return;
				}
			}
			this._connectionStateMap[this._lobby.Owner] = NetSocialModule.ConnectionState.Connected;
			SteamFriends.SetPlayedWith(this._lobby.Owner);
			SteamFriends.SetRichPresence("status", Language.GetTextValue("Social.StatusInGame"));
			Main.clrInput();
			Netplay.ServerPassword = "";
			Main.GetInputText("", false);
			Main.autoPass = false;
			Main.netMode = 1;
			Netplay.OnConnectedToSocialServer(new SocialSocket(new SteamAddress(this._lobby.Owner)));
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x004E56A0 File Offset: 0x004E38A0
		private void SendAuthTicket(CSteamID address)
		{
			WeGameHelper.WriteDebugString(" SendAuthTicket", new object[0]);
			if (this._authTicket == HAuthTicket.Invalid)
			{
				this._authTicket = SteamUser.GetAuthSessionTicket(this._authData, this._authData.Length, ref this._authDataLength);
			}
			int num = (int)(this._authDataLength + 3U);
			byte[] array = new byte[num];
			array[0] = (byte)(num & 255);
			array[1] = (byte)(num >> 8 & 255);
			array[2] = 93;
			int num2 = 0;
			while ((long)num2 < (long)((ulong)this._authDataLength))
			{
				array[num2 + 3] = this._authData[num2];
				num2++;
			}
			SteamNetworking.SendP2PPacket(address, array, (uint)num, 2, 1);
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x004E5748 File Offset: 0x004E3948
		private void ClearAuthTicket()
		{
			if (this._authTicket != HAuthTicket.Invalid)
			{
				SteamUser.CancelAuthTicket(this._authTicket);
			}
			this._authTicket = HAuthTicket.Invalid;
			for (int i = 0; i < this._authData.Length; i++)
			{
				this._authData[i] = 0;
			}
			this._authDataLength = 0U;
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x004E57A0 File Offset: 0x004E39A0
		private void OnDisconnect()
		{
			SteamFriends.ClearRichPresence();
			this._hasLocalHost = false;
			Netplay.OnDisconnect -= this.OnDisconnect;
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x004E57BF File Offset: 0x004E39BF
		private void OnSessionConnectFail(P2PSessionConnectFail_t result)
		{
			WeGameHelper.WriteDebugString(" OnSessionConnectFail", new object[0]);
			this.Close(result.m_steamIDRemote);
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x004E57E0 File Offset: 0x004E39E0
		private void OnP2PSessionRequest(P2PSessionRequest_t result)
		{
			WeGameHelper.WriteDebugString(" OnP2PSessionRequest", new object[0]);
			CSteamID steamIDRemote = result.m_steamIDRemote;
			if (this._connectionStateMap.ContainsKey(steamIDRemote) && this._connectionStateMap[steamIDRemote] != NetSocialModule.ConnectionState.Inactive)
			{
				SteamNetworking.AcceptP2PSessionWithUser(steamIDRemote);
			}
		}

		// Token: 0x040015B5 RID: 5557
		private Callback<GameLobbyJoinRequested_t> _gameLobbyJoinRequested;

		// Token: 0x040015B6 RID: 5558
		private Callback<P2PSessionRequest_t> _p2pSessionRequest;

		// Token: 0x040015B7 RID: 5559
		private Callback<P2PSessionConnectFail_t> _p2pSessionConnectfail;

		// Token: 0x040015B8 RID: 5560
		private HAuthTicket _authTicket = HAuthTicket.Invalid;

		// Token: 0x040015B9 RID: 5561
		private byte[] _authData = new byte[1021];

		// Token: 0x040015BA RID: 5562
		private uint _authDataLength;

		// Token: 0x040015BB RID: 5563
		private bool _hasLocalHost;
	}
}
