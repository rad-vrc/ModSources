using System;
using System.Collections.Generic;
using Steamworks;

namespace Terraria.Social.Steam
{
	// Token: 0x020000E7 RID: 231
	public class Lobby
	{
		// Token: 0x060017E5 RID: 6117 RVA: 0x004B9970 File Offset: 0x004B7B70
		public Lobby()
		{
			this._lobbyEnter = CallResult<LobbyEnter_t>.Create(new CallResult<LobbyEnter_t>.APIDispatchDelegate(this.OnLobbyEntered));
			this._lobbyCreated = CallResult<LobbyCreated_t>.Create(new CallResult<LobbyCreated_t>.APIDispatchDelegate(this.OnLobbyCreated));
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x004B99E4 File Offset: 0x004B7BE4
		public void Create(bool inviteOnly, CallResult<LobbyCreated_t>.APIDispatchDelegate callResult)
		{
			SteamAPICall_t hAPICall = SteamMatchmaking.CreateLobby((!inviteOnly) ? 1 : 0, 256);
			this._lobbyCreatedExternalCallback = callResult;
			this._lobbyCreated.Set(hAPICall, null);
			this.State = LobbyState.Creating;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x004B9A1E File Offset: 0x004B7C1E
		public void OpenInviteOverlay()
		{
			if (this.State == LobbyState.Inactive)
			{
				SteamFriends.ActivateGameOverlayInviteDialog(new CSteamID(Main.LobbyId));
				return;
			}
			SteamFriends.ActivateGameOverlayInviteDialog(this.Id);
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x004B9A44 File Offset: 0x004B7C44
		public void Join(CSteamID lobbyId, CallResult<LobbyEnter_t>.APIDispatchDelegate callResult)
		{
			if (this.State == LobbyState.Inactive)
			{
				this.State = LobbyState.Connecting;
				this._lobbyEnterExternalCallback = callResult;
				SteamAPICall_t hAPICall = SteamMatchmaking.JoinLobby(lobbyId);
				this._lobbyEnter.Set(hAPICall, null);
			}
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x004B9A7C File Offset: 0x004B7C7C
		public byte[] GetMessage(int index)
		{
			CSteamID pSteamIDUser;
			EChatEntryType peChatEntryType;
			int lobbyChatEntry = SteamMatchmaking.GetLobbyChatEntry(this.Id, index, ref pSteamIDUser, this._messageBuffer, this._messageBuffer.Length, ref peChatEntryType);
			byte[] array = new byte[lobbyChatEntry];
			Array.Copy(this._messageBuffer, array, lobbyChatEntry);
			return array;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x004B9ABD File Offset: 0x004B7CBD
		public int GetUserCount()
		{
			return SteamMatchmaking.GetNumLobbyMembers(this.Id);
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x004B9ACA File Offset: 0x004B7CCA
		public CSteamID GetUserByIndex(int index)
		{
			return SteamMatchmaking.GetLobbyMemberByIndex(this.Id, index);
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x004B9AD8 File Offset: 0x004B7CD8
		public bool SendMessage(byte[] data)
		{
			return this.SendMessage(data, data.Length);
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x004B9AE4 File Offset: 0x004B7CE4
		public bool SendMessage(byte[] data, int length)
		{
			return this.State == LobbyState.Active && SteamMatchmaking.SendLobbyChatMsg(this.Id, data, length);
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x004B9AFE File Offset: 0x004B7CFE
		public void Set(CSteamID lobbyId)
		{
			this.Id = lobbyId;
			this.State = LobbyState.Active;
			this.Owner = SteamMatchmaking.GetLobbyOwner(lobbyId);
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x004B9B1A File Offset: 0x004B7D1A
		public void SetPlayedWith(CSteamID userId)
		{
			if (!this._usersSeen.Contains(userId))
			{
				SteamFriends.SetPlayedWith(userId);
				this._usersSeen.Add(userId);
			}
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x004B9B3D File Offset: 0x004B7D3D
		public void Leave()
		{
			if (this.State == LobbyState.Active)
			{
				SteamMatchmaking.LeaveLobby(this.Id);
			}
			this.State = LobbyState.Inactive;
			this._usersSeen.Clear();
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x004B9B68 File Offset: 0x004B7D68
		private void OnLobbyEntered(LobbyEnter_t result, bool failure)
		{
			if (this.State == LobbyState.Connecting)
			{
				if (failure)
				{
					this.State = LobbyState.Inactive;
				}
				else
				{
					this.State = LobbyState.Active;
				}
				this.Id = new CSteamID(result.m_ulSteamIDLobby);
				this.Owner = SteamMatchmaking.GetLobbyOwner(this.Id);
				this._lobbyEnterExternalCallback.Invoke(result, failure);
			}
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x004B9BC0 File Offset: 0x004B7DC0
		private void OnLobbyCreated(LobbyCreated_t result, bool failure)
		{
			if (this.State == LobbyState.Creating)
			{
				if (failure)
				{
					this.State = LobbyState.Inactive;
				}
				else
				{
					this.State = LobbyState.Active;
				}
				this.Id = new CSteamID(result.m_ulSteamIDLobby);
				this.Owner = SteamMatchmaking.GetLobbyOwner(this.Id);
				this._lobbyCreatedExternalCallback.Invoke(result, failure);
			}
		}

		// Token: 0x04001330 RID: 4912
		private HashSet<CSteamID> _usersSeen = new HashSet<CSteamID>();

		// Token: 0x04001331 RID: 4913
		private byte[] _messageBuffer = new byte[1024];

		// Token: 0x04001332 RID: 4914
		public CSteamID Id = CSteamID.Nil;

		// Token: 0x04001333 RID: 4915
		public CSteamID Owner = CSteamID.Nil;

		// Token: 0x04001334 RID: 4916
		public LobbyState State;

		// Token: 0x04001335 RID: 4917
		private CallResult<LobbyEnter_t> _lobbyEnter;

		// Token: 0x04001336 RID: 4918
		private CallResult<LobbyEnter_t>.APIDispatchDelegate _lobbyEnterExternalCallback;

		// Token: 0x04001337 RID: 4919
		private CallResult<LobbyCreated_t> _lobbyCreated;

		// Token: 0x04001338 RID: 4920
		private CallResult<LobbyCreated_t>.APIDispatchDelegate _lobbyCreatedExternalCallback;
	}
}
