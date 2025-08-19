using System;
using System.Threading;
using Terraria.Social;

namespace Terraria.Net.Sockets
{
	// Token: 0x020000C7 RID: 199
	public class SocialSocket : ISocket
	{
		// Token: 0x0600144A RID: 5194 RVA: 0x0000B904 File Offset: 0x00009B04
		public SocialSocket()
		{
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x004A2A09 File Offset: 0x004A0C09
		public SocialSocket(RemoteAddress remoteAddress)
		{
			this._remoteAddress = remoteAddress;
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x004A2A18 File Offset: 0x004A0C18
		void ISocket.Close()
		{
			if (this._remoteAddress == null)
			{
				return;
			}
			SocialAPI.Network.Close(this._remoteAddress);
			this._remoteAddress = null;
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x004A2A3A File Offset: 0x004A0C3A
		bool ISocket.IsConnected()
		{
			return SocialAPI.Network.IsConnected(this._remoteAddress);
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x004A2A4C File Offset: 0x004A0C4C
		void ISocket.Connect(RemoteAddress address)
		{
			this._remoteAddress = address;
			SocialAPI.Network.Connect(address);
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x004A2A60 File Offset: 0x004A0C60
		void ISocket.AsyncSend(byte[] data, int offset, int size, SocketSendCallback callback, object state)
		{
			SocialAPI.Network.Send(this._remoteAddress, data, size);
			callback.BeginInvoke(state, null, null);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x004A2A84 File Offset: 0x004A0C84
		private void ReadCallback(byte[] data, int offset, int size, SocketReceiveCallback callback, object state)
		{
			int size2;
			while ((size2 = SocialAPI.Network.Receive(this._remoteAddress, data, offset, size)) == 0)
			{
				Thread.Sleep(1);
			}
			callback(state, size2);
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x004A2ABA File Offset: 0x004A0CBA
		void ISocket.AsyncReceive(byte[] data, int offset, int size, SocketReceiveCallback callback, object state)
		{
			new SocialSocket.InternalReadCallback(this.ReadCallback).BeginInvoke(data, offset, size, callback, state, null, null);
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		void ISocket.SendQueuedPackets()
		{
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x004A2AD7 File Offset: 0x004A0CD7
		bool ISocket.IsDataAvailable()
		{
			return SocialAPI.Network.IsDataAvailable(this._remoteAddress);
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x004A2AE9 File Offset: 0x004A0CE9
		RemoteAddress ISocket.GetRemoteAddress()
		{
			return this._remoteAddress;
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x004A2AF1 File Offset: 0x004A0CF1
		bool ISocket.StartListening(SocketConnectionAccepted callback)
		{
			return SocialAPI.Network.StartListening(callback);
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x004A2AFE File Offset: 0x004A0CFE
		void ISocket.StopListening()
		{
			SocialAPI.Network.StopListening();
		}

		// Token: 0x04001200 RID: 4608
		private RemoteAddress _remoteAddress;

		// Token: 0x0200055F RID: 1375
		// (Invoke) Token: 0x06003126 RID: 12582
		private delegate void InternalReadCallback(byte[] data, int offset, int size, SocketReceiveCallback callback, object state);
	}
}
