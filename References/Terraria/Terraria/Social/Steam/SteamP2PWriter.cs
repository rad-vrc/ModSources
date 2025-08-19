using System;
using System.Collections.Generic;
using Steamworks;

namespace Terraria.Social.Steam
{
	// Token: 0x0200017D RID: 381
	public class SteamP2PWriter
	{
		// Token: 0x06001AD5 RID: 6869 RVA: 0x004E6308 File Offset: 0x004E4508
		public SteamP2PWriter(int channel)
		{
			this._channel = channel;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x004E6344 File Offset: 0x004E4544
		public void QueueSend(CSteamID user, byte[] data, int length)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				Queue<SteamP2PWriter.WriteInformation> queue;
				if (this._pendingSendData.ContainsKey(user))
				{
					queue = this._pendingSendData[user];
				}
				else
				{
					queue = (this._pendingSendData[user] = new Queue<SteamP2PWriter.WriteInformation>());
				}
				int i = length;
				int num = 0;
				while (i > 0)
				{
					SteamP2PWriter.WriteInformation writeInformation;
					if (queue.Count == 0 || 1024 - queue.Peek().Size == 0)
					{
						if (this._bufferPool.Count > 0)
						{
							writeInformation = new SteamP2PWriter.WriteInformation(this._bufferPool.Dequeue());
						}
						else
						{
							writeInformation = new SteamP2PWriter.WriteInformation();
						}
						queue.Enqueue(writeInformation);
					}
					else
					{
						writeInformation = queue.Peek();
					}
					int num2 = Math.Min(i, 1024 - writeInformation.Size);
					Array.Copy(data, num, writeInformation.Data, writeInformation.Size, num2);
					writeInformation.Size += num2;
					i -= num2;
					num += num2;
				}
			}
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x004E6460 File Offset: 0x004E4660
		public void ClearUser(CSteamID user)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				if (this._pendingSendData.ContainsKey(user))
				{
					Queue<SteamP2PWriter.WriteInformation> queue = this._pendingSendData[user];
					while (queue.Count > 0)
					{
						this._bufferPool.Enqueue(queue.Dequeue().Data);
					}
				}
				if (this._pendingSendDataSwap.ContainsKey(user))
				{
					Queue<SteamP2PWriter.WriteInformation> queue2 = this._pendingSendDataSwap[user];
					while (queue2.Count > 0)
					{
						this._bufferPool.Enqueue(queue2.Dequeue().Data);
					}
				}
			}
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x004E6514 File Offset: 0x004E4714
		public void SendAll()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				Utils.Swap<Dictionary<CSteamID, Queue<SteamP2PWriter.WriteInformation>>>(ref this._pendingSendData, ref this._pendingSendDataSwap);
			}
			foreach (KeyValuePair<CSteamID, Queue<SteamP2PWriter.WriteInformation>> keyValuePair in this._pendingSendDataSwap)
			{
				Queue<SteamP2PWriter.WriteInformation> value = keyValuePair.Value;
				while (value.Count > 0)
				{
					SteamP2PWriter.WriteInformation writeInformation = value.Dequeue();
					SteamNetworking.SendP2PPacket(keyValuePair.Key, writeInformation.Data, (uint)writeInformation.Size, 2, this._channel);
					this._bufferPool.Enqueue(writeInformation.Data);
				}
			}
		}

		// Token: 0x040015D4 RID: 5588
		private const int BUFFER_SIZE = 1024;

		// Token: 0x040015D5 RID: 5589
		private Dictionary<CSteamID, Queue<SteamP2PWriter.WriteInformation>> _pendingSendData = new Dictionary<CSteamID, Queue<SteamP2PWriter.WriteInformation>>();

		// Token: 0x040015D6 RID: 5590
		private Dictionary<CSteamID, Queue<SteamP2PWriter.WriteInformation>> _pendingSendDataSwap = new Dictionary<CSteamID, Queue<SteamP2PWriter.WriteInformation>>();

		// Token: 0x040015D7 RID: 5591
		private Queue<byte[]> _bufferPool = new Queue<byte[]>();

		// Token: 0x040015D8 RID: 5592
		private int _channel;

		// Token: 0x040015D9 RID: 5593
		private object _lock = new object();

		// Token: 0x020005C1 RID: 1473
		public class WriteInformation
		{
			// Token: 0x060032A8 RID: 12968 RVA: 0x005EC4D9 File Offset: 0x005EA6D9
			public WriteInformation()
			{
				this.Data = new byte[1024];
				this.Size = 0;
			}

			// Token: 0x060032A9 RID: 12969 RVA: 0x005EC4F8 File Offset: 0x005EA6F8
			public WriteInformation(byte[] data)
			{
				this.Data = data;
				this.Size = 0;
			}

			// Token: 0x04005A93 RID: 23187
			public byte[] Data;

			// Token: 0x04005A94 RID: 23188
			public int Size;
		}
	}
}
