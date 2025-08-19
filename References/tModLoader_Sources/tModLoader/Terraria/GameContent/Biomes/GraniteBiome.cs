using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x02000657 RID: 1623
	public class GraniteBiome : MicroBiome
	{
		// Token: 0x060046DF RID: 18143 RVA: 0x00634C74 File Offset: 0x00632E74
		public static bool CanPlace(Point origin, StructureMap structures)
		{
			return !WorldGen.BiomeTileCheck(origin.X, origin.Y) && !GenBase._tiles[origin.X, origin.Y].active();
		}

		// Token: 0x060046E0 RID: 18144 RVA: 0x00634CB8 File Offset: 0x00632EB8
		public override bool Place(Point origin, StructureMap structures)
		{
			if (GenBase._tiles[origin.X, origin.Y].active())
			{
				return false;
			}
			origin.X -= this._sourceMagmaMap.GetLength(0) / 2;
			origin.Y -= this._sourceMagmaMap.GetLength(1) / 2;
			this.BuildMagmaMap(origin);
			Rectangle effectedMapArea;
			this.SimulatePressure(out effectedMapArea);
			this.PlaceGranite(origin, effectedMapArea);
			this.CleanupTiles(origin, effectedMapArea);
			this.PlaceDecorations(origin, effectedMapArea);
			structures.AddStructure(effectedMapArea.Modified(origin.X, origin.Y, 0, 0), 8);
			return true;
		}

		// Token: 0x060046E1 RID: 18145 RVA: 0x00634D5C File Offset: 0x00632F5C
		private void BuildMagmaMap(Point tileOrigin)
		{
			this._sourceMagmaMap = new GraniteBiome.Magma[200, 200];
			this._targetMagmaMap = new GraniteBiome.Magma[200, 200];
			for (int i = 0; i < this._sourceMagmaMap.GetLength(0); i++)
			{
				for (int j = 0; j < this._sourceMagmaMap.GetLength(1); j++)
				{
					int i2 = i + tileOrigin.X;
					int j2 = j + tileOrigin.Y;
					this._sourceMagmaMap[i, j] = GraniteBiome.Magma.CreateEmpty((double)((!WorldGen.SolidTile(i2, j2, false)) ? 1 : 4));
					this._targetMagmaMap[i, j] = this._sourceMagmaMap[i, j];
				}
			}
		}

		// Token: 0x060046E2 RID: 18146 RVA: 0x00634E10 File Offset: 0x00633010
		private void SimulatePressure(out Rectangle effectedMapArea)
		{
			int length = this._sourceMagmaMap.GetLength(0);
			int length2 = this._sourceMagmaMap.GetLength(1);
			int num = length / 2;
			int num2 = length2 / 2;
			int num3 = num;
			int num4 = num3;
			int num5 = num2;
			int num6 = num5;
			for (int i = 0; i < 300; i++)
			{
				for (int j = num3; j <= num4; j++)
				{
					for (int k = num5; k <= num6; k++)
					{
						GraniteBiome.Magma magma = this._sourceMagmaMap[j, k];
						if (magma.IsActive)
						{
							double num7 = 0.0;
							Vector2D zero = Vector2D.Zero;
							for (int l = -1; l <= 1; l++)
							{
								for (int m = -1; m <= 1; m++)
								{
									if (l != 0 || m != 0)
									{
										Vector2D vector2D = GraniteBiome._normalisedVectors[(l + 1) * 3 + (m + 1)];
										GraniteBiome.Magma magma2 = this._sourceMagmaMap[j + l, k + m];
										if (magma.Pressure > 0.01 && !magma2.IsActive)
										{
											if (l == -1)
											{
												num3 = Utils.Clamp<int>(j + l, 1, num3);
											}
											else
											{
												num4 = Utils.Clamp<int>(j + l, num4, length - 2);
											}
											if (m == -1)
											{
												num5 = Utils.Clamp<int>(k + m, 1, num5);
											}
											else
											{
												num6 = Utils.Clamp<int>(k + m, num6, length2 - 2);
											}
											this._targetMagmaMap[j + l, k + m] = magma2.ToFlow();
										}
										double pressure = magma2.Pressure;
										num7 += pressure;
										zero += pressure * vector2D;
									}
								}
							}
							num7 /= 8.0;
							if (num7 > magma.Resistance)
							{
								double num8 = zero.Length() / 8.0;
								double val = Math.Max(num7 - num8 - magma.Pressure, 0.0) + num8 + magma.Pressure * 0.875 - magma.Resistance;
								val = Math.Max(0.0, val);
								this._targetMagmaMap[j, k] = GraniteBiome.Magma.CreateFlow(val, Math.Max(0.0, magma.Resistance - val * 0.02));
							}
						}
					}
				}
				if (i < 2)
				{
					this._targetMagmaMap[num, num2] = GraniteBiome.Magma.CreateFlow(25.0, 0.0);
				}
				Utils.Swap<GraniteBiome.Magma[,]>(ref this._sourceMagmaMap, ref this._targetMagmaMap);
			}
			effectedMapArea = new Rectangle(num3, num5, num4 - num3 + 1, num6 - num5 + 1);
		}

		// Token: 0x060046E3 RID: 18147 RVA: 0x006350DC File Offset: 0x006332DC
		private unsafe bool ShouldUseLava(Point tileOrigin)
		{
			int length = this._sourceMagmaMap.GetLength(0);
			int length2 = this._sourceMagmaMap.GetLength(1);
			int num = length / 2;
			int num2 = length2 / 2;
			if (tileOrigin.Y + num2 <= GenVars.lavaLine - 30)
			{
				return false;
			}
			for (int i = -50; i < 50; i++)
			{
				for (int j = -50; j < 50; j++)
				{
					if (GenBase._tiles[tileOrigin.X + num + i, tileOrigin.Y + num2 + j].active())
					{
						ushort type = *GenBase._tiles[tileOrigin.X + num + i, tileOrigin.Y + num2 + j].type;
						if (type == 147 || type - 161 <= 2 || type == 200)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x006351B8 File Offset: 0x006333B8
		private unsafe void PlaceGranite(Point tileOrigin, Rectangle magmaMapArea)
		{
			bool flag = this.ShouldUseLava(tileOrigin);
			ushort type = 368;
			ushort wall = 180;
			if (WorldGen.drunkWorldGen)
			{
				type = 367;
				wall = 178;
			}
			for (int i = magmaMapArea.Left; i < magmaMapArea.Right; i++)
			{
				for (int j = magmaMapArea.Top; j < magmaMapArea.Bottom; j++)
				{
					GraniteBiome.Magma magma = this._sourceMagmaMap[i, j];
					if (magma.IsActive)
					{
						Tile tile = GenBase._tiles[tileOrigin.X + i, tileOrigin.Y + j];
						double num = Math.Sin((double)(tileOrigin.Y + j) * 0.4) * 0.7 + 1.2;
						double num2 = 0.2 + 0.5 / Math.Sqrt(Math.Max(0.0, magma.Pressure - magma.Resistance));
						if (Math.Max(1.0 - Math.Max(0.0, num * num2), magma.Pressure / 15.0) > 0.35 + (WorldGen.SolidTile(tileOrigin.X + i, tileOrigin.Y + j, false) ? 0.0 : 0.5))
						{
							if (TileID.Sets.Ore[(int)(*tile.type)])
							{
								tile.ResetToType(*tile.type);
							}
							else
							{
								tile.ResetToType(type);
							}
							*tile.wall = wall;
						}
						else if (magma.Resistance < 0.01)
						{
							WorldUtils.ClearTile(tileOrigin.X + i, tileOrigin.Y + j, false);
							*tile.wall = wall;
						}
						if (*tile.liquid > 0 && flag)
						{
							tile.liquidType(1);
						}
					}
				}
			}
		}

		// Token: 0x060046E5 RID: 18149 RVA: 0x006353B8 File Offset: 0x006335B8
		private unsafe void CleanupTiles(Point tileOrigin, Rectangle magmaMapArea)
		{
			ushort wall = 180;
			if (WorldGen.drunkWorldGen)
			{
				wall = 178;
			}
			List<Point16> list = new List<Point16>();
			for (int i = magmaMapArea.Left; i < magmaMapArea.Right; i++)
			{
				for (int j = magmaMapArea.Top; j < magmaMapArea.Bottom; j++)
				{
					if (this._sourceMagmaMap[i, j].IsActive)
					{
						int num = 0;
						int num2 = i + tileOrigin.X;
						int num3 = j + tileOrigin.Y;
						if (WorldGen.SolidTile(num2, num3, false))
						{
							for (int k = -1; k <= 1; k++)
							{
								for (int l = -1; l <= 1; l++)
								{
									if (WorldGen.SolidTile(num2 + k, num3 + l, false))
									{
										num++;
									}
								}
							}
							if (num < 3)
							{
								list.Add(new Point16(num2, num3));
							}
						}
					}
				}
			}
			foreach (Point16 point in list)
			{
				int x = (int)point.X;
				int y = (int)point.Y;
				WorldUtils.ClearTile(x, y, true);
				*GenBase._tiles[x, y].wall = wall;
			}
			list.Clear();
		}

		// Token: 0x060046E6 RID: 18150 RVA: 0x00635510 File Offset: 0x00633710
		private void PlaceDecorations(Point tileOrigin, Rectangle magmaMapArea)
		{
			FastRandom fastRandom = new FastRandom(Main.ActiveWorldFileData.Seed).WithModifier(65440UL);
			for (int i = magmaMapArea.Left; i < magmaMapArea.Right; i++)
			{
				for (int j = magmaMapArea.Top; j < magmaMapArea.Bottom; j++)
				{
					ref GraniteBiome.Magma ptr = this._sourceMagmaMap[i, j];
					int num = i + tileOrigin.X;
					int num2 = j + tileOrigin.Y;
					if (ptr.IsActive)
					{
						WorldUtils.TileFrame(num, num2, false);
						WorldGen.SquareWallFrame(num, num2, true);
						FastRandom fastRandom2 = fastRandom.WithModifier(num, num2);
						if (fastRandom2.Next(8) == 0 && GenBase._tiles[num, num2].active())
						{
							if (!GenBase._tiles[num, num2 + 1].active())
							{
								WorldGen.PlaceUncheckedStalactite(num, num2 + 1, fastRandom2.Next(2) == 0, fastRandom2.Next(3), false);
							}
							if (!GenBase._tiles[num, num2 - 1].active())
							{
								WorldGen.PlaceUncheckedStalactite(num, num2 - 1, fastRandom2.Next(2) == 0, fastRandom2.Next(3), false);
							}
						}
						if (fastRandom2.Next(2) == 0)
						{
							Tile.SmoothSlope(num, num2, true, false);
						}
					}
				}
			}
		}

		// Token: 0x04005BA5 RID: 23461
		private const int MAX_MAGMA_ITERATIONS = 300;

		// Token: 0x04005BA6 RID: 23462
		private GraniteBiome.Magma[,] _sourceMagmaMap = new GraniteBiome.Magma[200, 200];

		// Token: 0x04005BA7 RID: 23463
		private GraniteBiome.Magma[,] _targetMagmaMap = new GraniteBiome.Magma[200, 200];

		// Token: 0x04005BA8 RID: 23464
		private static Vector2D[] _normalisedVectors = new Vector2D[]
		{
			Vector2D.Normalize(new Vector2D(-1.0, -1.0)),
			Vector2D.Normalize(new Vector2D(-1.0, 0.0)),
			Vector2D.Normalize(new Vector2D(-1.0, 1.0)),
			Vector2D.Normalize(new Vector2D(0.0, -1.0)),
			new Vector2D(0.0, 0.0),
			Vector2D.Normalize(new Vector2D(0.0, 1.0)),
			Vector2D.Normalize(new Vector2D(1.0, -1.0)),
			Vector2D.Normalize(new Vector2D(1.0, 0.0)),
			Vector2D.Normalize(new Vector2D(1.0, 1.0))
		};

		// Token: 0x02000D10 RID: 3344
		private struct Magma
		{
			// Token: 0x060062F6 RID: 25334 RVA: 0x006D7816 File Offset: 0x006D5A16
			private Magma(double pressure, double resistance, bool active)
			{
				this.Pressure = pressure;
				this.Resistance = resistance;
				this.IsActive = active;
			}

			// Token: 0x060062F7 RID: 25335 RVA: 0x006D782D File Offset: 0x006D5A2D
			public GraniteBiome.Magma ToFlow()
			{
				return new GraniteBiome.Magma(this.Pressure, this.Resistance, true);
			}

			// Token: 0x060062F8 RID: 25336 RVA: 0x006D7841 File Offset: 0x006D5A41
			public static GraniteBiome.Magma CreateFlow(double pressure, double resistance = 0.0)
			{
				return new GraniteBiome.Magma(pressure, resistance, true);
			}

			// Token: 0x060062F9 RID: 25337 RVA: 0x006D784B File Offset: 0x006D5A4B
			public static GraniteBiome.Magma CreateEmpty(double resistance = 0.0)
			{
				return new GraniteBiome.Magma(0.0, resistance, false);
			}

			// Token: 0x04007AB7 RID: 31415
			public readonly double Pressure;

			// Token: 0x04007AB8 RID: 31416
			public readonly double Resistance;

			// Token: 0x04007AB9 RID: 31417
			public readonly bool IsActive;
		}
	}
}
