using System;
using rail;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000158 RID: 344
	public class Lobby
	{
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x004E0DB7 File Offset: 0x004DEFB7
		// (set) Token: 0x06001967 RID: 6503 RVA: 0x004E0DC9 File Offset: 0x004DEFC9
		private IRailGameServer RailServerHelper
		{
			get
			{
				if (this._gameServerInitSuccess)
				{
					return this._gameServer;
				}
				return null;
			}
			set
			{
				this._gameServer = value;
			}
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x004E0DE5 File Offset: 0x004DEFE5
		private IRailGameServerHelper GetRailServerHelper()
		{
			return rail_api.RailFactory().RailGameServerHelper();
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x004E0DF1 File Offset: 0x004DEFF1
		private void RegisterGameServerEvent()
		{
			if (this._callbackHelper != null)
			{
				this._callbackHelper.RegisterCallback(3002, new RailEventCallBackHandler(this.OnRailEvent));
			}
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x004E0E18 File Offset: 0x004DF018
		public void OnRailEvent(RAILEventID id, EventBase data)
		{
			WeGameHelper.WriteDebugString("OnRailEvent,id=" + id.ToString() + " ,result=" + data.result.ToString(), new object[0]);
			if (id == 3002)
			{
				this.OnGameServerCreated((CreateGameServerResult)data);
			}
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x004E0E71 File Offset: 0x004DF071
		private void OnGameServerCreated(CreateGameServerResult result)
		{
			if (result.result == null)
			{
				this._gameServerInitSuccess = true;
				this._lobbyCreatedExternalCallback(result.game_server_id);
				this._server_id = result.game_server_id;
			}
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x004E0EA0 File Offset: 0x004DF0A0
		public void Create(bool inviteOnly)
		{
			if (this.State == LobbyState.Inactive)
			{
				this.RegisterGameServerEvent();
			}
			IRailGameServer railServerHelper = rail_api.RailFactory().RailGameServerHelper().AsyncCreateGameServer(new CreateGameServerOptions
			{
				has_password = false,
				enable_team_voice = false
			}, "terraria", "terraria");
			this.RailServerHelper = railServerHelper;
			this.State = LobbyState.Creating;
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x004E0EF8 File Offset: 0x004DF0F8
		public void OpenInviteOverlay()
		{
			WeGameHelper.WriteDebugString("OpenInviteOverlay by wegame", new object[0]);
			rail_api.RailFactory().RailFloatingWindow().AsyncShowRailFloatingWindow(10, "");
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x004E0F21 File Offset: 0x004DF121
		public void Join(RailID local_peer, RailID remote_peer)
		{
			if (this.State != LobbyState.Inactive)
			{
				WeGameHelper.WriteDebugString("Lobby connection attempted while already in a lobby. This should never happen?", new object[0]);
				return;
			}
			this.State = LobbyState.Connecting;
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public byte[] GetMessage(int index)
		{
			return null;
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
		public int GetUserCount()
		{
			return 0;
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0006A9EF File Offset: 0x00068BEF
		public RailID GetUserByIndex(int index)
		{
			return null;
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x004E0F43 File Offset: 0x004DF143
		public bool SendMessage(byte[] data)
		{
			return this.SendMessage(data, data.Length);
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
		public bool SendMessage(byte[] data, int length)
		{
			return false;
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void Set(RailID lobbyId)
		{
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		public void SetPlayedWith(RailID userId)
		{
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x004E0F4F File Offset: 0x004DF14F
		public void Leave()
		{
			this.State = LobbyState.Inactive;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x004E0F58 File Offset: 0x004DF158
		public IRailGameServer GetServer()
		{
			return this.RailServerHelper;
		}

		// Token: 0x04001544 RID: 5444
		private RailCallBackHelper _callbackHelper = new RailCallBackHelper();

		// Token: 0x04001545 RID: 5445
		public LobbyState State;

		// Token: 0x04001546 RID: 5446
		private bool _gameServerInitSuccess;

		// Token: 0x04001547 RID: 5447
		private IRailGameServer _gameServer;

		// Token: 0x04001548 RID: 5448
		public Action<RailID> _lobbyCreatedExternalCallback;

		// Token: 0x04001549 RID: 5449
		private RailID _server_id;
	}
}
