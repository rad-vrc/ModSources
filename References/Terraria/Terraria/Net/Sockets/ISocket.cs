using System;

namespace Terraria.Net.Sockets
{
	// Token: 0x020000C6 RID: 198
	public interface ISocket
	{
		// Token: 0x06001440 RID: 5184
		void Close();

		// Token: 0x06001441 RID: 5185
		bool IsConnected();

		// Token: 0x06001442 RID: 5186
		void Connect(RemoteAddress address);

		// Token: 0x06001443 RID: 5187
		void AsyncSend(byte[] data, int offset, int size, SocketSendCallback callback, object state = null);

		// Token: 0x06001444 RID: 5188
		void AsyncReceive(byte[] data, int offset, int size, SocketReceiveCallback callback, object state = null);

		// Token: 0x06001445 RID: 5189
		bool IsDataAvailable();

		// Token: 0x06001446 RID: 5190
		void SendQueuedPackets();

		// Token: 0x06001447 RID: 5191
		bool StartListening(SocketConnectionAccepted callback);

		// Token: 0x06001448 RID: 5192
		void StopListening();

		// Token: 0x06001449 RID: 5193
		RemoteAddress GetRemoteAddress();
	}
}
