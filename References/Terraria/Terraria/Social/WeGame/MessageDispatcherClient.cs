using System;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000167 RID: 359
	public class MessageDispatcherClient
	{
		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06001A25 RID: 6693 RVA: 0x004E36AC File Offset: 0x004E18AC
		// (remove) Token: 0x06001A26 RID: 6694 RVA: 0x004E36E4 File Offset: 0x004E18E4
		public event Action<IPCMessage> OnMessage;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06001A27 RID: 6695 RVA: 0x004E371C File Offset: 0x004E191C
		// (remove) Token: 0x06001A28 RID: 6696 RVA: 0x004E3754 File Offset: 0x004E1954
		public event Action OnConnected;

		// Token: 0x06001A29 RID: 6697 RVA: 0x004E378C File Offset: 0x004E198C
		public void Init(string clientName, string serverName)
		{
			this._clientName = clientName;
			this._severName = serverName;
			this._ipcClient.Init(clientName);
			this._ipcClient.OnDataArrive += this.OnDataArrive;
			this._ipcClient.OnConnected += this.OnServerConnected;
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x004E37E1 File Offset: 0x004E19E1
		public void Start()
		{
			this._ipcClient.ConnectTo(this._severName);
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x004E37F4 File Offset: 0x004E19F4
		private void OnDataArrive(byte[] data)
		{
			IPCMessage ipcmessage = new IPCMessage();
			ipcmessage.BuildFrom(data);
			if (this.OnMessage != null)
			{
				this.OnMessage(ipcmessage);
			}
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x004E3822 File Offset: 0x004E1A22
		private void OnServerConnected()
		{
			if (this.OnConnected != null)
			{
				this.OnConnected();
			}
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x004E3837 File Offset: 0x004E1A37
		public void Tick()
		{
			this._ipcClient.Tick();
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x004E3844 File Offset: 0x004E1A44
		public bool SendMessage(IPCMessage msg)
		{
			return this._ipcClient.Send(msg.GetBytes());
		}

		// Token: 0x04001579 RID: 5497
		private IPCClient _ipcClient = new IPCClient();

		// Token: 0x0400157A RID: 5498
		private string _severName;

		// Token: 0x0400157B RID: 5499
		private string _clientName;
	}
}
