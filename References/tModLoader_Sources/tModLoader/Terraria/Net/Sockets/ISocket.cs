using System;

namespace Terraria.Net.Sockets
{
	// Token: 0x02000127 RID: 295
	public interface ISocket
	{
		// Token: 0x06001A46 RID: 6726
		void Close();

		// Token: 0x06001A47 RID: 6727
		bool IsConnected();

		// Token: 0x06001A48 RID: 6728
		void Connect(RemoteAddress address);

		// Token: 0x06001A49 RID: 6729
		void AsyncSend(byte[] data, int offset, int size, SocketSendCallback callback, object state = null);

		// Token: 0x06001A4A RID: 6730
		void AsyncReceive(byte[] data, int offset, int size, SocketReceiveCallback callback, object state = null);

		// Token: 0x06001A4B RID: 6731
		bool IsDataAvailable();

		// Token: 0x06001A4C RID: 6732
		void SendQueuedPackets();

		// Token: 0x06001A4D RID: 6733
		bool StartListening(SocketConnectionAccepted callback);

		// Token: 0x06001A4E RID: 6734
		void StopListening();

		// Token: 0x06001A4F RID: 6735
		RemoteAddress GetRemoteAddress();
	}
}
