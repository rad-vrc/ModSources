using System;
using System.Collections.Generic;

namespace Terraria.Net
{
	// Token: 0x0200011D RID: 285
	public class LegacyNetBufferPool
	{
		// Token: 0x06001A0C RID: 6668 RVA: 0x004CA6E0 File Offset: 0x004C88E0
		public static byte[] RequestBuffer(int size)
		{
			object obj = LegacyNetBufferPool.bufferLock;
			byte[] result;
			lock (obj)
			{
				if (size <= 256)
				{
					if (LegacyNetBufferPool._smallBufferQueue.Count == 0)
					{
						LegacyNetBufferPool._smallBufferCount++;
						result = new byte[256];
					}
					else
					{
						result = LegacyNetBufferPool._smallBufferQueue.Dequeue();
					}
				}
				else if (size <= 1024)
				{
					if (LegacyNetBufferPool._mediumBufferQueue.Count == 0)
					{
						LegacyNetBufferPool._mediumBufferCount++;
						result = new byte[1024];
					}
					else
					{
						result = LegacyNetBufferPool._mediumBufferQueue.Dequeue();
					}
				}
				else if (size <= 16384)
				{
					if (LegacyNetBufferPool._largeBufferQueue.Count == 0)
					{
						LegacyNetBufferPool._largeBufferCount++;
						result = new byte[16384];
					}
					else
					{
						result = LegacyNetBufferPool._largeBufferQueue.Dequeue();
					}
				}
				else
				{
					LegacyNetBufferPool._customBufferCount++;
					result = new byte[size];
				}
			}
			return result;
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x004CA7E4 File Offset: 0x004C89E4
		public static byte[] RequestBuffer(byte[] data, int offset, int size)
		{
			byte[] array = LegacyNetBufferPool.RequestBuffer(size);
			Buffer.BlockCopy(data, offset, array, 0, size);
			return array;
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x004CA804 File Offset: 0x004C8A04
		public static void ReturnBuffer(byte[] buffer)
		{
			int num = buffer.Length;
			object obj = LegacyNetBufferPool.bufferLock;
			lock (obj)
			{
				if (num <= 256)
				{
					LegacyNetBufferPool._smallBufferQueue.Enqueue(buffer);
				}
				else if (num <= 1024)
				{
					LegacyNetBufferPool._mediumBufferQueue.Enqueue(buffer);
				}
				else if (num <= 16384)
				{
					LegacyNetBufferPool._largeBufferQueue.Enqueue(buffer);
				}
			}
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x004CA880 File Offset: 0x004C8A80
		public static void DisplayBufferSizes()
		{
			object obj = LegacyNetBufferPool.bufferLock;
			lock (obj)
			{
				Main.NewText("Small Buffers:  " + LegacyNetBufferPool._smallBufferQueue.Count.ToString() + " queued of " + LegacyNetBufferPool._smallBufferCount.ToString(), byte.MaxValue, byte.MaxValue, byte.MaxValue);
				Main.NewText("Medium Buffers: " + LegacyNetBufferPool._mediumBufferQueue.Count.ToString() + " queued of " + LegacyNetBufferPool._mediumBufferCount.ToString(), byte.MaxValue, byte.MaxValue, byte.MaxValue);
				Main.NewText("Large Buffers:  " + LegacyNetBufferPool._largeBufferQueue.Count.ToString() + " queued of " + LegacyNetBufferPool._largeBufferCount.ToString(), byte.MaxValue, byte.MaxValue, byte.MaxValue);
				Main.NewText("Custom Buffers: 0 queued of " + LegacyNetBufferPool._customBufferCount.ToString(), byte.MaxValue, byte.MaxValue, byte.MaxValue);
			}
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x004CA9A0 File Offset: 0x004C8BA0
		public static void PrintBufferSizes()
		{
			object obj = LegacyNetBufferPool.bufferLock;
			lock (obj)
			{
				Console.WriteLine("Small Buffers:  " + LegacyNetBufferPool._smallBufferQueue.Count.ToString() + " queued of " + LegacyNetBufferPool._smallBufferCount.ToString());
				Console.WriteLine("Medium Buffers: " + LegacyNetBufferPool._mediumBufferQueue.Count.ToString() + " queued of " + LegacyNetBufferPool._mediumBufferCount.ToString());
				Console.WriteLine("Large Buffers:  " + LegacyNetBufferPool._largeBufferQueue.Count.ToString() + " queued of " + LegacyNetBufferPool._largeBufferCount.ToString());
				Console.WriteLine("Custom Buffers: 0 queued of " + LegacyNetBufferPool._customBufferCount.ToString());
				Console.WriteLine("");
			}
		}

		// Token: 0x04001409 RID: 5129
		private const int SMALL_BUFFER_SIZE = 256;

		// Token: 0x0400140A RID: 5130
		private const int MEDIUM_BUFFER_SIZE = 1024;

		// Token: 0x0400140B RID: 5131
		private const int LARGE_BUFFER_SIZE = 16384;

		// Token: 0x0400140C RID: 5132
		private static object bufferLock = new object();

		// Token: 0x0400140D RID: 5133
		private static Queue<byte[]> _smallBufferQueue = new Queue<byte[]>();

		// Token: 0x0400140E RID: 5134
		private static Queue<byte[]> _mediumBufferQueue = new Queue<byte[]>();

		// Token: 0x0400140F RID: 5135
		private static Queue<byte[]> _largeBufferQueue = new Queue<byte[]>();

		// Token: 0x04001410 RID: 5136
		private static int _smallBufferCount;

		// Token: 0x04001411 RID: 5137
		private static int _mediumBufferCount;

		// Token: 0x04001412 RID: 5138
		private static int _largeBufferCount;

		// Token: 0x04001413 RID: 5139
		private static int _customBufferCount;
	}
}
