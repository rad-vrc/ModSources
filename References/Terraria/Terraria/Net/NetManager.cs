using System;
using System.Collections.Generic;
using System.IO;
using Terraria.Net.Sockets;

namespace Terraria.Net
{
	// Token: 0x020000C1 RID: 193
	public class NetManager
	{
		// Token: 0x0600141D RID: 5149 RVA: 0x004A2633 File Offset: 0x004A0833
		private NetManager()
		{
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x004A2648 File Offset: 0x004A0848
		public void Register<T>() where T : NetModule, new()
		{
			T t = Activator.CreateInstance<T>();
			NetManager.PacketTypeStorage<T>.Id = this._moduleCount;
			NetManager.PacketTypeStorage<T>.Module = t;
			this._modules[this._moduleCount] = t;
			this._moduleCount += 1;
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x004A2692 File Offset: 0x004A0892
		public NetModule GetModule<T>() where T : NetModule
		{
			return NetManager.PacketTypeStorage<T>.Module;
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x004A269E File Offset: 0x004A089E
		public ushort GetId<T>() where T : NetModule
		{
			return NetManager.PacketTypeStorage<T>.Id;
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x004A26A8 File Offset: 0x004A08A8
		public void Read(BinaryReader reader, int userId, int readLength)
		{
			ushort num = reader.ReadUInt16();
			if (this._modules.ContainsKey(num))
			{
				this._modules[num].Deserialize(reader, userId);
			}
			Main.ActiveNetDiagnosticsUI.CountReadModuleMessage((int)num, readLength);
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x004A26EC File Offset: 0x004A08EC
		public void Broadcast(NetPacket packet, int ignoreClient = -1)
		{
			for (int i = 0; i < 256; i++)
			{
				if (i != ignoreClient && Netplay.Clients[i].IsConnected())
				{
					this.SendData(Netplay.Clients[i].Socket, packet);
				}
			}
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x004A2730 File Offset: 0x004A0930
		public void Broadcast(NetPacket packet, NetManager.BroadcastCondition conditionToBroadcast, int ignoreClient = -1)
		{
			for (int i = 0; i < 256; i++)
			{
				if (i != ignoreClient && Netplay.Clients[i].IsConnected() && conditionToBroadcast(i))
				{
					this.SendData(Netplay.Clients[i].Socket, packet);
				}
			}
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x004A277C File Offset: 0x004A097C
		public void SendToSelf(NetPacket packet)
		{
			packet.Reader.BaseStream.Position = 3L;
			this.Read(packet.Reader, Main.myPlayer, packet.Length);
			NetManager.SendCallback(packet);
			Main.ActiveNetDiagnosticsUI.CountSentModuleMessage((int)packet.Id, packet.Length);
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x004A27D7 File Offset: 0x004A09D7
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

		// Token: 0x06001426 RID: 5158 RVA: 0x004A27F8 File Offset: 0x004A09F8
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

		// Token: 0x06001427 RID: 5159 RVA: 0x004A2818 File Offset: 0x004A0A18
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

		// Token: 0x06001428 RID: 5160 RVA: 0x004A283F File Offset: 0x004A0A3F
		public void SendToServer(NetPacket packet)
		{
			this.SendData(Netplay.Connection.Socket, packet);
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x004A2852 File Offset: 0x004A0A52
		public void SendToClient(NetPacket packet, int playerId)
		{
			this.SendData(Netplay.Clients[playerId].Socket, packet);
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x004A2868 File Offset: 0x004A0A68
		private void SendData(ISocket socket, NetPacket packet)
		{
			if (Main.netMode == 0)
			{
				return;
			}
			packet.ShrinkToFit();
			try
			{
				socket.AsyncSend(packet.Buffer.Data, 0, packet.Length, new SocketSendCallback(NetManager.SendCallback), packet);
			}
			catch
			{
			}
			Main.ActiveNetDiagnosticsUI.CountSentModuleMessage((int)packet.Id, packet.Length);
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x004A28DC File Offset: 0x004A0ADC
		public static void SendCallback(object state)
		{
			((NetPacket)state).Recycle();
		}

		// Token: 0x040011F9 RID: 4601
		public static readonly NetManager Instance = new NetManager();

		// Token: 0x040011FA RID: 4602
		private Dictionary<ushort, NetModule> _modules = new Dictionary<ushort, NetModule>();

		// Token: 0x040011FB RID: 4603
		private ushort _moduleCount;

		// Token: 0x0200055D RID: 1373
		private class PacketTypeStorage<T> where T : NetModule
		{
			// Token: 0x040058EE RID: 22766
			public static ushort Id;

			// Token: 0x040058EF RID: 22767
			public static T Module;
		}

		// Token: 0x0200055E RID: 1374
		// (Invoke) Token: 0x06003122 RID: 12578
		public delegate bool BroadcastCondition(int clientIndex);
	}
}
