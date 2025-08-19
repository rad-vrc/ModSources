using System;
using System.Collections.Generic;

namespace Terraria.DataStructures
{
	// Token: 0x02000440 RID: 1088
	public class TileDrawSorter
	{
		// Token: 0x06002BA9 RID: 11177 RVA: 0x0059E9D2 File Offset: 0x0059CBD2
		public TileDrawSorter()
		{
			this._currentCacheIndex = 0;
			this._holderLength = 9000;
			this.tilesToDraw = new TileDrawSorter.TileTexPoint[this._holderLength];
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x0059EA08 File Offset: 0x0059CC08
		public void reset()
		{
			this._currentCacheIndex = 0;
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x0059EA14 File Offset: 0x0059CC14
		public void Cache(int x, int y, int type)
		{
			int currentCacheIndex = this._currentCacheIndex;
			this._currentCacheIndex = currentCacheIndex + 1;
			int num = currentCacheIndex;
			this.tilesToDraw[num].X = x;
			this.tilesToDraw[num].Y = y;
			this.tilesToDraw[num].TileType = type;
			if (this._currentCacheIndex == this._holderLength)
			{
				this.IncreaseArraySize();
			}
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x0059EA7D File Offset: 0x0059CC7D
		private void IncreaseArraySize()
		{
			this._holderLength *= 2;
			Array.Resize<TileDrawSorter.TileTexPoint>(ref this.tilesToDraw, this._holderLength);
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x0059EA9E File Offset: 0x0059CC9E
		public void Sort()
		{
			Array.Sort<TileDrawSorter.TileTexPoint>(this.tilesToDraw, 0, this._currentCacheIndex, this._tileComparer);
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x0059EAB8 File Offset: 0x0059CCB8
		public int GetAmountToDraw()
		{
			return this._currentCacheIndex;
		}

		// Token: 0x04004FD5 RID: 20437
		public TileDrawSorter.TileTexPoint[] tilesToDraw;

		// Token: 0x04004FD6 RID: 20438
		private int _holderLength;

		// Token: 0x04004FD7 RID: 20439
		private int _currentCacheIndex;

		// Token: 0x04004FD8 RID: 20440
		private TileDrawSorter.CustomComparer _tileComparer = new TileDrawSorter.CustomComparer();

		// Token: 0x0200077B RID: 1915
		public struct TileTexPoint
		{
			// Token: 0x06003917 RID: 14615 RVA: 0x00615388 File Offset: 0x00613588
			public override string ToString()
			{
				return string.Format("X:{0}, Y:{1}, Type:{2}", this.X, this.Y, this.TileType);
			}

			// Token: 0x04006499 RID: 25753
			public int X;

			// Token: 0x0400649A RID: 25754
			public int Y;

			// Token: 0x0400649B RID: 25755
			public int TileType;
		}

		// Token: 0x0200077C RID: 1916
		public class CustomComparer : Comparer<TileDrawSorter.TileTexPoint>
		{
			// Token: 0x06003918 RID: 14616 RVA: 0x006153B5 File Offset: 0x006135B5
			public override int Compare(TileDrawSorter.TileTexPoint x, TileDrawSorter.TileTexPoint y)
			{
				return x.TileType.CompareTo(y.TileType);
			}
		}
	}
}
