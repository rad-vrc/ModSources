using System;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000166 RID: 358
	public class MessageDispatcherServer
	{
		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06001A1A RID: 6682 RVA: 0x004E350C File Offset: 0x004E170C
		// (remove) Token: 0x06001A1B RID: 6683 RVA: 0x004E3544 File Offset: 0x004E1744
		public event Action OnIPCClientAccess;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06001A1C RID: 6684 RVA: 0x004E357C File Offset: 0x004E177C
		// (remove) Token: 0x06001A1D RID: 6685 RVA: 0x004E35B4 File Offset: 0x004E17B4
		public event Action<IPCMessage> OnMessage;

		// Token: 0x06001A1E RID: 6686 RVA: 0x004E35E9 File Offset: 0x004E17E9
		public void Init(string serverName)
		{
			this._ipcSever.Init(serverName);
			this._ipcSever.OnDataArrive += this.OnDataArrive;
			this._ipcSever.OnClientAccess += this.OnClientAccess;
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x004E3625 File Offset: 0x004E1825
		public void OnClientAccess()
		{
			if (this.OnIPCClientAccess != null)
			{
				this.OnIPCClientAccess();
			}
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x004E363A File Offset: 0x004E183A
		public void Start()
		{
			this._ipcSever.StartListen();
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x004E3648 File Offset: 0x004E1848
		private void OnDataArrive(byte[] data)
		{
			IPCMessage ipcmessage = new IPCMessage();
			ipcmessage.BuildFrom(data);
			if (this.OnMessage != null)
			{
				this.OnMessage(ipcmessage);
			}
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x004E3676 File Offset: 0x004E1876
		public void Tick()
		{
			this._ipcSever.Tick();
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x004E3683 File Offset: 0x004E1883
		public bool SendMessage(IPCMessage msg)
		{
			return this._ipcSever.Send(msg.GetBytes());
		}

		// Token: 0x04001576 RID: 5494
		private IPCServer _ipcSever = new IPCServer();
	}
}
