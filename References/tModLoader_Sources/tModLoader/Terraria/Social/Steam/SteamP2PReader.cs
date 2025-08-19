using System;
using System.Collections.Generic;
using Steamworks;

namespace Terraria.Social.Steam
{
	// Token: 0x020000EF RID: 239
	public class SteamP2PReader
	{
		// Token: 0x06001857 RID: 6231 RVA: 0x004BBA27 File Offset: 0x004B9C27
		public SteamP2PReader(int channel)
		{
			this._channel = channel;
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x004BBA64 File Offset: 0x004B9C64
		public void ClearUser(CSteamID id)
		{
			Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
			lock (pendingReadBuffers)
			{
				this._deletionQueue.Enqueue(id);
			}
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x004BBAAC File Offset: 0x004B9CAC
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

		// Token: 0x0600185A RID: 6234 RVA: 0x004BBB24 File Offset: 0x004B9D24
		public void SetReadEvent(SteamP2PReader.OnReadEvent method)
		{
			this._readEvent = method;
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x004BBB30 File Offset: 0x004B9D30
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

		// Token: 0x0600185C RID: 6236 RVA: 0x004BBB78 File Offset: 0x004B9D78
		public void ReadTick()
		{
			Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
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
					object steamLock = this.SteamLock;
					uint pcubMsgSize;
					CSteamID psteamIDRemote;
					bool flag;
					lock (steamLock)
					{
						flag = SteamNetworking.ReadP2PPacket(array, (uint)array.Length, ref pcubMsgSize, ref psteamIDRemote, this._channel);
					}
					if (flag)
					{
						if (this._readEvent == null || this._readEvent(array, (int)pcubMsgSize, psteamIDRemote))
						{
							if (!this._pendingReadBuffers.ContainsKey(psteamIDRemote))
							{
								this._pendingReadBuffers[psteamIDRemote] = new Queue<SteamP2PReader.ReadResult>();
							}
							this._pendingReadBuffers[psteamIDRemote].Enqueue(new SteamP2PReader.ReadResult(array, pcubMsgSize));
						}
						else
						{
							this._bufferPool.Enqueue(array);
						}
					}
				}
			}
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x004BBCD8 File Offset: 0x004B9ED8
		public int Receive(CSteamID user, byte[] buffer, int bufferOffset, int bufferSize)
		{
			uint num = 0U;
			Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
			int result;
			lock (pendingReadBuffers)
			{
				if (!this._pendingReadBuffers.ContainsKey(user))
				{
					result = 0;
				}
				else
				{
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
					result = (int)num;
				}
			}
			return result;
		}

		// Token: 0x0400135F RID: 4959
		public object SteamLock = new object();

		// Token: 0x04001360 RID: 4960
		private const int BUFFER_SIZE = 4096;

		// Token: 0x04001361 RID: 4961
		private Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>> _pendingReadBuffers = new Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>>();

		// Token: 0x04001362 RID: 4962
		private Queue<CSteamID> _deletionQueue = new Queue<CSteamID>();

		// Token: 0x04001363 RID: 4963
		private Queue<byte[]> _bufferPool = new Queue<byte[]>();

		// Token: 0x04001364 RID: 4964
		private int _channel;

		// Token: 0x04001365 RID: 4965
		private SteamP2PReader.OnReadEvent _readEvent;

		// Token: 0x0200088B RID: 2187
		public class ReadResult
		{
			// Token: 0x060051C2 RID: 20930 RVA: 0x00697C22 File Offset: 0x00695E22
			public ReadResult(byte[] data, uint size)
			{
				this.Data = data;
				this.Size = size;
				this.Offset = 0U;
			}

			// Token: 0x040069EF RID: 27119
			public byte[] Data;

			// Token: 0x040069F0 RID: 27120
			public uint Size;

			// Token: 0x040069F1 RID: 27121
			public uint Offset;
		}

		// Token: 0x0200088C RID: 2188
		// (Invoke) Token: 0x060051C4 RID: 20932
		public delegate bool OnReadEvent(byte[] data, int size, CSteamID user);
	}
}
