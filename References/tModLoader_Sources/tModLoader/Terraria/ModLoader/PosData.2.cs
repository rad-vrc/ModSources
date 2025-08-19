using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader
{
	// Token: 0x020001EB RID: 491
	public static class PosData
	{
		/// <summary>
		/// Gets a Position ID based on the x,y position. If using in an order sensitive case, see NextLocation.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		// Token: 0x06002684 RID: 9860 RVA: 0x004FE019 File Offset: 0x004FC219
		public static int CoordsToPos(int x, int y)
		{
			return x * Main.maxTilesY + y;
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x004FE024 File Offset: 0x004FC224
		public static int FindIndex<T>(this PosData<T>[] posMap, int x, int y)
		{
			return posMap.FindIndex(PosData.CoordsToPos(x, y));
		}

		/// <summary>
		/// Searches for the value i for which <code>posMap[i].pos &lt; pos &lt; posMap[i + 1].pos</code>
		/// </summary>
		/// <returns>The index of the nearest entry with <see cref="F:Terraria.ModLoader.PosData`1.pos" /> &lt;= <paramref name="pos" /> or -1 if <paramref name="pos" /> &lt; <paramref name="posMap" />[0].pos</returns>
		// Token: 0x06002686 RID: 9862 RVA: 0x004FE034 File Offset: 0x004FC234
		public static int FindIndex<T>(this PosData<T>[] posMap, int pos)
		{
			int minimum = -1;
			int maximum = posMap.Length;
			while (maximum - minimum > 1)
			{
				int split = (minimum + maximum) / 2;
				if (posMap[split].pos <= pos)
				{
					minimum = split;
				}
				else
				{
					maximum = split;
				}
			}
			return minimum;
		}

		/// <summary>
		/// Raw lookup function. Always returns the raw entry in the position map. Use if default values returned are a concern, as negative position returned are ~'null'
		/// </summary>
		// Token: 0x06002687 RID: 9863 RVA: 0x004FE06C File Offset: 0x004FC26C
		public static PosData<T> Find<T>(this PosData<T>[] posMap, int pos)
		{
			int i = posMap.FindIndex(pos);
			if (i >= 0)
			{
				return posMap[i];
			}
			return PosData<T>.nullPosData;
		}

		/// <summary>
		/// General purpose lookup function. Always returns a value (even if that value is `default`).
		/// See <see cref="M:Terraria.ModLoader.PosData`1.OrderedSparseLookupBuilder.#ctor(System.Int32,System.Boolean,System.Boolean)" />for more info
		/// </summary>
		// Token: 0x06002688 RID: 9864 RVA: 0x004FE092 File Offset: 0x004FC292
		public static T Lookup<T>(this PosData<T>[] posMap, int x, int y)
		{
			return posMap.Lookup(PosData.CoordsToPos(x, y));
		}

		/// <summary>
		/// General purpose lookup function. Always returns a value (even if that value is `default`).
		/// See <see cref="M:Terraria.ModLoader.PosData`1.OrderedSparseLookupBuilder.#ctor(System.Int32,System.Boolean,System.Boolean)" />for more info
		/// </summary>
		// Token: 0x06002689 RID: 9865 RVA: 0x004FE0A1 File Offset: 0x004FC2A1
		public static T Lookup<T>(this PosData<T>[] posMap, int pos)
		{
			return posMap.Find(pos).value;
		}

		/// <summary>
		/// For use with uncompressed sparse data lookups. Checks that the exact position exists in the lookup table.
		/// </summary>
		// Token: 0x0600268A RID: 9866 RVA: 0x004FE0AF File Offset: 0x004FC2AF
		public static bool LookupExact<T>(this PosData<T>[] posMap, int x, int y, out T data)
		{
			return posMap.LookupExact(PosData.CoordsToPos(x, y), out data);
		}

		/// <summary>
		/// For use with uncompressed sparse data lookups. Checks that the exact position exists in the lookup table.
		/// </summary>
		// Token: 0x0600268B RID: 9867 RVA: 0x004FE0C0 File Offset: 0x004FC2C0
		public static bool LookupExact<T>(this PosData<T>[] posMap, int pos, out T data)
		{
			PosData<T> posData = posMap.Find(pos);
			if (posData.pos != pos)
			{
				data = default(T);
				return false;
			}
			data = posData.value;
			return true;
		}

		/// <summary>
		/// Searches around the provided point to check for the nearest entry in the map for OrdereredSparse data
		/// Doesn't work with 'compressed' lookups from <see cref="T:Terraria.ModLoader.PosData`1.OrderedSparseLookupBuilder" />
		/// </summary>
		/// <param name="posMap"></param>
		/// <param name="pt"></param>
		/// <param name="distance"> The distance between the provided Point and nearby entry </param>
		/// <param name="entry"></param>
		/// <returns> True if successfully found an entry nearby </returns>
		// Token: 0x0600268C RID: 9868 RVA: 0x004FE0F4 File Offset: 0x004FC2F4
		public static bool NearbySearchOrderedPosMap<T>(PosData<T>[] posMap, Point pt, int distance, out PosData<T> entry)
		{
			entry = new PosData<T>(-1, default(T));
			if (posMap.Length == 0)
			{
				return false;
			}
			int minPos = PosData.CoordsToPos(Math.Max(pt.X - distance, 0), Math.Max(pt.Y - distance, 0));
			int maxPos = PosData.CoordsToPos(Math.Min(pt.X + distance, Main.maxTilesX - 1), Math.Min(pt.Y + distance, Main.maxTilesY - 1));
			if (posMap[0].pos > maxPos || posMap[posMap.Length - 1].pos < minPos)
			{
				return false;
			}
			int num = Math.Max(posMap.FindIndex(minPos), 0);
			int maximum = Math.Max(posMap.FindIndex(maxPos), 0);
			int bestSqDist = distance * distance + 1;
			for (int i = num; i < maximum; i++)
			{
				PosData<T> posData = posMap[i];
				int dy = posData.Y - pt.Y;
				if (dy >= -distance && dy <= distance)
				{
					int num2 = posData.X - pt.X;
					int sqDist = num2 * num2 + dy * dy;
					if (sqDist < bestSqDist)
					{
						bestSqDist = sqDist;
						entry = posData;
					}
				}
			}
			return entry.pos >= 0;
		}
	}
}
