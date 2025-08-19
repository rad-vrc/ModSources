using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Terraria
{
	// Token: 0x0200001B RID: 27
	public class SceneMetrics
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x0000CEB1 File Offset: 0x0000B0B1
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x0000CEB9 File Offset: 0x0000B0B9
		public Point? ClosestOrePosition { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000CEC2 File Offset: 0x0000B0C2
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000CECA File Offset: 0x0000B0CA
		public int ShimmerTileCount { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x0000CED3 File Offset: 0x0000B0D3
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x0000CEDB File Offset: 0x0000B0DB
		public int EvilTileCount { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000CEE4 File Offset: 0x0000B0E4
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x0000CEEC File Offset: 0x0000B0EC
		public int HolyTileCount { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000CEF5 File Offset: 0x0000B0F5
		// (set) Token: 0x060000EB RID: 235 RVA: 0x0000CEFD File Offset: 0x0000B0FD
		public int HoneyBlockCount { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000CF06 File Offset: 0x0000B106
		// (set) Token: 0x060000ED RID: 237 RVA: 0x0000CF0E File Offset: 0x0000B10E
		public int ActiveMusicBox { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000CF17 File Offset: 0x0000B117
		// (set) Token: 0x060000EF RID: 239 RVA: 0x0000CF1F File Offset: 0x0000B11F
		public int SandTileCount { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000CF28 File Offset: 0x0000B128
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x0000CF30 File Offset: 0x0000B130
		public int MushroomTileCount { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000CF39 File Offset: 0x0000B139
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x0000CF41 File Offset: 0x0000B141
		public int SnowTileCount { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000CF4A File Offset: 0x0000B14A
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x0000CF52 File Offset: 0x0000B152
		public int WaterCandleCount { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000CF5B File Offset: 0x0000B15B
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x0000CF63 File Offset: 0x0000B163
		public int PeaceCandleCount { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000CF6C File Offset: 0x0000B16C
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x0000CF74 File Offset: 0x0000B174
		public int ShadowCandleCount { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000FA RID: 250 RVA: 0x0000CF7D File Offset: 0x0000B17D
		// (set) Token: 0x060000FB RID: 251 RVA: 0x0000CF85 File Offset: 0x0000B185
		public int PartyMonolithCount { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000CF8E File Offset: 0x0000B18E
		// (set) Token: 0x060000FD RID: 253 RVA: 0x0000CF96 File Offset: 0x0000B196
		public int MeteorTileCount { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000CF9F File Offset: 0x0000B19F
		// (set) Token: 0x060000FF RID: 255 RVA: 0x0000CFA7 File Offset: 0x0000B1A7
		public int BloodTileCount { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000CFB0 File Offset: 0x0000B1B0
		// (set) Token: 0x06000101 RID: 257 RVA: 0x0000CFB8 File Offset: 0x0000B1B8
		public int JungleTileCount { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000CFC1 File Offset: 0x0000B1C1
		// (set) Token: 0x06000103 RID: 259 RVA: 0x0000CFC9 File Offset: 0x0000B1C9
		public int DungeonTileCount { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000CFD2 File Offset: 0x0000B1D2
		// (set) Token: 0x06000105 RID: 261 RVA: 0x0000CFDA File Offset: 0x0000B1DA
		public bool HasSunflower { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000CFE3 File Offset: 0x0000B1E3
		// (set) Token: 0x06000107 RID: 263 RVA: 0x0000CFEB File Offset: 0x0000B1EB
		public bool HasGardenGnome { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000CFF4 File Offset: 0x0000B1F4
		// (set) Token: 0x06000109 RID: 265 RVA: 0x0000CFFC File Offset: 0x0000B1FC
		public bool HasClock { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000D005 File Offset: 0x0000B205
		// (set) Token: 0x0600010B RID: 267 RVA: 0x0000D00D File Offset: 0x0000B20D
		public bool HasCampfire { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000D016 File Offset: 0x0000B216
		// (set) Token: 0x0600010D RID: 269 RVA: 0x0000D01E File Offset: 0x0000B21E
		public bool HasStarInBottle { get; private set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000D027 File Offset: 0x0000B227
		// (set) Token: 0x0600010F RID: 271 RVA: 0x0000D02F File Offset: 0x0000B22F
		public bool HasHeartLantern { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000D038 File Offset: 0x0000B238
		// (set) Token: 0x06000111 RID: 273 RVA: 0x0000D040 File Offset: 0x0000B240
		public int ActiveFountainColor { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000112 RID: 274 RVA: 0x0000D049 File Offset: 0x0000B249
		// (set) Token: 0x06000113 RID: 275 RVA: 0x0000D051 File Offset: 0x0000B251
		public int ActiveMonolithType { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000D05A File Offset: 0x0000B25A
		// (set) Token: 0x06000115 RID: 277 RVA: 0x0000D062 File Offset: 0x0000B262
		public bool BloodMoonMonolith { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000D06B File Offset: 0x0000B26B
		// (set) Token: 0x06000117 RID: 279 RVA: 0x0000D073 File Offset: 0x0000B273
		public bool MoonLordMonolith { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000D07C File Offset: 0x0000B27C
		// (set) Token: 0x06000119 RID: 281 RVA: 0x0000D084 File Offset: 0x0000B284
		public bool EchoMonolith { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000D08D File Offset: 0x0000B28D
		// (set) Token: 0x0600011B RID: 283 RVA: 0x0000D095 File Offset: 0x0000B295
		public int ShimmerMonolithState { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000D09E File Offset: 0x0000B29E
		// (set) Token: 0x0600011D RID: 285 RVA: 0x0000D0A6 File Offset: 0x0000B2A6
		public bool HasCatBast { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000D0AF File Offset: 0x0000B2AF
		// (set) Token: 0x0600011F RID: 287 RVA: 0x0000D0B7 File Offset: 0x0000B2B7
		public int GraveyardTileCount { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000D0C0 File Offset: 0x0000B2C0
		public bool EnoughTilesForShimmer
		{
			get
			{
				return this.ShimmerTileCount >= SceneMetrics.ShimmerTileThreshold;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000D0D2 File Offset: 0x0000B2D2
		public bool EnoughTilesForJungle
		{
			get
			{
				return this.JungleTileCount >= SceneMetrics.JungleTileThreshold;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000D0E4 File Offset: 0x0000B2E4
		public bool EnoughTilesForHallow
		{
			get
			{
				return this.HolyTileCount >= SceneMetrics.HallowTileThreshold;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000D0F6 File Offset: 0x0000B2F6
		public bool EnoughTilesForSnow
		{
			get
			{
				return this.SnowTileCount >= SceneMetrics.SnowTileThreshold;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000D108 File Offset: 0x0000B308
		public bool EnoughTilesForGlowingMushroom
		{
			get
			{
				return this.MushroomTileCount >= SceneMetrics.MushroomTileThreshold;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000D11A File Offset: 0x0000B31A
		public bool EnoughTilesForDesert
		{
			get
			{
				return this.SandTileCount >= SceneMetrics.DesertTileThreshold;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000D12C File Offset: 0x0000B32C
		public bool EnoughTilesForCorruption
		{
			get
			{
				return this.EvilTileCount >= SceneMetrics.CorruptionTileThreshold;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000D13E File Offset: 0x0000B33E
		public bool EnoughTilesForCrimson
		{
			get
			{
				return this.BloodTileCount >= SceneMetrics.CrimsonTileThreshold;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000D150 File Offset: 0x0000B350
		public bool EnoughTilesForMeteor
		{
			get
			{
				return this.MeteorTileCount >= SceneMetrics.MeteorTileThreshold;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000D162 File Offset: 0x0000B362
		public bool EnoughTilesForGraveyard
		{
			get
			{
				return this.GraveyardTileCount >= SceneMetrics.GraveyardTileThreshold;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000D174 File Offset: 0x0000B374
		public SceneMetrics()
		{
			this.Reset();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
		public void ScanAndExportToMain(SceneMetricsScanSettings settings)
		{
			this.Reset();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (settings.ScanOreFinderData)
			{
				this._oreFinderTileLocations.Clear();
			}
			if (settings.BiomeScanCenterPositionInWorld != null)
			{
				Point point = settings.BiomeScanCenterPositionInWorld.Value.ToTileCoordinates();
				Rectangle tileRectangle = new Rectangle(point.X - Main.buffScanAreaWidth / 2, point.Y - Main.buffScanAreaHeight / 2, Main.buffScanAreaWidth, Main.buffScanAreaHeight);
				tileRectangle = WorldUtils.ClampToWorld(tileRectangle);
				for (int i = tileRectangle.Left; i < tileRectangle.Right; i++)
				{
					for (int j = tileRectangle.Top; j < tileRectangle.Bottom; j++)
					{
						if (tileRectangle.Contains(i, j))
						{
							Tile tile = Main.tile[i, j];
							if (tile != null)
							{
								if (!tile.active())
								{
									if (tile.liquid > 0)
									{
										this._liquidCounts[(int)tile.liquidType()]++;
									}
								}
								else
								{
									tileRectangle.Contains(i, j);
									if (!TileID.Sets.isDesertBiomeSand[(int)tile.type] || !WorldGen.oceanDepths(i, j))
									{
										this._tileCounts[(int)tile.type]++;
									}
									if (tile.type == 215 && tile.frameY < 36)
									{
										this.HasCampfire = true;
									}
									if (tile.type == 49 && tile.frameX < 18)
									{
										num++;
									}
									if (tile.type == 372 && tile.frameX < 18)
									{
										num2++;
									}
									if (tile.type == 646 && tile.frameX < 18)
									{
										num3++;
									}
									if (tile.type == 405 && tile.frameX < 54)
									{
										this.HasCampfire = true;
									}
									if (tile.type == 506 && tile.frameX < 72)
									{
										this.HasCatBast = true;
									}
									if (tile.type == 42 && tile.frameY >= 324 && tile.frameY <= 358)
									{
										this.HasHeartLantern = true;
									}
									if (tile.type == 42 && tile.frameY >= 252 && tile.frameY <= 286)
									{
										this.HasStarInBottle = true;
									}
									if (tile.type == 91 && (tile.frameX >= 396 || tile.frameY >= 54))
									{
										int num4 = (int)(tile.frameX / 18 - 21);
										for (int k = (int)tile.frameY; k >= 54; k -= 54)
										{
											num4 += 90;
											num4 += 21;
										}
										int num5 = Item.BannerToItem(num4);
										if (ItemID.Sets.BannerStrength.IndexInRange(num5) && ItemID.Sets.BannerStrength[num5].Enabled)
										{
											this.NPCBannerBuff[num4] = true;
											this.hasBanner = true;
										}
									}
									if (settings.ScanOreFinderData && Main.tileOreFinderPriority[(int)tile.type] != 0)
									{
										this._oreFinderTileLocations.Add(new Point(i, j));
									}
								}
							}
						}
					}
				}
			}
			if (settings.VisualScanArea != null)
			{
				Rectangle rectangle = WorldUtils.ClampToWorld(settings.VisualScanArea.Value);
				for (int l = rectangle.Left; l < rectangle.Right; l++)
				{
					for (int m = rectangle.Top; m < rectangle.Bottom; m++)
					{
						Tile tile2 = Main.tile[l, m];
						if (tile2 != null && tile2.active())
						{
							if (tile2.type == 104)
							{
								this.HasClock = true;
							}
							ushort type = tile2.type;
							if (type <= 410)
							{
								if (type != 139)
								{
									if (type != 207)
									{
										if (type == 410)
										{
											if (tile2.frameY >= 56)
											{
												int activeMonolithType = (int)(tile2.frameX / 36);
												this.ActiveMonolithType = activeMonolithType;
											}
										}
									}
									else if (tile2.frameY >= 72)
									{
										switch (tile2.frameX / 36)
										{
										case 0:
											this.ActiveFountainColor = 0;
											break;
										case 1:
											this.ActiveFountainColor = 12;
											break;
										case 2:
											this.ActiveFountainColor = 3;
											break;
										case 3:
											this.ActiveFountainColor = 5;
											break;
										case 4:
											this.ActiveFountainColor = 2;
											break;
										case 5:
											this.ActiveFountainColor = 10;
											break;
										case 6:
											this.ActiveFountainColor = 4;
											break;
										case 7:
											this.ActiveFountainColor = 9;
											break;
										case 8:
											this.ActiveFountainColor = 8;
											break;
										case 9:
											this.ActiveFountainColor = 6;
											break;
										default:
											this.ActiveFountainColor = -1;
											break;
										}
									}
								}
								else if (tile2.frameX >= 36)
								{
									this.ActiveMusicBox = (int)(tile2.frameY / 36);
								}
							}
							else if (type <= 509)
							{
								if (type != 480)
								{
									if (type == 509)
									{
										if (tile2.frameY >= 56)
										{
											this.ActiveMonolithType = 4;
										}
									}
								}
								else if (tile2.frameY >= 54)
								{
									this.BloodMoonMonolith = true;
								}
							}
							else if (type != 657)
							{
								if (type == 658)
								{
									int shimmerMonolithState = (int)(tile2.frameY / 54);
									this.ShimmerMonolithState = shimmerMonolithState;
								}
							}
							else if (tile2.frameY >= 54)
							{
								this.EchoMonolith = true;
							}
						}
					}
				}
			}
			this.WaterCandleCount = num;
			this.PeaceCandleCount = num2;
			this.ShadowCandleCount = num3;
			this.ExportTileCountsToMain();
			this.CanPlayCreditsRoll = (this.ActiveMusicBox == 85);
			if (settings.ScanOreFinderData)
			{
				this.UpdateOreFinderData();
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000D7B0 File Offset: 0x0000B9B0
		private void ExportTileCountsToMain()
		{
			if (this._tileCounts[27] > 0)
			{
				this.HasSunflower = true;
			}
			if (this._tileCounts[567] > 0)
			{
				this.HasGardenGnome = true;
			}
			this.ShimmerTileCount = this._liquidCounts[3];
			this.HoneyBlockCount = this._tileCounts[229];
			this.HolyTileCount = this._tileCounts[109] + this._tileCounts[492] + this._tileCounts[110] + this._tileCounts[113] + this._tileCounts[117] + this._tileCounts[116] + this._tileCounts[164] + this._tileCounts[403] + this._tileCounts[402];
			this.SnowTileCount = this._tileCounts[147] + this._tileCounts[148] + this._tileCounts[161] + this._tileCounts[162] + this._tileCounts[164] + this._tileCounts[163] + this._tileCounts[200];
			if (Main.remixWorld)
			{
				this.JungleTileCount = this._tileCounts[60] + this._tileCounts[61] + this._tileCounts[62] + this._tileCounts[74] + this._tileCounts[225];
				this.EvilTileCount = this._tileCounts[23] + this._tileCounts[661] + this._tileCounts[24] + this._tileCounts[25] + this._tileCounts[32] + this._tileCounts[112] + this._tileCounts[163] + this._tileCounts[400] + this._tileCounts[398] + -10 * this._tileCounts[27] + this._tileCounts[474];
				this.BloodTileCount = this._tileCounts[199] + this._tileCounts[662] + this._tileCounts[203] + this._tileCounts[200] + this._tileCounts[401] + this._tileCounts[399] + this._tileCounts[234] + this._tileCounts[352] - 10 * this._tileCounts[27] + this._tileCounts[195];
			}
			else
			{
				this.JungleTileCount = this._tileCounts[60] + this._tileCounts[61] + this._tileCounts[62] + this._tileCounts[74] + this._tileCounts[226] + this._tileCounts[225];
				this.EvilTileCount = this._tileCounts[23] + this._tileCounts[661] + this._tileCounts[24] + this._tileCounts[25] + this._tileCounts[32] + this._tileCounts[112] + this._tileCounts[163] + this._tileCounts[400] + this._tileCounts[398] + -10 * this._tileCounts[27];
				this.BloodTileCount = this._tileCounts[199] + this._tileCounts[662] + this._tileCounts[203] + this._tileCounts[200] + this._tileCounts[401] + this._tileCounts[399] + this._tileCounts[234] + this._tileCounts[352] - 10 * this._tileCounts[27];
			}
			this.MushroomTileCount = this._tileCounts[70] + this._tileCounts[71] + this._tileCounts[72] + this._tileCounts[528];
			this.MeteorTileCount = this._tileCounts[37];
			this.DungeonTileCount = this._tileCounts[41] + this._tileCounts[43] + this._tileCounts[44] + this._tileCounts[481] + this._tileCounts[482] + this._tileCounts[483];
			this.SandTileCount = this._tileCounts[53] + this._tileCounts[112] + this._tileCounts[116] + this._tileCounts[234] + this._tileCounts[397] + this._tileCounts[398] + this._tileCounts[402] + this._tileCounts[399] + this._tileCounts[396] + this._tileCounts[400] + this._tileCounts[403] + this._tileCounts[401];
			this.PartyMonolithCount = this._tileCounts[455];
			this.GraveyardTileCount = this._tileCounts[85];
			this.GraveyardTileCount -= this._tileCounts[27] / 2;
			if (this._tileCounts[27] > 0)
			{
				this.HasSunflower = true;
			}
			if (this.GraveyardTileCount > SceneMetrics.GraveyardTileMin)
			{
				this.HasSunflower = false;
			}
			if (this.GraveyardTileCount < 0)
			{
				this.GraveyardTileCount = 0;
			}
			if (this.HolyTileCount < 0)
			{
				this.HolyTileCount = 0;
			}
			if (this.EvilTileCount < 0)
			{
				this.EvilTileCount = 0;
			}
			if (this.BloodTileCount < 0)
			{
				this.BloodTileCount = 0;
			}
			int holyTileCount = this.HolyTileCount;
			this.HolyTileCount -= this.EvilTileCount;
			this.HolyTileCount -= this.BloodTileCount;
			this.EvilTileCount -= holyTileCount;
			this.BloodTileCount -= holyTileCount;
			if (this.HolyTileCount < 0)
			{
				this.HolyTileCount = 0;
			}
			if (this.EvilTileCount < 0)
			{
				this.EvilTileCount = 0;
			}
			if (this.BloodTileCount < 0)
			{
				this.BloodTileCount = 0;
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000DD99 File Offset: 0x0000BF99
		public int GetTileCount(ushort tileId)
		{
			return this._tileCounts[(int)tileId];
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000DDA4 File Offset: 0x0000BFA4
		public void Reset()
		{
			Array.Clear(this._tileCounts, 0, this._tileCounts.Length);
			Array.Clear(this._liquidCounts, 0, this._liquidCounts.Length);
			this.SandTileCount = 0;
			this.EvilTileCount = 0;
			this.BloodTileCount = 0;
			this.GraveyardTileCount = 0;
			this.MushroomTileCount = 0;
			this.SnowTileCount = 0;
			this.HolyTileCount = 0;
			this.MeteorTileCount = 0;
			this.JungleTileCount = 0;
			this.DungeonTileCount = 0;
			this.HasCampfire = false;
			this.HasSunflower = false;
			this.HasGardenGnome = false;
			this.HasStarInBottle = false;
			this.HasHeartLantern = false;
			this.HasClock = false;
			this.HasCatBast = false;
			this.ActiveMusicBox = -1;
			this.WaterCandleCount = 0;
			this.ActiveFountainColor = -1;
			this.ActiveMonolithType = -1;
			this.bestOre = -1;
			this.BloodMoonMonolith = false;
			this.MoonLordMonolith = false;
			this.EchoMonolith = false;
			this.ShimmerMonolithState = 0;
			Array.Clear(this.NPCBannerBuff, 0, this.NPCBannerBuff.Length);
			this.hasBanner = false;
			this.CanPlayCreditsRoll = false;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000DEB4 File Offset: 0x0000C0B4
		private void UpdateOreFinderData()
		{
			int num = -1;
			foreach (Point point in this._oreFinderTileLocations)
			{
				Tile tile = Main.tile[point.X, point.Y];
				if (SceneMetrics.IsValidForOreFinder(tile) && (num < 0 || Main.tileOreFinderPriority[(int)tile.type] > Main.tileOreFinderPriority[num]))
				{
					num = (int)tile.type;
					this.ClosestOrePosition = new Point?(point);
				}
			}
			this.bestOre = num;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000DF54 File Offset: 0x0000C154
		public static bool IsValidForOreFinder(Tile t)
		{
			return (t.type != 227 || (t.frameX >= 272 && t.frameX <= 374)) && (t.type != 129 || t.frameX >= 324) && Main.tileOreFinderPriority[(int)t.type] > 0;
		}

		// Token: 0x040000A6 RID: 166
		public static int ShimmerTileThreshold = 300;

		// Token: 0x040000A7 RID: 167
		public static int CorruptionTileThreshold = 300;

		// Token: 0x040000A8 RID: 168
		public static int CorruptionTileMax = 1000;

		// Token: 0x040000A9 RID: 169
		public static int CrimsonTileThreshold = 300;

		// Token: 0x040000AA RID: 170
		public static int CrimsonTileMax = 1000;

		// Token: 0x040000AB RID: 171
		public static int HallowTileThreshold = 125;

		// Token: 0x040000AC RID: 172
		public static int HallowTileMax = 600;

		// Token: 0x040000AD RID: 173
		public static int JungleTileThreshold = 140;

		// Token: 0x040000AE RID: 174
		public static int JungleTileMax = 700;

		// Token: 0x040000AF RID: 175
		public static int SnowTileThreshold = 1500;

		// Token: 0x040000B0 RID: 176
		public static int SnowTileMax = 6000;

		// Token: 0x040000B1 RID: 177
		public static int DesertTileThreshold = 1500;

		// Token: 0x040000B2 RID: 178
		public static int MushroomTileThreshold = 100;

		// Token: 0x040000B3 RID: 179
		public static int MushroomTileMax = 160;

		// Token: 0x040000B4 RID: 180
		public static int MeteorTileThreshold = 75;

		// Token: 0x040000B5 RID: 181
		public static int GraveyardTileMax = 36;

		// Token: 0x040000B6 RID: 182
		public static int GraveyardTileMin = 16;

		// Token: 0x040000B7 RID: 183
		public static int GraveyardTileThreshold = 28;

		// Token: 0x040000D6 RID: 214
		public bool CanPlayCreditsRoll;

		// Token: 0x040000D7 RID: 215
		public bool[] NPCBannerBuff = new bool[290];

		// Token: 0x040000D8 RID: 216
		public bool hasBanner;

		// Token: 0x040000D9 RID: 217
		private readonly int[] _tileCounts = new int[(int)TileID.Count];

		// Token: 0x040000DA RID: 218
		private readonly int[] _liquidCounts = new int[(int)LiquidID.Count];

		// Token: 0x040000DB RID: 219
		private readonly List<Point> _oreFinderTileLocations = new List<Point>(512);

		// Token: 0x040000DC RID: 220
		public int bestOre;
	}
}
