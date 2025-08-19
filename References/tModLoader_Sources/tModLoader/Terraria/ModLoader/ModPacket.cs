using System;
using System.IO;
using System.Runtime.CompilerServices;
using Terraria.Localization;
using Terraria.Net.Sockets;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Used to send data between the server and client. Syncing data is essential for keeping clients up to date with changes to the game state. ModPacket is used to sync arbitrary data, most commonly data corresponding to this mod. The <see href="https://github.com/tModLoader/tModLoader/wiki/intermediate-netcode">Intermediate netcode wiki page</see> explains more about this concept.
	/// <para /> Initialize a ModPacket using the <see cref="M:Terraria.ModLoader.Mod.GetPacket(System.Int32)" /> method.
	/// <para /> This class inherits from BinaryWriter. This means that you can use all of its writing functions to send information between client and server. This class also comes with a <see cref="M:Terraria.ModLoader.ModPacket.Send(System.Int32,System.Int32)" /> method that's used to actually send everything you've written between client and server.
	/// <para /> ModPacket has all the same methods as BinaryWriter, and some additional ones.
	/// </summary>
	/// <seealso cref="T:System.IO.BinaryWriter" />
	// Token: 0x020001BA RID: 442
	public sealed class ModPacket : BinaryWriter
	{
		// Token: 0x06002278 RID: 8824 RVA: 0x004E7B34 File Offset: 0x004E5D34
		internal ModPacket(byte messageID, int capacity = 256) : base(new MemoryStream(capacity))
		{
			this.Write(0);
			this.Write(messageID);
		}

		/// <summary>
		/// Sends all the information you've written to this ModPacket over the network to clients or the server. When called on a client, the data will be sent to the server and the optional parameters are ignored. When called on a server, the data will be sent to either all clients, all clients except a specific client, or just a specific client:
		/// <code>
		/// // Sends to all connected clients
		/// packet.Send();
		///
		/// // Sends to a specific client only
		/// packet.Send(toClient: somePlayer.whoAmI);
		///
		/// // Sends to all other clients except a specific client
		/// packet.Send(ignoreClient: somePlayer.whoAmI);
		/// </code>
		/// Typically if data is sent from a client to the server, the server will then need to relay this to all other clients to keep them in sync. This is when the <paramref name="ignoreClient" /> option will be used.
		/// </summary>
		// Token: 0x06002279 RID: 8825 RVA: 0x004E7B58 File Offset: 0x004E5D58
		public void Send(int toClient = -1, int ignoreClient = -1)
		{
			this.Finish();
			if (ModNet.DetailedLogging)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 2);
				defaultInterpolatedStringHandler.AppendLiteral("ModPacket.Send ");
				Mod mod = ModNet.GetMod((int)this.netID);
				defaultInterpolatedStringHandler.AppendFormatted(((mod != null) ? mod.Name : null) ?? "ModLoader");
				defaultInterpolatedStringHandler.AppendLiteral("(");
				defaultInterpolatedStringHandler.AppendFormatted<short>(this.netID);
				defaultInterpolatedStringHandler.AppendLiteral(")");
				ModNet.LogSend(toClient, ignoreClient, defaultInterpolatedStringHandler.ToStringAndClear(), (int)this.len);
			}
			if (Main.netMode == 1)
			{
				Netplay.Connection.Socket.AsyncSend(this.buf, 0, (int)this.len, new SocketSendCallback(this.SendCallback), null);
				if (this.netID >= 0)
				{
					ModNet.ModNetDiagnosticsUI.CountSentMessage((int)this.netID, (int)this.len);
					return;
				}
			}
			else
			{
				if (toClient != -1)
				{
					Netplay.Clients[toClient].Socket.AsyncSend(this.buf, 0, (int)this.len, new SocketSendCallback(this.SendCallback), null);
					return;
				}
				for (int i = 0; i < 256; i++)
				{
					if (i != ignoreClient && Netplay.Clients[i].IsConnected() && NetMessage.buffer[i].broadcast)
					{
						Netplay.Clients[i].Socket.AsyncSend(this.buf, 0, (int)this.len, new SocketSendCallback(this.SendCallback), null);
					}
				}
			}
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x004E7CC7 File Offset: 0x004E5EC7
		private void SendCallback(object state)
		{
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x004E7CCC File Offset: 0x004E5ECC
		private void Finish()
		{
			if (this.buf != null)
			{
				return;
			}
			if (this.OutStream.Position > 65535L)
			{
				throw new Exception(Language.GetTextValue("tModLoader.MPPacketTooLarge", this.OutStream.Position, ushort.MaxValue));
			}
			this.len = (ushort)this.OutStream.Position;
			this.Seek(0, SeekOrigin.Begin);
			this.Write(this.len);
			this.Close();
			this.buf = ((MemoryStream)this.OutStream).GetBuffer();
		}

		// Token: 0x0400170A RID: 5898
		private byte[] buf;

		// Token: 0x0400170B RID: 5899
		private ushort len;

		// Token: 0x0400170C RID: 5900
		internal short netID = -1;
	}
}
