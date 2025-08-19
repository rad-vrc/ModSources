using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace Terraria.Testing
{
	// Token: 0x020000A6 RID: 166
	public class PacketHistory
	{
		// Token: 0x06001375 RID: 4981 RVA: 0x0000B904 File Offset: 0x00009B04
		public PacketHistory(int historySize = 100, int bufferSize = 65535)
		{
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0049F85C File Offset: 0x0049DA5C
		[Conditional("DEBUG")]
		public void Record(byte[] buffer, int offset, int length)
		{
			length = Math.Max(0, length);
			PacketHistory.PacketView packetView = this.AppendPacket(length);
			Buffer.BlockCopy(buffer, offset, this._buffer, packetView.Offset, length);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0049F890 File Offset: 0x0049DA90
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

		// Token: 0x06001378 RID: 4984 RVA: 0x0049F8F4 File Offset: 0x0049DAF4
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

		// Token: 0x06001379 RID: 4985 RVA: 0x0049FAC0 File Offset: 0x0049DCC0
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

		// Token: 0x0600137A RID: 4986 RVA: 0x0049FB2F File Offset: 0x0049DD2F
		[Conditional("DEBUG")]
		private void InitializeBuffer(int historySize, int bufferSize)
		{
			this._packets = new PacketHistory.PacketView[historySize];
			this._buffer = new byte[bufferSize];
		}

		// Token: 0x040011A9 RID: 4521
		private byte[] _buffer;

		// Token: 0x040011AA RID: 4522
		private PacketHistory.PacketView[] _packets;

		// Token: 0x040011AB RID: 4523
		private int _bufferPosition;

		// Token: 0x040011AC RID: 4524
		private int _historyPosition;

		// Token: 0x02000551 RID: 1361
		private struct PacketView
		{
			// Token: 0x06003109 RID: 12553 RVA: 0x005E4C5D File Offset: 0x005E2E5D
			public PacketView(int offset, int length, DateTime time)
			{
				this.Offset = offset;
				this.Length = length;
				this.Time = time;
			}

			// Token: 0x040058C7 RID: 22727
			public static readonly PacketHistory.PacketView Empty = new PacketHistory.PacketView(0, 0, DateTime.Now);

			// Token: 0x040058C8 RID: 22728
			public readonly int Offset;

			// Token: 0x040058C9 RID: 22729
			public readonly int Length;

			// Token: 0x040058CA RID: 22730
			public readonly DateTime Time;
		}
	}
}
