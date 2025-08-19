using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ReLogic.OS;
using Terraria.Localization;

namespace Terraria.Net.Sockets
{
	// Token: 0x020000C8 RID: 200
	public class TcpSocket : ISocket
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x004A2B0A File Offset: 0x004A0D0A
		public int MessagesInQueue
		{
			get
			{
				return this._messagesInQueue;
			}
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x004A2B12 File Offset: 0x004A0D12
		public TcpSocket()
		{
			this._connection = new TcpClient
			{
				NoDelay = true
			};
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x004A2B48 File Offset: 0x004A0D48
		public TcpSocket(TcpClient tcpClient)
		{
			this._connection = tcpClient;
			this._connection.NoDelay = true;
			IPEndPoint ipendPoint = (IPEndPoint)tcpClient.Client.RemoteEndPoint;
			this._remoteAddress = new TcpAddress(ipendPoint.Address, ipendPoint.Port);
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x004A2BB1 File Offset: 0x004A0DB1
		void ISocket.Close()
		{
			this._remoteAddress = null;
			this._connection.Close();
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x004A2BC5 File Offset: 0x004A0DC5
		bool ISocket.IsConnected()
		{
			return this._connection != null && this._connection.Client != null && this._connection.Connected;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x004A2BEC File Offset: 0x004A0DEC
		void ISocket.Connect(RemoteAddress address)
		{
			TcpAddress tcpAddress = (TcpAddress)address;
			this._connection.Connect(tcpAddress.Address, tcpAddress.Port);
			this._remoteAddress = address;
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x004A2C20 File Offset: 0x004A0E20
		private void ReadCallback(IAsyncResult result)
		{
			Tuple<SocketReceiveCallback, object> tuple = (Tuple<SocketReceiveCallback, object>)result.AsyncState;
			tuple.Item1(tuple.Item2, this._connection.GetStream().EndRead(result));
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x004A2C5C File Offset: 0x004A0E5C
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
			try
			{
				this._connection.GetStream().EndWrite(result);
				tuple.Item1(tuple.Item2);
			}
			catch (Exception)
			{
				((ISocket)this).Close();
			}
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0003C3EC File Offset: 0x0003A5EC
		void ISocket.SendQueuedPackets()
		{
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x004A2CDC File Offset: 0x004A0EDC
		void ISocket.AsyncSend(byte[] data, int offset, int size, SocketSendCallback callback, object state)
		{
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

		// Token: 0x06001461 RID: 5217 RVA: 0x004A2D59 File Offset: 0x004A0F59
		void ISocket.AsyncReceive(byte[] data, int offset, int size, SocketReceiveCallback callback, object state)
		{
			this._connection.GetStream().BeginRead(data, offset, size, new AsyncCallback(this.ReadCallback), new Tuple<SocketReceiveCallback, object>(callback, state));
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x004A2D84 File Offset: 0x004A0F84
		bool ISocket.IsDataAvailable()
		{
			return this._connection.Connected && this._connection.GetStream().DataAvailable;
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x004A2DA5 File Offset: 0x004A0FA5
		RemoteAddress ISocket.GetRemoteAddress()
		{
			return this._remoteAddress;
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x004A2DB0 File Offset: 0x004A0FB0
		bool ISocket.StartListening(SocketConnectionAccepted callback)
		{
			IPAddress any = IPAddress.Any;
			string ipString;
			if (Program.LaunchParameters.TryGetValue("-ip", out ipString) && !IPAddress.TryParse(ipString, out any))
			{
				any = IPAddress.Any;
			}
			this._isListening = true;
			this._listenerCallback = callback;
			if (this._listener == null)
			{
				this._listener = new TcpListener(any, Netplay.ListenPort);
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

		// Token: 0x06001465 RID: 5221 RVA: 0x004A2E5C File Offset: 0x004A105C
		void ISocket.StopListening()
		{
			this._isListening = false;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x004A2E68 File Offset: 0x004A1068
		private void ListenLoop()
		{
			while (this._isListening && !Netplay.Disconnect)
			{
				try
				{
					ISocket socket = new TcpSocket(this._listener.AcceptTcpClient());
					Console.WriteLine(Language.GetTextValue("Net.ClientConnecting", socket.GetRemoteAddress()));
					this._listenerCallback(socket);
				}
				catch (Exception)
				{
				}
			}
			this._listener.Stop();
		}

		// Token: 0x04001201 RID: 4609
		private byte[] _packetBuffer = new byte[1024];

		// Token: 0x04001202 RID: 4610
		private List<object> _callbackBuffer = new List<object>();

		// Token: 0x04001203 RID: 4611
		private int _messagesInQueue;

		// Token: 0x04001204 RID: 4612
		private TcpClient _connection;

		// Token: 0x04001205 RID: 4613
		private TcpListener _listener;

		// Token: 0x04001206 RID: 4614
		private SocketConnectionAccepted _listenerCallback;

		// Token: 0x04001207 RID: 4615
		private RemoteAddress _remoteAddress;

		// Token: 0x04001208 RID: 4616
		private bool _isListening;
	}
}
