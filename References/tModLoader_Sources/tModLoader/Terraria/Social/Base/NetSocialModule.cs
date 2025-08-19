using System;
using System.Diagnostics;
using Terraria.Net;
using Terraria.Net.Sockets;

namespace Terraria.Social.Base
{
	// Token: 0x020000FE RID: 254
	public abstract class NetSocialModule : ISocialModule
	{
		// Token: 0x060018CF RID: 6351
		public abstract void Initialize();

		// Token: 0x060018D0 RID: 6352
		public abstract void Shutdown();

		// Token: 0x060018D1 RID: 6353
		public abstract void Close(RemoteAddress address);

		// Token: 0x060018D2 RID: 6354
		public abstract bool IsConnected(RemoteAddress address);

		// Token: 0x060018D3 RID: 6355
		public abstract void Connect(RemoteAddress address);

		// Token: 0x060018D4 RID: 6356
		public abstract bool Send(RemoteAddress address, byte[] data, int length);

		// Token: 0x060018D5 RID: 6357
		public abstract int Receive(RemoteAddress address, byte[] data, int offset, int length);

		// Token: 0x060018D6 RID: 6358
		public abstract bool IsDataAvailable(RemoteAddress address);

		// Token: 0x060018D7 RID: 6359
		public abstract void LaunchLocalServer(Process process, ServerMode mode);

		// Token: 0x060018D8 RID: 6360
		public abstract bool CanInvite();

		// Token: 0x060018D9 RID: 6361
		public abstract void OpenInviteInterface();

		// Token: 0x060018DA RID: 6362
		public abstract void CancelJoin();

		// Token: 0x060018DB RID: 6363
		public abstract bool StartListening(SocketConnectionAccepted callback);

		// Token: 0x060018DC RID: 6364
		public abstract void StopListening();

		// Token: 0x060018DD RID: 6365
		public abstract ulong GetLobbyId();
	}
}
