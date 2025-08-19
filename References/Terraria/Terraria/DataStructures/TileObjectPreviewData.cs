using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200045D RID: 1117
	public class TileObjectPreviewData
	{
		// Token: 0x06002CB9 RID: 11449 RVA: 0x005BBC58 File Offset: 0x005B9E58
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

		// Token: 0x06002CBA RID: 11450 RVA: 0x005BBCE0 File Offset: 0x005B9EE0
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

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06002CBB RID: 11451 RVA: 0x005BBE99 File Offset: 0x005BA099
		// (set) Token: 0x06002CBC RID: 11452 RVA: 0x005BBEA1 File Offset: 0x005BA0A1
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

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06002CBD RID: 11453 RVA: 0x005BBEAA File Offset: 0x005BA0AA
		// (set) Token: 0x06002CBE RID: 11454 RVA: 0x005BBEB2 File Offset: 0x005BA0B2
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

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06002CBF RID: 11455 RVA: 0x005BBEBB File Offset: 0x005BA0BB
		// (set) Token: 0x06002CC0 RID: 11456 RVA: 0x005BBEC3 File Offset: 0x005BA0C3
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

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06002CC1 RID: 11457 RVA: 0x005BBECC File Offset: 0x005BA0CC
		// (set) Token: 0x06002CC2 RID: 11458 RVA: 0x005BBED4 File Offset: 0x005BA0D4
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

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06002CC3 RID: 11459 RVA: 0x005BBEDD File Offset: 0x005BA0DD
		// (set) Token: 0x06002CC4 RID: 11460 RVA: 0x005BBEE5 File Offset: 0x005BA0E5
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

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06002CC5 RID: 11461 RVA: 0x005BBEEE File Offset: 0x005BA0EE
		// (set) Token: 0x06002CC6 RID: 11462 RVA: 0x005BBEF8 File Offset: 0x005BA0F8
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

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06002CC7 RID: 11463 RVA: 0x005BC00B File Offset: 0x005BA20B
		// (set) Token: 0x06002CC8 RID: 11464 RVA: 0x005BC013 File Offset: 0x005BA213
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

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06002CC9 RID: 11465 RVA: 0x005BC01C File Offset: 0x005BA21C
		// (set) Token: 0x06002CCA RID: 11466 RVA: 0x005BC024 File Offset: 0x005BA224
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

		// Token: 0x06002CCB RID: 11467 RVA: 0x005BC030 File Offset: 0x005BA230
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

		// Token: 0x1700036C RID: 876
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

		// Token: 0x04005104 RID: 20740
		private ushort _type;

		// Token: 0x04005105 RID: 20741
		private short _style;

		// Token: 0x04005106 RID: 20742
		private int _alternate;

		// Token: 0x04005107 RID: 20743
		private int _random;

		// Token: 0x04005108 RID: 20744
		private bool _active;

		// Token: 0x04005109 RID: 20745
		private Point16 _size;

		// Token: 0x0400510A RID: 20746
		private Point16 _coordinates;

		// Token: 0x0400510B RID: 20747
		private Point16 _objectStart;

		// Token: 0x0400510C RID: 20748
		private int[,] _data;

		// Token: 0x0400510D RID: 20749
		private Point16 _dataSize;

		// Token: 0x0400510E RID: 20750
		private float _percentValid;

		// Token: 0x0400510F RID: 20751
		public static TileObjectPreviewData placementCache;

		// Token: 0x04005110 RID: 20752
		public static TileObjectPreviewData randomCache;

		// Token: 0x04005111 RID: 20753
		public const int None = 0;

		// Token: 0x04005112 RID: 20754
		public const int ValidSpot = 1;

		// Token: 0x04005113 RID: 20755
		public const int InvalidSpot = 2;
	}
}
