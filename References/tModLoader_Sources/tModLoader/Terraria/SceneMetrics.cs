using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Terraria
{
	// Token: 0x02000051 RID: 81
	public class SceneMetrics
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x003FA553 File Offset: 0x003F8753
		// (set) Token: 0x06000EAE RID: 3758 RVA: 0x003FA55B File Offset: 0x003F875B
		public Point? ClosestOrePosition { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x003FA564 File Offset: 0x003F8764
		// (set) Token: 0x06000EB0 RID: 3760 RVA: 0x003FA56C File Offset: 0x003F876C
		public int ShimmerTileCount { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x003FA575 File Offset: 0x003F8775
		// (set) Token: 0x06000EB2 RID: 3762 RVA: 0x003FA57D File Offset: 0x003F877D
		public int EvilTileCount { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x003FA586 File Offset: 0x003F8786
		// (set) Token: 0x06000EB4 RID: 3764 RVA: 0x003FA58E File Offset: 0x003F878E
		public int HolyTileCount { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x003FA597 File Offset: 0x003F8797
		// (set) Token: 0x06000EB6 RID: 3766 RVA: 0x003FA59F File Offset: 0x003F879F
		public int HoneyBlockCount { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x003FA5A8 File Offset: 0x003F87A8
		// (set) Token: 0x06000EB8 RID: 3768 RVA: 0x003FA5B0 File Offset: 0x003F87B0
		public int ActiveMusicBox { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x003FA5B9 File Offset: 0x003F87B9
		// (set) Token: 0x06000EBA RID: 3770 RVA: 0x003FA5C1 File Offset: 0x003F87C1
		public int SandTileCount { get; set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x003FA5CA File Offset: 0x003F87CA
		// (set) Token: 0x06000EBC RID: 3772 RVA: 0x003FA5D2 File Offset: 0x003F87D2
		public int MushroomTileCount { get; set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x003FA5DB File Offset: 0x003F87DB
		// (set) Token: 0x06000EBE RID: 3774 RVA: 0x003FA5E3 File Offset: 0x003F87E3
		public int SnowTileCount { get; set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x003FA5EC File Offset: 0x003F87EC
		// (set) Token: 0x06000EC0 RID: 3776 RVA: 0x003FA5F4 File Offset: 0x003F87F4
		public int WaterCandleCount { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x003FA5FD File Offset: 0x003F87FD
		// (set) Token: 0x06000EC2 RID: 3778 RVA: 0x003FA605 File Offset: 0x003F8805
		public int PeaceCandleCount { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x003FA60E File Offset: 0x003F880E
		// (set) Token: 0x06000EC4 RID: 3780 RVA: 0x003FA616 File Offset: 0x003F8816
		public int ShadowCandleCount { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x003FA61F File Offset: 0x003F881F
		// (set) Token: 0x06000EC6 RID: 3782 RVA: 0x003FA627 File Offset: 0x003F8827
		public int PartyMonolithCount { get; set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x003FA630 File Offset: 0x003F8830
		// (set) Token: 0x06000EC8 RID: 3784 RVA: 0x003FA638 File Offset: 0x003F8838
		public int MeteorTileCount { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x003FA641 File Offset: 0x003F8841
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x003FA649 File Offset: 0x003F8849
		public int BloodTileCount { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x003FA652 File Offset: 0x003F8852
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x003FA65A File Offset: 0x003F885A
		public int JungleTileCount { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x003FA663 File Offset: 0x003F8863
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x003FA66B File Offset: 0x003F886B
		public int DungeonTileCount { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x003FA674 File Offset: 0x003F8874
		// (set) Token: 0x06000ED0 RID: 3792 RVA: 0x003FA67C File Offset: 0x003F887C
		public bool HasSunflower { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x003FA685 File Offset: 0x003F8885
		// (set) Token: 0x06000ED2 RID: 3794 RVA: 0x003FA68D File Offset: 0x003F888D
		public bool HasGardenGnome { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x003FA696 File Offset: 0x003F8896
		// (set) Token: 0x06000ED4 RID: 3796 RVA: 0x003FA69E File Offset: 0x003F889E
		public bool HasClock { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x003FA6A7 File Offset: 0x003F88A7
		// (set) Token: 0x06000ED6 RID: 3798 RVA: 0x003FA6AF File Offset: 0x003F88AF
		public bool HasCampfire { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x003FA6B8 File Offset: 0x003F88B8
		// (set) Token: 0x06000ED8 RID: 3800 RVA: 0x003FA6C0 File Offset: 0x003F88C0
		public bool HasStarInBottle { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x003FA6C9 File Offset: 0x003F88C9
		// (set) Token: 0x06000EDA RID: 3802 RVA: 0x003FA6D1 File Offset: 0x003F88D1
		public bool HasHeartLantern { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x003FA6DA File Offset: 0x003F88DA
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x003FA6E2 File Offset: 0x003F88E2
		public int ActiveFountainColor { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x003FA6EB File Offset: 0x003F88EB
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x003FA6F3 File Offset: 0x003F88F3
		public int ActiveMonolithType { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x003FA6FC File Offset: 0x003F88FC
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x003FA704 File Offset: 0x003F8904
		public bool BloodMoonMonolith { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x003FA70D File Offset: 0x003F890D
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x003FA715 File Offset: 0x003F8915
		public bool MoonLordMonolith { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x003FA71E File Offset: 0x003F891E
		// (set) Token: 0x06000EE4 RID: 3812 RVA: 0x003FA726 File Offset: 0x003F8926
		public bool EchoMonolith { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x003FA72F File Offset: 0x003F892F
		// (set) Token: 0x06000EE6 RID: 3814 RVA: 0x003FA737 File Offset: 0x003F8937
		public int ShimmerMonolithState { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x003FA740 File Offset: 0x003F8940
		// (set) Token: 0x06000EE8 RID: 3816 RVA: 0x003FA748 File Offset: 0x003F8948
		public bool HasCatBast { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x003FA751 File Offset: 0x003F8951
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x003FA759 File Offset: 0x003F8959
		public int GraveyardTileCount { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x003FA762 File Offset: 0x003F8962
		public bool EnoughTilesForShimmer
		{
			get
			{
				return this.ShimmerTileCount >= SceneMetrics.ShimmerTileThreshold;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x003FA774 File Offset: 0x003F8974
		public bool EnoughTilesForJungle
		{
			get
			{
				return this.JungleTileCount >= SceneMetrics.JungleTileThreshold;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x003FA786 File Offset: 0x003F8986
		public bool EnoughTilesForHallow
		{
			get
			{
				return this.HolyTileCount >= SceneMetrics.HallowTileThreshold;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x003FA798 File Offset: 0x003F8998
		public bool EnoughTilesForSnow
		{
			get
			{
				return this.SnowTileCount >= SceneMetrics.SnowTileThreshold;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x003FA7AA File Offset: 0x003F89AA
		public bool EnoughTilesForGlowingMushroom
		{
			get
			{
				return this.MushroomTileCount >= SceneMetrics.MushroomTileThreshold;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x003FA7BC File Offset: 0x003F89BC
		public bool EnoughTilesForDesert
		{
			get
			{
				return this.SandTileCount >= SceneMetrics.DesertTileThreshold;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x003FA7CE File Offset: 0x003F89CE
		public bool EnoughTilesForCorruption
		{
			get
			{
				return this.EvilTileCount >= SceneMetrics.CorruptionTileThreshold;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x003FA7E0 File Offset: 0x003F89E0
		public bool EnoughTilesForCrimson
		{
			get
			{
				return this.BloodTileCount >= SceneMetrics.CrimsonTileThreshold;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x003FA7F2 File Offset: 0x003F89F2
		public bool EnoughTilesForMeteor
		{
			get
			{
				return this.MeteorTileCount >= SceneMetrics.MeteorTileThreshold;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x003FA804 File Offset: 0x003F8A04
		public bool EnoughTilesForGraveyard
		{
			get
			{
				return this.GraveyardTileCount >= SceneMetrics.GraveyardTileThreshold;
			}
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x003FA818 File Offset: 0x003F8A18
		public SceneMetrics()
		{
			this.Reset();
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x003FA874 File Offset: 0x003F8A74
		public unsafe void ScanAndExportToMain(SceneMetricsScanSettings settings)
		{
			this.Reset();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (settings.ScanOreFinderData)
			{
				this._oreFinderTileLocations.Clear();
			}
			SystemLoader.ResetNearbyTileEffects();
			if (settings.BiomeScanCenterPositionInWorld != null)
			{
				Point point = settings.BiomeScanCenterPositionInWorld.Value.ToTileCoordinates();
				Rectangle tileRectangle;
				tileRectangle..ctor(point.X - Main.buffScanAreaWidth / 2, point.Y - Main.buffScanAreaHeight / 2, Main.buffScanAreaWidth, Main.buffScanAreaHeight);
				tileRectangle = WorldUtils.ClampToWorld(tileRectangle);
				for (int i = tileRectangle.Left; i < tileRectangle.Right; i++)
				{
					for (int j = tileRectangle.Top; j < tileRectangle.Bottom; j++)
					{
						if (tileRectangle.Contains(i, j))
						{
							Tile tile = Main.tile[i, j];
							if (!(tile == null))
							{
								if (!tile.active())
								{
									if (*tile.liquid > 0)
									{
										this._liquidCounts[(int)tile.liquidType()]++;
									}
								}
								else
								{
									tileRectangle.Contains(i, j);
									if (!TileID.Sets.isDesertBiomeSand[(int)(*tile.type)] || !WorldGen.oceanDepths(i, j))
									{
										this._tileCounts[(int)(*tile.type)]++;
									}
									if (*tile.type == 215 && *tile.frameY < 36)
									{
										this.HasCampfire = true;
									}
									if (*tile.type == 49 && *tile.frameX < 18)
									{
										num++;
									}
									if (*tile.type == 372 && *tile.frameX < 18)
									{
										num2++;
									}
									if (*tile.type == 646 && *tile.frameX < 18)
									{
										num3++;
									}
									if (*tile.type == 405 && *tile.frameX < 54)
									{
										this.HasCampfire = true;
									}
									if (*tile.type == 506 && *tile.frameX < 72)
									{
										this.HasCatBast = true;
									}
									if (*tile.type == 42 && *tile.frameY >= 324 && *tile.frameY <= 358)
									{
										this.HasHeartLantern = true;
									}
									if (*tile.type == 42 && *tile.frameY >= 252 && *tile.frameY <= 286)
									{
										this.HasStarInBottle = true;
									}
									if (*tile.type == 91 && (*tile.frameX >= 396 || *tile.frameY >= 54))
									{
										int num4 = (int)(*tile.frameX / 18 - 21);
										for (int num5 = (int)(*tile.frameY); num5 >= 54; num5 -= 54)
										{
											num4 += 90;
											num4 += 21;
										}
										int num6 = Item.BannerToItem(num4);
										if (ItemID.Sets.BannerStrength.IndexInRange(num6) && ItemID.Sets.BannerStrength[num6].Enabled)
										{
											this.NPCBannerBuff[num4] = true;
											this.hasBanner = true;
										}
									}
									if (settings.ScanOreFinderData && Main.tileOreFinderPriority[(int)(*tile.type)] != 0)
									{
										this._oreFinderTileLocations.Add(new Point(i, j));
									}
									TileLoader.NearbyEffects(i, j, (int)(*tile.type), false);
								}
							}
						}
					}
				}
			}
			if (settings.VisualScanArea != null)
			{
				Rectangle rectangle = WorldUtils.ClampToWorld(settings.VisualScanArea.Value);
				for (int k = rectangle.Left; k < rectangle.Right; k++)
				{
					for (int l = rectangle.Top; l < rectangle.Bottom; l++)
					{
						Tile tile2 = Main.tile[k, l];
						if (!(tile2 == null) && tile2.active())
						{
							if (TileID.Sets.Clock[(int)(*tile2.type)])
							{
								this.HasClock = true;
							}
							ushort num7 = *tile2.type;
							if (num7 <= 410)
							{
								if (num7 != 139)
								{
									if (num7 != 207)
									{
										if (num7 == 410)
										{
											if (*tile2.frameY >= 56)
											{
												int activeMonolithType = (int)(*tile2.frameX / 36);
												this.ActiveMonolithType = activeMonolithType;
											}
										}
									}
									else if (*tile2.frameY >= 72)
									{
										switch (*tile2.frameX / 36)
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
								else if (*tile2.frameX >= 36)
								{
									this.ActiveMusicBox = (int)(*tile2.frameY / 36);
								}
							}
							else if (num7 <= 509)
							{
								if (num7 != 480)
								{
									if (num7 == 509)
									{
										if (*tile2.frameY >= 56)
										{
											this.ActiveMonolithType = 4;
										}
									}
								}
								else if (*tile2.frameY >= 54)
								{
									this.BloodMoonMonolith = true;
								}
							}
							else if (num7 != 657)
							{
								if (num7 == 658)
								{
									int shimmerMonolithState = (int)(*tile2.frameY / 54);
									this.ShimmerMonolithState = shimmerMonolithState;
								}
							}
							else if (*tile2.frameY >= 54)
							{
								this.EchoMonolith = true;
							}
							if (MusicLoader.tileToMusic.ContainsKey((int)(*tile2.type)) && MusicLoader.tileToMusic[(int)(*tile2.type)].ContainsKey((int)(*tile2.frameY)) && *tile2.frameX == 36)
							{
								this.ActiveMusicBox = MusicLoader.tileToMusic[(int)(*tile2.type)][(int)(*tile2.frameY)];
							}
							TileLoader.NearbyEffects(k, l, (int)(*tile2.type), true);
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
			SystemLoader.TileCountsAvailable(this._tileCounts);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x003FAF2C File Offset: 0x003F912C
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
			this.MeteorTileCount = this._tileCounts[37];
			this.PartyMonolithCount = this._tileCounts[455];
			this.GraveyardTileCount = this._tileCounts[85];
			this.GraveyardTileCount -= this._tileCounts[27] / 2;
			TileLoader.RecountTiles(this);
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

		// Token: 0x06000EF8 RID: 3832 RVA: 0x003FB0B0 File Offset: 0x003F92B0
		public int GetTileCount(ushort tileId)
		{
			return this._tileCounts[(int)tileId];
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x003FB0BA File Offset: 0x003F92BA
		public int GetLiquidCount(short liquidType)
		{
			return this._liquidCounts[(int)liquidType];
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x003FB0C4 File Offset: 0x003F92C4
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
			if (this.NPCBannerBuff.Length < NPCLoader.NPCCount)
			{
				Array.Resize<bool>(ref this.NPCBannerBuff, NPCLoader.NPCCount);
			}
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x003FB1F0 File Offset: 0x003F93F0
		private unsafe void UpdateOreFinderData()
		{
			int num = -1;
			foreach (Point oreFinderTileLocation in this._oreFinderTileLocations)
			{
				Tile tile = Main.tile[oreFinderTileLocation.X, oreFinderTileLocation.Y];
				if (SceneMetrics.IsValidForOreFinder(tile) && (num < 0 || Main.tileOreFinderPriority[(int)(*tile.type)] > Main.tileOreFinderPriority[num]))
				{
					num = (int)(*tile.type);
					this.ClosestOrePosition = new Point?(oreFinderTileLocation);
				}
			}
			this.bestOre = num;
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x003FB294 File Offset: 0x003F9494
		public unsafe static bool IsValidForOreFinder(Tile t)
		{
			return (*t.type != 227 || (*t.frameX >= 272 && *t.frameX <= 374)) && (*t.type != 129 || *t.frameX >= 324) && Main.tileOreFinderPriority[(int)(*t.type)] > 0;
		}

		// Token: 0x04000E71 RID: 3697
		public static int ShimmerTileThreshold = 300;

		// Token: 0x04000E72 RID: 3698
		public static int CorruptionTileThreshold = 300;

		// Token: 0x04000E73 RID: 3699
		public static int CorruptionTileMax = 1000;

		// Token: 0x04000E74 RID: 3700
		public static int CrimsonTileThreshold = 300;

		// Token: 0x04000E75 RID: 3701
		public static int CrimsonTileMax = 1000;

		// Token: 0x04000E76 RID: 3702
		public static int HallowTileThreshold = 125;

		// Token: 0x04000E77 RID: 3703
		public static int HallowTileMax = 600;

		// Token: 0x04000E78 RID: 3704
		public static int JungleTileThreshold = 140;

		// Token: 0x04000E79 RID: 3705
		public static int JungleTileMax = 700;

		// Token: 0x04000E7A RID: 3706
		public static int SnowTileThreshold = 1500;

		// Token: 0x04000E7B RID: 3707
		public static int SnowTileMax = 6000;

		// Token: 0x04000E7C RID: 3708
		public static int DesertTileThreshold = 1500;

		// Token: 0x04000E7D RID: 3709
		public static int MushroomTileThreshold = 100;

		// Token: 0x04000E7E RID: 3710
		public static int MushroomTileMax = 160;

		// Token: 0x04000E7F RID: 3711
		public static int MeteorTileThreshold = 75;

		// Token: 0x04000E80 RID: 3712
		public static int GraveyardTileMax = 36;

		// Token: 0x04000E81 RID: 3713
		public static int GraveyardTileMin = 16;

		// Token: 0x04000E82 RID: 3714
		public static int GraveyardTileThreshold = 28;

		// Token: 0x04000E83 RID: 3715
		public bool CanPlayCreditsRoll;

		/// <summary>
		/// Indexed by BannerIDs, indicates which enemies are currently subject to the banner damage bonus. Set to true in conjunction with <see cref="F:Terraria.SceneMetrics.NPCBannerBuff" />.
		/// </summary>
		// Token: 0x04000E84 RID: 3716
		public bool[] NPCBannerBuff = new bool[290];

		/// <summary>
		/// Indicates that the player is in range of a buff providing Banner tile and should receive the <see cref="F:Terraria.ID.BuffID.MonsterBanner" /> buff. Set to true in conjunction with <see cref="F:Terraria.SceneMetrics.NPCBannerBuff" />.
		/// </summary>
		// Token: 0x04000E85 RID: 3717
		public bool hasBanner;

		// Token: 0x04000E86 RID: 3718
		internal int[] _tileCounts = new int[TileLoader.TileCount];

		// Token: 0x04000E87 RID: 3719
		private readonly int[] _liquidCounts = new int[(int)LiquidID.Count];

		// Token: 0x04000E88 RID: 3720
		private readonly List<Point> _oreFinderTileLocations = new List<Point>(512);

		// Token: 0x04000E89 RID: 3721
		public int bestOre;
	}
}
