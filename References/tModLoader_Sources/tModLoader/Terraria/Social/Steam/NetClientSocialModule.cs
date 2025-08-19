using System;
using System.Diagnostics;
using Steamworks;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Net;
using Terraria.Net.Sockets;
using Terraria.Social.WeGame;

namespace Terraria.Social.Steam
{
	// Token: 0x020000E9 RID: 233
	public class NetClientSocialModule : NetSocialModule
	{
		// Token: 0x060017F3 RID: 6131 RVA: 0x004B9C18 File Offset: 0x004B7E18
		public NetClientSocialModule() : base(2, 1)
		{
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x004B9C40 File Offset: 0x004B7E40
		public override void Initialize()
		{
			base.Initialize();
			this._gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(new Callback<GameLobbyJoinRequested_t>.DispatchDelegate(this.OnLobbyJoinRequest));
			this._p2pSessionRequest = Callback<P2PSessionRequest_t>.Create(new Callback<P2PSessionRequest_t>.DispatchDelegate(this.OnP2PSessionRequest));
			this._p2pSessionConnectfail = Callback<P2PSessionConnectFail_t>.Create(new Callback<P2PSessionConnectFail_t>.DispatchDelegate(this.OnSessionConnectFail));
			if (Program.LaunchParameters.ContainsKey("+connect_lobby"))
			{
				ModLoader.OnSuccessfulLoad = (Action)Delegate.Combine(ModLoader.OnSuccessfulLoad, new Action(this.CheckParameters));
			}
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x004B9CCC File Offset: 0x004B7ECC
		private void CheckParameters()
		{
			ulong result;
			if (ulong.TryParse(Program.LaunchParameters["+connect_lobby"], out result))
			{
				this.ConnectToLobby(result);
				return;
			}
			Logging.tML.Error("The provided lobby ID was invalid: " + result.ToString());
			Main.menuMode = 0;
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x004B9D1C File Offset: 0x004B7F1C
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
					WeGameHelper.WriteDebugString(" CheckParameters， lobby.join", Array.Empty<object>());
					this._lobby.Join(lobbySteamId, new CallResult<LobbyEnter_t>.APIDispatchDelegate(this.OnLobbyEntered));
				});
			}
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x004B9D60 File Offset: 0x004B7F60
		public override void LaunchLocalServer(Process process, ServerMode mode)
		{
			WeGameHelper.WriteDebugString("LaunchLocalServer", Array.Empty<object>());
			if (this._lobby.State != LobbyState.Inactive)
			{
				this._lobby.Leave();
			}
			ProcessStartInfo startInfo = process.StartInfo;
			startInfo.Arguments = startInfo.Arguments + " -steam -localsteamid " + SteamUser.GetSteamID().m_SteamID.ToString();
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

		// Token: 0x060017F8 RID: 6136 RVA: 0x004B9E84 File Offset: 0x004B8084
		public override ulong GetLobbyId()
		{
			return 0UL;
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x004B9E88 File Offset: 0x004B8088
		public override bool StartListening(SocketConnectionAccepted callback)
		{
			return false;
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x004B9E8B File Offset: 0x004B808B
		public override void StopListening()
		{
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x004B9E90 File Offset: 0x004B8090
		public override void Close(RemoteAddress address)
		{
			SteamFriends.ClearRichPresence();
			CSteamID user = base.RemoteAddressToSteamId(address);
			this.Close(user);
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x004B9EB1 File Offset: 0x004B80B1
		public override bool CanInvite()
		{
			return (this._hasLocalHost || this._lobby.State == LobbyState.Active || Main.LobbyId != 0UL) && Main.netMode != 0;
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x004B9EDA File Offset: 0x004B80DA
		public override void OpenInviteInterface()
		{
			this._lobby.OpenInviteOverlay();
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x004B9EE8 File Offset: 0x004B80E8
		private void Close(CSteamID user)
		{
			if (this._connectionStateMap.ContainsKey(user))
			{
				SteamNetworking.CloseP2PSessionWithUser(user);
				this.ClearAuthTicket();
				this._connectionStateMap[user] = NetSocialModule.ConnectionState.Inactive;
				this._lobby.Leave();
				this._reader.ClearUser(user);
				this._writer.ClearUser(user);
			}
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x004B9F40 File Offset: 0x004B8140
		public override void Connect(RemoteAddress address)
		{
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x004B9F42 File Offset: 0x004B8142
		public override void CancelJoin()
		{
			if (this._lobby.State != LobbyState.Inactive)
			{
				this._lobby.Leave();
			}
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x004B9F5C File Offset: 0x004B815C
		private void OnLobbyJoinRequest(GameLobbyJoinRequested_t result)
		{
			WeGameHelper.WriteDebugString(" OnLobbyJoinRequest", Array.Empty<object>());
			if (this._lobby.State != LobbyState.Inactive)
			{
				this._lobby.Leave();
			}
			string friendName = SteamFriends.GetFriendPersonaName(result.m_steamIDFriend);
			Main.OnPlayerSelected <>9__2;
			Main.QueueMainThreadAction(delegate
			{
				Action joinAction = delegate()
				{
					Main.OnPlayerSelected method;
					if ((method = <>9__2) == null)
					{
						method = (<>9__2 = delegate(PlayerFileData playerData)
						{
							Main.ServerSideCharacter = false;
							playerData.SetAsActive();
							Main.menuMode = 882;
							Main.statusText = Language.GetTextValue("Social.JoiningFriend", friendName);
							this._lobby.Join(result.m_steamIDLobby, new CallResult<LobbyEnter_t>.APIDispatchDelegate(this.OnLobbyEntered));
						});
					}
					Main.OpenPlayerSelect(method);
				};
				if (ModLoader.Mods.Length == 0 || Main.menuMode == 10002)
				{
					Logging.tML.Info("OnLobbyJoinRequest: Delay joinAction via OnSuccessfulLoad");
					ModLoader.OnSuccessfulLoad = (Action)Delegate.Combine(ModLoader.OnSuccessfulLoad, joinAction);
					return;
				}
				Logging.tML.Info("OnLobbyJoinRequest: Running joinAction directly");
				joinAction();
			});
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x004B9FCC File Offset: 0x004B81CC
		private void OnLobbyEntered(LobbyEnter_t result, bool failure)
		{
			WeGameHelper.WriteDebugString(" OnLobbyEntered", Array.Empty<object>());
			SteamNetworking.AllowP2PPacketRelay(true);
			this.SendAuthTicket(this._lobby.Owner);
			int num = 0;
			P2PSessionState_t pConnectionState;
			while (SteamNetworking.GetP2PSessionState(this._lobby.Owner, ref pConnectionState) && pConnectionState.m_bConnectionActive != 1)
			{
				switch (pConnectionState.m_eP2PSessionError)
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

		// Token: 0x06001803 RID: 6147 RVA: 0x004BA114 File Offset: 0x004B8314
		private void SendAuthTicket(CSteamID address)
		{
			WeGameHelper.WriteDebugString(" SendAuthTicket", Array.Empty<object>());
			if (this._authTicket == HAuthTicket.Invalid)
			{
				this._authTicket = SteamUser.GetAuthSessionTicket(this._authData, this._authData.Length, ref this._authDataLength);
			}
			int num = (int)(this._authDataLength + 3U);
			byte[] array = new byte[num];
			array[0] = (byte)(num & 255);
			array[1] = (byte)(num >> 8 & 255);
			array[2] = 93;
			int i = 0;
			while ((long)i < (long)((ulong)this._authDataLength))
			{
				array[i + 3] = this._authData[i];
				i++;
			}
			SteamNetworking.SendP2PPacket(address, array, (uint)num, 2, 1);
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x004BA1BC File Offset: 0x004B83BC
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

		// Token: 0x06001805 RID: 6149 RVA: 0x004BA214 File Offset: 0x004B8414
		private void OnDisconnect()
		{
			SteamFriends.ClearRichPresence();
			this._hasLocalHost = false;
			Netplay.OnDisconnect -= this.OnDisconnect;
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x004BA233 File Offset: 0x004B8433
		private void OnSessionConnectFail(P2PSessionConnectFail_t result)
		{
			WeGameHelper.WriteDebugString(" OnSessionConnectFail", Array.Empty<object>());
			this.Close(result.m_steamIDRemote);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x004BA250 File Offset: 0x004B8450
		private void OnP2PSessionRequest(P2PSessionRequest_t result)
		{
			WeGameHelper.WriteDebugString(" OnP2PSessionRequest", Array.Empty<object>());
			CSteamID steamIDRemote = result.m_steamIDRemote;
			if (this._connectionStateMap.ContainsKey(steamIDRemote) && this._connectionStateMap[steamIDRemote] != NetSocialModule.ConnectionState.Inactive)
			{
				SteamNetworking.AcceptP2PSessionWithUser(steamIDRemote);
			}
		}

		// Token: 0x0400133E RID: 4926
		private Callback<GameLobbyJoinRequested_t> _gameLobbyJoinRequested;

		// Token: 0x0400133F RID: 4927
		private Callback<P2PSessionRequest_t> _p2pSessionRequest;

		// Token: 0x04001340 RID: 4928
		private Callback<P2PSessionConnectFail_t> _p2pSessionConnectfail;

		// Token: 0x04001341 RID: 4929
		private HAuthTicket _authTicket = HAuthTicket.Invalid;

		// Token: 0x04001342 RID: 4930
		private byte[] _authData = new byte[1021];

		// Token: 0x04001343 RID: 4931
		private uint _authDataLength;

		// Token: 0x04001344 RID: 4932
		private bool _hasLocalHost;
	}
}
