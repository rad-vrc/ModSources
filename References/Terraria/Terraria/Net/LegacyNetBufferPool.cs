using System;
using System.Collections.Generic;

namespace Terraria.Net
{
	// Token: 0x020000BF RID: 191
	public class LegacyNetBufferPool
	{
		// Token: 0x06001413 RID: 5139 RVA: 0x004A21D4 File Offset: 0x004A03D4
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

		// Token: 0x06001414 RID: 5140 RVA: 0x004A22D8 File Offset: 0x004A04D8
		public static byte[] RequestBuffer(byte[] data, int offset, int size)
		{
			byte[] array = LegacyNetBufferPool.RequestBuffer(size);
			Buffer.BlockCopy(data, offset, array, 0, size);
			return array;
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x004A22F8 File Offset: 0x004A04F8
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

		// Token: 0x06001416 RID: 5142 RVA: 0x004A2374 File Offset: 0x004A0574
		public static void DisplayBufferSizes()
		{
			object obj = LegacyNetBufferPool.bufferLock;
			lock (obj)
			{
				Main.NewText(string.Concat(new object[]
				{
					"Small Buffers:  ",
					LegacyNetBufferPool._smallBufferQueue.Count,
					" queued of ",
					LegacyNetBufferPool._smallBufferCount
				}), byte.MaxValue, byte.MaxValue, byte.MaxValue);
				Main.NewText(string.Concat(new object[]
				{
					"Medium Buffers: ",
					LegacyNetBufferPool._mediumBufferQueue.Count,
					" queued of ",
					LegacyNetBufferPool._mediumBufferCount
				}), byte.MaxValue, byte.MaxValue, byte.MaxValue);
				Main.NewText(string.Concat(new object[]
				{
					"Large Buffers:  ",
					LegacyNetBufferPool._largeBufferQueue.Count,
					" queued of ",
					LegacyNetBufferPool._largeBufferCount
				}), byte.MaxValue, byte.MaxValue, byte.MaxValue);
				Main.NewText("Custom Buffers: 0 queued of " + LegacyNetBufferPool._customBufferCount, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			}
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x004A24CC File Offset: 0x004A06CC
		public static void PrintBufferSizes()
		{
			object obj = LegacyNetBufferPool.bufferLock;
			lock (obj)
			{
				Console.WriteLine(string.Concat(new object[]
				{
					"Small Buffers:  ",
					LegacyNetBufferPool._smallBufferQueue.Count,
					" queued of ",
					LegacyNetBufferPool._smallBufferCount
				}));
				Console.WriteLine(string.Concat(new object[]
				{
					"Medium Buffers: ",
					LegacyNetBufferPool._mediumBufferQueue.Count,
					" queued of ",
					LegacyNetBufferPool._mediumBufferCount
				}));
				Console.WriteLine(string.Concat(new object[]
				{
					"Large Buffers:  ",
					LegacyNetBufferPool._largeBufferQueue.Count,
					" queued of ",
					LegacyNetBufferPool._largeBufferCount
				}));
				Console.WriteLine("Custom Buffers: 0 queued of " + LegacyNetBufferPool._customBufferCount);
				Console.WriteLine("");
			}
		}

		// Token: 0x040011EE RID: 4590
		private const int SMALL_BUFFER_SIZE = 256;

		// Token: 0x040011EF RID: 4591
		private const int MEDIUM_BUFFER_SIZE = 1024;

		// Token: 0x040011F0 RID: 4592
		private const int LARGE_BUFFER_SIZE = 16384;

		// Token: 0x040011F1 RID: 4593
		private static object bufferLock = new object();

		// Token: 0x040011F2 RID: 4594
		private static Queue<byte[]> _smallBufferQueue = new Queue<byte[]>();

		// Token: 0x040011F3 RID: 4595
		private static Queue<byte[]> _mediumBufferQueue = new Queue<byte[]>();

		// Token: 0x040011F4 RID: 4596
		private static Queue<byte[]> _largeBufferQueue = new Queue<byte[]>();

		// Token: 0x040011F5 RID: 4597
		private static int _smallBufferCount;

		// Token: 0x040011F6 RID: 4598
		private static int _mediumBufferCount;

		// Token: 0x040011F7 RID: 4599
		private static int _largeBufferCount;

		// Token: 0x040011F8 RID: 4600
		private static int _customBufferCount;
	}
}
