using System;
using System.Collections.Generic;
using Steamworks;

namespace Terraria.Social.Steam
{
	// Token: 0x020000F0 RID: 240
	public class SteamP2PWriter
	{
		// Token: 0x0600185E RID: 6238 RVA: 0x004BBDD8 File Offset: 0x004B9FD8
		public SteamP2PWriter(int channel)
		{
			this._channel = channel;
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x004BBE14 File Offset: 0x004BA014
		public void QueueSend(CSteamID user, byte[] data, int length)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				Queue<SteamP2PWriter.WriteInformation> queue2 = this._pendingSendData.ContainsKey(user) ? this._pendingSendData[user] : (this._pendingSendData[user] = new Queue<SteamP2PWriter.WriteInformation>());
				int num = length;
				int num2 = 0;
				while (num > 0)
				{
					SteamP2PWriter.WriteInformation writeInformation;
					if (queue2.Count == 0 || 1024 - queue2.Peek().Size == 0)
					{
						writeInformation = ((this._bufferPool.Count <= 0) ? new SteamP2PWriter.WriteInformation() : new SteamP2PWriter.WriteInformation(this._bufferPool.Dequeue()));
						queue2.Enqueue(writeInformation);
					}
					else
					{
						writeInformation = queue2.Peek();
					}
					int num3 = Math.Min(num, 1024 - writeInformation.Size);
					Array.Copy(data, num2, writeInformation.Data, writeInformation.Size, num3);
					writeInformation.Size += num3;
					num -= num3;
					num2 += num3;
				}
			}
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x004BBF30 File Offset: 0x004BA130
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

		// Token: 0x06001861 RID: 6241 RVA: 0x004BBFE4 File Offset: 0x004BA1E4
		public void SendAll()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				Utils.Swap<Dictionary<CSteamID, Queue<SteamP2PWriter.WriteInformation>>>(ref this._pendingSendData, ref this._pendingSendDataSwap);
			}
			foreach (KeyValuePair<CSteamID, Queue<SteamP2PWriter.WriteInformation>> item in this._pendingSendDataSwap)
			{
				Queue<SteamP2PWriter.WriteInformation> value = item.Value;
				while (value.Count > 0)
				{
					SteamP2PWriter.WriteInformation writeInformation = value.Dequeue();
					SteamNetworking.SendP2PPacket(item.Key, writeInformation.Data, (uint)writeInformation.Size, 2, this._channel);
					this._bufferPool.Enqueue(writeInformation.Data);
				}
			}
		}

		// Token: 0x04001366 RID: 4966
		private const int BUFFER_SIZE = 1024;

		// Token: 0x04001367 RID: 4967
		private Dictionary<CSteamID, Queue<SteamP2PWriter.WriteInformation>> _pendingSendData = new Dictionary<CSteamID, Queue<SteamP2PWriter.WriteInformation>>();

		// Token: 0x04001368 RID: 4968
		private Dictionary<CSteamID, Queue<SteamP2PWriter.WriteInformation>> _pendingSendDataSwap = new Dictionary<CSteamID, Queue<SteamP2PWriter.WriteInformation>>();

		// Token: 0x04001369 RID: 4969
		private Queue<byte[]> _bufferPool = new Queue<byte[]>();

		// Token: 0x0400136A RID: 4970
		private int _channel;

		// Token: 0x0400136B RID: 4971
		private object _lock = new object();

		// Token: 0x0200088D RID: 2189
		public class WriteInformation
		{
			// Token: 0x060051C7 RID: 20935 RVA: 0x00697C3F File Offset: 0x00695E3F
			public WriteInformation()
			{
				this.Data = new byte[1024];
				this.Size = 0;
			}

			// Token: 0x060051C8 RID: 20936 RVA: 0x00697C5E File Offset: 0x00695E5E
			public WriteInformation(byte[] data)
			{
				this.Data = data;
				this.Size = 0;
			}

			// Token: 0x040069F2 RID: 27122
			public byte[] Data;

			// Token: 0x040069F3 RID: 27123
			public int Size;
		}
	}
}
