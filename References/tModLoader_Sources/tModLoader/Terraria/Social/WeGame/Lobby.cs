using System;
using rail;

namespace Terraria.Social.WeGame
{
	// Token: 0x020000D5 RID: 213
	public class Lobby
	{
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06001718 RID: 5912 RVA: 0x004B6AA9 File Offset: 0x004B4CA9
		// (set) Token: 0x06001719 RID: 5913 RVA: 0x004B6ABB File Offset: 0x004B4CBB
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

		// Token: 0x0600171A RID: 5914 RVA: 0x004B6AC4 File Offset: 0x004B4CC4
		private IRailGameServerHelper GetRailServerHelper()
		{
			return rail_api.RailFactory().RailGameServerHelper();
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x004B6AD0 File Offset: 0x004B4CD0
		private void RegisterGameServerEvent()
		{
			if (this._callbackHelper != null)
			{
				this._callbackHelper.RegisterCallback(3002, new RailEventCallBackHandler(this.OnRailEvent));
			}
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x004B6AF8 File Offset: 0x004B4CF8
		public void OnRailEvent(RAILEventID id, EventBase data)
		{
			WeGameHelper.WriteDebugString("OnRailEvent,id=" + id.ToString() + " ,result=" + data.result.ToString(), Array.Empty<object>());
			if (id == 3002)
			{
				this.OnGameServerCreated((CreateGameServerResult)data);
			}
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x004B6B50 File Offset: 0x004B4D50
		private void OnGameServerCreated(CreateGameServerResult result)
		{
			if (result.result == null)
			{
				this._gameServerInitSuccess = true;
				this._lobbyCreatedExternalCallback(result.game_server_id);
				this._server_id = result.game_server_id;
			}
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x004B6B80 File Offset: 0x004B4D80
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

		// Token: 0x0600171F RID: 5919 RVA: 0x004B6BD6 File Offset: 0x004B4DD6
		public void OpenInviteOverlay()
		{
			WeGameHelper.WriteDebugString("OpenInviteOverlay by wegame", Array.Empty<object>());
			rail_api.RailFactory().RailFloatingWindow().AsyncShowRailFloatingWindow(10, "");
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x004B6BFE File Offset: 0x004B4DFE
		public void Join(RailID local_peer, RailID remote_peer)
		{
			if (this.State != LobbyState.Inactive)
			{
				WeGameHelper.WriteDebugString("Lobby connection attempted while already in a lobby. This should never happen?", Array.Empty<object>());
				return;
			}
			this.State = LobbyState.Connecting;
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x004B6C1F File Offset: 0x004B4E1F
		public byte[] GetMessage(int index)
		{
			return null;
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x004B6C22 File Offset: 0x004B4E22
		public int GetUserCount()
		{
			return 0;
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x004B6C25 File Offset: 0x004B4E25
		public RailID GetUserByIndex(int index)
		{
			return null;
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x004B6C28 File Offset: 0x004B4E28
		public bool SendMessage(byte[] data)
		{
			return this.SendMessage(data, data.Length);
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x004B6C34 File Offset: 0x004B4E34
		public bool SendMessage(byte[] data, int length)
		{
			return false;
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x004B6C37 File Offset: 0x004B4E37
		public void Set(RailID lobbyId)
		{
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x004B6C39 File Offset: 0x004B4E39
		public void SetPlayedWith(RailID userId)
		{
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x004B6C3B File Offset: 0x004B4E3B
		public void Leave()
		{
			this.State = LobbyState.Inactive;
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x004B6C44 File Offset: 0x004B4E44
		public IRailGameServer GetServer()
		{
			return this.RailServerHelper;
		}

		// Token: 0x040012E8 RID: 4840
		private RailCallBackHelper _callbackHelper = new RailCallBackHelper();

		// Token: 0x040012E9 RID: 4841
		public LobbyState State;

		// Token: 0x040012EA RID: 4842
		private bool _gameServerInitSuccess;

		// Token: 0x040012EB RID: 4843
		private IRailGameServer _gameServer;

		// Token: 0x040012EC RID: 4844
		public Action<RailID> _lobbyCreatedExternalCallback;

		// Token: 0x040012ED RID: 4845
		private RailID _server_id;
	}
}
