using System;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x0200065E RID: 1630
	public class TerrainPass : GenPass
	{
		// Token: 0x06004707 RID: 18183 RVA: 0x00638A20 File Offset: 0x00636C20
		public TerrainPass() : base("Terrain", 449.3721923828125)
		{
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x00638A38 File Offset: 0x00636C38
		protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
		{
			int num = configuration.Get<int>("FlatBeachPadding");
			progress.Message = Lang.gen[0].Value;
			TerrainPass.TerrainFeatureType terrainFeatureType = TerrainPass.TerrainFeatureType.Plateau;
			double num2 = (double)Main.maxTilesY * 0.3;
			num2 *= (double)GenBase._random.Next(90, 110) * 0.005;
			double num3 = num2 + (double)Main.maxTilesY * 0.2;
			num3 *= (double)GenBase._random.Next(90, 110) * 0.01;
			if (WorldGen.remixWorldGen)
			{
				num3 = (double)Main.maxTilesY * 0.5;
				if (Main.maxTilesX > 2500)
				{
					num3 = (double)Main.maxTilesY * 0.6;
				}
				num3 *= (double)GenBase._random.Next(95, 106) * 0.01;
			}
			double num4 = num2;
			double num5 = num2;
			double num6 = num3;
			double num7 = num3;
			double num8 = (double)Main.maxTilesY * 0.23;
			TerrainPass.SurfaceHistory surfaceHistory = new TerrainPass.SurfaceHistory(500);
			int num9 = GenVars.leftBeachEnd + num;
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				progress.Set((double)i / (double)Main.maxTilesX);
				num4 = Math.Min(num2, num4);
				num5 = Math.Max(num2, num5);
				num6 = Math.Min(num3, num6);
				num7 = Math.Max(num3, num7);
				if (num9 <= 0)
				{
					terrainFeatureType = (TerrainPass.TerrainFeatureType)GenBase._random.Next(0, 5);
					num9 = GenBase._random.Next(5, 40);
					if (terrainFeatureType == TerrainPass.TerrainFeatureType.Plateau)
					{
						num9 *= (int)((double)GenBase._random.Next(5, 30) * 0.2);
					}
				}
				num9--;
				if ((double)i > (double)Main.maxTilesX * 0.45 && (double)i < (double)Main.maxTilesX * 0.55 && (terrainFeatureType == TerrainPass.TerrainFeatureType.Mountain || terrainFeatureType == TerrainPass.TerrainFeatureType.Valley))
				{
					terrainFeatureType = (TerrainPass.TerrainFeatureType)GenBase._random.Next(3);
				}
				if ((double)i > (double)Main.maxTilesX * 0.48 && (double)i < (double)Main.maxTilesX * 0.52)
				{
					terrainFeatureType = TerrainPass.TerrainFeatureType.Plateau;
				}
				num2 += TerrainPass.GenerateWorldSurfaceOffset(terrainFeatureType);
				double num10 = 0.17;
				double num11 = 0.26;
				if (WorldGen.drunkWorldGen)
				{
					num10 = 0.15;
					num11 = 0.28;
				}
				if (i < GenVars.leftBeachEnd + num || i > GenVars.rightBeachStart - num)
				{
					num2 = Utils.Clamp<double>(num2, (double)Main.maxTilesY * 0.17, num8);
				}
				else if (num2 < (double)Main.maxTilesY * num10)
				{
					num2 = (double)Main.maxTilesY * num10;
					num9 = 0;
				}
				else if (num2 > (double)Main.maxTilesY * num11)
				{
					num2 = (double)Main.maxTilesY * num11;
					num9 = 0;
				}
				while (GenBase._random.Next(0, 3) == 0)
				{
					num3 += (double)GenBase._random.Next(-2, 3);
				}
				if (WorldGen.remixWorldGen)
				{
					if (Main.maxTilesX > 2500)
					{
						if (num3 > (double)Main.maxTilesY * 0.7)
						{
							num3 -= 1.0;
						}
					}
					else if (num3 > (double)Main.maxTilesY * 0.6)
					{
						num3 -= 1.0;
					}
				}
				else
				{
					if (num3 < num2 + (double)Main.maxTilesY * 0.06)
					{
						num3 += 1.0;
					}
					if (num3 > num2 + (double)Main.maxTilesY * 0.35)
					{
						num3 -= 1.0;
					}
				}
				surfaceHistory.Record(num2);
				TerrainPass.FillColumn(i, num2, num3);
				if (i == GenVars.rightBeachStart - num)
				{
					if (num2 > num8)
					{
						TerrainPass.RetargetSurfaceHistory(surfaceHistory, i, num8);
					}
					terrainFeatureType = TerrainPass.TerrainFeatureType.Plateau;
					num9 = Main.maxTilesX - i;
				}
			}
			Main.worldSurface = (double)((int)(num5 + 25.0));
			Main.rockLayer = num7;
			double num12 = (double)((int)((Main.rockLayer - Main.worldSurface) / 6.0) * 6);
			Main.rockLayer = (double)((int)(Main.worldSurface + num12));
			int num15 = (int)(Main.rockLayer + (double)Main.maxTilesY) / 2 + GenBase._random.Next(-100, 20);
			int lavaLine = num15 + GenBase._random.Next(50, 80);
			if (WorldGen.remixWorldGen)
			{
				lavaLine = (int)(Main.worldSurface * 4.0 + num3) / 5;
			}
			int num13 = 20;
			if (num6 < num5 + (double)num13)
			{
				double num16 = (num6 + num5) / 2.0;
				double num14 = Math.Abs(num6 - num5);
				if (num14 < (double)num13)
				{
					num14 = (double)num13;
				}
				num6 = num16 + num14 / 2.0;
				num5 = num16 - num14 / 2.0;
			}
			GenVars.rockLayer = num3;
			GenVars.rockLayerHigh = num7;
			GenVars.rockLayerLow = num6;
			GenVars.worldSurface = num2;
			GenVars.worldSurfaceHigh = num5;
			GenVars.worldSurfaceLow = num4;
			GenVars.waterLine = num15;
			GenVars.lavaLine = lavaLine;
		}

		// Token: 0x06004709 RID: 18185 RVA: 0x00638F08 File Offset: 0x00637108
		private unsafe static void FillColumn(int x, double worldSurface, double rockLayer)
		{
			int i = 0;
			while ((double)i < worldSurface)
			{
				Main.tile[x, i].active(false);
				*Main.tile[x, i].frameX = -1;
				*Main.tile[x, i].frameY = -1;
				i++;
			}
			for (int j = (int)worldSurface; j < Main.maxTilesY; j++)
			{
				if ((double)j < rockLayer)
				{
					Main.tile[x, j].active(true);
					*Main.tile[x, j].type = 0;
					*Main.tile[x, j].frameX = -1;
					*Main.tile[x, j].frameY = -1;
				}
				else
				{
					Main.tile[x, j].active(true);
					*Main.tile[x, j].type = 1;
					*Main.tile[x, j].frameX = -1;
					*Main.tile[x, j].frameY = -1;
				}
			}
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x00639030 File Offset: 0x00637230
		private unsafe static void RetargetColumn(int x, double worldSurface)
		{
			int i = 0;
			while ((double)i < worldSurface)
			{
				Main.tile[x, i].active(false);
				*Main.tile[x, i].frameX = -1;
				*Main.tile[x, i].frameY = -1;
				i++;
			}
			for (int j = (int)worldSurface; j < Main.maxTilesY; j++)
			{
				if (*Main.tile[x, j].type != 1 || !Main.tile[x, j].active())
				{
					Main.tile[x, j].active(true);
					*Main.tile[x, j].type = 0;
					*Main.tile[x, j].frameX = -1;
					*Main.tile[x, j].frameY = -1;
				}
			}
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x00639128 File Offset: 0x00637328
		private static double GenerateWorldSurfaceOffset(TerrainPass.TerrainFeatureType featureType)
		{
			double num = 0.0;
			if ((WorldGen.drunkWorldGen || WorldGen.getGoodWorldGen || WorldGen.remixWorldGen) && WorldGen.genRand.Next(2) == 0)
			{
				switch (featureType)
				{
				case TerrainPass.TerrainFeatureType.Plateau:
					while (GenBase._random.Next(0, 6) == 0)
					{
						num += (double)GenBase._random.Next(-1, 2);
					}
					break;
				case TerrainPass.TerrainFeatureType.Hill:
					while (GenBase._random.Next(0, 3) == 0)
					{
						num -= 1.0;
					}
					while (GenBase._random.Next(0, 10) == 0)
					{
						num += 1.0;
					}
					break;
				case TerrainPass.TerrainFeatureType.Dale:
					while (GenBase._random.Next(0, 3) == 0)
					{
						num += 1.0;
					}
					while (GenBase._random.Next(0, 10) == 0)
					{
						num -= 1.0;
					}
					break;
				case TerrainPass.TerrainFeatureType.Mountain:
					while (GenBase._random.Next(0, 3) != 0)
					{
						num -= 1.0;
					}
					while (GenBase._random.Next(0, 6) == 0)
					{
						num += 1.0;
					}
					break;
				case TerrainPass.TerrainFeatureType.Valley:
					while (GenBase._random.Next(0, 3) != 0)
					{
						num += 1.0;
					}
					while (GenBase._random.Next(0, 5) == 0)
					{
						num -= 1.0;
					}
					break;
				}
			}
			else
			{
				switch (featureType)
				{
				case TerrainPass.TerrainFeatureType.Plateau:
					while (GenBase._random.Next(0, 7) == 0)
					{
						num += (double)GenBase._random.Next(-1, 2);
					}
					break;
				case TerrainPass.TerrainFeatureType.Hill:
					while (GenBase._random.Next(0, 4) == 0)
					{
						num -= 1.0;
					}
					while (GenBase._random.Next(0, 10) == 0)
					{
						num += 1.0;
					}
					break;
				case TerrainPass.TerrainFeatureType.Dale:
					while (GenBase._random.Next(0, 4) == 0)
					{
						num += 1.0;
					}
					while (GenBase._random.Next(0, 10) == 0)
					{
						num -= 1.0;
					}
					break;
				case TerrainPass.TerrainFeatureType.Mountain:
					while (GenBase._random.Next(0, 2) == 0)
					{
						num -= 1.0;
					}
					while (GenBase._random.Next(0, 6) == 0)
					{
						num += 1.0;
					}
					break;
				case TerrainPass.TerrainFeatureType.Valley:
					while (GenBase._random.Next(0, 2) == 0)
					{
						num += 1.0;
					}
					while (GenBase._random.Next(0, 5) == 0)
					{
						num -= 1.0;
					}
					break;
				}
			}
			return num;
		}

		// Token: 0x0600470C RID: 18188 RVA: 0x006393C0 File Offset: 0x006375C0
		private static void RetargetSurfaceHistory(TerrainPass.SurfaceHistory history, int targetX, double targetHeight)
		{
			int i = 0;
			while (i < history.Length / 2 && history[history.Length - 1] > targetHeight)
			{
				for (int j = 0; j < history.Length - i * 2; j++)
				{
					double num = history[history.Length - j - 1];
					num -= 1.0;
					history[history.Length - j - 1] = num;
					if (num <= targetHeight)
					{
						break;
					}
				}
				i++;
			}
			for (int k = 0; k < history.Length; k++)
			{
				double worldSurface = history[history.Length - k - 1];
				TerrainPass.RetargetColumn(targetX - k, worldSurface);
			}
		}

		// Token: 0x02000D15 RID: 3349
		private enum TerrainFeatureType
		{
			// Token: 0x04007AC3 RID: 31427
			Plateau,
			// Token: 0x04007AC4 RID: 31428
			Hill,
			// Token: 0x04007AC5 RID: 31429
			Dale,
			// Token: 0x04007AC6 RID: 31430
			Mountain,
			// Token: 0x04007AC7 RID: 31431
			Valley
		}

		// Token: 0x02000D16 RID: 3350
		private class SurfaceHistory
		{
			// Token: 0x17000990 RID: 2448
			public double this[int index]
			{
				get
				{
					return this._heights[(index + this._index) % this._heights.Length];
				}
				set
				{
					this._heights[(index + this._index) % this._heights.Length] = value;
				}
			}

			// Token: 0x17000991 RID: 2449
			// (get) Token: 0x0600630B RID: 25355 RVA: 0x006D7902 File Offset: 0x006D5B02
			public int Length
			{
				get
				{
					return this._heights.Length;
				}
			}

			// Token: 0x0600630C RID: 25356 RVA: 0x006D790C File Offset: 0x006D5B0C
			public SurfaceHistory(int size)
			{
				this._heights = new double[size];
			}

			// Token: 0x0600630D RID: 25357 RVA: 0x006D7920 File Offset: 0x006D5B20
			public void Record(double height)
			{
				this._heights[this._index] = height;
				this._index = (this._index + 1) % this._heights.Length;
			}

			// Token: 0x04007AC8 RID: 31432
			private readonly double[] _heights;

			// Token: 0x04007AC9 RID: 31433
			private int _index;
		}
	}
}
