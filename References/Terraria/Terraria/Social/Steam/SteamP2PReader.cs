using System;
using System.Collections.Generic;
using Steamworks;

namespace Terraria.Social.Steam
{
	// Token: 0x0200017C RID: 380
	public class SteamP2PReader
	{
		// Token: 0x06001ACE RID: 6862 RVA: 0x004E5F56 File Offset: 0x004E4156
		public SteamP2PReader(int channel)
		{
			this._channel = channel;
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x004E5F94 File Offset: 0x004E4194
		public void ClearUser(CSteamID id)
		{
			Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
			lock (pendingReadBuffers)
			{
				this._deletionQueue.Enqueue(id);
			}
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x004E5FDC File Offset: 0x004E41DC
		public bool IsDataAvailable(CSteamID id)
		{
			Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
			bool result;
			lock (pendingReadBuffers)
			{
				if (!this._pendingReadBuffers.ContainsKey(id))
				{
					result = false;
				}
				else
				{
					Queue<SteamP2PReader.ReadResult> queue = this._pendingReadBuffers[id];
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

		// Token: 0x06001AD1 RID: 6865 RVA: 0x004E6054 File Offset: 0x004E4254
		public void SetReadEvent(SteamP2PReader.OnReadEvent method)
		{
			this._readEvent = method;
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x004E6060 File Offset: 0x004E4260
		private bool IsPacketAvailable(out uint size)
		{
			object steamLock = this.SteamLock;
			bool result;
			lock (steamLock)
			{
				result = SteamNetworking.IsP2PPacketAvailable(ref size, this._channel);
			}
			return result;
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x004E60A8 File Offset: 0x004E42A8
		public void ReadTick()
		{
			Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
			lock (pendingReadBuffers)
			{
				while (this._deletionQueue.Count > 0)
				{
					this._pendingReadBuffers.Remove(this._deletionQueue.Dequeue());
				}
				uint val;
				while (this.IsPacketAvailable(out val))
				{
					byte[] array;
					if (this._bufferPool.Count == 0)
					{
						array = new byte[Math.Max(val, 4096U)];
					}
					else
					{
						array = this._bufferPool.Dequeue();
					}
					object steamLock = this.SteamLock;
					uint size;
					CSteamID csteamID;
					bool flag3;
					lock (steamLock)
					{
						flag3 = SteamNetworking.ReadP2PPacket(array, (uint)array.Length, ref size, ref csteamID, this._channel);
					}
					if (flag3)
					{
						if (this._readEvent == null || this._readEvent(array, (int)size, csteamID))
						{
							if (!this._pendingReadBuffers.ContainsKey(csteamID))
							{
								this._pendingReadBuffers[csteamID] = new Queue<SteamP2PReader.ReadResult>();
							}
							this._pendingReadBuffers[csteamID].Enqueue(new SteamP2PReader.ReadResult(array, size));
						}
						else
						{
							this._bufferPool.Enqueue(array);
						}
					}
				}
			}
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x004E620C File Offset: 0x004E440C
		public int Receive(CSteamID user, byte[] buffer, int bufferOffset, int bufferSize)
		{
			uint num = 0U;
			Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
			lock (pendingReadBuffers)
			{
				if (!this._pendingReadBuffers.ContainsKey(user))
				{
					return 0;
				}
				Queue<SteamP2PReader.ReadResult> queue = this._pendingReadBuffers[user];
				while (queue.Count > 0)
				{
					SteamP2PReader.ReadResult readResult = queue.Peek();
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

		// Token: 0x040015CD RID: 5581
		public object SteamLock = new object();

		// Token: 0x040015CE RID: 5582
		private const int BUFFER_SIZE = 4096;

		// Token: 0x040015CF RID: 5583
		private Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>> _pendingReadBuffers = new Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>>();

		// Token: 0x040015D0 RID: 5584
		private Queue<CSteamID> _deletionQueue = new Queue<CSteamID>();

		// Token: 0x040015D1 RID: 5585
		private Queue<byte[]> _bufferPool = new Queue<byte[]>();

		// Token: 0x040015D2 RID: 5586
		private int _channel;

		// Token: 0x040015D3 RID: 5587
		private SteamP2PReader.OnReadEvent _readEvent;

		// Token: 0x020005BF RID: 1471
		public class ReadResult
		{
			// Token: 0x060032A3 RID: 12963 RVA: 0x005EC4BC File Offset: 0x005EA6BC
			public ReadResult(byte[] data, uint size)
			{
				this.Data = data;
				this.Size = size;
				this.Offset = 0U;
			}

			// Token: 0x04005A90 RID: 23184
			public byte[] Data;

			// Token: 0x04005A91 RID: 23185
			public uint Size;

			// Token: 0x04005A92 RID: 23186
			public uint Offset;
		}

		// Token: 0x020005C0 RID: 1472
		// (Invoke) Token: 0x060032A5 RID: 12965
		public delegate bool OnReadEvent(byte[] data, int size, CSteamID user);
	}
}
