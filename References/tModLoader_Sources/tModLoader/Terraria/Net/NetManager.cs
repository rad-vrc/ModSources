using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Core;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Net.Sockets;

namespace Terraria.Net
{
	// Token: 0x0200011F RID: 287
	public class NetManager
	{
		// Token: 0x06001A18 RID: 6680 RVA: 0x004CAC45 File Offset: 0x004C8E45
		private NetManager()
		{
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x004CAC58 File Offset: 0x004C8E58
		public void Register<T>() where T : NetModule, new()
		{
			T val = Activator.CreateInstance<T>();
			NetManager.PacketTypeStorage<T>.Id = this._moduleCount;
			NetManager.PacketTypeStorage<T>.Module = val;
			this._modules[this._moduleCount] = val;
			this._moduleCount += 1;
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x004CACA2 File Offset: 0x004C8EA2
		public NetModule GetModule<T>() where T : NetModule
		{
			return NetManager.PacketTypeStorage<T>.Module;
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x004CACAE File Offset: 0x004C8EAE
		public ushort GetId<T>() where T : NetModule
		{
			return NetManager.PacketTypeStorage<T>.Id;
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x004CACB8 File Offset: 0x004C8EB8
		public void Read(BinaryReader reader, int userId, int readLength)
		{
			ushort num = reader.ReadUInt16();
			if (this._modules.ContainsKey(num))
			{
				this._modules[num].Deserialize(reader, userId);
			}
			Main.ActiveNetDiagnosticsUI.CountReadModuleMessage((int)num, readLength);
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x004CACFC File Offset: 0x004C8EFC
		public void Broadcast(NetPacket packet, int ignoreClient = -1)
		{
			this.LogSend(packet, -1, ignoreClient);
			for (int i = 0; i < 256; i++)
			{
				if (i != ignoreClient && Netplay.Clients[i].IsConnected())
				{
					this.SendData(Netplay.Clients[i].Socket, packet);
				}
			}
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x004CAD48 File Offset: 0x004C8F48
		public void Broadcast(NetPacket packet, NetManager.BroadcastCondition conditionToBroadcast, int ignoreClient = -1)
		{
			this.LogSend(packet, -1, ignoreClient);
			for (int i = 0; i < 256; i++)
			{
				if (i != ignoreClient && Netplay.Clients[i].IsConnected() && conditionToBroadcast(i))
				{
					this.SendData(Netplay.Clients[i].Socket, packet);
				}
			}
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x004CAD9C File Offset: 0x004C8F9C
		public void SendToSelf(NetPacket packet)
		{
			packet.Reader.BaseStream.Position = 3L;
			this.Read(packet.Reader, Main.myPlayer, packet.Length);
			NetManager.SendCallback(packet);
			Main.ActiveNetDiagnosticsUI.CountSentModuleMessage((int)packet.Id, packet.Length);
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x004CADF7 File Offset: 0x004C8FF7
		public void BroadcastOrLoopback(NetPacket packet)
		{
			if (Main.netMode == 2)
			{
				this.Broadcast(packet, -1);
				return;
			}
			if (Main.netMode == 0)
			{
				this.SendToSelf(packet);
			}
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x004CAE18 File Offset: 0x004C9018
		public void SendToServerOrLoopback(NetPacket packet)
		{
			if (Main.netMode == 1)
			{
				this.SendToServer(packet);
				return;
			}
			if (Main.netMode == 0)
			{
				this.SendToSelf(packet);
			}
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x004CAE38 File Offset: 0x004C9038
		public void SendToServerAndSelf(NetPacket packet)
		{
			if (Main.netMode == 1)
			{
				this.SendToServer(packet);
				this.SendToSelf(packet);
				return;
			}
			if (Main.netMode == 0)
			{
				this.SendToSelf(packet);
			}
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x004CAE5F File Offset: 0x004C905F
		public void SendToServer(NetPacket packet)
		{
			this.LogSend(packet, -1, -1);
			this.SendData(Netplay.Connection.Socket, packet);
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x004CAE7B File Offset: 0x004C907B
		public void SendToClient(NetPacket packet, int playerId)
		{
			this.LogSend(packet, playerId, -1);
			this.SendData(Netplay.Clients[playerId].Socket, packet);
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x004CAE9C File Offset: 0x004C909C
		private void SendData(ISocket socket, NetPacket packet)
		{
			if (Main.netMode != 0)
			{
				packet.ShrinkToFit();
				try
				{
					byte[] data = packet.Buffer.Data;
					int offset = 0;
					int length = packet.Length;
					SocketSendCallback callback;
					if ((callback = NetManager.<>O.<0>__SendCallback) == null)
					{
						callback = (NetManager.<>O.<0>__SendCallback = new SocketSendCallback(NetManager.SendCallback));
					}
					socket.AsyncSend(data, offset, length, callback, packet);
				}
				catch
				{
					Logging.ServerConsoleLine(Language.GetTextValue("Error.ExceptionNormal", Language.GetTextValue("Error.DataSentAfterConnectionLost")), Level.Warn, null, null);
				}
				Main.ActiveNetDiagnosticsUI.CountSentModuleMessage((int)packet.Id, packet.Length);
			}
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x004CAF40 File Offset: 0x004C9140
		public static void SendCallback(object state)
		{
			((NetPacket)state).Recycle();
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x004CAF5B File Offset: 0x004C915B
		private void LogSend(NetPacket packet, int toClient = -1, int ignoreClient = -1)
		{
			if (ModNet.DetailedLogging)
			{
				ModNet.LogSend(toClient, ignoreClient, "NetPacket " + this._modules[packet.Id].GetType().Name, packet.Length);
			}
		}

		// Token: 0x04001417 RID: 5143
		public static readonly NetManager Instance = new NetManager();

		// Token: 0x04001418 RID: 5144
		private Dictionary<ushort, NetModule> _modules = new Dictionary<ushort, NetModule>();

		// Token: 0x04001419 RID: 5145
		private ushort _moduleCount;

		// Token: 0x020008A2 RID: 2210
		private class PacketTypeStorage<T> where T : NetModule
		{
			// Token: 0x04006A3A RID: 27194
			public static ushort Id;

			// Token: 0x04006A3B RID: 27195
			public static T Module;
		}

		// Token: 0x020008A3 RID: 2211
		// (Invoke) Token: 0x06005201 RID: 20993
		public delegate bool BroadcastCondition(int clientIndex);

		// Token: 0x020008A4 RID: 2212
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006A3C RID: 27196
			public static SocketSendCallback <0>__SendCallback;
		}
	}
}
