using System;
using System.Collections.Generic;

namespace Terraria.DataStructures
{
	// Token: 0x020006D4 RID: 1748
	public static class BufferPool
	{
		// Token: 0x0600490C RID: 18700 RVA: 0x0064C680 File Offset: 0x0064A880
		public static CachedBuffer Request(int size)
		{
			object obj = BufferPool.bufferLock;
			CachedBuffer result;
			lock (obj)
			{
				if (size <= 32)
				{
					if (BufferPool.SmallBufferQueue.Count == 0)
					{
						result = new CachedBuffer(new byte[32]);
					}
					else
					{
						result = BufferPool.SmallBufferQueue.Dequeue().Activate();
					}
				}
				else if (size <= 256)
				{
					if (BufferPool.MediumBufferQueue.Count == 0)
					{
						result = new CachedBuffer(new byte[256]);
					}
					else
					{
						result = BufferPool.MediumBufferQueue.Dequeue().Activate();
					}
				}
				else if (size <= 16384)
				{
					if (BufferPool.LargeBufferQueue.Count == 0)
					{
						result = new CachedBuffer(new byte[16384]);
					}
					else
					{
						result = BufferPool.LargeBufferQueue.Dequeue().Activate();
					}
				}
				else
				{
					result = new CachedBuffer(new byte[size]);
				}
			}
			return result;
		}

		// Token: 0x0600490D RID: 18701 RVA: 0x0064C770 File Offset: 0x0064A970
		public static CachedBuffer Request(byte[] data, int offset, int size)
		{
			CachedBuffer cachedBuffer = BufferPool.Request(size);
			Buffer.BlockCopy(data, offset, cachedBuffer.Data, 0, size);
			return cachedBuffer;
		}

		// Token: 0x0600490E RID: 18702 RVA: 0x0064C794 File Offset: 0x0064A994
		public static void Recycle(CachedBuffer buffer)
		{
			int length = buffer.Length;
			object obj = BufferPool.bufferLock;
			lock (obj)
			{
				if (length <= 32)
				{
					BufferPool.SmallBufferQueue.Enqueue(buffer);
				}
				else if (length <= 256)
				{
					BufferPool.MediumBufferQueue.Enqueue(buffer);
				}
				else if (length <= 16384)
				{
					BufferPool.LargeBufferQueue.Enqueue(buffer);
				}
			}
		}

		// Token: 0x0600490F RID: 18703 RVA: 0x0064C810 File Offset: 0x0064AA10
		public static void PrintBufferSizes()
		{
			object obj = BufferPool.bufferLock;
			lock (obj)
			{
				Console.WriteLine("SmallBufferQueue.Count: " + BufferPool.SmallBufferQueue.Count.ToString());
				Console.WriteLine("MediumBufferQueue.Count: " + BufferPool.MediumBufferQueue.Count.ToString());
				Console.WriteLine("LargeBufferQueue.Count: " + BufferPool.LargeBufferQueue.Count.ToString());
				Console.WriteLine("");
			}
		}

		// Token: 0x04005E67 RID: 24167
		private const int SMALL_BUFFER_SIZE = 32;

		// Token: 0x04005E68 RID: 24168
		private const int MEDIUM_BUFFER_SIZE = 256;

		// Token: 0x04005E69 RID: 24169
		private const int LARGE_BUFFER_SIZE = 16384;

		// Token: 0x04005E6A RID: 24170
		private static object bufferLock = new object();

		// Token: 0x04005E6B RID: 24171
		private static Queue<CachedBuffer> SmallBufferQueue = new Queue<CachedBuffer>();

		// Token: 0x04005E6C RID: 24172
		private static Queue<CachedBuffer> MediumBufferQueue = new Queue<CachedBuffer>();

		// Token: 0x04005E6D RID: 24173
		private static Queue<CachedBuffer> LargeBufferQueue = new Queue<CachedBuffer>();
	}
}
