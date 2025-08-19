using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x020002CC RID: 716
	public class MarbleBiome : MicroBiome
	{
		// Token: 0x060022BA RID: 8890 RVA: 0x00547678 File Offset: 0x00545878
		private void SmoothSlope(int x, int y)
		{
			MarbleBiome.Slab slab = this._slabs[x, y];
			if (!slab.IsSolid)
			{
				return;
			}
			bool isSolid = this._slabs[x, y - 1].IsSolid;
			bool isSolid2 = this._slabs[x, y + 1].IsSolid;
			bool isSolid3 = this._slabs[x - 1, y].IsSolid;
			bool isSolid4 = this._slabs[x + 1, y].IsSolid;
			switch ((isSolid ? 1 : 0) << 3 | (isSolid2 ? 1 : 0) << 2 | (isSolid3 ? 1 : 0) << 1 | (isSolid4 ? 1 : 0))
			{
			case 4:
				this._slabs[x, y] = slab.WithState(new MarbleBiome.SlabState(MarbleBiome.SlabStates.HalfBrick));
				return;
			case 5:
				this._slabs[x, y] = slab.WithState(new MarbleBiome.SlabState(MarbleBiome.SlabStates.BottomRightFilled));
				return;
			case 6:
				this._slabs[x, y] = slab.WithState(new MarbleBiome.SlabState(MarbleBiome.SlabStates.BottomLeftFilled));
				return;
			case 9:
				this._slabs[x, y] = slab.WithState(new MarbleBiome.SlabState(MarbleBiome.SlabStates.TopRightFilled));
				return;
			case 10:
				this._slabs[x, y] = slab.WithState(new MarbleBiome.SlabState(MarbleBiome.SlabStates.TopLeftFilled));
				return;
			}
			this._slabs[x, y] = slab.WithState(new MarbleBiome.SlabState(MarbleBiome.SlabStates.Solid));
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x00547808 File Offset: 0x00545A08
		private void PlaceSlab(MarbleBiome.Slab slab, int originX, int originY, int scale)
		{
			ushort num = 367;
			ushort wall = 178;
			if (WorldGen.drunkWorldGen)
			{
				num = 368;
				wall = 180;
			}
			int num2 = -1;
			int num3 = scale + 1;
			int num4 = 0;
			int num5 = scale;
			for (int i = num2; i < num3; i++)
			{
				if ((i != num2 && i != num3 - 1) || WorldGen.genRand.Next(2) != 0)
				{
					if (WorldGen.genRand.Next(2) == 0)
					{
						num4--;
					}
					if (WorldGen.genRand.Next(2) == 0)
					{
						num5++;
					}
					for (int j = num4; j < num5; j++)
					{
						Tile tile = GenBase._tiles[originX + i, originY + j];
						tile.ResetToType(TileID.Sets.Ore[(int)tile.type] ? tile.type : num);
						bool active = slab.State(i, j, scale);
						tile.active(active);
						if (slab.HasWall)
						{
							tile.wall = wall;
						}
						WorldUtils.TileFrame(originX + i, originY + j, true);
						WorldGen.SquareWallFrame(originX + i, originY + j, true);
						Tile.SmoothSlope(originX + i, originY + j, true, false);
						if (WorldGen.SolidTile(originX + i, originY + j - 1, false) && GenBase._random.Next(4) == 0)
						{
							WorldGen.PlaceTight(originX + i, originY + j, false);
						}
						if (WorldGen.SolidTile(originX + i, originY + j, false) && GenBase._random.Next(4) == 0)
						{
							WorldGen.PlaceTight(originX + i, originY + j - 1, false);
						}
					}
				}
			}
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x00547998 File Offset: 0x00545B98
		private static bool IsGroupSolid(int x, int y, int scale)
		{
			int num = 0;
			for (int i = 0; i < scale; i++)
			{
				for (int j = 0; j < scale; j++)
				{
					if (WorldGen.SolidOrSlopedTile(x + i, y + j))
					{
						num++;
					}
				}
			}
			return num > scale / 4 * 3;
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x005479D8 File Offset: 0x00545BD8
		public override bool Place(Point origin, StructureMap structures)
		{
			if (WorldGen.BiomeTileCheck(origin.X, origin.Y))
			{
				return false;
			}
			if (this._slabs == null)
			{
				this._slabs = new MarbleBiome.Slab[56, 26];
			}
			int num = GenBase._random.Next(80, 150) / 3;
			int num2 = GenBase._random.Next(40, 60) / 3;
			int num3 = (num2 * 3 - GenBase._random.Next(20, 30)) / 3;
			origin.X -= num * 3 / 2;
			origin.Y -= num2 * 3 / 2;
			for (int i = -1; i < num + 1; i++)
			{
				double num4 = (double)(i - num / 2) / (double)num + 0.5;
				int num5 = (int)((0.5 - Math.Abs(num4 - 0.5)) * 5.0) - 2;
				for (int j = -1; j < num2 + 1; j++)
				{
					bool hasWall = true;
					bool flag = false;
					bool flag2 = MarbleBiome.IsGroupSolid(i * 3 + origin.X, j * 3 + origin.Y, 3);
					int num6 = Math.Abs(j - num2 / 2) - num3 / 4 + num5;
					if (num6 > 3)
					{
						flag = flag2;
						hasWall = false;
					}
					else if (num6 > 0)
					{
						flag = (j - num2 / 2 > 0 || flag2);
						hasWall = (j - num2 / 2 < 0 || num6 <= 2);
					}
					else if (num6 == 0)
					{
						flag = (GenBase._random.Next(2) == 0 && (j - num2 / 2 > 0 || flag2));
					}
					if (Math.Abs(num4 - 0.5) > 0.35 + GenBase._random.NextDouble() * 0.1 && !flag2)
					{
						hasWall = false;
						flag = false;
					}
					this._slabs[i + 1, j + 1] = MarbleBiome.Slab.Create(flag ? new MarbleBiome.SlabState(MarbleBiome.SlabStates.Solid) : new MarbleBiome.SlabState(MarbleBiome.SlabStates.Empty), hasWall);
				}
			}
			for (int k = 0; k < num; k++)
			{
				for (int l = 0; l < num2; l++)
				{
					this.SmoothSlope(k + 1, l + 1);
				}
			}
			int num7 = num / 2;
			int num8 = num2 / 2;
			int num9 = (num8 + 1) * (num8 + 1);
			double value = GenBase._random.NextDouble() * 2.0 - 1.0;
			double num10 = GenBase._random.NextDouble() * 2.0 - 1.0;
			double value2 = GenBase._random.NextDouble() * 2.0 - 1.0;
			double num11 = 0.0;
			for (int m = 0; m <= num; m++)
			{
				double num12 = (double)num8 / (double)num7 * (double)(m - num7);
				int num13 = Math.Min(num8, (int)Math.Sqrt(Math.Max(0.0, (double)num9 - num12 * num12)));
				if (m < num / 2)
				{
					num11 += Utils.Lerp(value, num10, (double)m / (double)(num / 2));
				}
				else
				{
					num11 += Utils.Lerp(num10, value2, (double)m / (double)(num / 2) - 1.0);
				}
				for (int n = num8 - num13; n <= num8 + num13; n++)
				{
					this.PlaceSlab(this._slabs[m + 1, n + 1], m * 3 + origin.X, n * 3 + origin.Y + (int)num11, 3);
				}
			}
			structures.AddStructure(new Rectangle(origin.X, origin.Y, num * 3, num2 * 3), 8);
			return true;
		}

		// Token: 0x040047E5 RID: 18405
		private const int SCALE = 3;

		// Token: 0x040047E6 RID: 18406
		private MarbleBiome.Slab[,] _slabs;

		// Token: 0x020006CD RID: 1741
		// (Invoke) Token: 0x0600368F RID: 13967
		private delegate bool SlabState(int x, int y, int scale);

		// Token: 0x020006CE RID: 1742
		private static class SlabStates
		{
			// Token: 0x06003692 RID: 13970 RVA: 0x0048E5F6 File Offset: 0x0048C7F6
			public static bool Empty(int x, int y, int scale)
			{
				return false;
			}

			// Token: 0x06003693 RID: 13971 RVA: 0x0003266D File Offset: 0x0003086D
			public static bool Solid(int x, int y, int scale)
			{
				return true;
			}

			// Token: 0x06003694 RID: 13972 RVA: 0x0060C969 File Offset: 0x0060AB69
			public static bool HalfBrick(int x, int y, int scale)
			{
				return y >= scale / 2;
			}

			// Token: 0x06003695 RID: 13973 RVA: 0x0060C974 File Offset: 0x0060AB74
			public static bool BottomRightFilled(int x, int y, int scale)
			{
				return x >= scale - y;
			}

			// Token: 0x06003696 RID: 13974 RVA: 0x0060C97F File Offset: 0x0060AB7F
			public static bool BottomLeftFilled(int x, int y, int scale)
			{
				return x < y;
			}

			// Token: 0x06003697 RID: 13975 RVA: 0x0060C985 File Offset: 0x0060AB85
			public static bool TopRightFilled(int x, int y, int scale)
			{
				return x > y;
			}

			// Token: 0x06003698 RID: 13976 RVA: 0x0060C98B File Offset: 0x0060AB8B
			public static bool TopLeftFilled(int x, int y, int scale)
			{
				return x < scale - y;
			}
		}

		// Token: 0x020006CF RID: 1743
		private struct Slab
		{
			// Token: 0x170003F4 RID: 1012
			// (get) Token: 0x06003699 RID: 13977 RVA: 0x0060C993 File Offset: 0x0060AB93
			public bool IsSolid
			{
				get
				{
					return this.State != new MarbleBiome.SlabState(MarbleBiome.SlabStates.Empty);
				}
			}

			// Token: 0x0600369A RID: 13978 RVA: 0x0060C9AC File Offset: 0x0060ABAC
			private Slab(MarbleBiome.SlabState state, bool hasWall)
			{
				this.State = state;
				this.HasWall = hasWall;
			}

			// Token: 0x0600369B RID: 13979 RVA: 0x0060C9BC File Offset: 0x0060ABBC
			public MarbleBiome.Slab WithState(MarbleBiome.SlabState state)
			{
				return new MarbleBiome.Slab(state, this.HasWall);
			}

			// Token: 0x0600369C RID: 13980 RVA: 0x0060C9CA File Offset: 0x0060ABCA
			public static MarbleBiome.Slab Create(MarbleBiome.SlabState state, bool hasWall)
			{
				return new MarbleBiome.Slab(state, hasWall);
			}

			// Token: 0x0400622F RID: 25135
			public readonly MarbleBiome.SlabState State;

			// Token: 0x04006230 RID: 25136
			public readonly bool HasWall;
		}
	}
}
