using System;
using System.Collections.Generic;

namespace Terraria.DataStructures
{
	// Token: 0x02000443 RID: 1091
	public static class BufferPool
	{
		// Token: 0x06002BC8 RID: 11208 RVA: 0x0059F3C0 File Offset: 0x0059D5C0
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

		// Token: 0x06002BC9 RID: 11209 RVA: 0x0059F4B0 File Offset: 0x0059D6B0
		public static CachedBuffer Request(byte[] data, int offset, int size)
		{
			CachedBuffer cachedBuffer = BufferPool.Request(size);
			Buffer.BlockCopy(data, offset, cachedBuffer.Data, 0, size);
			return cachedBuffer;
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x0059F4D4 File Offset: 0x0059D6D4
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

		// Token: 0x06002BCB RID: 11211 RVA: 0x0059F550 File Offset: 0x0059D750
		public static void PrintBufferSizes()
		{
			object obj = BufferPool.bufferLock;
			lock (obj)
			{
				Console.WriteLine("SmallBufferQueue.Count: " + BufferPool.SmallBufferQueue.Count);
				Console.WriteLine("MediumBufferQueue.Count: " + BufferPool.MediumBufferQueue.Count);
				Console.WriteLine("LargeBufferQueue.Count: " + BufferPool.LargeBufferQueue.Count);
				Console.WriteLine("");
			}
		}

		// Token: 0x04004FE8 RID: 20456
		private const int SMALL_BUFFER_SIZE = 32;

		// Token: 0x04004FE9 RID: 20457
		private const int MEDIUM_BUFFER_SIZE = 256;

		// Token: 0x04004FEA RID: 20458
		private const int LARGE_BUFFER_SIZE = 16384;

		// Token: 0x04004FEB RID: 20459
		private static object bufferLock = new object();

		// Token: 0x04004FEC RID: 20460
		private static Queue<CachedBuffer> SmallBufferQueue = new Queue<CachedBuffer>();

		// Token: 0x04004FED RID: 20461
		private static Queue<CachedBuffer> MediumBufferQueue = new Queue<CachedBuffer>();

		// Token: 0x04004FEE RID: 20462
		private static Queue<CachedBuffer> LargeBufferQueue = new Queue<CachedBuffer>();
	}
}
