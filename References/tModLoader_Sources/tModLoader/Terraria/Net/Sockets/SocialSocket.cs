using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.Social;

namespace Terraria.Net.Sockets
{
	// Token: 0x02000128 RID: 296
	public class SocialSocket : ISocket
	{
		// Token: 0x06001A50 RID: 6736 RVA: 0x004CB285 File Offset: 0x004C9485
		public SocialSocket()
		{
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x004CB28D File Offset: 0x004C948D
		public SocialSocket(RemoteAddress remoteAddress)
		{
			this._remoteAddress = remoteAddress;
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x004CB29C File Offset: 0x004C949C
		void ISocket.Close()
		{
			if (this._remoteAddress != null)
			{
				ModNet.Log(this._remoteAddress, "Closing SocialSocket");
				SocialAPI.Network.Close(this._remoteAddress);
				this._remoteAddress = null;
			}
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x004CB2CD File Offset: 0x004C94CD
		bool ISocket.IsConnected()
		{
			return SocialAPI.Network.IsConnected(this._remoteAddress);
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x004CB2DF File Offset: 0x004C94DF
		void ISocket.Connect(RemoteAddress address)
		{
			this._remoteAddress = address;
			SocialAPI.Network.Connect(address);
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x004CB2F4 File Offset: 0x004C94F4
		void ISocket.AsyncSend(byte[] data, int offset, int size, SocketSendCallback callback, object state)
		{
			if (this._remoteAddress == null)
			{
				ModNet.Warn("SocialSocket, AsyncSend after connection closed.");
				return;
			}
			if (ModNet.DetailedLogging)
			{
				RemoteAddress remoteAddress = this._remoteAddress;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
				defaultInterpolatedStringHandler.AppendLiteral("send ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(size);
				ModNet.Debug(remoteAddress, defaultInterpolatedStringHandler.ToStringAndClear());
			}
			SocialAPI.Network.Send(this._remoteAddress, data, size);
			Task.Run(delegate()
			{
				callback(state);
			});
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x004CB388 File Offset: 0x004C9588
		private void ReadCallback(byte[] data, int offset, int size, SocketReceiveCallback callback, object state)
		{
			int size2;
			while ((size2 = SocialAPI.Network.Receive(this._remoteAddress, data, offset, size)) == 0)
			{
				Thread.Sleep(1);
			}
			callback(state, size2);
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x004CB3C0 File Offset: 0x004C95C0
		void ISocket.AsyncReceive(byte[] data, int offset, int size, SocketReceiveCallback callback, object state)
		{
			Task.Run(delegate()
			{
				new SocialSocket.InternalReadCallback(this.ReadCallback)(data, offset, size, callback, state);
			});
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x004CB40F File Offset: 0x004C960F
		void ISocket.SendQueuedPackets()
		{
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x004CB411 File Offset: 0x004C9611
		bool ISocket.IsDataAvailable()
		{
			return SocialAPI.Network.IsDataAvailable(this._remoteAddress);
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x004CB423 File Offset: 0x004C9623
		RemoteAddress ISocket.GetRemoteAddress()
		{
			return this._remoteAddress;
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x004CB42B File Offset: 0x004C962B
		bool ISocket.StartListening(SocketConnectionAccepted callback)
		{
			return SocialAPI.Network.StartListening(callback);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x004CB438 File Offset: 0x004C9638
		void ISocket.StopListening()
		{
			SocialAPI.Network.StopListening();
		}

		// Token: 0x0400142A RID: 5162
		private RemoteAddress _remoteAddress;

		// Token: 0x020008A5 RID: 2213
		// (Invoke) Token: 0x06005205 RID: 20997
		private delegate void InternalReadCallback(byte[] data, int offset, int size, SocketReceiveCallback callback, object state);
	}
}
