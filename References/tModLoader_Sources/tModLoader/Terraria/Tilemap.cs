using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria
{
	// Token: 0x02000064 RID: 100
	public readonly struct Tilemap
	{
		// Token: 0x17000233 RID: 563
		public Tile this[int x, int y]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
			get
			{
				if (x >= (int)this.Width || y >= (int)this.Height)
				{
					throw new IndexOutOfRangeException();
				}
				return new Tile((uint)(y + x * (int)this.Height));
			}
			internal set
			{
				throw new InvalidOperationException("Cannot set Tilemap tiles. Only used to init null tiles in Vanilla (which don't exist anymore)");
			}
		}

		// Token: 0x17000234 RID: 564
		public Tile this[Point pos]
		{
			get
			{
				return this[pos.X, pos.Y];
			}
		}

		// Token: 0x17000235 RID: 565
		public Tile this[Point16 pos]
		{
			get
			{
				return this[(int)pos.X, (int)pos.Y];
			}
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x003FD7CF File Offset: 0x003FB9CF
		internal Tilemap(ushort width, ushort height)
		{
			this.Width = width;
			this.Height = height;
			TileData.SetLength((uint)(width * height));
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x003FD7E7 File Offset: 0x003FB9E7
		public void ClearEverything()
		{
			TileData.ClearEverything();
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x003FD7EE File Offset: 0x003FB9EE
		public T[] GetData<[IsUnmanaged] T>() where T : struct, ValueType, ITileData
		{
			return TileData<T>.data;
		}

		// Token: 0x04000EDD RID: 3805
		public readonly ushort Width;

		// Token: 0x04000EDE RID: 3806
		public readonly ushort Height;
	}
}
