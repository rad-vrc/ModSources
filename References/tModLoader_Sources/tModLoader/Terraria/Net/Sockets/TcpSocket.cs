using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using ReLogic.OS;
using Terraria.ModLoader;

namespace Terraria.Net.Sockets
{
	// Token: 0x0200012C RID: 300
	public class TcpSocket : ISocket
	{
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x004CB444 File Offset: 0x004C9644
		public int MessagesInQueue
		{
			get
			{
				return this._messagesInQueue;
			}
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x004CB44C File Offset: 0x004C964C
		public TcpSocket()
		{
			this._connection = new TcpClient
			{
				NoDelay = true
			};
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x004CB484 File Offset: 0x004C9684
		public TcpSocket(TcpClient tcpClient)
		{
			this._connection = tcpClient;
			this._connection.NoDelay = true;
			IPEndPoint iPEndPoint = (IPEndPoint)tcpClient.Client.RemoteEndPoint;
			this._remoteAddress = new TcpAddress(iPEndPoint.Address, iPEndPoint.Port);
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x004CB4ED File Offset: 0x004C96ED
		void ISocket.Close()
		{
			if (this._remoteAddress != null)
			{
				ModNet.Log(this._remoteAddress, "Closing TcpSocket");
			}
			this._remoteAddress = null;
			this._connection.Close();
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x004CB519 File Offset: 0x004C9719
		bool ISocket.IsConnected()
		{
			return this._connection != null && this._connection.Client != null && this._connection.Connected;
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x004CB540 File Offset: 0x004C9740
		void ISocket.Connect(RemoteAddress address)
		{
			TcpAddress tcpAddress = (TcpAddress)address;
			this._connection.Connect(tcpAddress.Address, tcpAddress.Port);
			this._remoteAddress = address;
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x004CB574 File Offset: 0x004C9774
		private void ReadCallback(IAsyncResult result)
		{
			Tuple<SocketReceiveCallback, object> tuple = (Tuple<SocketReceiveCallback, object>)result.AsyncState;
			tuple.Item1(tuple.Item2, this._connection.GetStream().EndRead(result));
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x004CB5B0 File Offset: 0x004C97B0
		private void SendCallback(IAsyncResult result)
		{
			Tuple<SocketSendCallback, object> tuple;
			if (Platform.IsWindows)
			{
				tuple = (Tuple<SocketSendCallback, object>)result.AsyncState;
			}
			else
			{
				object[] array = (object[])result.AsyncState;
				LegacyNetBufferPool.ReturnBuffer((byte[])array[1]);
				tuple = (Tuple<SocketSendCallback, object>)array[0];
			}
			ValueTuple<object, int> valueTuple = (ValueTuple<object, int>)tuple.Item2;
			object state = valueTuple.Item1;
			int size = valueTuple.Item2;
			tuple = Tuple.Create<SocketSendCallback, object>(tuple.Item1, state);
			if (ModNet.DetailedLogging)
			{
				RemoteAddress remoteAddress = this._remoteAddress;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
				defaultInterpolatedStringHandler.AppendLiteral("sent ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(size);
				ModNet.Debug(remoteAddress, defaultInterpolatedStringHandler.ToStringAndClear());
			}
			try
			{
				if (this._connection.Connected)
				{
					this._connection.GetStream().EndWrite(result);
					tuple.Item1(tuple.Item2);
				}
			}
			catch (Exception)
			{
				((ISocket)this).Close();
			}
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x004CB69C File Offset: 0x004C989C
		void ISocket.SendQueuedPackets()
		{
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x004CB6A0 File Offset: 0x004C98A0
		void ISocket.AsyncSend(byte[] data, int offset, int size, SocketSendCallback callback, object state)
		{
			if (!this._connection.Connected)
			{
				ModNet.Warn("TcpSocket, AsyncSend after connection closed.");
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
			state = new ValueTuple<object, int>(state, size);
			if (!Platform.IsWindows)
			{
				byte[] array = LegacyNetBufferPool.RequestBuffer(data, offset, size);
				this._connection.GetStream().BeginWrite(array, 0, size, new AsyncCallback(this.SendCallback), new object[]
				{
					new Tuple<SocketSendCallback, object>(callback, state),
					array
				});
				return;
			}
			this._connection.GetStream().BeginWrite(data, 0, size, new AsyncCallback(this.SendCallback), new Tuple<SocketSendCallback, object>(callback, state));
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x004CB77A File Offset: 0x004C997A
		void ISocket.AsyncReceive(byte[] data, int offset, int size, SocketReceiveCallback callback, object state)
		{
			this._connection.GetStream().BeginRead(data, offset, size, new AsyncCallback(this.ReadCallback), new Tuple<SocketReceiveCallback, object>(callback, state));
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x004CB7A5 File Offset: 0x004C99A5
		bool ISocket.IsDataAvailable()
		{
			return this._connection.Connected && this._connection.GetStream().DataAvailable;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x004CB7C6 File Offset: 0x004C99C6
		RemoteAddress ISocket.GetRemoteAddress()
		{
			return this._remoteAddress;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x004CB7D0 File Offset: 0x004C99D0
		bool ISocket.StartListening(SocketConnectionAccepted callback)
		{
			IPAddress address = IPAddress.Any;
			string value;
			if (Program.LaunchParameters.TryGetValue("-ip", out value) && !IPAddress.TryParse(value, out address))
			{
				address = IPAddress.Any;
			}
			this._isListening = true;
			this._listenerCallback = callback;
			if (this._listener == null)
			{
				this._listener = new TcpListener(address, Netplay.ListenPort);
			}
			try
			{
				this._listener.Start();
			}
			catch (Exception)
			{
				return false;
			}
			new Thread(new ThreadStart(this.ListenLoop))
			{
				IsBackground = true,
				Name = "TCP Listen Thread"
			}.Start();
			return true;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x004CB87C File Offset: 0x004C9A7C
		void ISocket.StopListening()
		{
			this._isListening = false;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x004CB888 File Offset: 0x004C9A88
		private void ListenLoop()
		{
			while (this._isListening && !Netplay.Disconnect)
			{
				try
				{
					ISocket socket = new TcpSocket(this._listener.AcceptTcpClient());
					this._listenerCallback(socket);
				}
				catch (Exception)
				{
				}
			}
			this._listener.Stop();
		}

		// Token: 0x0400142B RID: 5163
		private byte[] _packetBuffer = new byte[1024];

		// Token: 0x0400142C RID: 5164
		private List<object> _callbackBuffer = new List<object>();

		// Token: 0x0400142D RID: 5165
		private int _messagesInQueue;

		// Token: 0x0400142E RID: 5166
		private TcpClient _connection;

		// Token: 0x0400142F RID: 5167
		private TcpListener _listener;

		// Token: 0x04001430 RID: 5168
		private SocketConnectionAccepted _listenerCallback;

		// Token: 0x04001431 RID: 5169
		private RemoteAddress _remoteAddress;

		// Token: 0x04001432 RID: 5170
		private bool _isListening;
	}
}
