using System;

namespace Terraria.Social.WeGame
{
	// Token: 0x020000D9 RID: 217
	public class MessageDispatcherServer
	{
		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06001737 RID: 5943 RVA: 0x004B6E28 File Offset: 0x004B5028
		// (remove) Token: 0x06001738 RID: 5944 RVA: 0x004B6E60 File Offset: 0x004B5060
		public event Action OnIPCClientAccess;

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06001739 RID: 5945 RVA: 0x004B6E98 File Offset: 0x004B5098
		// (remove) Token: 0x0600173A RID: 5946 RVA: 0x004B6ED0 File Offset: 0x004B50D0
		public event Action<IPCMessage> OnMessage;

		// Token: 0x0600173B RID: 5947 RVA: 0x004B6F05 File Offset: 0x004B5105
		public void Init(string serverName)
		{
			this._ipcSever.Init(serverName);
			this._ipcSever.OnDataArrive += this.OnDataArrive;
			this._ipcSever.OnClientAccess += this.OnClientAccess;
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x004B6F41 File Offset: 0x004B5141
		public void OnClientAccess()
		{
			if (this.OnIPCClientAccess != null)
			{
				this.OnIPCClientAccess();
			}
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x004B6F56 File Offset: 0x004B5156
		public void Start()
		{
			this._ipcSever.StartListen();
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x004B6F64 File Offset: 0x004B5164
		private void OnDataArrive(byte[] data)
		{
			IPCMessage iPCMessage = new IPCMessage();
			iPCMessage.BuildFrom(data);
			if (this.OnMessage != null)
			{
				this.OnMessage(iPCMessage);
			}
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x004B6F92 File Offset: 0x004B5192
		public void Tick()
		{
			this._ipcSever.Tick();
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x004B6F9F File Offset: 0x004B519F
		public bool SendMessage(IPCMessage msg)
		{
			return this._ipcSever.Send(msg.GetBytes());
		}

		// Token: 0x040012F8 RID: 4856
		private IPCServer _ipcSever = new IPCServer();
	}
}
