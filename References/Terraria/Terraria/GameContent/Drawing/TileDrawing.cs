using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.GameContent.Liquid;
using Terraria.GameContent.Tile_Entities;
using Terraria.Graphics;
using Terraria.Graphics.Capture;
using Terraria.ID;
using Terraria.ObjectData;
using Terraria.UI;
using Terraria.Utilities;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x020002B2 RID: 690
	public class TileDrawing
	{
		// Token: 0x060021B3 RID: 8627 RVA: 0x0052B474 File Offset: 0x00529674
		private void AddSpecialPoint(int x, int y, TileDrawing.TileCounterType type)
		{
			Point[] array = this._specialPositions[(int)type];
			int[] specialsCount = this._specialsCount;
			int num = specialsCount[(int)type];
			specialsCount[(int)type] = num + 1;
			array[num] = new Point(x, y);
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060021B4 RID: 8628 RVA: 0x0052B4A9 File Offset: 0x005296A9
		private bool[] _tileSolid
		{
			get
			{
				return Main.tileSolid;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x0052B4B0 File Offset: 0x005296B0
		private bool[] _tileSolidTop
		{
			get
			{
				return Main.tileSolidTop;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060021B6 RID: 8630 RVA: 0x0052B4B7 File Offset: 0x005296B7
		private Dust[] _dust
		{
			get
			{
				return Main.dust;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x0052B4BE File Offset: 0x005296BE
		private Gore[] _gore
		{
			get
			{
				return Main.gore;
			}
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x0052B4C8 File Offset: 0x005296C8
		public TileDrawing(TilePaintSystemV2 paintSystem)
		{
			this._paintSystem = paintSystem;
			this._rand = new UnifiedRandom();
			for (int i = 0; i < this._specialPositions.Length; i++)
			{
				this._specialPositions[i] = new Point[9000];
			}
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x0052B714 File Offset: 0x00529914
		public void PreparePaintForTilesOnScreen()
		{
			if (Main.GameUpdateCount % 6U > 0U)
			{
				return;
			}
			Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
			Vector2 zero = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
			int firstTileX;
			int lastTileX;
			int firstTileY;
			int lastTileY;
			this.GetScreenDrawArea(unscaledPosition, zero + (Main.Camera.UnscaledPosition - Main.Camera.ScaledPosition), out firstTileX, out lastTileX, out firstTileY, out lastTileY);
			this.PrepareForAreaDrawing(firstTileX, lastTileX, firstTileY, lastTileY, true);
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x0052B794 File Offset: 0x00529994
		public void PrepareForAreaDrawing(int firstTileX, int lastTileX, int firstTileY, int lastTileY, bool prepareLazily)
		{
			TilePaintSystemV2.TileVariationkey tileVariationkey = default(TilePaintSystemV2.TileVariationkey);
			TilePaintSystemV2.WallVariationKey wallVariationKey = default(TilePaintSystemV2.WallVariationKey);
			for (int i = firstTileY; i < lastTileY + 4; i++)
			{
				for (int j = firstTileX - 2; j < lastTileX + 2; j++)
				{
					Tile tile = Main.tile[j, i];
					if (tile != null)
					{
						if (tile.active())
						{
							Main.instance.LoadTiles((int)tile.type);
							tileVariationkey.TileType = (int)tile.type;
							tileVariationkey.PaintColor = (int)tile.color();
							int tileStyle = 0;
							ushort type = tile.type;
							if (type != 5)
							{
								if (type == 323)
								{
									tileStyle = this.GetPalmTreeBiome(j, i);
								}
							}
							else
							{
								tileStyle = TileDrawing.GetTreeBiome(j, i, (int)tile.frameX, (int)tile.frameY);
							}
							tileVariationkey.TileStyle = tileStyle;
							if (tileVariationkey.PaintColor != 0)
							{
								this._paintSystem.RequestTile(ref tileVariationkey);
							}
						}
						if (tile.wall != 0)
						{
							Main.instance.LoadWall((int)tile.wall);
							wallVariationKey.WallType = (int)tile.wall;
							wallVariationKey.PaintColor = (int)tile.wallColor();
							if (wallVariationKey.PaintColor != 0)
							{
								this._paintSystem.RequestWall(ref wallVariationKey);
							}
						}
						if (!prepareLazily)
						{
							this.MakeExtraPreparations(tile, j, i);
						}
					}
				}
			}
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x0052B8E0 File Offset: 0x00529AE0
		private void MakeExtraPreparations(Tile tile, int x, int y)
		{
			ushort type = tile.type;
			if (type <= 589)
			{
				if (type != 5)
				{
					if (type != 323)
					{
						if (type - 583 > 6)
						{
							return;
						}
						int num = 0;
						int num2 = 0;
						int num3 = 0;
						int num4 = 0;
						int textureIndex = 0;
						int xoffset = (tile.frameX == 44).ToInt() - (tile.frameX == 66).ToInt();
						if (WorldGen.GetGemTreeFoliageData(x, y, xoffset, ref num, ref textureIndex, out num2, out num3, out num4))
						{
							TilePaintSystemV2.TreeFoliageVariantKey treeFoliageVariantKey = new TilePaintSystemV2.TreeFoliageVariantKey
							{
								TextureIndex = textureIndex,
								PaintColor = (int)tile.color()
							};
							this._paintSystem.RequestTreeTop(ref treeFoliageVariantKey);
							this._paintSystem.RequestTreeBranch(ref treeFoliageVariantKey);
							return;
						}
					}
					else
					{
						int textureIndex2 = 15;
						if (x >= WorldGen.beachDistance && x <= Main.maxTilesX - WorldGen.beachDistance)
						{
							textureIndex2 = 21;
						}
						TilePaintSystemV2.TreeFoliageVariantKey treeFoliageVariantKey2 = new TilePaintSystemV2.TreeFoliageVariantKey
						{
							TextureIndex = textureIndex2,
							PaintColor = (int)tile.color()
						};
						this._paintSystem.RequestTreeTop(ref treeFoliageVariantKey2);
						this._paintSystem.RequestTreeBranch(ref treeFoliageVariantKey2);
					}
				}
				else
				{
					int num5 = 0;
					int num6 = 0;
					int num7 = 0;
					int num8 = 0;
					int textureIndex3 = 0;
					int xoffset2 = (tile.frameX == 44).ToInt() - (tile.frameX == 66).ToInt();
					if (WorldGen.GetCommonTreeFoliageData(x, y, xoffset2, ref num5, ref textureIndex3, out num6, out num7, out num8))
					{
						TilePaintSystemV2.TreeFoliageVariantKey treeFoliageVariantKey3 = new TilePaintSystemV2.TreeFoliageVariantKey
						{
							TextureIndex = textureIndex3,
							PaintColor = (int)tile.color()
						};
						this._paintSystem.RequestTreeTop(ref treeFoliageVariantKey3);
						this._paintSystem.RequestTreeBranch(ref treeFoliageVariantKey3);
						return;
					}
				}
			}
			else if (type != 596 && type != 616)
			{
				if (type != 634)
				{
					return;
				}
				int num9 = 0;
				int num10 = 0;
				int num11 = 0;
				int num12 = 0;
				int textureIndex4 = 0;
				int xoffset3 = (tile.frameX == 44).ToInt() - (tile.frameX == 66).ToInt();
				if (WorldGen.GetAshTreeFoliageData(x, y, xoffset3, ref num9, ref textureIndex4, out num10, out num11, out num12))
				{
					TilePaintSystemV2.TreeFoliageVariantKey treeFoliageVariantKey4 = new TilePaintSystemV2.TreeFoliageVariantKey
					{
						TextureIndex = textureIndex4,
						PaintColor = (int)tile.color()
					};
					this._paintSystem.RequestTreeTop(ref treeFoliageVariantKey4);
					this._paintSystem.RequestTreeBranch(ref treeFoliageVariantKey4);
					return;
				}
			}
			else
			{
				int num13 = 0;
				int num14 = 0;
				int num15 = 0;
				int num16 = 0;
				int textureIndex5 = 0;
				int xoffset4 = (tile.frameX == 44).ToInt() - (tile.frameX == 66).ToInt();
				if (WorldGen.GetVanityTreeFoliageData(x, y, xoffset4, ref num13, ref textureIndex5, out num14, out num15, out num16))
				{
					TilePaintSystemV2.TreeFoliageVariantKey treeFoliageVariantKey5 = new TilePaintSystemV2.TreeFoliageVariantKey
					{
						TextureIndex = textureIndex5,
						PaintColor = (int)tile.color()
					};
					this._paintSystem.RequestTreeTop(ref treeFoliageVariantKey5);
					this._paintSystem.RequestTreeBranch(ref treeFoliageVariantKey5);
					return;
				}
			}
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x0052BBA8 File Offset: 0x00529DA8
		public void Update()
		{
			if (Main.dedServ)
			{
				return;
			}
			double num = (double)Math.Abs(Main.WindForVisuals);
			num = (double)Utils.GetLerpValue(0.08f, 1.2f, (float)num, true);
			this._treeWindCounter += 0.004166666666666667 + 0.004166666666666667 * num * 2.0;
			this._grassWindCounter += 0.005555555555555556 + 0.005555555555555556 * num * 4.0;
			this._sunflowerWindCounter += 0.002380952380952381 + 0.002380952380952381 * num * 5.0;
			this._vineWindCounter += 0.008333333333333333 + 0.008333333333333333 * num * 0.4000000059604645;
			this.UpdateLeafFrequency();
			this.EnsureWindGridSize();
			this._windGrid.Update();
			this._shouldShowInvisibleBlocks = Main.ShouldShowInvisibleWalls();
			if (this._shouldShowInvisibleBlocks_LastFrame != this._shouldShowInvisibleBlocks)
			{
				this._shouldShowInvisibleBlocks_LastFrame = this._shouldShowInvisibleBlocks;
				Main.sectionManager.SetAllFramedSectionsAsNeedingRefresh();
			}
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x0052BCD3 File Offset: 0x00529ED3
		public void SpecificHacksForCapture()
		{
			Main.sectionManager.SetAllFramedSectionsAsNeedingRefresh();
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x0052BCE0 File Offset: 0x00529EE0
		public void PreDrawTiles(bool solidLayer, bool forRenderTargets, bool intoRenderTargets)
		{
			bool flag = intoRenderTargets || Lighting.UpdateEveryFrame;
			if (!solidLayer && flag)
			{
				this._specialsCount[5] = 0;
				this._specialsCount[4] = 0;
				this._specialsCount[8] = 0;
				this._specialsCount[6] = 0;
				this._specialsCount[3] = 0;
				this._specialsCount[12] = 0;
				this._specialsCount[0] = 0;
				this._specialsCount[9] = 0;
				this._specialsCount[10] = 0;
				this._specialsCount[11] = 0;
			}
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x0052BD60 File Offset: 0x00529F60
		public void PostDrawTiles(bool solidLayer, bool forRenderTargets, bool intoRenderTargets)
		{
			if (!solidLayer && !intoRenderTargets)
			{
				Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
				this.DrawMultiTileVines();
				this.DrawMultiTileGrass();
				this.DrawVoidLenses();
				this.DrawTeleportationPylons();
				this.DrawMasterTrophies();
				this.DrawGrass();
				this.DrawAnyDirectionalGrass();
				this.DrawTrees();
				this.DrawVines();
				this.DrawReverseVines();
				Main.spriteBatch.End();
			}
			if (solidLayer && !intoRenderTargets)
			{
				this.DrawEntities_HatRacks();
				this.DrawEntities_DisplayDolls();
			}
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x0052BDF0 File Offset: 0x00529FF0
		public void DrawLiquidBehindTiles(int waterStyleOverride = -1)
		{
			Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
			Vector2 zero = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
			int num;
			int num2;
			int num3;
			int num4;
			this.GetScreenDrawArea(unscaledPosition, zero + (Main.Camera.UnscaledPosition - Main.Camera.ScaledPosition), out num, out num2, out num3, out num4);
			for (int i = num3; i < num4 + 4; i++)
			{
				for (int j = num - 2; j < num2 + 2; j++)
				{
					Tile tile = Main.tile[j, i];
					if (tile != null)
					{
						this.DrawTile_LiquidBehindTile(false, false, waterStyleOverride, unscaledPosition, zero, j, i, tile);
					}
				}
			}
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x0052BEA4 File Offset: 0x0052A0A4
		public void Draw(bool solidLayer, bool forRenderTargets, bool intoRenderTargets, int waterStyleOverride = -1)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			this._isActiveAndNotPaused = (!Main.gamePaused && Main.instance.IsActive);
			this._localPlayer = Main.LocalPlayer;
			Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
			Vector2 zero = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
			if (!solidLayer)
			{
				Main.critterCage = false;
			}
			this.EnsureWindGridSize();
			this.ClearLegacyCachedDraws();
			bool flag = intoRenderTargets || Main.LightingEveryFrame;
			if (flag)
			{
				this.ClearCachedTileDraws(solidLayer);
			}
			float num = 255f * (1f - Main.gfxQuality) + 30f * Main.gfxQuality;
			this._highQualityLightingRequirement.R = (byte)num;
			this._highQualityLightingRequirement.G = (byte)((double)num * 1.1);
			this._highQualityLightingRequirement.B = (byte)((double)num * 1.2);
			float num2 = 50f * (1f - Main.gfxQuality) + 2f * Main.gfxQuality;
			this._mediumQualityLightingRequirement.R = (byte)num2;
			this._mediumQualityLightingRequirement.G = (byte)((double)num2 * 1.1);
			this._mediumQualityLightingRequirement.B = (byte)((double)num2 * 1.2);
			int num3;
			int num4;
			int num5;
			int num6;
			this.GetScreenDrawArea(unscaledPosition, zero + (Main.Camera.UnscaledPosition - Main.Camera.ScaledPosition), out num3, out num4, out num5, out num6);
			byte b = (byte)(100f + 150f * Main.martianLight);
			this._martianGlow = new Color((int)b, (int)b, (int)b, 0);
			TileDrawInfo value = this._currentTileDrawInfo.Value;
			for (int i = num5; i < num6 + 4; i++)
			{
				for (int j = num3 - 2; j < num4 + 2; j++)
				{
					Tile tile = Main.tile[j, i];
					if (tile == null)
					{
						tile = new Tile();
						Main.tile[j, i] = tile;
						Main.mapTime += 60;
					}
					else if (tile.active() && this.IsTileDrawLayerSolid(tile.type) == solidLayer)
					{
						if (solidLayer)
						{
							this.DrawTile_LiquidBehindTile(solidLayer, false, waterStyleOverride, unscaledPosition, zero, j, i, tile);
						}
						ushort type = tile.type;
						short frameX = tile.frameX;
						short frameY = tile.frameY;
						if (!TextureAssets.Tile[(int)type].IsLoaded)
						{
							Main.instance.LoadTiles((int)type);
						}
						if (type <= 382)
						{
							if (type <= 115)
							{
								if (type <= 52)
								{
									if (type <= 34)
									{
										if (type != 27)
										{
											if (type != 34)
											{
												goto IL_839;
											}
											if (frameX % 54 == 0 && frameY % 54 == 0 && flag)
											{
												this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.MultiTileVine);
												goto IL_868;
											}
											goto IL_868;
										}
										else
										{
											if (frameX % 36 == 0 && frameY == 0 && flag)
											{
												this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.MultiTileGrass);
												goto IL_868;
											}
											goto IL_868;
										}
									}
									else
									{
										if (type == 42)
										{
											goto IL_581;
										}
										if (type != 52)
										{
											goto IL_839;
										}
									}
								}
								else if (type <= 91)
								{
									if (type != 62)
									{
										if (type != 91)
										{
											goto IL_839;
										}
										if (frameX % 18 == 0 && frameY % 54 == 0 && flag)
										{
											this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.MultiTileVine);
											goto IL_868;
										}
										goto IL_868;
									}
								}
								else
								{
									if (type == 95)
									{
										goto IL_5D3;
									}
									if (type != 115)
									{
										goto IL_839;
									}
								}
							}
							else if (type <= 233)
							{
								if (type <= 184)
								{
									if (type == 126)
									{
										goto IL_5D3;
									}
									if (type != 184)
									{
										goto IL_839;
									}
									if (flag)
									{
										this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.AnyDirectionalGrass);
										goto IL_868;
									}
									goto IL_868;
								}
								else if (type != 205)
								{
									if (type != 233)
									{
										goto IL_839;
									}
									if (frameY == 0 && frameX % 54 == 0 && flag)
									{
										this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.MultiTileGrass);
									}
									if (frameY == 34 && frameX % 36 == 0 && flag)
									{
										this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.MultiTileGrass);
										goto IL_868;
									}
									goto IL_868;
								}
							}
							else if (type <= 238)
							{
								if (type != 236 && type != 238)
								{
									goto IL_839;
								}
								if (frameX % 36 == 0 && frameY == 0 && flag)
								{
									this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.MultiTileGrass);
									goto IL_868;
								}
								goto IL_868;
							}
							else
							{
								if (type - 270 <= 1)
								{
									goto IL_581;
								}
								if (type - 373 <= 2)
								{
									goto IL_7BB;
								}
								if (type != 382)
								{
									goto IL_839;
								}
							}
						}
						else
						{
							if (type <= 572)
							{
								if (type <= 465)
								{
									if (type <= 454)
									{
										if (type == 444)
										{
											goto IL_5D3;
										}
										if (type != 454)
										{
											goto IL_839;
										}
										if (frameX % 72 == 0 && frameY % 54 == 0 && flag)
										{
											this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.MultiTileVine);
											goto IL_868;
										}
										goto IL_868;
									}
									else
									{
										if (type == 461)
										{
											goto IL_7BB;
										}
										if (type != 465)
										{
											goto IL_839;
										}
									}
								}
								else if (type <= 530)
								{
									switch (type)
									{
									case 485:
									case 489:
									case 490:
										if (frameY == 0 && frameX % 36 == 0 && flag)
										{
											this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.MultiTileGrass);
											goto IL_868;
										}
										goto IL_868;
									case 486:
									case 487:
									case 488:
									case 492:
										goto IL_839;
									case 491:
										if (flag && frameX == 18 && frameY == 18)
										{
											this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.VoidLens);
											goto IL_857;
										}
										goto IL_857;
									case 493:
										if (frameY == 0 && frameX % 18 == 0 && flag)
										{
											this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.MultiTileGrass);
											goto IL_868;
										}
										goto IL_868;
									default:
										switch (type)
										{
										case 519:
											if (frameX / 18 <= 4 && flag)
											{
												this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.MultiTileGrass);
												goto IL_868;
											}
											goto IL_868;
										case 520:
										case 529:
											goto IL_839;
										case 521:
										case 522:
										case 523:
										case 524:
										case 525:
										case 526:
										case 527:
											if (frameY == 0 && frameX % 36 == 0 && flag)
											{
												this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.MultiTileGrass);
												goto IL_868;
											}
											goto IL_868;
										case 528:
											goto IL_505;
										case 530:
											if (frameX >= 270)
											{
												goto IL_857;
											}
											if (frameX % 54 == 0 && frameY == 0 && flag)
											{
												this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.MultiTileGrass);
												goto IL_868;
											}
											goto IL_868;
										default:
											goto IL_839;
										}
										break;
									}
								}
								else if (type != 549)
								{
									if (type != 572)
									{
										goto IL_839;
									}
									goto IL_581;
								}
								else
								{
									if (flag)
									{
										this.CrawlToBottomOfReverseVineAndAddSpecialPoint(i, j);
										goto IL_868;
									}
									goto IL_868;
								}
							}
							else if (type <= 617)
							{
								if (type <= 592)
								{
									if (type == 581)
									{
										goto IL_581;
									}
									if (type - 591 > 1)
									{
										goto IL_839;
									}
								}
								else if (type != 597)
								{
									if (type != 617)
									{
										goto IL_839;
									}
									if (flag && frameX % 54 == 0 && frameY % 72 == 0)
									{
										this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.MasterTrophy);
										goto IL_857;
									}
									goto IL_857;
								}
								else
								{
									if (flag && frameX % 54 == 0 && frameY == 0)
									{
										this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.TeleportationPylon);
										goto IL_857;
									}
									goto IL_857;
								}
							}
							else if (type <= 638)
							{
								if (type != 636 && type != 638)
								{
									goto IL_839;
								}
								goto IL_505;
							}
							else if (type != 651)
							{
								if (type != 652)
								{
									if (type != 660)
									{
										goto IL_839;
									}
									goto IL_581;
								}
								else
								{
									if (frameX % 36 == 0 && flag)
									{
										this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.MultiTileGrass);
										goto IL_868;
									}
									goto IL_868;
								}
							}
							else
							{
								if (frameX % 54 == 0 && flag)
								{
									this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.MultiTileGrass);
									goto IL_868;
								}
								goto IL_868;
							}
							if (frameX % 36 == 0 && frameY % 54 == 0 && flag)
							{
								this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.MultiTileVine);
								goto IL_868;
							}
							goto IL_868;
						}
						IL_505:
						if (flag)
						{
							this.CrawlToTopOfVineAndAddSpecialPoint(i, j);
							goto IL_868;
						}
						goto IL_868;
						IL_581:
						if (frameX % 18 == 0 && frameY % 36 == 0 && flag)
						{
							this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.MultiTileVine);
							goto IL_868;
						}
						goto IL_868;
						IL_5D3:
						if (frameX % 36 == 0 && frameY % 36 == 0 && flag)
						{
							this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.MultiTileVine);
							goto IL_868;
						}
						goto IL_868;
						IL_7BB:
						this.EmitLiquidDrops(i, j, tile, type);
						goto IL_868;
						IL_839:
						if (this.ShouldSwayInWind(j, i, tile))
						{
							if (flag)
							{
								this.AddSpecialPoint(j, i, TileDrawing.TileCounterType.WindyGrass);
								goto IL_868;
							}
							goto IL_868;
						}
						IL_857:
						this.DrawSingleTile(value, solidLayer, waterStyleOverride, unscaledPosition, zero, j, i);
					}
					IL_868:;
				}
			}
			if (solidLayer)
			{
				Main.instance.DrawTileCracks(1, Main.player[Main.myPlayer].hitReplace);
				Main.instance.DrawTileCracks(1, Main.player[Main.myPlayer].hitTile);
			}
			this.DrawSpecialTilesLegacy(unscaledPosition, zero);
			if (TileObject.objectPreview.Active && this._localPlayer.cursorItemIconEnabled && Main.placementPreview && !CaptureManager.Instance.Active)
			{
				Main.instance.LoadTiles((int)TileObject.objectPreview.Type);
				TileObject.DrawPreview(Main.spriteBatch, TileObject.objectPreview, unscaledPosition - zero);
			}
			if (solidLayer)
			{
				TimeLogger.DrawTime(0, stopwatch.Elapsed.TotalMilliseconds);
				return;
			}
			TimeLogger.DrawTime(1, stopwatch.Elapsed.TotalMilliseconds);
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x0052C800 File Offset: 0x0052AA00
		private void CrawlToTopOfVineAndAddSpecialPoint(int j, int i)
		{
			int y = j;
			for (int k = j - 1; k > 0; k--)
			{
				Tile tile = Main.tile[i, k];
				if (WorldGen.SolidTile(i, k, false) || !tile.active())
				{
					y = k + 1;
					break;
				}
			}
			Point item = new Point(i, y);
			if (this._vineRootsPositions.Contains(item))
			{
				return;
			}
			this._vineRootsPositions.Add(item);
			this.AddSpecialPoint(i, y, TileDrawing.TileCounterType.Vine);
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x0052C870 File Offset: 0x0052AA70
		private void CrawlToBottomOfReverseVineAndAddSpecialPoint(int j, int i)
		{
			int y = j;
			for (int k = j; k < Main.maxTilesY; k++)
			{
				Tile tile = Main.tile[i, k];
				if (WorldGen.SolidTile(i, k, false) || !tile.active())
				{
					y = k - 1;
					break;
				}
			}
			Point item = new Point(i, y);
			if (this._reverseVineRootsPositions.Contains(item))
			{
				return;
			}
			this._reverseVineRootsPositions.Add(item);
			this.AddSpecialPoint(i, y, TileDrawing.TileCounterType.ReverseVine);
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x0052C8E4 File Offset: 0x0052AAE4
		private void DrawSingleTile(TileDrawInfo drawData, bool solidLayer, int waterStyleOverride, Vector2 screenPosition, Vector2 screenOffset, int tileX, int tileY)
		{
			drawData.tileCache = Main.tile[tileX, tileY];
			drawData.typeCache = drawData.tileCache.type;
			drawData.tileFrameX = drawData.tileCache.frameX;
			drawData.tileFrameY = drawData.tileCache.frameY;
			drawData.tileLight = Lighting.GetColor(tileX, tileY);
			if (drawData.tileCache.liquid > 0 && drawData.tileCache.type == 518)
			{
				return;
			}
			this.GetTileDrawData(tileX, tileY, drawData.tileCache, drawData.typeCache, ref drawData.tileFrameX, ref drawData.tileFrameY, out drawData.tileWidth, out drawData.tileHeight, out drawData.tileTop, out drawData.halfBrickHeight, out drawData.addFrX, out drawData.addFrY, out drawData.tileSpriteEffect, out drawData.glowTexture, out drawData.glowSourceRect, out drawData.glowColor);
			drawData.drawTexture = this.GetTileDrawTexture(drawData.tileCache, tileX, tileY);
			Texture2D texture2D = null;
			Rectangle empty = Rectangle.Empty;
			Color transparent = Color.Transparent;
			if (TileID.Sets.HasOutlines[(int)drawData.typeCache])
			{
				this.GetTileOutlineInfo(tileX, tileY, drawData.typeCache, ref drawData.tileLight, ref texture2D, ref transparent);
			}
			if (this._localPlayer.dangerSense && TileDrawing.IsTileDangerous(this._localPlayer, drawData.tileCache, drawData.typeCache))
			{
				if (drawData.tileLight.R < 255)
				{
					drawData.tileLight.R = byte.MaxValue;
				}
				if (drawData.tileLight.G < 50)
				{
					drawData.tileLight.G = 50;
				}
				if (drawData.tileLight.B < 50)
				{
					drawData.tileLight.B = 50;
				}
				if (this._isActiveAndNotPaused && this._rand.Next(30) == 0)
				{
					int num = Dust.NewDust(new Vector2((float)(tileX * 16), (float)(tileY * 16)), 16, 16, 60, 0f, 0f, 100, default(Color), 0.3f);
					this._dust[num].fadeIn = 1f;
					this._dust[num].velocity *= 0.1f;
					this._dust[num].noLight = true;
					this._dust[num].noGravity = true;
				}
			}
			if (this._localPlayer.findTreasure && Main.IsTileSpelunkable(drawData.typeCache, drawData.tileFrameX, drawData.tileFrameY))
			{
				if (drawData.tileLight.R < 200)
				{
					drawData.tileLight.R = 200;
				}
				if (drawData.tileLight.G < 170)
				{
					drawData.tileLight.G = 170;
				}
				if (this._isActiveAndNotPaused && this._rand.Next(60) == 0)
				{
					int num2 = Dust.NewDust(new Vector2((float)(tileX * 16), (float)(tileY * 16)), 16, 16, 204, 0f, 0f, 150, default(Color), 0.3f);
					this._dust[num2].fadeIn = 1f;
					this._dust[num2].velocity *= 0.1f;
					this._dust[num2].noLight = true;
				}
			}
			if (this._localPlayer.biomeSight)
			{
				Color white = Color.White;
				if (Main.IsTileBiomeSightable(drawData.typeCache, drawData.tileFrameX, drawData.tileFrameY, ref white))
				{
					if (drawData.tileLight.R < white.R)
					{
						drawData.tileLight.R = white.R;
					}
					if (drawData.tileLight.G < white.G)
					{
						drawData.tileLight.G = white.G;
					}
					if (drawData.tileLight.B < white.B)
					{
						drawData.tileLight.B = white.B;
					}
					if (this._isActiveAndNotPaused && this._rand.Next(480) == 0)
					{
						Color newColor = white;
						int num3 = Dust.NewDust(new Vector2((float)(tileX * 16), (float)(tileY * 16)), 16, 16, 267, 0f, 0f, 150, newColor, 0.3f);
						this._dust[num3].noGravity = true;
						this._dust[num3].fadeIn = 1f;
						this._dust[num3].velocity *= 0.1f;
						this._dust[num3].noLightEmittence = true;
					}
				}
			}
			if (this._isActiveAndNotPaused)
			{
				if (!Lighting.UpdateEveryFrame || new FastRandom(Main.TileFrameSeed).WithModifier(tileX, tileY).Next(4) == 0)
				{
					this.DrawTiles_EmitParticles(tileY, tileX, drawData.tileCache, drawData.typeCache, drawData.tileFrameX, drawData.tileFrameY, drawData.tileLight);
				}
				drawData.tileLight = this.DrawTiles_GetLightOverride(tileY, tileX, drawData.tileCache, drawData.typeCache, drawData.tileFrameX, drawData.tileFrameY, drawData.tileLight);
			}
			bool flag = false;
			if (drawData.tileLight.R >= 1 || drawData.tileLight.G >= 1 || drawData.tileLight.B >= 1)
			{
				flag = true;
			}
			if (drawData.tileCache.wall > 0 && (drawData.tileCache.wall == 318 || drawData.tileCache.fullbrightWall()))
			{
				flag = true;
			}
			flag &= this.IsVisible(drawData.tileCache);
			this.CacheSpecialDraws_Part1(tileX, tileY, (int)drawData.typeCache, (int)drawData.tileFrameX, (int)drawData.tileFrameY, !flag);
			this.CacheSpecialDraws_Part2(tileX, tileY, drawData, !flag);
			if (drawData.typeCache == 72 && drawData.tileFrameX >= 36)
			{
				int num4 = 0;
				if (drawData.tileFrameY == 18)
				{
					num4 = 1;
				}
				else if (drawData.tileFrameY == 36)
				{
					num4 = 2;
				}
				Main.spriteBatch.Draw(TextureAssets.ShroomCap.Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X - 22), (float)(tileY * 16 - (int)screenPosition.Y - 26)) + screenOffset, new Rectangle?(new Rectangle(num4 * 62, 0, 60, 42)), Lighting.GetColor(tileX, tileY), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			Rectangle rectangle = new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight - drawData.halfBrickHeight);
			Vector2 vector = new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop + drawData.halfBrickHeight)) + screenOffset;
			if (flag)
			{
				drawData.colorTint = Color.White;
				drawData.finalColor = TileDrawing.GetFinalLight(drawData.tileCache, drawData.typeCache, drawData.tileLight, drawData.colorTint);
				ushort num5 = drawData.typeCache;
				if (num5 <= 129)
				{
					if (num5 <= 80)
					{
						if (num5 != 51)
						{
							if (num5 == 80)
							{
								bool flag2;
								bool flag3;
								bool flag4;
								WorldGen.GetCactusType(tileX, tileY, (int)drawData.tileFrameX, (int)drawData.tileFrameY, out flag2, out flag3, out flag4);
								if (flag2)
								{
									rectangle.Y += 54;
								}
								if (flag3)
								{
									rectangle.Y += 108;
								}
								if (flag4)
								{
									rectangle.Y += 162;
								}
							}
						}
						else
						{
							drawData.finalColor = drawData.tileLight * 0.5f;
						}
					}
					else if (num5 != 83)
					{
						if (num5 != 114)
						{
							if (num5 == 129)
							{
								drawData.finalColor = new Color(255, 255, 255, 100);
								int num6 = 2;
								if (drawData.tileFrameX >= 324)
								{
									drawData.finalColor = Color.Transparent;
								}
								if (drawData.tileFrameY < 36)
								{
									vector.Y += (float)(num6 * (drawData.tileFrameY == 0).ToDirectionInt());
								}
								else
								{
									vector.X += (float)(num6 * (drawData.tileFrameY == 36).ToDirectionInt());
								}
							}
						}
						else if (drawData.tileFrameY > 0)
						{
							rectangle.Height += 2;
						}
					}
					else
					{
						drawData.drawTexture = this.GetTileDrawTexture(drawData.tileCache, tileX, tileY);
					}
				}
				else
				{
					if (num5 <= 272)
					{
						if (num5 != 136)
						{
							if (num5 != 160)
							{
								if (num5 != 272)
								{
									goto IL_A48;
								}
								int num7 = Main.tileFrame[(int)drawData.typeCache];
								num7 += tileX % 2;
								num7 += tileY % 2;
								num7 += tileX % 3;
								num7 += tileY % 3;
								num7 %= 2;
								num7 *= 90;
								drawData.addFrY += num7;
								rectangle.Y += num7;
								goto IL_A48;
							}
						}
						else
						{
							int num8 = (int)(drawData.tileFrameX / 18);
							if (num8 == 1)
							{
								vector.X += -2f;
								goto IL_A48;
							}
							if (num8 != 2)
							{
								goto IL_A48;
							}
							vector.X += 2f;
							goto IL_A48;
						}
					}
					else if (num5 != 323)
					{
						if (num5 != 442)
						{
							if (num5 != 692)
							{
								goto IL_A48;
							}
						}
						else
						{
							int num8 = (int)(drawData.tileFrameX / 22);
							if (num8 == 3)
							{
								vector.X += 2f;
								goto IL_A48;
							}
							goto IL_A48;
						}
					}
					else
					{
						if (drawData.tileCache.frameX <= 132 && drawData.tileCache.frameX >= 88)
						{
							return;
						}
						vector.X += (float)drawData.tileCache.frameY;
						goto IL_A48;
					}
					Color color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 255);
					if (drawData.tileCache.inActive())
					{
						color = drawData.tileCache.actColor(color);
					}
					drawData.finalColor = color;
				}
				IL_A48:
				if (drawData.typeCache == 314)
				{
					this.DrawTile_MinecartTrack(screenPosition, screenOffset, tileX, tileY, drawData);
				}
				else if (drawData.typeCache == 171)
				{
					this.DrawXmasTree(screenPosition, screenOffset, tileX, tileY, drawData);
				}
				else
				{
					this.DrawBasicTile(screenPosition, screenOffset, tileX, tileY, drawData, rectangle, vector);
				}
				if (Main.tileGlowMask[(int)drawData.tileCache.type] != -1)
				{
					short num9 = Main.tileGlowMask[(int)drawData.tileCache.type];
					if (TextureAssets.GlowMask.IndexInRange((int)num9))
					{
						drawData.drawTexture = TextureAssets.GlowMask[(int)num9].Value;
					}
					double num10 = Main.timeForVisualEffects * 0.08;
					Color color2 = Color.White;
					bool flag5 = false;
					num5 = drawData.tileCache.type;
					if (num5 > 391)
					{
						if (num5 > 540)
						{
							if (num5 <= 659)
							{
								switch (num5)
								{
								case 625:
								case 626:
									goto IL_D1A;
								case 627:
								case 628:
									goto IL_D27;
								case 629:
								case 630:
								case 631:
								case 632:
									goto IL_EFD;
								case 633:
									color2 = Color.Lerp(Color.White, drawData.finalColor, 0.75f);
									goto IL_EFD;
								default:
									if (num5 != 659)
									{
										goto IL_EFD;
									}
									break;
								}
							}
							else if (num5 != 667)
							{
								switch (num5)
								{
								case 687:
									goto IL_CE6;
								case 688:
									goto IL_D0D;
								case 689:
									goto IL_CF3;
								case 690:
									goto IL_D00;
								case 691:
									goto IL_D1A;
								case 692:
									goto IL_D27;
								default:
									goto IL_EFD;
								}
							}
							color2 = LiquidRenderer.GetShimmerGlitterColor(true, (float)tileX, (float)tileY);
							goto IL_EFD;
							IL_D1A:
							color2 = this._violetMossGlow;
							goto IL_EFD;
							IL_D27:
							color2 = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
							goto IL_EFD;
						}
						if (num5 <= 445)
						{
							if (num5 != 429 && num5 != 445)
							{
								goto IL_EFD;
							}
							drawData.drawTexture = this.GetTileDrawTexture(drawData.tileCache, tileX, tileY);
							drawData.addFrY = 18;
							goto IL_EFD;
						}
						else
						{
							if (num5 == 517)
							{
								goto IL_CE6;
							}
							switch (num5)
							{
							case 534:
							case 535:
								break;
							case 536:
							case 537:
								goto IL_D00;
							case 538:
								goto IL_EFD;
							case 539:
							case 540:
								goto IL_D0D;
							default:
								goto IL_EFD;
							}
						}
						IL_CF3:
						color2 = this._kryptonMossGlow;
						goto IL_EFD;
						IL_D00:
						color2 = this._xenonMossGlow;
						goto IL_EFD;
						IL_D0D:
						color2 = this._argonMossGlow;
						goto IL_EFD;
					}
					if (num5 > 350)
					{
						if (num5 <= 381)
						{
							if (num5 != 370)
							{
								if (num5 != 381)
								{
									goto IL_EFD;
								}
								goto IL_CE6;
							}
						}
						else if (num5 != 390)
						{
							if (num5 != 391)
							{
								goto IL_EFD;
							}
							color2 = new Color(250, 250, 250, 200);
							goto IL_EFD;
						}
						color2 = this._meteorGlow;
						goto IL_EFD;
					}
					if (num5 != 129)
					{
						if (num5 == 209)
						{
							color2 = PortalHelper.GetPortalColor(Main.myPlayer, (drawData.tileCache.frameX >= 288) ? 1 : 0);
							goto IL_EFD;
						}
						if (num5 != 350)
						{
							goto IL_EFD;
						}
						color2 = new Color(new Vector4((float)(-(float)Math.Cos(((int)(num10 / 6.283) % 3 == 1) ? num10 : 0.0) * 0.2 + 0.2)));
						goto IL_EFD;
					}
					else
					{
						if (drawData.tileFrameX < 324)
						{
							flag5 = true;
							goto IL_EFD;
						}
						drawData.drawTexture = this.GetTileDrawTexture(drawData.tileCache, tileX, tileY);
						color2 = Main.hslToRgb(0.7f + (float)Math.Sin((double)(6.2831855f * Main.GlobalTimeWrappedHourly * 0.16f + (float)tileX * 0.3f + (float)tileY * 0.7f)) * 0.16f, 1f, 0.5f, byte.MaxValue);
						color2.A /= 2;
						color2 *= 0.3f;
						int num11 = 72;
						for (float num12 = 0f; num12 < 6.2831855f; num12 += 1.5707964f)
						{
							Main.spriteBatch.Draw(drawData.drawTexture, vector + num12.ToRotationVector2() * 2f, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY + num11, drawData.tileWidth, drawData.tileHeight)), color2, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
						}
						color2 = new Color(255, 255, 255, 100);
						goto IL_EFD;
					}
					IL_CE6:
					color2 = this._lavaMossGlow;
					IL_EFD:
					if (!flag5)
					{
						if (drawData.tileCache.slope() == 0 && !drawData.tileCache.halfBrick())
						{
							Main.spriteBatch.Draw(drawData.drawTexture, vector, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight)), color2, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
						}
						else if (drawData.tileCache.halfBrick())
						{
							Main.spriteBatch.Draw(drawData.drawTexture, vector, new Rectangle?(rectangle), color2, 0f, TileDrawing._zero, 1f, SpriteEffects.None, 0f);
						}
						else if (TileID.Sets.Platforms[(int)drawData.tileCache.type])
						{
							Main.spriteBatch.Draw(drawData.drawTexture, vector, new Rectangle?(rectangle), color2, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
							if (drawData.tileCache.slope() == 1 && Main.tile[tileX + 1, tileY + 1].active() && Main.tileSolid[(int)Main.tile[tileX + 1, tileY + 1].type] && Main.tile[tileX + 1, tileY + 1].slope() != 2 && !Main.tile[tileX + 1, tileY + 1].halfBrick() && (!Main.tile[tileX, tileY + 1].active() || (Main.tile[tileX, tileY + 1].blockType() != 0 && Main.tile[tileX, tileY + 1].blockType() != 5) || (!TileID.Sets.BlocksStairs[(int)Main.tile[tileX, tileY + 1].type] && !TileID.Sets.BlocksStairsAbove[(int)Main.tile[tileX, tileY + 1].type])))
							{
								Rectangle value = new Rectangle(198, (int)drawData.tileFrameY, 16, 16);
								if (TileID.Sets.Platforms[(int)Main.tile[tileX + 1, tileY + 1].type] && Main.tile[tileX + 1, tileY + 1].slope() == 0)
								{
									value.X = 324;
								}
								Main.spriteBatch.Draw(drawData.drawTexture, vector + new Vector2(0f, 16f), new Rectangle?(value), color2, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
							}
							else if (drawData.tileCache.slope() == 2 && Main.tile[tileX - 1, tileY + 1].active() && Main.tileSolid[(int)Main.tile[tileX - 1, tileY + 1].type] && Main.tile[tileX - 1, tileY + 1].slope() != 1 && !Main.tile[tileX - 1, tileY + 1].halfBrick() && (!Main.tile[tileX, tileY + 1].active() || (Main.tile[tileX, tileY + 1].blockType() != 0 && Main.tile[tileX, tileY + 1].blockType() != 4) || (!TileID.Sets.BlocksStairs[(int)Main.tile[tileX, tileY + 1].type] && !TileID.Sets.BlocksStairsAbove[(int)Main.tile[tileX, tileY + 1].type])))
							{
								Rectangle value2 = new Rectangle(162, (int)drawData.tileFrameY, 16, 16);
								if (TileID.Sets.Platforms[(int)Main.tile[tileX - 1, tileY + 1].type] && Main.tile[tileX - 1, tileY + 1].slope() == 0)
								{
									value2.X = 306;
								}
								Main.spriteBatch.Draw(drawData.drawTexture, vector + new Vector2(0f, 16f), new Rectangle?(value2), color2, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
							}
						}
						else if (TileID.Sets.HasSlopeFrames[(int)drawData.tileCache.type])
						{
							Main.spriteBatch.Draw(drawData.drawTexture, vector, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, 16, 16)), color2, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						else
						{
							int num13 = (int)drawData.tileCache.slope();
							int num14 = 2;
							for (int i = 0; i < 8; i++)
							{
								int num15 = i * -2;
								int num16 = 16 - i * 2;
								int num17 = 16 - num16;
								int num18;
								switch (num13)
								{
								case 1:
									num15 = 0;
									num18 = i * 2;
									num16 = 14 - i * 2;
									num17 = 0;
									break;
								case 2:
									num15 = 0;
									num18 = 16 - i * 2 - 2;
									num16 = 14 - i * 2;
									num17 = 0;
									break;
								case 3:
									num18 = i * 2;
									break;
								default:
									num18 = 16 - i * 2 - 2;
									break;
								}
								Main.spriteBatch.Draw(drawData.drawTexture, vector + new Vector2((float)num18, (float)(i * num14 + num15)), new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX + num18, (int)drawData.tileFrameY + drawData.addFrY + num17, num14, num16)), color2, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
							}
							int num19 = (num13 > 2) ? 0 : 14;
							Main.spriteBatch.Draw(drawData.drawTexture, vector + new Vector2(0f, (float)num19), new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY + num19, 16, 2)), color2, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
				}
				if (drawData.glowTexture != null)
				{
					Vector2 position = new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset;
					if (TileID.Sets.Platforms[(int)drawData.typeCache])
					{
						position = vector;
					}
					Main.spriteBatch.Draw(drawData.glowTexture, position, new Rectangle?(drawData.glowSourceRect), drawData.glowColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
				}
				if (texture2D != null)
				{
					empty = new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight);
					int num20 = 0;
					int num21 = 0;
					Main.spriteBatch.Draw(texture2D, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + (float)num20, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop + num21)) + screenOffset, new Rectangle?(empty), transparent, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
				}
			}
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x0052DFA8 File Offset: 0x0052C1A8
		private bool IsVisible(Tile tile)
		{
			bool flag = tile.invisibleBlock();
			ushort type = tile.type;
			if (type != 19)
			{
				if (type == 541 || type == 631)
				{
					flag = true;
				}
			}
			else if (tile.frameY / 18 == 48)
			{
				flag = true;
			}
			return !flag || this._shouldShowInvisibleBlocks;
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x0052DFF8 File Offset: 0x0052C1F8
		private Texture2D GetTileDrawTexture(Tile tile, int tileX, int tileY)
		{
			Texture2D result = TextureAssets.Tile[(int)tile.type].Value;
			int tileStyle = 0;
			int num = (int)tile.type;
			ushort type = tile.type;
			if (type != 5)
			{
				if (type != 83)
				{
					if (type == 323)
					{
						tileStyle = this.GetPalmTreeBiome(tileX, tileY);
					}
				}
				else
				{
					if (this.IsAlchemyPlantHarvestable((int)(tile.frameX / 18)))
					{
						num = 84;
					}
					Main.instance.LoadTiles(num);
				}
			}
			else
			{
				tileStyle = TileDrawing.GetTreeBiome(tileX, tileY, (int)tile.frameX, (int)tile.frameY);
			}
			Texture2D texture2D = this._paintSystem.TryGetTileAndRequestIfNotReady(num, tileStyle, (int)tile.color());
			if (texture2D != null)
			{
				result = texture2D;
			}
			return result;
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x0052E098 File Offset: 0x0052C298
		private Texture2D GetTileDrawTexture(Tile tile, int tileX, int tileY, int paintOverride)
		{
			Texture2D result = TextureAssets.Tile[(int)tile.type].Value;
			int tileStyle = 0;
			int num = (int)tile.type;
			ushort type = tile.type;
			if (type != 5)
			{
				if (type != 83)
				{
					if (type == 323)
					{
						tileStyle = this.GetPalmTreeBiome(tileX, tileY);
					}
				}
				else
				{
					if (this.IsAlchemyPlantHarvestable((int)(tile.frameX / 18)))
					{
						num = 84;
					}
					Main.instance.LoadTiles(num);
				}
			}
			else
			{
				tileStyle = TileDrawing.GetTreeBiome(tileX, tileY, (int)tile.frameX, (int)tile.frameY);
			}
			Texture2D texture2D = this._paintSystem.TryGetTileAndRequestIfNotReady(num, tileStyle, paintOverride);
			if (texture2D != null)
			{
				result = texture2D;
			}
			return result;
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x0052E134 File Offset: 0x0052C334
		private void DrawBasicTile(Vector2 screenPosition, Vector2 screenOffset, int tileX, int tileY, TileDrawInfo drawData, Rectangle normalTileRect, Vector2 normalTilePosition)
		{
			if (TileID.Sets.Platforms[(int)drawData.typeCache] && WorldGen.IsRope(tileX, tileY) && Main.tile[tileX, tileY - 1] != null)
			{
				ushort type = Main.tile[tileX, tileY - 1].type;
				int y = (tileY + tileX) % 3 * 18;
				Texture2D tileDrawTexture = this.GetTileDrawTexture(Main.tile[tileX, tileY - 1], tileX, tileY);
				if (tileDrawTexture != null)
				{
					Main.spriteBatch.Draw(tileDrawTexture, new Vector2((float)(tileX * 16 - (int)screenPosition.X), (float)(tileY * 16 - (int)screenPosition.Y)) + screenOffset, new Rectangle?(new Rectangle(90, y, 16, 16)), drawData.tileLight, 0f, default(Vector2), 1f, drawData.tileSpriteEffect, 0f);
				}
			}
			if (drawData.tileCache.slope() > 0)
			{
				if (TileID.Sets.Platforms[(int)drawData.tileCache.type])
				{
					Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition, new Rectangle?(normalTileRect), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					if (drawData.tileCache.slope() == 1 && Main.tile[tileX + 1, tileY + 1].active() && Main.tileSolid[(int)Main.tile[tileX + 1, tileY + 1].type] && Main.tile[tileX + 1, tileY + 1].slope() != 2 && !Main.tile[tileX + 1, tileY + 1].halfBrick() && (!Main.tile[tileX, tileY + 1].active() || (Main.tile[tileX, tileY + 1].blockType() != 0 && Main.tile[tileX, tileY + 1].blockType() != 5) || (!TileID.Sets.BlocksStairs[(int)Main.tile[tileX, tileY + 1].type] && !TileID.Sets.BlocksStairsAbove[(int)Main.tile[tileX, tileY + 1].type])))
					{
						Rectangle value = new Rectangle(198, (int)drawData.tileFrameY, 16, 16);
						if (TileID.Sets.Platforms[(int)Main.tile[tileX + 1, tileY + 1].type] && Main.tile[tileX + 1, tileY + 1].slope() == 0)
						{
							value.X = 324;
						}
						Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition + new Vector2(0f, 16f), new Rectangle?(value), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						return;
					}
					if (drawData.tileCache.slope() == 2 && Main.tile[tileX - 1, tileY + 1].active() && Main.tileSolid[(int)Main.tile[tileX - 1, tileY + 1].type] && Main.tile[tileX - 1, tileY + 1].slope() != 1 && !Main.tile[tileX - 1, tileY + 1].halfBrick() && (!Main.tile[tileX, tileY + 1].active() || (Main.tile[tileX, tileY + 1].blockType() != 0 && Main.tile[tileX, tileY + 1].blockType() != 4) || (!TileID.Sets.BlocksStairs[(int)Main.tile[tileX, tileY + 1].type] && !TileID.Sets.BlocksStairsAbove[(int)Main.tile[tileX, tileY + 1].type])))
					{
						Rectangle value2 = new Rectangle(162, (int)drawData.tileFrameY, 16, 16);
						if (TileID.Sets.Platforms[(int)Main.tile[tileX - 1, tileY + 1].type] && Main.tile[tileX - 1, tileY + 1].slope() == 0)
						{
							value2.X = 306;
						}
						Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition + new Vector2(0f, 16f), new Rectangle?(value2), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						return;
					}
				}
				else
				{
					if (TileID.Sets.HasSlopeFrames[(int)drawData.tileCache.type])
					{
						Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, 16, 16)), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						return;
					}
					int num = (int)drawData.tileCache.slope();
					int num2 = 2;
					for (int i = 0; i < 8; i++)
					{
						int num3 = i * -2;
						int num4 = 16 - i * 2;
						int num5 = 16 - num4;
						int num6;
						switch (num)
						{
						case 1:
							num3 = 0;
							num6 = i * 2;
							num4 = 14 - i * 2;
							num5 = 0;
							break;
						case 2:
							num3 = 0;
							num6 = 16 - i * 2 - 2;
							num4 = 14 - i * 2;
							num5 = 0;
							break;
						case 3:
							num6 = i * 2;
							break;
						default:
							num6 = 16 - i * 2 - 2;
							break;
						}
						Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition + new Vector2((float)num6, (float)(i * num2 + num3)), new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX + num6, (int)drawData.tileFrameY + drawData.addFrY + num5, num2, num4)), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
					int num7 = (num > 2) ? 0 : 14;
					Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition + new Vector2(0f, (float)num7), new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY + num7, 16, 2)), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					return;
				}
			}
			else if (!TileID.Sets.Platforms[(int)drawData.typeCache] && !TileID.Sets.IgnoresNearbyHalfbricksWhenDrawn[(int)drawData.typeCache] && this._tileSolid[(int)drawData.typeCache] && !TileID.Sets.NotReallySolid[(int)drawData.typeCache] && !drawData.tileCache.halfBrick() && (Main.tile[tileX - 1, tileY].halfBrick() || Main.tile[tileX + 1, tileY].halfBrick()))
			{
				if (Main.tile[tileX - 1, tileY].halfBrick() && Main.tile[tileX + 1, tileY].halfBrick())
				{
					Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition + new Vector2(0f, 8f), new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, drawData.addFrY + (int)drawData.tileFrameY + 8, drawData.tileWidth, 8)), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					Rectangle value3 = new Rectangle(126 + drawData.addFrX, drawData.addFrY, 16, 8);
					if (Main.tile[tileX, tileY - 1].active() && !Main.tile[tileX, tileY - 1].bottomSlope() && Main.tile[tileX, tileY - 1].type == drawData.typeCache)
					{
						value3 = new Rectangle(90 + drawData.addFrX, drawData.addFrY, 16, 8);
					}
					Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition, new Rectangle?(value3), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					return;
				}
				if (Main.tile[tileX - 1, tileY].halfBrick())
				{
					int num8 = 4;
					if (TileID.Sets.AllBlocksWithSmoothBordersToResolveHalfBlockIssue[(int)drawData.typeCache])
					{
						num8 = 2;
					}
					Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition + new Vector2(0f, 8f), new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, drawData.addFrY + (int)drawData.tileFrameY + 8, drawData.tileWidth, 8)), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition + new Vector2((float)num8, 0f), new Rectangle?(new Rectangle((int)drawData.tileFrameX + num8 + drawData.addFrX, drawData.addFrY + (int)drawData.tileFrameY, drawData.tileWidth - num8, drawData.tileHeight)), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition, new Rectangle?(new Rectangle(144 + drawData.addFrX, drawData.addFrY, num8, 8)), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					if (num8 == 2)
					{
						Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition, new Rectangle?(new Rectangle(148 + drawData.addFrX, drawData.addFrY, 2, 2)), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						return;
					}
				}
				else if (Main.tile[tileX + 1, tileY].halfBrick())
				{
					int num9 = 4;
					if (TileID.Sets.AllBlocksWithSmoothBordersToResolveHalfBlockIssue[(int)drawData.typeCache])
					{
						num9 = 2;
					}
					Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition + new Vector2(0f, 8f), new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, drawData.addFrY + (int)drawData.tileFrameY + 8, drawData.tileWidth, 8)), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, drawData.addFrY + (int)drawData.tileFrameY, drawData.tileWidth - num9, drawData.tileHeight)), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition + new Vector2((float)(16 - num9), 0f), new Rectangle?(new Rectangle(144 + (16 - num9), 0, num9, 8)), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					if (num9 == 2)
					{
						Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition + new Vector2(14f, 0f), new Rectangle?(new Rectangle(156, 0, 2, 2)), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						return;
					}
				}
			}
			else
			{
				if (Lighting.NotRetro && this._tileSolid[(int)drawData.typeCache] && !drawData.tileCache.halfBrick() && !TileID.Sets.DontDrawTileSliced[(int)drawData.tileCache.type])
				{
					this.DrawSingleTile_SlicedBlock(normalTilePosition, tileX, tileY, drawData);
					return;
				}
				if (drawData.halfBrickHeight == 8 && (!Main.tile[tileX, tileY + 1].active() || !this._tileSolid[(int)Main.tile[tileX, tileY + 1].type] || Main.tile[tileX, tileY + 1].halfBrick()))
				{
					if (TileID.Sets.Platforms[(int)drawData.typeCache])
					{
						Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition, new Rectangle?(normalTileRect), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
					else
					{
						Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition, new Rectangle?(normalTileRect.Modified(0, 0, 0, -4)), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition + new Vector2(0f, 4f), new Rectangle?(new Rectangle(144 + drawData.addFrX, 66 + drawData.addFrY, drawData.tileWidth, 4)), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
				}
				else if (TileID.Sets.CritterCageLidStyle[(int)drawData.typeCache] >= 0)
				{
					int num10 = TileID.Sets.CritterCageLidStyle[(int)drawData.typeCache];
					if ((num10 < 3 && normalTileRect.Y % 54 == 0) || (num10 >= 3 && normalTileRect.Y % 36 == 0))
					{
						Vector2 position = normalTilePosition;
						position.Y += 8f;
						Rectangle value4 = normalTileRect;
						value4.Y += 8;
						value4.Height -= 8;
						Main.spriteBatch.Draw(drawData.drawTexture, position, new Rectangle?(value4), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						position = normalTilePosition;
						position.Y -= 2f;
						value4 = normalTileRect;
						value4.Y = 0;
						value4.Height = 10;
						Main.spriteBatch.Draw(TextureAssets.CageTop[num10].Value, position, new Rectangle?(value4), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
					else
					{
						Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition, new Rectangle?(normalTileRect), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
				}
				else
				{
					Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition, new Rectangle?(normalTileRect), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
				}
				this.DrawSingleTile_Flames(screenPosition, screenOffset, tileX, tileY, drawData);
			}
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x0052F0F4 File Offset: 0x0052D2F4
		private int GetPalmTreeBiome(int tileX, int tileY)
		{
			int num = tileY;
			while (Main.tile[tileX, num].active() && Main.tile[tileX, num].type == 323)
			{
				num++;
			}
			return this.GetPalmTreeVariant(tileX, num);
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x0052F140 File Offset: 0x0052D340
		private static int GetTreeBiome(int tileX, int tileY, int tileFrameX, int tileFrameY)
		{
			int num = tileX;
			int num2 = tileY;
			int type = (int)Main.tile[num, num2].type;
			if (tileFrameX == 66 && tileFrameY <= 45)
			{
				num++;
			}
			if (tileFrameX == 88 && tileFrameY >= 66 && tileFrameY <= 110)
			{
				num--;
			}
			if (tileFrameY >= 198)
			{
				if (tileFrameX == 66)
				{
					num--;
				}
				else if (tileFrameX == 44)
				{
					num++;
				}
			}
			else if (tileFrameY >= 132)
			{
				if (tileFrameX == 22)
				{
					num--;
				}
				else if (tileFrameX == 44)
				{
					num++;
				}
			}
			while (Main.tile[num, num2].active() && (int)Main.tile[num, num2].type == type)
			{
				num2++;
			}
			return TileDrawing.GetTreeVariant(num, num2);
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x0052F1F4 File Offset: 0x0052D3F4
		public static int GetTreeVariant(int x, int y)
		{
			if (Main.tile[x, y] == null || !Main.tile[x, y].active())
			{
				return -1;
			}
			int type = (int)Main.tile[x, y].type;
			if (type > 109)
			{
				if (type <= 199)
				{
					if (type == 147)
					{
						return 3;
					}
					if (type != 199)
					{
						return -1;
					}
				}
				else
				{
					if (type == 492)
					{
						return 2;
					}
					if (type == 661)
					{
						return 0;
					}
					if (type != 662)
					{
						return -1;
					}
				}
				return 4;
			}
			if (type <= 60)
			{
				if (type != 23)
				{
					if (type != 60)
					{
						return -1;
					}
					if ((double)y <= Main.worldSurface)
					{
						return 1;
					}
					return 5;
				}
			}
			else
			{
				if (type == 70)
				{
					return 6;
				}
				if (type != 109)
				{
					return -1;
				}
				return 2;
			}
			return 0;
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x0052F2A4 File Offset: 0x0052D4A4
		private TileDrawing.TileFlameData GetTileFlameData(int tileX, int tileY, int type, int tileFrameY)
		{
			if (type == 270)
			{
				return new TileDrawing.TileFlameData
				{
					flameTexture = TextureAssets.FireflyJar.Value,
					flameColor = new Color(200, 200, 200, 0),
					flameCount = 1
				};
			}
			if (type == 271)
			{
				return new TileDrawing.TileFlameData
				{
					flameTexture = TextureAssets.LightningbugJar.Value,
					flameColor = new Color(200, 200, 200, 0),
					flameCount = 1
				};
			}
			if (type == 581)
			{
				return new TileDrawing.TileFlameData
				{
					flameTexture = TextureAssets.GlowMask[291].Value,
					flameColor = new Color(200, 100, 100, 0),
					flameCount = 1
				};
			}
			if (!Main.tileFlame[type])
			{
				return default(TileDrawing.TileFlameData);
			}
			ulong flameSeed = Main.TileFrameSeed ^ (ulong)((long)tileX << 32 | (long)((ulong)tileY));
			int num = 0;
			if (type == 4)
			{
				num = 0;
			}
			else if (type == 33 || type == 174)
			{
				num = 1;
			}
			else if (type == 100 || type == 173)
			{
				num = 2;
			}
			else if (type == 34)
			{
				num = 3;
			}
			else if (type == 93)
			{
				num = 4;
			}
			else if (type == 49)
			{
				num = 5;
			}
			else if (type == 372)
			{
				num = 16;
			}
			else if (type == 646)
			{
				num = 17;
			}
			else if (type == 98)
			{
				num = 6;
			}
			else if (type == 35)
			{
				num = 7;
			}
			else if (type == 42)
			{
				num = 13;
			}
			TileDrawing.TileFlameData result = new TileDrawing.TileFlameData
			{
				flameTexture = TextureAssets.Flames[num].Value,
				flameSeed = flameSeed
			};
			switch (num)
			{
			case 1:
			{
				int num2 = (int)(Main.tile[tileX, tileY].frameY / 22);
				switch (num2)
				{
				case 5:
				case 6:
				case 7:
				case 10:
					result.flameCount = 7;
					result.flameColor = new Color(50, 50, 50, 0);
					result.flameRangeXMin = -10;
					result.flameRangeXMax = 11;
					result.flameRangeYMin = -10;
					result.flameRangeYMax = 11;
					result.flameRangeMultX = 0.075f;
					result.flameRangeMultY = 0.075f;
					return result;
				case 8:
					result.flameCount = 7;
					result.flameColor = new Color(50, 50, 50, 0);
					result.flameRangeXMin = -10;
					result.flameRangeXMax = 11;
					result.flameRangeYMin = -10;
					result.flameRangeYMax = 11;
					result.flameRangeMultX = 0.3f;
					result.flameRangeMultY = 0.3f;
					return result;
				case 9:
				case 11:
				case 13:
				case 15:
					break;
				case 12:
					result.flameCount = 7;
					result.flameColor = new Color(50, 50, 50, 0);
					result.flameRangeXMin = -10;
					result.flameRangeXMax = 11;
					result.flameRangeYMin = -10;
					result.flameRangeYMax = 1;
					result.flameRangeMultX = 0.1f;
					result.flameRangeMultY = 0.15f;
					return result;
				case 14:
					result.flameCount = 8;
					result.flameColor = new Color(75, 75, 75, 0);
					result.flameRangeXMin = -10;
					result.flameRangeXMax = 11;
					result.flameRangeYMin = -10;
					result.flameRangeYMax = 11;
					result.flameRangeMultX = 0.1f;
					result.flameRangeMultY = 0.1f;
					return result;
				case 16:
					result.flameCount = 4;
					result.flameColor = new Color(75, 75, 75, 0);
					result.flameRangeXMin = -10;
					result.flameRangeXMax = 11;
					result.flameRangeYMin = -10;
					result.flameRangeYMax = 11;
					result.flameRangeMultX = 0.15f;
					result.flameRangeMultY = 0.15f;
					return result;
				default:
					if (num2 - 27 <= 1)
					{
						result.flameCount = 1;
						result.flameColor = new Color(75, 75, 75, 0);
						result.flameRangeXMin = -10;
						result.flameRangeXMax = 11;
						result.flameRangeYMin = -10;
						result.flameRangeYMax = 11;
						result.flameRangeMultX = 0f;
						result.flameRangeMultY = 0f;
						return result;
					}
					break;
				}
				result.flameCount = 7;
				result.flameColor = new Color(100, 100, 100, 0);
				result.flameRangeXMin = -10;
				result.flameRangeXMax = 11;
				result.flameRangeYMin = -10;
				result.flameRangeYMax = 1;
				result.flameRangeMultX = 0.15f;
				result.flameRangeMultY = 0.35f;
				return result;
			}
			case 2:
			{
				int num3 = (int)(Main.tile[tileX, tileY].frameY / 36);
				if (num3 <= 6)
				{
					if (num3 == 3)
					{
						result.flameCount = 3;
						result.flameColor = new Color(50, 50, 50, 0);
						result.flameRangeXMin = -10;
						result.flameRangeXMax = 11;
						result.flameRangeYMin = -10;
						result.flameRangeYMax = 11;
						result.flameRangeMultX = 0.05f;
						result.flameRangeMultY = 0.15f;
						return result;
					}
					if (num3 == 6)
					{
						result.flameCount = 5;
						result.flameColor = new Color(75, 75, 75, 0);
						result.flameRangeXMin = -10;
						result.flameRangeXMax = 11;
						result.flameRangeYMin = -10;
						result.flameRangeYMax = 11;
						result.flameRangeMultX = 0.15f;
						result.flameRangeMultY = 0.15f;
						return result;
					}
				}
				else
				{
					switch (num3)
					{
					case 9:
						result.flameCount = 7;
						result.flameColor = new Color(100, 100, 100, 0);
						result.flameRangeXMin = -10;
						result.flameRangeXMax = 11;
						result.flameRangeYMin = -10;
						result.flameRangeYMax = 11;
						result.flameRangeMultX = 0.3f;
						result.flameRangeMultY = 0.3f;
						return result;
					case 10:
					case 12:
						break;
					case 11:
						result.flameCount = 7;
						result.flameColor = new Color(50, 50, 50, 0);
						result.flameRangeXMin = -10;
						result.flameRangeXMax = 11;
						result.flameRangeYMin = -10;
						result.flameRangeYMax = 1;
						result.flameRangeMultX = 0.1f;
						result.flameRangeMultY = 0.15f;
						return result;
					case 13:
						result.flameCount = 8;
						result.flameColor = new Color(75, 75, 75, 0);
						result.flameRangeXMin = -10;
						result.flameRangeXMax = 11;
						result.flameRangeYMin = -10;
						result.flameRangeYMax = 11;
						result.flameRangeMultX = 0.1f;
						result.flameRangeMultY = 0.1f;
						return result;
					default:
						if (num3 - 28 <= 1)
						{
							result.flameCount = 1;
							result.flameColor = new Color(75, 75, 75, 0);
							result.flameRangeXMin = -10;
							result.flameRangeXMax = 11;
							result.flameRangeYMin = -10;
							result.flameRangeYMax = 1;
							result.flameRangeMultX = 0f;
							result.flameRangeMultY = 0f;
							return result;
						}
						break;
					}
				}
				result.flameCount = 7;
				result.flameColor = new Color(100, 100, 100, 0);
				result.flameRangeXMin = -10;
				result.flameRangeXMax = 11;
				result.flameRangeYMin = -10;
				result.flameRangeYMax = 1;
				result.flameRangeMultX = 0.15f;
				result.flameRangeMultY = 0.35f;
				return result;
			}
			case 3:
			{
				int num4 = (int)(Main.tile[tileX, tileY].frameY / 54);
				switch (num4)
				{
				case 8:
					result.flameCount = 7;
					result.flameColor = new Color(50, 50, 50, 0);
					result.flameRangeXMin = -10;
					result.flameRangeXMax = 11;
					result.flameRangeYMin = -10;
					result.flameRangeYMax = 11;
					result.flameRangeMultX = 0.075f;
					result.flameRangeMultY = 0.075f;
					return result;
				case 9:
					result.flameCount = 3;
					result.flameColor = new Color(50, 50, 50, 0);
					result.flameRangeXMin = -10;
					result.flameRangeXMax = 11;
					result.flameRangeYMin = -10;
					result.flameRangeYMax = 11;
					result.flameRangeMultX = 0.05f;
					result.flameRangeMultY = 0.15f;
					return result;
				case 10:
				case 12:
				case 13:
				case 14:
				case 16:
				case 19:
					break;
				case 11:
					result.flameCount = 7;
					result.flameColor = new Color(50, 50, 50, 0);
					result.flameRangeXMin = -10;
					result.flameRangeXMax = 11;
					result.flameRangeYMin = -10;
					result.flameRangeYMax = 11;
					result.flameRangeMultX = 0.3f;
					result.flameRangeMultY = 0.3f;
					return result;
				case 15:
					result.flameCount = 7;
					result.flameColor = new Color(50, 50, 50, 0);
					result.flameRangeXMin = -10;
					result.flameRangeXMax = 11;
					result.flameRangeYMin = -10;
					result.flameRangeYMax = 1;
					result.flameRangeMultX = 0.1f;
					result.flameRangeMultY = 0.15f;
					return result;
				case 17:
				case 20:
					result.flameCount = 7;
					result.flameColor = new Color(50, 50, 50, 0);
					result.flameRangeXMin = -10;
					result.flameRangeXMax = 11;
					result.flameRangeYMin = -10;
					result.flameRangeYMax = 11;
					result.flameRangeMultX = 0.075f;
					result.flameRangeMultY = 0.075f;
					return result;
				case 18:
					result.flameCount = 8;
					result.flameColor = new Color(75, 75, 75, 0);
					result.flameRangeXMin = -10;
					result.flameRangeXMax = 11;
					result.flameRangeYMin = -10;
					result.flameRangeYMax = 11;
					result.flameRangeMultX = 0.1f;
					result.flameRangeMultY = 0.1f;
					return result;
				default:
					if (num4 - 34 <= 1)
					{
						result.flameCount = 1;
						result.flameColor = new Color(75, 75, 75, 0);
						result.flameRangeXMin = -10;
						result.flameRangeXMax = 11;
						result.flameRangeYMin = -10;
						result.flameRangeYMax = 11;
						result.flameRangeMultX = 0f;
						result.flameRangeMultY = 0f;
						return result;
					}
					break;
				}
				result.flameCount = 7;
				result.flameColor = new Color(100, 100, 100, 0);
				result.flameRangeXMin = -10;
				result.flameRangeXMax = 11;
				result.flameRangeYMin = -10;
				result.flameRangeYMax = 1;
				result.flameRangeMultX = 0.15f;
				result.flameRangeMultY = 0.35f;
				return result;
			}
			case 4:
			{
				int num5 = (int)(Main.tile[tileX, tileY].frameY / 54);
				switch (num5)
				{
				case 1:
					result.flameCount = 3;
					result.flameColor = new Color(50, 50, 50, 0);
					result.flameRangeXMin = -10;
					result.flameRangeXMax = 11;
					result.flameRangeYMin = -10;
					result.flameRangeYMax = 11;
					result.flameRangeMultX = 0.15f;
					result.flameRangeMultY = 0.15f;
					return result;
				case 2:
				case 4:
					result.flameCount = 7;
					result.flameColor = new Color(50, 50, 50, 0);
					result.flameRangeXMin = -10;
					result.flameRangeXMax = 11;
					result.flameRangeYMin = -10;
					result.flameRangeYMax = 11;
					result.flameRangeMultX = 0.075f;
					result.flameRangeMultY = 0.075f;
					return result;
				case 3:
					result.flameCount = 7;
					result.flameColor = new Color(100, 100, 100, 0);
					result.flameRangeXMin = -10;
					result.flameRangeXMax = 11;
					result.flameRangeYMin = -20;
					result.flameRangeYMax = 1;
					result.flameRangeMultX = 0.2f;
					result.flameRangeMultY = 0.35f;
					return result;
				case 5:
					result.flameCount = 7;
					result.flameColor = new Color(50, 50, 50, 0);
					result.flameRangeXMin = -10;
					result.flameRangeXMax = 11;
					result.flameRangeYMin = -10;
					result.flameRangeYMax = 11;
					result.flameRangeMultX = 0.3f;
					result.flameRangeMultY = 0.3f;
					return result;
				case 6:
				case 7:
				case 8:
				case 10:
				case 11:
					break;
				case 9:
					result.flameCount = 7;
					result.flameColor = new Color(50, 50, 50, 0);
					result.flameRangeXMin = -10;
					result.flameRangeXMax = 11;
					result.flameRangeYMin = -10;
					result.flameRangeYMax = 1;
					result.flameRangeMultX = 0.1f;
					result.flameRangeMultY = 0.15f;
					return result;
				case 12:
					result.flameCount = 1;
					result.flameColor = new Color(100, 100, 100, 0);
					result.flameRangeXMin = -10;
					result.flameRangeXMax = 11;
					result.flameRangeYMin = -10;
					result.flameRangeYMax = 11;
					result.flameRangeMultX = 0.01f;
					result.flameRangeMultY = 0.01f;
					return result;
				case 13:
					result.flameCount = 8;
					result.flameColor = new Color(50, 50, 50, 0);
					result.flameRangeXMin = -10;
					result.flameRangeXMax = 11;
					result.flameRangeYMin = -10;
					result.flameRangeYMax = 11;
					result.flameRangeMultX = 0.1f;
					result.flameRangeMultY = 0.1f;
					return result;
				default:
					if (num5 - 28 <= 1)
					{
						result.flameCount = 1;
						result.flameColor = new Color(75, 75, 75, 0);
						result.flameRangeXMin = -10;
						result.flameRangeXMax = 11;
						result.flameRangeYMin = -10;
						result.flameRangeYMax = 11;
						result.flameRangeMultX = 0f;
						result.flameRangeMultY = 0f;
						return result;
					}
					break;
				}
				result.flameCount = 7;
				result.flameColor = new Color(100, 100, 100, 0);
				result.flameRangeXMin = -10;
				result.flameRangeXMax = 11;
				result.flameRangeYMin = -10;
				result.flameRangeYMax = 1;
				result.flameRangeMultX = 0.15f;
				result.flameRangeMultY = 0.35f;
				return result;
			}
			case 5:
			case 6:
				break;
			case 7:
				result.flameCount = 4;
				result.flameColor = new Color(50, 50, 50, 0);
				result.flameRangeXMin = -10;
				result.flameRangeXMax = 11;
				result.flameRangeYMin = -10;
				result.flameRangeYMax = 10;
				result.flameRangeMultX = 0f;
				result.flameRangeMultY = 0f;
				return result;
			default:
				if (num == 13)
				{
					int num6 = tileFrameY / 36;
					if (num6 <= 36)
					{
						switch (num6)
						{
						case 1:
						case 3:
						case 6:
						case 8:
							goto IL_F82;
						case 2:
							break;
						case 4:
						case 5:
						case 7:
						case 9:
						case 10:
							goto IL_114A;
						case 11:
							result.flameCount = 7;
							result.flameColor = new Color(50, 50, 50, 0);
							result.flameRangeXMin = -10;
							result.flameRangeXMax = 11;
							result.flameRangeYMin = -10;
							result.flameRangeYMax = 11;
							result.flameRangeMultX = 0.075f;
							result.flameRangeMultY = 0.075f;
							return result;
						default:
							switch (num6)
							{
							case 16:
							case 25:
								break;
							case 17:
							case 18:
							case 20:
							case 21:
							case 22:
							case 23:
							case 24:
							case 26:
							case 28:
							case 33:
								goto IL_114A;
							case 19:
							case 27:
							case 29:
							case 30:
							case 31:
							case 32:
							case 36:
								goto IL_F82;
							case 34:
							case 35:
								result.flameCount = 1;
								result.flameColor = new Color(75, 75, 75, 0);
								result.flameRangeXMin = -10;
								result.flameRangeXMax = 11;
								result.flameRangeYMin = -10;
								result.flameRangeYMax = 1;
								result.flameRangeMultX = 0f;
								result.flameRangeMultY = 0f;
								return result;
							default:
								goto IL_114A;
							}
							break;
						}
						result.flameCount = 7;
						result.flameColor = new Color(50, 50, 50, 0);
						result.flameRangeXMin = -10;
						result.flameRangeXMax = 11;
						result.flameRangeYMin = -10;
						result.flameRangeYMax = 1;
						result.flameRangeMultX = 0.15f;
						result.flameRangeMultY = 0.1f;
						return result;
					}
					if (num6 != 39)
					{
						if (num6 != 44)
						{
							goto IL_114A;
						}
						result.flameCount = 7;
						result.flameColor = new Color(100, 100, 100, 0);
						result.flameRangeXMin = -10;
						result.flameRangeXMax = 11;
						result.flameRangeYMin = -10;
						result.flameRangeYMax = 1;
						result.flameRangeMultX = 0.15f;
						result.flameRangeMultY = 0.35f;
						return result;
					}
					IL_F82:
					result.flameCount = 7;
					result.flameColor = new Color(100, 100, 100, 0);
					result.flameRangeXMin = -10;
					result.flameRangeXMax = 11;
					result.flameRangeYMin = -10;
					result.flameRangeYMax = 1;
					result.flameRangeMultX = 0.15f;
					result.flameRangeMultY = 0.35f;
					return result;
					IL_114A:
					result.flameCount = 0;
					return result;
				}
				break;
			}
			result.flameCount = 7;
			result.flameColor = new Color(100, 100, 100, 0);
			if (tileFrameY / 22 == 14)
			{
				result.flameColor = new Color((float)Main.DiscoR / 255f, (float)Main.DiscoG / 255f, (float)Main.DiscoB / 255f, 0f);
			}
			result.flameRangeXMin = -10;
			result.flameRangeXMax = 11;
			result.flameRangeYMin = -10;
			result.flameRangeYMax = 1;
			result.flameRangeMultX = 0.15f;
			result.flameRangeMultY = 0.35f;
			return result;
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x005304A0 File Offset: 0x0052E6A0
		private void DrawSingleTile_Flames(Vector2 screenPosition, Vector2 screenOffset, int tileX, int tileY, TileDrawInfo drawData)
		{
			if (drawData.typeCache == 548 && drawData.tileFrameX / 54 > 6)
			{
				Main.spriteBatch.Draw(TextureAssets.GlowMask[297].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight)), Color.White, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 613)
			{
				Main.spriteBatch.Draw(TextureAssets.GlowMask[298].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight)), Color.White, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 614)
			{
				Main.spriteBatch.Draw(TextureAssets.GlowMask[299].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight)), Color.White, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 593)
			{
				Main.spriteBatch.Draw(TextureAssets.GlowMask[295].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight)), Color.White, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 594)
			{
				Main.spriteBatch.Draw(TextureAssets.GlowMask[296].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight)), Color.White, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 215 && drawData.tileFrameY < 36)
			{
				int num = 15;
				Color color = new Color(255, 255, 255, 0);
				int num2 = (int)(drawData.tileFrameX / 54);
				if (num2 != 5)
				{
					if (num2 != 14)
					{
						if (num2 == 15)
						{
							color = new Color(255, 255, 255, 200);
						}
					}
					else
					{
						color = new Color(50, 50, 100, 20);
					}
				}
				else
				{
					color = new Color((float)Main.DiscoR / 255f, (float)Main.DiscoG / 255f, (float)Main.DiscoB / 255f, 0f);
				}
				Main.spriteBatch.Draw(TextureAssets.Flames[num].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight)), color, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 85)
			{
				float graveyardVisualIntensity = Main.GraveyardVisualIntensity;
				if (graveyardVisualIntensity > 0f)
				{
					ulong num3 = Main.TileFrameSeed ^ (ulong)((long)tileX << 32 | (long)((ulong)tileY));
					TileDrawing.TileFlameData tileFlameData = this.GetTileFlameData(tileX, tileY, (int)drawData.typeCache, (int)drawData.tileFrameY);
					if (num3 == 0UL)
					{
						num3 = tileFlameData.flameSeed;
					}
					tileFlameData.flameSeed = num3;
					Vector2 vector = new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset;
					Rectangle value = new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight);
					for (int i = 0; i < tileFlameData.flameCount; i++)
					{
						Color color2 = tileFlameData.flameColor * graveyardVisualIntensity;
						float x = (float)Utils.RandomInt(ref tileFlameData.flameSeed, tileFlameData.flameRangeXMin, tileFlameData.flameRangeXMax) * tileFlameData.flameRangeMultX;
						float y = (float)Utils.RandomInt(ref tileFlameData.flameSeed, tileFlameData.flameRangeYMin, tileFlameData.flameRangeYMax) * tileFlameData.flameRangeMultY;
						for (float num4 = 0f; num4 < 1f; num4 += 0.25f)
						{
							Main.spriteBatch.Draw(tileFlameData.flameTexture, vector + new Vector2(x, y) + Vector2.UnitX.RotatedBy((double)(num4 * 6.2831855f), default(Vector2)) * 2f, new Rectangle?(value), color2, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						Main.spriteBatch.Draw(tileFlameData.flameTexture, vector, new Rectangle?(value), Color.White * graveyardVisualIntensity, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
				}
			}
			if (drawData.typeCache == 356 && Main.sundialCooldown == 0)
			{
				Texture2D value2 = TextureAssets.GlowMask[325].Value;
				Rectangle value3 = new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight);
				Color color3 = new Color(100, 100, 100, 0);
				int num5 = tileX - (int)(drawData.tileFrameX / 18);
				int num6 = tileY - (int)(drawData.tileFrameY / 18);
				ulong num7 = Main.TileFrameSeed ^ (ulong)((long)num5 << 32 | (long)((ulong)num6));
				for (int j = 0; j < 7; j++)
				{
					float num8 = (float)Utils.RandomInt(ref num7, -10, 11) * 0.15f;
					float num9 = (float)Utils.RandomInt(ref num7, -10, 1) * 0.35f;
					Main.spriteBatch.Draw(value2, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num8, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num9) + screenOffset, new Rectangle?(value3), color3, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
				}
			}
			if (drawData.typeCache == 663 && Main.moondialCooldown == 0)
			{
				Texture2D value4 = TextureAssets.GlowMask[335].Value;
				Rectangle value5 = new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight);
				value5.Y += 54 * Main.moonPhase;
				Main.spriteBatch.Draw(value4, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(value5), Color.White * ((float)Main.mouseTextColor / 255f), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 286)
			{
				Main.spriteBatch.Draw(TextureAssets.GlowSnail.Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 100, 255, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 582)
			{
				Main.spriteBatch.Draw(TextureAssets.GlowMask[293].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight)), new Color(200, 100, 100, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 391)
			{
				Main.spriteBatch.Draw(TextureAssets.GlowMask[131].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight)), new Color(250, 250, 250, 200), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 619)
			{
				Main.spriteBatch.Draw(TextureAssets.GlowMask[300].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 100, 255, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 270)
			{
				Main.spriteBatch.Draw(TextureAssets.FireflyJar.Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(200, 200, 200, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 271)
			{
				Main.spriteBatch.Draw(TextureAssets.LightningbugJar.Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(200, 200, 200, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 581)
			{
				Main.spriteBatch.Draw(TextureAssets.GlowMask[291].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(200, 200, 200, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 316 || drawData.typeCache == 317 || drawData.typeCache == 318)
			{
				int num10 = tileX - (int)(drawData.tileFrameX / 18);
				int num11 = tileY - (int)(drawData.tileFrameY / 18);
				int num12 = num10 / 2 * (num11 / 3);
				num12 %= Main.cageFrames;
				Main.spriteBatch.Draw(TextureAssets.JellyfishBowl[(int)(drawData.typeCache - 316)].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + Main.jellyfishCageFrame[(int)(drawData.typeCache - 316), num12] * 36, drawData.tileWidth, drawData.tileHeight)), new Color(200, 200, 200, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 149 && drawData.tileFrameX < 54)
			{
				Main.spriteBatch.Draw(TextureAssets.XmasLight.Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(200, 200, 200, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 300 || drawData.typeCache == 302 || drawData.typeCache == 303 || drawData.typeCache == 306)
			{
				int num13 = 9;
				if (drawData.typeCache == 302)
				{
					num13 = 10;
				}
				if (drawData.typeCache == 303)
				{
					num13 = 11;
				}
				if (drawData.typeCache == 306)
				{
					num13 = 12;
				}
				Main.spriteBatch.Draw(TextureAssets.Flames[num13].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight)), new Color(200, 200, 200, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			else if (Main.tileFlame[(int)drawData.typeCache])
			{
				ulong num14 = Main.TileFrameSeed ^ (ulong)((long)tileX << 32 | (long)((ulong)tileY));
				int typeCache = (int)drawData.typeCache;
				int num15 = 0;
				if (typeCache == 4)
				{
					num15 = 0;
				}
				else if (typeCache == 33 || typeCache == 174)
				{
					num15 = 1;
				}
				else if (typeCache == 100 || typeCache == 173)
				{
					num15 = 2;
				}
				else if (typeCache == 34)
				{
					num15 = 3;
				}
				else if (typeCache == 93)
				{
					num15 = 4;
				}
				else if (typeCache == 49)
				{
					num15 = 5;
				}
				else if (typeCache == 372)
				{
					num15 = 16;
				}
				else if (typeCache == 646)
				{
					num15 = 17;
				}
				else if (typeCache == 98)
				{
					num15 = 6;
				}
				else if (typeCache == 35)
				{
					num15 = 7;
				}
				else if (typeCache == 42)
				{
					num15 = 13;
				}
				if (num15 == 7)
				{
					for (int k = 0; k < 4; k++)
					{
						float num16 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.15f;
						float num17 = (float)Utils.RandomInt(ref num14, -10, 10) * 0.15f;
						num16 = 0f;
						num17 = 0f;
						Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num16, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num17) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
				}
				else if (num15 == 1)
				{
					int num18 = (int)(Main.tile[tileX, tileY].frameY / 22);
					if (num18 == 5 || num18 == 6 || num18 == 7 || num18 == 10)
					{
						for (int l = 0; l < 7; l++)
						{
							float num19 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.075f;
							float num20 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.075f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num19, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num20) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num18 == 8)
					{
						for (int m = 0; m < 7; m++)
						{
							float num21 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.3f;
							float num22 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.3f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num21, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num22) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num18 == 12)
					{
						for (int n = 0; n < 7; n++)
						{
							float num23 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.1f;
							float num24 = (float)Utils.RandomInt(ref num14, -10, 1) * 0.15f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num23, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num24) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num18 == 14)
					{
						for (int num25 = 0; num25 < 8; num25++)
						{
							float num26 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.1f;
							float num27 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.1f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num26, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num27) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num18 == 16)
					{
						for (int num28 = 0; num28 < 4; num28++)
						{
							float num29 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.15f;
							float num30 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.15f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num29, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num30) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num18 == 27 || num18 == 28)
					{
						Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
					else
					{
						for (int num31 = 0; num31 < 7; num31++)
						{
							float num32 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.15f;
							float num33 = (float)Utils.RandomInt(ref num14, -10, 1) * 0.35f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num32, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num33) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(100, 100, 100, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
				}
				else if (num15 == 2)
				{
					int num34 = (int)(Main.tile[tileX, tileY].frameY / 36);
					if (num34 == 3)
					{
						for (int num35 = 0; num35 < 3; num35++)
						{
							float num36 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.05f;
							float num37 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.15f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num36, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num37) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num34 == 6)
					{
						for (int num38 = 0; num38 < 5; num38++)
						{
							float num39 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.15f;
							float num40 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.15f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num39, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num40) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num34 == 9)
					{
						for (int num41 = 0; num41 < 7; num41++)
						{
							float num42 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.3f;
							float num43 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.3f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num42, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num43) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(100, 100, 100, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num34 == 11)
					{
						for (int num44 = 0; num44 < 7; num44++)
						{
							float num45 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.1f;
							float num46 = (float)Utils.RandomInt(ref num14, -10, 1) * 0.15f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num45, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num46) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num34 == 13)
					{
						for (int num47 = 0; num47 < 8; num47++)
						{
							float num48 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.1f;
							float num49 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.1f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num48, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num49) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num34 == 28 || num34 == 29)
					{
						Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
					else
					{
						for (int num50 = 0; num50 < 7; num50++)
						{
							float num51 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.15f;
							float num52 = (float)Utils.RandomInt(ref num14, -10, 1) * 0.35f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num51, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num52) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(100, 100, 100, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
				}
				else if (num15 == 3)
				{
					int num53 = (int)(Main.tile[tileX, tileY].frameY / 54);
					if (num53 == 8)
					{
						for (int num54 = 0; num54 < 7; num54++)
						{
							float num55 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.075f;
							float num56 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.075f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num55, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num56) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num53 == 9)
					{
						for (int num57 = 0; num57 < 3; num57++)
						{
							float num58 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.05f;
							float num59 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.15f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num58, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num59) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num53 == 11)
					{
						for (int num60 = 0; num60 < 7; num60++)
						{
							float num61 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.3f;
							float num62 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.3f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num61, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num62) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num53 == 15)
					{
						for (int num63 = 0; num63 < 7; num63++)
						{
							float num64 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.1f;
							float num65 = (float)Utils.RandomInt(ref num14, -10, 1) * 0.15f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num64, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num65) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num53 == 17 || num53 == 20)
					{
						for (int num66 = 0; num66 < 7; num66++)
						{
							float num67 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.075f;
							float num68 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.075f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num67, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num68) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num53 == 18)
					{
						for (int num69 = 0; num69 < 8; num69++)
						{
							float num70 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.1f;
							float num71 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.1f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num70, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num71) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num53 == 34 || num53 == 35)
					{
						Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
					else
					{
						for (int num72 = 0; num72 < 7; num72++)
						{
							float num73 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.15f;
							float num74 = (float)Utils.RandomInt(ref num14, -10, 1) * 0.35f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num73, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num74) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(100, 100, 100, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
				}
				else if (num15 == 4)
				{
					int num75 = (int)(Main.tile[tileX, tileY].frameY / 54);
					if (num75 == 1)
					{
						for (int num76 = 0; num76 < 3; num76++)
						{
							float num77 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.15f;
							float num78 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.15f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num77, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num78) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num75 == 2 || num75 == 4)
					{
						for (int num79 = 0; num79 < 7; num79++)
						{
							float num80 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.075f;
							float num81 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.075f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num80, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num81) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num75 == 3)
					{
						for (int num82 = 0; num82 < 7; num82++)
						{
							float num83 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.2f;
							float num84 = (float)Utils.RandomInt(ref num14, -20, 1) * 0.35f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num83, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num84) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(100, 100, 100, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num75 == 5)
					{
						for (int num85 = 0; num85 < 7; num85++)
						{
							float num86 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.3f;
							float num87 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.3f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num86, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num87) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num75 == 9)
					{
						for (int num88 = 0; num88 < 7; num88++)
						{
							float num89 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.1f;
							float num90 = (float)Utils.RandomInt(ref num14, -10, 1) * 0.15f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num89, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num90) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num75 == 13)
					{
						for (int num91 = 0; num91 < 8; num91++)
						{
							float num92 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.1f;
							float num93 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.1f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num92, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num93) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num75 == 12)
					{
						float num94 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.01f;
						float num95 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.01f;
						Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num94, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num95) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(Utils.RandomInt(ref num14, 90, 111), Utils.RandomInt(ref num14, 90, 111), Utils.RandomInt(ref num14, 90, 111), 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
					else if (num75 == 28 || num75 == 29)
					{
						Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
					else
					{
						for (int num96 = 0; num96 < 7; num96++)
						{
							float num97 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.15f;
							float num98 = (float)Utils.RandomInt(ref num14, -10, 1) * 0.35f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num97, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num98) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight)), new Color(100, 100, 100, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
				}
				else if (num15 == 13)
				{
					int num99 = (int)(drawData.tileFrameY / 36);
					if (num99 == 1 || num99 == 3 || num99 == 6 || num99 == 8 || num99 == 19 || num99 == 27 || num99 == 29 || num99 == 30 || num99 == 31 || num99 == 32 || num99 == 36 || num99 == 39)
					{
						for (int num100 = 0; num100 < 7; num100++)
						{
							float num101 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.15f;
							float num102 = (float)Utils.RandomInt(ref num14, -10, 1) * 0.35f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num101, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num102) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(100, 100, 100, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num99 == 25 || num99 == 16 || num99 == 2)
					{
						for (int num103 = 0; num103 < 7; num103++)
						{
							float num104 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.15f;
							float num105 = (float)Utils.RandomInt(ref num14, -10, 1) * 0.1f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num104, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num105) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num99 == 29)
					{
						for (int num106 = 0; num106 < 7; num106++)
						{
							float num107 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.15f;
							float num108 = (float)Utils.RandomInt(ref num14, -10, 1) * 0.15f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num107, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num108) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(25, 25, 25, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (num99 == 34 || num99 == 35)
					{
						Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
				}
				else
				{
					Color color4 = new Color(100, 100, 100, 0);
					if (drawData.tileCache.type == 4)
					{
						int num109 = (int)(drawData.tileCache.frameY / 22);
						if (num109 != 14)
						{
							if (num109 != 22)
							{
								if (num109 == 23)
								{
									color4 = new Color(255, 255, 255, 200);
								}
							}
							else
							{
								color4 = new Color(50, 50, 100, 20);
							}
						}
						else
						{
							color4 = new Color((float)Main.DiscoR / 255f, (float)Main.DiscoG / 255f, (float)Main.DiscoB / 255f, 0f);
						}
					}
					if (drawData.tileCache.type == 646)
					{
						color4 = new Color(100, 100, 100, 150);
					}
					for (int num110 = 0; num110 < 7; num110++)
					{
						float num111 = (float)Utils.RandomInt(ref num14, -10, 11) * 0.15f;
						float num112 = (float)Utils.RandomInt(ref num14, -10, 1) * 0.35f;
						Main.spriteBatch.Draw(TextureAssets.Flames[num15].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num111, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num112) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), color4, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
				}
			}
			if (drawData.typeCache == 144)
			{
				Main.spriteBatch.Draw(TextureAssets.Timer.Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(200, 200, 200, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 237)
			{
				Main.spriteBatch.Draw(TextureAssets.SunAltar.Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color((int)(Main.mouseTextColor / 2), (int)(Main.mouseTextColor / 2), (int)(Main.mouseTextColor / 2), 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 658 && drawData.tileFrameX % 36 == 0 && drawData.tileFrameY % 54 == 0)
			{
				int num113 = (int)(drawData.tileFrameY / 54);
				if (num113 != 2)
				{
					Texture2D value6 = TextureAssets.GlowMask[334].Value;
					Vector2 value7 = new Vector2(0f, -10f);
					Vector2 position = new Vector2((float)(tileX * 16 - (int)screenPosition.X) - (float)drawData.tileWidth / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset + value7;
					Rectangle value8 = value6.Frame(1, 1, 0, 0, 0, 0);
					Color color5 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, 0);
					if (num113 == 0)
					{
						color5 *= 0.75f;
					}
					Main.spriteBatch.Draw(value6, position, new Rectangle?(value8), color5, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
				}
			}
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x00533EB4 File Offset: 0x005320B4
		private int GetPalmTreeVariant(int x, int y)
		{
			int num = -1;
			if (Main.tile[x, y].active() && Main.tile[x, y].type == 53)
			{
				num = 0;
			}
			if (Main.tile[x, y].active() && Main.tile[x, y].type == 234)
			{
				num = 1;
			}
			if (Main.tile[x, y].active() && Main.tile[x, y].type == 116)
			{
				num = 2;
			}
			if (Main.tile[x, y].active() && Main.tile[x, y].type == 112)
			{
				num = 3;
			}
			if (WorldGen.IsPalmOasisTree(x))
			{
				num += 4;
			}
			return num;
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x00533F7C File Offset: 0x0053217C
		private void DrawSingleTile_SlicedBlock(Vector2 normalTilePosition, int tileX, int tileY, TileDrawInfo drawData)
		{
			Color color = default(Color);
			Vector2 origin = default(Vector2);
			Rectangle rectangle;
			Vector3 vector3;
			Vector2 position;
			if (drawData.tileLight.R > this._highQualityLightingRequirement.R || drawData.tileLight.G > this._highQualityLightingRequirement.G || drawData.tileLight.B > this._highQualityLightingRequirement.B)
			{
				Vector3[] array = drawData.colorSlices;
				Lighting.GetColor9Slice(tileX, tileY, ref array);
				Vector3 vector = drawData.tileLight.ToVector3();
				Vector3 vector2 = drawData.colorTint.ToVector3();
				if (drawData.tileCache.fullbrightBlock())
				{
					array = this._glowPaintColorSlices;
				}
				for (int i = 0; i < 9; i++)
				{
					rectangle.X = 0;
					rectangle.Y = 0;
					rectangle.Width = 4;
					rectangle.Height = 4;
					switch (i)
					{
					case 1:
						rectangle.Width = 8;
						rectangle.X = 4;
						break;
					case 2:
						rectangle.X = 12;
						break;
					case 3:
						rectangle.Height = 8;
						rectangle.Y = 4;
						break;
					case 4:
						rectangle.Width = 8;
						rectangle.Height = 8;
						rectangle.X = 4;
						rectangle.Y = 4;
						break;
					case 5:
						rectangle.X = 12;
						rectangle.Y = 4;
						rectangle.Height = 8;
						break;
					case 6:
						rectangle.Y = 12;
						break;
					case 7:
						rectangle.Width = 8;
						rectangle.Height = 4;
						rectangle.X = 4;
						rectangle.Y = 12;
						break;
					case 8:
						rectangle.X = 12;
						rectangle.Y = 12;
						break;
					}
					vector3.X = (array[i].X + vector.X) * 0.5f;
					vector3.Y = (array[i].Y + vector.Y) * 0.5f;
					vector3.Z = (array[i].Z + vector.Z) * 0.5f;
					TileDrawing.GetFinalLight(drawData.tileCache, drawData.typeCache, ref vector3, ref vector2);
					position.X = normalTilePosition.X + (float)rectangle.X;
					position.Y = normalTilePosition.Y + (float)rectangle.Y;
					rectangle.X += (int)drawData.tileFrameX + drawData.addFrX;
					rectangle.Y += (int)drawData.tileFrameY + drawData.addFrY;
					int num = (int)(vector3.X * 255f);
					int num2 = (int)(vector3.Y * 255f);
					int num3 = (int)(vector3.Z * 255f);
					if (num > 255)
					{
						num = 255;
					}
					if (num2 > 255)
					{
						num2 = 255;
					}
					if (num3 > 255)
					{
						num3 = 255;
					}
					num3 <<= 16;
					num2 <<= 8;
					color.PackedValue = (uint)(num | num2 | num3 | -16777216);
					Main.spriteBatch.Draw(drawData.drawTexture, position, new Rectangle?(rectangle), color, 0f, origin, 1f, drawData.tileSpriteEffect, 0f);
				}
				return;
			}
			if (drawData.tileLight.R > this._mediumQualityLightingRequirement.R || drawData.tileLight.G > this._mediumQualityLightingRequirement.G || drawData.tileLight.B > this._mediumQualityLightingRequirement.B)
			{
				Vector3[] array2 = drawData.colorSlices;
				Lighting.GetColor4Slice(tileX, tileY, ref array2);
				Vector3 vector4 = drawData.tileLight.ToVector3();
				Vector3 vector5 = drawData.colorTint.ToVector3();
				if (drawData.tileCache.fullbrightBlock())
				{
					array2 = this._glowPaintColorSlices;
				}
				rectangle.Width = 8;
				rectangle.Height = 8;
				for (int j = 0; j < 4; j++)
				{
					rectangle.X = 0;
					rectangle.Y = 0;
					switch (j)
					{
					case 1:
						rectangle.X = 8;
						break;
					case 2:
						rectangle.Y = 8;
						break;
					case 3:
						rectangle.X = 8;
						rectangle.Y = 8;
						break;
					}
					vector3.X = (array2[j].X + vector4.X) * 0.5f;
					vector3.Y = (array2[j].Y + vector4.Y) * 0.5f;
					vector3.Z = (array2[j].Z + vector4.Z) * 0.5f;
					TileDrawing.GetFinalLight(drawData.tileCache, drawData.typeCache, ref vector3, ref vector5);
					position.X = normalTilePosition.X + (float)rectangle.X;
					position.Y = normalTilePosition.Y + (float)rectangle.Y;
					rectangle.X += (int)drawData.tileFrameX + drawData.addFrX;
					rectangle.Y += (int)drawData.tileFrameY + drawData.addFrY;
					int num4 = (int)(vector3.X * 255f);
					int num5 = (int)(vector3.Y * 255f);
					int num6 = (int)(vector3.Z * 255f);
					if (num4 > 255)
					{
						num4 = 255;
					}
					if (num5 > 255)
					{
						num5 = 255;
					}
					if (num6 > 255)
					{
						num6 = 255;
					}
					num6 <<= 16;
					num5 <<= 8;
					color.PackedValue = (uint)(num4 | num5 | num6 | -16777216);
					Main.spriteBatch.Draw(drawData.drawTexture, position, new Rectangle?(rectangle), color, 0f, origin, 1f, drawData.tileSpriteEffect, 0f);
				}
				return;
			}
			Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight)), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x005345D8 File Offset: 0x005327D8
		private void DrawXmasTree(Vector2 screenPosition, Vector2 screenOffset, int tileX, int tileY, TileDrawInfo drawData)
		{
			if (tileY - (int)drawData.tileFrameY > 0 && drawData.tileFrameY == 7 && Main.tile[tileX, tileY - (int)drawData.tileFrameY] != null)
			{
				drawData.tileTop -= (int)(16 * drawData.tileFrameY);
				drawData.tileFrameX = Main.tile[tileX, tileY - (int)drawData.tileFrameY].frameX;
				drawData.tileFrameY = Main.tile[tileX, tileY - (int)drawData.tileFrameY].frameY;
			}
			if (drawData.tileFrameX >= 10)
			{
				int num = 0;
				if ((drawData.tileFrameY & 1) == 1)
				{
					num++;
				}
				if ((drawData.tileFrameY & 2) == 2)
				{
					num += 2;
				}
				if ((drawData.tileFrameY & 4) == 4)
				{
					num += 4;
				}
				int num2 = 0;
				if ((drawData.tileFrameY & 8) == 8)
				{
					num2++;
				}
				if ((drawData.tileFrameY & 16) == 16)
				{
					num2 += 2;
				}
				if ((drawData.tileFrameY & 32) == 32)
				{
					num2 += 4;
				}
				int num3 = 0;
				if ((drawData.tileFrameY & 64) == 64)
				{
					num3++;
				}
				if ((drawData.tileFrameY & 128) == 128)
				{
					num3 += 2;
				}
				if ((drawData.tileFrameY & 256) == 256)
				{
					num3 += 4;
				}
				if ((drawData.tileFrameY & 512) == 512)
				{
					num3 += 8;
				}
				int num4 = 0;
				if ((drawData.tileFrameY & 1024) == 1024)
				{
					num4++;
				}
				if ((drawData.tileFrameY & 2048) == 2048)
				{
					num4 += 2;
				}
				if ((drawData.tileFrameY & 4096) == 4096)
				{
					num4 += 4;
				}
				if ((drawData.tileFrameY & 8192) == 8192)
				{
					num4 += 8;
				}
				Color color = Lighting.GetColor(tileX + 1, tileY - 3);
				Main.spriteBatch.Draw(TextureAssets.XmasTree[0].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle(0, 0, 64, 128)), color, 0f, TileDrawing._zero, 1f, SpriteEffects.None, 0f);
				if (num > 0)
				{
					num--;
					Color color2 = color;
					if (num != 3)
					{
						color2 = new Color(255, 255, 255, 255);
					}
					Main.spriteBatch.Draw(TextureAssets.XmasTree[3].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle(66 * num, 0, 64, 128)), color2, 0f, TileDrawing._zero, 1f, SpriteEffects.None, 0f);
				}
				if (num2 > 0)
				{
					num2--;
					Main.spriteBatch.Draw(TextureAssets.XmasTree[1].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle(66 * num2, 0, 64, 128)), color, 0f, TileDrawing._zero, 1f, SpriteEffects.None, 0f);
				}
				if (num3 > 0)
				{
					num3--;
					Main.spriteBatch.Draw(TextureAssets.XmasTree[2].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle(66 * num3, 0, 64, 128)), color, 0f, TileDrawing._zero, 1f, SpriteEffects.None, 0f);
				}
				if (num4 > 0)
				{
					num4--;
					Main.spriteBatch.Draw(TextureAssets.XmasTree[4].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle(66 * num4, 130 * Main.tileFrame[171], 64, 128)), new Color(255, 255, 255, 255), 0f, TileDrawing._zero, 1f, SpriteEffects.None, 0f);
				}
			}
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x00534AC0 File Offset: 0x00532CC0
		private void DrawTile_MinecartTrack(Vector2 screenPosition, Vector2 screenOffset, int tileX, int tileY, TileDrawInfo drawData)
		{
			drawData.tileLight = TileDrawing.GetFinalLight(drawData.tileCache, drawData.typeCache, drawData.tileLight, drawData.colorTint);
			int paintOverride;
			int paintOverride2;
			Minecart.TrackColors(tileX, tileY, drawData.tileCache, out paintOverride, out paintOverride2);
			drawData.drawTexture = this.GetTileDrawTexture(drawData.tileCache, tileX, tileY, paintOverride);
			Texture2D tileDrawTexture = this.GetTileDrawTexture(drawData.tileCache, tileX, tileY, paintOverride2);
			if (WorldGen.IsRope(tileX, tileY) && Main.tile[tileX, tileY - 1] != null)
			{
				ushort type = Main.tile[tileX, tileY - 1].type;
				int y = (tileY + tileX) % 3 * 18;
				Texture2D tileDrawTexture2 = this.GetTileDrawTexture(Main.tile[tileX, tileY - 1], tileX, tileY);
				Main.spriteBatch.Draw(tileDrawTexture2, new Vector2((float)(tileX * 16 - (int)screenPosition.X), (float)(tileY * 16 - (int)screenPosition.Y)) + screenOffset, new Rectangle?(new Rectangle(90, y, 16, 16)), drawData.tileLight, 0f, default(Vector2), 1f, drawData.tileSpriteEffect, 0f);
			}
			drawData.tileCache.frameNumber();
			if (drawData.tileFrameY != -1)
			{
				Main.spriteBatch.Draw(tileDrawTexture, new Vector2((float)(tileX * 16 - (int)screenPosition.X), (float)(tileY * 16 - (int)screenPosition.Y)) + screenOffset, new Rectangle?(Minecart.GetSourceRect((int)drawData.tileFrameY, Main.tileFrame[314])), drawData.tileLight, 0f, default(Vector2), 1f, drawData.tileSpriteEffect, 0f);
			}
			Main.spriteBatch.Draw(drawData.drawTexture, new Vector2((float)(tileX * 16 - (int)screenPosition.X), (float)(tileY * 16 - (int)screenPosition.Y)) + screenOffset, new Rectangle?(Minecart.GetSourceRect((int)drawData.tileFrameX, Main.tileFrame[314])), drawData.tileLight, 0f, default(Vector2), 1f, drawData.tileSpriteEffect, 0f);
			if (Minecart.DrawLeftDecoration((int)drawData.tileFrameY))
			{
				Main.spriteBatch.Draw(tileDrawTexture, new Vector2((float)(tileX * 16 - (int)screenPosition.X), (float)((tileY + 1) * 16 - (int)screenPosition.Y)) + screenOffset, new Rectangle?(Minecart.GetSourceRect(36, 0)), drawData.tileLight, 0f, default(Vector2), 1f, drawData.tileSpriteEffect, 0f);
			}
			if (Minecart.DrawLeftDecoration((int)drawData.tileFrameX))
			{
				Main.spriteBatch.Draw(drawData.drawTexture, new Vector2((float)(tileX * 16 - (int)screenPosition.X), (float)((tileY + 1) * 16 - (int)screenPosition.Y)) + screenOffset, new Rectangle?(Minecart.GetSourceRect(36, 0)), drawData.tileLight, 0f, default(Vector2), 1f, drawData.tileSpriteEffect, 0f);
			}
			if (Minecart.DrawRightDecoration((int)drawData.tileFrameY))
			{
				Main.spriteBatch.Draw(tileDrawTexture, new Vector2((float)(tileX * 16 - (int)screenPosition.X), (float)((tileY + 1) * 16 - (int)screenPosition.Y)) + screenOffset, new Rectangle?(Minecart.GetSourceRect(37, Main.tileFrame[314])), drawData.tileLight, 0f, default(Vector2), 1f, drawData.tileSpriteEffect, 0f);
			}
			if (Minecart.DrawRightDecoration((int)drawData.tileFrameX))
			{
				Main.spriteBatch.Draw(drawData.drawTexture, new Vector2((float)(tileX * 16 - (int)screenPosition.X), (float)((tileY + 1) * 16 - (int)screenPosition.Y)) + screenOffset, new Rectangle?(Minecart.GetSourceRect(37, 0)), drawData.tileLight, 0f, default(Vector2), 1f, drawData.tileSpriteEffect, 0f);
			}
			if (Minecart.DrawBumper((int)drawData.tileFrameX))
			{
				Main.spriteBatch.Draw(drawData.drawTexture, new Vector2((float)(tileX * 16 - (int)screenPosition.X), (float)((tileY - 1) * 16 - (int)screenPosition.Y)) + screenOffset, new Rectangle?(Minecart.GetSourceRect(39, 0)), drawData.tileLight, 0f, default(Vector2), 1f, drawData.tileSpriteEffect, 0f);
				return;
			}
			if (Minecart.DrawBouncyBumper((int)drawData.tileFrameX))
			{
				Main.spriteBatch.Draw(drawData.drawTexture, new Vector2((float)(tileX * 16 - (int)screenPosition.X), (float)((tileY - 1) * 16 - (int)screenPosition.Y)) + screenOffset, new Rectangle?(Minecart.GetSourceRect(38, 0)), drawData.tileLight, 0f, default(Vector2), 1f, drawData.tileSpriteEffect, 0f);
			}
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x00534FDC File Offset: 0x005331DC
		private void DrawTile_LiquidBehindTile(bool solidLayer, bool inFrontOfPlayers, int waterStyleOverride, Vector2 screenPosition, Vector2 screenOffset, int tileX, int tileY, Tile tileCache)
		{
			Tile tile = Main.tile[tileX + 1, tileY];
			Tile tile2 = Main.tile[tileX - 1, tileY];
			Tile tile3 = Main.tile[tileX, tileY - 1];
			Tile tile4 = Main.tile[tileX, tileY + 1];
			if (tile == null)
			{
				tile = new Tile();
				Main.tile[tileX + 1, tileY] = tile;
			}
			if (tile2 == null)
			{
				tile2 = new Tile();
				Main.tile[tileX - 1, tileY] = tile2;
			}
			if (tile3 == null)
			{
				tile3 = new Tile();
				Main.tile[tileX, tileY - 1] = tile3;
			}
			if (tile4 == null)
			{
				tile4 = new Tile();
				Main.tile[tileX, tileY + 1] = tile4;
			}
			if (!tileCache.active())
			{
				return;
			}
			if (tileCache.inActive() || this._tileSolidTop[(int)tileCache.type])
			{
				return;
			}
			if (tileCache.halfBrick() && (tile2.liquid > 160 || tile.liquid > 160) && Main.instance.waterfallManager.CheckForWaterfall(tileX, tileY))
			{
				return;
			}
			if (TileID.Sets.BlocksWaterDrawingBehindSelf[(int)tileCache.type] && tileCache.slope() == 0)
			{
				return;
			}
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			int num2 = 0;
			bool flag6 = false;
			int num3 = (int)tileCache.slope();
			int num4 = tileCache.blockType();
			if (tileCache.type == 546 && tileCache.liquid > 0)
			{
				flag5 = true;
				flag4 = true;
				flag = true;
				flag2 = true;
				switch (tileCache.liquidType())
				{
				case 0:
					flag6 = true;
					break;
				case 1:
					num2 = 1;
					break;
				case 2:
					num2 = 11;
					break;
				case 3:
					num2 = 14;
					break;
				}
				num = (int)tileCache.liquid;
			}
			else
			{
				if (tileCache.liquid > 0 && num4 != 0 && (num4 != 1 || tileCache.liquid > 160))
				{
					flag5 = true;
					switch (tileCache.liquidType())
					{
					case 0:
						flag6 = true;
						break;
					case 1:
						num2 = 1;
						break;
					case 2:
						num2 = 11;
						break;
					case 3:
						num2 = 14;
						break;
					}
					if ((int)tileCache.liquid > num)
					{
						num = (int)tileCache.liquid;
					}
				}
				if (tile2.liquid > 0 && num3 != 1 && num3 != 3)
				{
					flag = true;
					switch (tile2.liquidType())
					{
					case 0:
						flag6 = true;
						break;
					case 1:
						num2 = 1;
						break;
					case 2:
						num2 = 11;
						break;
					case 3:
						num2 = 14;
						break;
					}
					if ((int)tile2.liquid > num)
					{
						num = (int)tile2.liquid;
					}
				}
				if (tile.liquid > 0 && num3 != 2 && num3 != 4)
				{
					flag2 = true;
					switch (tile.liquidType())
					{
					case 0:
						flag6 = true;
						break;
					case 1:
						num2 = 1;
						break;
					case 2:
						num2 = 11;
						break;
					case 3:
						num2 = 14;
						break;
					}
					if ((int)tile.liquid > num)
					{
						num = (int)tile.liquid;
					}
				}
				if (tile3.liquid > 0 && num3 != 3 && num3 != 4)
				{
					flag3 = true;
					switch (tile3.liquidType())
					{
					case 0:
						flag6 = true;
						break;
					case 1:
						num2 = 1;
						break;
					case 2:
						num2 = 11;
						break;
					case 3:
						num2 = 14;
						break;
					}
				}
				if (tile4.liquid > 0 && num3 != 1 && num3 != 2)
				{
					if (tile4.liquid > 240)
					{
						flag4 = true;
					}
					switch (tile4.liquidType())
					{
					case 0:
						flag6 = true;
						break;
					case 1:
						num2 = 1;
						break;
					case 2:
						num2 = 11;
						break;
					case 3:
						num2 = 14;
						break;
					}
				}
			}
			if (!flag3 && !flag4 && !flag && !flag2 && !flag5)
			{
				return;
			}
			if (waterStyleOverride != -1)
			{
				Main.waterStyle = waterStyleOverride;
			}
			if (num2 == 0)
			{
				num2 = Main.waterStyle;
			}
			VertexColors vertexColors;
			Lighting.GetCornerColors(tileX, tileY, out vertexColors, 1f);
			Vector2 value = new Vector2((float)(tileX * 16), (float)(tileY * 16));
			Rectangle rectangle = new Rectangle(0, 4, 16, 16);
			if (flag4 && (flag || flag2))
			{
				flag = true;
				flag2 = true;
			}
			if (tileCache.active() && (Main.tileSolidTop[(int)tileCache.type] || !Main.tileSolid[(int)tileCache.type]))
			{
				return;
			}
			if ((!flag3 || (!flag && !flag2)) && (!flag4 || !flag3))
			{
				if (flag3)
				{
					rectangle = new Rectangle(0, 4, 16, 4);
					if (tileCache.halfBrick() || tileCache.slope() != 0)
					{
						rectangle = new Rectangle(0, 4, 16, 12);
					}
				}
				else if (flag4 && !flag && !flag2)
				{
					value = new Vector2((float)(tileX * 16), (float)(tileY * 16 + 12));
					rectangle = new Rectangle(0, 4, 16, 4);
				}
				else
				{
					int num5 = (int)((float)(256 - num) / 32f);
					int y = 4;
					if (tile3.liquid == 0 && (num4 != 0 || !WorldGen.SolidTile(tileX, tileY - 1, false)))
					{
						y = 0;
					}
					int num6 = num5 * 2;
					if (tileCache.slope() != 0)
					{
						value = new Vector2((float)(tileX * 16), (float)(tileY * 16 + num6));
						rectangle = new Rectangle(0, num6, 16, 16 - num6);
					}
					else if ((flag && flag2) || tileCache.halfBrick())
					{
						value = new Vector2((float)(tileX * 16), (float)(tileY * 16 + num6));
						rectangle = new Rectangle(0, y, 16, 16 - num6);
					}
					else if (flag)
					{
						value = new Vector2((float)(tileX * 16), (float)(tileY * 16 + num6));
						rectangle = new Rectangle(0, y, 4, 16 - num6);
					}
					else
					{
						value = new Vector2((float)(tileX * 16 + 12), (float)(tileY * 16 + num6));
						rectangle = new Rectangle(0, y, 4, 16 - num6);
					}
				}
			}
			Vector2 vector = value - screenPosition + screenOffset;
			float num7 = 0.5f;
			if (num2 != 1)
			{
				if (num2 == 11)
				{
					num7 = Math.Max(num7 * 1.7f, 1f);
				}
			}
			else
			{
				num7 = 1f;
			}
			if ((double)tileY <= Main.worldSurface || num7 > 1f)
			{
				num7 = 1f;
				if (tileCache.wall == 21)
				{
					num7 = 0.9f;
				}
				else if (tileCache.wall > 0)
				{
					num7 = 0.6f;
				}
			}
			if (tileCache.halfBrick() && tile3.liquid > 0 && tileCache.wall > 0)
			{
				num7 = 0f;
			}
			if (num3 == 4 && tile2.liquid == 0 && !WorldGen.SolidTile(tileX - 1, tileY, false))
			{
				num7 = 0f;
			}
			if (num3 == 3 && tile.liquid == 0 && !WorldGen.SolidTile(tileX + 1, tileY, false))
			{
				num7 = 0f;
			}
			vertexColors.BottomLeftColor *= num7;
			vertexColors.BottomRightColor *= num7;
			vertexColors.TopLeftColor *= num7;
			vertexColors.TopRightColor *= num7;
			bool flag7 = false;
			if (flag6)
			{
				for (int i = 0; i < 15; i++)
				{
					if (Main.IsLiquidStyleWater(i) && Main.liquidAlpha[i] > 0f && i != num2)
					{
						this.DrawPartialLiquid(!solidLayer, tileCache, ref vector, ref rectangle, i, ref vertexColors);
						flag7 = true;
						break;
					}
				}
			}
			VertexColors vertexColors2 = vertexColors;
			float scale = flag7 ? Main.liquidAlpha[num2] : 1f;
			vertexColors2.BottomLeftColor *= scale;
			vertexColors2.BottomRightColor *= scale;
			vertexColors2.TopLeftColor *= scale;
			vertexColors2.TopRightColor *= scale;
			if (num2 == 14)
			{
				LiquidRenderer.SetShimmerVertexColors(ref vertexColors2, solidLayer ? 0.75f : 1f, tileX, tileY);
			}
			this.DrawPartialLiquid(!solidLayer, tileCache, ref vector, ref rectangle, num2, ref vertexColors2);
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x005357CC File Offset: 0x005339CC
		private void CacheSpecialDraws_Part1(int tileX, int tileY, int tileType, int drawDataTileFrameX, int drawDataTileFrameY, bool skipDraw)
		{
			if (tileType == 395)
			{
				Point point = new Point(tileX, tileY);
				if (drawDataTileFrameX % 36 != 0)
				{
					point.X--;
				}
				if (drawDataTileFrameY % 36 != 0)
				{
					point.Y--;
				}
				if (!this._itemFrameTileEntityPositions.ContainsKey(point))
				{
					this._itemFrameTileEntityPositions[point] = TEItemFrame.Find(point.X, point.Y);
					if (this._itemFrameTileEntityPositions[point] != -1)
					{
						this.AddSpecialLegacyPoint(point);
					}
				}
			}
			if (tileType == 520)
			{
				Point point2 = new Point(tileX, tileY);
				if (!this._foodPlatterTileEntityPositions.ContainsKey(point2))
				{
					this._foodPlatterTileEntityPositions[point2] = TEFoodPlatter.Find(point2.X, point2.Y);
					if (this._foodPlatterTileEntityPositions[point2] != -1)
					{
						this.AddSpecialLegacyPoint(point2);
					}
				}
			}
			if (tileType == 471)
			{
				Point point3 = new Point(tileX, tileY);
				point3.X -= drawDataTileFrameX % 54 / 18;
				point3.Y -= drawDataTileFrameY % 54 / 18;
				if (!this._weaponRackTileEntityPositions.ContainsKey(point3))
				{
					this._weaponRackTileEntityPositions[point3] = TEWeaponsRack.Find(point3.X, point3.Y);
					if (this._weaponRackTileEntityPositions[point3] != -1)
					{
						this.AddSpecialLegacyPoint(point3);
					}
				}
			}
			if (tileType == 470)
			{
				Point point4 = new Point(tileX, tileY);
				point4.X -= drawDataTileFrameX % 36 / 18;
				point4.Y -= drawDataTileFrameY % 54 / 18;
				if (!this._displayDollTileEntityPositions.ContainsKey(point4))
				{
					this._displayDollTileEntityPositions[point4] = TEDisplayDoll.Find(point4.X, point4.Y);
					if (this._displayDollTileEntityPositions[point4] != -1)
					{
						this.AddSpecialLegacyPoint(point4);
					}
				}
			}
			if (tileType == 475)
			{
				Point point5 = new Point(tileX, tileY);
				point5.X -= drawDataTileFrameX % 54 / 18;
				point5.Y -= drawDataTileFrameY % 72 / 18;
				if (!this._hatRackTileEntityPositions.ContainsKey(point5))
				{
					this._hatRackTileEntityPositions[point5] = TEHatRack.Find(point5.X, point5.Y);
					if (this._hatRackTileEntityPositions[point5] != -1)
					{
						this.AddSpecialLegacyPoint(point5);
					}
				}
			}
			if (tileType == 412 && drawDataTileFrameX == 0 && drawDataTileFrameY == 0)
			{
				this.AddSpecialLegacyPoint(tileX, tileY);
			}
			if (tileType == 620 && drawDataTileFrameX == 0 && drawDataTileFrameY == 0)
			{
				this.AddSpecialLegacyPoint(tileX, tileY);
			}
			if (tileType == 237 && drawDataTileFrameX == 18 && drawDataTileFrameY == 0)
			{
				this.AddSpecialLegacyPoint(tileX, tileY);
			}
			if (skipDraw)
			{
				return;
			}
			if (tileType <= 589)
			{
				if (tileType != 5)
				{
					if (tileType != 323)
					{
						if (tileType - 583 > 6)
						{
							return;
						}
					}
					else
					{
						if (drawDataTileFrameX <= 132 && drawDataTileFrameX >= 88)
						{
							this.AddSpecialPoint(tileX, tileY, TileDrawing.TileCounterType.Tree);
							return;
						}
						return;
					}
				}
			}
			else if (tileType != 596 && tileType != 616 && tileType != 634)
			{
				return;
			}
			if (drawDataTileFrameY >= 198 && drawDataTileFrameX >= 22)
			{
				this.AddSpecialPoint(tileX, tileY, TileDrawing.TileCounterType.Tree);
			}
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x00535ACC File Offset: 0x00533CCC
		private void CacheSpecialDraws_Part2(int tileX, int tileY, TileDrawInfo drawData, bool skipDraw)
		{
			if (TileID.Sets.BasicChest[(int)drawData.typeCache])
			{
				Point point = new Point(tileX, tileY);
				if (drawData.tileFrameX % 36 != 0)
				{
					point.X--;
				}
				if (drawData.tileFrameY % 36 != 0)
				{
					point.Y--;
				}
				if (!this._chestPositions.ContainsKey(point))
				{
					this._chestPositions[point] = Chest.FindChest(point.X, point.Y);
				}
				int num = (int)(drawData.tileFrameX / 18);
				short num2 = drawData.tileFrameY / 18;
				int num3 = (int)(drawData.tileFrameX / 36);
				int num4 = num * 18;
				drawData.addFrX = num4 - (int)drawData.tileFrameX;
				int num5 = (int)(num2 * 18);
				if (this._chestPositions[point] != -1)
				{
					int frame = Main.chest[this._chestPositions[point]].frame;
					if (frame == 1)
					{
						num5 += 38;
					}
					if (frame == 2)
					{
						num5 += 76;
					}
				}
				drawData.addFrY = num5 - (int)drawData.tileFrameY;
				if (num2 != 0)
				{
					drawData.tileHeight = 18;
				}
				if (drawData.typeCache == 21 && (num3 == 48 || num3 == 49))
				{
					drawData.glowSourceRect = new Rectangle(16 * (num % 2), (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight);
				}
			}
			if (drawData.typeCache == 378)
			{
				Point point2 = new Point(tileX, tileY);
				if (drawData.tileFrameX % 36 != 0)
				{
					point2.X--;
				}
				if (drawData.tileFrameY % 54 != 0)
				{
					point2.Y -= (int)(drawData.tileFrameY / 18);
				}
				if (!this._trainingDummyTileEntityPositions.ContainsKey(point2))
				{
					this._trainingDummyTileEntityPositions[point2] = TETrainingDummy.Find(point2.X, point2.Y);
				}
				if (this._trainingDummyTileEntityPositions[point2] != -1)
				{
					int npc = ((TETrainingDummy)TileEntity.ByID[this._trainingDummyTileEntityPositions[point2]]).npc;
					if (npc != -1)
					{
						int num6 = Main.npc[npc].frame.Y / 55;
						num6 *= 54;
						num6 += (int)drawData.tileFrameY;
						drawData.addFrY = num6 - (int)drawData.tileFrameY;
					}
				}
			}
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x00535D04 File Offset: 0x00533F04
		private static Color GetFinalLight(Tile tileCache, ushort typeCache, Color tileLight, Color tint)
		{
			int num = (int)((float)(tileLight.R * tint.R) / 255f);
			int num2 = (int)((float)(tileLight.G * tint.G) / 255f);
			int num3 = (int)((float)(tileLight.B * tint.B) / 255f);
			if (num > 255)
			{
				num = 255;
			}
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num3 > 255)
			{
				num3 = 255;
			}
			num3 <<= 16;
			num2 <<= 8;
			tileLight.PackedValue = (uint)(num | num2 | num3 | -16777216);
			if (tileCache.fullbrightBlock())
			{
				tileLight = Color.White;
			}
			if (tileCache.inActive())
			{
				tileLight = tileCache.actColor(tileLight);
			}
			else if (TileDrawing.ShouldTileShine(typeCache, tileCache.frameX))
			{
				tileLight = Main.shine(tileLight, (int)typeCache);
			}
			return tileLight;
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x00535DD8 File Offset: 0x00533FD8
		private static void GetFinalLight(Tile tileCache, ushort typeCache, ref Vector3 tileLight, ref Vector3 tint)
		{
			tileLight *= tint;
			if (tileCache.inActive())
			{
				tileCache.actColor(ref tileLight);
				return;
			}
			if (TileDrawing.ShouldTileShine(typeCache, tileCache.frameX))
			{
				Main.shine(ref tileLight, (int)typeCache);
			}
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x00535E18 File Offset: 0x00534018
		private static bool ShouldTileShine(ushort type, short frameX)
		{
			if ((Main.shimmerAlpha > 0f && Main.tileSolid[(int)type]) || type == 165)
			{
				return true;
			}
			if (!Main.tileShine2[(int)type])
			{
				return false;
			}
			if (type != 21 && type != 441)
			{
				return type - 467 > 1 || (frameX >= 144 && frameX < 178);
			}
			return frameX >= 36 && frameX < 178;
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x00535E8C File Offset: 0x0053408C
		private static bool IsTileDangerous(Player localPlayer, Tile tileCache, ushort typeCache)
		{
			bool flag = false || typeCache == 135 || typeCache == 137 || TileID.Sets.Boulders[(int)typeCache] || typeCache == 141 || typeCache == 210 || typeCache == 442 || typeCache == 443 || typeCache == 444 || typeCache == 411 || typeCache == 485 || typeCache == 85 || typeCache == 654 || (typeCache == 314 && Minecart.IsPressurePlate(tileCache));
			flag |= (Main.getGoodWorld && typeCache == 230);
			flag |= (Main.dontStarveWorld && typeCache == 80);
			if (tileCache.slope() == 0 && !tileCache.inActive())
			{
				flag = (flag || typeCache == 32 || typeCache == 69 || typeCache == 48 || typeCache == 232 || typeCache == 352 || typeCache == 483 || typeCache == 482 || typeCache == 481 || typeCache == 51 || typeCache == 229);
				if (!localPlayer.fireWalk)
				{
					flag = (flag || typeCache == 37 || typeCache == 58 || typeCache == 76);
				}
				if (!localPlayer.iceSkate)
				{
					flag = (flag || typeCache == 162);
				}
			}
			return flag;
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x00535FD6 File Offset: 0x005341D6
		private bool IsTileDrawLayerSolid(ushort typeCache)
		{
			if (TileID.Sets.DrawTileInSolidLayer[(int)typeCache] != null)
			{
				return TileID.Sets.DrawTileInSolidLayer[(int)typeCache].Value;
			}
			return this._tileSolid[(int)typeCache];
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x00536004 File Offset: 0x00534204
		private void GetTileOutlineInfo(int x, int y, ushort typeCache, ref Color tileLight, ref Texture2D highlightTexture, ref Color highlightColor)
		{
			bool isTileSelected;
			if (Main.InSmartCursorHighlightArea(x, y, out isTileSelected))
			{
				int num = (int)((tileLight.R + tileLight.G + tileLight.B) / 3);
				if (num > 10)
				{
					highlightTexture = TextureAssets.HighlightMask[(int)typeCache].Value;
					highlightColor = Colors.GetSelectionGlowColor(isTileSelected, num);
				}
			}
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x00536058 File Offset: 0x00534258
		private void DrawPartialLiquid(bool behindBlocks, Tile tileCache, ref Vector2 position, ref Rectangle liquidSize, int liquidType, ref VertexColors colors)
		{
			int num = (int)tileCache.slope();
			bool flag = !TileID.Sets.BlocksWaterDrawingBehindSelf[(int)tileCache.type];
			if (!behindBlocks)
			{
				flag = false;
			}
			if (flag || num == 0)
			{
				Main.tileBatch.Draw(TextureAssets.Liquid[liquidType].Value, position, new Rectangle?(liquidSize), colors, default(Vector2), 1f, SpriteEffects.None);
				return;
			}
			liquidSize.X += 18 * (num - 1);
			if (num == 1)
			{
				Main.tileBatch.Draw(TextureAssets.LiquidSlope[liquidType].Value, position, new Rectangle?(liquidSize), colors, Vector2.Zero, 1f, SpriteEffects.None);
				return;
			}
			if (num == 2)
			{
				Main.tileBatch.Draw(TextureAssets.LiquidSlope[liquidType].Value, position, new Rectangle?(liquidSize), colors, Vector2.Zero, 1f, SpriteEffects.None);
				return;
			}
			if (num == 3)
			{
				Main.tileBatch.Draw(TextureAssets.LiquidSlope[liquidType].Value, position, new Rectangle?(liquidSize), colors, Vector2.Zero, 1f, SpriteEffects.None);
				return;
			}
			if (num == 4)
			{
				Main.tileBatch.Draw(TextureAssets.LiquidSlope[liquidType].Value, position, new Rectangle?(liquidSize), colors, Vector2.Zero, 1f, SpriteEffects.None);
			}
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x005361D7 File Offset: 0x005343D7
		private bool InAPlaceWithWind(int x, int y, int width, int height)
		{
			return WorldGen.InAPlaceWithWind(x, y, width, height);
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x005361E4 File Offset: 0x005343E4
		private void GetTileDrawData(int x, int y, Tile tileCache, ushort typeCache, ref short tileFrameX, ref short tileFrameY, out int tileWidth, out int tileHeight, out int tileTop, out int halfBrickHeight, out int addFrX, out int addFrY, out SpriteEffects tileSpriteEffect, out Texture2D glowTexture, out Rectangle glowSourceRect, out Color glowColor)
		{
			tileTop = 0;
			tileWidth = 16;
			tileHeight = 16;
			halfBrickHeight = 0;
			addFrY = Main.tileFrame[(int)typeCache] * 38;
			addFrX = 0;
			tileSpriteEffect = SpriteEffects.None;
			glowTexture = null;
			glowSourceRect = Rectangle.Empty;
			glowColor = Color.Transparent;
			Color color = Lighting.GetColor(x, y);
			switch (typeCache)
			{
			case 3:
			case 24:
			case 61:
			case 71:
			case 110:
			case 201:
			case 637:
				tileHeight = 20;
				if (x % 2 == 0)
				{
					tileSpriteEffect = SpriteEffects.FlipHorizontally;
				}
				break;
			case 4:
				tileWidth = 20;
				tileHeight = 20;
				if (WorldGen.SolidTile(x, y - 1, false))
				{
					tileTop = 4;
				}
				break;
			case 5:
			{
				tileWidth = 20;
				tileHeight = 20;
				int treeBiome = TileDrawing.GetTreeBiome(x, y, (int)tileFrameX, (int)tileFrameY);
				tileFrameX += (short)(176 * (treeBiome + 1));
				break;
			}
			case 12:
			case 31:
			case 96:
			case 639:
			case 665:
				addFrY = Main.tileFrame[(int)typeCache] * 36;
				break;
			case 14:
			case 21:
			case 411:
			case 467:
			case 469:
				if (tileFrameY == 18)
				{
					tileHeight = 18;
				}
				break;
			case 15:
			case 497:
				if (tileFrameY % 40 == 18)
				{
					tileHeight = 18;
				}
				break;
			case 16:
			case 17:
			case 18:
			case 26:
			case 32:
			case 69:
			case 72:
			case 77:
			case 79:
			case 124:
			case 137:
			case 138:
			case 352:
			case 462:
			case 487:
			case 488:
			case 574:
			case 575:
			case 576:
			case 577:
			case 578:
			case 664:
				tileHeight = 18;
				break;
			case 20:
			case 590:
			case 595:
				tileHeight = 18;
				if (x % 2 == 0)
				{
					tileSpriteEffect = SpriteEffects.FlipHorizontally;
				}
				break;
			case 27:
				if (tileFrameY % 74 == 54)
				{
					tileHeight = 18;
				}
				break;
			case 28:
			case 105:
			case 470:
			case 475:
			case 506:
			case 547:
			case 548:
			case 552:
			case 560:
			case 597:
			case 613:
			case 621:
			case 622:
			case 623:
			case 653:
				tileTop = 2;
				break;
			case 33:
			case 49:
			case 174:
			case 372:
			case 646:
				tileHeight = 20;
				tileTop = -4;
				break;
			case 52:
			case 62:
			case 115:
			case 205:
			case 382:
			case 528:
			case 636:
			case 638:
				tileTop = -2;
				if (x % 2 == 0)
				{
					tileSpriteEffect = SpriteEffects.FlipHorizontally;
				}
				break;
			case 73:
			case 74:
			case 113:
				tileTop = -12;
				tileHeight = 32;
				if (x % 2 == 0)
				{
					tileSpriteEffect = SpriteEffects.FlipHorizontally;
				}
				break;
			case 78:
			case 85:
			case 100:
			case 133:
			case 134:
			case 173:
			case 210:
			case 233:
			case 254:
			case 283:
			case 378:
			case 457:
			case 466:
			case 520:
			case 651:
			case 652:
				tileTop = 2;
				break;
			case 80:
			case 142:
			case 143:
				tileTop = 2;
				break;
			case 81:
				tileTop -= 8;
				tileHeight = 26;
				tileWidth = 24;
				if (x % 2 == 0)
				{
					tileSpriteEffect = SpriteEffects.FlipHorizontally;
				}
				break;
			case 82:
			case 83:
			case 84:
				tileHeight = 20;
				tileTop = -2;
				if (x % 2 == 0)
				{
					tileSpriteEffect = SpriteEffects.FlipHorizontally;
				}
				break;
			case 89:
				tileTop = 2;
				break;
			case 102:
				tileTop = 2;
				break;
			case 106:
				addFrY = Main.tileFrame[(int)typeCache] * 54;
				break;
			case 129:
				addFrY = 0;
				if (tileFrameX >= 324)
				{
					int num = (int)((tileFrameX - 324) / 18);
					int num2 = (num + Main.tileFrame[(int)typeCache]) % 6 - num;
					addFrX = num2 * 18;
				}
				break;
			case 132:
			case 135:
				tileTop = 2;
				tileHeight = 18;
				break;
			case 136:
				if (tileFrameX == 0)
				{
					tileTop = 2;
				}
				break;
			case 139:
			{
				tileTop = 2;
				int num3 = (int)(tileFrameY / 2016);
				addFrY -= 2016 * num3;
				addFrX += 72 * num3;
				break;
			}
			case 172:
			case 376:
				if (tileFrameY % 38 == 18)
				{
					tileHeight = 18;
				}
				break;
			case 178:
				if (tileFrameY <= 36)
				{
					tileTop = 2;
				}
				break;
			case 184:
				tileWidth = 20;
				if (tileFrameY <= 36)
				{
					tileTop = 2;
				}
				else if (tileFrameY <= 108)
				{
					tileTop = -2;
				}
				break;
			case 185:
			case 186:
			case 187:
				tileTop = 2;
				if (typeCache == 185)
				{
					if (tileFrameY == 18 && tileFrameX >= 576 && tileFrameX <= 882)
					{
						Main.tileShine2[185] = true;
					}
					else
					{
						Main.tileShine2[185] = false;
					}
					if (tileFrameY == 18)
					{
						int num4 = (int)(tileFrameX / 1908);
						addFrX -= 1908 * num4;
						addFrY += 18 * num4;
					}
				}
				else if (typeCache == 186)
				{
					if (tileFrameX >= 864 && tileFrameX <= 1170)
					{
						Main.tileShine2[186] = true;
					}
					else
					{
						Main.tileShine2[186] = false;
					}
				}
				else if (typeCache == 187)
				{
					int num5 = (int)(tileFrameX / 1890);
					addFrX -= 1890 * num5;
					addFrY += 36 * num5;
				}
				break;
			case 207:
				tileTop = 2;
				if (tileFrameY >= 72)
				{
					addFrY = Main.tileFrame[(int)typeCache];
					int num6 = x;
					if (tileFrameX % 36 != 0)
					{
						num6--;
					}
					addFrY += num6 % 6;
					if (addFrY >= 6)
					{
						addFrY -= 6;
					}
					addFrY *= 72;
				}
				else
				{
					addFrY = 0;
				}
				break;
			case 215:
				if (tileFrameY < 36)
				{
					addFrY = Main.tileFrame[(int)typeCache] * 36;
				}
				else
				{
					addFrY = 252;
				}
				tileTop = 2;
				break;
			case 217:
			case 218:
			case 564:
				addFrY = Main.tileFrame[(int)typeCache] * 36;
				tileTop = 2;
				break;
			case 219:
			case 220:
			case 642:
				addFrY = Main.tileFrame[(int)typeCache] * 54;
				tileTop = 2;
				break;
			case 227:
				tileWidth = 32;
				tileHeight = 38;
				if (tileFrameX == 238)
				{
					tileTop -= 6;
				}
				else
				{
					tileTop -= 20;
				}
				if (tileFrameX == 204)
				{
					bool flag;
					bool flag2;
					bool flag3;
					WorldGen.GetCactusType(x, y, (int)tileFrameX, (int)tileFrameY, out flag, out flag2, out flag3);
					if (flag2)
					{
						tileFrameX += 238;
					}
					if (flag)
					{
						tileFrameX += 204;
					}
					if (flag3)
					{
						tileFrameX += 272;
					}
				}
				if (x % 2 == 0)
				{
					tileSpriteEffect = SpriteEffects.FlipHorizontally;
				}
				break;
			case 228:
			case 231:
			case 243:
			case 247:
				tileTop = 2;
				addFrY = Main.tileFrame[(int)typeCache] * 54;
				break;
			case 235:
				addFrY = Main.tileFrame[(int)typeCache] * 18;
				break;
			case 238:
				tileTop = 2;
				addFrY = Main.tileFrame[(int)typeCache] * 36;
				break;
			case 244:
				tileTop = 2;
				if (tileFrameX < 54)
				{
					addFrY = Main.tileFrame[(int)typeCache] * 36;
				}
				else
				{
					addFrY = 0;
				}
				break;
			case 270:
			case 271:
			case 581:
			{
				int i = Main.tileFrame[(int)typeCache] + x % 6;
				if (x % 2 == 0)
				{
					i += 3;
				}
				if (x % 3 == 0)
				{
					i += 3;
				}
				if (x % 4 == 0)
				{
					i += 3;
				}
				while (i > 5)
				{
					i -= 6;
				}
				addFrX = i * 18;
				addFrY = 0;
				if (x % 2 == 0)
				{
					tileSpriteEffect = SpriteEffects.FlipHorizontally;
				}
				break;
			}
			case 272:
				addFrY = 0;
				break;
			case 275:
			case 276:
			case 277:
			case 278:
			case 279:
			case 280:
			case 281:
			case 296:
			case 297:
			case 309:
			case 358:
			case 359:
			case 413:
			case 414:
			case 542:
			case 550:
			case 551:
			case 553:
			case 554:
			case 558:
			case 559:
			case 599:
			case 600:
			case 601:
			case 602:
			case 603:
			case 604:
			case 605:
			case 606:
			case 607:
			case 608:
			case 609:
			case 610:
			case 611:
			case 612:
			case 632:
			case 640:
			case 643:
			case 644:
			case 645:
			{
				tileTop = 2;
				Main.critterCage = true;
				int bigAnimalCageFrame = this.GetBigAnimalCageFrame(x, y, (int)tileFrameX, (int)tileFrameY);
				if (typeCache <= 414)
				{
					if (typeCache <= 309)
					{
						switch (typeCache)
						{
						case 275:
							goto IL_1C63;
						case 276:
							goto IL_1C9C;
						case 277:
							addFrY = Main.mallardCageFrame[bigAnimalCageFrame] * 54;
							goto IL_22BF;
						case 278:
							addFrY = Main.duckCageFrame[bigAnimalCageFrame] * 54;
							goto IL_22BF;
						case 279:
							break;
						case 280:
							addFrY = Main.blueBirdCageFrame[bigAnimalCageFrame] * 54;
							goto IL_22BF;
						case 281:
							addFrY = Main.redBirdCageFrame[bigAnimalCageFrame] * 54;
							goto IL_22BF;
						default:
							if (typeCache - 296 <= 1)
							{
								addFrY = Main.scorpionCageFrame[0, bigAnimalCageFrame] * 54;
								goto IL_22BF;
							}
							if (typeCache != 309)
							{
								goto IL_22BF;
							}
							addFrY = Main.penguinCageFrame[bigAnimalCageFrame] * 54;
							goto IL_22BF;
						}
					}
					else if (typeCache != 358)
					{
						if (typeCache == 359)
						{
							goto IL_1C63;
						}
						if (typeCache - 413 > 1)
						{
							break;
						}
						goto IL_1C9C;
					}
					addFrY = Main.birdCageFrame[bigAnimalCageFrame] * 54;
					break;
				}
				if (typeCache > 605)
				{
					if (typeCache <= 632)
					{
						if (typeCache - 606 <= 6)
						{
							goto IL_1C9C;
						}
						if (typeCache != 632)
						{
							break;
						}
					}
					else if (typeCache != 640 && typeCache - 643 > 2)
					{
						break;
					}
					addFrY = Main.macawCageFrame[bigAnimalCageFrame] * 54;
					break;
				}
				if (typeCache == 542)
				{
					addFrY = Main.owlCageFrame[bigAnimalCageFrame] * 54;
					break;
				}
				switch (typeCache)
				{
				case 550:
				case 551:
					addFrY = Main.turtleCageFrame[bigAnimalCageFrame] * 54;
					goto IL_22BF;
				case 552:
				case 555:
				case 556:
				case 557:
					goto IL_22BF;
				case 553:
					addFrY = Main.grebeCageFrame[bigAnimalCageFrame] * 54;
					goto IL_22BF;
				case 554:
					addFrY = Main.seagullCageFrame[bigAnimalCageFrame] * 54;
					goto IL_22BF;
				case 558:
				case 559:
					addFrY = Main.seahorseCageFrame[bigAnimalCageFrame] * 54;
					goto IL_22BF;
				default:
					if (typeCache - 599 > 6)
					{
						goto IL_22BF;
					}
					break;
				}
				IL_1C63:
				addFrY = Main.bunnyCageFrame[bigAnimalCageFrame] * 54;
				break;
				IL_1C9C:
				addFrY = Main.squirrelCageFrame[bigAnimalCageFrame] * 54;
				break;
			}
			case 282:
			case 505:
			case 543:
			{
				tileTop = 2;
				Main.critterCage = true;
				int waterAnimalCageFrame = this.GetWaterAnimalCageFrame(x, y, (int)tileFrameX, (int)tileFrameY);
				addFrY = Main.fishBowlFrame[waterAnimalCageFrame] * 36;
				break;
			}
			case 285:
			case 286:
			case 298:
			case 299:
			case 310:
			case 339:
			case 361:
			case 362:
			case 363:
			case 364:
			case 391:
			case 392:
			case 393:
			case 394:
			case 532:
			case 533:
			case 538:
			case 544:
			case 555:
			case 556:
			case 582:
			case 619:
			case 629:
			{
				tileTop = 2;
				Main.critterCage = true;
				int smallAnimalCageFrame = this.GetSmallAnimalCageFrame(x, y, (int)tileFrameX, (int)tileFrameY);
				if (typeCache <= 391)
				{
					if (typeCache > 299)
					{
						if (typeCache <= 339)
						{
							if (typeCache == 310)
							{
								goto IL_1F43;
							}
							if (typeCache != 339)
							{
								break;
							}
						}
						else
						{
							switch (typeCache)
							{
							case 361:
								goto IL_1F0A;
							case 362:
								break;
							case 363:
								goto IL_1F30;
							case 364:
								goto IL_1F43;
							default:
								if (typeCache != 391)
								{
									goto IL_22BF;
								}
								goto IL_1F43;
							}
						}
						addFrY = Main.grasshopperCageFrame[smallAnimalCageFrame] * 36;
						break;
					}
					if (typeCache <= 286)
					{
						if (typeCache == 285)
						{
							addFrY = Main.snailCageFrame[smallAnimalCageFrame] * 36;
							break;
						}
						if (typeCache != 286)
						{
							break;
						}
						goto IL_1EF7;
					}
					else if (typeCache != 298)
					{
						if (typeCache != 299)
						{
							break;
						}
						goto IL_1F30;
					}
					IL_1F0A:
					addFrY = Main.frogCageFrame[smallAnimalCageFrame] * 36;
					break;
					IL_1F30:
					addFrY = Main.mouseCageFrame[smallAnimalCageFrame] * 36;
					break;
				}
				if (typeCache <= 538)
				{
					if (typeCache <= 532)
					{
						if (typeCache - 392 <= 2)
						{
							addFrY = Main.slugCageFrame[(int)(typeCache - 392), smallAnimalCageFrame] * 36;
							break;
						}
						if (typeCache != 532)
						{
							break;
						}
						addFrY = Main.maggotCageFrame[smallAnimalCageFrame] * 36;
						break;
					}
					else
					{
						if (typeCache == 533)
						{
							addFrY = Main.ratCageFrame[smallAnimalCageFrame] * 36;
							break;
						}
						if (typeCache != 538)
						{
							break;
						}
					}
				}
				else if (typeCache <= 556)
				{
					if (typeCache != 544)
					{
						if (typeCache - 555 > 1)
						{
							break;
						}
						addFrY = Main.waterStriderCageFrame[smallAnimalCageFrame] * 36;
						break;
					}
				}
				else
				{
					if (typeCache == 582)
					{
						goto IL_1EF7;
					}
					if (typeCache == 619)
					{
						goto IL_1F43;
					}
					if (typeCache != 629)
					{
						break;
					}
				}
				addFrY = Main.ladybugCageFrame[smallAnimalCageFrame] * 36;
				break;
				IL_1EF7:
				addFrY = Main.snail2CageFrame[smallAnimalCageFrame] * 36;
				break;
				IL_1F43:
				addFrY = Main.wormCageFrame[smallAnimalCageFrame] * 36;
				break;
			}
			case 288:
			case 289:
			case 290:
			case 291:
			case 292:
			case 293:
			case 294:
			case 295:
			case 360:
			case 580:
			case 620:
			{
				tileTop = 2;
				Main.critterCage = true;
				int waterAnimalCageFrame2 = this.GetWaterAnimalCageFrame(x, y, (int)tileFrameX, (int)tileFrameY);
				int num7 = (int)(typeCache - 288);
				if (typeCache == 360 || typeCache == 580 || typeCache == 620)
				{
					num7 = 8;
				}
				addFrY = Main.butterflyCageFrame[num7, waterAnimalCageFrame2] * 36;
				break;
			}
			case 300:
			case 301:
			case 302:
			case 303:
			case 304:
			case 305:
			case 306:
			case 307:
			case 308:
			case 354:
			case 355:
			case 499:
				addFrY = Main.tileFrame[(int)typeCache] * 54;
				tileTop = 2;
				break;
			case 316:
			case 317:
			case 318:
			{
				tileTop = 2;
				Main.critterCage = true;
				int smallAnimalCageFrame2 = this.GetSmallAnimalCageFrame(x, y, (int)tileFrameX, (int)tileFrameY);
				int num8 = (int)(typeCache - 316);
				addFrY = Main.jellyfishCageFrame[num8, smallAnimalCageFrame2] * 36;
				break;
			}
			case 323:
			{
				tileWidth = 20;
				tileHeight = 20;
				int palmTreeBiome = this.GetPalmTreeBiome(x, y);
				tileFrameY = (short)(22 * palmTreeBiome);
				break;
			}
			case 324:
				tileWidth = 20;
				tileHeight = 20;
				tileTop = -2;
				if (x % 2 == 0)
				{
					tileSpriteEffect = SpriteEffects.FlipHorizontally;
				}
				break;
			case 326:
			case 327:
			case 328:
			case 329:
			case 345:
			case 351:
			case 421:
			case 422:
			case 458:
			case 459:
				addFrY = Main.tileFrame[(int)typeCache] * 90;
				break;
			case 330:
			case 331:
			case 332:
			case 333:
				tileTop += 2;
				break;
			case 336:
			case 340:
			case 341:
			case 342:
			case 343:
			case 344:
				addFrY = Main.tileFrame[(int)typeCache] * 90;
				tileTop = 2;
				break;
			case 349:
			{
				tileTop = 2;
				int num9 = (int)(tileFrameX % 36);
				int num10 = (int)(tileFrameY % 54);
				int num11;
				if (Animation.GetTemporaryFrame(x - num9 / 18, y - num10 / 18, out num11))
				{
					tileFrameX = (short)(36 * num11 + num9);
				}
				break;
			}
			case 377:
				addFrY = Main.tileFrame[(int)typeCache] * 38;
				tileTop = 2;
				break;
			case 379:
				addFrY = Main.tileFrame[(int)typeCache] * 90;
				break;
			case 388:
			case 389:
			{
				TileObjectData.GetTileData((int)typeCache, (int)(tileFrameX / 18), 0);
				int num12 = 94;
				tileTop = -2;
				if ((int)tileFrameY == num12 - 20 || (int)tileFrameY == num12 * 2 - 20 || tileFrameY == 0 || (int)tileFrameY == num12)
				{
					tileHeight = 18;
				}
				if (tileFrameY != 0 && (int)tileFrameY != num12)
				{
					tileTop = 0;
				}
				break;
			}
			case 390:
				addFrY = Main.tileFrame[(int)typeCache] * 36;
				break;
			case 405:
			{
				tileHeight = 16;
				if (tileFrameY > 0)
				{
					tileHeight = 18;
				}
				int num13 = Main.tileFrame[(int)typeCache];
				if (tileFrameX >= 54)
				{
					num13 = 0;
				}
				addFrY = num13 * 38;
				break;
			}
			case 406:
			{
				tileHeight = 16;
				if (tileFrameY % 54 >= 36)
				{
					tileHeight = 18;
				}
				int num14 = Main.tileFrame[(int)typeCache];
				if (tileFrameY >= 108)
				{
					num14 = (int)(6 - tileFrameY / 54);
				}
				else if (tileFrameY >= 54)
				{
					num14 = Main.tileFrame[(int)typeCache] - 1;
				}
				addFrY = num14 * 56;
				addFrY += (int)(tileFrameY / 54 * 2);
				break;
			}
			case 410:
				if (tileFrameY == 36)
				{
					tileHeight = 18;
				}
				if (tileFrameY >= 56)
				{
					addFrY = Main.tileFrame[(int)typeCache];
					addFrY *= 56;
				}
				else
				{
					addFrY = 0;
				}
				break;
			case 412:
				addFrY = 0;
				tileTop = 2;
				break;
			case 426:
			case 430:
			case 431:
			case 432:
			case 433:
			case 434:
				addFrY = 90;
				break;
			case 428:
				tileTop += 4;
				if (PressurePlateHelper.PressurePlatesPressed.ContainsKey(new Point(x, y)))
				{
					addFrX += 18;
				}
				break;
			case 441:
			case 468:
			{
				if (tileFrameY == 18)
				{
					tileHeight = 18;
				}
				int num15 = (int)(tileFrameX % 36);
				int num16 = (int)(tileFrameY % 38);
				int num17;
				if (Animation.GetTemporaryFrame(x - num15 / 18, y - num16 / 18, out num17))
				{
					tileFrameY = (short)(38 * num17 + num16);
				}
				break;
			}
			case 442:
				tileWidth = 20;
				tileHeight = 20;
				switch (tileFrameX / 22)
				{
				case 1:
					tileTop = -4;
					break;
				case 2:
					tileTop = -2;
					tileWidth = 24;
					break;
				case 3:
					tileTop = -2;
					break;
				}
				break;
			case 443:
				if (tileFrameX / 36 >= 2)
				{
					tileTop = -2;
				}
				else
				{
					tileTop = 2;
				}
				break;
			case 452:
			{
				int num18 = Main.tileFrame[(int)typeCache];
				if (tileFrameX >= 54)
				{
					num18 = 0;
				}
				addFrY = num18 * 54;
				break;
			}
			case 453:
			{
				int num19 = Main.tileFrameCounter[(int)typeCache];
				num19 /= 20;
				int num20 = y - (int)(tileFrameY / 18);
				num19 += num20 + x;
				num19 %= 3;
				addFrY = num19 * 54;
				break;
			}
			case 454:
				addFrY = Main.tileFrame[(int)typeCache] * 54;
				break;
			case 455:
			{
				addFrY = 0;
				tileTop = 2;
				int num21 = 1 + Main.tileFrame[(int)typeCache];
				if (!BirthdayParty.PartyIsUp)
				{
					num21 = 0;
				}
				addFrY = num21 * 54;
				break;
			}
			case 456:
			{
				int num22 = Main.tileFrameCounter[(int)typeCache];
				num22 /= 20;
				int num23 = y - (int)(tileFrameY / 18);
				int num24 = x - (int)(tileFrameX / 18);
				num22 += num23 + num24;
				num22 %= 4;
				addFrY = num22 * 54;
				break;
			}
			case 463:
			case 464:
				addFrY = Main.tileFrame[(int)typeCache] * 72;
				tileTop = 2;
				break;
			case 476:
				tileWidth = 20;
				tileHeight = 18;
				break;
			case 480:
			case 509:
			case 657:
				tileTop = 2;
				if (tileFrameY >= 54)
				{
					addFrY = Main.tileFrame[(int)typeCache];
					addFrY *= 54;
				}
				else
				{
					addFrY = 0;
				}
				break;
			case 485:
			{
				tileTop = 2;
				int num25 = Main.tileFrameCounter[(int)typeCache];
				num25 /= 5;
				int num26 = y - (int)(tileFrameY / 18);
				int num27 = x - (int)(tileFrameX / 18);
				num25 += num26 + num27;
				num25 %= 4;
				addFrY = num25 * 36;
				break;
			}
			case 489:
			{
				tileTop = 2;
				int num28 = y - (int)(tileFrameY / 18);
				int num29 = x - (int)(tileFrameX / 18);
				if (this.InAPlaceWithWind(num29, num28, 2, 3))
				{
					int num30 = Main.tileFrameCounter[(int)typeCache];
					num30 /= 5;
					num30 += num28 + num29;
					num30 %= 16;
					addFrY = num30 * 54;
				}
				break;
			}
			case 490:
			{
				tileTop = 2;
				int y2 = y - (int)(tileFrameY / 18);
				int x2 = x - (int)(tileFrameX / 18);
				bool flag4 = this.InAPlaceWithWind(x2, y2, 2, 2);
				int num31 = flag4 ? Main.tileFrame[(int)typeCache] : 0;
				int num32 = 0;
				if (flag4)
				{
					if (Math.Abs(Main.WindForVisuals) > 0.5f)
					{
						switch (Main.weatherVaneBobframe)
						{
						case 0:
							num32 = 0;
							break;
						case 1:
							num32 = 1;
							break;
						case 2:
							num32 = 2;
							break;
						case 3:
							num32 = 1;
							break;
						case 4:
							num32 = 0;
							break;
						case 5:
							num32 = -1;
							break;
						case 6:
							num32 = -2;
							break;
						case 7:
							num32 = -1;
							break;
						}
					}
					else
					{
						switch (Main.weatherVaneBobframe)
						{
						case 0:
							num32 = 0;
							break;
						case 1:
							num32 = 1;
							break;
						case 2:
							num32 = 0;
							break;
						case 3:
							num32 = -1;
							break;
						case 4:
							num32 = 0;
							break;
						case 5:
							num32 = 1;
							break;
						case 6:
							num32 = 0;
							break;
						case 7:
							num32 = -1;
							break;
						}
					}
				}
				num31 += num32;
				if (num31 < 0)
				{
					num31 += 12;
				}
				num31 %= 12;
				addFrY = num31 * 36;
				break;
			}
			case 491:
				tileTop = 2;
				addFrX = 54;
				break;
			case 493:
				if (tileFrameY == 0)
				{
					int num33 = Main.tileFrameCounter[(int)typeCache];
					float num34 = Math.Abs(Main.WindForVisuals);
					int num35 = y - (int)(tileFrameY / 18);
					int num36 = x - (int)(tileFrameX / 18);
					if (!this.InAPlaceWithWind(x, num35, 1, 1))
					{
						num34 = 0f;
					}
					if (num34 >= 0.1f)
					{
						if (num34 < 0.5f)
						{
							num33 /= 20;
							num33 += num35 + num36;
							num33 %= 6;
							if (Main.WindForVisuals < 0f)
							{
								num33 = 6 - num33;
							}
							else
							{
								num33++;
							}
							addFrY = num33 * 36;
						}
						else
						{
							num33 /= 10;
							num33 += num35 + num36;
							num33 %= 6;
							if (Main.WindForVisuals < 0f)
							{
								num33 = 12 - num33;
							}
							else
							{
								num33 += 7;
							}
							addFrY = num33 * 36;
						}
					}
				}
				tileTop = 2;
				break;
			case 494:
				tileTop = 2;
				break;
			case 507:
			case 508:
			{
				int num37 = 20;
				int num38 = (Main.tileFrameCounter[(int)typeCache] + x * 11 + y * 27) % (num37 * 8);
				addFrY = 90 * (num38 / num37);
				break;
			}
			case 518:
			{
				int num39 = (int)(tileCache.liquid / 16);
				num39 -= 3;
				if (WorldGen.SolidTile(x, y - 1, false) && num39 > 8)
				{
					num39 = 8;
				}
				if (tileCache.liquid == 0)
				{
					Tile tileSafely = Framing.GetTileSafely(x, y + 1);
					if (tileSafely.nactive())
					{
						int num40 = tileSafely.blockType();
						if (num40 == 1)
						{
							num39 = -16 + Math.Max(8, (int)(tileSafely.liquid / 16));
						}
						else if (num40 == 3 || num40 == 2)
						{
							num39 -= 4;
						}
					}
				}
				tileTop -= num39;
				break;
			}
			case 519:
				tileTop = 2;
				if (x % 2 == 0)
				{
					tileSpriteEffect = SpriteEffects.FlipHorizontally;
				}
				break;
			case 521:
			case 522:
			case 523:
			case 524:
			case 525:
			case 526:
			case 527:
			{
				tileTop = 2;
				Main.critterCage = true;
				int waterAnimalCageFrame3 = this.GetWaterAnimalCageFrame(x, y, (int)tileFrameX, (int)tileFrameY);
				int num41 = (int)(typeCache - 521);
				addFrY = Main.dragonflyJarFrame[num41, waterAnimalCageFrame3] * 36;
				break;
			}
			case 529:
			{
				int num42 = y + 1;
				int num43;
				int num44;
				int num45;
				WorldGen.GetBiomeInfluence(x, x, num42, num42, out num43, out num44, out num45);
				int num46 = num43;
				if (num46 < num44)
				{
					num46 = num44;
				}
				if (num46 < num45)
				{
					num46 = num45;
				}
				int num47;
				if (num43 == 0 && num44 == 0 && num45 == 0)
				{
					if (x < WorldGen.beachDistance || x > Main.maxTilesX - WorldGen.beachDistance)
					{
						num47 = 1;
					}
					else
					{
						num47 = 0;
					}
				}
				else if (num45 == num46)
				{
					num47 = 2;
				}
				else if (num44 == num46)
				{
					num47 = 3;
				}
				else
				{
					num47 = 4;
				}
				addFrY += 34 * num47 - (int)tileFrameY;
				tileHeight = 32;
				tileTop = -14;
				if (x % 2 == 0)
				{
					tileSpriteEffect = SpriteEffects.FlipHorizontally;
				}
				break;
			}
			case 530:
			{
				int num48 = y - (int)(tileFrameY % 36 / 18) + 2;
				int num49 = x - (int)(tileFrameX % 54 / 18);
				int num50;
				int num51;
				int num52;
				WorldGen.GetBiomeInfluence(num49, num49 + 3, num48, num48, out num50, out num51, out num52);
				int num53 = num50;
				if (num53 < num51)
				{
					num53 = num51;
				}
				if (num53 < num52)
				{
					num53 = num52;
				}
				int num54;
				if (num50 == 0 && num51 == 0 && num52 == 0)
				{
					num54 = 0;
				}
				else if (num52 == num53)
				{
					num54 = 1;
				}
				else if (num51 == num53)
				{
					num54 = 2;
				}
				else
				{
					num54 = 3;
				}
				addFrY += 36 * num54;
				tileTop = 2;
				break;
			}
			case 541:
				addFrY = (this._shouldShowInvisibleBlocks ? 0 : 90);
				break;
			case 561:
				tileTop -= 2;
				tileHeight = 20;
				addFrY = (int)(tileFrameY / 18 * 4);
				break;
			case 565:
				tileTop = 2;
				if (tileFrameX < 36)
				{
					addFrY = Main.tileFrame[(int)typeCache] * 36;
				}
				else
				{
					addFrY = 0;
				}
				break;
			case 567:
				tileWidth = 26;
				tileHeight = 18;
				if (tileFrameY == 0)
				{
					tileTop = -2;
				}
				if (x % 2 == 0)
				{
					tileSpriteEffect = SpriteEffects.FlipHorizontally;
				}
				break;
			case 568:
			case 569:
			case 570:
			{
				tileTop = 2;
				Main.critterCage = true;
				int waterAnimalCageFrame4 = this.GetWaterAnimalCageFrame(x, y, (int)tileFrameX, (int)tileFrameY);
				addFrY = Main.fairyJarFrame[waterAnimalCageFrame4] * 36;
				break;
			}
			case 571:
				if (x % 2 == 0)
				{
					tileSpriteEffect = SpriteEffects.FlipHorizontally;
				}
				tileTop = 2;
				break;
			case 572:
			{
				int j;
				for (j = Main.tileFrame[(int)typeCache] + x % 4; j > 3; j -= 4)
				{
				}
				addFrX = j * 18;
				addFrY = 0;
				if (x % 2 == 0)
				{
					tileSpriteEffect = SpriteEffects.FlipHorizontally;
				}
				break;
			}
			case 579:
			{
				tileWidth = 20;
				tileHeight = 20;
				tileTop -= 2;
				bool flag5 = (float)(x * 16 + 8) > Main.LocalPlayer.Center.X;
				if (tileFrameX > 0)
				{
					if (flag5)
					{
						addFrY = 22;
					}
					else
					{
						addFrY = 0;
					}
				}
				else if (flag5)
				{
					addFrY = 0;
				}
				else
				{
					addFrY = 22;
				}
				break;
			}
			case 583:
			case 584:
			case 585:
			case 586:
			case 587:
			case 588:
			case 589:
			case 596:
			case 616:
			case 634:
				tileWidth = 20;
				tileHeight = 20;
				break;
			case 592:
				addFrY = Main.tileFrame[(int)typeCache] * 54;
				break;
			case 593:
			{
				if (tileFrameX >= 18)
				{
					addFrX = -18;
				}
				tileTop = 2;
				int num55;
				if (Animation.GetTemporaryFrame(x, y, out num55))
				{
					addFrY = (int)((short)(18 * num55));
				}
				else if (tileFrameX < 18)
				{
					addFrY = Main.tileFrame[(int)typeCache] * 18;
				}
				else
				{
					addFrY = 0;
				}
				break;
			}
			case 594:
			{
				if (tileFrameX >= 36)
				{
					addFrX = -36;
				}
				tileTop = 2;
				int num56 = (int)(tileFrameX % 36);
				int num57 = (int)(tileFrameY % 36);
				int num58;
				if (Animation.GetTemporaryFrame(x - num56 / 18, y - num57 / 18, out num58))
				{
					addFrY = (int)((short)(36 * num58));
				}
				else if (tileFrameX < 36)
				{
					addFrY = Main.tileFrame[(int)typeCache] * 36;
				}
				else
				{
					addFrY = 0;
				}
				break;
			}
			case 598:
			{
				tileTop = 2;
				Main.critterCage = true;
				int waterAnimalCageFrame5 = this.GetWaterAnimalCageFrame(x, y, (int)tileFrameX, (int)tileFrameY);
				addFrY = Main.lavaFishBowlFrame[waterAnimalCageFrame5] * 36;
				break;
			}
			case 614:
				addFrX = Main.tileFrame[(int)typeCache] * 54;
				addFrY = 0;
				tileTop = 2;
				break;
			case 615:
				tileHeight = 18;
				if (x % 2 == 0)
				{
					tileSpriteEffect = SpriteEffects.FlipHorizontally;
				}
				break;
			case 617:
				tileTop = 2;
				tileFrameY %= 144;
				tileFrameX %= 54;
				break;
			case 624:
				tileWidth = 20;
				tileHeight = 16;
				tileTop += 2;
				if (x % 2 == 0)
				{
					tileSpriteEffect = SpriteEffects.FlipHorizontally;
				}
				break;
			case 647:
				tileTop = 2;
				break;
			case 648:
			{
				tileTop = 2;
				int num59 = (int)(tileFrameX / 1890);
				addFrX -= 1890 * num59;
				addFrY += 36 * num59;
				break;
			}
			case 649:
			{
				tileTop = 2;
				int num60 = (int)(tileFrameX / 1908);
				addFrX -= 1908 * num60;
				addFrY += 18 * num60;
				break;
			}
			case 650:
				tileTop = 2;
				break;
			case 654:
				tileTop += 2;
				break;
			case 656:
				tileWidth = 24;
				tileHeight = 34;
				tileTop -= 16;
				if (x % 2 == 0)
				{
					tileSpriteEffect = SpriteEffects.FlipHorizontally;
				}
				break;
			case 658:
				tileTop = 2;
				switch (tileFrameY / 54)
				{
				default:
					addFrY = Main.tileFrame[(int)typeCache];
					addFrY *= 54;
					break;
				case 1:
					addFrY = Main.tileFrame[(int)typeCache];
					addFrY *= 54;
					addFrY += 486;
					break;
				case 2:
					addFrY = Main.tileFrame[(int)typeCache];
					addFrY *= 54;
					addFrY += 972;
					break;
				}
				break;
			case 660:
			{
				int k = Main.tileFrame[(int)typeCache] + x % 5;
				if (x % 2 == 0)
				{
					k += 3;
				}
				if (x % 3 == 0)
				{
					k += 3;
				}
				if (x % 4 == 0)
				{
					k += 3;
				}
				while (k > 4)
				{
					k -= 5;
				}
				addFrX = k * 18;
				addFrY = 0;
				if (x % 2 == 0)
				{
					tileSpriteEffect = SpriteEffects.FlipHorizontally;
				}
				break;
			}
			}
			IL_22BF:
			if (tileCache.halfBrick())
			{
				halfBrickHeight = 8;
			}
			if (typeCache <= 184)
			{
				if (typeCache <= 42)
				{
					if (typeCache <= 33)
					{
						switch (typeCache)
						{
						case 10:
							if (tileFrameY / 54 == 32)
							{
								glowTexture = TextureAssets.GlowMask[57].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 54), tileWidth, tileHeight);
								glowColor = this._martianGlow;
								return;
							}
							return;
						case 11:
						{
							short num61 = tileFrameY / 54;
							if (num61 == 32)
							{
								glowTexture = TextureAssets.GlowMask[58].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 54), tileWidth, tileHeight);
								glowColor = this._martianGlow;
							}
							if (num61 == 33)
							{
								glowTexture = TextureAssets.GlowMask[119].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 54), tileWidth, tileHeight);
								glowColor = this._meteorGlow;
								return;
							}
							return;
						}
						case 12:
						case 13:
						case 16:
						case 17:
						case 20:
							return;
						case 14:
						{
							short num62 = tileFrameX / 54;
							if (num62 == 31)
							{
								glowTexture = TextureAssets.GlowMask[67].Value;
								glowSourceRect = new Rectangle((int)(tileFrameX % 54), (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._martianGlow;
							}
							if (num62 == 32)
							{
								glowTexture = TextureAssets.GlowMask[124].Value;
								glowSourceRect = new Rectangle((int)(tileFrameX % 54), (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._meteorGlow;
								return;
							}
							return;
						}
						case 15:
						{
							short num63 = tileFrameY / 40;
							if (num63 == 32)
							{
								glowTexture = TextureAssets.GlowMask[54].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 40), tileWidth, tileHeight);
								glowColor = this._martianGlow;
							}
							if (num63 == 33)
							{
								glowTexture = TextureAssets.GlowMask[116].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 40), tileWidth, tileHeight);
								glowColor = this._meteorGlow;
								return;
							}
							return;
						}
						case 18:
						{
							short num64 = tileFrameX / 36;
							if (num64 == 27)
							{
								glowTexture = TextureAssets.GlowMask[69].Value;
								glowSourceRect = new Rectangle((int)(tileFrameX % 36), (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._martianGlow;
							}
							if (num64 == 28)
							{
								glowTexture = TextureAssets.GlowMask[125].Value;
								glowSourceRect = new Rectangle((int)(tileFrameX % 36), (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._meteorGlow;
								return;
							}
							return;
						}
						case 19:
						{
							short num65 = tileFrameY / 18;
							if (num65 == 26)
							{
								glowTexture = TextureAssets.GlowMask[65].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 18), tileWidth, tileHeight);
								glowColor = this._martianGlow;
							}
							if (num65 == 27)
							{
								glowTexture = TextureAssets.GlowMask[112].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 18), tileWidth, tileHeight);
								glowColor = this._meteorGlow;
								return;
							}
							return;
						}
						case 21:
							break;
						default:
							if (typeCache != 33)
							{
								return;
							}
							if (tileFrameX / 18 == 0 && tileFrameY / 22 == 26)
							{
								glowTexture = TextureAssets.GlowMask[61].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 22), tileWidth, tileHeight);
								glowColor = this._martianGlow;
								return;
							}
							return;
						}
					}
					else if (typeCache != 34)
					{
						if (typeCache != 42)
						{
							return;
						}
						if (tileFrameY / 36 == 33)
						{
							glowTexture = TextureAssets.GlowMask[63].Value;
							glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 36), tileWidth, tileHeight);
							glowColor = this._martianGlow;
							return;
						}
						return;
					}
					else
					{
						if (tileFrameX / 54 == 0 && tileFrameY / 54 == 33)
						{
							glowTexture = TextureAssets.GlowMask[55].Value;
							glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 54), tileWidth, tileHeight);
							glowColor = this._martianGlow;
							return;
						}
						return;
					}
				}
				else if (typeCache <= 93)
				{
					if (typeCache != 79)
					{
						switch (typeCache)
						{
						case 87:
						{
							short num66 = tileFrameX / 54;
							int num67 = (int)(tileFrameX / 1998);
							addFrX -= 1998 * num67;
							addFrY += 36 * num67;
							if (num66 == 26)
							{
								glowTexture = TextureAssets.GlowMask[64].Value;
								glowSourceRect = new Rectangle((int)(tileFrameX % 54), (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._martianGlow;
							}
							if (num66 == 27)
							{
								glowTexture = TextureAssets.GlowMask[121].Value;
								glowSourceRect = new Rectangle((int)(tileFrameX % 54), (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._meteorGlow;
								return;
							}
							return;
						}
						case 88:
						{
							short num68 = tileFrameX / 54;
							int num69 = (int)(tileFrameX / 1998);
							addFrX -= 1998 * num69;
							addFrY += 36 * num69;
							if (num68 == 24)
							{
								glowTexture = TextureAssets.GlowMask[59].Value;
								glowSourceRect = new Rectangle((int)(tileFrameX % 54), (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._martianGlow;
							}
							if (num68 == 25)
							{
								glowTexture = TextureAssets.GlowMask[120].Value;
								glowSourceRect = new Rectangle((int)(tileFrameX % 54), (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._meteorGlow;
								return;
							}
							return;
						}
						case 89:
						{
							short num70 = tileFrameX / 54;
							int num71 = (int)(tileFrameX / 1998);
							addFrX -= 1998 * num71;
							addFrY += 36 * num71;
							if (num70 == 29)
							{
								glowTexture = TextureAssets.GlowMask[66].Value;
								glowSourceRect = new Rectangle((int)(tileFrameX % 54), (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._martianGlow;
							}
							if (num70 == 30)
							{
								glowTexture = TextureAssets.GlowMask[123].Value;
								glowSourceRect = new Rectangle((int)(tileFrameX % 54), (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._meteorGlow;
								return;
							}
							return;
						}
						case 90:
						{
							short num72 = tileFrameY / 36;
							if (num72 == 27)
							{
								glowTexture = TextureAssets.GlowMask[52].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 36), tileWidth, tileHeight);
								glowColor = this._martianGlow;
							}
							if (num72 == 28)
							{
								glowTexture = TextureAssets.GlowMask[113].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 36), tileWidth, tileHeight);
								glowColor = this._meteorGlow;
								return;
							}
							return;
						}
						case 91:
						case 92:
							return;
						case 93:
						{
							int num73 = (int)(tileFrameY / 54);
							int num74 = (int)(tileFrameY / 1998);
							addFrY -= 1998 * num74;
							addFrX += 36 * num74;
							tileTop += 2;
							if (num73 == 27)
							{
								glowTexture = TextureAssets.GlowMask[62].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 54), tileWidth, tileHeight);
								glowColor = this._martianGlow;
								return;
							}
							return;
						}
						default:
							return;
						}
					}
					else
					{
						short num75 = tileFrameY / 36;
						if (num75 == 27)
						{
							glowTexture = TextureAssets.GlowMask[53].Value;
							glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 36), tileWidth, tileHeight);
							glowColor = this._martianGlow;
						}
						if (num75 == 28)
						{
							glowTexture = TextureAssets.GlowMask[114].Value;
							glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 36), tileWidth, tileHeight);
							glowColor = this._meteorGlow;
							return;
						}
						return;
					}
				}
				else
				{
					switch (typeCache)
					{
					case 100:
						if (tileFrameX / 36 == 0 && tileFrameY / 36 == 27)
						{
							glowTexture = TextureAssets.GlowMask[68].Value;
							glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 36), tileWidth, tileHeight);
							glowColor = this._martianGlow;
							return;
						}
						return;
					case 101:
					{
						short num76 = tileFrameX / 54;
						int num77 = (int)(tileFrameX / 1998);
						addFrX -= 1998 * num77;
						addFrY += 72 * num77;
						if (num76 == 28)
						{
							glowTexture = TextureAssets.GlowMask[60].Value;
							glowSourceRect = new Rectangle((int)(tileFrameX % 54), (int)tileFrameY, tileWidth, tileHeight);
							glowColor = this._martianGlow;
						}
						if (num76 == 29)
						{
							glowTexture = TextureAssets.GlowMask[115].Value;
							glowSourceRect = new Rectangle((int)(tileFrameX % 54), (int)tileFrameY, tileWidth, tileHeight);
							glowColor = this._meteorGlow;
							return;
						}
						return;
					}
					case 102:
					case 103:
						return;
					case 104:
					{
						short num78 = tileFrameX / 36;
						tileTop = 2;
						if (num78 == 24)
						{
							glowTexture = TextureAssets.GlowMask[51].Value;
							glowSourceRect = new Rectangle((int)(tileFrameX % 36), (int)tileFrameY, tileWidth, tileHeight);
							glowColor = this._martianGlow;
						}
						if (num78 == 25)
						{
							glowTexture = TextureAssets.GlowMask[118].Value;
							glowSourceRect = new Rectangle((int)(tileFrameX % 36), (int)tileFrameY, tileWidth, tileHeight);
							glowColor = this._meteorGlow;
							return;
						}
						return;
					}
					default:
						if (typeCache != 172)
						{
							if (typeCache != 184)
							{
								return;
							}
							if (tileCache.frameX == 110)
							{
								glowTexture = TextureAssets.GlowMask[127].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._lavaMossGlow;
							}
							if (tileCache.frameX == 132)
							{
								glowTexture = TextureAssets.GlowMask[127].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._kryptonMossGlow;
							}
							if (tileCache.frameX == 154)
							{
								glowTexture = TextureAssets.GlowMask[127].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._xenonMossGlow;
							}
							if (tileCache.frameX == 176)
							{
								glowTexture = TextureAssets.GlowMask[127].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._argonMossGlow;
							}
							if (tileCache.frameX == 198)
							{
								glowTexture = TextureAssets.GlowMask[127].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._violetMossGlow;
							}
							if (tileCache.frameX == 220)
							{
								glowTexture = TextureAssets.GlowMask[127].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY, tileWidth, tileHeight);
								glowColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
								return;
							}
							return;
						}
						else
						{
							short num79 = tileFrameY / 38;
							if (num79 == 28)
							{
								glowTexture = TextureAssets.GlowMask[88].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 38), tileWidth, tileHeight);
								glowColor = this._martianGlow;
							}
							if (num79 == 29)
							{
								glowTexture = TextureAssets.GlowMask[122].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 38), tileWidth, tileHeight);
								glowColor = this._meteorGlow;
								return;
							}
							return;
						}
						break;
					}
				}
			}
			else if (typeCache <= 468)
			{
				if (typeCache <= 463)
				{
					if (typeCache != 441)
					{
						if (typeCache != 463)
						{
							return;
						}
						glowTexture = TextureAssets.GlowMask[243].Value;
						glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY + addFrY, tileWidth, tileHeight);
						glowColor = new Color(127, 127, 127, 0);
						return;
					}
				}
				else
				{
					if (typeCache == 467)
					{
						goto IL_2CC4;
					}
					if (typeCache != 468)
					{
						return;
					}
				}
				short num80 = tileFrameX / 36;
				if (num80 == 48)
				{
					glowTexture = TextureAssets.GlowMask[56].Value;
					glowSourceRect = new Rectangle((int)(tileFrameX % 36), (int)tileFrameY, tileWidth, tileHeight);
					glowColor = this._martianGlow;
				}
				if (num80 == 49)
				{
					glowTexture = TextureAssets.GlowMask[117].Value;
					glowSourceRect = new Rectangle((int)(tileFrameX % 36), (int)tileFrameY, tileWidth, tileHeight);
					glowColor = this._meteorGlow;
					return;
				}
				return;
			}
			else if (typeCache <= 580)
			{
				switch (typeCache)
				{
				case 564:
					if (tileCache.frameX < 36)
					{
						glowTexture = TextureAssets.GlowMask[267].Value;
						glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY + addFrY, tileWidth, tileHeight);
						glowColor = new Color(200, 200, 200, 0) * ((float)Main.mouseTextColor / 255f);
					}
					addFrY = 0;
					return;
				case 565:
				case 566:
				case 567:
					return;
				case 568:
					glowTexture = TextureAssets.GlowMask[268].Value;
					glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY + addFrY, tileWidth, tileHeight);
					glowColor = Color.White;
					return;
				case 569:
					glowTexture = TextureAssets.GlowMask[269].Value;
					glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY + addFrY, tileWidth, tileHeight);
					glowColor = Color.White;
					return;
				case 570:
					glowTexture = TextureAssets.GlowMask[270].Value;
					glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY + addFrY, tileWidth, tileHeight);
					glowColor = Color.White;
					return;
				default:
					if (typeCache != 580)
					{
						return;
					}
					glowTexture = TextureAssets.GlowMask[289].Value;
					glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY + addFrY, tileWidth, tileHeight);
					glowColor = new Color(225, 110, 110, 0);
					return;
				}
			}
			else
			{
				switch (typeCache)
				{
				case 634:
					glowTexture = TextureAssets.GlowMask[315].Value;
					glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY + addFrY, tileWidth, tileHeight);
					glowColor = Color.White;
					return;
				case 635:
				case 636:
					return;
				case 637:
					glowTexture = TextureAssets.Tile[637].Value;
					glowSourceRect = new Rectangle((int)tileFrameX + addFrX, (int)tileFrameY + addFrY, tileWidth, tileHeight);
					glowColor = Color.Lerp(Color.White, color, 0.75f);
					return;
				case 638:
					glowTexture = TextureAssets.GlowMask[327].Value;
					glowSourceRect = new Rectangle((int)tileFrameX + addFrX, (int)tileFrameY + addFrY, tileWidth, tileHeight - halfBrickHeight);
					glowColor = Color.Lerp(Color.White, color, 0.75f);
					return;
				default:
					if (typeCache == 656)
					{
						glowTexture = TextureAssets.GlowMask[329].Value;
						glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY + addFrY, tileWidth, tileHeight);
						glowColor = new Color(255, 255, 255, 0) * ((float)Main.mouseTextColor / 255f);
						return;
					}
					if (typeCache == 657 && tileFrameY >= 54)
					{
						glowTexture = TextureAssets.GlowMask[330].Value;
						glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY + addFrY, tileWidth, tileHeight);
						glowColor = Color.White;
						return;
					}
					return;
				}
			}
			IL_2CC4:
			short num81 = tileFrameX / 36;
			if (num81 == 48)
			{
				glowTexture = TextureAssets.GlowMask[56].Value;
				glowSourceRect = new Rectangle((int)(tileFrameX % 36), (int)tileFrameY, tileWidth, tileHeight);
				glowColor = this._martianGlow;
			}
			if (num81 == 49)
			{
				glowTexture = TextureAssets.GlowMask[117].Value;
				glowSourceRect = new Rectangle((int)(tileFrameX % 36), (int)tileFrameY, tileWidth, tileHeight);
				glowColor = this._meteorGlow;
				return;
			}
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x00539548 File Offset: 0x00537748
		private bool IsWindBlocked(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return tile == null || (tile.wall > 0 && !WallID.Sets.AllowsWind[(int)tile.wall]) || (double)y > Main.worldSurface;
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x0053958C File Offset: 0x0053778C
		private int GetWaterAnimalCageFrame(int x, int y, int tileFrameX, int tileFrameY)
		{
			int num = x - tileFrameX / 18;
			int num2 = y - tileFrameY / 18;
			return num / 2 * (num2 / 3) % Main.cageFrames;
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x005395B4 File Offset: 0x005377B4
		private int GetSmallAnimalCageFrame(int x, int y, int tileFrameX, int tileFrameY)
		{
			int num = x - tileFrameX / 18;
			int num2 = y - tileFrameY / 18;
			return num / 3 * (num2 / 3) % Main.cageFrames;
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x005395DC File Offset: 0x005377DC
		private int GetBigAnimalCageFrame(int x, int y, int tileFrameX, int tileFrameY)
		{
			int num = x - tileFrameX / 18;
			int num2 = y - tileFrameY / 18;
			return num / 6 * (num2 / 4) % Main.cageFrames;
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x00539604 File Offset: 0x00537804
		private void GetScreenDrawArea(Vector2 screenPosition, Vector2 offSet, out int firstTileX, out int lastTileX, out int firstTileY, out int lastTileY)
		{
			firstTileX = (int)((screenPosition.X - offSet.X) / 16f - 1f);
			lastTileX = (int)((screenPosition.X + (float)Main.screenWidth + offSet.X) / 16f) + 2;
			firstTileY = (int)((screenPosition.Y - offSet.Y) / 16f - 1f);
			lastTileY = (int)((screenPosition.Y + (float)Main.screenHeight + offSet.Y) / 16f) + 5;
			if (firstTileX < 4)
			{
				firstTileX = 4;
			}
			if (lastTileX > Main.maxTilesX - 4)
			{
				lastTileX = Main.maxTilesX - 4;
			}
			if (firstTileY < 4)
			{
				firstTileY = 4;
			}
			if (lastTileY > Main.maxTilesY - 4)
			{
				lastTileY = Main.maxTilesY - 4;
			}
			if (Main.sectionManager.AnyUnfinishedSections)
			{
				TimeLogger.DetailedDrawReset();
				WorldGen.SectionTileFrameWithCheck(firstTileX, firstTileY, lastTileX, lastTileY);
				TimeLogger.DetailedDrawTime(5);
			}
			if (Main.sectionManager.AnyNeedRefresh)
			{
				WorldGen.RefreshSections(firstTileX, firstTileY, lastTileX, lastTileY);
			}
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x0053970B File Offset: 0x0053790B
		public void ClearCachedTileDraws(bool solidLayer)
		{
			if (solidLayer)
			{
				this._displayDollTileEntityPositions.Clear();
				this._hatRackTileEntityPositions.Clear();
				this._vineRootsPositions.Clear();
				this._reverseVineRootsPositions.Clear();
			}
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x0053973C File Offset: 0x0053793C
		private void AddSpecialLegacyPoint(Point p)
		{
			this.AddSpecialLegacyPoint(p.X, p.Y);
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x00539750 File Offset: 0x00537950
		private void AddSpecialLegacyPoint(int x, int y)
		{
			this._specialTileX[this._specialTilesCount] = x;
			this._specialTileY[this._specialTilesCount] = y;
			this._specialTilesCount++;
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x0053977C File Offset: 0x0053797C
		private void ClearLegacyCachedDraws()
		{
			this._chestPositions.Clear();
			this._trainingDummyTileEntityPositions.Clear();
			this._foodPlatterTileEntityPositions.Clear();
			this._itemFrameTileEntityPositions.Clear();
			this._weaponRackTileEntityPositions.Clear();
			this._specialTilesCount = 0;
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x005397BC File Offset: 0x005379BC
		private Color DrawTiles_GetLightOverride(int j, int i, Tile tileCache, ushort typeCache, short tileFrameX, short tileFrameY, Color tileLight)
		{
			if (tileCache.fullbrightBlock())
			{
				return Color.White;
			}
			if (typeCache <= 61)
			{
				if (typeCache != 19)
				{
					if (typeCache == 61)
					{
						if (tileFrameX == 144)
						{
							tileLight.A = (tileLight.R = (tileLight.G = (tileLight.B = (byte)(245f - (float)Main.mouseTextColor * 1.5f))));
						}
					}
				}
				else if (tileFrameY / 18 == 48)
				{
					return Color.White;
				}
			}
			else if (typeCache != 83)
			{
				if (typeCache == 541 || typeCache == 631)
				{
					return Color.White;
				}
			}
			else
			{
				int num = (int)(tileFrameX / 18);
				if (this.IsAlchemyPlantHarvestable(num))
				{
					if (num == 5)
					{
						tileLight.A = Main.mouseTextColor / 2;
						tileLight.G = Main.mouseTextColor;
						tileLight.B = Main.mouseTextColor;
					}
					if (num == 6)
					{
						byte b = (Main.mouseTextColor + tileLight.G * 2) / 3;
						byte b2 = (Main.mouseTextColor + tileLight.B * 2) / 3;
						if (b > tileLight.G)
						{
							tileLight.G = b;
						}
						if (b2 > tileLight.B)
						{
							tileLight.B = b2;
						}
					}
				}
			}
			return tileLight;
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x005398FC File Offset: 0x00537AFC
		private void DrawTiles_EmitParticles(int j, int i, Tile tileCache, ushort typeCache, short tileFrameX, short tileFrameY, Color tileLight)
		{
			bool flag = this.IsVisible(tileCache);
			int num = this._leafFrequency;
			num /= 4;
			if (typeCache == 244 && tileFrameX == 18 && tileFrameY == 18 && this._rand.Next(2) == 0)
			{
				if (this._rand.Next(500) == 0)
				{
					Gore.NewGore(new Vector2((float)(i * 16 + 8), (float)(j * 16 + 8)), default(Vector2), 415, (float)this._rand.Next(51, 101) * 0.01f);
				}
				else if (this._rand.Next(250) == 0)
				{
					Gore.NewGore(new Vector2((float)(i * 16 + 8), (float)(j * 16 + 8)), default(Vector2), 414, (float)this._rand.Next(51, 101) * 0.01f);
				}
				else if (this._rand.Next(80) == 0)
				{
					Gore.NewGore(new Vector2((float)(i * 16 + 8), (float)(j * 16 + 8)), default(Vector2), 413, (float)this._rand.Next(51, 101) * 0.01f);
				}
				else if (this._rand.Next(10) == 0)
				{
					Gore.NewGore(new Vector2((float)(i * 16 + 8), (float)(j * 16 + 8)), default(Vector2), 412, (float)this._rand.Next(51, 101) * 0.01f);
				}
				else if (this._rand.Next(3) == 0)
				{
					Gore.NewGore(new Vector2((float)(i * 16 + 8), (float)(j * 16 + 8)), default(Vector2), 411, (float)this._rand.Next(51, 101) * 0.01f);
				}
			}
			if (typeCache == 565 && tileFrameX == 0 && tileFrameY == 18 && this._rand.Next(3) == 0 && ((Main.drawToScreen && this._rand.Next(4) == 0) || !Main.drawToScreen))
			{
				Vector2 value = new Point(i, j).ToWorldCoordinates(8f, 8f);
				int type = 1202;
				float scale = 8f + Main.rand.NextFloat() * 1.6f;
				Vector2 position = value + new Vector2(0f, -18f);
				Vector2 vector = Main.rand.NextVector2Circular(0.7f, 0.25f) * 0.4f + Main.rand.NextVector2CircularEdge(1f, 0.4f) * 0.1f;
				vector *= 4f;
				Gore.NewGorePerfect(position, vector, type, scale);
			}
			if (typeCache == 215 && tileFrameY < 36 && this._rand.Next(3) == 0 && ((Main.drawToScreen && this._rand.Next(4) == 0) || !Main.drawToScreen) && tileFrameY == 0)
			{
				int num2 = Dust.NewDust(new Vector2((float)(i * 16 + 2), (float)(j * 16 - 4)), 4, 8, 31, 0f, 0f, 100, default(Color), 1f);
				if (tileFrameX == 0)
				{
					Dust dust = this._dust[num2];
					dust.position.X = dust.position.X + (float)this._rand.Next(8);
				}
				if (tileFrameX == 36)
				{
					Dust dust2 = this._dust[num2];
					dust2.position.X = dust2.position.X - (float)this._rand.Next(8);
				}
				this._dust[num2].alpha += this._rand.Next(100);
				this._dust[num2].velocity *= 0.2f;
				Dust dust3 = this._dust[num2];
				dust3.velocity.Y = dust3.velocity.Y - (0.5f + (float)this._rand.Next(10) * 0.1f);
				this._dust[num2].fadeIn = 0.5f + (float)this._rand.Next(10) * 0.1f;
			}
			if (typeCache == 592 && tileFrameY == 18 && this._rand.Next(3) == 0)
			{
				if ((Main.drawToScreen && this._rand.Next(6) == 0) || !Main.drawToScreen)
				{
					int num3 = Dust.NewDust(new Vector2((float)(i * 16 + 2), (float)(j * 16 + 4)), 4, 8, 31, 0f, 0f, 100, default(Color), 1f);
					if (tileFrameX == 0)
					{
						Dust dust4 = this._dust[num3];
						dust4.position.X = dust4.position.X + (float)this._rand.Next(8);
					}
					if (tileFrameX == 36)
					{
						Dust dust5 = this._dust[num3];
						dust5.position.X = dust5.position.X - (float)this._rand.Next(8);
					}
					this._dust[num3].alpha += this._rand.Next(100);
					this._dust[num3].velocity *= 0.2f;
					Dust dust6 = this._dust[num3];
					dust6.velocity.Y = dust6.velocity.Y - (0.5f + (float)this._rand.Next(10) * 0.1f);
					this._dust[num3].fadeIn = 0.5f + (float)this._rand.Next(10) * 0.1f;
				}
			}
			else if (typeCache == 406 && tileFrameY == 54 && tileFrameX == 0 && this._rand.Next(3) == 0)
			{
				Vector2 position2 = new Vector2((float)(i * 16 + 16), (float)(j * 16 + 8));
				Vector2 velocity = new Vector2(0f, 0f);
				if (Main.WindForVisuals < 0f)
				{
					velocity.X = -Main.WindForVisuals;
				}
				int type2 = this._rand.Next(825, 828);
				if (this._rand.Next(4) == 0)
				{
					Gore.NewGore(position2, velocity, type2, this._rand.NextFloat() * 0.2f + 0.2f);
				}
				else if (this._rand.Next(2) == 0)
				{
					Gore.NewGore(position2, velocity, type2, this._rand.NextFloat() * 0.3f + 0.3f);
				}
				else
				{
					Gore.NewGore(position2, velocity, type2, this._rand.NextFloat() * 0.4f + 0.4f);
				}
			}
			else if (typeCache == 452 && tileFrameY == 0 && tileFrameX == 0 && this._rand.Next(3) == 0)
			{
				Vector2 position3 = new Vector2((float)(i * 16 + 16), (float)(j * 16 + 8));
				Vector2 velocity2 = new Vector2(0f, 0f);
				if (Main.WindForVisuals < 0f)
				{
					velocity2.X = -Main.WindForVisuals;
				}
				int num4 = Main.tileFrame[(int)typeCache];
				int type3 = 907 + num4 / 5;
				if (this._rand.Next(2) == 0)
				{
					Gore.NewGore(position3, velocity2, type3, this._rand.NextFloat() * 0.4f + 0.4f);
				}
			}
			if (typeCache == 192 && this._rand.Next(num) == 0)
			{
				this.EmitLivingTreeLeaf(i, j, 910);
			}
			if (typeCache == 384 && this._rand.Next(num) == 0)
			{
				this.EmitLivingTreeLeaf(i, j, 914);
			}
			if (typeCache == 666 && this._rand.Next(100) == 0 && j - 1 > 0 && !WorldGen.ActiveAndWalkableTile(i, j - 1))
			{
				ParticleOrchestrator.RequestParticleSpawn(true, ParticleOrchestraType.PooFly, new ParticleOrchestraSettings
				{
					PositionInWorld = new Vector2((float)(i * 16 + 8), (float)(j * 16 - 8))
				}, null);
			}
			if (!flag)
			{
				return;
			}
			if (typeCache == 238 && this._rand.Next(10) == 0)
			{
				int num5 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 168, 0f, 0f, 0, default(Color), 1f);
				this._dust[num5].noGravity = true;
				this._dust[num5].alpha = 200;
			}
			if (typeCache == 139)
			{
				if (tileCache.frameX == 36 && tileCache.frameY % 36 == 0 && (int)Main.timeForVisualEffects % 7 == 0 && this._rand.Next(3) == 0)
				{
					int num6 = this._rand.Next(570, 573);
					Vector2 position4 = new Vector2((float)(i * 16 + 8), (float)(j * 16 - 8));
					Vector2 velocity3 = new Vector2(Main.WindForVisuals * 2f, -0.5f);
					velocity3.X *= 1f + (float)this._rand.Next(-50, 51) * 0.01f;
					velocity3.Y *= 1f + (float)this._rand.Next(-50, 51) * 0.01f;
					if (num6 == 572)
					{
						position4.X -= 8f;
					}
					if (num6 == 571)
					{
						position4.X -= 4f;
					}
					Gore.NewGore(position4, velocity3, num6, 0.8f);
				}
			}
			else if (typeCache == 463)
			{
				if (tileFrameY == 54 && tileFrameX == 0)
				{
					for (int k = 0; k < 4; k++)
					{
						if (this._rand.Next(2) != 0)
						{
							Dust dust7 = Dust.NewDustDirect(new Vector2((float)(i * 16 + 4), (float)(j * 16)), 36, 8, 16, 0f, 0f, 0, default(Color), 1f);
							dust7.noGravity = true;
							dust7.alpha = 140;
							dust7.fadeIn = 1.2f;
							dust7.velocity = Vector2.Zero;
						}
					}
				}
				if (tileFrameY == 18 && (tileFrameX == 0 || tileFrameX == 36))
				{
					for (int l = 0; l < 1; l++)
					{
						if (this._rand.Next(13) == 0)
						{
							Dust dust8 = Dust.NewDustDirect(new Vector2((float)(i * 16), (float)(j * 16)), 8, 8, 274, 0f, 0f, 0, default(Color), 1f);
							dust8.position = new Vector2((float)(i * 16 + 8), (float)(j * 16 + 8));
							dust8.position.X = dust8.position.X + (float)((tileFrameX == 36) ? 4 : -4);
							dust8.noGravity = true;
							dust8.alpha = 128;
							dust8.fadeIn = 1.2f;
							dust8.noLight = true;
							dust8.velocity = new Vector2(0f, this._rand.NextFloatDirection() * 1.2f);
						}
					}
				}
			}
			else if (typeCache == 497)
			{
				if (tileCache.frameY / 40 == 31 && tileCache.frameY % 40 == 0)
				{
					for (int m = 0; m < 1; m++)
					{
						if (this._rand.Next(10) == 0)
						{
							Dust dust9 = Dust.NewDustDirect(new Vector2((float)(i * 16), (float)(j * 16 + 8)), 16, 12, 43, 0f, 0f, 0, default(Color), 1f);
							dust9.noGravity = true;
							dust9.alpha = 254;
							dust9.color = Color.White;
							dust9.scale = 0.7f;
							dust9.velocity = Vector2.Zero;
							dust9.noLight = true;
						}
					}
				}
			}
			else if (typeCache == 165 && tileFrameX >= 162 && tileFrameX <= 214 && tileFrameY == 72)
			{
				if (this._rand.Next(60) == 0)
				{
					int num7 = Dust.NewDust(new Vector2((float)(i * 16 + 2), (float)(j * 16 + 6)), 8, 4, 153, 0f, 0f, 0, default(Color), 1f);
					this._dust[num7].scale -= (float)this._rand.Next(3) * 0.1f;
					this._dust[num7].velocity.Y = 0f;
					Dust dust10 = this._dust[num7];
					dust10.velocity.X = dust10.velocity.X * 0.05f;
					this._dust[num7].alpha = 100;
				}
			}
			else if (typeCache == 42 && tileFrameX == 0)
			{
				int num8 = (int)(tileFrameY / 36);
				int num9 = (int)(tileFrameY / 18 % 2);
				if (num8 == 7 && num9 == 1)
				{
					if (this._rand.Next(50) == 0)
					{
						int num10 = Dust.NewDust(new Vector2((float)(i * 16 + 4), (float)(j * 16 + 4)), 8, 8, 58, 0f, 0f, 150, default(Color), 1f);
						this._dust[num10].velocity *= 0.5f;
					}
					if (this._rand.Next(100) == 0)
					{
						int num11 = Gore.NewGore(new Vector2((float)(i * 16 - 2), (float)(j * 16 - 4)), default(Vector2), this._rand.Next(16, 18), 1f);
						this._gore[num11].scale *= 0.7f;
						this._gore[num11].velocity *= 0.25f;
					}
				}
				else if (num8 == 29 && num9 == 1 && this._rand.Next(40) == 0)
				{
					int num12 = Dust.NewDust(new Vector2((float)(i * 16 + 4), (float)(j * 16)), 8, 8, 59, 0f, 0f, 100, default(Color), 1f);
					if (this._rand.Next(3) != 0)
					{
						this._dust[num12].noGravity = true;
					}
					this._dust[num12].velocity *= 0.3f;
					Dust dust11 = this._dust[num12];
					dust11.velocity.Y = dust11.velocity.Y - 1.5f;
				}
			}
			if (typeCache == 4 && this._rand.Next(40) == 0 && tileFrameX < 66)
			{
				int num13 = (int)MathHelper.Clamp((float)(tileCache.frameY / 22), 0f, (float)(TorchID.Count - 1));
				int num14 = TorchID.Dust[num13];
				int num15;
				if (tileFrameX == 22)
				{
					num15 = Dust.NewDust(new Vector2((float)(i * 16 + 6), (float)(j * 16)), 4, 4, num14, 0f, 0f, 100, default(Color), 1f);
				}
				else if (tileFrameX == 44)
				{
					num15 = Dust.NewDust(new Vector2((float)(i * 16 + 2), (float)(j * 16)), 4, 4, num14, 0f, 0f, 100, default(Color), 1f);
				}
				else
				{
					num15 = Dust.NewDust(new Vector2((float)(i * 16 + 4), (float)(j * 16)), 4, 4, num14, 0f, 0f, 100, default(Color), 1f);
				}
				if (this._rand.Next(3) != 0)
				{
					this._dust[num15].noGravity = true;
				}
				this._dust[num15].velocity *= 0.3f;
				Dust dust12 = this._dust[num15];
				dust12.velocity.Y = dust12.velocity.Y - 1.5f;
				if (num14 == 66)
				{
					this._dust[num15].color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
					this._dust[num15].noGravity = true;
				}
			}
			if (typeCache == 93 && this._rand.Next(40) == 0 && tileFrameX == 0)
			{
				int num16 = (int)(tileFrameY / 54);
				if (tileFrameY / 18 % 3 == 0)
				{
					int num17;
					if (num16 != 0)
					{
						switch (num16)
						{
						case 6:
						case 7:
						case 8:
						case 10:
						case 14:
						case 15:
						case 16:
							goto IL_10B4;
						case 20:
							num17 = 59;
							goto IL_10C2;
						}
						num17 = -1;
						goto IL_10C2;
					}
					IL_10B4:
					num17 = 6;
					IL_10C2:
					if (num17 != -1)
					{
						int num18 = Dust.NewDust(new Vector2((float)(i * 16 + 4), (float)(j * 16 + 2)), 4, 4, num17, 0f, 0f, 100, default(Color), 1f);
						if (this._rand.Next(3) != 0)
						{
							this._dust[num18].noGravity = true;
						}
						this._dust[num18].velocity *= 0.3f;
						Dust dust13 = this._dust[num18];
						dust13.velocity.Y = dust13.velocity.Y - 1.5f;
					}
				}
			}
			if (typeCache == 100 && this._rand.Next(40) == 0 && tileFrameX < 36)
			{
				int num19 = (int)(tileFrameY / 36);
				if (tileFrameY / 18 % 2 == 0)
				{
					int num20;
					switch (num19)
					{
					case 0:
					case 5:
					case 7:
					case 8:
					case 10:
					case 12:
					case 14:
					case 15:
					case 16:
						num20 = 6;
						goto IL_11F2;
					case 1:
					case 2:
					case 3:
					case 4:
					case 6:
					case 9:
					case 11:
					case 13:
						break;
					default:
						if (num19 == 20)
						{
							num20 = 59;
							goto IL_11F2;
						}
						break;
					}
					num20 = -1;
					IL_11F2:
					if (num20 != -1)
					{
						Vector2 position5;
						if (tileFrameX == 0)
						{
							if (this._rand.Next(3) == 0)
							{
								position5 = new Vector2((float)(i * 16 + 4), (float)(j * 16 + 2));
							}
							else
							{
								position5 = new Vector2((float)(i * 16 + 14), (float)(j * 16 + 2));
							}
						}
						else if (this._rand.Next(3) == 0)
						{
							position5 = new Vector2((float)(i * 16 + 6), (float)(j * 16 + 2));
						}
						else
						{
							position5 = new Vector2((float)(i * 16), (float)(j * 16 + 2));
						}
						int num21 = Dust.NewDust(position5, 4, 4, num20, 0f, 0f, 100, default(Color), 1f);
						if (this._rand.Next(3) != 0)
						{
							this._dust[num21].noGravity = true;
						}
						this._dust[num21].velocity *= 0.3f;
						Dust dust14 = this._dust[num21];
						dust14.velocity.Y = dust14.velocity.Y - 1.5f;
					}
				}
			}
			if (typeCache == 98 && this._rand.Next(40) == 0 && tileFrameY == 0 && tileFrameX == 0)
			{
				int num22 = Dust.NewDust(new Vector2((float)(i * 16 + 12), (float)(j * 16 + 2)), 4, 4, 6, 0f, 0f, 100, default(Color), 1f);
				if (this._rand.Next(3) != 0)
				{
					this._dust[num22].noGravity = true;
				}
				this._dust[num22].velocity *= 0.3f;
				Dust dust15 = this._dust[num22];
				dust15.velocity.Y = dust15.velocity.Y - 1.5f;
			}
			if (typeCache == 49 && tileFrameX == 0 && this._rand.Next(2) == 0)
			{
				int num23 = Dust.NewDust(new Vector2((float)(i * 16 + 4), (float)(j * 16 - 4)), 4, 4, 172, 0f, 0f, 100, default(Color), 1f);
				if (this._rand.Next(3) == 0)
				{
					this._dust[num23].scale = 0.5f;
				}
				else
				{
					this._dust[num23].scale = 0.9f;
					this._dust[num23].noGravity = true;
				}
				this._dust[num23].velocity *= 0.3f;
				Dust dust16 = this._dust[num23];
				dust16.velocity.Y = dust16.velocity.Y - 1.5f;
			}
			if (typeCache == 372 && tileFrameX == 0 && this._rand.Next(2) == 0)
			{
				int num24 = Dust.NewDust(new Vector2((float)(i * 16 + 4), (float)(j * 16 - 4)), 4, 4, 242, 0f, 0f, 100, default(Color), 1f);
				if (this._rand.Next(3) == 0)
				{
					this._dust[num24].scale = 0.5f;
				}
				else
				{
					this._dust[num24].scale = 0.9f;
					this._dust[num24].noGravity = true;
				}
				this._dust[num24].velocity *= 0.3f;
				Dust dust17 = this._dust[num24];
				dust17.velocity.Y = dust17.velocity.Y - 1.5f;
			}
			if (typeCache == 646 && tileFrameX == 0)
			{
				this._rand.Next(2);
			}
			if (typeCache == 34 && this._rand.Next(40) == 0 && tileFrameX < 54)
			{
				int num25 = (int)(tileFrameY / 54);
				int num26 = (int)(tileFrameX / 18 % 3);
				if (tileFrameY / 18 % 3 == 1 && num26 != 1)
				{
					int num27;
					if (num25 <= 19)
					{
						switch (num25)
						{
						case 0:
						case 1:
						case 2:
						case 3:
						case 4:
						case 5:
						case 12:
						case 13:
						case 16:
							break;
						case 6:
						case 7:
						case 8:
						case 9:
						case 10:
						case 11:
						case 14:
						case 15:
							goto IL_163E;
						default:
							if (num25 != 19)
							{
								goto IL_163E;
							}
							break;
						}
					}
					else if (num25 != 21)
					{
						if (num25 != 25)
						{
							goto IL_163E;
						}
						num27 = 59;
						goto IL_1641;
					}
					num27 = 6;
					goto IL_1641;
					IL_163E:
					num27 = -1;
					IL_1641:
					if (num27 != -1)
					{
						int num28 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16 + 2)), 14, 6, num27, 0f, 0f, 100, default(Color), 1f);
						if (this._rand.Next(3) != 0)
						{
							this._dust[num28].noGravity = true;
						}
						this._dust[num28].velocity *= 0.3f;
						Dust dust18 = this._dust[num28];
						dust18.velocity.Y = dust18.velocity.Y - 1.5f;
					}
				}
			}
			if (typeCache == 83)
			{
				int style = (int)(tileFrameX / 18);
				if (this.IsAlchemyPlantHarvestable(style))
				{
					this.EmitAlchemyHerbParticles(j, i, style);
				}
			}
			if (typeCache == 22 && this._rand.Next(400) == 0)
			{
				Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 14, 0f, 0f, 0, default(Color), 1f);
				return;
			}
			if ((typeCache == 23 || typeCache == 24 || typeCache == 32) && this._rand.Next(500) == 0)
			{
				Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 14, 0f, 0f, 0, default(Color), 1f);
				return;
			}
			if (typeCache == 25 && this._rand.Next(700) == 0)
			{
				Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 14, 0f, 0f, 0, default(Color), 1f);
				return;
			}
			if (typeCache == 112 && this._rand.Next(700) == 0)
			{
				Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 14, 0f, 0f, 0, default(Color), 1f);
				return;
			}
			if (typeCache == 31 && this._rand.Next(20) == 0)
			{
				if (tileFrameX >= 36)
				{
					int num29 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 5, 0f, 0f, 100, default(Color), 1f);
					this._dust[num29].velocity.Y = 0f;
					Dust dust19 = this._dust[num29];
					dust19.velocity.X = dust19.velocity.X * 0.3f;
					return;
				}
				Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 14, 0f, 0f, 100, default(Color), 1f);
				return;
			}
			else if (typeCache == 26 && this._rand.Next(20) == 0)
			{
				if (tileFrameX >= 54)
				{
					int num30 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 5, 0f, 0f, 100, default(Color), 1f);
					this._dust[num30].scale = 1.5f;
					this._dust[num30].noGravity = true;
					this._dust[num30].velocity *= 0.75f;
					return;
				}
				Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 14, 0f, 0f, 100, default(Color), 1f);
				return;
			}
			else
			{
				if ((typeCache == 71 || typeCache == 72) && tileCache.color() == 0 && this._rand.Next(500) == 0)
				{
					Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 41, 0f, 0f, 250, default(Color), 0.8f);
					return;
				}
				if ((typeCache == 17 || typeCache == 77 || typeCache == 133) && this._rand.Next(40) == 0)
				{
					if (tileFrameX == 18 & tileFrameY == 18)
					{
						int num31 = Dust.NewDust(new Vector2((float)(i * 16 - 4), (float)(j * 16 - 6)), 8, 6, 6, 0f, 0f, 100, default(Color), 1f);
						if (this._rand.Next(3) != 0)
						{
							this._dust[num31].noGravity = true;
							return;
						}
					}
				}
				else if (typeCache == 405 && this._rand.Next(20) == 0)
				{
					if (tileFrameX == 18 & tileFrameY == 18)
					{
						int num32 = Dust.NewDust(new Vector2((float)(i * 16 - 4), (float)(j * 16 - 6)), 24, 10, 6, 0f, 0f, 100, default(Color), 1f);
						if (this._rand.Next(5) != 0)
						{
							this._dust[num32].noGravity = true;
							return;
						}
					}
				}
				else if (typeCache == 37 && this._rand.Next(250) == 0)
				{
					int num33 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 6, 0f, 0f, 0, default(Color), (float)this._rand.Next(3));
					if (this._dust[num33].scale > 1f)
					{
						this._dust[num33].noGravity = true;
						return;
					}
				}
				else
				{
					if ((typeCache == 58 || typeCache == 76 || typeCache == 684) && this._rand.Next(250) == 0)
					{
						int num34 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 6, 0f, 0f, 0, default(Color), (float)this._rand.Next(3));
						if (this._dust[num34].scale > 1f)
						{
							this._dust[num34].noGravity = true;
						}
						this._dust[num34].noLight = true;
						return;
					}
					if (typeCache == 61)
					{
						if (tileFrameX == 144 && this._rand.Next(60) == 0)
						{
							int num35 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 44, 0f, 0f, 250, default(Color), 0.4f);
							this._dust[num35].fadeIn = 0.7f;
							return;
						}
					}
					else if (Main.tileShine[(int)typeCache] > 0)
					{
						if (tileLight.R > 20 || tileLight.B > 20 || tileLight.G > 20)
						{
							int num36 = (int)tileLight.R;
							if ((int)tileLight.G > num36)
							{
								num36 = (int)tileLight.G;
							}
							if ((int)tileLight.B > num36)
							{
								num36 = (int)tileLight.B;
							}
							num36 /= 30;
							if (this._rand.Next(Main.tileShine[(int)typeCache]) < num36 && ((typeCache != 21 && typeCache != 441) || (tileFrameX >= 36 && tileFrameX < 180) || (tileFrameX >= 396 && tileFrameX <= 409)) && ((typeCache != 467 && typeCache != 468) || (tileFrameX >= 144 && tileFrameX < 180)))
							{
								Color white = Color.White;
								if (typeCache == 178)
								{
									int num37 = (int)(tileFrameX / 18);
									if (num37 == 0)
									{
										white = new Color(255, 0, 255, 255);
									}
									else if (num37 == 1)
									{
										white = new Color(255, 255, 0, 255);
									}
									else if (num37 == 2)
									{
										white = new Color(0, 0, 255, 255);
									}
									else if (num37 == 3)
									{
										white = new Color(0, 255, 0, 255);
									}
									else if (num37 == 4)
									{
										white = new Color(255, 0, 0, 255);
									}
									else if (num37 == 5)
									{
										white = new Color(255, 255, 255, 255);
									}
									else if (num37 == 6)
									{
										white = new Color(255, 255, 0, 255);
									}
									int num38 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 43, 0f, 0f, 254, white, 0.5f);
									this._dust[num38].velocity *= 0f;
									return;
								}
								if (typeCache == 63)
								{
									white = new Color(0, 0, 255, 255);
								}
								if (typeCache == 64)
								{
									white = new Color(255, 0, 0, 255);
								}
								if (typeCache == 65)
								{
									white = new Color(0, 255, 0, 255);
								}
								if (typeCache == 66)
								{
									white = new Color(255, 255, 0, 255);
								}
								if (typeCache == 67)
								{
									white = new Color(255, 0, 255, 255);
								}
								if (typeCache == 68)
								{
									white = new Color(255, 255, 255, 255);
								}
								if (typeCache == 12 || typeCache == 665)
								{
									white = new Color(255, 0, 0, 255);
								}
								if (typeCache == 639)
								{
									white = new Color(0, 0, 255, 255);
								}
								if (typeCache == 204)
								{
									white = new Color(255, 0, 0, 255);
								}
								if (typeCache == 211)
								{
									white = new Color(50, 255, 100, 255);
								}
								int num39 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 43, 0f, 0f, 254, white, 0.5f);
								this._dust[num39].velocity *= 0f;
								return;
							}
						}
					}
					else if (Main.tileSolid[(int)tileCache.type] && Main.shimmerAlpha > 0f && (tileLight.R > 20 || tileLight.B > 20 || tileLight.G > 20))
					{
						int num40 = (int)tileLight.R;
						if ((int)tileLight.G > num40)
						{
							num40 = (int)tileLight.G;
						}
						if ((int)tileLight.B > num40)
						{
							num40 = (int)tileLight.B;
						}
						int maxValue = 500;
						if ((float)this._rand.Next(maxValue) < 2f * Main.shimmerAlpha)
						{
							Color white2 = Color.White;
							float scale2 = ((float)num40 / 255f + 1f) / 2f;
							int num41 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 43, 0f, 0f, 254, white2, scale2);
							this._dust[num41].velocity *= 0f;
						}
					}
				}
				return;
			}
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x0053BA63 File Offset: 0x00539C63
		private void EmitLivingTreeLeaf(int i, int j, int leafGoreType)
		{
			this.EmitLivingTreeLeaf_Below(i, j, leafGoreType);
			if (this._rand.Next(2) == 0)
			{
				this.EmitLivingTreeLeaf_Sideways(i, j, leafGoreType);
			}
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x0053BA88 File Offset: 0x00539C88
		private void EmitLivingTreeLeaf_Below(int x, int y, int leafGoreType)
		{
			Tile tile = Main.tile[x, y + 1];
			if (WorldGen.SolidTile(tile) || tile.liquid > 0)
			{
				return;
			}
			float windForVisuals = Main.WindForVisuals;
			if (windForVisuals < -0.2f && (WorldGen.SolidTile(Main.tile[x - 1, y + 1]) || WorldGen.SolidTile(Main.tile[x - 2, y + 1])))
			{
				return;
			}
			if (windForVisuals > 0.2f && (WorldGen.SolidTile(Main.tile[x + 1, y + 1]) || WorldGen.SolidTile(Main.tile[x + 2, y + 1])))
			{
				return;
			}
			Gore.NewGorePerfect(new Vector2((float)(x * 16), (float)(y * 16 + 16)), Vector2.Zero, leafGoreType, 1f).Frame.CurrentColumn = Main.tile[x, y].color();
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x0053BB68 File Offset: 0x00539D68
		private void EmitLivingTreeLeaf_Sideways(int x, int y, int leafGoreType)
		{
			int num = 0;
			if (Main.WindForVisuals > 0.2f)
			{
				num = 1;
			}
			else if (Main.WindForVisuals < -0.2f)
			{
				num = -1;
			}
			Tile tile = Main.tile[x + num, y];
			if (WorldGen.SolidTile(tile) || tile.liquid > 0)
			{
				return;
			}
			int num2 = 0;
			if (num == -1)
			{
				num2 = -10;
			}
			Gore.NewGorePerfect(new Vector2((float)(x * 16 + 8 + 4 * num + num2), (float)(y * 16 + 8)), Vector2.Zero, leafGoreType, 1f).Frame.CurrentColumn = Main.tile[x, y].color();
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x0053BC04 File Offset: 0x00539E04
		private void EmitLiquidDrops(int j, int i, Tile tileCache, ushort typeCache)
		{
			int num = 60;
			if (typeCache == 374)
			{
				num = 120;
			}
			else if (typeCache == 375)
			{
				num = 180;
			}
			else if (typeCache == 461)
			{
				num = 180;
			}
			if (tileCache.liquid != 0 || this._rand.Next(num * 2) != 0)
			{
				return;
			}
			Rectangle rectangle = new Rectangle(i * 16, j * 16, 16, 16);
			rectangle.X -= 34;
			rectangle.Width += 68;
			rectangle.Y -= 100;
			rectangle.Height = 400;
			for (int k = 0; k < 600; k++)
			{
				if (this._gore[k].active && ((this._gore[k].type >= 706 && this._gore[k].type <= 717) || this._gore[k].type == 943 || this._gore[k].type == 1147 || (this._gore[k].type >= 1160 && this._gore[k].type <= 1162)))
				{
					Rectangle value = new Rectangle((int)this._gore[k].position.X, (int)this._gore[k].position.Y, 16, 16);
					if (rectangle.Intersects(value))
					{
						return;
					}
				}
			}
			Vector2 position = new Vector2((float)(i * 16), (float)(j * 16));
			int type = 706;
			if (Main.waterStyle == 14)
			{
				type = 706;
			}
			else if (Main.waterStyle == 13)
			{
				type = 706;
			}
			else if (Main.waterStyle == 12)
			{
				type = 1147;
			}
			else if (Main.waterStyle > 1)
			{
				type = 706 + Main.waterStyle - 1;
			}
			if (typeCache == 374)
			{
				type = 716;
			}
			if (typeCache == 375)
			{
				type = 717;
			}
			if (typeCache == 461)
			{
				type = 943;
				if (Main.player[Main.myPlayer].ZoneCorrupt)
				{
					type = 1160;
				}
				if (Main.player[Main.myPlayer].ZoneCrimson)
				{
					type = 1161;
				}
				if (Main.player[Main.myPlayer].ZoneHallow)
				{
					type = 1162;
				}
			}
			int num2 = Gore.NewGore(position, default(Vector2), type, 1f);
			this._gore[num2].velocity *= 0f;
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x0053BE94 File Offset: 0x0053A094
		private float GetWindCycle(int x, int y, double windCounter)
		{
			if (!Main.SettingsEnabled_TilesSwayInWind)
			{
				return 0f;
			}
			float num = (float)x * 0.5f + (float)(y / 100) * 0.5f;
			float num2 = (float)Math.Cos(windCounter * 6.2831854820251465 + (double)num) * 0.5f;
			if (Main.remixWorld)
			{
				if ((double)y <= Main.worldSurface)
				{
					return 0f;
				}
				num2 += Main.WindForVisuals;
			}
			else
			{
				if ((double)y >= Main.worldSurface)
				{
					return 0f;
				}
				num2 += Main.WindForVisuals;
			}
			float lerpValue = Utils.GetLerpValue(0.08f, 0.18f, Math.Abs(Main.WindForVisuals), true);
			return num2 * lerpValue;
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x0053BF38 File Offset: 0x0053A138
		private bool ShouldSwayInWind(int x, int y, Tile tileCache)
		{
			return Main.SettingsEnabled_TilesSwayInWind && TileID.Sets.SwaysInWindBasic[(int)tileCache.type] && (tileCache.type != 227 || (tileCache.frameX != 204 && tileCache.frameX != 238 && tileCache.frameX != 408 && tileCache.frameX != 442 && tileCache.frameX != 476));
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x0053BFB0 File Offset: 0x0053A1B0
		private void UpdateLeafFrequency()
		{
			float num = Math.Abs(Main.WindForVisuals);
			if (num <= 0.1f)
			{
				this._leafFrequency = 2000;
			}
			else if (num <= 0.2f)
			{
				this._leafFrequency = 1000;
			}
			else if (num <= 0.3f)
			{
				this._leafFrequency = 450;
			}
			else if (num <= 0.4f)
			{
				this._leafFrequency = 300;
			}
			else if (num <= 0.5f)
			{
				this._leafFrequency = 200;
			}
			else if (num <= 0.6f)
			{
				this._leafFrequency = 130;
			}
			else if (num <= 0.7f)
			{
				this._leafFrequency = 75;
			}
			else if (num <= 0.8f)
			{
				this._leafFrequency = 50;
			}
			else if (num <= 0.9f)
			{
				this._leafFrequency = 40;
			}
			else if (num <= 1f)
			{
				this._leafFrequency = 30;
			}
			else if (num <= 1.1f)
			{
				this._leafFrequency = 20;
			}
			else
			{
				this._leafFrequency = 10;
			}
			this._leafFrequency *= 7;
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x0053C0C4 File Offset: 0x0053A2C4
		private void EnsureWindGridSize()
		{
			Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
			Vector2 zero = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
			int num;
			int num2;
			int num3;
			int num4;
			this.GetScreenDrawArea(unscaledPosition, zero, out num, out num2, out num3, out num4);
			this._windGrid.SetSize(num2 - num, num4 - num3);
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x0053C120 File Offset: 0x0053A320
		private void EmitTreeLeaves(int tilePosX, int tilePosY, int grassPosX, int grassPosY)
		{
			if (this._isActiveAndNotPaused)
			{
				int num = grassPosY - tilePosY;
				Tile tile = Main.tile[tilePosX, tilePosY];
				if (tile.liquid > 0)
				{
					return;
				}
				int num2;
				int num3;
				WorldGen.GetTreeLeaf(tilePosX, tile, Main.tile[grassPosX, grassPosY], ref num, out num2, out num3);
				if (num3 == -1 || num3 == 912 || num3 == 913 || num3 == 1278)
				{
					return;
				}
				bool flag = (num3 >= 917 && num3 <= 925) || (num3 >= 1113 && num3 <= 1121);
				int num4 = this._leafFrequency;
				bool flag2 = tilePosX - grassPosX != 0;
				if (flag)
				{
					num4 /= 2;
				}
				if (!WorldGen.DoesWindBlowAtThisHeight(tilePosY))
				{
					num4 = 10000;
				}
				if (flag2)
				{
					num4 *= 3;
				}
				if (this._rand.Next(num4) == 0)
				{
					int num5 = 2;
					Vector2 vector = new Vector2((float)(tilePosX * 16 + 8), (float)(tilePosY * 16 + 8));
					if (flag2)
					{
						int num6 = tilePosX - grassPosX;
						vector.X += (float)(num6 * 12);
						int num7 = 0;
						if (tile.frameY == 220)
						{
							num7 = 1;
						}
						else if (tile.frameY == 242)
						{
							num7 = 2;
						}
						if (tile.frameX == 66)
						{
							switch (num7)
							{
							case 0:
								vector += new Vector2(0f, -6f);
								break;
							case 1:
								vector += new Vector2(0f, -6f);
								break;
							case 2:
								vector += new Vector2(0f, 8f);
								break;
							}
						}
						else
						{
							switch (num7)
							{
							case 0:
								vector += new Vector2(0f, 4f);
								break;
							case 1:
								vector += new Vector2(2f, -6f);
								break;
							case 2:
								vector += new Vector2(6f, -6f);
								break;
							}
						}
					}
					else
					{
						vector += new Vector2(-16f, -16f);
						if (flag)
						{
							vector.Y -= (float)(Main.rand.Next(0, 28) * 4);
						}
					}
					if (!WorldGen.SolidTile(vector.ToTileCoordinates()))
					{
						Gore.NewGoreDirect(vector, Utils.RandomVector2(Main.rand, (float)(-(float)num5), (float)num5), num3, 0.7f + Main.rand.NextFloat() * 0.6f).Frame.CurrentColumn = Main.tile[tilePosX, tilePosY].color();
					}
				}
			}
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x0053C3C8 File Offset: 0x0053A5C8
		private void DrawSpecialTilesLegacy(Vector2 screenPosition, Vector2 offSet)
		{
			for (int i = 0; i < this._specialTilesCount; i++)
			{
				int num = this._specialTileX[i];
				int num2 = this._specialTileY[i];
				Tile tile = Main.tile[num, num2];
				ushort type = tile.type;
				short frameX = tile.frameX;
				short frameY = tile.frameY;
				if (type == 237)
				{
					Main.spriteBatch.Draw(TextureAssets.SunOrb.Value, new Vector2((float)(num * 16 - (int)screenPosition.X) + 8f, (float)(num2 * 16 - (int)screenPosition.Y - 36)) + offSet, new Rectangle?(new Rectangle(0, 0, TextureAssets.SunOrb.Width(), TextureAssets.SunOrb.Height())), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, 0), Main.sunCircle, new Vector2((float)(TextureAssets.SunOrb.Width() / 2), (float)(TextureAssets.SunOrb.Height() / 2)), 1f, SpriteEffects.None, 0f);
				}
				if (type == 334 && frameX >= 5000)
				{
					short num3 = frameY / 18;
					int j = (int)frameX;
					int num4 = 0;
					int num5 = j % 5000;
					num5 -= 100;
					while (j >= 5000)
					{
						num4++;
						j -= 5000;
					}
					int num6 = (int)Main.tile[num + 1, num2].frameX;
					if (num6 >= 25000)
					{
						num6 -= 25000;
					}
					else
					{
						num6 -= 10000;
					}
					Item item = new Item();
					item.netDefaults(num5);
					item.Prefix(num6);
					Main.instance.LoadItem(item.type);
					Texture2D value = TextureAssets.Item[item.type].Value;
					Rectangle rectangle;
					if (Main.itemAnimations[item.type] != null)
					{
						rectangle = Main.itemAnimations[item.type].GetFrame(value, -1);
					}
					else
					{
						rectangle = value.Frame(1, 1, 0, 0, 0, 0);
					}
					int width = rectangle.Width;
					int height = rectangle.Height;
					float num7 = 1f;
					if (width > 40 || height > 40)
					{
						if (width > height)
						{
							num7 = 40f / (float)width;
						}
						else
						{
							num7 = 40f / (float)height;
						}
					}
					num7 *= item.scale;
					SpriteEffects effects = SpriteEffects.None;
					if (num4 >= 3)
					{
						effects = SpriteEffects.FlipHorizontally;
					}
					Color color = Lighting.GetColor(num, num2);
					Main.spriteBatch.Draw(value, new Vector2((float)(num * 16 - (int)screenPosition.X + 24), (float)(num2 * 16 - (int)screenPosition.Y + 8)) + offSet, new Rectangle?(rectangle), Lighting.GetColor(num, num2), 0f, new Vector2((float)(width / 2), (float)(height / 2)), num7, effects, 0f);
					if (item.color != default(Color))
					{
						Main.spriteBatch.Draw(value, new Vector2((float)(num * 16 - (int)screenPosition.X + 24), (float)(num2 * 16 - (int)screenPosition.Y + 8)) + offSet, new Rectangle?(rectangle), item.GetColor(color), 0f, new Vector2((float)(width / 2), (float)(height / 2)), num7, effects, 0f);
					}
				}
				if (type == 395)
				{
					Item item2 = ((TEItemFrame)TileEntity.ByPosition[new Point16(num, num2)]).item;
					Vector2 screenPositionForItemCenter = new Vector2((float)(num * 16 - (int)screenPosition.X + 16), (float)(num2 * 16 - (int)screenPosition.Y + 16)) + offSet;
					Color color2 = Lighting.GetColor(num, num2);
					ItemSlot.DrawItemIcon(item2, 31, Main.spriteBatch, screenPositionForItemCenter, item2.scale, 20f, color2);
				}
				if (type == 520)
				{
					Item item3 = ((TEFoodPlatter)TileEntity.ByPosition[new Point16(num, num2)]).item;
					if (!item3.IsAir)
					{
						Main.instance.LoadItem(item3.type);
						Texture2D value2 = TextureAssets.Item[item3.type].Value;
						Rectangle rectangle2;
						if (ItemID.Sets.IsFood[item3.type])
						{
							rectangle2 = value2.Frame(1, 3, 0, 2, 0, 0);
						}
						else
						{
							rectangle2 = value2.Frame(1, 1, 0, 0, 0, 0);
						}
						int width2 = rectangle2.Width;
						int height2 = rectangle2.Height;
						float num8 = 1f;
						SpriteEffects effects2 = (tile.frameX == 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
						Color color3 = Lighting.GetColor(num, num2);
						Color color4 = color3;
						float num9 = 1f;
						ItemSlot.GetItemLight(ref color4, ref num9, item3, false);
						num8 *= num9;
						Vector2 position = new Vector2((float)(num * 16 - (int)screenPosition.X + 8), (float)(num2 * 16 - (int)screenPosition.Y + 16)) + offSet;
						position.Y += 2f;
						Vector2 origin = new Vector2((float)(width2 / 2), (float)height2);
						Main.spriteBatch.Draw(value2, position, new Rectangle?(rectangle2), color4, 0f, origin, num8, effects2, 0f);
						if (item3.color != default(Color))
						{
							Main.spriteBatch.Draw(value2, position, new Rectangle?(rectangle2), item3.GetColor(color3), 0f, origin, num8, effects2, 0f);
						}
					}
				}
				if (type == 471)
				{
					Item item4 = (TileEntity.ByPosition[new Point16(num, num2)] as TEWeaponsRack).item;
					Texture2D texture;
					Rectangle rectangle3;
					Main.GetItemDrawFrame(item4.type, out texture, out rectangle3);
					int width3 = rectangle3.Width;
					int height3 = rectangle3.Height;
					float num10 = 1f;
					float num11 = 40f;
					if ((float)width3 > num11 || (float)height3 > num11)
					{
						if (width3 > height3)
						{
							num10 = num11 / (float)width3;
						}
						else
						{
							num10 = num11 / (float)height3;
						}
					}
					num10 *= item4.scale;
					SpriteEffects effects3 = SpriteEffects.FlipHorizontally;
					if (tile.frameX < 54)
					{
						effects3 = SpriteEffects.None;
					}
					Color color5 = Lighting.GetColor(num, num2);
					Color color6 = color5;
					float num12 = 1f;
					ItemSlot.GetItemLight(ref color6, ref num12, item4, false);
					num10 *= num12;
					Main.spriteBatch.Draw(texture, new Vector2((float)(num * 16 - (int)screenPosition.X + 24), (float)(num2 * 16 - (int)screenPosition.Y + 24)) + offSet, new Rectangle?(rectangle3), color6, 0f, new Vector2((float)(width3 / 2), (float)(height3 / 2)), num10, effects3, 0f);
					if (item4.color != default(Color))
					{
						Main.spriteBatch.Draw(texture, new Vector2((float)(num * 16 - (int)screenPosition.X + 24), (float)(num2 * 16 - (int)screenPosition.Y + 24)) + offSet, new Rectangle?(rectangle3), item4.GetColor(color5), 0f, new Vector2((float)(width3 / 2), (float)(height3 / 2)), num10, effects3, 0f);
					}
				}
				if (type == 412)
				{
					Texture2D value3 = TextureAssets.GlowMask[202].Value;
					int num13 = Main.tileFrame[(int)type] / 60;
					int frameY2 = (num13 + 1) % 4;
					float num14 = (float)(Main.tileFrame[(int)type] % 60) / 60f;
					Color value4 = new Color(255, 255, 255, 255);
					Main.spriteBatch.Draw(value3, new Vector2((float)(num * 16 - (int)screenPosition.X), (float)(num2 * 16 - (int)screenPosition.Y + 10)) + offSet, new Rectangle?(value3.Frame(1, 4, 0, num13, 0, 0)), value4 * (1f - num14), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
					Main.spriteBatch.Draw(value3, new Vector2((float)(num * 16 - (int)screenPosition.X), (float)(num2 * 16 - (int)screenPosition.Y + 10)) + offSet, new Rectangle?(value3.Frame(1, 4, 0, frameY2, 0, 0)), value4 * num14, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				}
				if (type == 620)
				{
					Texture2D value5 = TextureAssets.Extra[202].Value;
					float num15 = (float)(Main.tileFrame[(int)type] % 60) / 60f;
					int num16 = 2;
					Main.critterCage = true;
					int waterAnimalCageFrame = this.GetWaterAnimalCageFrame(num, num2, (int)frameX, (int)frameY);
					int num17 = 8;
					int num18 = Main.butterflyCageFrame[num17, waterAnimalCageFrame];
					int num19 = 6;
					float num20 = 1f;
					Rectangle value6 = new Rectangle(0, 34 * num18, 32, 32);
					Vector2 vector = new Vector2((float)(num * 16 - (int)screenPosition.X), (float)(num2 * 16 - (int)screenPosition.Y + num16)) + offSet;
					Main.spriteBatch.Draw(value5, vector, new Rectangle?(value6), new Color(255, 255, 255, 255), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
					for (int k = 0; k < num19; k++)
					{
						Color color7 = new Color(127, 127, 127, 0).MultiplyRGBA(Main.hslToRgb((Main.GlobalTimeWrappedHourly + (float)k / (float)num19) % 1f, 1f, 0.5f, byte.MaxValue));
						color7 *= 1f - num20 * 0.5f;
						color7.A = 0;
						int num21 = 2;
						Vector2 position2 = vector + ((float)k / (float)num19 * 6.2831855f).ToRotationVector2() * ((float)num21 * num20 + 2f);
						Main.spriteBatch.Draw(value5, position2, new Rectangle?(value6), color7, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
					}
					Main.spriteBatch.Draw(value5, vector, new Rectangle?(value6), new Color(255, 255, 255, 0) * 0.1f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				}
			}
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x0053CDE0 File Offset: 0x0053AFE0
		private void DrawEntities_DisplayDolls()
		{
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
			foreach (KeyValuePair<Point, int> keyValuePair in this._displayDollTileEntityPositions)
			{
				TileEntity tileEntity;
				if (keyValuePair.Value != -1 && TileEntity.ByPosition.TryGetValue(new Point16(keyValuePair.Key.X, keyValuePair.Key.Y), out tileEntity))
				{
					(tileEntity as TEDisplayDoll).Draw(keyValuePair.Key.X, keyValuePair.Key.Y);
				}
			}
			Main.spriteBatch.End();
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x0053CEB4 File Offset: 0x0053B0B4
		private void DrawEntities_HatRacks()
		{
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
			foreach (KeyValuePair<Point, int> keyValuePair in this._hatRackTileEntityPositions)
			{
				TileEntity tileEntity;
				if (keyValuePair.Value != -1 && TileEntity.ByPosition.TryGetValue(new Point16(keyValuePair.Key.X, keyValuePair.Key.Y), out tileEntity))
				{
					(tileEntity as TEHatRack).Draw(keyValuePair.Key.X, keyValuePair.Key.Y);
				}
			}
			Main.spriteBatch.End();
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x0053CF88 File Offset: 0x0053B188
		private void DrawTrees()
		{
			Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
			Vector2 zero = Vector2.Zero;
			int num = 0;
			int num2 = this._specialsCount[num];
			float num3 = 0.08f;
			float num4 = 0.06f;
			for (int i = 0; i < num2; i++)
			{
				Point point = this._specialPositions[num][i];
				int x = point.X;
				int y = point.Y;
				Tile tile = Main.tile[x, y];
				if (tile != null && tile.active())
				{
					ushort type = tile.type;
					short frameX = tile.frameX;
					short frameY = tile.frameY;
					bool flag = tile.wall > 0;
					WorldGen.GetTreeFoliageDataMethod getTreeFoliageDataMethod = null;
					try
					{
						bool flag2 = false;
						if (type <= 589)
						{
							if (type != 5)
							{
								if (type - 583 <= 6)
								{
									flag2 = true;
									getTreeFoliageDataMethod = new WorldGen.GetTreeFoliageDataMethod(WorldGen.GetGemTreeFoliageData);
								}
							}
							else
							{
								flag2 = true;
								getTreeFoliageDataMethod = new WorldGen.GetTreeFoliageDataMethod(WorldGen.GetCommonTreeFoliageData);
							}
						}
						else if (type != 596 && type != 616)
						{
							if (type == 634)
							{
								flag2 = true;
								getTreeFoliageDataMethod = new WorldGen.GetTreeFoliageDataMethod(WorldGen.GetAshTreeFoliageData);
							}
						}
						else
						{
							flag2 = true;
							getTreeFoliageDataMethod = new WorldGen.GetTreeFoliageDataMethod(WorldGen.GetVanityTreeFoliageData);
						}
						if (flag2 && frameY >= 198 && frameX >= 22)
						{
							int treeFrame = WorldGen.GetTreeFrame(tile);
							if (frameX == 22)
							{
								int num5 = 0;
								int num6 = 80;
								int num7 = 80;
								int num8 = 0;
								int grassPosX = x + num8;
								int grassPosY = y;
								if (!getTreeFoliageDataMethod(x, y, num8, ref treeFrame, ref num5, out grassPosY, out num6, out num7))
								{
									goto IL_9F2;
								}
								this.EmitTreeLeaves(x, y, grassPosX, grassPosY);
								if (num5 == 14)
								{
									float num9 = (float)this._rand.Next(28, 42) * 0.005f;
									num9 += (float)(270 - (int)Main.mouseTextColor) / 1000f;
									if (tile.color() == 0)
									{
										Lighting.AddLight(x, y, 0.1f, 0.2f + num9 / 2f, 0.7f + num9);
									}
									else
									{
										Color color = WorldGen.paintColor((int)tile.color());
										float r = (float)color.R / 255f;
										float g = (float)color.G / 255f;
										float b = (float)color.B / 255f;
										Lighting.AddLight(x, y, r, g, b);
									}
								}
								byte tileColor = tile.color();
								Texture2D treeTopTexture = this.GetTreeTopTexture(num5, 0, tileColor);
								Vector2 position = position = new Vector2((float)(x * 16 - (int)unscaledPosition.X + 8), (float)(y * 16 - (int)unscaledPosition.Y + 16)) + zero;
								float num10 = 0f;
								if (!flag)
								{
									num10 = this.GetWindCycle(x, y, this._treeWindCounter);
								}
								position.X += num10 * 2f;
								position.Y += Math.Abs(num10) * 2f;
								Color color2 = Lighting.GetColor(x, y);
								if (tile.fullbrightBlock())
								{
									color2 = Color.White;
								}
								Main.spriteBatch.Draw(treeTopTexture, position, new Rectangle?(new Rectangle(treeFrame * (num6 + 2), 0, num6, num7)), color2, num10 * num3, new Vector2((float)(num6 / 2), (float)num7), 1f, SpriteEffects.None, 0f);
								if (type == 634)
								{
									Texture2D value = TextureAssets.GlowMask[316].Value;
									Color white = Color.White;
									Main.spriteBatch.Draw(value, position, new Rectangle?(new Rectangle(treeFrame * (num6 + 2), 0, num6, num7)), white, num10 * num3, new Vector2((float)(num6 / 2), (float)num7), 1f, SpriteEffects.None, 0f);
								}
							}
							else if (frameX == 44)
							{
								int num11 = 0;
								int num12 = x;
								int grassPosY2 = y;
								int num13 = 1;
								int num14;
								int num15;
								if (!getTreeFoliageDataMethod(x, y, num13, ref treeFrame, ref num11, out grassPosY2, out num14, out num15))
								{
									goto IL_9F2;
								}
								this.EmitTreeLeaves(x, y, num12 + num13, grassPosY2);
								if (num11 == 14)
								{
									float num16 = (float)this._rand.Next(28, 42) * 0.005f;
									num16 += (float)(270 - (int)Main.mouseTextColor) / 1000f;
									if (tile.color() == 0)
									{
										Lighting.AddLight(x, y, 0.1f, 0.2f + num16 / 2f, 0.7f + num16);
									}
									else
									{
										Color color3 = WorldGen.paintColor((int)tile.color());
										float r2 = (float)color3.R / 255f;
										float g2 = (float)color3.G / 255f;
										float b2 = (float)color3.B / 255f;
										Lighting.AddLight(x, y, r2, g2, b2);
									}
								}
								byte tileColor2 = tile.color();
								Texture2D treeBranchTexture = this.GetTreeBranchTexture(num11, 0, tileColor2);
								Vector2 position2 = new Vector2((float)(x * 16), (float)(y * 16)) - unscaledPosition.Floor() + zero + new Vector2(16f, 12f);
								float num17 = 0f;
								if (!flag)
								{
									num17 = this.GetWindCycle(x, y, this._treeWindCounter);
								}
								if (num17 > 0f)
								{
									position2.X += num17;
								}
								position2.X += Math.Abs(num17) * 2f;
								Color color4 = Lighting.GetColor(x, y);
								if (tile.fullbrightBlock())
								{
									color4 = Color.White;
								}
								Main.spriteBatch.Draw(treeBranchTexture, position2, new Rectangle?(new Rectangle(0, treeFrame * 42, 40, 40)), color4, num17 * num4, new Vector2(40f, 24f), 1f, SpriteEffects.None, 0f);
								if (type == 634)
								{
									Texture2D value2 = TextureAssets.GlowMask[317].Value;
									Color white2 = Color.White;
									Main.spriteBatch.Draw(value2, position2, new Rectangle?(new Rectangle(0, treeFrame * 42, 40, 40)), white2, num17 * num4, new Vector2(40f, 24f), 1f, SpriteEffects.None, 0f);
								}
							}
							else if (frameX == 66)
							{
								int num18 = 0;
								int num19 = x;
								int grassPosY3 = y;
								int num20 = -1;
								int num21;
								int num22;
								if (!getTreeFoliageDataMethod(x, y, num20, ref treeFrame, ref num18, out grassPosY3, out num21, out num22))
								{
									goto IL_9F2;
								}
								this.EmitTreeLeaves(x, y, num19 + num20, grassPosY3);
								if (num18 == 14)
								{
									float num23 = (float)this._rand.Next(28, 42) * 0.005f;
									num23 += (float)(270 - (int)Main.mouseTextColor) / 1000f;
									if (tile.color() == 0)
									{
										Lighting.AddLight(x, y, 0.1f, 0.2f + num23 / 2f, 0.7f + num23);
									}
									else
									{
										Color color5 = WorldGen.paintColor((int)tile.color());
										float r3 = (float)color5.R / 255f;
										float g3 = (float)color5.G / 255f;
										float b3 = (float)color5.B / 255f;
										Lighting.AddLight(x, y, r3, g3, b3);
									}
								}
								byte tileColor3 = tile.color();
								Texture2D treeBranchTexture2 = this.GetTreeBranchTexture(num18, 0, tileColor3);
								Vector2 position3 = new Vector2((float)(x * 16), (float)(y * 16)) - unscaledPosition.Floor() + zero + new Vector2(0f, 18f);
								float num24 = 0f;
								if (!flag)
								{
									num24 = this.GetWindCycle(x, y, this._treeWindCounter);
								}
								if (num24 < 0f)
								{
									position3.X += num24;
								}
								position3.X -= Math.Abs(num24) * 2f;
								Color color6 = Lighting.GetColor(x, y);
								if (tile.fullbrightBlock())
								{
									color6 = Color.White;
								}
								Main.spriteBatch.Draw(treeBranchTexture2, position3, new Rectangle?(new Rectangle(42, treeFrame * 42, 40, 40)), color6, num24 * num4, new Vector2(0f, 30f), 1f, SpriteEffects.None, 0f);
								if (type == 634)
								{
									Texture2D value3 = TextureAssets.GlowMask[317].Value;
									Color white3 = Color.White;
									Main.spriteBatch.Draw(value3, position3, new Rectangle?(new Rectangle(42, treeFrame * 42, 40, 40)), white3, num24 * num4, new Vector2(0f, 30f), 1f, SpriteEffects.None, 0f);
								}
							}
						}
						if (type == 323 && frameX >= 88 && frameX <= 132)
						{
							int num25 = 0;
							if (frameX == 110)
							{
								num25 = 1;
							}
							else if (frameX == 132)
							{
								num25 = 2;
							}
							int treeTextureIndex = 15;
							int num26 = 80;
							int num27 = 80;
							int num28 = 32;
							int num29 = 0;
							int palmTreeBiome = this.GetPalmTreeBiome(x, y);
							int y2 = palmTreeBiome * 82;
							if (palmTreeBiome >= 4 && palmTreeBiome <= 7)
							{
								treeTextureIndex = 21;
								num26 = 114;
								num27 = 98;
								y2 = (palmTreeBiome - 4) * 98;
								num28 = 48;
								num29 = 2;
							}
							int frameY2 = (int)Main.tile[x, y].frameY;
							byte tileColor4 = tile.color();
							Texture2D treeTopTexture2 = this.GetTreeTopTexture(treeTextureIndex, palmTreeBiome, tileColor4);
							Vector2 position4 = new Vector2((float)(x * 16 - (int)unscaledPosition.X - num28 + frameY2 + num26 / 2), (float)(y * 16 - (int)unscaledPosition.Y + 16 + num29)) + zero;
							float num30 = 0f;
							if (!flag)
							{
								num30 = this.GetWindCycle(x, y, this._treeWindCounter);
							}
							position4.X += num30 * 2f;
							position4.Y += Math.Abs(num30) * 2f;
							Color color7 = Lighting.GetColor(x, y);
							if (tile.fullbrightBlock())
							{
								color7 = Color.White;
							}
							Main.spriteBatch.Draw(treeTopTexture2, position4, new Rectangle?(new Rectangle(num25 * (num26 + 2), y2, num26, num27)), color7, num30 * num3, new Vector2((float)(num26 / 2), (float)num27), 1f, SpriteEffects.None, 0f);
						}
					}
					catch
					{
					}
				}
				IL_9F2:;
			}
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x0053D9B4 File Offset: 0x0053BBB4
		private Texture2D GetTreeTopTexture(int treeTextureIndex, int treeTextureStyle, byte tileColor)
		{
			Texture2D texture2D = this._paintSystem.TryGetTreeTopAndRequestIfNotReady(treeTextureIndex, treeTextureStyle, (int)tileColor);
			if (texture2D == null)
			{
				texture2D = TextureAssets.TreeTop[treeTextureIndex].Value;
			}
			return texture2D;
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x0053D9E4 File Offset: 0x0053BBE4
		private Texture2D GetTreeBranchTexture(int treeTextureIndex, int treeTextureStyle, byte tileColor)
		{
			Texture2D texture2D = this._paintSystem.TryGetTreeBranchAndRequestIfNotReady(treeTextureIndex, treeTextureStyle, (int)tileColor);
			if (texture2D == null)
			{
				texture2D = TextureAssets.TreeBranch[treeTextureIndex].Value;
			}
			return texture2D;
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x0053DA14 File Offset: 0x0053BC14
		private void DrawGrass()
		{
			Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
			Vector2 zero = Vector2.Zero;
			int num = 3;
			int num2 = this._specialsCount[num];
			for (int i = 0; i < num2; i++)
			{
				Point point = this._specialPositions[num][i];
				int x = point.X;
				int y = point.Y;
				Tile tile = Main.tile[x, y];
				if (tile != null && tile.active() && this.IsVisible(tile))
				{
					ushort num3 = tile.type;
					short frameX = tile.frameX;
					short frameY = tile.frameY;
					int num4;
					int num5;
					int num6;
					int num7;
					int num8;
					int num9;
					SpriteEffects effects;
					Texture2D texture2D;
					Rectangle value;
					Color color;
					this.GetTileDrawData(x, y, tile, num3, ref frameX, ref frameY, out num4, out num5, out num6, out num7, out num8, out num9, out effects, out texture2D, out value, out color);
					bool flag = this._rand.Next(4) == 0;
					Color color2 = Lighting.GetColor(x, y);
					this.DrawAnimatedTile_AdjustForVisionChangers(x, y, tile, num3, frameX, frameY, ref color2, flag);
					color2 = this.DrawTiles_GetLightOverride(y, x, tile, num3, frameX, frameY, color2);
					if (this._isActiveAndNotPaused && flag)
					{
						this.DrawTiles_EmitParticles(y, x, tile, num3, frameX, frameY, color2);
					}
					if (num3 == 83 && this.IsAlchemyPlantHarvestable((int)(frameX / 18)))
					{
						num3 = 84;
						Main.instance.LoadTiles((int)num3);
					}
					Vector2 position = new Vector2((float)(x * 16 - (int)unscaledPosition.X + 8), (float)(y * 16 - (int)unscaledPosition.Y + 16)) + zero;
					double grassWindCounter = this._grassWindCounter;
					float num10 = this.GetWindCycle(x, y, this._grassWindCounter);
					if (!WallID.Sets.AllowsWind[(int)tile.wall])
					{
						num10 = 0f;
					}
					if (!this.InAPlaceWithWind(x, y, 1, 1))
					{
						num10 = 0f;
					}
					num10 += this.GetWindGridPush(x, y, 20, 0.35f);
					position.X += num10 * 1f;
					position.Y += Math.Abs(num10) * 1f;
					Texture2D tileDrawTexture = this.GetTileDrawTexture(tile, x, y);
					if (tileDrawTexture != null)
					{
						Main.spriteBatch.Draw(tileDrawTexture, position, new Rectangle?(new Rectangle((int)frameX + num8, (int)frameY + num9, num4, num5 - num7)), color2, num10 * 0.1f, new Vector2((float)(num4 / 2), (float)(16 - num7 - num6)), 1f, effects, 0f);
						if (texture2D != null)
						{
							Main.spriteBatch.Draw(texture2D, position, new Rectangle?(value), color, num10 * 0.1f, new Vector2((float)(num4 / 2), (float)(16 - num7 - num6)), 1f, effects, 0f);
						}
					}
				}
			}
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x0053DCC8 File Offset: 0x0053BEC8
		private void DrawAnyDirectionalGrass()
		{
			Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
			Vector2 zero = Vector2.Zero;
			int num = 12;
			int num2 = this._specialsCount[num];
			for (int i = 0; i < num2; i++)
			{
				Point point = this._specialPositions[num][i];
				int x = point.X;
				int y = point.Y;
				Tile tile = Main.tile[x, y];
				if (tile != null && tile.active() && this.IsVisible(tile))
				{
					ushort num3 = tile.type;
					short frameX = tile.frameX;
					short frameY = tile.frameY;
					int num4;
					int num5;
					int num6;
					int num7;
					int num8;
					int num9;
					SpriteEffects effects;
					Texture2D texture2D;
					Rectangle rectangle;
					Color color;
					this.GetTileDrawData(x, y, tile, num3, ref frameX, ref frameY, out num4, out num5, out num6, out num7, out num8, out num9, out effects, out texture2D, out rectangle, out color);
					bool flag = this._rand.Next(4) == 0;
					Color color2 = Lighting.GetColor(x, y);
					this.DrawAnimatedTile_AdjustForVisionChangers(x, y, tile, num3, frameX, frameY, ref color2, flag);
					color2 = this.DrawTiles_GetLightOverride(y, x, tile, num3, frameX, frameY, color2);
					if (this._isActiveAndNotPaused && flag)
					{
						this.DrawTiles_EmitParticles(y, x, tile, num3, frameX, frameY, color2);
					}
					if (num3 == 83 && this.IsAlchemyPlantHarvestable((int)(frameX / 18)))
					{
						num3 = 84;
						Main.instance.LoadTiles((int)num3);
					}
					Vector2 position = new Vector2((float)(x * 16 - (int)unscaledPosition.X), (float)(y * 16 - (int)unscaledPosition.Y)) + zero;
					double grassWindCounter = this._grassWindCounter;
					float num10 = this.GetWindCycle(x, y, this._grassWindCounter);
					if (!WallID.Sets.AllowsWind[(int)tile.wall])
					{
						num10 = 0f;
					}
					if (!this.InAPlaceWithWind(x, y, 1, 1))
					{
						num10 = 0f;
					}
					float num11;
					float num12;
					this.GetWindGridPush2Axis(x, y, 20, 0.35f, out num11, out num12);
					int num13 = 1;
					int num14 = 0;
					Vector2 origin = new Vector2((float)(num4 / 2), (float)(16 - num7 - num6));
					switch (frameY / 54)
					{
					case 0:
						num13 = 1;
						num14 = 0;
						origin = new Vector2((float)(num4 / 2), (float)(16 - num7 - num6));
						position.X += 8f;
						position.Y += 16f;
						position.X += num10;
						position.Y += Math.Abs(num10);
						break;
					case 1:
						num10 *= -1f;
						num13 = -1;
						num14 = 0;
						origin = new Vector2((float)(num4 / 2), (float)(-(float)num6));
						position.X += 8f;
						position.X += -num10;
						position.Y += -Math.Abs(num10);
						break;
					case 2:
						num13 = 0;
						num14 = 1;
						origin = new Vector2(2f, (float)((16 - num7 - num6) / 2));
						position.Y += 8f;
						position.Y += num10;
						position.X += -Math.Abs(num10);
						break;
					case 3:
						num10 *= -1f;
						num13 = 0;
						num14 = -1;
						origin = new Vector2(14f, (float)((16 - num7 - num6) / 2));
						position.X += 16f;
						position.Y += 8f;
						position.Y += -num10;
						position.X += Math.Abs(num10);
						break;
					}
					num10 += num11 * (float)num13 + num12 * (float)num14;
					Texture2D tileDrawTexture = this.GetTileDrawTexture(tile, x, y);
					if (tileDrawTexture != null)
					{
						Main.spriteBatch.Draw(tileDrawTexture, position, new Rectangle?(new Rectangle((int)frameX + num8, (int)frameY + num9, num4, num5 - num7)), color2, num10 * 0.1f, origin, 1f, effects, 0f);
						if (texture2D != null)
						{
							Main.spriteBatch.Draw(texture2D, position, new Rectangle?(new Rectangle((int)frameX + num8, (int)frameY + num9, num4, num5 - num7)), color, num10 * 0.1f, origin, 1f, effects, 0f);
						}
					}
				}
			}
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0053E0FC File Offset: 0x0053C2FC
		private void DrawAnimatedTile_AdjustForVisionChangers(int i, int j, Tile tileCache, ushort typeCache, short tileFrameX, short tileFrameY, ref Color tileLight, bool canDoDust)
		{
			if (this._localPlayer.dangerSense && TileDrawing.IsTileDangerous(this._localPlayer, tileCache, typeCache))
			{
				if (tileLight.R < 255)
				{
					tileLight.R = byte.MaxValue;
				}
				if (tileLight.G < 50)
				{
					tileLight.G = 50;
				}
				if (tileLight.B < 50)
				{
					tileLight.B = 50;
				}
				if (this._isActiveAndNotPaused && canDoDust && this._rand.Next(30) == 0)
				{
					int num = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 60, 0f, 0f, 100, default(Color), 0.3f);
					this._dust[num].fadeIn = 1f;
					this._dust[num].velocity *= 0.1f;
					this._dust[num].noLight = true;
					this._dust[num].noGravity = true;
				}
			}
			if (this._localPlayer.findTreasure && Main.IsTileSpelunkable(typeCache, tileFrameX, tileFrameY))
			{
				if (tileLight.R < 200)
				{
					tileLight.R = 200;
				}
				if (tileLight.G < 170)
				{
					tileLight.G = 170;
				}
				if (this._isActiveAndNotPaused && (this._rand.Next(60) == 0 && canDoDust))
				{
					int num2 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 204, 0f, 0f, 150, default(Color), 0.3f);
					this._dust[num2].fadeIn = 1f;
					this._dust[num2].velocity *= 0.1f;
					this._dust[num2].noLight = true;
				}
			}
			if (this._localPlayer.biomeSight)
			{
				Color white = Color.White;
				if (Main.IsTileBiomeSightable(typeCache, tileFrameX, tileFrameY, ref white))
				{
					if (tileLight.R < white.R)
					{
						tileLight.R = white.R;
					}
					if (tileLight.G < white.G)
					{
						tileLight.G = white.G;
					}
					if (tileLight.B < white.B)
					{
						tileLight.B = white.B;
					}
					if (this._isActiveAndNotPaused && canDoDust && this._rand.Next(480) == 0)
					{
						Color newColor = white;
						int num3 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 267, 0f, 0f, 150, newColor, 0.3f);
						this._dust[num3].noGravity = true;
						this._dust[num3].fadeIn = 1f;
						this._dust[num3].velocity *= 0.1f;
						this._dust[num3].noLightEmittence = true;
					}
				}
			}
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x0053E428 File Offset: 0x0053C628
		private float GetWindGridPush(int i, int j, int pushAnimationTimeTotal, float pushForcePerFrame)
		{
			int num;
			int num2;
			int num3;
			this._windGrid.GetWindTime(i, j, pushAnimationTimeTotal, out num, out num2, out num3);
			if (num >= pushAnimationTimeTotal / 2)
			{
				return (float)(pushAnimationTimeTotal - num) * pushForcePerFrame * (float)num2;
			}
			return (float)num * pushForcePerFrame * (float)num2;
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x0053E464 File Offset: 0x0053C664
		private void GetWindGridPush2Axis(int i, int j, int pushAnimationTimeTotal, float pushForcePerFrame, out float pushX, out float pushY)
		{
			int num;
			int num2;
			int num3;
			this._windGrid.GetWindTime(i, j, pushAnimationTimeTotal, out num, out num2, out num3);
			if (num >= pushAnimationTimeTotal / 2)
			{
				pushX = (float)(pushAnimationTimeTotal - num) * pushForcePerFrame * (float)num2;
				pushY = (float)(pushAnimationTimeTotal - num) * pushForcePerFrame * (float)num3;
				return;
			}
			pushX = (float)num * pushForcePerFrame * (float)num2;
			pushY = (float)num * pushForcePerFrame * (float)num3;
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x0053E4BC File Offset: 0x0053C6BC
		private float GetWindGridPushComplex(int i, int j, int pushAnimationTimeTotal, float totalPushForce, int loops, bool flipDirectionPerLoop)
		{
			int num;
			int num2;
			int num3;
			this._windGrid.GetWindTime(i, j, pushAnimationTimeTotal, out num, out num2, out num3);
			float num4 = (float)num / (float)pushAnimationTimeTotal;
			int num5 = (int)(num4 * (float)loops);
			float num6 = num4 * (float)loops % 1f;
			float num7 = 1f / (float)loops;
			if (flipDirectionPerLoop && num5 % 2 == 1)
			{
				num2 *= -1;
			}
			if (num4 * (float)loops % 1f > 0.5f)
			{
				return (1f - num6) * totalPushForce * (float)num2 * (float)(loops - num5);
			}
			return num6 * totalPushForce * (float)num2 * (float)(loops - num5);
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x0053E544 File Offset: 0x0053C744
		private void DrawMasterTrophies()
		{
			int num = 11;
			int num2 = this._specialsCount[num];
			for (int i = 0; i < num2; i++)
			{
				Point point = this._specialPositions[num][i];
				Tile tile = Main.tile[point.X, point.Y];
				if (tile != null && tile.active())
				{
					Texture2D value = TextureAssets.Extra[198].Value;
					int frameY = (int)(tile.frameX / 54);
					bool flag = tile.frameY / 72 != 0;
					int horizontalFrames = 1;
					int verticalFrames = 28;
					Rectangle rectangle = value.Frame(horizontalFrames, verticalFrames, 0, frameY, 0, 0);
					Vector2 origin = rectangle.Size() / 2f;
					Vector2 value2 = point.ToWorldCoordinates(24f, 64f);
					float num3 = (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f / 5f));
					Vector2 value3 = value2 + new Vector2(0f, -40f) + new Vector2(0f, num3 * 4f);
					Color color = Lighting.GetColor(point.X, point.Y);
					SpriteEffects effects = flag ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
					Main.spriteBatch.Draw(value, value3 - Main.screenPosition, new Rectangle?(rectangle), color, 0f, origin, 1f, effects, 0f);
					float scale = (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f / 2f)) * 0.3f + 0.7f;
					Color color2 = color;
					color2.A = 0;
					color2 = color2 * 0.1f * scale;
					for (float num4 = 0f; num4 < 1f; num4 += 0.16666667f)
					{
						Main.spriteBatch.Draw(value, value3 - Main.screenPosition + (6.2831855f * num4).ToRotationVector2() * (6f + num3 * 2f), new Rectangle?(rectangle), color2, 0f, origin, 1f, effects, 0f);
					}
				}
			}
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x0053E764 File Offset: 0x0053C964
		private void DrawTeleportationPylons()
		{
			int num = 10;
			int num2 = this._specialsCount[num];
			for (int i = 0; i < num2; i++)
			{
				Point point = this._specialPositions[num][i];
				Tile tile = Main.tile[point.X, point.Y];
				if (tile != null && tile.active())
				{
					Texture2D value = TextureAssets.Extra[181].Value;
					int num3 = (int)(tile.frameX / 54);
					int num4 = 3;
					int horizontalFrames = num4 + 9;
					int verticalFrames = 8;
					int frameY = (Main.tileFrameCounter[597] + point.X + point.Y) % 64 / 8;
					Rectangle rectangle = value.Frame(horizontalFrames, verticalFrames, num4 + num3, frameY, 0, 0);
					Rectangle value2 = value.Frame(horizontalFrames, verticalFrames, 2, frameY, 0, 0);
					value.Frame(horizontalFrames, verticalFrames, 0, frameY, 0, 0);
					Vector2 origin = rectangle.Size() / 2f;
					Vector2 value3 = point.ToWorldCoordinates(24f, 64f);
					float num5 = (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f / 5f));
					Vector2 vector = value3 + new Vector2(0f, -40f) + new Vector2(0f, num5 * 4f);
					bool flag = this._rand.Next(4) == 0;
					if (this._isActiveAndNotPaused && flag && this._rand.Next(10) == 0)
					{
						Rectangle dustBox = Utils.CenteredRectangle(vector, rectangle.Size());
						TeleportPylonsSystem.SpawnInWorldDust(num3, dustBox);
					}
					Color color = Lighting.GetColor(point.X, point.Y);
					color = Color.Lerp(color, Color.White, 0.8f);
					Main.spriteBatch.Draw(value, vector - Main.screenPosition, new Rectangle?(rectangle), color * 0.7f, 0f, origin, 1f, SpriteEffects.None, 0f);
					float scale = (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f / 1f)) * 0.2f + 0.8f;
					Color color2 = new Color(255, 255, 255, 0) * 0.1f * scale;
					for (float num6 = 0f; num6 < 1f; num6 += 0.16666667f)
					{
						Main.spriteBatch.Draw(value, vector - Main.screenPosition + (6.2831855f * num6).ToRotationVector2() * (6f + num5 * 2f), new Rectangle?(rectangle), color2, 0f, origin, 1f, SpriteEffects.None, 0f);
					}
					int num7 = 0;
					bool flag2;
					if (Main.InSmartCursorHighlightArea(point.X, point.Y, out flag2))
					{
						num7 = 1;
						if (flag2)
						{
							num7 = 2;
						}
					}
					if (num7 != 0)
					{
						int num8 = (int)((color.R + color.G + color.B) / 3);
						if (num8 > 10)
						{
							Color selectionGlowColor = Colors.GetSelectionGlowColor(num7 == 2, num8);
							Main.spriteBatch.Draw(value, vector - Main.screenPosition, new Rectangle?(value2), selectionGlowColor, 0f, origin, 1f, SpriteEffects.None, 0f);
						}
					}
				}
			}
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x0053EAA8 File Offset: 0x0053CCA8
		private void DrawVoidLenses()
		{
			int num = 8;
			int num2 = this._specialsCount[num];
			this._voidLensData.Clear();
			for (int i = 0; i < num2; i++)
			{
				Point point = this._specialPositions[num][i];
				VoidLensHelper voidLensHelper = new VoidLensHelper(point.ToWorldCoordinates(8f, 8f), 1f);
				if (!Main.gamePaused)
				{
					voidLensHelper.Update();
				}
				int selectionMode = 0;
				bool flag;
				if (Main.InSmartCursorHighlightArea(point.X, point.Y, out flag))
				{
					selectionMode = 1;
					if (flag)
					{
						selectionMode = 2;
					}
				}
				voidLensHelper.DrawToDrawData(this._voidLensData, selectionMode);
			}
			foreach (DrawData drawData in this._voidLensData)
			{
				drawData.Draw(Main.spriteBatch);
			}
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x0053EB90 File Offset: 0x0053CD90
		private void DrawMultiTileGrass()
		{
			Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
			Vector2 zero = Vector2.Zero;
			int num = 4;
			int num2 = this._specialsCount[num];
			for (int i = 0; i < num2; i++)
			{
				Point point = this._specialPositions[num][i];
				int x = point.X;
				int num3 = point.Y;
				int sizeX = 1;
				int num4 = 1;
				Tile tile = Main.tile[x, num3];
				if (tile != null && tile.active())
				{
					ushort type = Main.tile[x, num3].type;
					if (type <= 238)
					{
						if (type <= 233)
						{
							if (type != 27)
							{
								if (type == 233)
								{
									if (Main.tile[x, num3].frameY == 0)
									{
										sizeX = 3;
									}
									else
									{
										sizeX = 2;
									}
									num4 = 2;
								}
							}
							else
							{
								sizeX = 2;
								num4 = 5;
							}
						}
						else if (type == 236 || type == 238)
						{
							num4 = (sizeX = 2);
						}
					}
					else
					{
						if (type > 493)
						{
							switch (type)
							{
							case 519:
								sizeX = 1;
								num4 = this.ClimbCatTail(x, num3);
								num3 -= num4 - 1;
								goto IL_1BF;
							case 520:
							case 528:
							case 529:
								goto IL_1BF;
							case 521:
							case 522:
							case 523:
							case 524:
							case 525:
							case 526:
							case 527:
								goto IL_18F;
							case 530:
								break;
							default:
								if (type != 651)
								{
									if (type != 652)
									{
										goto IL_1BF;
									}
									goto IL_18F;
								}
								break;
							}
							sizeX = 3;
							num4 = 2;
							goto IL_1BF;
						}
						if (type != 485)
						{
							switch (type)
							{
							case 489:
								sizeX = 2;
								num4 = 3;
								goto IL_1BF;
							case 490:
								break;
							case 491:
							case 492:
								goto IL_1BF;
							case 493:
								sizeX = 1;
								num4 = 2;
								goto IL_1BF;
							default:
								goto IL_1BF;
							}
						}
						IL_18F:
						sizeX = 2;
						num4 = 2;
					}
					IL_1BF:
					this.DrawMultiTileGrassInWind(unscaledPosition, zero, x, num3, sizeX, num4);
				}
			}
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x0053ED7C File Offset: 0x0053CF7C
		private int ClimbCatTail(int originx, int originy)
		{
			int num = 0;
			int i = originy;
			while (i > 10)
			{
				Tile tile = Main.tile[originx, i];
				if (!tile.active() || tile.type != 519)
				{
					break;
				}
				if (tile.frameX >= 180)
				{
					num++;
					break;
				}
				i--;
				num++;
			}
			return num;
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x0053EDD4 File Offset: 0x0053CFD4
		private void DrawMultiTileVines()
		{
			Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
			Vector2 zero = Vector2.Zero;
			int num = 5;
			int num2 = this._specialsCount[num];
			for (int i = 0; i < num2; i++)
			{
				Point point = this._specialPositions[num][i];
				int x = point.X;
				int y = point.Y;
				int sizeX = 1;
				int sizeY = 1;
				Tile tile = Main.tile[x, y];
				if (tile != null && tile.active())
				{
					ushort type = Main.tile[x, y].type;
					if (type <= 271)
					{
						if (type <= 91)
						{
							if (type != 34)
							{
								if (type == 42)
								{
									goto IL_13A;
								}
								if (type == 91)
								{
									sizeX = 1;
									sizeY = 3;
								}
							}
							else
							{
								sizeX = 3;
								sizeY = 3;
							}
						}
						else
						{
							if (type == 95 || type == 126)
							{
								goto IL_14A;
							}
							if (type - 270 <= 1)
							{
								goto IL_13A;
							}
						}
					}
					else
					{
						if (type <= 465)
						{
							if (type == 444)
							{
								goto IL_14A;
							}
							if (type == 454)
							{
								sizeX = 4;
								sizeY = 3;
								goto IL_158;
							}
							if (type != 465)
							{
								goto IL_158;
							}
						}
						else if (type <= 581)
						{
							if (type != 572 && type != 581)
							{
								goto IL_158;
							}
							goto IL_13A;
						}
						else if (type - 591 > 1)
						{
							if (type != 660)
							{
								goto IL_158;
							}
							goto IL_13A;
						}
						sizeX = 2;
						sizeY = 3;
					}
					IL_158:
					this.DrawMultiTileVinesInWind(unscaledPosition, zero, x, y, sizeX, sizeY);
					goto IL_168;
					IL_13A:
					sizeX = 1;
					sizeY = 2;
					goto IL_158;
					IL_14A:
					sizeX = 2;
					sizeY = 2;
					goto IL_158;
				}
				IL_168:;
			}
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x0053EF58 File Offset: 0x0053D158
		private void DrawVines()
		{
			Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
			Vector2 zero = Vector2.Zero;
			int num = 6;
			int num2 = this._specialsCount[num];
			for (int i = 0; i < num2; i++)
			{
				Point point = this._specialPositions[num][i];
				int x = point.X;
				int y = point.Y;
				this.DrawVineStrip(unscaledPosition, zero, x, y);
			}
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x0053EFBC File Offset: 0x0053D1BC
		private void DrawReverseVines()
		{
			Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
			Vector2 zero = Vector2.Zero;
			int num = 9;
			int num2 = this._specialsCount[num];
			for (int i = 0; i < num2; i++)
			{
				Point point = this._specialPositions[num][i];
				int x = point.X;
				int y = point.Y;
				this.DrawRisingVineStrip(unscaledPosition, zero, x, y);
			}
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x0053F020 File Offset: 0x0053D220
		private void DrawMultiTileGrassInWind(Vector2 screenPosition, Vector2 offSet, int topLeftX, int topLeftY, int sizeX, int sizeY)
		{
			float windCycle = this.GetWindCycle(topLeftX, topLeftY, this._sunflowerWindCounter);
			new Vector2((float)(sizeX * 16) * 0.5f, (float)(sizeY * 16));
			Vector2 value = new Vector2((float)(topLeftX * 16 - (int)screenPosition.X) + (float)sizeX * 16f * 0.5f, (float)(topLeftY * 16 - (int)screenPosition.Y + 16 * sizeY)) + offSet;
			float num = 0.07f;
			int type = (int)Main.tile[topLeftX, topLeftY].type;
			Texture2D texture2D = null;
			Color color = Color.Transparent;
			bool flag = this.InAPlaceWithWind(topLeftX, topLeftY, sizeX, sizeY);
			if (type != 27)
			{
				if (type != 519)
				{
					if (type - 521 > 6)
					{
						num = 0.15f;
					}
					else
					{
						num = 0f;
						flag = false;
					}
				}
				else
				{
					flag = this.InAPlaceWithWind(topLeftX, topLeftY, sizeX, 1);
				}
			}
			else
			{
				texture2D = TextureAssets.Flames[14].Value;
				color = Color.White;
			}
			for (int i = topLeftX; i < topLeftX + sizeX; i++)
			{
				for (int j = topLeftY; j < topLeftY + sizeY; j++)
				{
					Tile tile = Main.tile[i, j];
					ushort type2 = tile.type;
					if ((int)type2 == type && this.IsVisible(tile))
					{
						Math.Abs(((float)(i - topLeftX) + 0.5f) / (float)sizeX - 0.5f);
						short frameX = tile.frameX;
						short frameY = tile.frameY;
						float num2 = 1f - (float)(j - topLeftY + 1) / (float)sizeY;
						if (num2 == 0f)
						{
							num2 = 0.1f;
						}
						if (!flag)
						{
							num2 = 0f;
						}
						int width;
						int num3;
						int num4;
						int num5;
						int num6;
						int num7;
						SpriteEffects spriteEffects;
						Texture2D texture2D2;
						Rectangle rectangle;
						Color color2;
						this.GetTileDrawData(i, j, tile, type2, ref frameX, ref frameY, out width, out num3, out num4, out num5, out num6, out num7, out spriteEffects, out texture2D2, out rectangle, out color2);
						bool flag2 = this._rand.Next(4) == 0;
						Color color3 = Lighting.GetColor(i, j);
						this.DrawAnimatedTile_AdjustForVisionChangers(i, j, tile, type2, frameX, frameY, ref color3, flag2);
						color3 = this.DrawTiles_GetLightOverride(j, i, tile, type2, frameX, frameY, color3);
						if (this._isActiveAndNotPaused && flag2)
						{
							this.DrawTiles_EmitParticles(j, i, tile, type2, frameX, frameY, color3);
						}
						Vector2 value2 = new Vector2((float)(i * 16 - (int)screenPosition.X), (float)(j * 16 - (int)screenPosition.Y + num4)) + offSet;
						if (tile.type == 493 && tile.frameY == 0)
						{
							if (Main.WindForVisuals >= 0f)
							{
								spriteEffects ^= SpriteEffects.FlipHorizontally;
							}
							if (!spriteEffects.HasFlag(SpriteEffects.FlipHorizontally))
							{
								value2.X -= 6f;
							}
							else
							{
								value2.X += 6f;
							}
						}
						Vector2 vector = new Vector2(windCycle * 1f, Math.Abs(windCycle) * 2f * num2);
						Vector2 origin = value - value2;
						Texture2D tileDrawTexture = this.GetTileDrawTexture(tile, i, j);
						if (tileDrawTexture != null)
						{
							Main.spriteBatch.Draw(tileDrawTexture, value + new Vector2(0f, vector.Y), new Rectangle?(new Rectangle((int)frameX + num6, (int)frameY + num7, width, num3 - num5)), color3, windCycle * num * num2, origin, 1f, spriteEffects, 0f);
							if (texture2D != null)
							{
								Main.spriteBatch.Draw(texture2D, value + new Vector2(0f, vector.Y), new Rectangle?(new Rectangle((int)frameX + num6, (int)frameY + num7, width, num3 - num5)), color, windCycle * num * num2, origin, 1f, spriteEffects, 0f);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x0053F3D4 File Offset: 0x0053D5D4
		private void DrawVineStrip(Vector2 screenPosition, Vector2 offSet, int x, int startY)
		{
			int num = 0;
			int num2 = 0;
			Vector2 vector = new Vector2((float)(x * 16 + 8), (float)(startY * 16 - 2));
			float num3 = Math.Abs(Main.WindForVisuals) / 1.2f;
			num3 = MathHelper.Lerp(0.2f, 1f, num3);
			float num4 = -0.08f * num3;
			float windCycle = this.GetWindCycle(x, startY, this._vineWindCounter);
			float num5 = 0f;
			float num6 = 0f;
			for (int i = startY; i < Main.maxTilesY - 10; i++)
			{
				Tile tile = Main.tile[x, i];
				if (tile != null)
				{
					ushort type = tile.type;
					if (!tile.active() || !TileID.Sets.VineThreads[(int)type])
					{
						break;
					}
					num++;
					if (num2 >= 5)
					{
						num4 += 0.0075f * num3;
					}
					if (num2 >= 2)
					{
						num4 += 0.0025f;
					}
					if (Main.remixWorld)
					{
						if (WallID.Sets.AllowsWind[(int)tile.wall] && (double)i > Main.worldSurface)
						{
							num2++;
						}
					}
					else if (WallID.Sets.AllowsWind[(int)tile.wall] && (double)i < Main.worldSurface)
					{
						num2++;
					}
					float windGridPush = this.GetWindGridPush(x, i, 20, 0.01f);
					if (windGridPush == 0f && num6 != 0f)
					{
						num5 *= -0.78f;
					}
					else
					{
						num5 -= windGridPush;
					}
					num6 = windGridPush;
					short frameX = tile.frameX;
					short frameY = tile.frameY;
					Color color = Lighting.GetColor(x, i);
					int num7;
					int num8;
					int num9;
					int num10;
					int num11;
					int num12;
					SpriteEffects effects;
					Texture2D texture2D;
					Rectangle value;
					Color color2;
					this.GetTileDrawData(x, i, tile, type, ref frameX, ref frameY, out num7, out num8, out num9, out num10, out num11, out num12, out effects, out texture2D, out value, out color2);
					Vector2 position = new Vector2((float)(-(float)((int)screenPosition.X)), (float)(-(float)((int)screenPosition.Y))) + offSet + vector;
					if (tile.fullbrightBlock())
					{
						color = Color.White;
					}
					float num13 = (float)num2 * num4 * windCycle + num5;
					if (this._localPlayer.biomeSight)
					{
						Color white = Color.White;
						if (Main.IsTileBiomeSightable(type, frameX, frameY, ref white))
						{
							if (color.R < white.R)
							{
								color.R = white.R;
							}
							if (color.G < white.G)
							{
								color.G = white.G;
							}
							if (color.B < white.B)
							{
								color.B = white.B;
							}
							if (this._isActiveAndNotPaused && this._rand.Next(480) == 0)
							{
								Color newColor = white;
								int num14 = Dust.NewDust(new Vector2((float)(x * 16), (float)(i * 16)), 16, 16, 267, 0f, 0f, 150, newColor, 0.3f);
								this._dust[num14].noGravity = true;
								this._dust[num14].fadeIn = 1f;
								this._dust[num14].velocity *= 0.1f;
								this._dust[num14].noLightEmittence = true;
							}
						}
					}
					Texture2D tileDrawTexture = this.GetTileDrawTexture(tile, x, i);
					if (tileDrawTexture == null)
					{
						return;
					}
					if (this.IsVisible(tile))
					{
						Main.spriteBatch.Draw(tileDrawTexture, position, new Rectangle?(new Rectangle((int)frameX + num11, (int)frameY + num12, num7, num8 - num10)), color, num13, new Vector2((float)(num7 / 2), (float)(num10 - num9)), 1f, effects, 0f);
						if (texture2D != null)
						{
							Main.spriteBatch.Draw(texture2D, position, new Rectangle?(value), color2, num13, new Vector2((float)(num7 / 2), (float)(num10 - num9)), 1f, effects, 0f);
						}
					}
					vector += (num13 + 1.5707964f).ToRotationVector2() * 16f;
				}
			}
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x0053F7AC File Offset: 0x0053D9AC
		private void DrawRisingVineStrip(Vector2 screenPosition, Vector2 offSet, int x, int startY)
		{
			int num = 0;
			int num2 = 0;
			Vector2 vector = new Vector2((float)(x * 16 + 8), (float)(startY * 16 + 16 + 2));
			float num3 = Math.Abs(Main.WindForVisuals) / 1.2f;
			num3 = MathHelper.Lerp(0.2f, 1f, num3);
			float num4 = -0.08f * num3;
			float windCycle = this.GetWindCycle(x, startY, this._vineWindCounter);
			float num5 = 0f;
			float num6 = 0f;
			for (int i = startY; i > 10; i--)
			{
				Tile tile = Main.tile[x, i];
				if (tile != null)
				{
					ushort type = tile.type;
					if (!tile.active() || !TileID.Sets.ReverseVineThreads[(int)type])
					{
						break;
					}
					num++;
					if (num2 >= 5)
					{
						num4 += 0.0075f * num3;
					}
					if (num2 >= 2)
					{
						num4 += 0.0025f;
					}
					if (WallID.Sets.AllowsWind[(int)tile.wall] && (double)i < Main.worldSurface)
					{
						num2++;
					}
					float windGridPush = this.GetWindGridPush(x, i, 40, -0.004f);
					if (windGridPush == 0f && num6 != 0f)
					{
						num5 *= -0.78f;
					}
					else
					{
						num5 -= windGridPush;
					}
					num6 = windGridPush;
					short frameX = tile.frameX;
					short frameY = tile.frameY;
					Color color = Lighting.GetColor(x, i);
					int num7;
					int num8;
					int num9;
					int num10;
					int num11;
					int num12;
					SpriteEffects effects;
					Texture2D texture2D;
					Rectangle rectangle;
					Color color2;
					this.GetTileDrawData(x, i, tile, type, ref frameX, ref frameY, out num7, out num8, out num9, out num10, out num11, out num12, out effects, out texture2D, out rectangle, out color2);
					Vector2 position = new Vector2((float)(-(float)((int)screenPosition.X)), (float)(-(float)((int)screenPosition.Y))) + offSet + vector;
					float num13 = (float)num2 * -num4 * windCycle + num5;
					Texture2D tileDrawTexture = this.GetTileDrawTexture(tile, x, i);
					if (tileDrawTexture == null)
					{
						return;
					}
					if (this.IsVisible(tile))
					{
						Main.spriteBatch.Draw(tileDrawTexture, position, new Rectangle?(new Rectangle((int)frameX + num11, (int)frameY + num12, num7, num8 - num10)), color, num13, new Vector2((float)(num7 / 2), (float)(num10 - num9 + num8)), 1f, effects, 0f);
					}
					vector += (num13 - 1.5707964f).ToRotationVector2() * 16f;
				}
			}
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x0053F9E4 File Offset: 0x0053DBE4
		private float GetAverageWindGridPush(int topLeftX, int topLeftY, int sizeX, int sizeY, int totalPushTime, float pushForcePerFrame)
		{
			float num = 0f;
			int num2 = 0;
			for (int i = 0; i < sizeX; i++)
			{
				for (int j = 0; j < sizeY; j++)
				{
					float windGridPush = this.GetWindGridPush(topLeftX + i, topLeftY + j, totalPushTime, pushForcePerFrame);
					if (windGridPush != 0f)
					{
						num += windGridPush;
						num2++;
					}
				}
			}
			if (num2 == 0)
			{
				return 0f;
			}
			return num / (float)num2;
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x0053FA44 File Offset: 0x0053DC44
		private float GetHighestWindGridPushComplex(int topLeftX, int topLeftY, int sizeX, int sizeY, int totalPushTime, float pushForcePerFrame, int loops, bool swapLoopDir)
		{
			float result = 0f;
			int num = int.MaxValue;
			for (int i = 0; i < 1; i++)
			{
				for (int j = 0; j < sizeY; j++)
				{
					int num2;
					int num3;
					int num4;
					this._windGrid.GetWindTime(topLeftX + i + sizeX / 2, topLeftY + j, totalPushTime, out num2, out num3, out num4);
					float windGridPushComplex = this.GetWindGridPushComplex(topLeftX + i, topLeftY + j, totalPushTime, pushForcePerFrame, loops, swapLoopDir);
					if (num2 < num && num2 != 0)
					{
						result = windGridPushComplex;
						num = num2;
					}
				}
			}
			return result;
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x0053FABC File Offset: 0x0053DCBC
		private void DrawMultiTileVinesInWind(Vector2 screenPosition, Vector2 offSet, int topLeftX, int topLeftY, int sizeX, int sizeY)
		{
			float num = this.GetWindCycle(topLeftX, topLeftY, this._sunflowerWindCounter);
			float num2 = num;
			int totalPushTime = 60;
			float pushForcePerFrame = 1.26f;
			float highestWindGridPushComplex = this.GetHighestWindGridPushComplex(topLeftX, topLeftY, sizeX, sizeY, totalPushTime, pushForcePerFrame, 3, true);
			num += highestWindGridPushComplex;
			new Vector2((float)(sizeX * 16) * 0.5f, 0f);
			Vector2 value = new Vector2((float)(topLeftX * 16 - (int)screenPosition.X) + (float)sizeX * 16f * 0.5f, (float)(topLeftY * 16 - (int)screenPosition.Y)) + offSet;
			Tile tile = Main.tile[topLeftX, topLeftY];
			int type = (int)tile.type;
			Vector2 value2 = new Vector2(0f, -2f);
			value += value2;
			bool flag;
			if (type == 465 || type - 591 <= 1)
			{
				flag = (WorldGen.IsBelowANonHammeredPlatform(topLeftX, topLeftY) && WorldGen.IsBelowANonHammeredPlatform(topLeftX + 1, topLeftY));
			}
			else
			{
				flag = (sizeX == 1 && WorldGen.IsBelowANonHammeredPlatform(topLeftX, topLeftY));
			}
			if (flag)
			{
				value.Y -= 8f;
				value2.Y -= 8f;
			}
			Texture2D texture2D = null;
			Color transparent = Color.Transparent;
			float? num3 = null;
			float num4 = 1f;
			float num5 = -4f;
			bool flag2 = false;
			float num6 = 0.15f;
			if (type <= 444)
			{
				int num7;
				if (type <= 95)
				{
					if (type != 34)
					{
						if (type != 42)
						{
							if (type != 95)
							{
								goto IL_61C;
							}
							goto IL_5C3;
						}
						else
						{
							num3 = new float?((float)1);
							num5 = 0f;
							num7 = (int)(tile.frameY / 36);
							if (num7 == 0)
							{
								num3 = null;
								num5 = -1f;
								goto IL_61C;
							}
							switch (num7)
							{
							case 9:
								num3 = new float?(0f);
								goto IL_61C;
							case 10:
							case 11:
							case 13:
							case 15:
							case 16:
							case 17:
							case 18:
							case 19:
							case 20:
							case 21:
							case 22:
							case 23:
							case 24:
							case 25:
							case 26:
							case 27:
							case 29:
							case 31:
							case 36:
							case 37:
								goto IL_61C;
							case 12:
								num3 = null;
								num5 = -1f;
								goto IL_61C;
							case 14:
								num3 = null;
								num5 = -1f;
								goto IL_61C;
							case 28:
								num3 = null;
								num5 = -1f;
								goto IL_61C;
							case 30:
								num3 = new float?(0f);
								goto IL_61C;
							case 32:
								num3 = new float?(0f);
								goto IL_61C;
							case 33:
								num3 = new float?(0f);
								goto IL_61C;
							case 34:
								num3 = null;
								num5 = -1f;
								goto IL_61C;
							case 35:
								num3 = new float?(0f);
								goto IL_61C;
							case 38:
								num3 = null;
								num5 = -1f;
								goto IL_61C;
							case 39:
								num3 = null;
								num5 = -1f;
								flag2 = true;
								goto IL_61C;
							case 40:
							case 41:
							case 42:
							case 43:
								num3 = new float?(0f);
								num3 = null;
								num5 = -1f;
								flag2 = true;
								goto IL_61C;
							default:
								goto IL_61C;
							}
						}
					}
				}
				else if (type != 126)
				{
					if (type - 270 > 1 && type != 444)
					{
						goto IL_61C;
					}
					goto IL_5C3;
				}
				num3 = new float?((float)1);
				num5 = 0f;
				num7 = (int)(tile.frameY / 54 + tile.frameX / 108 * 37);
				switch (num7)
				{
				case 9:
					num3 = null;
					num5 = -1f;
					flag2 = true;
					num6 *= 0.3f;
					goto IL_61C;
				case 10:
					goto IL_61C;
				case 11:
					num6 *= 0.5f;
					goto IL_61C;
				case 12:
					num3 = null;
					num5 = -1f;
					goto IL_61C;
				default:
					switch (num7)
					{
					case 18:
						num3 = null;
						num5 = -1f;
						goto IL_61C;
					case 19:
					case 20:
					case 22:
					case 24:
					case 26:
					case 27:
					case 28:
					case 29:
					case 30:
					case 31:
					case 34:
					case 38:
						goto IL_61C;
					case 21:
						num3 = null;
						num5 = -1f;
						goto IL_61C;
					case 23:
						num3 = new float?(0f);
						goto IL_61C;
					case 25:
						num3 = null;
						num5 = -1f;
						flag2 = true;
						goto IL_61C;
					case 32:
						num6 *= 0.5f;
						goto IL_61C;
					case 33:
						num6 *= 0.5f;
						goto IL_61C;
					case 35:
						num3 = new float?(0f);
						goto IL_61C;
					case 36:
						num3 = null;
						num5 = -1f;
						flag2 = true;
						goto IL_61C;
					case 37:
						num3 = null;
						num5 = -1f;
						flag2 = true;
						num6 *= 0.5f;
						goto IL_61C;
					case 39:
						num3 = null;
						num5 = -1f;
						flag2 = true;
						goto IL_61C;
					case 40:
					case 41:
					case 42:
					case 43:
						num3 = null;
						num5 = -2f;
						flag2 = true;
						num6 *= 0.5f;
						goto IL_61C;
					case 44:
						num3 = null;
						num5 = -3f;
						goto IL_61C;
					default:
						goto IL_61C;
					}
					break;
				}
			}
			else if (type <= 581)
			{
				if (type != 454 && type != 572 && type != 581)
				{
					goto IL_61C;
				}
			}
			else
			{
				if (type == 591)
				{
					num4 = 0.5f;
					num5 = -2f;
					goto IL_61C;
				}
				if (type == 592)
				{
					num4 = 0.5f;
					num5 = -2f;
					texture2D = TextureAssets.GlowMask[294].Value;
					transparent = new Color(255, 255, 255, 0);
					goto IL_61C;
				}
				if (type != 660)
				{
					goto IL_61C;
				}
			}
			IL_5C3:
			num3 = new float?((float)1);
			num5 = 0f;
			IL_61C:
			if (flag2)
			{
				value += new Vector2(0f, 16f);
			}
			num6 *= -1f;
			if (!this.InAPlaceWithWind(topLeftX, topLeftY, sizeX, sizeY))
			{
				num -= num2;
			}
			ulong num8 = 0UL;
			for (int i = topLeftX; i < topLeftX + sizeX; i++)
			{
				for (int j = topLeftY; j < topLeftY + sizeY; j++)
				{
					Tile tile2 = Main.tile[i, j];
					ushort type2 = tile2.type;
					if ((int)type2 == type && this.IsVisible(tile2))
					{
						Math.Abs(((float)(i - topLeftX) + 0.5f) / (float)sizeX - 0.5f);
						short frameX = tile2.frameX;
						short frameY = tile2.frameY;
						float num9 = (float)(j - topLeftY + 1) / (float)sizeY;
						if (num9 == 0f)
						{
							num9 = 0.1f;
						}
						if (num3 != null)
						{
							num9 = num3.Value;
						}
						if (flag2 && j == topLeftY)
						{
							num9 = 0f;
						}
						int width;
						int num10;
						int num11;
						int num12;
						int num13;
						int num14;
						SpriteEffects effects;
						Texture2D texture2D2;
						Rectangle rectangle;
						Color color;
						this.GetTileDrawData(i, j, tile2, type2, ref frameX, ref frameY, out width, out num10, out num11, out num12, out num13, out num14, out effects, out texture2D2, out rectangle, out color);
						bool flag3 = this._rand.Next(4) == 0;
						Color color2 = Lighting.GetColor(i, j);
						this.DrawAnimatedTile_AdjustForVisionChangers(i, j, tile2, type2, frameX, frameY, ref color2, flag3);
						color2 = this.DrawTiles_GetLightOverride(j, i, tile2, type2, frameX, frameY, color2);
						if (this._isActiveAndNotPaused && flag3)
						{
							this.DrawTiles_EmitParticles(j, i, tile2, type2, frameX, frameY, color2);
						}
						Vector2 vector = new Vector2((float)(i * 16 - (int)screenPosition.X), (float)(j * 16 - (int)screenPosition.Y + num11)) + offSet;
						vector += value2;
						Vector2 vector2 = new Vector2(num * num4, Math.Abs(num) * num5 * num9);
						Vector2 vector3 = value - vector;
						Texture2D tileDrawTexture = this.GetTileDrawTexture(tile2, i, j);
						if (tileDrawTexture != null)
						{
							Vector2 vector4 = value + new Vector2(0f, vector2.Y);
							Rectangle rectangle2 = new Rectangle((int)frameX + num13, (int)frameY + num14, width, num10 - num12);
							float rotation = num * num6 * num9;
							if (type2 == 660 && j == topLeftY + sizeY - 1)
							{
								Texture2D value3 = TextureAssets.Extra[260].Value;
								float num15 = ((float)((i + j) % 200) * 0.11f + (float)Main.timeForVisualEffects / 360f) % 1f;
								Color white = Color.White;
								Main.spriteBatch.Draw(value3, vector4, new Rectangle?(rectangle2), white, rotation, vector3, 1f, effects, 0f);
							}
							Main.spriteBatch.Draw(tileDrawTexture, vector4, new Rectangle?(rectangle2), color2, rotation, vector3, 1f, effects, 0f);
							if (type2 == 660 && j == topLeftY + sizeY - 1)
							{
								Texture2D value4 = TextureAssets.Extra[260].Value;
								Color color3 = Main.hslToRgb(((float)((i + j) % 200) * 0.11f + (float)Main.timeForVisualEffects / 360f) % 1f, 1f, 0.8f, byte.MaxValue);
								color3.A = 127;
								Rectangle value5 = rectangle2;
								Vector2 position = vector4;
								Vector2 origin = vector3;
								Main.spriteBatch.Draw(value4, position, new Rectangle?(value5), color3, rotation, origin, 1f, effects, 0f);
							}
							if (texture2D != null)
							{
								Main.spriteBatch.Draw(texture2D, vector4, new Rectangle?(rectangle2), transparent, rotation, vector3, 1f, effects, 0f);
							}
							TileDrawing.TileFlameData tileFlameData = this.GetTileFlameData(i, j, (int)type2, (int)frameY);
							if (num8 == 0UL)
							{
								num8 = tileFlameData.flameSeed;
							}
							tileFlameData.flameSeed = num8;
							for (int k = 0; k < tileFlameData.flameCount; k++)
							{
								float x = (float)Utils.RandomInt(ref tileFlameData.flameSeed, tileFlameData.flameRangeXMin, tileFlameData.flameRangeXMax) * tileFlameData.flameRangeMultX;
								float y = (float)Utils.RandomInt(ref tileFlameData.flameSeed, tileFlameData.flameRangeYMin, tileFlameData.flameRangeYMax) * tileFlameData.flameRangeMultY;
								Main.spriteBatch.Draw(tileFlameData.flameTexture, vector4 + new Vector2(x, y), new Rectangle?(rectangle2), tileFlameData.flameColor, rotation, vector3, 1f, effects, 0f);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x00540564 File Offset: 0x0053E764
		private void EmitAlchemyHerbParticles(int j, int i, int style)
		{
			if (style == 0 && this._rand.Next(100) == 0)
			{
				int num = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16 - 4)), 16, 16, 19, 0f, 0f, 160, default(Color), 0.1f);
				Dust dust = this._dust[num];
				dust.velocity.X = dust.velocity.X / 2f;
				Dust dust2 = this._dust[num];
				dust2.velocity.Y = dust2.velocity.Y / 2f;
				this._dust[num].noGravity = true;
				this._dust[num].fadeIn = 1f;
			}
			if (style == 1 && this._rand.Next(100) == 0)
			{
				Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 41, 0f, 0f, 250, default(Color), 0.8f);
			}
			if (style == 3)
			{
				if (this._rand.Next(200) == 0)
				{
					int num2 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 14, 0f, 0f, 100, default(Color), 0.2f);
					this._dust[num2].fadeIn = 1.2f;
				}
				if (this._rand.Next(75) == 0)
				{
					int num3 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 27, 0f, 0f, 100, default(Color), 1f);
					Dust dust3 = this._dust[num3];
					dust3.velocity.X = dust3.velocity.X / 2f;
					Dust dust4 = this._dust[num3];
					dust4.velocity.Y = dust4.velocity.Y / 2f;
				}
			}
			if (style == 4 && this._rand.Next(150) == 0)
			{
				int num4 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 8, 16, 0f, 0f, 0, default(Color), 1f);
				Dust dust5 = this._dust[num4];
				dust5.velocity.X = dust5.velocity.X / 3f;
				Dust dust6 = this._dust[num4];
				dust6.velocity.Y = dust6.velocity.Y / 3f;
				Dust dust7 = this._dust[num4];
				dust7.velocity.Y = dust7.velocity.Y - 0.7f;
				this._dust[num4].alpha = 50;
				this._dust[num4].scale *= 0.1f;
				this._dust[num4].fadeIn = 0.9f;
				this._dust[num4].noGravity = true;
			}
			if (style == 5 && this._rand.Next(40) == 0)
			{
				int num5 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16 - 6)), 16, 16, 6, 0f, 0f, 0, default(Color), 1.5f);
				Dust dust8 = this._dust[num5];
				dust8.velocity.Y = dust8.velocity.Y - 2f;
				this._dust[num5].noGravity = true;
			}
			if (style == 6 && this._rand.Next(30) == 0)
			{
				Color newColor = new Color(50, 255, 255, 255);
				int num6 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 43, 0f, 0f, 254, newColor, 0.5f);
				this._dust[num6].velocity *= 0f;
			}
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x00540920 File Offset: 0x0053EB20
		private bool IsAlchemyPlantHarvestable(int style)
		{
			return (style == 0 && Main.dayTime) || (style == 1 && !Main.dayTime) || (style == 3 && !Main.dayTime && (Main.bloodMoon || Main.moonPhase == 0)) || (style == 4 && (Main.raining || Main.cloudAlpha > 0f)) || (style == 5 && !Main.raining && Main.time > 40500.0);
		}

		// Token: 0x0400477C RID: 18300
		private const int MAX_SPECIALS = 9000;

		// Token: 0x0400477D RID: 18301
		private const int MAX_SPECIALS_LEGACY = 1000;

		// Token: 0x0400477E RID: 18302
		private const float FORCE_FOR_MIN_WIND = 0.08f;

		// Token: 0x0400477F RID: 18303
		private const float FORCE_FOR_MAX_WIND = 1.2f;

		// Token: 0x04004780 RID: 18304
		private int _leafFrequency = 100000;

		// Token: 0x04004781 RID: 18305
		private int[] _specialsCount = new int[13];

		// Token: 0x04004782 RID: 18306
		private Point[][] _specialPositions = new Point[13][];

		// Token: 0x04004783 RID: 18307
		private Dictionary<Point, int> _displayDollTileEntityPositions = new Dictionary<Point, int>();

		// Token: 0x04004784 RID: 18308
		private Dictionary<Point, int> _hatRackTileEntityPositions = new Dictionary<Point, int>();

		// Token: 0x04004785 RID: 18309
		private Dictionary<Point, int> _trainingDummyTileEntityPositions = new Dictionary<Point, int>();

		// Token: 0x04004786 RID: 18310
		private Dictionary<Point, int> _itemFrameTileEntityPositions = new Dictionary<Point, int>();

		// Token: 0x04004787 RID: 18311
		private Dictionary<Point, int> _foodPlatterTileEntityPositions = new Dictionary<Point, int>();

		// Token: 0x04004788 RID: 18312
		private Dictionary<Point, int> _weaponRackTileEntityPositions = new Dictionary<Point, int>();

		// Token: 0x04004789 RID: 18313
		private Dictionary<Point, int> _chestPositions = new Dictionary<Point, int>();

		// Token: 0x0400478A RID: 18314
		private int _specialTilesCount;

		// Token: 0x0400478B RID: 18315
		private int[] _specialTileX = new int[1000];

		// Token: 0x0400478C RID: 18316
		private int[] _specialTileY = new int[1000];

		// Token: 0x0400478D RID: 18317
		private UnifiedRandom _rand;

		// Token: 0x0400478E RID: 18318
		private double _treeWindCounter;

		// Token: 0x0400478F RID: 18319
		private double _grassWindCounter;

		// Token: 0x04004790 RID: 18320
		private double _sunflowerWindCounter;

		// Token: 0x04004791 RID: 18321
		private double _vineWindCounter;

		// Token: 0x04004792 RID: 18322
		private WindGrid _windGrid = new WindGrid();

		// Token: 0x04004793 RID: 18323
		private bool _shouldShowInvisibleBlocks;

		// Token: 0x04004794 RID: 18324
		private bool _shouldShowInvisibleBlocks_LastFrame;

		// Token: 0x04004795 RID: 18325
		private List<Point> _vineRootsPositions = new List<Point>();

		// Token: 0x04004796 RID: 18326
		private List<Point> _reverseVineRootsPositions = new List<Point>();

		// Token: 0x04004797 RID: 18327
		private TilePaintSystemV2 _paintSystem;

		// Token: 0x04004798 RID: 18328
		private Color _martianGlow = new Color(0, 0, 0, 0);

		// Token: 0x04004799 RID: 18329
		private Color _meteorGlow = new Color(100, 100, 100, 0);

		// Token: 0x0400479A RID: 18330
		private Color _lavaMossGlow = new Color(150, 100, 50, 0);

		// Token: 0x0400479B RID: 18331
		private Color _kryptonMossGlow = new Color(0, 200, 0, 0);

		// Token: 0x0400479C RID: 18332
		private Color _xenonMossGlow = new Color(0, 180, 250, 0);

		// Token: 0x0400479D RID: 18333
		private Color _argonMossGlow = new Color(225, 0, 125, 0);

		// Token: 0x0400479E RID: 18334
		private Color _violetMossGlow = new Color(150, 0, 250, 0);

		// Token: 0x0400479F RID: 18335
		private bool _isActiveAndNotPaused;

		// Token: 0x040047A0 RID: 18336
		private Player _localPlayer = new Player();

		// Token: 0x040047A1 RID: 18337
		private Color _highQualityLightingRequirement;

		// Token: 0x040047A2 RID: 18338
		private Color _mediumQualityLightingRequirement;

		// Token: 0x040047A3 RID: 18339
		private static readonly Vector2 _zero;

		// Token: 0x040047A4 RID: 18340
		private ThreadLocal<TileDrawInfo> _currentTileDrawInfo = new ThreadLocal<TileDrawInfo>(() => new TileDrawInfo());

		// Token: 0x040047A5 RID: 18341
		private TileDrawInfo _currentTileDrawInfoNonThreaded = new TileDrawInfo();

		// Token: 0x040047A6 RID: 18342
		private Vector3[] _glowPaintColorSlices = new Vector3[]
		{
			Vector3.One,
			Vector3.One,
			Vector3.One,
			Vector3.One,
			Vector3.One,
			Vector3.One,
			Vector3.One,
			Vector3.One,
			Vector3.One
		};

		// Token: 0x040047A7 RID: 18343
		private List<DrawData> _voidLensData = new List<DrawData>();

		// Token: 0x02000692 RID: 1682
		private enum TileCounterType
		{
			// Token: 0x04006199 RID: 24985
			Tree,
			// Token: 0x0400619A RID: 24986
			DisplayDoll,
			// Token: 0x0400619B RID: 24987
			HatRack,
			// Token: 0x0400619C RID: 24988
			WindyGrass,
			// Token: 0x0400619D RID: 24989
			MultiTileGrass,
			// Token: 0x0400619E RID: 24990
			MultiTileVine,
			// Token: 0x0400619F RID: 24991
			Vine,
			// Token: 0x040061A0 RID: 24992
			BiomeGrass,
			// Token: 0x040061A1 RID: 24993
			VoidLens,
			// Token: 0x040061A2 RID: 24994
			ReverseVine,
			// Token: 0x040061A3 RID: 24995
			TeleportationPylon,
			// Token: 0x040061A4 RID: 24996
			MasterTrophy,
			// Token: 0x040061A5 RID: 24997
			AnyDirectionalGrass,
			// Token: 0x040061A6 RID: 24998
			Count
		}

		// Token: 0x02000693 RID: 1683
		private struct TileFlameData
		{
			// Token: 0x040061A7 RID: 24999
			public Texture2D flameTexture;

			// Token: 0x040061A8 RID: 25000
			public ulong flameSeed;

			// Token: 0x040061A9 RID: 25001
			public int flameCount;

			// Token: 0x040061AA RID: 25002
			public Color flameColor;

			// Token: 0x040061AB RID: 25003
			public int flameRangeXMin;

			// Token: 0x040061AC RID: 25004
			public int flameRangeXMax;

			// Token: 0x040061AD RID: 25005
			public int flameRangeYMin;

			// Token: 0x040061AE RID: 25006
			public int flameRangeYMax;

			// Token: 0x040061AF RID: 25007
			public float flameRangeMultX;

			// Token: 0x040061B0 RID: 25008
			public float flameRangeMultY;
		}
	}
}
