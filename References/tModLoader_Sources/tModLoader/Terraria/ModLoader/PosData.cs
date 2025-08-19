using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terraria.ModLoader
{
	// Token: 0x020001EA RID: 490
	public struct PosData<T>
	{
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x004FDFBC File Offset: 0x004FC1BC
		public int X
		{
			get
			{
				return this.pos / Main.maxTilesY;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06002680 RID: 9856 RVA: 0x004FDFCA File Offset: 0x004FC1CA
		public int Y
		{
			get
			{
				return this.pos % Main.maxTilesY;
			}
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x004FDFD8 File Offset: 0x004FC1D8
		public PosData(int pos, T value)
		{
			this.pos = pos;
			this.value = value;
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x004FDFE8 File Offset: 0x004FC1E8
		public PosData(int x, int y, T value)
		{
			this = new PosData<T>(PosData.CoordsToPos(x, y), value);
		}

		// Token: 0x04001862 RID: 6242
		public readonly int pos;

		// Token: 0x04001863 RID: 6243
		public T value;

		// Token: 0x04001864 RID: 6244
		public static PosData<T> nullPosData = new PosData<T>(-1, default(T));

		/// <summary>
		/// Efficient builder for <see cref="T:Terraria.ModLoader.PosData`1" />[] lookups covering the whole world.
		/// Must add elements in ascending pos order.
		/// </summary>
		// Token: 0x020009B2 RID: 2482
		public class OrderedSparseLookupBuilder
		{
			/// <summary>
			/// Use <paramref name="compressEqualValues" /> to produce a smaller lookup which won't work with <see cref="M:Terraria.ModLoader.PosData.LookupExact``1(Terraria.ModLoader.PosData{``0}[],System.Int32,System.Int32,``0@)" />
			/// When using <paramref name="compressEqualValues" /> without <paramref name="insertDefaultEntries" />,
			/// unspecified positions will default to the value of the previous specified position
			/// </summary>
			/// <param name="capacity">Defaults to 1M entries to reduce reallocations. Final built collection will be smaller. </param>
			/// <param name="compressEqualValues">Reduces the size of the map, but gives unspecified positions a value.</param>
			/// <param name="insertDefaultEntries">Ensures unspecified positions are assigned a default value when used with <paramref name="compressEqualValues" /></param>
			// Token: 0x060055E8 RID: 21992 RVA: 0x0069C13F File Offset: 0x0069A33F
			public OrderedSparseLookupBuilder(int capacity = 1048576, bool compressEqualValues = true, bool insertDefaultEntries = false)
			{
				this.list = new List<PosData<T>>(capacity);
				this.last = PosData<T>.nullPosData;
				this.compressEqualValues = compressEqualValues;
				this.insertDefaultEntries = insertDefaultEntries;
			}

			// Token: 0x060055E9 RID: 21993 RVA: 0x0069C16C File Offset: 0x0069A36C
			public void Add(int x, int y, T value)
			{
				this.Add(PosData.CoordsToPos(x, y), value);
			}

			// Token: 0x060055EA RID: 21994 RVA: 0x0069C17C File Offset: 0x0069A37C
			public void Add(int pos, T value)
			{
				if (pos <= this.last.pos)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(50, 2);
					defaultInterpolatedStringHandler.AppendLiteral("Must build in ascending index order. Prev: ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(this.last.pos);
					defaultInterpolatedStringHandler.AppendLiteral(", pos: ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(pos);
					throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				if (this.compressEqualValues)
				{
					if (this.insertDefaultEntries && pos >= this.last.pos + 2)
					{
						this.Add(this.last.pos + 1, default(T));
					}
					if (EqualityComparer<T>.Default.Equals(value, this.last.value))
					{
						return;
					}
				}
				this.list.Add(this.last = new PosData<T>(pos, value));
			}

			// Token: 0x060055EB RID: 21995 RVA: 0x0069C250 File Offset: 0x0069A450
			public PosData<T>[] Build()
			{
				return this.list.ToArray();
			}

			// Token: 0x04006B9C RID: 27548
			private readonly List<PosData<T>> list;

			// Token: 0x04006B9D RID: 27549
			private readonly bool compressEqualValues;

			// Token: 0x04006B9E RID: 27550
			private readonly bool insertDefaultEntries;

			// Token: 0x04006B9F RID: 27551
			private PosData<T> last;
		}

		// Token: 0x020009B3 RID: 2483
		public class OrderedSparseLookupReader
		{
			// Token: 0x060055EC RID: 21996 RVA: 0x0069C25D File Offset: 0x0069A45D
			public OrderedSparseLookupReader(PosData<T>[] data, bool hasEqualValueCompression = true)
			{
				this.data = data;
				this.hasEqualValueCompression = hasEqualValueCompression;
				this.current = PosData<T>.nullPosData;
				this.nextIdx = 0;
			}

			// Token: 0x060055ED RID: 21997 RVA: 0x0069C285 File Offset: 0x0069A485
			public T Get(int x, int y)
			{
				return this.Get(PosData.CoordsToPos(x, y));
			}

			// Token: 0x060055EE RID: 21998 RVA: 0x0069C294 File Offset: 0x0069A494
			public T Get(int pos)
			{
				if (pos <= this.current.pos)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(49, 2);
					defaultInterpolatedStringHandler.AppendLiteral("Must read in ascending index order. Prev: ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(this.current.pos);
					defaultInterpolatedStringHandler.AppendLiteral(", pos: ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(pos);
					throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				while (this.nextIdx < this.data.Length && this.data[this.nextIdx].pos <= pos)
				{
					PosData<T>[] array = this.data;
					int num = this.nextIdx;
					this.nextIdx = num + 1;
					this.current = array[num];
				}
				if (!this.hasEqualValueCompression && this.current.pos != pos)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 3);
					defaultInterpolatedStringHandler.AppendLiteral("Position does not exist in map. ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(pos);
					defaultInterpolatedStringHandler.AppendLiteral(" (X: ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(this.current.X);
					defaultInterpolatedStringHandler.AppendLiteral(", Y: ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(this.current.Y);
					defaultInterpolatedStringHandler.AppendLiteral(")");
					throw new KeyNotFoundException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				return this.current.value;
			}

			// Token: 0x04006BA0 RID: 27552
			private readonly PosData<T>[] data;

			// Token: 0x04006BA1 RID: 27553
			private readonly bool hasEqualValueCompression;

			// Token: 0x04006BA2 RID: 27554
			private PosData<T> current;

			// Token: 0x04006BA3 RID: 27555
			private int nextIdx;
		}
	}
}
