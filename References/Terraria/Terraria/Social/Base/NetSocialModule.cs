using System;
using System.Diagnostics;
using Terraria.Net;
using Terraria.Net.Sockets;

namespace Terraria.Social.Base
{
	// Token: 0x02000192 RID: 402
	public abstract class NetSocialModule : ISocialModule
	{
		// Token: 0x06001B4C RID: 6988
		public abstract void Initialize();

		// Token: 0x06001B4D RID: 6989
		public abstract void Shutdown();

		// Token: 0x06001B4E RID: 6990
		public abstract void Close(RemoteAddress address);

		// Token: 0x06001B4F RID: 6991
		public abstract bool IsConnected(RemoteAddress address);

		// Token: 0x06001B50 RID: 6992
		public abstract void Connect(RemoteAddress address);

		// Token: 0x06001B51 RID: 6993
		public abstract bool Send(RemoteAddress address, byte[] data, int length);

		// Token: 0x06001B52 RID: 6994
		public abstract int Receive(RemoteAddress address, byte[] data, int offset, int length);

		// Token: 0x06001B53 RID: 6995
		public abstract bool IsDataAvailable(RemoteAddress address);

		// Token: 0x06001B54 RID: 6996
		public abstract void LaunchLocalServer(Process process, ServerMode mode);

		// Token: 0x06001B55 RID: 6997
		public abstract bool CanInvite();

		// Token: 0x06001B56 RID: 6998
		public abstract void OpenInviteInterface();

		// Token: 0x06001B57 RID: 6999
		public abstract void CancelJoin();

		// Token: 0x06001B58 RID: 7000
		public abstract bool StartListening(SocketConnectionAccepted callback);

		// Token: 0x06001B59 RID: 7001
		public abstract void StopListening();

		// Token: 0x06001B5A RID: 7002
		public abstract ulong GetLobbyId();
	}
}
