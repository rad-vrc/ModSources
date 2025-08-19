using System;
using System.Collections.Generic;
using rail;

namespace Terraria.Social.WeGame
{
	// Token: 0x020000E2 RID: 226
	public class WeGameP2PWriter
	{
		// Token: 0x060017B2 RID: 6066 RVA: 0x004B8F14 File Offset: 0x004B7114
		public void QueueSend(RailID user, byte[] data, int length)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				Queue<WeGameP2PWriter.WriteInformation> queue2 = this._pendingSendData.ContainsKey(user) ? this._pendingSendData[user] : (this._pendingSendData[user] = new Queue<WeGameP2PWriter.WriteInformation>());
				int num = length;
				int num2 = 0;
				while (num > 0)
				{
					WeGameP2PWriter.WriteInformation writeInformation;
					if (queue2.Count == 0 || 1024 - queue2.Peek().Size == 0)
					{
						writeInformation = ((this._bufferPool.Count <= 0) ? new WeGameP2PWriter.WriteInformation() : new WeGameP2PWriter.WriteInformation(this._bufferPool.Dequeue()));
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

		// Token: 0x060017B3 RID: 6067 RVA: 0x004B9030 File Offset: 0x004B7230
		public void ClearUser(RailID user)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				if (this._pendingSendData.ContainsKey(user))
				{
					Queue<WeGameP2PWriter.WriteInformation> queue = this._pendingSendData[user];
					while (queue.Count > 0)
					{
						this._bufferPool.Enqueue(queue.Dequeue().Data);
					}
				}
				if (this._pendingSendDataSwap.ContainsKey(user))
				{
					Queue<WeGameP2PWriter.WriteInformation> queue2 = this._pendingSendDataSwap[user];
					while (queue2.Count > 0)
					{
						this._bufferPool.Enqueue(queue2.Dequeue().Data);
					}
				}
			}
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x004B90E4 File Offset: 0x004B72E4
		public void SetLocalPeer(RailID rail_id)
		{
			if (this._local_id == null)
			{
				this._local_id = new RailID();
			}
			this._local_id.id_ = rail_id.id_;
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x004B9110 File Offset: 0x004B7310
		private RailID GetLocalPeer()
		{
			return this._local_id;
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x004B9118 File Offset: 0x004B7318
		private bool IsValid()
		{
			return this._local_id != null && this._local_id.IsValid();
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x004B9138 File Offset: 0x004B7338
		public void SendAll()
		{
			if (!this.IsValid())
			{
				return;
			}
			object @lock = this._lock;
			lock (@lock)
			{
				Utils.Swap<Dictionary<RailID, Queue<WeGameP2PWriter.WriteInformation>>>(ref this._pendingSendData, ref this._pendingSendDataSwap);
			}
			foreach (KeyValuePair<RailID, Queue<WeGameP2PWriter.WriteInformation>> item in this._pendingSendDataSwap)
			{
				Queue<WeGameP2PWriter.WriteInformation> value = item.Value;
				while (value.Count > 0)
				{
					WeGameP2PWriter.WriteInformation writeInformation = value.Dequeue();
					rail_api.RailFactory().RailNetworkHelper().SendData(this.GetLocalPeer(), item.Key, writeInformation.Data, (uint)writeInformation.Size);
					this._bufferPool.Enqueue(writeInformation.Data);
				}
			}
		}

		// Token: 0x0400131B RID: 4891
		private const int BUFFER_SIZE = 1024;

		// Token: 0x0400131C RID: 4892
		private RailID _local_id;

		// Token: 0x0400131D RID: 4893
		private Dictionary<RailID, Queue<WeGameP2PWriter.WriteInformation>> _pendingSendData = new Dictionary<RailID, Queue<WeGameP2PWriter.WriteInformation>>();

		// Token: 0x0400131E RID: 4894
		private Dictionary<RailID, Queue<WeGameP2PWriter.WriteInformation>> _pendingSendDataSwap = new Dictionary<RailID, Queue<WeGameP2PWriter.WriteInformation>>();

		// Token: 0x0400131F RID: 4895
		private Queue<byte[]> _bufferPool = new Queue<byte[]>();

		// Token: 0x04001320 RID: 4896
		private object _lock = new object();

		// Token: 0x02000883 RID: 2179
		public class WriteInformation
		{
			// Token: 0x060051AF RID: 20911 RVA: 0x006977FD File Offset: 0x006959FD
			public WriteInformation()
			{
				this.Data = new byte[1024];
				this.Size = 0;
			}

			// Token: 0x060051B0 RID: 20912 RVA: 0x0069781C File Offset: 0x00695A1C
			public WriteInformation(byte[] data)
			{
				this.Data = data;
				this.Size = 0;
			}

			// Token: 0x040069DD RID: 27101
			public byte[] Data;

			// Token: 0x040069DE RID: 27102
			public int Size;
		}
	}
}
