using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x0200065C RID: 1628
	public class MarbleBiome : MicroBiome
	{
		// Token: 0x06004700 RID: 18176 RVA: 0x00637E74 File Offset: 0x00636074
		private void SmoothSlope(int x, int y)
		{
			MarbleBiome.Slab slab = this._slabs[x, y];
			if (slab.IsSolid)
			{
				int isSolid5 = this._slabs[x, y - 1].IsSolid ? 1 : 0;
				bool isSolid2 = this._slabs[x, y + 1].IsSolid;
				bool isSolid3 = this._slabs[x - 1, y].IsSolid;
				bool isSolid4 = this._slabs[x + 1, y].IsSolid;
				switch (((isSolid5 != 0) << 3 | (isSolid2 > false) << 2 | (isSolid3 > false) << 1 | isSolid4 > false) ? 1 : 0)
				{
				case 4:
				{
					MarbleBiome.Slab[,] slabs = this._slabs;
					MarbleBiome.SlabState state;
					if ((state = MarbleBiome.<>O.<4>__HalfBrick) == null)
					{
						state = (MarbleBiome.<>O.<4>__HalfBrick = new MarbleBiome.SlabState(MarbleBiome.SlabStates.HalfBrick));
					}
					slabs[x, y] = slab.WithState(state);
					return;
				}
				case 5:
				{
					MarbleBiome.Slab[,] slabs2 = this._slabs;
					MarbleBiome.SlabState state2;
					if ((state2 = MarbleBiome.<>O.<3>__BottomRightFilled) == null)
					{
						state2 = (MarbleBiome.<>O.<3>__BottomRightFilled = new MarbleBiome.SlabState(MarbleBiome.SlabStates.BottomRightFilled));
					}
					slabs2[x, y] = slab.WithState(state2);
					return;
				}
				case 6:
				{
					MarbleBiome.Slab[,] slabs3 = this._slabs;
					MarbleBiome.SlabState state3;
					if ((state3 = MarbleBiome.<>O.<2>__BottomLeftFilled) == null)
					{
						state3 = (MarbleBiome.<>O.<2>__BottomLeftFilled = new MarbleBiome.SlabState(MarbleBiome.SlabStates.BottomLeftFilled));
					}
					slabs3[x, y] = slab.WithState(state3);
					return;
				}
				case 9:
				{
					MarbleBiome.Slab[,] slabs4 = this._slabs;
					MarbleBiome.SlabState state4;
					if ((state4 = MarbleBiome.<>O.<1>__TopRightFilled) == null)
					{
						state4 = (MarbleBiome.<>O.<1>__TopRightFilled = new MarbleBiome.SlabState(MarbleBiome.SlabStates.TopRightFilled));
					}
					slabs4[x, y] = slab.WithState(state4);
					return;
				}
				case 10:
				{
					MarbleBiome.Slab[,] slabs5 = this._slabs;
					MarbleBiome.SlabState state5;
					if ((state5 = MarbleBiome.<>O.<0>__TopLeftFilled) == null)
					{
						state5 = (MarbleBiome.<>O.<0>__TopLeftFilled = new MarbleBiome.SlabState(MarbleBiome.SlabStates.TopLeftFilled));
					}
					slabs5[x, y] = slab.WithState(state5);
					return;
				}
				}
				MarbleBiome.Slab[,] slabs6 = this._slabs;
				MarbleBiome.SlabState state6;
				if ((state6 = MarbleBiome.<>O.<5>__Solid) == null)
				{
					state6 = (MarbleBiome.<>O.<5>__Solid = new MarbleBiome.SlabState(MarbleBiome.SlabStates.Solid));
				}
				slabs6[x, y] = slab.WithState(state6);
			}
		}

		// Token: 0x06004701 RID: 18177 RVA: 0x00638054 File Offset: 0x00636254
		private unsafe void PlaceSlab(MarbleBiome.Slab slab, int originX, int originY, int scale)
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
						tile.ResetToType(TileID.Sets.Ore[(int)(*tile.type)] ? (*tile.type) : num);
						bool active = slab.State(i, j, scale);
						tile.active(active);
						if (slab.HasWall)
						{
							*tile.wall = wall;
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

		// Token: 0x06004702 RID: 18178 RVA: 0x006381E8 File Offset: 0x006363E8
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

		// Token: 0x06004703 RID: 18179 RVA: 0x00638228 File Offset: 0x00636428
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
				num11 = ((m >= num / 2) ? (num11 + Utils.Lerp(num10, value2, (double)m / (double)(num / 2) - 1.0)) : (num11 + Utils.Lerp(value, num10, (double)m / (double)(num / 2))));
				for (int n = num8 - num13; n <= num8 + num13; n++)
				{
					this.PlaceSlab(this._slabs[m + 1, n + 1], m * 3 + origin.X, n * 3 + origin.Y + (int)num11, 3);
				}
			}
			structures.AddStructure(new Rectangle(origin.X, origin.Y, num * 3, num2 * 3), 8);
			return true;
		}

		// Token: 0x04005BAA RID: 23466
		private const int SCALE = 3;

		// Token: 0x04005BAB RID: 23467
		private MarbleBiome.Slab[,] _slabs;

		// Token: 0x02000D11 RID: 3345
		// (Invoke) Token: 0x060062FB RID: 25339
		private delegate bool SlabState(int x, int y, int scale);

		// Token: 0x02000D12 RID: 3346
		private static class SlabStates
		{
			// Token: 0x060062FE RID: 25342 RVA: 0x006D785D File Offset: 0x006D5A5D
			public static bool Empty(int x, int y, int scale)
			{
				return false;
			}

			// Token: 0x060062FF RID: 25343 RVA: 0x006D7860 File Offset: 0x006D5A60
			public static bool Solid(int x, int y, int scale)
			{
				return true;
			}

			// Token: 0x06006300 RID: 25344 RVA: 0x006D7863 File Offset: 0x006D5A63
			public static bool HalfBrick(int x, int y, int scale)
			{
				return y >= scale / 2;
			}

			// Token: 0x06006301 RID: 25345 RVA: 0x006D786E File Offset: 0x006D5A6E
			public static bool BottomRightFilled(int x, int y, int scale)
			{
				return x >= scale - y;
			}

			// Token: 0x06006302 RID: 25346 RVA: 0x006D7879 File Offset: 0x006D5A79
			public static bool BottomLeftFilled(int x, int y, int scale)
			{
				return x < y;
			}

			// Token: 0x06006303 RID: 25347 RVA: 0x006D787F File Offset: 0x006D5A7F
			public static bool TopRightFilled(int x, int y, int scale)
			{
				return x > y;
			}

			// Token: 0x06006304 RID: 25348 RVA: 0x006D7885 File Offset: 0x006D5A85
			public static bool TopLeftFilled(int x, int y, int scale)
			{
				return x < scale - y;
			}
		}

		// Token: 0x02000D13 RID: 3347
		private struct Slab
		{
			// Token: 0x1700098F RID: 2447
			// (get) Token: 0x06006305 RID: 25349 RVA: 0x006D788D File Offset: 0x006D5A8D
			public bool IsSolid
			{
				get
				{
					return this.State != new MarbleBiome.SlabState(MarbleBiome.SlabStates.Empty);
				}
			}

			// Token: 0x06006306 RID: 25350 RVA: 0x006D78A6 File Offset: 0x006D5AA6
			private Slab(MarbleBiome.SlabState state, bool hasWall)
			{
				this.State = state;
				this.HasWall = hasWall;
			}

			// Token: 0x06006307 RID: 25351 RVA: 0x006D78B6 File Offset: 0x006D5AB6
			public MarbleBiome.Slab WithState(MarbleBiome.SlabState state)
			{
				return new MarbleBiome.Slab(state, this.HasWall);
			}

			// Token: 0x06006308 RID: 25352 RVA: 0x006D78C4 File Offset: 0x006D5AC4
			public static MarbleBiome.Slab Create(MarbleBiome.SlabState state, bool hasWall)
			{
				return new MarbleBiome.Slab(state, hasWall);
			}

			// Token: 0x04007ABA RID: 31418
			public readonly MarbleBiome.SlabState State;

			// Token: 0x04007ABB RID: 31419
			public readonly bool HasWall;
		}

		// Token: 0x02000D14 RID: 3348
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04007ABC RID: 31420
			public static MarbleBiome.SlabState <0>__TopLeftFilled;

			// Token: 0x04007ABD RID: 31421
			public static MarbleBiome.SlabState <1>__TopRightFilled;

			// Token: 0x04007ABE RID: 31422
			public static MarbleBiome.SlabState <2>__BottomLeftFilled;

			// Token: 0x04007ABF RID: 31423
			public static MarbleBiome.SlabState <3>__BottomRightFilled;

			// Token: 0x04007AC0 RID: 31424
			public static MarbleBiome.SlabState <4>__HalfBrick;

			// Token: 0x04007AC1 RID: 31425
			public static MarbleBiome.SlabState <5>__Solid;
		}
	}
}
