using System;
using System.Collections.Generic;
using rail;

namespace Terraria.Social.WeGame
{
	// Token: 0x020000E1 RID: 225
	public class WeGameP2PReader
	{
		// Token: 0x060017A8 RID: 6056 RVA: 0x004B8AE4 File Offset: 0x004B6CE4
		public void ClearUser(RailID id)
		{
			Dictionary<RailID, Queue<WeGameP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
			lock (pendingReadBuffers)
			{
				this._deletionQueue.Enqueue(id);
			}
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x004B8B2C File Offset: 0x004B6D2C
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

		// Token: 0x060017AA RID: 6058 RVA: 0x004B8BA4 File Offset: 0x004B6DA4
		public void SetReadEvent(WeGameP2PReader.OnReadEvent method)
		{
			this._readEvent = method;
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x004B8BB0 File Offset: 0x004B6DB0
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

		// Token: 0x060017AC RID: 6060 RVA: 0x004B8C14 File Offset: 0x004B6E14
		private RailID GetLocalPeer()
		{
			return this._local_id;
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x004B8C1C File Offset: 0x004B6E1C
		public void SetLocalPeer(RailID rail_id)
		{
			if (this._local_id == null)
			{
				this._local_id = new RailID();
			}
			this._local_id.id_ = rail_id.id_;
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x004B8C48 File Offset: 0x004B6E48
		private bool IsValid()
		{
			return this._local_id != null && this._local_id.IsValid();
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x004B8C68 File Offset: 0x004B6E68
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
				uint size;
				while (this.IsPacketAvailable(out size))
				{
					byte[] array = (this._bufferPool.Count != 0) ? this._bufferPool.Dequeue() : new byte[Math.Max(size, 4096U)];
					RailID railID = new RailID();
					object railLock = this.RailLock;
					bool flag;
					lock (railLock)
					{
						flag = (rail_api.RailFactory().RailNetworkHelper().ReadData(this.GetLocalPeer(), railID, array, size) == 0);
					}
					if (flag)
					{
						if (this._readEvent == null || this._readEvent(array, (int)size, railID))
						{
							if (!this._pendingReadBuffers.ContainsKey(railID))
							{
								this._pendingReadBuffers[railID] = new Queue<WeGameP2PReader.ReadResult>();
							}
							this._pendingReadBuffers[railID].Enqueue(new WeGameP2PReader.ReadResult(array, size));
						}
						else
						{
							this._bufferPool.Enqueue(array);
						}
					}
				}
			}
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x004B8DE0 File Offset: 0x004B6FE0
		public int Receive(RailID user, byte[] buffer, int bufferOffset, int bufferSize)
		{
			uint num = 0U;
			Dictionary<RailID, Queue<WeGameP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
			int result;
			lock (pendingReadBuffers)
			{
				if (!this._pendingReadBuffers.ContainsKey(user))
				{
					result = 0;
				}
				else
				{
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
					result = (int)num;
				}
			}
			return result;
		}

		// Token: 0x04001314 RID: 4884
		public object RailLock = new object();

		// Token: 0x04001315 RID: 4885
		private const int BUFFER_SIZE = 4096;

		// Token: 0x04001316 RID: 4886
		private Dictionary<RailID, Queue<WeGameP2PReader.ReadResult>> _pendingReadBuffers = new Dictionary<RailID, Queue<WeGameP2PReader.ReadResult>>();

		// Token: 0x04001317 RID: 4887
		private Queue<RailID> _deletionQueue = new Queue<RailID>();

		// Token: 0x04001318 RID: 4888
		private Queue<byte[]> _bufferPool = new Queue<byte[]>();

		// Token: 0x04001319 RID: 4889
		private WeGameP2PReader.OnReadEvent _readEvent;

		// Token: 0x0400131A RID: 4890
		private RailID _local_id;

		// Token: 0x02000881 RID: 2177
		public class ReadResult
		{
			// Token: 0x060051AA RID: 20906 RVA: 0x006977E0 File Offset: 0x006959E0
			public ReadResult(byte[] data, uint size)
			{
				this.Data = data;
				this.Size = size;
				this.Offset = 0U;
			}

			// Token: 0x040069DA RID: 27098
			public byte[] Data;

			// Token: 0x040069DB RID: 27099
			public uint Size;

			// Token: 0x040069DC RID: 27100
			public uint Offset;
		}

		// Token: 0x02000882 RID: 2178
		// (Invoke) Token: 0x060051AC RID: 20908
		public delegate bool OnReadEvent(byte[] data, int size, RailID user);
	}
}
