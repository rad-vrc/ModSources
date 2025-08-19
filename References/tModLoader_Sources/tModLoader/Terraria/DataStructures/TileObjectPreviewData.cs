using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000739 RID: 1849
	public class TileObjectPreviewData
	{
		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06004B1F RID: 19231 RVA: 0x00669489 File Offset: 0x00667689
		// (set) Token: 0x06004B20 RID: 19232 RVA: 0x00669491 File Offset: 0x00667691
		public bool Active
		{
			get
			{
				return this._active;
			}
			set
			{
				this._active = value;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06004B21 RID: 19233 RVA: 0x0066949A File Offset: 0x0066769A
		// (set) Token: 0x06004B22 RID: 19234 RVA: 0x006694A2 File Offset: 0x006676A2
		public ushort Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06004B23 RID: 19235 RVA: 0x006694AB File Offset: 0x006676AB
		// (set) Token: 0x06004B24 RID: 19236 RVA: 0x006694B3 File Offset: 0x006676B3
		public short Style
		{
			get
			{
				return this._style;
			}
			set
			{
				this._style = value;
			}
		}

		/// <inheritdoc cref="F:Terraria.TileObject.alternate" />
		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06004B25 RID: 19237 RVA: 0x006694BC File Offset: 0x006676BC
		// (set) Token: 0x06004B26 RID: 19238 RVA: 0x006694C4 File Offset: 0x006676C4
		public int Alternate
		{
			get
			{
				return this._alternate;
			}
			set
			{
				this._alternate = value;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06004B27 RID: 19239 RVA: 0x006694CD File Offset: 0x006676CD
		// (set) Token: 0x06004B28 RID: 19240 RVA: 0x006694D5 File Offset: 0x006676D5
		public int Random
		{
			get
			{
				return this._random;
			}
			set
			{
				this._random = value;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06004B29 RID: 19241 RVA: 0x006694DE File Offset: 0x006676DE
		// (set) Token: 0x06004B2A RID: 19242 RVA: 0x006694E8 File Offset: 0x006676E8
		public Point16 Size
		{
			get
			{
				return this._size;
			}
			set
			{
				if (value.X <= 0 || value.Y <= 0)
				{
					throw new FormatException("PlacementData.Size was set to a negative value.");
				}
				if (value.X > this._dataSize.X || value.Y > this._dataSize.Y)
				{
					int num = (int)((value.X > this._dataSize.X) ? value.X : this._dataSize.X);
					int num2 = (int)((value.Y > this._dataSize.Y) ? value.Y : this._dataSize.Y);
					int[,] array = new int[num, num2];
					if (this._data != null)
					{
						for (int i = 0; i < (int)this._dataSize.X; i++)
						{
							for (int j = 0; j < (int)this._dataSize.Y; j++)
							{
								array[i, j] = this._data[i, j];
							}
						}
					}
					this._data = array;
					this._dataSize = new Point16(num, num2);
				}
				this._size = value;
			}
		}

		/// <summary>
		/// The top left tile coordinate of this preview. Not necessarily the top left of the tile itself since the dimensions of this object also include space for the anchors of the selected placement style. Add <see cref="P:Terraria.DataStructures.TileObjectPreviewData.ObjectStart" /> to get the top left tile.
		/// <see cref="P:Terraria.DataStructures.TileObjectPreviewData.Item(System.Int32,System.Int32)" />
		/// </summary>
		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06004B2B RID: 19243 RVA: 0x006695FB File Offset: 0x006677FB
		// (set) Token: 0x06004B2C RID: 19244 RVA: 0x00669603 File Offset: 0x00667803
		public Point16 Coordinates
		{
			get
			{
				return this._coordinates;
			}
			set
			{
				this._coordinates = value;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06004B2D RID: 19245 RVA: 0x0066960C File Offset: 0x0066780C
		// (set) Token: 0x06004B2E RID: 19246 RVA: 0x00669614 File Offset: 0x00667814
		public Point16 ObjectStart
		{
			get
			{
				return this._objectStart;
			}
			set
			{
				this._objectStart = value;
			}
		}

		/// <summary>
		/// The placement validity data. Values of 0 are ignored, 1 means the tile or anchor at the tile coordinate offset by Coordinates is valid, and 2 means it is invalid.
		/// </summary>
		// Token: 0x17000840 RID: 2112
		public int this[int x, int y]
		{
			get
			{
				if (x < 0 || y < 0 || x >= (int)this._size.X || y >= (int)this._size.Y)
				{
					throw new IndexOutOfRangeException();
				}
				return this._data[x, y];
			}
			set
			{
				if (x < 0 || y < 0 || x >= (int)this._size.X || y >= (int)this._size.Y)
				{
					throw new IndexOutOfRangeException();
				}
				this._data[x, y] = value;
			}
		}

		// Token: 0x06004B31 RID: 19249 RVA: 0x00669690 File Offset: 0x00667890
		public void Reset()
		{
			this._active = false;
			this._size = Point16.Zero;
			this._coordinates = Point16.Zero;
			this._objectStart = Point16.Zero;
			this._percentValid = 0f;
			this._type = 0;
			this._style = 0;
			this._alternate = -1;
			this._random = -1;
			if (this._data != null)
			{
				Array.Clear(this._data, 0, (int)(this._dataSize.X * this._dataSize.Y));
			}
		}

		// Token: 0x06004B32 RID: 19250 RVA: 0x00669718 File Offset: 0x00667918
		public void CopyFrom(TileObjectPreviewData copy)
		{
			this._type = copy._type;
			this._style = copy._style;
			this._alternate = copy._alternate;
			this._random = copy._random;
			this._active = copy._active;
			this._size = copy._size;
			this._coordinates = copy._coordinates;
			this._objectStart = copy._objectStart;
			this._percentValid = copy._percentValid;
			if (this._data == null)
			{
				this._data = new int[(int)copy._dataSize.X, (int)copy._dataSize.Y];
				this._dataSize = copy._dataSize;
			}
			else
			{
				Array.Clear(this._data, 0, this._data.Length);
			}
			if (this._dataSize.X < copy._dataSize.X || this._dataSize.Y < copy._dataSize.Y)
			{
				int num = (int)((copy._dataSize.X > this._dataSize.X) ? copy._dataSize.X : this._dataSize.X);
				int num2 = (int)((copy._dataSize.Y > this._dataSize.Y) ? copy._dataSize.Y : this._dataSize.Y);
				this._data = new int[num, num2];
				this._dataSize = new Point16(num, num2);
			}
			for (int i = 0; i < (int)copy._dataSize.X; i++)
			{
				for (int j = 0; j < (int)copy._dataSize.Y; j++)
				{
					this._data[i, j] = copy._data[i, j];
				}
			}
		}

		// Token: 0x06004B33 RID: 19251 RVA: 0x006698D4 File Offset: 0x00667AD4
		public void AllInvalid()
		{
			for (int i = 0; i < (int)this._size.X; i++)
			{
				for (int j = 0; j < (int)this._size.Y; j++)
				{
					if (this._data[i, j] != 0)
					{
						this._data[i, j] = 2;
					}
				}
			}
		}

		// Token: 0x04006045 RID: 24645
		private ushort _type;

		// Token: 0x04006046 RID: 24646
		private short _style;

		// Token: 0x04006047 RID: 24647
		private int _alternate;

		// Token: 0x04006048 RID: 24648
		private int _random;

		// Token: 0x04006049 RID: 24649
		private bool _active;

		// Token: 0x0400604A RID: 24650
		private Point16 _size;

		// Token: 0x0400604B RID: 24651
		private Point16 _coordinates;

		// Token: 0x0400604C RID: 24652
		private Point16 _objectStart;

		// Token: 0x0400604D RID: 24653
		private int[,] _data;

		// Token: 0x0400604E RID: 24654
		private Point16 _dataSize;

		// Token: 0x0400604F RID: 24655
		private float _percentValid;

		// Token: 0x04006050 RID: 24656
		public static TileObjectPreviewData placementCache;

		// Token: 0x04006051 RID: 24657
		public static TileObjectPreviewData randomCache;

		// Token: 0x04006052 RID: 24658
		public const int None = 0;

		// Token: 0x04006053 RID: 24659
		public const int ValidSpot = 1;

		// Token: 0x04006054 RID: 24660
		public const int InvalidSpot = 2;
	}
}
