using System;
using Terraria.IO;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x020002C8 RID: 712
	public class TerrainPass : GenPass
	{
		// Token: 0x0600229A RID: 8858 RVA: 0x00544E50 File Offset: 0x00543050
		public TerrainPass() : base("Terrain", 449.3721923828125)
		{
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x00544E68 File Offset: 0x00543068
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
			int num13 = (int)(Main.rockLayer + (double)Main.maxTilesY) / 2 + GenBase._random.Next(-100, 20);
			int lavaLine = num13 + GenBase._random.Next(50, 80);
			if (WorldGen.remixWorldGen)
			{
				lavaLine = (int)(Main.worldSurface * 4.0 + num3) / 5;
			}
			int num14 = 20;
			if (num6 < num5 + (double)num14)
			{
				double num15 = (num6 + num5) / 2.0;
				double num16 = Math.Abs(num6 - num5);
				if (num16 < (double)num14)
				{
					num16 = (double)num14;
				}
				num6 = num15 + num16 / 2.0;
				num5 = num15 - num16 / 2.0;
			}
			GenVars.rockLayer = num3;
			GenVars.rockLayerHigh = num7;
			GenVars.rockLayerLow = num6;
			GenVars.worldSurface = num2;
			GenVars.worldSurfaceHigh = num5;
			GenVars.worldSurfaceLow = num4;
			GenVars.waterLine = num13;
			GenVars.lavaLine = lavaLine;
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x00545338 File Offset: 0x00543538
		private static void FillColumn(int x, double worldSurface, double rockLayer)
		{
			int num = 0;
			while ((double)num < worldSurface)
			{
				Main.tile[x, num].active(false);
				Main.tile[x, num].frameX = -1;
				Main.tile[x, num].frameY = -1;
				num++;
			}
			for (int i = (int)worldSurface; i < Main.maxTilesY; i++)
			{
				if ((double)i < rockLayer)
				{
					Main.tile[x, i].active(true);
					Main.tile[x, i].type = 0;
					Main.tile[x, i].frameX = -1;
					Main.tile[x, i].frameY = -1;
				}
				else
				{
					Main.tile[x, i].active(true);
					Main.tile[x, i].type = 1;
					Main.tile[x, i].frameX = -1;
					Main.tile[x, i].frameY = -1;
				}
			}
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x00545438 File Offset: 0x00543638
		private static void RetargetColumn(int x, double worldSurface)
		{
			int num = 0;
			while ((double)num < worldSurface)
			{
				Main.tile[x, num].active(false);
				Main.tile[x, num].frameX = -1;
				Main.tile[x, num].frameY = -1;
				num++;
			}
			for (int i = (int)worldSurface; i < Main.maxTilesY; i++)
			{
				if (Main.tile[x, i].type != 1 || !Main.tile[x, i].active())
				{
					Main.tile[x, i].active(true);
					Main.tile[x, i].type = 0;
					Main.tile[x, i].frameX = -1;
					Main.tile[x, i].frameY = -1;
				}
			}
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x00545508 File Offset: 0x00543708
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

		// Token: 0x0600229F RID: 8863 RVA: 0x005457A0 File Offset: 0x005439A0
		private static void RetargetSurfaceHistory(TerrainPass.SurfaceHistory history, int targetX, double targetHeight)
		{
			int num = 0;
			while (num < history.Length / 2 && history[history.Length - 1] > targetHeight)
			{
				for (int i = 0; i < history.Length - num * 2; i++)
				{
					double num2 = history[history.Length - i - 1];
					num2 -= 1.0;
					history[history.Length - i - 1] = num2;
					if (num2 <= targetHeight)
					{
						break;
					}
				}
				num++;
			}
			for (int j = 0; j < history.Length; j++)
			{
				double worldSurface = history[history.Length - j - 1];
				TerrainPass.RetargetColumn(targetX - j, worldSurface);
			}
		}

		// Token: 0x020006C8 RID: 1736
		private enum TerrainFeatureType
		{
			// Token: 0x0400621E RID: 25118
			Plateau,
			// Token: 0x0400621F RID: 25119
			Hill,
			// Token: 0x04006220 RID: 25120
			Dale,
			// Token: 0x04006221 RID: 25121
			Mountain,
			// Token: 0x04006222 RID: 25122
			Valley
		}

		// Token: 0x020006C9 RID: 1737
		private class SurfaceHistory
		{
			// Token: 0x170003EE RID: 1006
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

			// Token: 0x170003EF RID: 1007
			// (get) Token: 0x0600367D RID: 13949 RVA: 0x0060C826 File Offset: 0x0060AA26
			public int Length
			{
				get
				{
					return this._heights.Length;
				}
			}

			// Token: 0x0600367E RID: 13950 RVA: 0x0060C830 File Offset: 0x0060AA30
			public SurfaceHistory(int size)
			{
				this._heights = new double[size];
			}

			// Token: 0x0600367F RID: 13951 RVA: 0x0060C844 File Offset: 0x0060AA44
			public void Record(double height)
			{
				this._heights[this._index] = height;
				this._index = (this._index + 1) % this._heights.Length;
			}

			// Token: 0x04006223 RID: 25123
			private readonly double[] _heights;

			// Token: 0x04006224 RID: 25124
			private int _index;
		}
	}
}
