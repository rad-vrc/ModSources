using System;

namespace Terraria.Social.WeGame
{
	// Token: 0x020000D8 RID: 216
	public class MessageDispatcherClient
	{
		// Token: 0x14000036 RID: 54
		// (add) Token: 0x0600172C RID: 5932 RVA: 0x004B6C68 File Offset: 0x004B4E68
		// (remove) Token: 0x0600172D RID: 5933 RVA: 0x004B6CA0 File Offset: 0x004B4EA0
		public event Action<IPCMessage> OnMessage;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x0600172E RID: 5934 RVA: 0x004B6CD8 File Offset: 0x004B4ED8
		// (remove) Token: 0x0600172F RID: 5935 RVA: 0x004B6D10 File Offset: 0x004B4F10
		public event Action OnConnected;

		// Token: 0x06001730 RID: 5936 RVA: 0x004B6D48 File Offset: 0x004B4F48
		public void Init(string clientName, string serverName)
		{
			this._clientName = clientName;
			this._severName = serverName;
			this._ipcClient.Init(clientName);
			this._ipcClient.OnDataArrive += this.OnDataArrive;
			this._ipcClient.OnConnected += this.OnServerConnected;
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x004B6D9D File Offset: 0x004B4F9D
		public void Start()
		{
			this._ipcClient.ConnectTo(this._severName);
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x004B6DB0 File Offset: 0x004B4FB0
		private void OnDataArrive(byte[] data)
		{
			IPCMessage iPCMessage = new IPCMessage();
			iPCMessage.BuildFrom(data);
			if (this.OnMessage != null)
			{
				this.OnMessage(iPCMessage);
			}
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x004B6DDE File Offset: 0x004B4FDE
		private void OnServerConnected()
		{
			if (this.OnConnected != null)
			{
				this.OnConnected();
			}
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x004B6DF3 File Offset: 0x004B4FF3
		public void Tick()
		{
			this._ipcClient.Tick();
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x004B6E00 File Offset: 0x004B5000
		public bool SendMessage(IPCMessage msg)
		{
			return this._ipcClient.Send(msg.GetBytes());
		}

		// Token: 0x040012F3 RID: 4851
		private IPCClient _ipcClient = new IPCClient();

		// Token: 0x040012F4 RID: 4852
		private string _severName;

		// Token: 0x040012F5 RID: 4853
		private string _clientName;
	}
}
