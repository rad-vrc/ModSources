using System;
using System.Collections.Generic;
using rail;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000168 RID: 360
	public class WeGameP2PReader
	{
		// Token: 0x06001A31 RID: 6705 RVA: 0x004E38A0 File Offset: 0x004E1AA0
		public void ClearUser(RailID id)
		{
			Dictionary<RailID, Queue<WeGameP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
			lock (pendingReadBuffers)
			{
				this._deletionQueue.Enqueue(id);
			}
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x004E38E8 File Offset: 0x004E1AE8
		public bool IsDataAvailable(RailID id)
		{
			Dictionary<RailID, Queue<WeGameP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
			bool result;
			lock (pendingReadBuffers)
			{
				if (!this._pendingReadBuffers.ContainsKey(id))
				{
					result = false;
				}
				else
				{
					Queue<WeGameP2PReader.ReadResult> queue = this._pendingReadBuffers[id];
					if (queue.Count == 0 || queue.Peek().Size == 0U)
					{
						result = false;
					}
					else
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x004E3960 File Offset: 0x004E1B60
		public void SetReadEvent(WeGameP2PReader.OnReadEvent method)
		{
			this._readEvent = method;
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x004E396C File Offset: 0x004E1B6C
		private bool IsPacketAvailable(out uint size)
		{
			object railLock = this.RailLock;
			bool result;
			lock (railLock)
			{
				result = rail_api.RailFactory().RailNetworkHelper().IsDataReady(new RailID
				{
					id_ = this.GetLocalPeer().id_
				}, ref size);
			}
			return result;
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x004E39D0 File Offset: 0x004E1BD0
		private RailID GetLocalPeer()
		{
			return this._local_id;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x004E39D8 File Offset: 0x004E1BD8
		public void SetLocalPeer(RailID rail_id)
		{
			if (this._local_id == null)
			{
				this._local_id = new RailID();
			}
			this._local_id.id_ = rail_id.id_;
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x004E3A04 File Offset: 0x004E1C04
		private bool IsValid()
		{
			return this._local_id != null && this._local_id.IsValid();
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x004E3A24 File Offset: 0x004E1C24
		public void ReadTick()
		{
			if (!this.IsValid())
			{
				return;
			}
			Dictionary<RailID, Queue<WeGameP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
			lock (pendingReadBuffers)
			{
				while (this._deletionQueue.Count > 0)
				{
					this._pendingReadBuffers.Remove(this._deletionQueue.Dequeue());
				}
				uint num;
				while (this.IsPacketAvailable(out num))
				{
					byte[] array;
					if (this._bufferPool.Count == 0)
					{
						array = new byte[Math.Max(num, 4096U)];
					}
					else
					{
						array = this._bufferPool.Dequeue();
					}
					RailID railID = new RailID();
					object railLock = this.RailLock;
					bool flag3;
					lock (railLock)
					{
						flag3 = (rail_api.RailFactory().RailNetworkHelper().ReadData(this.GetLocalPeer(), railID, array, num) == 0);
					}
					if (flag3)
					{
						if (this._readEvent == null || this._readEvent(array, (int)num, railID))
						{
							if (!this._pendingReadBuffers.ContainsKey(railID))
							{
								this._pendingReadBuffers[railID] = new Queue<WeGameP2PReader.ReadResult>();
							}
							this._pendingReadBuffers[railID].Enqueue(new WeGameP2PReader.ReadResult(array, num));
						}
						else
						{
							this._bufferPool.Enqueue(array);
						}
					}
				}
			}
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x004E3B9C File Offset: 0x004E1D9C
		public int Receive(RailID user, byte[] buffer, int bufferOffset, int bufferSize)
		{
			uint num = 0U;
			Dictionary<RailID, Queue<WeGameP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
			lock (pendingReadBuffers)
			{
				if (!this._pendingReadBuffers.ContainsKey(user))
				{
					return 0;
				}
				Queue<WeGameP2PReader.ReadResult> queue = this._pendingReadBuffers[user];
				while (queue.Count > 0)
				{
					WeGameP2PReader.ReadResult readResult = queue.Peek();
					uint num2 = Math.Min((uint)(bufferSize - (int)num), readResult.Size - readResult.Offset);
					if (num2 == 0U)
					{
						return (int)num;
					}
					Array.Copy(readResult.Data, (long)((ulong)readResult.Offset), buffer, (long)bufferOffset + (long)((ulong)num), (long)((ulong)num2));
					if (num2 == readResult.Size - readResult.Offset)
					{
						this._bufferPool.Enqueue(queue.Dequeue().Data);
					}
					else
					{
						readResult.Offset += num2;
					}
					num += num2;
				}
			}
			return (int)num;
		}

		// Token: 0x0400157E RID: 5502
		public object RailLock = new object();

		// Token: 0x0400157F RID: 5503
		private const int BUFFER_SIZE = 4096;

		// Token: 0x04001580 RID: 5504
		private Dictionary<RailID, Queue<WeGameP2PReader.ReadResult>> _pendingReadBuffers = new Dictionary<RailID, Queue<WeGameP2PReader.ReadResult>>();

		// Token: 0x04001581 RID: 5505
		private Queue<RailID> _deletionQueue = new Queue<RailID>();

		// Token: 0x04001582 RID: 5506
		private Queue<byte[]> _bufferPool = new Queue<byte[]>();

		// Token: 0x04001583 RID: 5507
		private WeGameP2PReader.OnReadEvent _readEvent;

		// Token: 0x04001584 RID: 5508
		private RailID _local_id;

		// Token: 0x020005B5 RID: 1461
		public class ReadResult
		{
			// Token: 0x0600328D RID: 12941 RVA: 0x005EC324 File Offset: 0x005EA524
			public ReadResult(byte[] data, uint size)
			{
				this.Data = data;
				this.Size = size;
				this.Offset = 0U;
			}

			// Token: 0x04005A7D RID: 23165
			public byte[] Data;

			// Token: 0x04005A7E RID: 23166
			public uint Size;

			// Token: 0x04005A7F RID: 23167
			public uint Offset;
		}

		// Token: 0x020005B6 RID: 1462
		// (Invoke) Token: 0x0600328F RID: 12943
		public delegate bool OnReadEvent(byte[] data, int size, RailID user);
	}
}
