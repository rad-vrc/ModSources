using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace Terraria.Testing
{
	// Token: 0x020000C5 RID: 197
	public class PacketHistory
	{
		// Token: 0x0600169A RID: 5786 RVA: 0x004B50FF File Offset: 0x004B32FF
		public PacketHistory(int historySize = 100, int bufferSize = 65535)
		{
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x004B5108 File Offset: 0x004B3308
		[Conditional("DEBUG")]
		public void Record(byte[] buffer, int offset, int length)
		{
			length = Math.Max(0, length);
			int offset2 = this.AppendPacket(length).Offset;
			Buffer.BlockCopy(buffer, offset, this._buffer, offset2, length);
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x004B513C File Offset: 0x004B333C
		private PacketHistory.PacketView AppendPacket(int size)
		{
			int num = this._bufferPosition;
			if (num + size > this._buffer.Length)
			{
				num = 0;
			}
			PacketHistory.PacketView packetView = new PacketHistory.PacketView(num, size, DateTime.Now);
			this._packets[this._historyPosition] = packetView;
			this._historyPosition = (this._historyPosition + 1) % this._packets.Length;
			this._bufferPosition = num + size;
			return packetView;
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x004B51A0 File Offset: 0x004B33A0
		[Conditional("DEBUG")]
		public void Dump(string reason)
		{
			byte[] dst = new byte[this._buffer.Length];
			Buffer.BlockCopy(this._buffer, this._bufferPosition, dst, 0, this._buffer.Length - this._bufferPosition);
			Buffer.BlockCopy(this._buffer, 0, dst, this._buffer.Length - this._bufferPosition, this._bufferPosition);
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1;
			for (int i = 0; i < this._packets.Length; i++)
			{
				PacketHistory.PacketView packetView = this._packets[(i + this._historyPosition) % this._packets.Length];
				if (packetView.Offset != 0 || packetView.Length != 0)
				{
					stringBuilder.Append(string.Format("Packet {0} [Assumed MessageID: {4}, Size: {2}, Buffer Position: {1}, Timestamp: {3:G}]\r\n", new object[]
					{
						num++,
						packetView.Offset,
						packetView.Length,
						packetView.Time,
						this._buffer[packetView.Offset]
					}));
					for (int j = 0; j < packetView.Length; j++)
					{
						stringBuilder.Append(this._buffer[packetView.Offset + j].ToString("X2") + " ");
						if (j % 16 == 15 && j != this._packets.Length - 1)
						{
							stringBuilder.Append("\r\n");
						}
					}
					stringBuilder.Append("\r\n\r\n");
				}
			}
			stringBuilder.Append(reason);
			Directory.CreateDirectory(Path.Combine(Main.SavePath, "NetDump"));
			File.WriteAllText(Path.Combine(Main.SavePath, "NetDump", this.CreateDumpFileName()), stringBuilder.ToString());
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x004B536C File Offset: 0x004B356C
		private string CreateDumpFileName()
		{
			DateTime dateTime = DateTime.Now.ToLocalTime();
			return string.Format("Net_{0}_{1}_{2}_{3}.txt", new object[]
			{
				Main.dedServ ? "TerrariaServer" : "Terraria",
				Main.versionNumber,
				dateTime.ToString("MM-dd-yy_HH-mm-ss-ffff", CultureInfo.InvariantCulture),
				Thread.CurrentThread.ManagedThreadId
			});
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x004B53DB File Offset: 0x004B35DB
		[Conditional("DEBUG")]
		private void InitializeBuffer(int historySize, int bufferSize)
		{
			this._packets = new PacketHistory.PacketView[historySize];
			this._buffer = new byte[bufferSize];
		}

		// Token: 0x040012AF RID: 4783
		private byte[] _buffer;

		// Token: 0x040012B0 RID: 4784
		private PacketHistory.PacketView[] _packets;

		// Token: 0x040012B1 RID: 4785
		private int _bufferPosition;

		// Token: 0x040012B2 RID: 4786
		private int _historyPosition;

		// Token: 0x0200087A RID: 2170
		private struct PacketView
		{
			// Token: 0x0600519E RID: 20894 RVA: 0x00697635 File Offset: 0x00695835
			public PacketView(int offset, int length, DateTime time)
			{
				this.Offset = offset;
				this.Length = length;
				this.Time = time;
			}

			// Token: 0x040069C6 RID: 27078
			public static readonly PacketHistory.PacketView Empty = new PacketHistory.PacketView(0, 0, DateTime.Now);

			// Token: 0x040069C7 RID: 27079
			public readonly int Offset;

			// Token: 0x040069C8 RID: 27080
			public readonly int Length;

			// Token: 0x040069C9 RID: 27081
			public readonly DateTime Time;
		}
	}
}
