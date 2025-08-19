using System;
using System.Collections.Generic;
using rail;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000169 RID: 361
	public class WeGameP2PWriter
	{
		// Token: 0x06001A3B RID: 6715 RVA: 0x004E3CCC File Offset: 0x004E1ECC
		public void QueueSend(RailID user, byte[] data, int length)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				Queue<WeGameP2PWriter.WriteInformation> queue;
				if (this._pendingSendData.ContainsKey(user))
				{
					queue = this._pendingSendData[user];
				}
				else
				{
					queue = (this._pendingSendData[user] = new Queue<WeGameP2PWriter.WriteInformation>());
				}
				int i = length;
				int num = 0;
				while (i > 0)
				{
					WeGameP2PWriter.WriteInformation writeInformation;
					if (queue.Count == 0 || 1024 - queue.Peek().Size == 0)
					{
						if (this._bufferPool.Count > 0)
						{
							writeInformation = new WeGameP2PWriter.WriteInformation(this._bufferPool.Dequeue());
						}
						else
						{
							writeInformation = new WeGameP2PWriter.WriteInformation();
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

		// Token: 0x06001A3C RID: 6716 RVA: 0x004E3DE8 File Offset: 0x004E1FE8
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

		// Token: 0x06001A3D RID: 6717 RVA: 0x004E3E9C File Offset: 0x004E209C
		public void SetLocalPeer(RailID rail_id)
		{
			if (this._local_id == null)
			{
				this._local_id = new RailID();
			}
			this._local_id.id_ = rail_id.id_;
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x004E3EC8 File Offset: 0x004E20C8
		private RailID GetLocalPeer()
		{
			return this._local_id;
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x004E3ED0 File Offset: 0x004E20D0
		private bool IsValid()
		{
			return this._local_id != null && this._local_id.IsValid();
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x004E3EF0 File Offset: 0x004E20F0
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
			foreach (KeyValuePair<RailID, Queue<WeGameP2PWriter.WriteInformation>> keyValuePair in this._pendingSendDataSwap)
			{
				Queue<WeGameP2PWriter.WriteInformation> value = keyValuePair.Value;
				while (value.Count > 0)
				{
					WeGameP2PWriter.WriteInformation writeInformation = value.Dequeue();
					bool flag2 = rail_api.RailFactory().RailNetworkHelper().SendData(this.GetLocalPeer(), keyValuePair.Key, writeInformation.Data, (uint)writeInformation.Size) == 0;
					this._bufferPool.Enqueue(writeInformation.Data);
				}
			}
		}

		// Token: 0x04001585 RID: 5509
		private const int BUFFER_SIZE = 1024;

		// Token: 0x04001586 RID: 5510
		private RailID _local_id;

		// Token: 0x04001587 RID: 5511
		private Dictionary<RailID, Queue<WeGameP2PWriter.WriteInformation>> _pendingSendData = new Dictionary<RailID, Queue<WeGameP2PWriter.WriteInformation>>();

		// Token: 0x04001588 RID: 5512
		private Dictionary<RailID, Queue<WeGameP2PWriter.WriteInformation>> _pendingSendDataSwap = new Dictionary<RailID, Queue<WeGameP2PWriter.WriteInformation>>();

		// Token: 0x04001589 RID: 5513
		private Queue<byte[]> _bufferPool = new Queue<byte[]>();

		// Token: 0x0400158A RID: 5514
		private object _lock = new object();

		// Token: 0x020005B7 RID: 1463
		public class WriteInformation
		{
			// Token: 0x06003292 RID: 12946 RVA: 0x005EC341 File Offset: 0x005EA541
			public WriteInformation()
			{
				this.Data = new byte[1024];
				this.Size = 0;
			}

			// Token: 0x06003293 RID: 12947 RVA: 0x005EC360 File Offset: 0x005EA560
			public WriteInformation(byte[] data)
			{
				this.Data = data;
				this.Size = 0;
			}

			// Token: 0x04005A80 RID: 23168
			public byte[] Data;

			// Token: 0x04005A81 RID: 23169
			public int Size;
		}
	}
}
