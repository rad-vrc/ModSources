using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	// Token: 0x02000736 RID: 1846
	public class TileDrawSorter
	{
		// Token: 0x06004AE7 RID: 19175 RVA: 0x00668CD7 File Offset: 0x00666ED7
		public TileDrawSorter()
		{
			this._currentCacheIndex = 0;
			this._holderLength = 9000;
			this.tilesToDraw = new TileDrawSorter.TileTexPoint[this._holderLength];
		}

		// Token: 0x06004AE8 RID: 19176 RVA: 0x00668D0D File Offset: 0x00666F0D
		public void reset()
		{
			this._currentCacheIndex = 0;
		}

		// Token: 0x06004AE9 RID: 19177 RVA: 0x00668D18 File Offset: 0x00666F18
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

		// Token: 0x06004AEA RID: 19178 RVA: 0x00668D81 File Offset: 0x00666F81
		private void IncreaseArraySize()
		{
			this._holderLength *= 2;
			Array.Resize<TileDrawSorter.TileTexPoint>(ref this.tilesToDraw, this._holderLength);
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x00668DA2 File Offset: 0x00666FA2
		public void Sort()
		{
			Array.Sort<TileDrawSorter.TileTexPoint>(this.tilesToDraw, 0, this._currentCacheIndex, this._tileComparer);
		}

		// Token: 0x06004AEC RID: 19180 RVA: 0x00668DBC File Offset: 0x00666FBC
		public int GetAmountToDraw()
		{
			return this._currentCacheIndex;
		}

		// Token: 0x04006033 RID: 24627
		public TileDrawSorter.TileTexPoint[] tilesToDraw;

		// Token: 0x04006034 RID: 24628
		private int _holderLength;

		// Token: 0x04006035 RID: 24629
		private int _currentCacheIndex;

		// Token: 0x04006036 RID: 24630
		private TileDrawSorter.CustomComparer _tileComparer = new TileDrawSorter.CustomComparer();

		// Token: 0x02000D62 RID: 3426
		public struct TileTexPoint
		{
			// Token: 0x060063FB RID: 25595 RVA: 0x006D9C18 File Offset: 0x006D7E18
			public override string ToString()
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(13, 3);
				defaultInterpolatedStringHandler.AppendLiteral("X:");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.X);
				defaultInterpolatedStringHandler.AppendLiteral(", Y:");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.Y);
				defaultInterpolatedStringHandler.AppendLiteral(", Type:");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this.TileType);
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}

			// Token: 0x04007B93 RID: 31635
			public int X;

			// Token: 0x04007B94 RID: 31636
			public int Y;

			// Token: 0x04007B95 RID: 31637
			public int TileType;
		}

		// Token: 0x02000D63 RID: 3427
		public class CustomComparer : Comparer<TileDrawSorter.TileTexPoint>
		{
			// Token: 0x060063FC RID: 25596 RVA: 0x006D9C81 File Offset: 0x006D7E81
			public override int Compare(TileDrawSorter.TileTexPoint x, TileDrawSorter.TileTexPoint y)
			{
				return x.TileType.CompareTo(y.TileType);
			}
		}
	}
}
