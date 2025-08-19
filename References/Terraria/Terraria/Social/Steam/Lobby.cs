using System;
using System.Collections.Generic;
using Steamworks;

namespace Terraria.Social.Steam
{
	// Token: 0x02000178 RID: 376
	public class Lobby
	{
		// Token: 0x06001A90 RID: 6800 RVA: 0x004E4F34 File Offset: 0x004E3134
		public Lobby()
		{
			this._lobbyEnter = CallResult<LobbyEnter_t>.Create(new CallResult<LobbyEnter_t>.APIDispatchDelegate(this.OnLobbyEntered));
			this._lobbyCreated = CallResult<LobbyCreated_t>.Create(new CallResult<LobbyCreated_t>.APIDispatchDelegate(this.OnLobbyCreated));
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x004E4FA8 File Offset: 0x004E31A8
		public void Create(bool inviteOnly, CallResult<LobbyCreated_t>.APIDispatchDelegate callResult)
		{
			SteamAPICall_t steamAPICall_t = SteamMatchmaking.CreateLobby(inviteOnly ? 0 : 1, 256);
			this._lobbyCreatedExternalCallback = callResult;
			this._lobbyCreated.Set(steamAPICall_t, null);
			this.State = LobbyState.Creating;
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x004E4FE2 File Offset: 0x004E31E2
		public void OpenInviteOverlay()
		{
			if (this.State == LobbyState.Inactive)
			{
				SteamFriends.ActivateGameOverlayInviteDialog(new CSteamID(Main.LobbyId));
				return;
			}
			SteamFriends.ActivateGameOverlayInviteDialog(this.Id);
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x004E5008 File Offset: 0x004E3208
		public void Join(CSteamID lobbyId, CallResult<LobbyEnter_t>.APIDispatchDelegate callResult)
		{
			if (this.State != LobbyState.Inactive)
			{
				return;
			}
			this.State = LobbyState.Connecting;
			this._lobbyEnterExternalCallback = callResult;
			SteamAPICall_t steamAPICall_t = SteamMatchmaking.JoinLobby(lobbyId);
			this._lobbyEnter.Set(steamAPICall_t, null);
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x004E5040 File Offset: 0x004E3240
		public byte[] GetMessage(int index)
		{
			CSteamID csteamID;
			EChatEntryType echatEntryType;
			int lobbyChatEntry = SteamMatchmaking.GetLobbyChatEntry(this.Id, index, ref csteamID, this._messageBuffer, this._messageBuffer.Length, ref echatEntryType);
			byte[] array = new byte[lobbyChatEntry];
			Array.Copy(this._messageBuffer, array, lobbyChatEntry);
			return array;
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x004E5081 File Offset: 0x004E3281
		public int GetUserCount()
		{
			return SteamMatchmaking.GetNumLobbyMembers(this.Id);
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x004E508E File Offset: 0x004E328E
		public CSteamID GetUserByIndex(int index)
		{
			return SteamMatchmaking.GetLobbyMemberByIndex(this.Id, index);
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x004E509C File Offset: 0x004E329C
		public bool SendMessage(byte[] data)
		{
			return this.SendMessage(data, data.Length);
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x004E50A8 File Offset: 0x004E32A8
		public bool SendMessage(byte[] data, int length)
		{
			return this.State == LobbyState.Active && SteamMatchmaking.SendLobbyChatMsg(this.Id, data, length);
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x004E50C2 File Offset: 0x004E32C2
		public void Set(CSteamID lobbyId)
		{
			this.Id = lobbyId;
			this.State = LobbyState.Active;
			this.Owner = SteamMatchmaking.GetLobbyOwner(lobbyId);
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x004E50DE File Offset: 0x004E32DE
		public void SetPlayedWith(CSteamID userId)
		{
			if (this._usersSeen.Contains(userId))
			{
				return;
			}
			SteamFriends.SetPlayedWith(userId);
			this._usersSeen.Add(userId);
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x004E5102 File Offset: 0x004E3302
		public void Leave()
		{
			if (this.State == LobbyState.Active)
			{
				SteamMatchmaking.LeaveLobby(this.Id);
			}
			this.State = LobbyState.Inactive;
			this._usersSeen.Clear();
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x004E512C File Offset: 0x004E332C
		private void OnLobbyEntered(LobbyEnter_t result, bool failure)
		{
			if (this.State != LobbyState.Connecting)
			{
				return;
			}
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

		// Token: 0x06001A9D RID: 6813 RVA: 0x004E5188 File Offset: 0x004E3388
		private void OnLobbyCreated(LobbyCreated_t result, bool failure)
		{
			if (this.State != LobbyState.Creating)
			{
				return;
			}
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

		// Token: 0x040015AC RID: 5548
		private HashSet<CSteamID> _usersSeen = new HashSet<CSteamID>();

		// Token: 0x040015AD RID: 5549
		private byte[] _messageBuffer = new byte[1024];

		// Token: 0x040015AE RID: 5550
		public CSteamID Id = CSteamID.Nil;

		// Token: 0x040015AF RID: 5551
		public CSteamID Owner = CSteamID.Nil;

		// Token: 0x040015B0 RID: 5552
		public LobbyState State;

		// Token: 0x040015B1 RID: 5553
		private CallResult<LobbyEnter_t> _lobbyEnter;

		// Token: 0x040015B2 RID: 5554
		private CallResult<LobbyEnter_t>.APIDispatchDelegate _lobbyEnterExternalCallback;

		// Token: 0x040015B3 RID: 5555
		private CallResult<LobbyCreated_t> _lobbyCreated;

		// Token: 0x040015B4 RID: 5556
		private CallResult<LobbyCreated_t>.APIDispatchDelegate _lobbyCreatedExternalCallback;
	}
}
