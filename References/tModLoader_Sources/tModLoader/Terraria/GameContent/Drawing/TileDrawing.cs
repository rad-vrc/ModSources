using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.UI;
using Terraria.Utilities;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x0200063C RID: 1596
	public class TileDrawing
	{
		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060045D9 RID: 17881 RVA: 0x0061A1F3 File Offset: 0x006183F3
		private bool[] _tileSolid
		{
			get
			{
				return Main.tileSolid;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060045DA RID: 17882 RVA: 0x0061A1FA File Offset: 0x006183FA
		private bool[] _tileSolidTop
		{
			get
			{
				return Main.tileSolidTop;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060045DB RID: 17883 RVA: 0x0061A201 File Offset: 0x00618401
		private Dust[] _dust
		{
			get
			{
				return Main.dust;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060045DC RID: 17884 RVA: 0x0061A208 File Offset: 0x00618408
		private Gore[] _gore
		{
			get
			{
				return Main.gore;
			}
		}

		/// <summary>
		/// Registers a tile coordinate to use custom drawing code corresponding to the <see cref="T:Terraria.GameContent.Drawing.TileDrawing.TileCounterType" /> provided. This is mostly used to apply wind and player interaction effects to tiles.
		/// <para /> For multitiles, make sure to only call this for the top left corner of the multitile to prevent duplicate draws by checking <see cref="M:Terraria.ObjectData.TileObjectData.IsTopLeft(System.Int32,System.Int32)" /> first. This should be called in <c>ModTile.PreDraw</c> (or <c>GlobalTile.PreDraw</c>) and false should be returned to prevent the default tile drawing. It can also be called in <see cref="M:Terraria.ModLoader.ModTile.DrawEffects(System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteBatch,Terraria.DataStructures.TileDrawInfo@)" /> as well.
		/// <para /> When <see cref="F:Terraria.GameContent.Drawing.TileDrawing.TileCounterType.CustomNonSolid" /> or <see cref="F:Terraria.GameContent.Drawing.TileDrawing.TileCounterType.CustomSolid" /> is used, <see cref="M:Terraria.ModLoader.ModTile.SpecialDraw(System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteBatch)" /> will be called to allow custom rendering. Compared to using <see cref="M:Terraria.GameContent.Drawing.TileDrawing.AddSpecialLegacyPoint(System.Int32,System.Int32)" />, SpecialDraw will be called at 60fps rather than 15fps, the sampler state will default to PointClamp instead of LinearClamp, and adjusting for <see cref="F:Terraria.Main.offScreenRange" /> is no longer necessary because the render target is the screen rather than the tile render targets.
		/// </summary>
		// Token: 0x060045DD RID: 17885 RVA: 0x0061A210 File Offset: 0x00618410
		public void AddSpecialPoint(int x, int y, TileDrawing.TileCounterType type)
		{
			Point[] array = this._specialPositions[(int)type];
			int[] specialsCount = this._specialsCount;
			int num = specialsCount[(int)type];
			specialsCount[(int)type] = num + 1;
			array[num] = new Point(x, y);
		}

		// Token: 0x060045DE RID: 17886 RVA: 0x0061A248 File Offset: 0x00618448
		public TileDrawing(TilePaintSystemV2 paintSystem)
		{
			this._paintSystem = paintSystem;
			this._rand = new UnifiedRandom();
			for (int i = 0; i < this._specialPositions.Length; i++)
			{
				this._specialPositions[i] = new Point[9000];
			}
		}

		// Token: 0x060045DF RID: 17887 RVA: 0x0061A494 File Offset: 0x00618694
		public void PreparePaintForTilesOnScreen()
		{
			if (Main.GameUpdateCount % 6U == 0U)
			{
				Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
				Vector2 vector;
				vector..ctor((float)Main.offScreenRange, (float)Main.offScreenRange);
				if (Main.drawToScreen)
				{
					vector = Vector2.Zero;
				}
				int firstTileX;
				int lastTileX;
				int firstTileY;
				int lastTileY;
				this.GetScreenDrawArea(unscaledPosition, vector + (Main.Camera.UnscaledPosition - Main.Camera.ScaledPosition), out firstTileX, out lastTileX, out firstTileY, out lastTileY);
				this.PrepareForAreaDrawing(firstTileX, lastTileX, firstTileY, lastTileY, true);
			}
		}

		// Token: 0x060045E0 RID: 17888 RVA: 0x0061A510 File Offset: 0x00618710
		public unsafe void PrepareForAreaDrawing(int firstTileX, int lastTileX, int firstTileY, int lastTileY, bool prepareLazily)
		{
			TilePaintSystemV2.TileVariationkey lookupKey = default(TilePaintSystemV2.TileVariationkey);
			TilePaintSystemV2.WallVariationKey lookupKey2 = default(TilePaintSystemV2.WallVariationKey);
			for (int i = firstTileY; i < lastTileY + 4; i++)
			{
				for (int j = firstTileX - 2; j < lastTileX + 2; j++)
				{
					Tile tile = Main.tile[j, i];
					if (!(tile == null))
					{
						int tileStyle = 0;
						if (tile.active())
						{
							Main.instance.LoadTiles((int)(*tile.type));
							lookupKey.TileType = (int)(*tile.type);
							lookupKey.PaintColor = (int)tile.color();
							ushort num = *tile.type;
							if (num != 5)
							{
								if (num == 323)
								{
									tileStyle = this.GetPalmTreeBiome(j, i);
								}
							}
							else
							{
								tileStyle = TileDrawing.GetTreeBiome(j, i, (int)(*tile.frameX), (int)(*tile.frameY));
							}
							lookupKey.TileStyle = tileStyle;
							if (lookupKey.PaintColor != 0)
							{
								this._paintSystem.RequestTile(ref lookupKey);
							}
						}
						if (*tile.wall != 0)
						{
							Main.instance.LoadWall((int)(*tile.wall));
							lookupKey2.WallType = (int)(*tile.wall);
							lookupKey2.PaintColor = (int)tile.wallColor();
							if (lookupKey2.PaintColor != 0)
							{
								this._paintSystem.RequestWall(ref lookupKey2);
							}
						}
						if (!prepareLazily)
						{
							this.MakeExtraPreparations(tile, j, i, tileStyle);
						}
					}
				}
			}
		}

		// Token: 0x060045E1 RID: 17889 RVA: 0x0061A66C File Offset: 0x0061886C
		private unsafe void MakeExtraPreparations(Tile tile, int x, int y, int tileStyle)
		{
			ushort num = *tile.type;
			if (num <= 589)
			{
				if (num != 5)
				{
					if (num != 323)
					{
						if (num - 583 > 6)
						{
							return;
						}
						int treeFrame3 = 0;
						int floorY3 = 0;
						int topTextureFrameWidth3 = 0;
						int topTextureFrameHeight3 = 0;
						int treeStyle3 = 0;
						int xoffset3 = (*tile.frameX == 44).ToInt() - (*tile.frameX == 66).ToInt();
						if (WorldGen.GetGemTreeFoliageData(x, y, xoffset3, ref treeFrame3, ref treeStyle3, out floorY3, out topTextureFrameWidth3, out topTextureFrameHeight3))
						{
							TilePaintSystemV2.TreeFoliageVariantKey lookupKey4 = new TilePaintSystemV2.TreeFoliageVariantKey
							{
								TextureIndex = treeStyle3,
								PaintColor = (int)tile.color()
							};
							this._paintSystem.RequestTreeTop(ref lookupKey4);
							this._paintSystem.RequestTreeBranch(ref lookupKey4);
							return;
						}
					}
					else
					{
						int textureIndex = 15;
						bool isOcean = false;
						if (x >= WorldGen.beachDistance && x <= Main.maxTilesX - WorldGen.beachDistance)
						{
							isOcean = true;
						}
						if (isOcean)
						{
							textureIndex = 21;
						}
						if (Math.Abs(tileStyle) >= 8)
						{
							textureIndex = Math.Abs(tileStyle) - 8;
							textureIndex *= -2;
							if (!isOcean)
							{
								textureIndex--;
							}
						}
						TilePaintSystemV2.TreeFoliageVariantKey lookupKey5 = new TilePaintSystemV2.TreeFoliageVariantKey
						{
							TextureIndex = textureIndex,
							PaintColor = (int)tile.color()
						};
						this._paintSystem.RequestTreeTop(ref lookupKey5);
						this._paintSystem.RequestTreeBranch(ref lookupKey5);
					}
				}
				else
				{
					int treeFrame4 = 0;
					int floorY4 = 0;
					int topTextureFrameWidth4 = 0;
					int topTextureFrameHeight4 = 0;
					int treeStyle4 = 0;
					int xoffset4 = (*tile.frameX == 44).ToInt() - (*tile.frameX == 66).ToInt();
					if (WorldGen.GetCommonTreeFoliageData(x, y, xoffset4, ref treeFrame4, ref treeStyle4, out floorY4, out topTextureFrameWidth4, out topTextureFrameHeight4))
					{
						TilePaintSystemV2.TreeFoliageVariantKey lookupKey6 = new TilePaintSystemV2.TreeFoliageVariantKey
						{
							TextureIndex = treeStyle4,
							PaintColor = (int)tile.color()
						};
						this._paintSystem.RequestTreeTop(ref lookupKey6);
						this._paintSystem.RequestTreeBranch(ref lookupKey6);
						return;
					}
				}
			}
			else if (num != 596 && num != 616)
			{
				if (num != 634)
				{
					return;
				}
				int treeFrame5 = 0;
				int floorY5 = 0;
				int topTextureFrameWidth5 = 0;
				int topTextureFrameHeight5 = 0;
				int treeStyle5 = 0;
				int xoffset5 = (*tile.frameX == 44).ToInt() - (*tile.frameX == 66).ToInt();
				if (WorldGen.GetAshTreeFoliageData(x, y, xoffset5, ref treeFrame5, ref treeStyle5, out floorY5, out topTextureFrameWidth5, out topTextureFrameHeight5))
				{
					TilePaintSystemV2.TreeFoliageVariantKey lookupKey7 = new TilePaintSystemV2.TreeFoliageVariantKey
					{
						TextureIndex = treeStyle5,
						PaintColor = (int)tile.color()
					};
					this._paintSystem.RequestTreeTop(ref lookupKey7);
					this._paintSystem.RequestTreeBranch(ref lookupKey7);
					return;
				}
			}
			else
			{
				int treeFrame6 = 0;
				int floorY6 = 0;
				int topTextureFrameWidth6 = 0;
				int topTextureFrameHeight6 = 0;
				int treeStyle6 = 0;
				int xoffset6 = (*tile.frameX == 44).ToInt() - (*tile.frameX == 66).ToInt();
				if (WorldGen.GetVanityTreeFoliageData(x, y, xoffset6, ref treeFrame6, ref treeStyle6, out floorY6, out topTextureFrameWidth6, out topTextureFrameHeight6))
				{
					TilePaintSystemV2.TreeFoliageVariantKey lookupKey8 = new TilePaintSystemV2.TreeFoliageVariantKey
					{
						TextureIndex = treeStyle6,
						PaintColor = (int)tile.color()
					};
					this._paintSystem.RequestTreeTop(ref lookupKey8);
					this._paintSystem.RequestTreeBranch(ref lookupKey8);
					return;
				}
			}
		}

		// Token: 0x060045E2 RID: 17890 RVA: 0x0061A97C File Offset: 0x00618B7C
		public void Update()
		{
			if (!Main.dedServ)
			{
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
		}

		// Token: 0x060045E3 RID: 17891 RVA: 0x0061AAA9 File Offset: 0x00618CA9
		public void SpecificHacksForCapture()
		{
			Main.sectionManager.SetAllFramedSectionsAsNeedingRefresh();
		}

		// Token: 0x060045E4 RID: 17892 RVA: 0x0061AAB8 File Offset: 0x00618CB8
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
				this._specialsCount[13] = 0;
			}
			if (solidLayer && flag)
			{
				this._specialsCount[14] = 0;
			}
		}

		// Token: 0x060045E5 RID: 17893 RVA: 0x0061AB50 File Offset: 0x00618D50
		public void PostDrawTiles(bool solidLayer, bool forRenderTargets, bool intoRenderTargets)
		{
			if (!solidLayer && !intoRenderTargets)
			{
				Main.spriteBatch.Begin(0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
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
				this.DrawCustom(solidLayer);
				Main.spriteBatch.End();
			}
			if (solidLayer && !intoRenderTargets)
			{
				this.DrawEntities_HatRacks();
				this.DrawEntities_DisplayDolls();
				this.DrawCustom(solidLayer);
			}
		}

		// Token: 0x060045E6 RID: 17894 RVA: 0x0061ABF0 File Offset: 0x00618DF0
		public void DrawLiquidBehindTiles(int waterStyleOverride = -1)
		{
			Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
			Vector2 vector;
			vector..ctor((float)Main.offScreenRange, (float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				vector = Vector2.Zero;
			}
			int firstTileX;
			int lastTileX;
			int firstTileY;
			int lastTileY;
			this.GetScreenDrawArea(unscaledPosition, vector + (Main.Camera.UnscaledPosition - Main.Camera.ScaledPosition), out firstTileX, out lastTileX, out firstTileY, out lastTileY);
			for (int i = firstTileY; i < lastTileY + 4; i++)
			{
				for (int j = firstTileX - 2; j < lastTileX + 2; j++)
				{
					Tile tile = Main.tile[j, i];
					if (tile != null)
					{
						this.DrawTile_LiquidBehindTile(false, false, waterStyleOverride, unscaledPosition, vector, j, i, tile);
					}
				}
			}
		}

		// Token: 0x060045E7 RID: 17895 RVA: 0x0061ACAC File Offset: 0x00618EAC
		public unsafe void Draw(bool solidLayer, bool forRenderTargets, bool intoRenderTargets, int waterStyleOverride = -1)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			this._isActiveAndNotPaused = (!Main.gamePaused && Main.instance.IsActive);
			this._localPlayer = Main.LocalPlayer;
			Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
			Vector2 vector;
			vector..ctor((float)Main.offScreenRange, (float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				vector = Vector2.Zero;
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
			int firstTileX;
			int lastTileX;
			int firstTileY;
			int lastTileY;
			this.GetScreenDrawArea(unscaledPosition, vector + (Main.Camera.UnscaledPosition - Main.Camera.ScaledPosition), out firstTileX, out lastTileX, out firstTileY, out lastTileY);
			byte b = (byte)(100f + 150f * Main.martianLight);
			this._martianGlow = new Color((int)b, (int)b, (int)b, 0);
			TileDrawInfo value = this._currentTileDrawInfo.Value;
			for (int i = firstTileX - 2; i < lastTileX + 2; i++)
			{
				for (int j = firstTileY; j < lastTileY + 4; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile == null)
					{
						Main.tile[i, j] = default(Tile);
						Main.mapTime += 60;
					}
					else if (tile.active() && this.IsTileDrawLayerSolid(*tile.type) == solidLayer)
					{
						if (solidLayer)
						{
							this.DrawTile_LiquidBehindTile(solidLayer, false, waterStyleOverride, unscaledPosition, vector, i, j, tile);
						}
						ushort type = *tile.type;
						short frameX = *tile.frameX;
						short frameY = *tile.frameY;
						if (!TextureAssets.Tile[(int)type].IsLoaded)
						{
							Main.instance.LoadTiles((int)type);
						}
						if (TileLoader.PreDraw(i, j, (int)type, Main.spriteBatch))
						{
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
													goto IL_859;
												}
												if (frameX % 54 == 0 && frameY % 54 == 0 && flag)
												{
													this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileVine);
													goto IL_898;
												}
												goto IL_898;
											}
											else
											{
												if (frameX % 36 == 0 && frameY == 0 && flag)
												{
													this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileGrass);
													goto IL_898;
												}
												goto IL_898;
											}
										}
										else
										{
											if (type == 42)
											{
												goto IL_5A1;
											}
											if (type != 52)
											{
												goto IL_859;
											}
										}
									}
									else if (type <= 91)
									{
										if (type != 62)
										{
											if (type != 91)
											{
												goto IL_859;
											}
											if (frameX % 18 == 0 && frameY % 54 == 0 && flag)
											{
												this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileVine);
												goto IL_898;
											}
											goto IL_898;
										}
									}
									else
									{
										if (type == 95)
										{
											goto IL_5F3;
										}
										if (type != 115)
										{
											goto IL_859;
										}
									}
								}
								else if (type <= 233)
								{
									if (type <= 184)
									{
										if (type == 126)
										{
											goto IL_5F3;
										}
										if (type != 184)
										{
											goto IL_859;
										}
										if (flag)
										{
											this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.AnyDirectionalGrass);
											goto IL_898;
										}
										goto IL_898;
									}
									else if (type != 205)
									{
										if (type != 233)
										{
											goto IL_859;
										}
										if (frameY == 0 && frameX % 54 == 0 && flag)
										{
											this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileGrass);
										}
										if (frameY == 36 && frameX % 36 == 0 && flag)
										{
											this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileGrass);
											goto IL_898;
										}
										goto IL_898;
									}
								}
								else if (type <= 238)
								{
									if (type != 236 && type != 238)
									{
										goto IL_859;
									}
									if (frameX % 36 == 0 && frameY == 0 && flag)
									{
										this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileGrass);
										goto IL_898;
									}
									goto IL_898;
								}
								else
								{
									if (type - 270 <= 1)
									{
										goto IL_5A1;
									}
									if (type - 373 <= 2)
									{
										goto IL_7DB;
									}
									if (type != 382)
									{
										goto IL_859;
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
												goto IL_5F3;
											}
											if (type != 454)
											{
												goto IL_859;
											}
											if (frameX % 72 == 0 && frameY % 54 == 0 && flag)
											{
												this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileVine);
												goto IL_898;
											}
											goto IL_898;
										}
										else
										{
											if (type == 461)
											{
												goto IL_7DB;
											}
											if (type != 465)
											{
												goto IL_859;
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
												this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileGrass);
												goto IL_898;
											}
											goto IL_898;
										case 486:
										case 487:
										case 488:
										case 492:
											goto IL_859;
										case 491:
											if (flag && frameX == 18 && frameY == 18)
											{
												this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.VoidLens);
												goto IL_877;
											}
											goto IL_877;
										case 493:
											if (frameY == 0 && frameX % 18 == 0 && flag)
											{
												this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileGrass);
												goto IL_898;
											}
											goto IL_898;
										default:
											switch (type)
											{
											case 519:
												if (frameX / 18 <= 4 && flag)
												{
													this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileGrass);
													goto IL_898;
												}
												goto IL_898;
											case 520:
											case 529:
												goto IL_859;
											case 521:
											case 522:
											case 523:
											case 524:
											case 525:
											case 526:
											case 527:
												if (frameY == 0 && frameX % 36 == 0 && flag)
												{
													this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileGrass);
													goto IL_898;
												}
												goto IL_898;
											case 528:
												goto IL_525;
											case 530:
												if (frameX >= 270)
												{
													goto IL_877;
												}
												if (frameX % 54 == 0 && frameY == 0 && flag)
												{
													this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileGrass);
													goto IL_898;
												}
												goto IL_898;
											default:
												goto IL_859;
											}
											break;
										}
									}
									else if (type != 549)
									{
										if (type != 572)
										{
											goto IL_859;
										}
										goto IL_5A1;
									}
									else
									{
										if (flag)
										{
											this.CrawlToBottomOfReverseVineAndAddSpecialPoint(j, i);
											goto IL_898;
										}
										goto IL_898;
									}
								}
								else if (type <= 617)
								{
									if (type <= 592)
									{
										if (type == 581)
										{
											goto IL_5A1;
										}
										if (type - 591 > 1)
										{
											goto IL_859;
										}
									}
									else if (type != 597)
									{
										if (type != 617)
										{
											goto IL_859;
										}
										if (flag && frameX % 54 == 0 && frameY % 72 == 0)
										{
											this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MasterTrophy);
											goto IL_877;
										}
										goto IL_877;
									}
									else
									{
										if (flag && frameX % 54 == 0 && frameY == 0)
										{
											this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.TeleportationPylon);
											goto IL_877;
										}
										goto IL_877;
									}
								}
								else if (type <= 638)
								{
									if (type != 636 && type != 638)
									{
										goto IL_859;
									}
									goto IL_525;
								}
								else if (type != 651)
								{
									if (type != 652)
									{
										if (type != 660)
										{
											goto IL_859;
										}
										goto IL_5A1;
									}
									else
									{
										if (frameX % 36 == 0 && flag)
										{
											this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileGrass);
											goto IL_898;
										}
										goto IL_898;
									}
								}
								else
								{
									if (frameX % 54 == 0 && flag)
									{
										this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileGrass);
										goto IL_898;
									}
									goto IL_898;
								}
								if (frameX % 36 == 0 && frameY % 54 == 0 && flag)
								{
									this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileVine);
									goto IL_898;
								}
								goto IL_898;
							}
							IL_525:
							if (flag)
							{
								this.CrawlToTopOfVineAndAddSpecialPoint(j, i);
								goto IL_898;
							}
							goto IL_898;
							IL_5A1:
							if (frameX % 18 == 0 && frameY % 36 == 0 && flag)
							{
								this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileVine);
								goto IL_898;
							}
							goto IL_898;
							IL_5F3:
							if (frameX % 36 == 0 && frameY % 36 == 0 && flag)
							{
								this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileVine);
								goto IL_898;
							}
							goto IL_898;
							IL_7DB:
							this.EmitLiquidDrops(j, i, tile, type);
							goto IL_898;
							IL_859:
							if (this.ShouldSwayInWind(i, j, tile))
							{
								if (flag)
								{
									this.AddSpecialPoint(i, j, TileDrawing.TileCounterType.WindyGrass);
									goto IL_898;
								}
								goto IL_898;
							}
							IL_877:
							this.DrawSingleTile(value, solidLayer, waterStyleOverride, unscaledPosition, vector, i, j);
						}
						TileLoader.PostDraw(i, j, (int)type, Main.spriteBatch);
					}
					IL_898:;
				}
			}
			if (solidLayer)
			{
				Main.instance.DrawTileCracks(1, Main.player[Main.myPlayer].hitReplace);
				Main.instance.DrawTileCracks(1, Main.player[Main.myPlayer].hitTile);
			}
			this.DrawSpecialTilesLegacy(unscaledPosition, vector);
			if (TileObject.objectPreview.Active && this._localPlayer.cursorItemIconEnabled && Main.placementPreview && !CaptureManager.Instance.Active)
			{
				Main.instance.LoadTiles((int)TileObject.objectPreview.Type);
				TileObject.DrawPreview(Main.spriteBatch, TileObject.objectPreview, unscaledPosition - vector);
			}
			if (solidLayer)
			{
				TimeLogger.DrawTime(0, stopwatch.Elapsed.TotalMilliseconds);
				return;
			}
			TimeLogger.DrawTime(1, stopwatch.Elapsed.TotalMilliseconds);
		}

		/// <summary>
		/// Finds the top of a tile chain of vine tiles and calls <see cref="M:Terraria.GameContent.Drawing.TileDrawing.AddSpecialPoint(System.Int32,System.Int32,Terraria.GameContent.Drawing.TileDrawing.TileCounterType)" /> using <see cref="F:Terraria.GameContent.Drawing.TileDrawing.TileCounterType.Vine" /> if not already registered for custom drawing. See <see cref="F:Terraria.ID.TileID.Sets.VineThreads" />.
		/// </summary>
		// Token: 0x060045E8 RID: 17896 RVA: 0x0061B638 File Offset: 0x00619838
		public void CrawlToTopOfVineAndAddSpecialPoint(int j, int i)
		{
			int y = j;
			for (int num = j - 1; num > 0; num--)
			{
				Tile tile = Main.tile[i, num];
				if (WorldGen.SolidTile(i, num, false) || !tile.active())
				{
					y = num + 1;
					break;
				}
			}
			Point item;
			item..ctor(i, y);
			if (!this._vineRootsPositions.Contains(item))
			{
				this._vineRootsPositions.Add(item);
				this.AddSpecialPoint(i, y, TileDrawing.TileCounterType.Vine);
			}
		}

		/// <summary>
		/// Finds the bottom of a tile chain of reverse vine tiles and calls <see cref="M:Terraria.GameContent.Drawing.TileDrawing.AddSpecialPoint(System.Int32,System.Int32,Terraria.GameContent.Drawing.TileDrawing.TileCounterType)" /> using <see cref="F:Terraria.GameContent.Drawing.TileDrawing.TileCounterType.ReverseVine" /> if not already registered for custom drawing. See <see cref="F:Terraria.ID.TileID.Sets.ReverseVineThreads" />.
		/// </summary>
		// Token: 0x060045E9 RID: 17897 RVA: 0x0061B6A8 File Offset: 0x006198A8
		public void CrawlToBottomOfReverseVineAndAddSpecialPoint(int j, int i)
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
			Point item;
			item..ctor(i, y);
			if (!this._reverseVineRootsPositions.Contains(item))
			{
				this._reverseVineRootsPositions.Add(item);
				this.AddSpecialPoint(i, y, TileDrawing.TileCounterType.ReverseVine);
			}
		}

		// Token: 0x060045EA RID: 17898 RVA: 0x0061B71C File Offset: 0x0061991C
		private unsafe void DrawSingleTile(TileDrawInfo drawData, bool solidLayer, int waterStyleOverride, Vector2 screenPosition, Vector2 screenOffset, int tileX, int tileY)
		{
			drawData.tileCache = Main.tile[tileX, tileY];
			drawData.typeCache = *drawData.tileCache.type;
			drawData.tileFrameX = *drawData.tileCache.frameX;
			drawData.tileFrameY = *drawData.tileCache.frameY;
			drawData.tileLight = Lighting.GetColor(tileX, tileY);
			if (*drawData.tileCache.liquid > 0 && *drawData.tileCache.type == 518)
			{
				return;
			}
			this.GetTileDrawData(tileX, tileY, drawData.tileCache, drawData.typeCache, ref drawData.tileFrameX, ref drawData.tileFrameY, out drawData.tileWidth, out drawData.tileHeight, out drawData.tileTop, out drawData.halfBrickHeight, out drawData.addFrX, out drawData.addFrY, out drawData.tileSpriteEffect, out drawData.glowTexture, out drawData.glowSourceRect, out drawData.glowColor);
			drawData.drawTexture = this.GetTileDrawTexture(drawData.tileCache, tileX, tileY);
			Texture2D highlightTexture = null;
			Rectangle empty = Rectangle.Empty;
			Color highlightColor = Color.Transparent;
			if (TileID.Sets.HasOutlines[(int)drawData.typeCache])
			{
				this.GetTileOutlineInfo(tileX, tileY, drawData.typeCache, ref drawData.tileLight, ref highlightTexture, ref highlightColor);
			}
			if (this._localPlayer.dangerSense && TileDrawing.IsTileDangerous(tileX, tileY, this._localPlayer, drawData.tileCache, drawData.typeCache))
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
			if (this._localPlayer.findTreasure && Main.IsTileSpelunkable(tileX, tileY, drawData.typeCache, drawData.tileFrameX, drawData.tileFrameY))
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
				Color sightColor = Color.White;
				if (Main.IsTileBiomeSightable(tileX, tileY, drawData.typeCache, drawData.tileFrameX, drawData.tileFrameY, ref sightColor))
				{
					if (drawData.tileLight.R < sightColor.R)
					{
						drawData.tileLight.R = sightColor.R;
					}
					if (drawData.tileLight.G < sightColor.G)
					{
						drawData.tileLight.G = sightColor.G;
					}
					if (drawData.tileLight.B < sightColor.B)
					{
						drawData.tileLight.B = sightColor.B;
					}
					if (this._isActiveAndNotPaused && this._rand.Next(480) == 0)
					{
						Color newColor = sightColor;
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
			if (*drawData.tileCache.wall > 0 && (*drawData.tileCache.wall == 318 || drawData.tileCache.fullbrightWall()))
			{
				flag = true;
			}
			flag &= TileDrawing.IsVisible(drawData.tileCache);
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
			Rectangle rectangle;
			rectangle..ctor((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight - drawData.halfBrickHeight);
			Vector2 vector = new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop + drawData.halfBrickHeight)) + screenOffset;
			drawData.colorTint = Color.White;
			drawData.finalColor = TileDrawing.GetFinalLight(drawData.tileCache, drawData.typeCache, drawData.tileLight, drawData.colorTint);
			TileLoader.DrawEffects(tileX, tileY, (int)drawData.typeCache, Main.spriteBatch, ref drawData);
			if (!flag)
			{
				return;
			}
			ushort num20 = drawData.typeCache;
			if (num20 <= 129)
			{
				if (num20 <= 80)
				{
					if (num20 != 51)
					{
						if (num20 == 80)
						{
							bool evil;
							bool good;
							bool crimson;
							WorldGen.GetCactusType(tileX, tileY, (int)drawData.tileFrameX, (int)drawData.tileFrameY, out evil, out good, out crimson);
							if (evil)
							{
								rectangle.Y += 54;
							}
							if (good)
							{
								rectangle.Y += 108;
							}
							if (crimson)
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
				else if (num20 != 83)
				{
					if (num20 != 114)
					{
						if (num20 == 129)
						{
							drawData.finalColor = new Color(255, 255, 255, 100);
							int num5 = 2;
							if (drawData.tileFrameX >= 324)
							{
								drawData.finalColor = Color.Transparent;
							}
							if (drawData.tileFrameY < 36)
							{
								vector.Y += (float)(num5 * (drawData.tileFrameY == 0).ToDirectionInt());
							}
							else
							{
								vector.X += (float)(num5 * (drawData.tileFrameY == 36).ToDirectionInt());
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
				if (num20 <= 272)
				{
					if (num20 != 136)
					{
						if (num20 != 160)
						{
							if (num20 != 272)
							{
								goto IL_A6D;
							}
							int num6 = Main.tileFrame[(int)drawData.typeCache];
							num6 += tileX % 2;
							num6 += tileY % 2;
							num6 += tileX % 3;
							num6 += tileY % 3;
							num6 %= 2;
							num6 *= 90;
							drawData.addFrY += num6;
							rectangle.Y += num6;
							goto IL_A6D;
						}
					}
					else
					{
						int num21 = (int)(drawData.tileFrameX / 18);
						if (num21 == 1)
						{
							vector.X += -2f;
							goto IL_A6D;
						}
						if (num21 != 2)
						{
							goto IL_A6D;
						}
						vector.X += 2f;
						goto IL_A6D;
					}
				}
				else if (num20 != 323)
				{
					if (num20 != 442)
					{
						if (num20 != 692)
						{
							goto IL_A6D;
						}
					}
					else
					{
						if (drawData.tileFrameX / 22 == 3)
						{
							vector.X += 2f;
							goto IL_A6D;
						}
						goto IL_A6D;
					}
				}
				else
				{
					if (*drawData.tileCache.frameX <= 132 && *drawData.tileCache.frameX >= 88)
					{
						return;
					}
					vector.X += (float)(*drawData.tileCache.frameY);
					goto IL_A6D;
				}
				Color color;
				color..ctor(Main.DiscoR, Main.DiscoG, Main.DiscoB, 255);
				if (drawData.tileCache.inActive())
				{
					color = drawData.tileCache.actColor(color);
				}
				drawData.finalColor = color;
			}
			IL_A6D:
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
			if (Main.tileGlowMask[(int)(*drawData.tileCache.type)] != -1)
			{
				short num7 = Main.tileGlowMask[(int)(*drawData.tileCache.type)];
				if (TextureAssets.GlowMask.IndexInRange((int)num7))
				{
					drawData.drawTexture = TextureAssets.GlowMask[(int)num7].Value;
				}
				double num8 = Main.timeForVisualEffects * 0.08;
				Color color2 = Color.White;
				bool flag2 = false;
				num20 = *drawData.tileCache.type;
				if (num20 > 391)
				{
					if (num20 > 540)
					{
						if (num20 <= 659)
						{
							switch (num20)
							{
							case 625:
							case 626:
								goto IL_D4B;
							case 627:
							case 628:
								goto IL_D58;
							case 629:
							case 630:
							case 631:
							case 632:
								goto IL_F2E;
							case 633:
								color2 = Color.Lerp(Color.White, drawData.finalColor, 0.75f);
								goto IL_F2E;
							default:
								if (num20 != 659)
								{
									goto IL_F2E;
								}
								break;
							}
						}
						else if (num20 != 667)
						{
							switch (num20)
							{
							case 687:
								goto IL_D17;
							case 688:
								goto IL_D3E;
							case 689:
								goto IL_D24;
							case 690:
								goto IL_D31;
							case 691:
								goto IL_D4B;
							case 692:
								goto IL_D58;
							default:
								goto IL_F2E;
							}
						}
						color2 = LiquidRenderer.GetShimmerGlitterColor(true, (float)tileX, (float)tileY);
						goto IL_F2E;
						IL_D4B:
						color2 = this._violetMossGlow;
						goto IL_F2E;
						IL_D58:
						color2..ctor(Main.DiscoR, Main.DiscoG, Main.DiscoB);
						goto IL_F2E;
					}
					if (num20 <= 445)
					{
						if (num20 != 429 && num20 != 445)
						{
							goto IL_F2E;
						}
						drawData.drawTexture = this.GetTileDrawTexture(drawData.tileCache, tileX, tileY);
						drawData.addFrY = 18;
						goto IL_F2E;
					}
					else
					{
						if (num20 == 517)
						{
							goto IL_D17;
						}
						switch (num20)
						{
						case 534:
						case 535:
							break;
						case 536:
						case 537:
							goto IL_D31;
						case 538:
							goto IL_F2E;
						case 539:
						case 540:
							goto IL_D3E;
						default:
							goto IL_F2E;
						}
					}
					IL_D24:
					color2 = this._kryptonMossGlow;
					goto IL_F2E;
					IL_D31:
					color2 = this._xenonMossGlow;
					goto IL_F2E;
					IL_D3E:
					color2 = this._argonMossGlow;
					goto IL_F2E;
				}
				if (num20 > 350)
				{
					if (num20 <= 381)
					{
						if (num20 != 370)
						{
							if (num20 != 381)
							{
								goto IL_F2E;
							}
							goto IL_D17;
						}
					}
					else if (num20 != 390)
					{
						if (num20 != 391)
						{
							goto IL_F2E;
						}
						color2..ctor(250, 250, 250, 200);
						goto IL_F2E;
					}
					color2 = this._meteorGlow;
					goto IL_F2E;
				}
				if (num20 != 129)
				{
					if (num20 == 209)
					{
						color2 = PortalHelper.GetPortalColor(Main.myPlayer, (*drawData.tileCache.frameX >= 288) ? 1 : 0);
						goto IL_F2E;
					}
					if (num20 != 350)
					{
						goto IL_F2E;
					}
					color2..ctor(new Vector4((float)((0.0 - Math.Cos(((int)(num8 / 6.283) % 3 == 1) ? num8 : 0.0)) * 0.2 + 0.2)));
					goto IL_F2E;
				}
				else
				{
					if (drawData.tileFrameX < 324)
					{
						flag2 = true;
						goto IL_F2E;
					}
					drawData.drawTexture = this.GetTileDrawTexture(drawData.tileCache, tileX, tileY);
					color2 = Main.hslToRgb(0.7f + (float)Math.Sin((double)(6.2831855f * Main.GlobalTimeWrappedHourly * 0.16f + (float)tileX * 0.3f + (float)tileY * 0.7f)) * 0.16f, 1f, 0.5f, byte.MaxValue);
					color2.A /= 2;
					color2 *= 0.3f;
					int num9 = 72;
					for (float num10 = 0f; num10 < 6.2831855f; num10 += 1.5707964f)
					{
						Main.spriteBatch.Draw(drawData.drawTexture, vector + num10.ToRotationVector2() * 2f, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY + num9, drawData.tileWidth, drawData.tileHeight)), color2, 0f, Vector2.Zero, 1f, 0, 0f);
					}
					color2..ctor(255, 255, 255, 100);
					goto IL_F2E;
				}
				IL_D17:
				color2 = this._lavaMossGlow;
				IL_F2E:
				if (!flag2)
				{
					if (drawData.tileCache.slope() == 0 && !drawData.tileCache.halfBrick())
					{
						Main.spriteBatch.Draw(drawData.drawTexture, vector, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight)), color2, 0f, Vector2.Zero, 1f, 0, 0f);
					}
					else if (drawData.tileCache.halfBrick())
					{
						Main.spriteBatch.Draw(drawData.drawTexture, vector, new Rectangle?(rectangle), color2, 0f, TileDrawing._zero, 1f, 0, 0f);
					}
					else if (TileID.Sets.Platforms[(int)(*drawData.tileCache.type)])
					{
						Main.spriteBatch.Draw(drawData.drawTexture, vector, new Rectangle?(rectangle), color2, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						if (drawData.tileCache.slope() == 1 && Main.tile[tileX + 1, tileY + 1].active() && Main.tileSolid[(int)(*Main.tile[tileX + 1, tileY + 1].type)] && Main.tile[tileX + 1, tileY + 1].slope() != 2 && !Main.tile[tileX + 1, tileY + 1].halfBrick() && (!Main.tile[tileX, tileY + 1].active() || (Main.tile[tileX, tileY + 1].blockType() != 0 && Main.tile[tileX, tileY + 1].blockType() != 5) || (!TileID.Sets.BlocksStairs[(int)(*Main.tile[tileX, tileY + 1].type)] && !TileID.Sets.BlocksStairsAbove[(int)(*Main.tile[tileX, tileY + 1].type)])))
						{
							Rectangle value;
							value..ctor(198, (int)drawData.tileFrameY, 16, 16);
							if (TileID.Sets.Platforms[(int)(*Main.tile[tileX + 1, tileY + 1].type)] && Main.tile[tileX + 1, tileY + 1].slope() == 0)
							{
								value.X = 324;
							}
							Main.spriteBatch.Draw(drawData.drawTexture, vector + new Vector2(0f, 16f), new Rectangle?(value), color2, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						else if (drawData.tileCache.slope() == 2 && Main.tile[tileX - 1, tileY + 1].active() && Main.tileSolid[(int)(*Main.tile[tileX - 1, tileY + 1].type)] && Main.tile[tileX - 1, tileY + 1].slope() != 1 && !Main.tile[tileX - 1, tileY + 1].halfBrick() && (!Main.tile[tileX, tileY + 1].active() || (Main.tile[tileX, tileY + 1].blockType() != 0 && Main.tile[tileX, tileY + 1].blockType() != 4) || (!TileID.Sets.BlocksStairs[(int)(*Main.tile[tileX, tileY + 1].type)] && !TileID.Sets.BlocksStairsAbove[(int)(*Main.tile[tileX, tileY + 1].type)])))
						{
							Rectangle value2;
							value2..ctor(162, (int)drawData.tileFrameY, 16, 16);
							if (TileID.Sets.Platforms[(int)(*Main.tile[tileX - 1, tileY + 1].type)] && Main.tile[tileX - 1, tileY + 1].slope() == 0)
							{
								value2.X = 306;
							}
							Main.spriteBatch.Draw(drawData.drawTexture, vector + new Vector2(0f, 16f), new Rectangle?(value2), color2, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
					}
					else if (TileID.Sets.HasSlopeFrames[(int)(*drawData.tileCache.type)])
					{
						Main.spriteBatch.Draw(drawData.drawTexture, vector, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, 16, 16)), color2, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
					else
					{
						int num11 = (int)drawData.tileCache.slope();
						int num12 = 2;
						for (int i = 0; i < 8; i++)
						{
							int num13 = i * -2;
							int num14 = 16 - i * 2;
							int num15 = 16 - num14;
							int num16;
							switch (num11)
							{
							case 1:
								num13 = 0;
								num16 = i * 2;
								num14 = 14 - i * 2;
								num15 = 0;
								break;
							case 2:
								num13 = 0;
								num16 = 16 - i * 2 - 2;
								num14 = 14 - i * 2;
								num15 = 0;
								break;
							case 3:
								num16 = i * 2;
								break;
							default:
								num16 = 16 - i * 2 - 2;
								break;
							}
							Main.spriteBatch.Draw(drawData.drawTexture, vector + new Vector2((float)num16, (float)(i * num12 + num13)), new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX + num16, (int)drawData.tileFrameY + drawData.addFrY + num15, num12, num14)), color2, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						int num17 = (num11 <= 2) ? 14 : 0;
						Main.spriteBatch.Draw(drawData.drawTexture, vector + new Vector2(0f, (float)num17), new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY + num17, 16, 2)), color2, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
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
			if (highlightTexture != null)
			{
				empty..ctor((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight);
				int num18 = 0;
				int num19 = 0;
				Main.spriteBatch.Draw(highlightTexture, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + (float)num18, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop + num19)) + screenOffset, new Rectangle?(empty), highlightColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
		}

		/// <summary>
		/// Returns true if the tile is visible.
		/// <para />Tiles painted with Echo Coating as well as the Echo Platform, Echo Block, and Ghostly Stinkbug Blocker tiles will all be invisible unless the player has Echo Sight (Nearby active Echo Chamber tile or wearing Spectre Goggles)
		/// </summary>
		/// <param name="tile"></param>
		/// <returns></returns>
		// Token: 0x060045EB RID: 17899 RVA: 0x0061CE7C File Offset: 0x0061B07C
		public unsafe static bool IsVisible(Tile tile)
		{
			bool flag = tile.invisibleBlock();
			ushort num = *tile.type;
			if (num != 19)
			{
				if (num == 541 || num == 631)
				{
					flag = true;
				}
			}
			else if (*tile.frameY / 18 == 48)
			{
				flag = true;
			}
			return !flag || Main.instance.TilesRenderer._shouldShowInvisibleBlocks;
		}

		// Token: 0x060045EC RID: 17900 RVA: 0x0061CEDC File Offset: 0x0061B0DC
		public unsafe Texture2D GetTileDrawTexture(Tile tile, int tileX, int tileY)
		{
			Texture2D result = TextureAssets.Tile[(int)(*tile.type)].Value;
			int tileStyle = 0;
			int num = (int)(*tile.type);
			ushort num2 = *tile.type;
			if (num2 <= 80)
			{
				if (num2 == 5)
				{
					tileStyle = TileDrawing.GetTreeBiome(tileX, tileY, (int)(*tile.frameX), (int)(*tile.frameY));
					goto IL_10B;
				}
				if (num2 != 80)
				{
					goto IL_10B;
				}
			}
			else
			{
				if (num2 == 83)
				{
					if (this.IsAlchemyPlantHarvestable((int)(*tile.frameX / 18)))
					{
						num = 84;
					}
					Main.instance.LoadTiles(num);
					goto IL_10B;
				}
				if (num2 != 227)
				{
					if (num2 != 323)
					{
						goto IL_10B;
					}
					tileStyle = this.GetPalmTreeBiome(tileX, tileY);
					goto IL_10B;
				}
			}
			int sandType;
			WorldGen.GetCactusType(tileX, tileY, (int)(*tile.frameX), (int)(*tile.frameY), out sandType);
			if (TileLoader.CanGrowModCactus(sandType))
			{
				if (num == 80)
				{
					tileStyle = sandType + 1;
				}
				else if (*tile.frameX == 204 || *tile.frameX == 202)
				{
					Asset<Texture2D> asset = PlantLoader.GetCactusFruitTexture(sandType);
					if (asset != null)
					{
						return asset.Value;
					}
				}
			}
			IL_10B:
			Texture2D texture2D = this._paintSystem.TryGetTileAndRequestIfNotReady(num, tileStyle, (int)tile.color());
			if (texture2D != null)
			{
				result = texture2D;
			}
			return result;
		}

		// Token: 0x060045ED RID: 17901 RVA: 0x0061D010 File Offset: 0x0061B210
		public unsafe Texture2D GetTileDrawTexture(Tile tile, int tileX, int tileY, int paintOverride)
		{
			Texture2D result = TextureAssets.Tile[(int)(*tile.type)].Value;
			int tileStyle = 0;
			int num = (int)(*tile.type);
			ushort num2 = *tile.type;
			if (num2 <= 80)
			{
				if (num2 == 5)
				{
					tileStyle = TileDrawing.GetTreeBiome(tileX, tileY, (int)(*tile.frameX), (int)(*tile.frameY));
					goto IL_10B;
				}
				if (num2 != 80)
				{
					goto IL_10B;
				}
			}
			else
			{
				if (num2 == 83)
				{
					if (this.IsAlchemyPlantHarvestable((int)(*tile.frameX / 18)))
					{
						num = 84;
					}
					Main.instance.LoadTiles(num);
					goto IL_10B;
				}
				if (num2 != 227)
				{
					if (num2 != 323)
					{
						goto IL_10B;
					}
					tileStyle = this.GetPalmTreeBiome(tileX, tileY);
					goto IL_10B;
				}
			}
			int sandType;
			WorldGen.GetCactusType(tileX, tileY, (int)(*tile.frameX), (int)(*tile.frameY), out sandType);
			if (TileLoader.CanGrowModCactus(sandType))
			{
				if (num == 80)
				{
					tileStyle = sandType + 1;
				}
				else if (*tile.frameX == 204 || *tile.frameX == 202)
				{
					Asset<Texture2D> asset = PlantLoader.GetCactusFruitTexture(sandType);
					if (asset != null)
					{
						return asset.Value;
					}
				}
			}
			IL_10B:
			Texture2D texture2D = this._paintSystem.TryGetTileAndRequestIfNotReady(num, tileStyle, paintOverride);
			if (texture2D != null)
			{
				result = texture2D;
			}
			return result;
		}

		// Token: 0x060045EE RID: 17902 RVA: 0x0061D140 File Offset: 0x0061B340
		private unsafe void DrawBasicTile(Vector2 screenPosition, Vector2 screenOffset, int tileX, int tileY, TileDrawInfo drawData, Rectangle normalTileRect, Vector2 normalTilePosition)
		{
			if (TileID.Sets.Platforms[(int)drawData.typeCache] && WorldGen.IsRope(tileX, tileY) && Main.tile[tileX, tileY - 1] != null)
			{
				ref ushort type = ref Main.tile[tileX, tileY - 1].type;
				int y = (tileY + tileX) % 3 * 18;
				Texture2D tileDrawTexture = this.GetTileDrawTexture(Main.tile[tileX, tileY - 1], tileX, tileY);
				if (tileDrawTexture != null)
				{
					Main.spriteBatch.Draw(tileDrawTexture, new Vector2((float)(tileX * 16 - (int)screenPosition.X), (float)(tileY * 16 - (int)screenPosition.Y)) + screenOffset, new Rectangle?(new Rectangle(90, y, 16, 16)), drawData.tileLight, 0f, default(Vector2), 1f, drawData.tileSpriteEffect, 0f);
				}
			}
			if (drawData.tileCache.slope() > 0)
			{
				if (TileID.Sets.Platforms[(int)(*drawData.tileCache.type)])
				{
					Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition, new Rectangle?(normalTileRect), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					if (drawData.tileCache.slope() == 1 && Main.tile[tileX + 1, tileY + 1].active() && Main.tileSolid[(int)(*Main.tile[tileX + 1, tileY + 1].type)] && Main.tile[tileX + 1, tileY + 1].slope() != 2 && !Main.tile[tileX + 1, tileY + 1].halfBrick() && (!Main.tile[tileX, tileY + 1].active() || (Main.tile[tileX, tileY + 1].blockType() != 0 && Main.tile[tileX, tileY + 1].blockType() != 5) || (!TileID.Sets.BlocksStairs[(int)(*Main.tile[tileX, tileY + 1].type)] && !TileID.Sets.BlocksStairsAbove[(int)(*Main.tile[tileX, tileY + 1].type)])))
					{
						Rectangle value;
						value..ctor(198, (int)drawData.tileFrameY, 16, 16);
						if (TileID.Sets.Platforms[(int)(*Main.tile[tileX + 1, tileY + 1].type)] && Main.tile[tileX + 1, tileY + 1].slope() == 0)
						{
							value.X = 324;
						}
						Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition + new Vector2(0f, 16f), new Rectangle?(value), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						return;
					}
					if (drawData.tileCache.slope() == 2 && Main.tile[tileX - 1, tileY + 1].active() && Main.tileSolid[(int)(*Main.tile[tileX - 1, tileY + 1].type)] && Main.tile[tileX - 1, tileY + 1].slope() != 1 && !Main.tile[tileX - 1, tileY + 1].halfBrick() && (!Main.tile[tileX, tileY + 1].active() || (Main.tile[tileX, tileY + 1].blockType() != 0 && Main.tile[tileX, tileY + 1].blockType() != 4) || (!TileID.Sets.BlocksStairs[(int)(*Main.tile[tileX, tileY + 1].type)] && !TileID.Sets.BlocksStairsAbove[(int)(*Main.tile[tileX, tileY + 1].type)])))
					{
						Rectangle value2;
						value2..ctor(162, (int)drawData.tileFrameY, 16, 16);
						if (TileID.Sets.Platforms[(int)(*Main.tile[tileX - 1, tileY + 1].type)] && Main.tile[tileX - 1, tileY + 1].slope() == 0)
						{
							value2.X = 306;
						}
						Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition + new Vector2(0f, 16f), new Rectangle?(value2), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
					return;
				}
				else
				{
					if (TileID.Sets.HasSlopeFrames[(int)(*drawData.tileCache.type)])
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
					int num7 = (num <= 2) ? 14 : 0;
					Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition + new Vector2(0f, (float)num7), new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY + num7, 16, 2)), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					return;
				}
			}
			else if (!TileID.Sets.Platforms[(int)drawData.typeCache] && !TileID.Sets.IgnoresNearbyHalfbricksWhenDrawn[(int)drawData.typeCache] && this._tileSolid[(int)drawData.typeCache] && !TileID.Sets.NotReallySolid[(int)drawData.typeCache] && !drawData.tileCache.halfBrick() && (Main.tile[tileX - 1, tileY].halfBrick() || Main.tile[tileX + 1, tileY].halfBrick()))
			{
				if (Main.tile[tileX - 1, tileY].halfBrick() && Main.tile[tileX + 1, tileY].halfBrick())
				{
					Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition + new Vector2(0f, 8f), new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, drawData.addFrY + (int)drawData.tileFrameY + 8, drawData.tileWidth, 8)), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					Rectangle value3;
					value3..ctor(126 + drawData.addFrX, drawData.addFrY, 16, 8);
					if (Main.tile[tileX, tileY - 1].active() && !Main.tile[tileX, tileY - 1].bottomSlope() && *Main.tile[tileX, tileY - 1].type == drawData.typeCache)
					{
						value3..ctor(90 + drawData.addFrX, drawData.addFrY, 16, 8);
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
					}
				}
				return;
			}
			else
			{
				if (Lighting.NotRetro && this._tileSolid[(int)drawData.typeCache] && !drawData.tileCache.halfBrick() && !TileID.Sets.DontDrawTileSliced[(int)(*drawData.tileCache.type)])
				{
					this.DrawSingleTile_SlicedBlock(normalTilePosition, tileX, tileY, drawData);
					return;
				}
				if (drawData.halfBrickHeight == 8 && (!Main.tile[tileX, tileY + 1].active() || !this._tileSolid[(int)(*Main.tile[tileX, tileY + 1].type)] || Main.tile[tileX, tileY + 1].halfBrick()))
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
						value4.X %= ((num10 < 3) ? 108 : 54);
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
				return;
			}
		}

		// Token: 0x060045EF RID: 17903 RVA: 0x0061E190 File Offset: 0x0061C390
		private unsafe int GetPalmTreeBiome(int tileX, int tileY)
		{
			int i = tileY;
			while (Main.tile[tileX, i].active() && *Main.tile[tileX, i].type == 323)
			{
				i++;
			}
			return this.GetPalmTreeVariant(tileX, i);
		}

		// Token: 0x060045F0 RID: 17904 RVA: 0x0061E1E0 File Offset: 0x0061C3E0
		private unsafe static int GetTreeBiome(int tileX, int tileY, int tileFrameX, int tileFrameY)
		{
			int num = tileX;
			int i = tileY;
			int type = (int)(*Main.tile[num, i].type);
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
				if (tileFrameX != 44)
				{
					if (tileFrameX == 66)
					{
						num--;
					}
				}
				else
				{
					num++;
				}
			}
			else if (tileFrameY >= 132)
			{
				if (tileFrameX != 22)
				{
					if (tileFrameX == 44)
					{
						num++;
					}
				}
				else
				{
					num--;
				}
			}
			while (Main.tile[num, i].active() && (int)(*Main.tile[num, i].type) == type)
			{
				i++;
			}
			return TileDrawing.GetTreeVariant(num, i);
		}

		// Token: 0x060045F1 RID: 17905 RVA: 0x0061E2A0 File Offset: 0x0061C4A0
		public unsafe static int GetTreeVariant(int x, int y)
		{
			if (Main.tile[x, y] == null || !Main.tile[x, y].active())
			{
				return -1;
			}
			ushort num = *Main.tile[x, y].type;
			if (num > 109)
			{
				if (num <= 199)
				{
					if (num == 147)
					{
						return 3;
					}
					if (num != 199)
					{
						goto IL_AF;
					}
				}
				else
				{
					if (num == 492)
					{
						return 2;
					}
					if (num == 661)
					{
						return 0;
					}
					if (num != 662)
					{
						goto IL_AF;
					}
				}
				return 4;
			}
			if (num <= 60)
			{
				if (num != 23)
				{
					if (num != 60)
					{
						goto IL_AF;
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
				if (num == 70)
				{
					return 6;
				}
				if (num != 109)
				{
					goto IL_AF;
				}
				return 2;
			}
			return 0;
			IL_AF:
			if (TileLoader.CanGrowModTree((int)(*Main.tile[x, y].type)))
			{
				return (int)(7 + *Main.tile[x, y].type);
			}
			return -1;
		}

		// Token: 0x060045F2 RID: 17906 RVA: 0x0061E394 File Offset: 0x0061C594
		public unsafe TileDrawing.TileFlameData GetTileFlameData(int tileX, int tileY, int type, int tileFrameY)
		{
			if (type >= (int)TileID.Count)
			{
				TileDrawing.TileFlameData tileFlameData = default(TileDrawing.TileFlameData);
				ModTile tile = TileLoader.GetTile(type);
				if (tile != null)
				{
					tile.GetTileFlameData(tileX, tileY, ref tileFlameData);
				}
				return tileFlameData;
			}
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
			if (type <= 93)
			{
				if (type <= 35)
				{
					if (type == 4)
					{
						num = 0;
						goto IL_1CF;
					}
					switch (type)
					{
					case 33:
						break;
					case 34:
						num = 3;
						goto IL_1CF;
					case 35:
						num = 7;
						goto IL_1CF;
					default:
						goto IL_1CF;
					}
				}
				else
				{
					if (type == 42)
					{
						num = 13;
						goto IL_1CF;
					}
					if (type == 49)
					{
						num = 5;
						goto IL_1CF;
					}
					if (type != 93)
					{
						goto IL_1CF;
					}
					num = 4;
					goto IL_1CF;
				}
			}
			else if (type <= 173)
			{
				if (type == 98)
				{
					num = 6;
					goto IL_1CF;
				}
				if (type != 100 && type != 173)
				{
					goto IL_1CF;
				}
				num = 2;
				goto IL_1CF;
			}
			else if (type != 174)
			{
				if (type == 372)
				{
					num = 16;
					goto IL_1CF;
				}
				if (type != 646)
				{
					goto IL_1CF;
				}
				num = 17;
				goto IL_1CF;
			}
			num = 1;
			IL_1CF:
			TileDrawing.TileFlameData result = new TileDrawing.TileFlameData
			{
				flameTexture = TextureAssets.Flames[num].Value,
				flameSeed = flameSeed
			};
			switch (num)
			{
			case 1:
			{
				int num2 = (int)(*Main.tile[tileX, tileY].frameY / 22);
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
				int num2 = (int)(*Main.tile[tileX, tileY].frameY / 36);
				if (num2 <= 6)
				{
					if (num2 == 3)
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
					if (num2 == 6)
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
					switch (num2)
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
						if (num2 - 28 <= 1)
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
				int num2 = (int)(*Main.tile[tileX, tileY].frameY / 54);
				switch (num2)
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
					if (num2 - 34 <= 1)
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
				int num2 = (int)(*Main.tile[tileX, tileY].frameY / 54);
				switch (num2)
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
					if (num2 - 28 <= 1)
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
					int num2 = tileFrameY / 36;
					if (num2 <= 36)
					{
						switch (num2)
						{
						case 1:
						case 3:
						case 6:
						case 8:
							goto IL_FF1;
						case 2:
							break;
						case 4:
						case 5:
						case 7:
						case 9:
						case 10:
							goto IL_11B9;
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
							switch (num2)
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
								goto IL_11B9;
							case 19:
							case 27:
							case 29:
							case 30:
							case 31:
							case 32:
							case 36:
								goto IL_FF1;
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
								goto IL_11B9;
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
					if (num2 != 39)
					{
						if (num2 != 44)
						{
							goto IL_11B9;
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
					IL_FF1:
					result.flameCount = 7;
					result.flameColor = new Color(100, 100, 100, 0);
					result.flameRangeXMin = -10;
					result.flameRangeXMax = 11;
					result.flameRangeYMin = -10;
					result.flameRangeYMax = 1;
					result.flameRangeMultX = 0.15f;
					result.flameRangeMultY = 0.35f;
					return result;
					IL_11B9:
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

		// Token: 0x060045F3 RID: 17907 RVA: 0x0061F600 File Offset: 0x0061D800
		private unsafe void DrawSingleTile_Flames(Vector2 screenPosition, Vector2 screenOffset, int tileX, int tileY, TileDrawInfo drawData)
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
				Color color;
				color..ctor(255, 255, 255, 0);
				int num105 = (int)(drawData.tileFrameX / 54);
				if (num105 != 5)
				{
					if (num105 != 14)
					{
						if (num105 == 15)
						{
							color..ctor(255, 255, 255, 200);
						}
					}
					else
					{
						color..ctor(50, 50, 100, 20);
					}
				}
				else
				{
					color..ctor((float)Main.DiscoR / 255f, (float)Main.DiscoG / 255f, (float)Main.DiscoB / 255f, 0f);
				}
				Main.spriteBatch.Draw(TextureAssets.Flames[num].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight)), color, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 85)
			{
				float graveyardVisualIntensity = Main.GraveyardVisualIntensity;
				if (graveyardVisualIntensity > 0f)
				{
					ulong num2 = Main.TileFrameSeed ^ (ulong)((long)tileX << 32 | (long)((ulong)tileY));
					TileDrawing.TileFlameData tileFlameData = this.GetTileFlameData(tileX, tileY, (int)drawData.typeCache, (int)drawData.tileFrameY);
					if (num2 == 0UL)
					{
						num2 = tileFlameData.flameSeed;
					}
					tileFlameData.flameSeed = num2;
					Vector2 vector = new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset;
					Rectangle value;
					value..ctor((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight);
					for (int i = 0; i < tileFlameData.flameCount; i++)
					{
						Color color2 = tileFlameData.flameColor * graveyardVisualIntensity;
						float x = (float)Utils.RandomInt(ref tileFlameData.flameSeed, tileFlameData.flameRangeXMin, tileFlameData.flameRangeXMax) * tileFlameData.flameRangeMultX;
						float y = (float)Utils.RandomInt(ref tileFlameData.flameSeed, tileFlameData.flameRangeYMin, tileFlameData.flameRangeYMax) * tileFlameData.flameRangeMultY;
						for (float num3 = 0f; num3 < 1f; num3 += 0.25f)
						{
							Main.spriteBatch.Draw(tileFlameData.flameTexture, vector + new Vector2(x, y) + Vector2.UnitX.RotatedBy((double)(num3 * 6.2831855f), default(Vector2)) * 2f, new Rectangle?(value), color2, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						Main.spriteBatch.Draw(tileFlameData.flameTexture, vector, new Rectangle?(value), Color.White * graveyardVisualIntensity, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
				}
			}
			if (drawData.typeCache == 356 && Main.sundialCooldown == 0)
			{
				Texture2D value2 = TextureAssets.GlowMask[325].Value;
				Rectangle value3;
				value3..ctor((int)drawData.tileFrameX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight);
				Color color3;
				color3..ctor(100, 100, 100, 0);
				int num4 = tileX - (int)(drawData.tileFrameX / 18);
				int num5 = tileY - (int)(drawData.tileFrameY / 18);
				ulong seed = Main.TileFrameSeed ^ (ulong)((long)num4 << 32 | (long)((ulong)num5));
				for (int j = 0; j < 7; j++)
				{
					float num6 = (float)Utils.RandomInt(ref seed, -10, 11) * 0.15f;
					float num7 = (float)Utils.RandomInt(ref seed, -10, 1) * 0.35f;
					Main.spriteBatch.Draw(value2, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num6, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num7) + screenOffset, new Rectangle?(value3), color3, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
				}
			}
			if (drawData.typeCache == 663 && Main.moondialCooldown == 0)
			{
				Texture2D value4 = TextureAssets.GlowMask[335].Value;
				Rectangle value5;
				value5..ctor((int)drawData.tileFrameX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight);
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
				int num106 = tileX - (int)(drawData.tileFrameX / 18);
				int num8 = tileY - (int)(drawData.tileFrameY / 18);
				int num9 = num106 / 2 * (num8 / 3);
				num9 %= Main.cageFrames;
				Main.spriteBatch.Draw(TextureAssets.JellyfishBowl[(int)(drawData.typeCache - 316)].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + Main.jellyfishCageFrame[(int)(drawData.typeCache - 316), num9] * 36, drawData.tileWidth, drawData.tileHeight)), new Color(200, 200, 200, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 149 && drawData.tileFrameX < 54)
			{
				Main.spriteBatch.Draw(TextureAssets.XmasLight.Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(200, 200, 200, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 300 || drawData.typeCache == 302 || drawData.typeCache == 303 || drawData.typeCache == 306)
			{
				int num10 = 9;
				if (drawData.typeCache == 302)
				{
					num10 = 10;
				}
				if (drawData.typeCache == 303)
				{
					num10 = 11;
				}
				if (drawData.typeCache == 306)
				{
					num10 = 12;
				}
				Main.spriteBatch.Draw(TextureAssets.Flames[num10].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight)), new Color(200, 200, 200, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			else if (Main.tileFlame[(int)drawData.typeCache])
			{
				ulong seed2 = Main.TileFrameSeed ^ (ulong)((long)tileX << 32 | (long)((ulong)tileY));
				int typeCache = (int)drawData.typeCache;
				int num11 = 0;
				if (typeCache <= 93)
				{
					if (typeCache <= 35)
					{
						if (typeCache == 4)
						{
							num11 = 0;
							goto IL_133F;
						}
						switch (typeCache)
						{
						case 33:
							break;
						case 34:
							num11 = 3;
							goto IL_133F;
						case 35:
							num11 = 7;
							goto IL_133F;
						default:
							goto IL_133F;
						}
					}
					else
					{
						if (typeCache == 42)
						{
							num11 = 13;
							goto IL_133F;
						}
						if (typeCache == 49)
						{
							num11 = 5;
							goto IL_133F;
						}
						if (typeCache != 93)
						{
							goto IL_133F;
						}
						num11 = 4;
						goto IL_133F;
					}
				}
				else if (typeCache <= 173)
				{
					if (typeCache == 98)
					{
						num11 = 6;
						goto IL_133F;
					}
					if (typeCache != 100 && typeCache != 173)
					{
						goto IL_133F;
					}
					num11 = 2;
					goto IL_133F;
				}
				else if (typeCache != 174)
				{
					if (typeCache == 372)
					{
						num11 = 16;
						goto IL_133F;
					}
					if (typeCache != 646)
					{
						goto IL_133F;
					}
					num11 = 17;
					goto IL_133F;
				}
				num11 = 1;
				IL_133F:
				switch (num11)
				{
				case 1:
				{
					int num105 = (int)(*Main.tile[tileX, tileY].frameY / 22);
					switch (num105)
					{
					case 5:
					case 6:
					case 7:
					case 10:
						for (int num12 = 0; num12 < 7; num12++)
						{
							float num13 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.075f;
							float num14 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.075f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num13, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num14) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						goto IL_37D5;
					case 8:
						for (int num15 = 0; num15 < 7; num15++)
						{
							float num16 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.3f;
							float num17 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.3f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num16, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num17) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						goto IL_37D5;
					case 9:
					case 11:
					case 13:
					case 15:
						break;
					case 12:
						for (int num18 = 0; num18 < 7; num18++)
						{
							float num19 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.1f;
							float num20 = (float)Utils.RandomInt(ref seed2, -10, 1) * 0.15f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num19, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num20) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						goto IL_37D5;
					case 14:
						for (int num21 = 0; num21 < 8; num21++)
						{
							float num22 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.1f;
							float num23 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.1f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num22, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num23) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						goto IL_37D5;
					case 16:
						for (int num24 = 0; num24 < 4; num24++)
						{
							float num25 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.15f;
							float num26 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.15f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num25, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num26) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						goto IL_37D5;
					default:
						if (num105 - 27 <= 1)
						{
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
							goto IL_37D5;
						}
						break;
					}
					for (int num27 = 0; num27 < 7; num27++)
					{
						float num28 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.15f;
						float num29 = (float)Utils.RandomInt(ref seed2, -10, 1) * 0.35f;
						Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num28, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num29) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(100, 100, 100, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
					goto IL_37D5;
				}
				case 2:
				{
					int num105 = (int)(*Main.tile[tileX, tileY].frameY / 36);
					if (num105 <= 6)
					{
						if (num105 == 3)
						{
							for (int num30 = 0; num30 < 3; num30++)
							{
								float num31 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.05f;
								float num32 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.15f;
								Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num31, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num32) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
							}
							goto IL_37D5;
						}
						if (num105 == 6)
						{
							for (int num33 = 0; num33 < 5; num33++)
							{
								float num34 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.15f;
								float num35 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.15f;
								Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num34, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num35) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
							}
							goto IL_37D5;
						}
					}
					else
					{
						switch (num105)
						{
						case 9:
							for (int num36 = 0; num36 < 7; num36++)
							{
								float num37 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.3f;
								float num38 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.3f;
								Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num37, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num38) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(100, 100, 100, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
							}
							goto IL_37D5;
						case 10:
						case 12:
							break;
						case 11:
							for (int num39 = 0; num39 < 7; num39++)
							{
								float num40 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.1f;
								float num41 = (float)Utils.RandomInt(ref seed2, -10, 1) * 0.15f;
								Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num40, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num41) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
							}
							goto IL_37D5;
						case 13:
							for (int num42 = 0; num42 < 8; num42++)
							{
								float num43 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.1f;
								float num44 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.1f;
								Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num43, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num44) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
							}
							goto IL_37D5;
						default:
							if (num105 - 28 <= 1)
							{
								Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
								goto IL_37D5;
							}
							break;
						}
					}
					for (int num45 = 0; num45 < 7; num45++)
					{
						float num46 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.15f;
						float num47 = (float)Utils.RandomInt(ref seed2, -10, 1) * 0.35f;
						Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num46, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num47) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(100, 100, 100, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
					goto IL_37D5;
				}
				case 3:
				{
					int num105 = (int)(*Main.tile[tileX, tileY].frameY / 54);
					switch (num105)
					{
					case 8:
						for (int num48 = 0; num48 < 7; num48++)
						{
							float num49 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.075f;
							float num50 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.075f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num49, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num50) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						goto IL_37D5;
					case 9:
						for (int k = 0; k < 3; k++)
						{
							float num51 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.05f;
							float num52 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.15f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num51, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num52) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						goto IL_37D5;
					case 10:
					case 12:
					case 13:
					case 14:
					case 16:
					case 19:
						break;
					case 11:
						for (int num53 = 0; num53 < 7; num53++)
						{
							float num54 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.3f;
							float num55 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.3f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num54, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num55) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						goto IL_37D5;
					case 15:
						for (int num56 = 0; num56 < 7; num56++)
						{
							float num57 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.1f;
							float num58 = (float)Utils.RandomInt(ref seed2, -10, 1) * 0.15f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num57, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num58) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						goto IL_37D5;
					case 17:
					case 20:
						for (int num59 = 0; num59 < 7; num59++)
						{
							float num60 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.075f;
							float num61 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.075f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num60, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num61) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						goto IL_37D5;
					case 18:
						for (int l = 0; l < 8; l++)
						{
							float num62 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.1f;
							float num63 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.1f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num62, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num63) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						goto IL_37D5;
					default:
						if (num105 - 34 <= 1)
						{
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
							goto IL_37D5;
						}
						break;
					}
					for (int m = 0; m < 7; m++)
					{
						float num64 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.15f;
						float num65 = (float)Utils.RandomInt(ref seed2, -10, 1) * 0.35f;
						Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num64, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num65) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(100, 100, 100, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
					goto IL_37D5;
				}
				case 4:
				{
					int num105 = (int)(*Main.tile[tileX, tileY].frameY / 54);
					switch (num105)
					{
					case 1:
						for (int num66 = 0; num66 < 3; num66++)
						{
							float num67 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.15f;
							float num68 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.15f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num67, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num68) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						goto IL_37D5;
					case 2:
					case 4:
						for (int num69 = 0; num69 < 7; num69++)
						{
							float num70 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.075f;
							float num71 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.075f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num70, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num71) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						goto IL_37D5;
					case 3:
						for (int num72 = 0; num72 < 7; num72++)
						{
							float num73 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.2f;
							float num74 = (float)Utils.RandomInt(ref seed2, -20, 1) * 0.35f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num73, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num74) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(100, 100, 100, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						goto IL_37D5;
					case 5:
						for (int num75 = 0; num75 < 7; num75++)
						{
							float num76 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.3f;
							float num77 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.3f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num76, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num77) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						goto IL_37D5;
					case 6:
					case 7:
					case 8:
					case 10:
					case 11:
						break;
					case 9:
						for (int num78 = 0; num78 < 7; num78++)
						{
							float num79 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.1f;
							float num80 = (float)Utils.RandomInt(ref seed2, -10, 1) * 0.15f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num79, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num80) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						goto IL_37D5;
					case 12:
					{
						float num81 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.01f;
						float num82 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.01f;
						Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num81, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num82) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(Utils.RandomInt(ref seed2, 90, 111), Utils.RandomInt(ref seed2, 90, 111), Utils.RandomInt(ref seed2, 90, 111), 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						goto IL_37D5;
					}
					case 13:
						for (int num83 = 0; num83 < 8; num83++)
						{
							float num84 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.1f;
							float num85 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.1f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num84, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num85) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						goto IL_37D5;
					default:
						if (num105 - 28 <= 1)
						{
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
							goto IL_37D5;
						}
						break;
					}
					for (int num86 = 0; num86 < 7; num86++)
					{
						float num87 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.15f;
						float num88 = (float)Utils.RandomInt(ref seed2, -10, 1) * 0.35f;
						Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num87, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num88) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight)), new Color(100, 100, 100, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
					goto IL_37D5;
				}
				case 5:
				case 6:
					break;
				case 7:
					for (int num89 = 0; num89 < 4; num89++)
					{
						float num90 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.15f;
						float num91 = (float)Utils.RandomInt(ref seed2, -10, 10) * 0.15f;
						num90 = 0f;
						num91 = 0f;
						Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num90, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num91) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
					}
					goto IL_37D5;
				default:
					if (num11 == 13)
					{
						int num92 = (int)(drawData.tileFrameY / 36);
						if (num92 <= 16)
						{
							switch (num92)
							{
							case 1:
							case 3:
							case 6:
							case 8:
								break;
							case 2:
								goto IL_338E;
							case 4:
							case 5:
							case 7:
								goto IL_347D;
							default:
								if (num92 != 16)
								{
									goto IL_347D;
								}
								goto IL_338E;
							}
						}
						else if (num92 != 19)
						{
							switch (num92)
							{
							case 25:
								goto IL_338E;
							case 26:
							case 28:
							case 33:
							case 34:
							case 35:
							case 37:
							case 38:
								goto IL_347D;
							case 27:
							case 29:
							case 30:
							case 31:
							case 32:
							case 36:
							case 39:
								break;
							default:
								goto IL_347D;
							}
						}
						for (int num93 = 0; num93 < 7; num93++)
						{
							float num94 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.15f;
							float num95 = (float)Utils.RandomInt(ref seed2, -10, 1) * 0.35f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num94, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num95) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(100, 100, 100, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						goto IL_37D5;
						IL_338E:
						for (int num96 = 0; num96 < 7; num96++)
						{
							float num97 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.15f;
							float num98 = (float)Utils.RandomInt(ref seed2, -10, 1) * 0.1f;
							Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num97, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num98) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(50, 50, 50, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						}
						goto IL_37D5;
						IL_347D:
						if (num92 == 29)
						{
							for (int num99 = 0; num99 < 7; num99++)
							{
								float num100 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.15f;
								float num101 = (float)Utils.RandomInt(ref seed2, -10, 1) * 0.15f;
								Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num100, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num101) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(25, 25, 25, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
							}
							goto IL_37D5;
						}
						if (num92 - 34 > 1)
						{
							goto IL_37D5;
						}
						Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(75, 75, 75, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
						goto IL_37D5;
					}
					break;
				}
				Color color4;
				color4..ctor(100, 100, 100, 0);
				if (*drawData.tileCache.type == 4)
				{
					int num105 = (int)(*drawData.tileCache.frameY / 22);
					if (num105 != 14)
					{
						if (num105 != 22)
						{
							if (num105 == 23)
							{
								color4..ctor(255, 255, 255, 200);
							}
						}
						else
						{
							color4..ctor(50, 50, 100, 20);
						}
					}
					else
					{
						color4..ctor((float)Main.DiscoR / 255f, (float)Main.DiscoG / 255f, (float)Main.DiscoB / 255f, 0f);
					}
				}
				if (*drawData.tileCache.type == 646)
				{
					color4..ctor(100, 100, 100, 150);
				}
				for (int n = 0; n < 7; n++)
				{
					float num102 = (float)Utils.RandomInt(ref seed2, -10, 11) * 0.15f;
					float num103 = (float)Utils.RandomInt(ref seed2, -10, 1) * 0.35f;
					Main.spriteBatch.Draw(TextureAssets.Flames[num11].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f + num102, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop) + num103) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), color4, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
				}
			}
			IL_37D5:
			if (drawData.typeCache == 144)
			{
				Main.spriteBatch.Draw(TextureAssets.Timer.Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color(200, 200, 200, 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache == 237)
			{
				Main.spriteBatch.Draw(TextureAssets.SunAltar.Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle((int)drawData.tileFrameX, (int)drawData.tileFrameY, drawData.tileWidth, drawData.tileHeight)), new Color((int)(Main.mouseTextColor / 2), (int)(Main.mouseTextColor / 2), (int)(Main.mouseTextColor / 2), 0), 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
			if (drawData.typeCache != 658 || drawData.tileFrameX % 36 != 0 || drawData.tileFrameY % 54 != 0)
			{
				return;
			}
			int num104 = (int)(drawData.tileFrameY / 54);
			if (num104 != 2)
			{
				Texture2D value6 = TextureAssets.GlowMask[334].Value;
				Vector2 vector2;
				vector2..ctor(0f, -10f);
				Vector2 position = new Vector2((float)(tileX * 16 - (int)screenPosition.X) - (float)drawData.tileWidth / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset + vector2;
				Rectangle value7 = value6.Frame(1, 1, 0, 0, 0, 0);
				Color color5;
				color5..ctor((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, 0);
				if (num104 == 0)
				{
					color5 *= 0.75f;
				}
				Main.spriteBatch.Draw(value6, position, new Rectangle?(value7), color5, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
			}
		}

		// Token: 0x060045F4 RID: 17908 RVA: 0x00623070 File Offset: 0x00621270
		private unsafe int GetPalmTreeVariant(int x, int y)
		{
			int num = -1;
			if (Main.tile[x, y].active() && *Main.tile[x, y].type == 53)
			{
				num = 0;
			}
			if (Main.tile[x, y].active() && *Main.tile[x, y].type == 234)
			{
				num = 1;
			}
			if (Main.tile[x, y].active() && *Main.tile[x, y].type == 116)
			{
				num = 2;
			}
			if (Main.tile[x, y].active() && *Main.tile[x, y].type == 112)
			{
				num = 3;
			}
			if (WorldGen.IsPalmOasisTree(x))
			{
				num += 4;
			}
			if (Main.tile[x, y].active() && TileLoader.CanGrowModPalmTree((int)(*Main.tile[x, y].type)))
			{
				num = (int)(8 + *Main.tile[x, y].type) * (WorldGen.IsPalmOasisTree(x) ? -1 : 1);
			}
			return num;
		}

		// Token: 0x060045F5 RID: 17909 RVA: 0x006231AC File Offset: 0x006213AC
		private void DrawSingleTile_SlicedBlock(Vector2 normalTilePosition, int tileX, int tileY, TileDrawInfo drawData)
		{
			Color color = default(Color);
			Vector2 origin = default(Vector2);
			Rectangle value = default(Rectangle);
			Vector3 tileLight = default(Vector3);
			Vector2 position = default(Vector2);
			if (drawData.tileLight.R > this._highQualityLightingRequirement.R || drawData.tileLight.G > this._highQualityLightingRequirement.G || drawData.tileLight.B > this._highQualityLightingRequirement.B)
			{
				Vector3[] slices = drawData.colorSlices;
				Lighting.GetColor9Slice(tileX, tileY, ref slices);
				Vector3 vector = drawData.tileLight.ToVector3();
				Vector3 tint = drawData.colorTint.ToVector3();
				if (drawData.tileCache.fullbrightBlock())
				{
					slices = this._glowPaintColorSlices;
				}
				for (int i = 0; i < 9; i++)
				{
					value.X = 0;
					value.Y = 0;
					value.Width = 4;
					value.Height = 4;
					switch (i)
					{
					case 1:
						value.Width = 8;
						value.X = 4;
						break;
					case 2:
						value.X = 12;
						break;
					case 3:
						value.Height = 8;
						value.Y = 4;
						break;
					case 4:
						value.Width = 8;
						value.Height = 8;
						value.X = 4;
						value.Y = 4;
						break;
					case 5:
						value.X = 12;
						value.Y = 4;
						value.Height = 8;
						break;
					case 6:
						value.Y = 12;
						break;
					case 7:
						value.Width = 8;
						value.Height = 4;
						value.X = 4;
						value.Y = 12;
						break;
					case 8:
						value.X = 12;
						value.Y = 12;
						break;
					}
					tileLight.X = (slices[i].X + vector.X) * 0.5f;
					tileLight.Y = (slices[i].Y + vector.Y) * 0.5f;
					tileLight.Z = (slices[i].Z + vector.Z) * 0.5f;
					TileDrawing.GetFinalLight(drawData.tileCache, drawData.typeCache, ref tileLight, ref tint);
					position.X = normalTilePosition.X + (float)value.X;
					position.Y = normalTilePosition.Y + (float)value.Y;
					value.X += (int)drawData.tileFrameX + drawData.addFrX;
					value.Y += (int)drawData.tileFrameY + drawData.addFrY;
					int num = (int)(tileLight.X * 255f);
					int num2 = (int)(tileLight.Y * 255f);
					int num3 = (int)(tileLight.Z * 255f);
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
					Main.spriteBatch.Draw(drawData.drawTexture, position, new Rectangle?(value), color, 0f, origin, 1f, drawData.tileSpriteEffect, 0f);
				}
				return;
			}
			if (drawData.tileLight.R > this._mediumQualityLightingRequirement.R || drawData.tileLight.G > this._mediumQualityLightingRequirement.G || drawData.tileLight.B > this._mediumQualityLightingRequirement.B)
			{
				Vector3[] slices2 = drawData.colorSlices;
				Lighting.GetColor4Slice(tileX, tileY, ref slices2);
				Vector3 vector2 = drawData.tileLight.ToVector3();
				Vector3 tint2 = drawData.colorTint.ToVector3();
				if (drawData.tileCache.fullbrightBlock())
				{
					slices2 = this._glowPaintColorSlices;
				}
				value.Width = 8;
				value.Height = 8;
				for (int j = 0; j < 4; j++)
				{
					value.X = 0;
					value.Y = 0;
					switch (j)
					{
					case 1:
						value.X = 8;
						break;
					case 2:
						value.Y = 8;
						break;
					case 3:
						value.X = 8;
						value.Y = 8;
						break;
					}
					tileLight.X = (slices2[j].X + vector2.X) * 0.5f;
					tileLight.Y = (slices2[j].Y + vector2.Y) * 0.5f;
					tileLight.Z = (slices2[j].Z + vector2.Z) * 0.5f;
					TileDrawing.GetFinalLight(drawData.tileCache, drawData.typeCache, ref tileLight, ref tint2);
					position.X = normalTilePosition.X + (float)value.X;
					position.Y = normalTilePosition.Y + (float)value.Y;
					value.X += (int)drawData.tileFrameX + drawData.addFrX;
					value.Y += (int)drawData.tileFrameY + drawData.addFrY;
					int num4 = (int)(tileLight.X * 255f);
					int num5 = (int)(tileLight.Y * 255f);
					int num6 = (int)(tileLight.Z * 255f);
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
					Main.spriteBatch.Draw(drawData.drawTexture, position, new Rectangle?(value), color, 0f, origin, 1f, drawData.tileSpriteEffect, 0f);
				}
				return;
			}
			Main.spriteBatch.Draw(drawData.drawTexture, normalTilePosition, new Rectangle?(new Rectangle((int)drawData.tileFrameX + drawData.addFrX, (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight)), drawData.finalColor, 0f, TileDrawing._zero, 1f, drawData.tileSpriteEffect, 0f);
		}

		// Token: 0x060045F6 RID: 17910 RVA: 0x00623818 File Offset: 0x00621A18
		private unsafe void DrawXmasTree(Vector2 screenPosition, Vector2 screenOffset, int tileX, int tileY, TileDrawInfo drawData)
		{
			if (tileY - (int)drawData.tileFrameY > 0 && drawData.tileFrameY == 7 && Main.tile[tileX, tileY - (int)drawData.tileFrameY] != null)
			{
				drawData.tileTop -= (int)(16 * drawData.tileFrameY);
				drawData.tileFrameX = *Main.tile[tileX, tileY - (int)drawData.tileFrameY].frameX;
				drawData.tileFrameY = *Main.tile[tileX, tileY - (int)drawData.tileFrameY].frameY;
			}
			if (drawData.tileFrameX < 10)
			{
				return;
			}
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
			Main.spriteBatch.Draw(TextureAssets.XmasTree[0].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle(0, 0, 64, 128)), color, 0f, TileDrawing._zero, 1f, 0, 0f);
			if (num > 0)
			{
				num--;
				Color color2 = color;
				if (num != 3)
				{
					color2..ctor(255, 255, 255, 255);
				}
				Main.spriteBatch.Draw(TextureAssets.XmasTree[3].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle(66 * num, 0, 64, 128)), color2, 0f, TileDrawing._zero, 1f, 0, 0f);
			}
			if (num2 > 0)
			{
				num2--;
				Main.spriteBatch.Draw(TextureAssets.XmasTree[1].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle(66 * num2, 0, 64, 128)), color, 0f, TileDrawing._zero, 1f, 0, 0f);
			}
			if (num3 > 0)
			{
				num3--;
				Main.spriteBatch.Draw(TextureAssets.XmasTree[2].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle(66 * num3, 0, 64, 128)), color, 0f, TileDrawing._zero, 1f, 0, 0f);
			}
			if (num4 > 0)
			{
				num4--;
				Main.spriteBatch.Draw(TextureAssets.XmasTree[4].Value, new Vector2((float)(tileX * 16 - (int)screenPosition.X) - ((float)drawData.tileWidth - 16f) / 2f, (float)(tileY * 16 - (int)screenPosition.Y + drawData.tileTop)) + screenOffset, new Rectangle?(new Rectangle(66 * num4, 130 * Main.tileFrame[171], 64, 128)), new Color(255, 255, 255, 255), 0f, TileDrawing._zero, 1f, 0, 0f);
			}
		}

		// Token: 0x060045F7 RID: 17911 RVA: 0x00623D14 File Offset: 0x00621F14
		private void DrawTile_MinecartTrack(Vector2 screenPosition, Vector2 screenOffset, int tileX, int tileY, TileDrawInfo drawData)
		{
			drawData.tileLight = TileDrawing.GetFinalLight(drawData.tileCache, drawData.typeCache, drawData.tileLight, drawData.colorTint);
			int frontColor;
			int backColor;
			Minecart.TrackColors(tileX, tileY, drawData.tileCache, out frontColor, out backColor);
			drawData.drawTexture = this.GetTileDrawTexture(drawData.tileCache, tileX, tileY, frontColor);
			Texture2D tileDrawTexture = this.GetTileDrawTexture(drawData.tileCache, tileX, tileY, backColor);
			if (WorldGen.IsRope(tileX, tileY) && Main.tile[tileX, tileY - 1] != null)
			{
				ref ushort type = ref Main.tile[tileX, tileY - 1].type;
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

		// Token: 0x060045F8 RID: 17912 RVA: 0x0062423C File Offset: 0x0062243C
		private unsafe void DrawTile_LiquidBehindTile(bool solidLayer, bool inFrontOfPlayers, int waterStyleOverride, Vector2 screenPosition, Vector2 screenOffset, int tileX, int tileY, Tile tileCache)
		{
			Tile tile = Main.tile[tileX + 1, tileY];
			Tile tile2 = Main.tile[tileX - 1, tileY];
			Tile tile3 = Main.tile[tileX, tileY - 1];
			Tile tile4 = Main.tile[tileX, tileY + 1];
			if (tile == null)
			{
				tile = default(Tile);
				Main.tile[tileX + 1, tileY] = tile;
			}
			if (tile2 == null)
			{
				tile2 = default(Tile);
				Main.tile[tileX - 1, tileY] = tile2;
			}
			if (tile3 == null)
			{
				tile3 = default(Tile);
				Main.tile[tileX, tileY - 1] = tile3;
			}
			if (tile4 == null)
			{
				tile4 = default(Tile);
				Main.tile[tileX, tileY + 1] = tile4;
			}
			if (!tileCache.active() || tileCache.inActive() || this._tileSolidTop[(int)(*tileCache.type)] || (tileCache.halfBrick() && (*tile2.liquid > 160 || *tile.liquid > 160) && Main.instance.waterfallManager.CheckForWaterfall(tileX, tileY)) || (TileID.Sets.BlocksWaterDrawingBehindSelf[(int)(*tileCache.type)] && tileCache.slope() == 0))
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
			if (*tileCache.type == 546 && *tileCache.liquid > 0)
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
				num = (int)(*tileCache.liquid);
			}
			else
			{
				if (*tileCache.liquid > 0 && num4 != 0 && (num4 != 1 || *tileCache.liquid > 160))
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
					if ((int)(*tileCache.liquid) > num)
					{
						num = (int)(*tileCache.liquid);
					}
				}
				if (*tile2.liquid > 0 && num3 != 1 && num3 != 3)
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
					if ((int)(*tile2.liquid) > num)
					{
						num = (int)(*tile2.liquid);
					}
				}
				if (*tile.liquid > 0 && num3 != 2 && num3 != 4)
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
					if ((int)(*tile.liquid) > num)
					{
						num = (int)(*tile.liquid);
					}
				}
				if (*tile3.liquid > 0 && num3 != 3 && num3 != 4)
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
				if (*tile4.liquid > 0 && num3 != 1 && num3 != 2)
				{
					if (*tile4.liquid > 240)
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
			VertexColors vertices;
			Lighting.GetCornerColors(tileX, tileY, out vertices, 1f);
			Vector2 vector;
			vector..ctor((float)(tileX * 16), (float)(tileY * 16));
			Rectangle liquidSize;
			liquidSize..ctor(0, 4, 16, 16);
			if (flag4 && (flag || flag2))
			{
				flag = true;
				flag2 = true;
			}
			if (tileCache.active() && (Main.tileSolidTop[(int)(*tileCache.type)] || !Main.tileSolid[(int)(*tileCache.type)]))
			{
				return;
			}
			if ((!flag3 || (!flag && !flag2)) && (!flag4 || !flag3))
			{
				if (flag3)
				{
					liquidSize..ctor(0, 4, 16, 4);
					if (tileCache.halfBrick() || tileCache.slope() != 0)
					{
						liquidSize..ctor(0, 4, 16, 12);
					}
				}
				else if (flag4 && !flag && !flag2)
				{
					vector..ctor((float)(tileX * 16), (float)(tileY * 16 + 12));
					liquidSize..ctor(0, 4, 16, 4);
				}
				else
				{
					int num8 = (int)((float)(256 - num) / 32f);
					int y = 4;
					if (*tile3.liquid == 0 && (num4 != 0 || !WorldGen.SolidTile(tileX, tileY - 1, false)))
					{
						y = 0;
					}
					int num5 = num8 * 2;
					if (tileCache.slope() != 0)
					{
						vector..ctor((float)(tileX * 16), (float)(tileY * 16 + num5));
						liquidSize..ctor(0, num5, 16, 16 - num5);
					}
					else if ((flag && flag2) || tileCache.halfBrick())
					{
						vector..ctor((float)(tileX * 16), (float)(tileY * 16 + num5));
						liquidSize..ctor(0, y, 16, 16 - num5);
					}
					else if (flag)
					{
						vector..ctor((float)(tileX * 16), (float)(tileY * 16 + num5));
						liquidSize..ctor(0, y, 4, 16 - num5);
					}
					else
					{
						vector..ctor((float)(tileX * 16 + 12), (float)(tileY * 16 + num5));
						liquidSize..ctor(0, y, 4, 16 - num5);
					}
				}
			}
			Vector2 position = vector - screenPosition + screenOffset;
			float num6 = 0.5f;
			if (num2 != 1)
			{
				if (num2 == 11)
				{
					num6 = Math.Max(num6 * 1.7f, 1f);
				}
			}
			else
			{
				num6 = 1f;
			}
			if ((double)tileY <= Main.worldSurface || num6 > 1f)
			{
				num6 = 1f;
				if (*tileCache.wall == 21)
				{
					num6 = 0.9f;
				}
				else if (*tileCache.wall > 0)
				{
					num6 = 0.6f;
				}
			}
			if (tileCache.halfBrick() && *tile3.liquid > 0 && *tileCache.wall > 0)
			{
				num6 = 0f;
			}
			if (num3 == 4 && *tile2.liquid == 0 && !WorldGen.SolidTile(tileX - 1, tileY, false))
			{
				num6 = 0f;
			}
			if (num3 == 3 && *tile.liquid == 0 && !WorldGen.SolidTile(tileX + 1, tileY, false))
			{
				num6 = 0f;
			}
			vertices.BottomLeftColor *= num6;
			vertices.BottomRightColor *= num6;
			vertices.TopLeftColor *= num6;
			vertices.TopRightColor *= num6;
			bool flag7 = false;
			if (flag6)
			{
				int totalCount = LoaderManager.Get<WaterStylesLoader>().TotalCount;
				for (int i = 0; i < totalCount; i++)
				{
					if (Main.IsLiquidStyleWater(i) && Main.liquidAlpha[i] > 0f && i != num2)
					{
						this.DrawPartialLiquid(!solidLayer, tileCache, ref position, ref liquidSize, i, ref vertices);
						flag7 = true;
						break;
					}
				}
			}
			VertexColors colors = vertices;
			float num7 = flag7 ? Main.liquidAlpha[num2] : 1f;
			colors.BottomLeftColor *= num7;
			colors.BottomRightColor *= num7;
			colors.TopLeftColor *= num7;
			colors.TopRightColor *= num7;
			if (num2 == 14)
			{
				LiquidRenderer.SetShimmerVertexColors(ref colors, solidLayer ? 0.75f : 1f, tileX, tileY);
			}
			this.DrawPartialLiquid(!solidLayer, tileCache, ref position, ref liquidSize, num2, ref colors);
		}

		// Token: 0x060045F9 RID: 17913 RVA: 0x00624A7C File Offset: 0x00622C7C
		private void CacheSpecialDraws_Part1(int tileX, int tileY, int tileType, int drawDataTileFrameX, int drawDataTileFrameY, bool skipDraw)
		{
			if (tileType == 395)
			{
				Point point;
				point..ctor(tileX, tileY);
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
				Point point2;
				point2..ctor(tileX, tileY);
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
				Point point3;
				point3..ctor(tileX, tileY);
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
				Point point4;
				point4..ctor(tileX, tileY);
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
				Point point5;
				point5..ctor(tileX, tileY);
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

		// Token: 0x060045FA RID: 17914 RVA: 0x00624D7C File Offset: 0x00622F7C
		private void CacheSpecialDraws_Part2(int tileX, int tileY, TileDrawInfo drawData, bool skipDraw)
		{
			if (TileID.Sets.BasicChest[(int)drawData.typeCache])
			{
				Point key;
				key..ctor(tileX, tileY);
				if (drawData.tileFrameX % 36 != 0)
				{
					key.X--;
				}
				if (drawData.tileFrameY % 36 != 0)
				{
					key.Y--;
				}
				if (!this._chestPositions.ContainsKey(key))
				{
					this._chestPositions[key] = Chest.FindChest(key.X, key.Y);
				}
				int num = (int)(drawData.tileFrameX / 18);
				short num6 = drawData.tileFrameY / 18;
				int num2 = (int)(drawData.tileFrameX / 36);
				int num3 = num * 18;
				drawData.addFrX = num3 - (int)drawData.tileFrameX;
				int num4 = (int)(num6 * 18);
				if (this._chestPositions[key] != -1)
				{
					int frame = Main.chest[this._chestPositions[key]].frame;
					if (frame == 1)
					{
						num4 += 38;
					}
					if (frame == 2)
					{
						num4 += 76;
					}
				}
				drawData.addFrY = num4 - (int)drawData.tileFrameY;
				if (num6 != 0)
				{
					drawData.tileHeight = 18;
				}
				if (drawData.typeCache == 21 && (num2 == 48 || num2 == 49))
				{
					drawData.glowSourceRect = new Rectangle(16 * (num % 2), (int)drawData.tileFrameY + drawData.addFrY, drawData.tileWidth, drawData.tileHeight);
				}
			}
			if (drawData.typeCache != 378)
			{
				return;
			}
			Point key2;
			key2..ctor(tileX, tileY);
			if (drawData.tileFrameX % 36 != 0)
			{
				key2.X--;
			}
			if (drawData.tileFrameY % 54 != 0)
			{
				key2.Y -= (int)(drawData.tileFrameY / 18);
			}
			if (!this._trainingDummyTileEntityPositions.ContainsKey(key2))
			{
				this._trainingDummyTileEntityPositions[key2] = TETrainingDummy.Find(key2.X, key2.Y);
			}
			if (this._trainingDummyTileEntityPositions[key2] != -1)
			{
				int npc = ((TETrainingDummy)TileEntity.ByID[this._trainingDummyTileEntityPositions[key2]]).npc;
				if (npc != -1)
				{
					int num5 = Main.npc[npc].frame.Y / 55;
					num5 *= 54;
					num5 += (int)drawData.tileFrameY;
					drawData.addFrY = num5 - (int)drawData.tileFrameY;
				}
			}
		}

		// Token: 0x060045FB RID: 17915 RVA: 0x00624FAC File Offset: 0x006231AC
		private unsafe static Color GetFinalLight(Tile tileCache, ushort typeCache, Color tileLight, Color tint)
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
			else if (TileDrawing.ShouldTileShine(typeCache, *tileCache.frameX))
			{
				tileLight = Main.shine(tileLight, (int)typeCache);
			}
			return tileLight;
		}

		// Token: 0x060045FC RID: 17916 RVA: 0x00625088 File Offset: 0x00623288
		private unsafe static void GetFinalLight(Tile tileCache, ushort typeCache, ref Vector3 tileLight, ref Vector3 tint)
		{
			tileLight *= tint;
			if (tileCache.inActive())
			{
				tileCache.actColor(ref tileLight);
				return;
			}
			if (TileDrawing.ShouldTileShine(typeCache, *tileCache.frameX))
			{
				Main.shine(ref tileLight, (int)typeCache);
			}
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x006250D8 File Offset: 0x006232D8
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

		// Token: 0x060045FE RID: 17918 RVA: 0x0062514C File Offset: 0x0062334C
		internal static bool IsTileDangerous(int tileX, int tileY, Player localPlayer, Tile tileCache, ushort typeCache)
		{
			bool flag = typeCache == 135 || typeCache == 137 || TileID.Sets.Boulders[(int)typeCache] || typeCache == 141 || typeCache == 210 || typeCache == 442 || typeCache == 443 || typeCache == 444 || typeCache == 411 || typeCache == 485 || typeCache == 85 || typeCache == 654 || (typeCache == 314 && Minecart.IsPressurePlate(tileCache));
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
			bool? flag2 = TileLoader.IsTileDangerous(tileX, tileY, (int)typeCache, localPlayer);
			if (flag2 != null)
			{
				return flag2.GetValueOrDefault();
			}
			return flag;
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x006252C7 File Offset: 0x006234C7
		private bool IsTileDrawLayerSolid(ushort typeCache)
		{
			if (TileID.Sets.DrawTileInSolidLayer[(int)typeCache] != null)
			{
				return TileID.Sets.DrawTileInSolidLayer[(int)typeCache].Value;
			}
			return this._tileSolid[(int)typeCache];
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x006252F4 File Offset: 0x006234F4
		public void GetTileOutlineInfo(int x, int y, ushort typeCache, ref Color tileLight, ref Texture2D highlightTexture, ref Color highlightColor)
		{
			bool actuallySelected;
			if (Main.InSmartCursorHighlightArea(x, y, out actuallySelected))
			{
				int num = (int)((tileLight.R + tileLight.G + tileLight.B) / 3);
				if (num > 10)
				{
					highlightTexture = TextureAssets.HighlightMask[(int)typeCache].Value;
					highlightColor = Colors.GetSelectionGlowColor(actuallySelected, num);
				}
			}
		}

		// Token: 0x06004601 RID: 17921 RVA: 0x00625348 File Offset: 0x00623548
		private unsafe void DrawPartialLiquid(bool behindBlocks, Tile tileCache, ref Vector2 position, ref Rectangle liquidSize, int liquidType, ref VertexColors colors)
		{
			int num = (int)tileCache.slope();
			bool flag = !TileID.Sets.BlocksWaterDrawingBehindSelf[(int)(*tileCache.type)];
			if (!behindBlocks)
			{
				flag = false;
			}
			if (flag || num == 0)
			{
				Main.tileBatch.Draw(TextureAssets.Liquid[liquidType].Value, position, new Rectangle?(liquidSize), colors, default(Vector2), 1f, 0);
				return;
			}
			liquidSize.X += 18 * (num - 1);
			switch (num)
			{
			case 1:
				Main.tileBatch.Draw(TextureAssets.LiquidSlope[liquidType].Value, position, new Rectangle?(liquidSize), colors, Vector2.Zero, 1f, 0);
				return;
			case 2:
				Main.tileBatch.Draw(TextureAssets.LiquidSlope[liquidType].Value, position, new Rectangle?(liquidSize), colors, Vector2.Zero, 1f, 0);
				return;
			case 3:
				Main.tileBatch.Draw(TextureAssets.LiquidSlope[liquidType].Value, position, new Rectangle?(liquidSize), colors, Vector2.Zero, 1f, 0);
				return;
			case 4:
				Main.tileBatch.Draw(TextureAssets.LiquidSlope[liquidType].Value, position, new Rectangle?(liquidSize), colors, Vector2.Zero, 1f, 0);
				return;
			default:
				return;
			}
		}

		// Token: 0x06004602 RID: 17922 RVA: 0x006254D3 File Offset: 0x006236D3
		public bool InAPlaceWithWind(int x, int y, int width, int height)
		{
			return WorldGen.InAPlaceWithWind(x, y, width, height);
		}

		// Token: 0x06004603 RID: 17923 RVA: 0x006254E0 File Offset: 0x006236E0
		public unsafe void GetTileDrawData(int x, int y, Tile tileCache, ushort typeCache, ref short tileFrameX, ref short tileFrameY, out int tileWidth, out int tileHeight, out int tileTop, out int halfBrickHeight, out int addFrX, out int addFrY, out SpriteEffects tileSpriteEffect, out Texture2D glowTexture, out Rectangle glowSourceRect, out Color glowColor)
		{
			tileTop = 0;
			tileWidth = 16;
			tileHeight = 16;
			halfBrickHeight = 0;
			addFrY = Main.tileFrame[(int)typeCache] * 38;
			addFrX = 0;
			tileSpriteEffect = 0;
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
					tileSpriteEffect = 1;
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
				if (treeBiome < 7)
				{
					tileFrameX += (short)(176 * (treeBiome + 1));
				}
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
					tileSpriteEffect = 1;
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
					tileSpriteEffect = 1;
				}
				break;
			case 73:
			case 74:
			case 113:
				tileTop = -12;
				tileHeight = 32;
				if (x % 2 == 0)
				{
					tileSpriteEffect = 1;
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
					tileSpriteEffect = 1;
				}
				break;
			case 82:
			case 83:
			case 84:
				tileHeight = 20;
				tileTop = -2;
				if (x % 2 == 0)
				{
					tileSpriteEffect = 1;
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
					int num22 = (int)((tileFrameX - 324) / 18);
					int num23 = (num22 + Main.tileFrame[(int)typeCache]) % 6 - num22;
					addFrX = num23 * 18;
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
				int num24 = (int)(tileFrameY / 2016);
				addFrY -= 2016 * num24;
				addFrX += 72 * num24;
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
				switch (typeCache)
				{
				case 185:
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
						int num25 = (int)(tileFrameX / 1908);
						addFrX -= 1908 * num25;
						addFrY += 18 * num25;
					}
					break;
				case 186:
					if (tileFrameX >= 864 && tileFrameX <= 1170)
					{
						Main.tileShine2[186] = true;
					}
					else
					{
						Main.tileShine2[186] = false;
					}
					break;
				case 187:
				{
					int num26 = (int)(tileFrameX / 1890);
					addFrX -= 1890 * num26;
					addFrY += 36 * num26;
					break;
				}
				}
				break;
			case 207:
				tileTop = 2;
				if (tileFrameY >= 72)
				{
					addFrY = Main.tileFrame[(int)typeCache];
					int num27 = x;
					if (tileFrameX % 36 != 0)
					{
						num27--;
					}
					addFrY += num27 % 6;
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
					bool evil;
					bool good;
					bool crimson;
					WorldGen.GetCactusType(x, y, (int)tileFrameX, (int)tileFrameY, out evil, out good, out crimson);
					if (good)
					{
						tileFrameX += 238;
					}
					if (evil)
					{
						tileFrameX += 204;
					}
					if (crimson)
					{
						tileFrameX += 272;
					}
				}
				if (x % 2 == 0)
				{
					tileSpriteEffect = 1;
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
				int num28 = Main.tileFrame[(int)typeCache] + x % 6;
				if (x % 2 == 0)
				{
					num28 += 3;
				}
				if (x % 3 == 0)
				{
					num28 += 3;
				}
				if (x % 4 == 0)
				{
					num28 += 3;
				}
				while (num28 > 5)
				{
					num28 -= 6;
				}
				addFrX = num28 * 18;
				addFrY = 0;
				if (x % 2 == 0)
				{
					tileSpriteEffect = 1;
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
				int bigAnimalCageFrame = TileDrawing.GetBigAnimalCageFrame(x, y, (int)tileFrameX, (int)tileFrameY);
				if (typeCache <= 414)
				{
					if (typeCache <= 309)
					{
						switch (typeCache)
						{
						case 275:
							goto IL_1C75;
						case 276:
							goto IL_1CAE;
						case 277:
							addFrY = Main.mallardCageFrame[bigAnimalCageFrame] * 54;
							goto IL_22BE;
						case 278:
							addFrY = Main.duckCageFrame[bigAnimalCageFrame] * 54;
							goto IL_22BE;
						case 279:
							break;
						case 280:
							addFrY = Main.blueBirdCageFrame[bigAnimalCageFrame] * 54;
							goto IL_22BE;
						case 281:
							addFrY = Main.redBirdCageFrame[bigAnimalCageFrame] * 54;
							goto IL_22BE;
						default:
							if (typeCache - 296 <= 1)
							{
								addFrY = Main.scorpionCageFrame[0, bigAnimalCageFrame] * 54;
								goto IL_22BE;
							}
							if (typeCache != 309)
							{
								goto IL_22BE;
							}
							addFrY = Main.penguinCageFrame[bigAnimalCageFrame] * 54;
							goto IL_22BE;
						}
					}
					else if (typeCache != 358)
					{
						if (typeCache == 359)
						{
							goto IL_1C75;
						}
						if (typeCache - 413 > 1)
						{
							break;
						}
						goto IL_1CAE;
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
							goto IL_1CAE;
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
					goto IL_22BE;
				case 552:
				case 555:
				case 556:
				case 557:
					goto IL_22BE;
				case 553:
					addFrY = Main.grebeCageFrame[bigAnimalCageFrame] * 54;
					goto IL_22BE;
				case 554:
					addFrY = Main.seagullCageFrame[bigAnimalCageFrame] * 54;
					goto IL_22BE;
				case 558:
				case 559:
					addFrY = Main.seahorseCageFrame[bigAnimalCageFrame] * 54;
					goto IL_22BE;
				default:
					if (typeCache - 599 > 6)
					{
						goto IL_22BE;
					}
					break;
				}
				IL_1C75:
				addFrY = Main.bunnyCageFrame[bigAnimalCageFrame] * 54;
				break;
				IL_1CAE:
				addFrY = Main.squirrelCageFrame[bigAnimalCageFrame] * 54;
				break;
			}
			case 282:
			case 505:
			case 543:
			{
				tileTop = 2;
				Main.critterCage = true;
				int waterAnimalCageFrame5 = TileDrawing.GetWaterAnimalCageFrame(x, y, (int)tileFrameX, (int)tileFrameY);
				addFrY = Main.fishBowlFrame[waterAnimalCageFrame5] * 36;
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
				int smallAnimalCageFrame2 = TileDrawing.GetSmallAnimalCageFrame(x, y, (int)tileFrameX, (int)tileFrameY);
				if (typeCache <= 391)
				{
					if (typeCache > 299)
					{
						if (typeCache <= 339)
						{
							if (typeCache == 310)
							{
								goto IL_1F54;
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
								goto IL_1F1B;
							case 362:
								break;
							case 363:
								goto IL_1F41;
							case 364:
								goto IL_1F54;
							default:
								if (typeCache != 391)
								{
									goto IL_22BE;
								}
								goto IL_1F54;
							}
						}
						addFrY = Main.grasshopperCageFrame[smallAnimalCageFrame2] * 36;
						break;
					}
					if (typeCache <= 286)
					{
						if (typeCache == 285)
						{
							addFrY = Main.snailCageFrame[smallAnimalCageFrame2] * 36;
							break;
						}
						if (typeCache != 286)
						{
							break;
						}
						goto IL_1F08;
					}
					else if (typeCache != 298)
					{
						if (typeCache != 299)
						{
							break;
						}
						goto IL_1F41;
					}
					IL_1F1B:
					addFrY = Main.frogCageFrame[smallAnimalCageFrame2] * 36;
					break;
					IL_1F41:
					addFrY = Main.mouseCageFrame[smallAnimalCageFrame2] * 36;
					break;
				}
				if (typeCache <= 538)
				{
					if (typeCache <= 532)
					{
						if (typeCache - 392 <= 2)
						{
							addFrY = Main.slugCageFrame[(int)(typeCache - 392), smallAnimalCageFrame2] * 36;
							break;
						}
						if (typeCache != 532)
						{
							break;
						}
						addFrY = Main.maggotCageFrame[smallAnimalCageFrame2] * 36;
						break;
					}
					else
					{
						if (typeCache == 533)
						{
							addFrY = Main.ratCageFrame[smallAnimalCageFrame2] * 36;
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
						addFrY = Main.waterStriderCageFrame[smallAnimalCageFrame2] * 36;
						break;
					}
				}
				else
				{
					if (typeCache == 582)
					{
						goto IL_1F08;
					}
					if (typeCache == 619)
					{
						goto IL_1F54;
					}
					if (typeCache != 629)
					{
						break;
					}
				}
				addFrY = Main.ladybugCageFrame[smallAnimalCageFrame2] * 36;
				break;
				IL_1F08:
				addFrY = Main.snail2CageFrame[smallAnimalCageFrame2] * 36;
				break;
				IL_1F54:
				addFrY = Main.wormCageFrame[smallAnimalCageFrame2] * 36;
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
				int waterAnimalCageFrame6 = TileDrawing.GetWaterAnimalCageFrame(x, y, (int)tileFrameX, (int)tileFrameY);
				int num29 = (int)(typeCache - 288);
				if (typeCache == 360 || typeCache == 580 || typeCache == 620)
				{
					num29 = 8;
				}
				addFrY = Main.butterflyCageFrame[num29, waterAnimalCageFrame6] * 36;
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
				int smallAnimalCageFrame3 = TileDrawing.GetSmallAnimalCageFrame(x, y, (int)tileFrameX, (int)tileFrameY);
				int num30 = (int)(typeCache - 316);
				addFrY = Main.jellyfishCageFrame[num30, smallAnimalCageFrame3] * 36;
				break;
			}
			case 323:
			{
				tileWidth = 20;
				tileHeight = 20;
				int palmTreeBiome = this.GetPalmTreeBiome(x, y);
				if (Math.Abs(palmTreeBiome) >= 8)
				{
					tileFrameY = (short)(22 * ((palmTreeBiome < 0) ? 1 : 0));
				}
				else
				{
					tileFrameY = (short)(22 * palmTreeBiome);
				}
				break;
			}
			case 324:
				tileWidth = 20;
				tileHeight = 20;
				tileTop = -2;
				if (x % 2 == 0)
				{
					tileSpriteEffect = 1;
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
				int num31 = (int)(tileFrameX % 36);
				int num32 = (int)(tileFrameY % 54);
				int frameData2;
				if (Animation.GetTemporaryFrame(x - num31 / 18, y - num32 / 18, out frameData2))
				{
					tileFrameX = (short)(36 * frameData2 + num31);
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
				int num33 = 94;
				tileTop = -2;
				if ((int)tileFrameY == num33 - 20 || (int)tileFrameY == num33 * 2 - 20 || tileFrameY == 0 || (int)tileFrameY == num33)
				{
					tileHeight = 18;
				}
				if (tileFrameY != 0 && (int)tileFrameY != num33)
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
				int num34 = Main.tileFrame[(int)typeCache];
				if (tileFrameX >= 54)
				{
					num34 = 0;
				}
				addFrY = num34 * 38;
				break;
			}
			case 406:
			{
				tileHeight = 16;
				if (tileFrameY % 54 >= 36)
				{
					tileHeight = 18;
				}
				int num35 = Main.tileFrame[(int)typeCache];
				if (tileFrameY >= 108)
				{
					num35 = (int)(6 - tileFrameY / 54);
				}
				else if (tileFrameY >= 54)
				{
					num35 = Main.tileFrame[(int)typeCache] - 1;
				}
				addFrY = num35 * 56;
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
				int num36 = (int)(tileFrameX % 36);
				int num37 = (int)(tileFrameY % 38);
				int frameData3;
				if (Animation.GetTemporaryFrame(x - num36 / 18, y - num37 / 18, out frameData3))
				{
					tileFrameY = (short)(38 * frameData3 + num37);
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
				int num38 = Main.tileFrame[(int)typeCache];
				if (tileFrameX >= 54)
				{
					num38 = 0;
				}
				addFrY = num38 * 54;
				break;
			}
			case 453:
			{
				int num39 = Main.tileFrameCounter[(int)typeCache];
				num39 /= 20;
				int num40 = y - (int)(tileFrameY / 18);
				num39 += num40 + x;
				num39 %= 3;
				addFrY = num39 * 54;
				break;
			}
			case 454:
				addFrY = Main.tileFrame[(int)typeCache] * 54;
				break;
			case 455:
			{
				addFrY = 0;
				tileTop = 2;
				int num41 = 1 + Main.tileFrame[(int)typeCache];
				if (!BirthdayParty.PartyIsUp)
				{
					num41 = 0;
				}
				addFrY = num41 * 54;
				break;
			}
			case 456:
			{
				int num42 = Main.tileFrameCounter[(int)typeCache];
				num42 /= 20;
				int num43 = y - (int)(tileFrameY / 18);
				int num44 = x - (int)(tileFrameX / 18);
				num42 += num43 + num44;
				num42 %= 4;
				addFrY = num42 * 54;
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
				int num45 = Main.tileFrameCounter[(int)typeCache];
				num45 /= 5;
				int num46 = y - (int)(tileFrameY / 18);
				int num47 = x - (int)(tileFrameX / 18);
				num45 += num46 + num47;
				num45 %= 4;
				addFrY = num45 * 36;
				break;
			}
			case 489:
			{
				tileTop = 2;
				int num48 = y - (int)(tileFrameY / 18);
				int num49 = x - (int)(tileFrameX / 18);
				if (this.InAPlaceWithWind(num49, num48, 2, 3))
				{
					int num50 = Main.tileFrameCounter[(int)typeCache];
					num50 /= 5;
					num50 += num48 + num49;
					num50 %= 16;
					addFrY = num50 * 54;
				}
				break;
			}
			case 490:
			{
				tileTop = 2;
				int y2 = y - (int)(tileFrameY / 18);
				int x2 = x - (int)(tileFrameX / 18);
				bool flag2 = this.InAPlaceWithWind(x2, y2, 2, 2);
				int num51 = flag2 ? Main.tileFrame[(int)typeCache] : 0;
				int num52 = 0;
				if (flag2)
				{
					if (Math.Abs(Main.WindForVisuals) > 0.5f)
					{
						switch (Main.weatherVaneBobframe)
						{
						case 0:
							num52 = 0;
							break;
						case 1:
							num52 = 1;
							break;
						case 2:
							num52 = 2;
							break;
						case 3:
							num52 = 1;
							break;
						case 4:
							num52 = 0;
							break;
						case 5:
							num52 = -1;
							break;
						case 6:
							num52 = -2;
							break;
						case 7:
							num52 = -1;
							break;
						}
					}
					else
					{
						switch (Main.weatherVaneBobframe)
						{
						case 0:
							num52 = 0;
							break;
						case 1:
							num52 = 1;
							break;
						case 2:
							num52 = 0;
							break;
						case 3:
							num52 = -1;
							break;
						case 4:
							num52 = 0;
							break;
						case 5:
							num52 = 1;
							break;
						case 6:
							num52 = 0;
							break;
						case 7:
							num52 = -1;
							break;
						}
					}
				}
				num51 += num52;
				if (num51 < 0)
				{
					num51 += 12;
				}
				num51 %= 12;
				addFrY = num51 * 36;
				break;
			}
			case 491:
				tileTop = 2;
				addFrX = 54;
				break;
			case 493:
				if (tileFrameY == 0)
				{
					int num53 = Main.tileFrameCounter[(int)typeCache];
					float num54 = Math.Abs(Main.WindForVisuals);
					int num55 = y - (int)(tileFrameY / 18);
					int num56 = x - (int)(tileFrameX / 18);
					if (!this.InAPlaceWithWind(x, num55, 1, 1))
					{
						num54 = 0f;
					}
					if (num54 >= 0.1f)
					{
						if (num54 < 0.5f)
						{
							num53 /= 20;
							num53 += num55 + num56;
							num53 %= 6;
							num53 = ((Main.WindForVisuals >= 0f) ? (num53 + 1) : (6 - num53));
							addFrY = num53 * 36;
						}
						else
						{
							num53 /= 10;
							num53 += num55 + num56;
							num53 %= 6;
							num53 = ((Main.WindForVisuals >= 0f) ? (num53 + 7) : (12 - num53));
							addFrY = num53 * 36;
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
				int num57 = 20;
				int num58 = (Main.tileFrameCounter[(int)typeCache] + x * 11 + y * 27) % (num57 * 8);
				addFrY = 90 * (num58 / num57);
				break;
			}
			case 518:
			{
				int num59 = (int)(*tileCache.liquid / 16);
				num59 -= 3;
				if (WorldGen.SolidTile(x, y - 1, false) && num59 > 8)
				{
					num59 = 8;
				}
				if (*tileCache.liquid == 0)
				{
					Tile tileSafely = Framing.GetTileSafely(x, y + 1);
					if (tileSafely.nactive())
					{
						int num78 = tileSafely.blockType();
						if (num78 != 1)
						{
							if (num78 - 2 <= 1)
							{
								num59 -= 4;
							}
						}
						else
						{
							num59 = -16 + Math.Max(8, (int)(*tileSafely.liquid / 16));
						}
					}
				}
				tileTop -= num59;
				break;
			}
			case 519:
				tileTop = 2;
				if (x % 2 == 0)
				{
					tileSpriteEffect = 1;
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
				int waterAnimalCageFrame7 = TileDrawing.GetWaterAnimalCageFrame(x, y, (int)tileFrameX, (int)tileFrameY);
				int num60 = (int)(typeCache - 521);
				addFrY = Main.dragonflyJarFrame[num60, waterAnimalCageFrame7] * 36;
				break;
			}
			case 529:
			{
				int num61 = y + 1;
				int corruptCount;
				int crimsonCount;
				int hallowedCount;
				WorldGen.GetBiomeInfluence(x, x, num61, num61, out corruptCount, out crimsonCount, out hallowedCount);
				int num62 = corruptCount;
				if (num62 < crimsonCount)
				{
					num62 = crimsonCount;
				}
				if (num62 < hallowedCount)
				{
					num62 = hallowedCount;
				}
				int num63 = (corruptCount == 0 && crimsonCount == 0 && hallowedCount == 0) ? ((x < WorldGen.beachDistance || x > Main.maxTilesX - WorldGen.beachDistance) ? 1 : 0) : ((hallowedCount == num62) ? 2 : ((crimsonCount != num62) ? 4 : 3));
				addFrY += 34 * num63 - (int)tileFrameY;
				tileHeight = 32;
				tileTop = -14;
				if (x % 2 == 0)
				{
					tileSpriteEffect = 1;
				}
				break;
			}
			case 530:
			{
				int num64 = y - (int)(tileFrameY % 36 / 18) + 2;
				int num79 = x - (int)(tileFrameX % 54 / 18);
				int corruptCount2;
				int crimsonCount2;
				int hallowedCount2;
				WorldGen.GetBiomeInfluence(num79, num79 + 3, num64, num64, out corruptCount2, out crimsonCount2, out hallowedCount2);
				int num65 = corruptCount2;
				if (num65 < crimsonCount2)
				{
					num65 = crimsonCount2;
				}
				if (num65 < hallowedCount2)
				{
					num65 = hallowedCount2;
				}
				int num66 = (corruptCount2 != 0 || crimsonCount2 != 0 || hallowedCount2 != 0) ? ((hallowedCount2 == num65) ? 1 : ((crimsonCount2 != num65) ? 3 : 2)) : 0;
				addFrY += 36 * num66;
				tileTop = 2;
				break;
			}
			case 541:
				addFrY = ((!this._shouldShowInvisibleBlocks) ? 90 : 0);
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
					tileSpriteEffect = 1;
				}
				break;
			case 568:
			case 569:
			case 570:
			{
				tileTop = 2;
				Main.critterCage = true;
				int waterAnimalCageFrame8 = TileDrawing.GetWaterAnimalCageFrame(x, y, (int)tileFrameX, (int)tileFrameY);
				addFrY = Main.fairyJarFrame[waterAnimalCageFrame8] * 36;
				break;
			}
			case 571:
				if (x % 2 == 0)
				{
					tileSpriteEffect = 1;
				}
				tileTop = 2;
				break;
			case 572:
			{
				int num67;
				for (num67 = Main.tileFrame[(int)typeCache] + x % 4; num67 > 3; num67 -= 4)
				{
				}
				addFrX = num67 * 18;
				addFrY = 0;
				if (x % 2 == 0)
				{
					tileSpriteEffect = 1;
				}
				break;
			}
			case 579:
			{
				tileWidth = 20;
				tileHeight = 20;
				tileTop -= 2;
				bool flag = (float)(x * 16 + 8) > Main.LocalPlayer.Center.X;
				if (tileFrameX > 0)
				{
					if (flag)
					{
						addFrY = 22;
					}
					else
					{
						addFrY = 0;
					}
				}
				else if (flag)
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
				int frameData4;
				if (Animation.GetTemporaryFrame(x, y, out frameData4))
				{
					addFrY = (int)((short)(18 * frameData4));
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
				int num68 = (int)(tileFrameX % 36);
				int num69 = (int)(tileFrameY % 36);
				int frameData5;
				if (Animation.GetTemporaryFrame(x - num68 / 18, y - num69 / 18, out frameData5))
				{
					addFrY = (int)((short)(36 * frameData5));
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
				int waterAnimalCageFrame9 = TileDrawing.GetWaterAnimalCageFrame(x, y, (int)tileFrameX, (int)tileFrameY);
				addFrY = Main.lavaFishBowlFrame[waterAnimalCageFrame9] * 36;
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
					tileSpriteEffect = 1;
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
					tileSpriteEffect = 1;
				}
				break;
			case 647:
				tileTop = 2;
				break;
			case 648:
			{
				tileTop = 2;
				int num70 = (int)(tileFrameX / 1890);
				addFrX -= 1890 * num70;
				addFrY += 36 * num70;
				break;
			}
			case 649:
			{
				tileTop = 2;
				int num71 = (int)(tileFrameX / 1908);
				addFrX -= 1908 * num71;
				addFrY += 18 * num71;
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
					tileSpriteEffect = 1;
				}
				break;
			case 658:
			{
				tileTop = 2;
				int num78 = (int)(tileFrameY / 54);
				if (num78 != 1)
				{
					if (num78 != 2)
					{
						addFrY = Main.tileFrame[(int)typeCache];
						addFrY *= 54;
					}
					else
					{
						addFrY = Main.tileFrame[(int)typeCache];
						addFrY *= 54;
						addFrY += 972;
					}
				}
				else
				{
					addFrY = Main.tileFrame[(int)typeCache];
					addFrY *= 54;
					addFrY += 486;
				}
				break;
			}
			case 660:
			{
				int num72 = Main.tileFrame[(int)typeCache] + x % 5;
				if (x % 2 == 0)
				{
					num72 += 3;
				}
				if (x % 3 == 0)
				{
					num72 += 3;
				}
				if (x % 4 == 0)
				{
					num72 += 3;
				}
				while (num72 > 4)
				{
					num72 -= 5;
				}
				addFrX = num72 * 18;
				addFrY = 0;
				if (x % 2 == 0)
				{
					tileSpriteEffect = 1;
				}
				break;
			}
			}
			IL_22BE:
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
								goto IL_33FB;
							}
							goto IL_33FB;
						case 11:
						{
							short num80 = tileFrameY / 54;
							if (num80 == 32)
							{
								glowTexture = TextureAssets.GlowMask[58].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 54), tileWidth, tileHeight);
								glowColor = this._martianGlow;
							}
							if (num80 == 33)
							{
								glowTexture = TextureAssets.GlowMask[119].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 54), tileWidth, tileHeight);
								glowColor = this._meteorGlow;
								goto IL_33FB;
							}
							goto IL_33FB;
						}
						case 12:
						case 13:
						case 16:
						case 17:
						case 20:
							goto IL_33FB;
						case 14:
						{
							short num81 = tileFrameX / 54;
							if (num81 == 31)
							{
								glowTexture = TextureAssets.GlowMask[67].Value;
								glowSourceRect = new Rectangle((int)(tileFrameX % 54), (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._martianGlow;
							}
							if (num81 == 32)
							{
								glowTexture = TextureAssets.GlowMask[124].Value;
								glowSourceRect = new Rectangle((int)(tileFrameX % 54), (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._meteorGlow;
								goto IL_33FB;
							}
							goto IL_33FB;
						}
						case 15:
						{
							short num82 = tileFrameY / 40;
							if (num82 == 32)
							{
								glowTexture = TextureAssets.GlowMask[54].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 40), tileWidth, tileHeight);
								glowColor = this._martianGlow;
							}
							if (num82 == 33)
							{
								glowTexture = TextureAssets.GlowMask[116].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 40), tileWidth, tileHeight);
								glowColor = this._meteorGlow;
								goto IL_33FB;
							}
							goto IL_33FB;
						}
						case 18:
						{
							short num83 = tileFrameX / 36;
							if (num83 == 27)
							{
								glowTexture = TextureAssets.GlowMask[69].Value;
								glowSourceRect = new Rectangle((int)(tileFrameX % 36), (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._martianGlow;
							}
							if (num83 == 28)
							{
								glowTexture = TextureAssets.GlowMask[125].Value;
								glowSourceRect = new Rectangle((int)(tileFrameX % 36), (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._meteorGlow;
								goto IL_33FB;
							}
							goto IL_33FB;
						}
						case 19:
						{
							short num84 = tileFrameY / 18;
							if (num84 == 26)
							{
								glowTexture = TextureAssets.GlowMask[65].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 18), tileWidth, tileHeight);
								glowColor = this._martianGlow;
							}
							if (num84 == 27)
							{
								glowTexture = TextureAssets.GlowMask[112].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 18), tileWidth, tileHeight);
								glowColor = this._meteorGlow;
								goto IL_33FB;
							}
							goto IL_33FB;
						}
						case 21:
							break;
						default:
							if (typeCache != 33)
							{
								goto IL_33FB;
							}
							if (tileFrameX / 18 == 0 && tileFrameY / 22 == 26)
							{
								glowTexture = TextureAssets.GlowMask[61].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 22), tileWidth, tileHeight);
								glowColor = this._martianGlow;
								goto IL_33FB;
							}
							goto IL_33FB;
						}
					}
					else if (typeCache != 34)
					{
						if (typeCache != 42)
						{
							goto IL_33FB;
						}
						if (tileFrameY / 36 == 33)
						{
							glowTexture = TextureAssets.GlowMask[63].Value;
							glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 36), tileWidth, tileHeight);
							glowColor = this._martianGlow;
							goto IL_33FB;
						}
						goto IL_33FB;
					}
					else
					{
						if (tileFrameX / 54 == 0 && tileFrameY / 54 == 33)
						{
							glowTexture = TextureAssets.GlowMask[55].Value;
							glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 54), tileWidth, tileHeight);
							glowColor = this._martianGlow;
							goto IL_33FB;
						}
						goto IL_33FB;
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
							short num85 = tileFrameX / 54;
							int num73 = (int)(tileFrameX / 1998);
							addFrX -= 1998 * num73;
							addFrY += 36 * num73;
							if (num85 == 26)
							{
								glowTexture = TextureAssets.GlowMask[64].Value;
								glowSourceRect = new Rectangle((int)(tileFrameX % 54), (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._martianGlow;
							}
							if (num85 == 27)
							{
								glowTexture = TextureAssets.GlowMask[121].Value;
								glowSourceRect = new Rectangle((int)(tileFrameX % 54), (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._meteorGlow;
								goto IL_33FB;
							}
							goto IL_33FB;
						}
						case 88:
						{
							short num86 = tileFrameX / 54;
							int num74 = (int)(tileFrameX / 1998);
							addFrX -= 1998 * num74;
							addFrY += 36 * num74;
							if (num86 == 24)
							{
								glowTexture = TextureAssets.GlowMask[59].Value;
								glowSourceRect = new Rectangle((int)(tileFrameX % 54), (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._martianGlow;
							}
							if (num86 == 25)
							{
								glowTexture = TextureAssets.GlowMask[120].Value;
								glowSourceRect = new Rectangle((int)(tileFrameX % 54), (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._meteorGlow;
								goto IL_33FB;
							}
							goto IL_33FB;
						}
						case 89:
						{
							short num87 = tileFrameX / 54;
							int num75 = (int)(tileFrameX / 1998);
							addFrX -= 1998 * num75;
							addFrY += 36 * num75;
							if (num87 == 29)
							{
								glowTexture = TextureAssets.GlowMask[66].Value;
								glowSourceRect = new Rectangle((int)(tileFrameX % 54), (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._martianGlow;
							}
							if (num87 == 30)
							{
								glowTexture = TextureAssets.GlowMask[123].Value;
								glowSourceRect = new Rectangle((int)(tileFrameX % 54), (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._meteorGlow;
								goto IL_33FB;
							}
							goto IL_33FB;
						}
						case 90:
						{
							short num88 = tileFrameY / 36;
							if (num88 == 27)
							{
								glowTexture = TextureAssets.GlowMask[52].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 36), tileWidth, tileHeight);
								glowColor = this._martianGlow;
							}
							if (num88 == 28)
							{
								glowTexture = TextureAssets.GlowMask[113].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 36), tileWidth, tileHeight);
								glowColor = this._meteorGlow;
								goto IL_33FB;
							}
							goto IL_33FB;
						}
						case 91:
						case 92:
							goto IL_33FB;
						case 93:
						{
							int num89 = (int)(tileFrameY / 54);
							int num76 = (int)(tileFrameY / 1998);
							addFrY -= 1998 * num76;
							addFrX += 36 * num76;
							tileTop += 2;
							if (num89 == 27)
							{
								glowTexture = TextureAssets.GlowMask[62].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 54), tileWidth, tileHeight);
								glowColor = this._martianGlow;
								goto IL_33FB;
							}
							goto IL_33FB;
						}
						default:
							goto IL_33FB;
						}
					}
					else
					{
						short num90 = tileFrameY / 36;
						if (num90 == 27)
						{
							glowTexture = TextureAssets.GlowMask[53].Value;
							glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 36), tileWidth, tileHeight);
							glowColor = this._martianGlow;
						}
						if (num90 == 28)
						{
							glowTexture = TextureAssets.GlowMask[114].Value;
							glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 36), tileWidth, tileHeight);
							glowColor = this._meteorGlow;
							goto IL_33FB;
						}
						goto IL_33FB;
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
							goto IL_33FB;
						}
						goto IL_33FB;
					case 101:
					{
						short num91 = tileFrameX / 54;
						int num77 = (int)(tileFrameX / 1998);
						addFrX -= 1998 * num77;
						addFrY += 72 * num77;
						if (num91 == 28)
						{
							glowTexture = TextureAssets.GlowMask[60].Value;
							glowSourceRect = new Rectangle((int)(tileFrameX % 54), (int)tileFrameY, tileWidth, tileHeight);
							glowColor = this._martianGlow;
						}
						if (num91 == 29)
						{
							glowTexture = TextureAssets.GlowMask[115].Value;
							glowSourceRect = new Rectangle((int)(tileFrameX % 54), (int)tileFrameY, tileWidth, tileHeight);
							glowColor = this._meteorGlow;
							goto IL_33FB;
						}
						goto IL_33FB;
					}
					case 102:
					case 103:
						goto IL_33FB;
					case 104:
					{
						short num92 = tileFrameX / 36;
						tileTop = 2;
						if (num92 == 24)
						{
							glowTexture = TextureAssets.GlowMask[51].Value;
							glowSourceRect = new Rectangle((int)(tileFrameX % 36), (int)tileFrameY, tileWidth, tileHeight);
							glowColor = this._martianGlow;
						}
						if (num92 == 25)
						{
							glowTexture = TextureAssets.GlowMask[118].Value;
							glowSourceRect = new Rectangle((int)(tileFrameX % 36), (int)tileFrameY, tileWidth, tileHeight);
							glowColor = this._meteorGlow;
							goto IL_33FB;
						}
						goto IL_33FB;
					}
					default:
						if (typeCache != 172)
						{
							if (typeCache != 184)
							{
								goto IL_33FB;
							}
							if (*tileCache.frameX == 110)
							{
								glowTexture = TextureAssets.GlowMask[127].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._lavaMossGlow;
							}
							if (*tileCache.frameX == 132)
							{
								glowTexture = TextureAssets.GlowMask[127].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._kryptonMossGlow;
							}
							if (*tileCache.frameX == 154)
							{
								glowTexture = TextureAssets.GlowMask[127].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._xenonMossGlow;
							}
							if (*tileCache.frameX == 176)
							{
								glowTexture = TextureAssets.GlowMask[127].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._argonMossGlow;
							}
							if (*tileCache.frameX == 198)
							{
								glowTexture = TextureAssets.GlowMask[127].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY, tileWidth, tileHeight);
								glowColor = this._violetMossGlow;
							}
							if (*tileCache.frameX == 220)
							{
								glowTexture = TextureAssets.GlowMask[127].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY, tileWidth, tileHeight);
								glowColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
								goto IL_33FB;
							}
							goto IL_33FB;
						}
						else
						{
							short num93 = tileFrameY / 38;
							if (num93 == 28)
							{
								glowTexture = TextureAssets.GlowMask[88].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 38), tileWidth, tileHeight);
								glowColor = this._martianGlow;
							}
							if (num93 == 29)
							{
								glowTexture = TextureAssets.GlowMask[122].Value;
								glowSourceRect = new Rectangle((int)tileFrameX, (int)(tileFrameY % 38), tileWidth, tileHeight);
								glowColor = this._meteorGlow;
								goto IL_33FB;
							}
							goto IL_33FB;
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
							goto IL_33FB;
						}
						glowTexture = TextureAssets.GlowMask[243].Value;
						glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY + addFrY, tileWidth, tileHeight);
						glowColor = new Color(127, 127, 127, 0);
						goto IL_33FB;
					}
				}
				else
				{
					if (typeCache == 467)
					{
						goto IL_2D3E;
					}
					if (typeCache != 468)
					{
						goto IL_33FB;
					}
				}
				short num94 = tileFrameX / 36;
				if (num94 == 48)
				{
					glowTexture = TextureAssets.GlowMask[56].Value;
					glowSourceRect = new Rectangle((int)(tileFrameX % 36), (int)tileFrameY, tileWidth, tileHeight);
					glowColor = this._martianGlow;
				}
				if (num94 == 49)
				{
					glowTexture = TextureAssets.GlowMask[117].Value;
					glowSourceRect = new Rectangle((int)(tileFrameX % 36), (int)tileFrameY, tileWidth, tileHeight);
					glowColor = this._meteorGlow;
					goto IL_33FB;
				}
				goto IL_33FB;
			}
			else if (typeCache <= 580)
			{
				switch (typeCache)
				{
				case 564:
					if (*tileCache.frameX < 36)
					{
						glowTexture = TextureAssets.GlowMask[267].Value;
						glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY + addFrY, tileWidth, tileHeight);
						glowColor = new Color(200, 200, 200, 0) * ((float)Main.mouseTextColor / 255f);
					}
					addFrY = 0;
					goto IL_33FB;
				case 565:
				case 566:
				case 567:
					goto IL_33FB;
				case 568:
					glowTexture = TextureAssets.GlowMask[268].Value;
					glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY + addFrY, tileWidth, tileHeight);
					glowColor = Color.White;
					goto IL_33FB;
				case 569:
					glowTexture = TextureAssets.GlowMask[269].Value;
					glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY + addFrY, tileWidth, tileHeight);
					glowColor = Color.White;
					goto IL_33FB;
				case 570:
					glowTexture = TextureAssets.GlowMask[270].Value;
					glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY + addFrY, tileWidth, tileHeight);
					glowColor = Color.White;
					goto IL_33FB;
				default:
					if (typeCache != 580)
					{
						goto IL_33FB;
					}
					glowTexture = TextureAssets.GlowMask[289].Value;
					glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY + addFrY, tileWidth, tileHeight);
					glowColor = new Color(225, 110, 110, 0);
					goto IL_33FB;
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
					goto IL_33FB;
				case 635:
				case 636:
					goto IL_33FB;
				case 637:
					glowTexture = TextureAssets.Tile[637].Value;
					glowSourceRect = new Rectangle((int)tileFrameX + addFrX, (int)tileFrameY + addFrY, tileWidth, tileHeight);
					glowColor = Color.Lerp(Color.White, color, 0.75f);
					goto IL_33FB;
				case 638:
					glowTexture = TextureAssets.GlowMask[327].Value;
					glowSourceRect = new Rectangle((int)tileFrameX + addFrX, (int)tileFrameY + addFrY, tileWidth, tileHeight - halfBrickHeight);
					glowColor = Color.Lerp(Color.White, color, 0.75f);
					goto IL_33FB;
				default:
					if (typeCache == 656)
					{
						glowTexture = TextureAssets.GlowMask[329].Value;
						glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY + addFrY, tileWidth, tileHeight);
						glowColor = new Color(255, 255, 255, 0) * ((float)Main.mouseTextColor / 255f);
						goto IL_33FB;
					}
					if (typeCache == 657 && tileFrameY >= 54)
					{
						glowTexture = TextureAssets.GlowMask[330].Value;
						glowSourceRect = new Rectangle((int)tileFrameX, (int)tileFrameY + addFrY, tileWidth, tileHeight);
						glowColor = Color.White;
						goto IL_33FB;
					}
					goto IL_33FB;
				}
			}
			IL_2D3E:
			short num95 = tileFrameX / 36;
			if (num95 == 48)
			{
				glowTexture = TextureAssets.GlowMask[56].Value;
				glowSourceRect = new Rectangle((int)(tileFrameX % 36), (int)tileFrameY, tileWidth, tileHeight);
				glowColor = this._martianGlow;
			}
			if (num95 == 49)
			{
				glowTexture = TextureAssets.GlowMask[117].Value;
				glowSourceRect = new Rectangle((int)(tileFrameX % 36), (int)tileFrameY, tileWidth, tileHeight);
				glowColor = this._meteorGlow;
			}
			IL_33FB:
			TileLoader.SetSpriteEffects(x, y, (int)typeCache, ref tileSpriteEffect);
			TileLoader.SetDrawPositions(x, y, ref tileWidth, ref tileTop, ref tileHeight, ref tileFrameX, ref tileFrameY);
			TileLoader.SetAnimationFrame((int)typeCache, x, y, ref addFrX, ref addFrY);
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x00628914 File Offset: 0x00626B14
		private unsafe bool IsWindBlocked(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return tile == null || (*tile.wall > 0 && !WallID.Sets.AllowsWind[(int)(*tile.wall)]) || (double)y > Main.worldSurface;
		}

		// Token: 0x06004605 RID: 17925 RVA: 0x00628964 File Offset: 0x00626B64
		public static int GetWaterAnimalCageFrame(int x, int y, int tileFrameX, int tileFrameY)
		{
			int num3 = x - tileFrameX / 18;
			int num2 = y - tileFrameY / 18;
			return num3 / 2 * (num2 / 3) % Main.cageFrames;
		}

		// Token: 0x06004606 RID: 17926 RVA: 0x0062898C File Offset: 0x00626B8C
		public static int GetSmallAnimalCageFrame(int x, int y, int tileFrameX, int tileFrameY)
		{
			int num3 = x - tileFrameX / 18;
			int num2 = y - tileFrameY / 18;
			return num3 / 3 * (num2 / 3) % Main.cageFrames;
		}

		// Token: 0x06004607 RID: 17927 RVA: 0x006289B4 File Offset: 0x00626BB4
		public static int GetBigAnimalCageFrame(int x, int y, int tileFrameX, int tileFrameY)
		{
			int num3 = x - tileFrameX / 18;
			int num2 = y - tileFrameY / 18;
			return num3 / 6 * (num2 / 4) % Main.cageFrames;
		}

		// Token: 0x06004608 RID: 17928 RVA: 0x006289DC File Offset: 0x00626BDC
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

		// Token: 0x06004609 RID: 17929 RVA: 0x00628AE3 File Offset: 0x00626CE3
		public void ClearCachedTileDraws(bool solidLayer)
		{
			if (solidLayer)
			{
				this._displayDollTileEntityPositions.Clear();
				this._hatRackTileEntityPositions.Clear();
				return;
			}
			this._vineRootsPositions.Clear();
			this._reverseVineRootsPositions.Clear();
		}

		/// <inheritdoc cref="M:Terraria.GameContent.Drawing.TileDrawing.AddSpecialLegacyPoint(System.Int32,System.Int32)" />
		// Token: 0x0600460A RID: 17930 RVA: 0x00628B15 File Offset: 0x00626D15
		public void AddSpecialLegacyPoint(Point p)
		{
			this.AddSpecialLegacyPoint(p.X, p.Y);
		}

		/// <summary>
		/// Registers a tile coordinate to have additional drawing code executed after all tiles are drawn. <see cref="M:Terraria.ModLoader.ModTile.SpecialDraw(System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteBatch)" /> (or <see cref="M:Terraria.ModLoader.GlobalTile.SpecialDraw(System.Int32,System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteBatch)" />) will be called with the same coordinates after all tiles have been rendered. For multitiles, make sure to only call this for the top left corner of the multitile to prevent duplicate draws by checking <see cref="M:Terraria.ObjectData.TileObjectData.IsTopLeft(System.Int32,System.Int32)" /> first. This should be called in <see cref="M:Terraria.ModLoader.ModTile.DrawEffects(System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteBatch,Terraria.DataStructures.TileDrawInfo@)" /> (or <see cref="M:Terraria.ModLoader.GlobalTile.DrawEffects(System.Int32,System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteBatch,Terraria.DataStructures.TileDrawInfo@)" />)
		/// <para /> This is useful for drawing additional visuals which overlap multiple tiles.
		/// <para /> Some examples include how the <see cref="F:Terraria.ID.TileID.LihzahrdAltar" /> draws a sun texture hovering over the tile and how <see cref="F:Terraria.ID.TileID.ItemFrame" /> draws the contained item sprite over the tile.
		/// <para /> This additional drawing will draw to the tile rendering targets, meaning it will render at 15fps and requires adjusting for <see cref="F:Terraria.Main.offScreenRange" />. As mentioned in <see cref="M:Terraria.ModLoader.ModTile.AnimateTile(System.Int32@,System.Int32@)" />, any animation done using this method should stick to animating at multiples of 4 frames to avoid jerky animation. For smoother animations <see cref="M:Terraria.GameContent.Drawing.TileDrawing.AddSpecialPoint(System.Int32,System.Int32,Terraria.GameContent.Drawing.TileDrawing.TileCounterType)" /> can be used with <see cref="F:Terraria.GameContent.Drawing.TileDrawing.TileCounterType.CustomSolid" /> or CustomNonSolid to render at full frame rate.
		/// </summary>
		// Token: 0x0600460B RID: 17931 RVA: 0x00628B29 File Offset: 0x00626D29
		public void AddSpecialLegacyPoint(int x, int y)
		{
			if (this._specialTilesCount >= this._specialTileX.Length)
			{
				return;
			}
			this._specialTileX[this._specialTilesCount] = x;
			this._specialTileY[this._specialTilesCount] = y;
			this._specialTilesCount++;
		}

		// Token: 0x0600460C RID: 17932 RVA: 0x00628B66 File Offset: 0x00626D66
		private void ClearLegacyCachedDraws()
		{
			this._chestPositions.Clear();
			this._trainingDummyTileEntityPositions.Clear();
			this._foodPlatterTileEntityPositions.Clear();
			this._itemFrameTileEntityPositions.Clear();
			this._weaponRackTileEntityPositions.Clear();
			this._specialTilesCount = 0;
		}

		// Token: 0x0600460D RID: 17933 RVA: 0x00628BA8 File Offset: 0x00626DA8
		public Color DrawTiles_GetLightOverride(int j, int i, Tile tileCache, ushort typeCache, short tileFrameX, short tileFrameY, Color tileLight)
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
							byte b2 = tileLight.B = (byte)(245f - (float)Main.mouseTextColor * 1.5f);
							byte b3 = tileLight.G = b2;
							byte a = tileLight.R = b3;
							tileLight.A = a;
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
						byte b4 = (Main.mouseTextColor + tileLight.G * 2) / 3;
						byte b5 = (Main.mouseTextColor + tileLight.B * 2) / 3;
						if (b4 > tileLight.G)
						{
							tileLight.G = b4;
						}
						if (b5 > tileLight.B)
						{
							tileLight.B = b5;
						}
					}
				}
			}
			return tileLight;
		}

		// Token: 0x0600460E RID: 17934 RVA: 0x00628CF4 File Offset: 0x00626EF4
		public unsafe void DrawTiles_EmitParticles(int j, int i, Tile tileCache, ushort typeCache, short tileFrameX, short tileFrameY, Color tileLight)
		{
			bool num = TileDrawing.IsVisible(tileCache);
			TileLoader.EmitParticles(i, j, tileCache, typeCache, tileFrameX, tileFrameY, tileLight, num);
			int leafFrequency = this._leafFrequency;
			leafFrequency /= 4;
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
				Vector2 vector = new Point(i, j).ToWorldCoordinates(8f, 8f);
				int type = 1202;
				float scale = 8f + Main.rand.NextFloat() * 1.6f;
				Vector2 position5 = vector + new Vector2(0f, -18f);
				Vector2 velocity = Main.rand.NextVector2Circular(0.7f, 0.25f) * 0.4f + Main.rand.NextVector2CircularEdge(1f, 0.4f) * 0.1f;
				velocity *= 4f;
				Gore.NewGorePerfect(position5, velocity, type, scale);
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
				Vector2 position2;
				position2..ctor((float)(i * 16 + 16), (float)(j * 16 + 8));
				Vector2 velocity2;
				velocity2..ctor(0f, 0f);
				if (Main.WindForVisuals < 0f)
				{
					velocity2.X = 0f - Main.WindForVisuals;
				}
				int type2 = this._rand.Next(825, 828);
				if (this._rand.Next(4) == 0)
				{
					Gore.NewGore(position2, velocity2, type2, this._rand.NextFloat() * 0.2f + 0.2f);
				}
				else if (this._rand.Next(2) == 0)
				{
					Gore.NewGore(position2, velocity2, type2, this._rand.NextFloat() * 0.3f + 0.3f);
				}
				else
				{
					Gore.NewGore(position2, velocity2, type2, this._rand.NextFloat() * 0.4f + 0.4f);
				}
			}
			else if (typeCache == 452 && tileFrameY == 0 && tileFrameX == 0 && this._rand.Next(3) == 0)
			{
				Vector2 position3;
				position3..ctor((float)(i * 16 + 16), (float)(j * 16 + 8));
				Vector2 velocity3;
				velocity3..ctor(0f, 0f);
				if (Main.WindForVisuals < 0f)
				{
					velocity3.X = 0f - Main.WindForVisuals;
				}
				int num4 = Main.tileFrame[(int)typeCache];
				int type3 = 907 + num4 / 5;
				if (this._rand.Next(2) == 0)
				{
					Gore.NewGore(position3, velocity3, type3, this._rand.NextFloat() * 0.4f + 0.4f);
				}
			}
			if (typeCache == 192 && this._rand.Next(leafFrequency) == 0)
			{
				this.EmitLivingTreeLeaf(i, j, 910);
			}
			if (typeCache == 384 && this._rand.Next(leafFrequency) == 0)
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
			if (!num)
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
				if (*tileCache.frameX == 36 && *tileCache.frameY % 36 == 0 && (int)Main.timeForVisualEffects % 7 == 0 && this._rand.Next(3) == 0)
				{
					int num6 = this._rand.Next(570, 573);
					Vector2 position4;
					position4..ctor((float)(i * 16 + 8), (float)(j * 16 - 8));
					Vector2 velocity4;
					velocity4..ctor(Main.WindForVisuals * 2f, -0.5f);
					velocity4.X *= 1f + (float)this._rand.Next(-50, 51) * 0.01f;
					velocity4.Y *= 1f + (float)this._rand.Next(-50, 51) * 0.01f;
					if (num6 == 572)
					{
						position4.X -= 8f;
					}
					if (num6 == 571)
					{
						position4.X -= 4f;
					}
					Gore.NewGore(position4, velocity4, num6, 0.8f);
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
				if (*tileCache.frameY / 40 == 31 && *tileCache.frameY % 40 == 0)
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
				int num13 = (int)MathHelper.Clamp((float)(*tileCache.frameY / 22), 0f, (float)(TorchID.Count - 1));
				int num14 = TorchID.Dust[num13];
				int num15;
				if (tileFrameX != 22)
				{
					if (tileFrameX != 44)
					{
						num15 = Dust.NewDust(new Vector2((float)(i * 16 + 4), (float)(j * 16)), 4, 4, num14, 0f, 0f, 100, default(Color), 1f);
					}
					else
					{
						num15 = Dust.NewDust(new Vector2((float)(i * 16 + 2), (float)(j * 16)), 4, 4, num14, 0f, 0f, 100, default(Color), 1f);
					}
				}
				else
				{
					num15 = Dust.NewDust(new Vector2((float)(i * 16 + 6), (float)(j * 16)), 4, 4, num14, 0f, 0f, 100, default(Color), 1f);
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
							goto IL_10DE;
						case 20:
							num17 = 59;
							goto IL_10EC;
						}
						num17 = -1;
						goto IL_10EC;
					}
					IL_10DE:
					num17 = 6;
					IL_10EC:
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
						goto IL_121C;
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
							goto IL_121C;
						}
						break;
					}
					num20 = -1;
					IL_121C:
					if (num20 != -1)
					{
						int num21 = Dust.NewDust((tileFrameX == 0) ? ((this._rand.Next(3) != 0) ? new Vector2((float)(i * 16 + 14), (float)(j * 16 + 2)) : new Vector2((float)(i * 16 + 4), (float)(j * 16 + 2))) : ((this._rand.Next(3) != 0) ? new Vector2((float)(i * 16), (float)(j * 16 + 2)) : new Vector2((float)(i * 16 + 6), (float)(j * 16 + 2))), 4, 4, num20, 0f, 0f, 100, default(Color), 1f);
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
							goto IL_165E;
						default:
							if (num25 != 19)
							{
								goto IL_165E;
							}
							break;
						}
					}
					else if (num25 != 21)
					{
						if (num25 != 25)
						{
							goto IL_165E;
						}
						num27 = 59;
						goto IL_1661;
					}
					num27 = 6;
					goto IL_1661;
					IL_165E:
					num27 = -1;
					IL_1661:
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
					if (tileFrameX == 18 && tileFrameY == 18)
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
					if (tileFrameX == 18 && tileFrameY == 18)
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
						if (tileLight.R <= 20 && tileLight.B <= 20 && tileLight.G <= 20)
						{
							return;
						}
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
						if (this._rand.Next(Main.tileShine[(int)typeCache]) >= num36 || ((typeCache == 21 || typeCache == 441) && (tileFrameX < 36 || tileFrameX >= 180) && (tileFrameX < 396 || tileFrameX > 409)) || ((typeCache == 467 || typeCache == 468) && (tileFrameX < 144 || tileFrameX >= 180)))
						{
							return;
						}
						Color newColor = Color.White;
						if (typeCache != 63)
						{
							if (typeCache == 178)
							{
								switch (tileFrameX / 18)
								{
								case 0:
									newColor..ctor(255, 0, 255, 255);
									break;
								case 1:
									newColor..ctor(255, 255, 0, 255);
									break;
								case 2:
									newColor..ctor(0, 0, 255, 255);
									break;
								case 3:
									newColor..ctor(0, 255, 0, 255);
									break;
								case 4:
									newColor..ctor(255, 0, 0, 255);
									break;
								case 5:
									newColor..ctor(255, 255, 255, 255);
									break;
								case 6:
									newColor..ctor(255, 255, 0, 255);
									break;
								}
								int num37 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 43, 0f, 0f, 254, newColor, 0.5f);
								this._dust[num37].velocity *= 0f;
								return;
							}
						}
						else
						{
							newColor..ctor(0, 0, 255, 255);
						}
						if (typeCache == 64)
						{
							newColor..ctor(255, 0, 0, 255);
						}
						if (typeCache == 65)
						{
							newColor..ctor(0, 255, 0, 255);
						}
						if (typeCache == 66)
						{
							newColor..ctor(255, 255, 0, 255);
						}
						if (typeCache == 67)
						{
							newColor..ctor(255, 0, 255, 255);
						}
						if (typeCache == 68)
						{
							newColor..ctor(255, 255, 255, 255);
						}
						if (typeCache == 12 || typeCache == 665)
						{
							newColor..ctor(255, 0, 0, 255);
						}
						if (typeCache == 639)
						{
							newColor..ctor(0, 0, 255, 255);
						}
						if (typeCache == 204)
						{
							newColor..ctor(255, 0, 0, 255);
						}
						if (typeCache == 211)
						{
							newColor..ctor(50, 255, 100, 255);
						}
						int num38 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 43, 0f, 0f, 254, newColor, 0.5f);
						this._dust[num38].velocity *= 0f;
						return;
					}
					else if (Main.tileSolid[(int)(*tileCache.type)] && Main.shimmerAlpha > 0f && (tileLight.R > 20 || tileLight.B > 20 || tileLight.G > 20))
					{
						int num39 = (int)tileLight.R;
						if ((int)tileLight.G > num39)
						{
							num39 = (int)tileLight.G;
						}
						if ((int)tileLight.B > num39)
						{
							num39 = (int)tileLight.B;
						}
						int maxValue = 500;
						if ((float)this._rand.Next(maxValue) < 2f * Main.shimmerAlpha)
						{
							Color white = Color.White;
							float scale2 = ((float)num39 / 255f + 1f) / 2f;
							int num40 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 43, 0f, 0f, 254, white, scale2);
							this._dust[num40].velocity *= 0f;
						}
					}
				}
				return;
			}
		}

		/// <summary>
		/// Emits a single living tree leaf or other gore instance directly below the target tile.<br />
		/// With a 50% chance, also emits a second leaf or other gore instance directly to the side of the target tile, dependent on wind direction.<br />
		/// Used by vanilla's two types of Living Trees, from which this method and its two submethods get their collective name.<br />
		/// </summary>
		/// <param name="i">The X coordinate of the target tile.</param>
		/// <param name="j">The Y coordinate of the target tile.</param>
		/// <param name="leafGoreType">The numerical ID of the leaf or other gore instance that should be spawned.</param>
		// Token: 0x0600460F RID: 17935 RVA: 0x0062AE74 File Offset: 0x00629074
		public void EmitLivingTreeLeaf(int i, int j, int leafGoreType)
		{
			this.EmitLivingTreeLeaf_Below(i, j, leafGoreType);
			if (this._rand.Next(2) == 0)
			{
				this.EmitLivingTreeLeaf_Sideways(i, j, leafGoreType);
			}
		}

		/// <summary>
		/// Emits a single living tree leaf or other gore instance directly below the target tile.<br />
		/// </summary>
		/// <param name="x">The X coordinate of the target tile.</param>
		/// <param name="y">The Y coordinate of the target tile.</param>
		/// <param name="leafGoreType">The numerical ID of the leaf or other gore instance that should be spawned.</param>
		// Token: 0x06004610 RID: 17936 RVA: 0x0062AE98 File Offset: 0x00629098
		public unsafe void EmitLivingTreeLeaf_Below(int x, int y, int leafGoreType)
		{
			Tile tile = Main.tile[x, y + 1];
			if (!WorldGen.SolidTile(tile) && *tile.liquid <= 0)
			{
				float windForVisuals = Main.WindForVisuals;
				if ((windForVisuals >= -0.2f || (!WorldGen.SolidTile(Main.tile[x - 1, y + 1]) && !WorldGen.SolidTile(Main.tile[x - 2, y + 1]))) && (windForVisuals <= 0.2f || (!WorldGen.SolidTile(Main.tile[x + 1, y + 1]) && !WorldGen.SolidTile(Main.tile[x + 2, y + 1]))))
				{
					Gore.NewGorePerfect(new Vector2((float)(x * 16), (float)(y * 16 + 16)), Vector2.Zero, leafGoreType, 1f).Frame.CurrentColumn = Main.tile[x, y].color();
				}
			}
		}

		/// <summary>
		/// Emits a single living tree leaf or other gore instance directly to the side of the target tile, dependent on wind direction.<br />
		/// </summary>
		/// <param name="x">The X coordinate of the target tile.</param>
		/// <param name="y">The Y coordinate of the target tile.</param>
		/// <param name="leafGoreType">The numerical ID of the leaf or other gore instance that should be spawned.</param>
		// Token: 0x06004611 RID: 17937 RVA: 0x0062AF84 File Offset: 0x00629184
		public unsafe void EmitLivingTreeLeaf_Sideways(int x, int y, int leafGoreType)
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
			if (!WorldGen.SolidTile(tile) && *tile.liquid <= 0)
			{
				int num2 = 0;
				if (num == -1)
				{
					num2 = -10;
				}
				Gore.NewGorePerfect(new Vector2((float)(x * 16 + 8 + 4 * num + num2), (float)(y * 16 + 8)), Vector2.Zero, leafGoreType, 1f).Frame.CurrentColumn = Main.tile[x, y].color();
			}
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x0062B024 File Offset: 0x00629224
		private unsafe void EmitLiquidDrops(int j, int i, Tile tileCache, ushort typeCache)
		{
			int num = 60;
			if (typeCache != 374)
			{
				if (typeCache != 375)
				{
					if (typeCache == 461)
					{
						num = 180;
					}
				}
				else
				{
					num = 180;
				}
			}
			else
			{
				num = 120;
			}
			if (*tileCache.liquid != 0 || this._rand.Next(num * 2) != 0)
			{
				return;
			}
			Rectangle rectangle;
			rectangle..ctor(i * 16, j * 16, 16, 16);
			rectangle.X -= 34;
			rectangle.Width += 68;
			rectangle.Y -= 100;
			rectangle.Height = 400;
			for (int k = 0; k < 600; k++)
			{
				if (this._gore[k].active && GoreID.Sets.LiquidDroplet[this._gore[k].type])
				{
					Rectangle value;
					value..ctor((int)this._gore[k].position.X, (int)this._gore[k].position.Y, 16, 16);
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
			else if (Main.waterStyle >= 15)
			{
				type = LoaderManager.Get<WaterStylesLoader>().Get(Main.waterStyle).GetDropletGore();
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

		/// <summary>
		/// Fetches the degree to which wind would/should affect a tile at the given location.
		/// </summary>
		/// <param name="x">The X coordinate of the theoretical target tile.</param>
		/// <param name="y">The Y coordinate of the theoretical target tile.</param>
		/// <param name="windCounter"></param>
		/// <returns>
		/// If <see cref="F:Terraria.Main.SettingsEnabled_TilesSwayInWind" /> is false or the tile is below surface level, 0.<br />
		/// Otherwise, returns a value from 0.08f to 0.18f.
		/// </returns>
		// Token: 0x06004613 RID: 17939 RVA: 0x0062B264 File Offset: 0x00629464
		public float GetWindCycle(int x, int y, double windCounter)
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

		/// <summary>
		/// Determines whether or not the tile at the given location should be able to sway in the wind.
		/// </summary>
		/// <param name="x">The X coordinate of the given tile.</param>
		/// <param name="y">The Y coordinate of the given tile.</param>
		/// <param name="tileCache">The tile to determine the sway-in-wind-ability of.</param>
		/// <returns>
		/// False if something dictates that the tile should NOT be able to sway in the wind; returns true by default.<br />
		/// Vanilla conditions that prevent wind sway are, in this order:<br />
		/// - if <see cref="F:Terraria.Main.SettingsEnabled_TilesSwayInWind" /> is false<br />
		/// - if <see cref="F:Terraria.ID.TileID.Sets.SwaysInWindBasic" /> is false for the tile type of <paramref name="tileCache" /><br />
		/// - if the tile is an Orange Bloodroot
		/// - if the tile is a Pink Prickly Pear on any vanilla cactus variant
		/// </returns>
		// Token: 0x06004614 RID: 17940 RVA: 0x0062B304 File Offset: 0x00629504
		public unsafe bool ShouldSwayInWind(int x, int y, Tile tileCache)
		{
			return Main.SettingsEnabled_TilesSwayInWind && TileID.Sets.SwaysInWindBasic[(int)(*tileCache.type)] && (*tileCache.type != 227 || (*tileCache.frameX != 204 && *tileCache.frameX != 238 && *tileCache.frameX != 408 && *tileCache.frameX != 442 && *tileCache.frameX != 476));
		}

		// Token: 0x06004615 RID: 17941 RVA: 0x0062B38C File Offset: 0x0062958C
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

		// Token: 0x06004616 RID: 17942 RVA: 0x0062B4A0 File Offset: 0x006296A0
		private void EnsureWindGridSize()
		{
			Vector2 unscaledPosition = Main.Camera.UnscaledPosition;
			Vector2 offSet;
			offSet..ctor((float)Main.offScreenRange, (float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				offSet = Vector2.Zero;
			}
			int firstTileX;
			int lastTileX;
			int firstTileY;
			int lastTileY;
			this.GetScreenDrawArea(unscaledPosition, offSet, out firstTileX, out lastTileX, out firstTileY, out lastTileY);
			this._windGrid.SetSize(lastTileX - firstTileX, lastTileY - firstTileY);
		}

		// Token: 0x06004617 RID: 17943 RVA: 0x0062B4FC File Offset: 0x006296FC
		private unsafe void EmitTreeLeaves(int tilePosX, int tilePosY, int grassPosX, int grassPosY)
		{
			if (!this._isActiveAndNotPaused)
			{
				return;
			}
			int treeHeight = grassPosY - tilePosY;
			Tile tile = Main.tile[tilePosX, tilePosY];
			if (*tile.liquid > 0)
			{
				return;
			}
			int num6;
			int passStyle;
			WorldGen.GetTreeLeaf(tilePosX, tile, Main.tile[grassPosX, grassPosY], ref treeHeight, out num6, out passStyle);
			int num;
			if (passStyle <= 913)
			{
				if (passStyle != -1 && passStyle - 912 > 1)
				{
					goto IL_6C;
				}
			}
			else
			{
				if (passStyle - 917 <= 8)
				{
					num = 1;
					goto IL_85;
				}
				if (passStyle != 1278)
				{
					goto IL_6C;
				}
			}
			return;
			IL_6C:
			num = ((passStyle >= 1113 && passStyle <= 1121) ? 1 : 0);
			IL_85:
			bool flag = (byte)num > 0;
			int num2 = this._leafFrequency;
			bool flag2 = tilePosX - grassPosX != 0;
			if (flag)
			{
				num2 /= 2;
			}
			if (!WorldGen.DoesWindBlowAtThisHeight(tilePosY))
			{
				num2 = 10000;
			}
			if (flag2)
			{
				num2 *= 3;
			}
			if (this._rand.Next(num2) != 0)
			{
				return;
			}
			int num3 = 2;
			Vector2 vector;
			vector..ctor((float)(tilePosX * 16 + 8), (float)(tilePosY * 16 + 8));
			if (flag2)
			{
				int num4 = tilePosX - grassPosX;
				vector.X += (float)(num4 * 12);
				int num5 = 0;
				if (*tile.frameY == 220)
				{
					num5 = 1;
				}
				else if (*tile.frameY == 242)
				{
					num5 = 2;
				}
				if (*tile.frameX == 66)
				{
					switch (num5)
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
					switch (num5)
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
				Gore.NewGoreDirect(vector, Utils.RandomVector2(Main.rand, (float)(-(float)num3), (float)num3), passStyle, 0.7f + Main.rand.NextFloat() * 0.6f).Frame.CurrentColumn = Main.tile[tilePosX, tilePosY].color();
			}
		}

		// Token: 0x06004618 RID: 17944 RVA: 0x0062B7AC File Offset: 0x006299AC
		private unsafe void DrawSpecialTilesLegacy(Vector2 screenPosition, Vector2 offSet)
		{
			for (int i = 0; i < this._specialTilesCount; i++)
			{
				int num = this._specialTileX[i];
				int num2 = this._specialTileY[i];
				Tile tile = Main.tile[num, num2];
				ushort type = *tile.type;
				short frameX = *tile.frameX;
				short frameY = *tile.frameY;
				if (type == 237)
				{
					Main.spriteBatch.Draw(TextureAssets.SunOrb.Value, new Vector2((float)(num * 16 - (int)screenPosition.X) + 8f, (float)(num2 * 16 - (int)screenPosition.Y - 36)) + offSet, new Rectangle?(new Rectangle(0, 0, TextureAssets.SunOrb.Width(), TextureAssets.SunOrb.Height())), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, 0), Main.sunCircle, new Vector2((float)(TextureAssets.SunOrb.Width() / 2), (float)(TextureAssets.SunOrb.Height() / 2)), 1f, 0, 0f);
				}
				if (type == 334 && frameX >= 5000)
				{
					short num18 = frameY / 18;
					int num3 = (int)frameX;
					int num4 = 0;
					int num5 = num3 % 5000;
					num5 -= 100;
					while (num3 >= 5000)
					{
						num4++;
						num3 -= 5000;
					}
					int frameX2 = (int)(*Main.tile[num + 1, num2].frameX);
					frameX2 = ((frameX2 < 25000) ? (frameX2 - 10000) : (frameX2 - 25000));
					Item item = new Item();
					item.netDefaults(num5);
					item.Prefix(frameX2);
					Main.instance.LoadItem(item.type);
					Texture2D value = TextureAssets.Item[item.type].Value;
					Rectangle value2 = (Main.itemAnimations[item.type] == null) ? value.Frame(1, 1, 0, 0, 0, 0) : Main.itemAnimations[item.type].GetFrame(value, -1);
					int width = value2.Width;
					int height = value2.Height;
					float num6 = 1f;
					if (width > 40 || height > 40)
					{
						num6 = ((width <= height) ? (40f / (float)height) : (40f / (float)width));
					}
					num6 *= item.scale;
					SpriteEffects effects = 0;
					if (num4 >= 3)
					{
						effects = 1;
					}
					Color color = Lighting.GetColor(num, num2);
					Main.spriteBatch.Draw(value, new Vector2((float)(num * 16 - (int)screenPosition.X + 24), (float)(num2 * 16 - (int)screenPosition.Y + 8)) + offSet, new Rectangle?(value2), Lighting.GetColor(num, num2), 0f, new Vector2((float)(width / 2), (float)(height / 2)), num6, effects, 0f);
					if (item.color != default(Color))
					{
						Main.spriteBatch.Draw(value, new Vector2((float)(num * 16 - (int)screenPosition.X + 24), (float)(num2 * 16 - (int)screenPosition.Y + 8)) + offSet, new Rectangle?(value2), item.GetColor(color), 0f, new Vector2((float)(width / 2), (float)(height / 2)), num6, effects, 0f);
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
						Texture2D value3 = TextureAssets.Item[item3.type].Value;
						Rectangle value4 = (!ItemID.Sets.IsFood[item3.type]) ? value3.Frame(1, 1, 0, 0, 0, 0) : value3.Frame(1, 3, 0, 2, 0, 0);
						int width2 = value4.Width;
						int height2 = value4.Height;
						float num7 = 1f;
						SpriteEffects effects2 = (*tile.frameX == 0) ? 1 : 0;
						Color color3 = Lighting.GetColor(num, num2);
						Color currentColor = color3;
						float scale = 1f;
						ItemSlot.GetItemLight(ref currentColor, ref scale, item3, false);
						num7 *= scale;
						Vector2 position = new Vector2((float)(num * 16 - (int)screenPosition.X + 8), (float)(num2 * 16 - (int)screenPosition.Y + 16)) + offSet;
						position.Y += 2f;
						Vector2 origin;
						origin..ctor((float)(width2 / 2), (float)height2);
						Main.spriteBatch.Draw(value3, position, new Rectangle?(value4), currentColor, 0f, origin, num7, effects2, 0f);
						if (item3.color != default(Color))
						{
							Main.spriteBatch.Draw(value3, position, new Rectangle?(value4), item3.GetColor(color3), 0f, origin, num7, effects2, 0f);
						}
					}
				}
				if (type == 471)
				{
					Item item4 = (TileEntity.ByPosition[new Point16(num, num2)] as TEWeaponsRack).item;
					Texture2D itemTexture;
					Rectangle itemFrame;
					Main.GetItemDrawFrame(item4.type, out itemTexture, out itemFrame);
					int width3 = itemFrame.Width;
					int height3 = itemFrame.Height;
					float num8 = 1f;
					float num9 = 40f;
					if ((float)width3 > num9 || (float)height3 > num9)
					{
						num8 = ((width3 <= height3) ? (num9 / (float)height3) : (num9 / (float)width3));
					}
					num8 *= item4.scale;
					SpriteEffects effects3 = 1;
					if (*tile.frameX < 54)
					{
						effects3 = 0;
					}
					Color color4 = Lighting.GetColor(num, num2);
					Color currentColor2 = color4;
					float scale2 = 1f;
					ItemSlot.GetItemLight(ref currentColor2, ref scale2, item4, false);
					num8 *= scale2;
					Main.spriteBatch.Draw(itemTexture, new Vector2((float)(num * 16 - (int)screenPosition.X + 24), (float)(num2 * 16 - (int)screenPosition.Y + 24)) + offSet, new Rectangle?(itemFrame), currentColor2, 0f, new Vector2((float)(width3 / 2), (float)(height3 / 2)), num8, effects3, 0f);
					if (item4.color != default(Color))
					{
						Main.spriteBatch.Draw(itemTexture, new Vector2((float)(num * 16 - (int)screenPosition.X + 24), (float)(num2 * 16 - (int)screenPosition.Y + 24)) + offSet, new Rectangle?(itemFrame), item4.GetColor(color4), 0f, new Vector2((float)(width3 / 2), (float)(height3 / 2)), num8, effects3, 0f);
					}
				}
				if (type == 412)
				{
					Texture2D value5 = TextureAssets.GlowMask[202].Value;
					int num10 = Main.tileFrame[(int)type] / 60;
					int frameY2 = (num10 + 1) % 4;
					float num11 = (float)(Main.tileFrame[(int)type] % 60) / 60f;
					Color color5;
					color5..ctor(255, 255, 255, 255);
					Main.spriteBatch.Draw(value5, new Vector2((float)(num * 16 - (int)screenPosition.X), (float)(num2 * 16 - (int)screenPosition.Y + 10)) + offSet, new Rectangle?(value5.Frame(1, 4, 0, num10, 0, 0)), color5 * (1f - num11), 0f, Vector2.Zero, 1f, 0, 0f);
					Main.spriteBatch.Draw(value5, new Vector2((float)(num * 16 - (int)screenPosition.X), (float)(num2 * 16 - (int)screenPosition.Y + 10)) + offSet, new Rectangle?(value5.Frame(1, 4, 0, frameY2, 0, 0)), color5 * num11, 0f, Vector2.Zero, 1f, 0, 0f);
				}
				if (type == 620)
				{
					Texture2D value6 = TextureAssets.Extra[202].Value;
					float num19 = (float)(Main.tileFrame[(int)type] % 60) / 60f;
					int num12 = 2;
					Main.critterCage = true;
					int waterAnimalCageFrame = TileDrawing.GetWaterAnimalCageFrame(num, num2, (int)frameX, (int)frameY);
					int num13 = 8;
					int num14 = Main.butterflyCageFrame[num13, waterAnimalCageFrame];
					int num15 = 6;
					float num16 = 1f;
					Rectangle value7;
					value7..ctor(0, 34 * num14, 32, 32);
					Vector2 vector = new Vector2((float)(num * 16 - (int)screenPosition.X), (float)(num2 * 16 - (int)screenPosition.Y + num12)) + offSet;
					Main.spriteBatch.Draw(value6, vector, new Rectangle?(value7), new Color(255, 255, 255, 255), 0f, Vector2.Zero, 1f, 0, 0f);
					for (int j = 0; j < num15; j++)
					{
						Color color6 = new Color(127, 127, 127, 0).MultiplyRGBA(Main.hslToRgb((Main.GlobalTimeWrappedHourly + (float)j / (float)num15) % 1f, 1f, 0.5f, byte.MaxValue));
						color6 *= 1f - num16 * 0.5f;
						color6.A = 0;
						int num17 = 2;
						Vector2 position2 = vector + ((float)j / (float)num15 * 6.2831855f).ToRotationVector2() * ((float)num17 * num16 + 2f);
						Main.spriteBatch.Draw(value6, position2, new Rectangle?(value7), color6, 0f, Vector2.Zero, 1f, 0, 0f);
					}
					Main.spriteBatch.Draw(value6, vector, new Rectangle?(value7), new Color(255, 255, 255, 0) * 0.1f, 0f, Vector2.Zero, 1f, 0, 0f);
				}
				TileLoader.SpecialDraw((int)type, num, num2, Main.spriteBatch);
			}
		}

		// Token: 0x06004619 RID: 17945 RVA: 0x0062C1D4 File Offset: 0x0062A3D4
		private void DrawEntities_DisplayDolls()
		{
			Main.spriteBatch.Begin(1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
			foreach (KeyValuePair<Point, int> displayDollTileEntityPosition in this._displayDollTileEntityPositions)
			{
				TileEntity value;
				if (displayDollTileEntityPosition.Value != -1 && TileEntity.ByPosition.TryGetValue(new Point16(displayDollTileEntityPosition.Key.X, displayDollTileEntityPosition.Key.Y), out value))
				{
					(value as TEDisplayDoll).Draw(displayDollTileEntityPosition.Key.X, displayDollTileEntityPosition.Key.Y);
				}
			}
			Main.spriteBatch.End();
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x0062C2A8 File Offset: 0x0062A4A8
		private void DrawEntities_HatRacks()
		{
			Main.spriteBatch.Begin(1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
			foreach (KeyValuePair<Point, int> hatRackTileEntityPosition in this._hatRackTileEntityPositions)
			{
				TileEntity value;
				if (hatRackTileEntityPosition.Value != -1 && TileEntity.ByPosition.TryGetValue(new Point16(hatRackTileEntityPosition.Key.X, hatRackTileEntityPosition.Key.Y), out value))
				{
					(value as TEHatRack).Draw(hatRackTileEntityPosition.Key.X, hatRackTileEntityPosition.Key.Y);
				}
			}
			Main.spriteBatch.End();
		}

		// Token: 0x0600461B RID: 17947 RVA: 0x0062C37C File Offset: 0x0062A57C
		private unsafe void DrawTrees()
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
				if (!(tile == null) && tile.active())
				{
					ushort type = *tile.type;
					short frameX = *tile.frameX;
					short frameY = *tile.frameY;
					bool flag = *tile.wall > 0;
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
									WorldGen.GetTreeFoliageDataMethod getTreeFoliageDataMethod2;
									if ((getTreeFoliageDataMethod2 = TileDrawing.<>O.<1>__GetGemTreeFoliageData) == null)
									{
										getTreeFoliageDataMethod2 = (TileDrawing.<>O.<1>__GetGemTreeFoliageData = new WorldGen.GetTreeFoliageDataMethod(WorldGen.GetGemTreeFoliageData));
									}
									getTreeFoliageDataMethod = getTreeFoliageDataMethod2;
								}
							}
							else
							{
								flag2 = true;
								WorldGen.GetTreeFoliageDataMethod getTreeFoliageDataMethod3;
								if ((getTreeFoliageDataMethod3 = TileDrawing.<>O.<0>__GetCommonTreeFoliageData) == null)
								{
									getTreeFoliageDataMethod3 = (TileDrawing.<>O.<0>__GetCommonTreeFoliageData = new WorldGen.GetTreeFoliageDataMethod(WorldGen.GetCommonTreeFoliageData));
								}
								getTreeFoliageDataMethod = getTreeFoliageDataMethod3;
							}
						}
						else if (type != 596 && type != 616)
						{
							if (type == 634)
							{
								flag2 = true;
								WorldGen.GetTreeFoliageDataMethod getTreeFoliageDataMethod4;
								if ((getTreeFoliageDataMethod4 = TileDrawing.<>O.<3>__GetAshTreeFoliageData) == null)
								{
									getTreeFoliageDataMethod4 = (TileDrawing.<>O.<3>__GetAshTreeFoliageData = new WorldGen.GetTreeFoliageDataMethod(WorldGen.GetAshTreeFoliageData));
								}
								getTreeFoliageDataMethod = getTreeFoliageDataMethod4;
							}
						}
						else
						{
							flag2 = true;
							WorldGen.GetTreeFoliageDataMethod getTreeFoliageDataMethod5;
							if ((getTreeFoliageDataMethod5 = TileDrawing.<>O.<2>__GetVanityTreeFoliageData) == null)
							{
								getTreeFoliageDataMethod5 = (TileDrawing.<>O.<2>__GetVanityTreeFoliageData = new WorldGen.GetTreeFoliageDataMethod(WorldGen.GetVanityTreeFoliageData));
							}
							getTreeFoliageDataMethod = getTreeFoliageDataMethod5;
						}
						if (flag2 && frameY >= 198 && frameX >= 22)
						{
							int treeFrame = WorldGen.GetTreeFrame(tile);
							if (frameX != 22)
							{
								if (frameX != 44)
								{
									if (frameX == 66)
									{
										int treeStyle = 0;
										int num5 = x;
										int floorY = y;
										int num6 = -1;
										int num22;
										int num23;
										if (!getTreeFoliageDataMethod(x, y, num6, ref treeFrame, ref treeStyle, out floorY, out num22, out num23))
										{
											goto IL_A85;
										}
										this.EmitTreeLeaves(x, y, num5 + num6, floorY);
										if (treeStyle == 14)
										{
											float num7 = (float)this._rand.Next(28, 42) * 0.005f;
											num7 += (float)(270 - (int)Main.mouseTextColor) / 1000f;
											if (tile.color() == 0)
											{
												Lighting.AddLight(x, y, 0.1f, 0.2f + num7 / 2f, 0.7f + num7);
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
										Texture2D treeBranchTexture = this.GetTreeBranchTexture(treeStyle, 0, tileColor);
										Vector2 position = new Vector2((float)(x * 16), (float)(y * 16)) - unscaledPosition.Floor() + zero + new Vector2(0f, 18f);
										float num8 = 0f;
										if (!flag)
										{
											num8 = this.GetWindCycle(x, y, this._treeWindCounter);
										}
										if (num8 < 0f)
										{
											position.X += num8;
										}
										position.X -= Math.Abs(num8) * 2f;
										Color color2 = Lighting.GetColor(x, y);
										if (tile.fullbrightBlock())
										{
											color2 = Color.White;
										}
										Main.spriteBatch.Draw(treeBranchTexture, position, new Rectangle?(new Rectangle(42, treeFrame * 42, 40, 40)), color2, num8 * num4, new Vector2(0f, 30f), 1f, 0, 0f);
										if (type == 634)
										{
											Texture2D value = TextureAssets.GlowMask[317].Value;
											Color white = Color.White;
											Main.spriteBatch.Draw(value, position, new Rectangle?(new Rectangle(42, treeFrame * 42, 40, 40)), white, num8 * num4, new Vector2(0f, 30f), 1f, 0, 0f);
										}
									}
								}
								else
								{
									int treeStyle2 = 0;
									int num9 = x;
									int floorY2 = y;
									int num10 = 1;
									int num22;
									int num23;
									if (!getTreeFoliageDataMethod(x, y, num10, ref treeFrame, ref treeStyle2, out floorY2, out num23, out num22))
									{
										goto IL_A85;
									}
									this.EmitTreeLeaves(x, y, num9 + num10, floorY2);
									if (treeStyle2 == 14)
									{
										float num11 = (float)this._rand.Next(28, 42) * 0.005f;
										num11 += (float)(270 - (int)Main.mouseTextColor) / 1000f;
										if (tile.color() == 0)
										{
											Lighting.AddLight(x, y, 0.1f, 0.2f + num11 / 2f, 0.7f + num11);
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
									Texture2D treeBranchTexture2 = this.GetTreeBranchTexture(treeStyle2, 0, tileColor2);
									Vector2 position2 = new Vector2((float)(x * 16), (float)(y * 16)) - unscaledPosition.Floor() + zero + new Vector2(16f, 12f);
									float num12 = 0f;
									if (!flag)
									{
										num12 = this.GetWindCycle(x, y, this._treeWindCounter);
									}
									if (num12 > 0f)
									{
										position2.X += num12;
									}
									position2.X += Math.Abs(num12) * 2f;
									Color color4 = Lighting.GetColor(x, y);
									if (tile.fullbrightBlock())
									{
										color4 = Color.White;
									}
									Main.spriteBatch.Draw(treeBranchTexture2, position2, new Rectangle?(new Rectangle(0, treeFrame * 42, 40, 40)), color4, num12 * num4, new Vector2(40f, 24f), 1f, 0, 0f);
									if (type == 634)
									{
										Texture2D value2 = TextureAssets.GlowMask[317].Value;
										Color white2 = Color.White;
										Main.spriteBatch.Draw(value2, position2, new Rectangle?(new Rectangle(0, treeFrame * 42, 40, 40)), white2, num12 * num4, new Vector2(40f, 24f), 1f, 0, 0f);
									}
								}
							}
							else
							{
								int treeStyle3 = 0;
								int topTextureFrameWidth3 = 80;
								int topTextureFrameHeight3 = 80;
								int num13 = 0;
								int grassPosX = x + num13;
								int floorY3 = y;
								if (!getTreeFoliageDataMethod(x, y, num13, ref treeFrame, ref treeStyle3, out floorY3, out topTextureFrameWidth3, out topTextureFrameHeight3))
								{
									goto IL_A85;
								}
								this.EmitTreeLeaves(x, y, grassPosX, floorY3);
								if (treeStyle3 == 14)
								{
									float num14 = (float)this._rand.Next(28, 42) * 0.005f;
									num14 += (float)(270 - (int)Main.mouseTextColor) / 1000f;
									if (tile.color() == 0)
									{
										Lighting.AddLight(x, y, 0.1f, 0.2f + num14 / 2f, 0.7f + num14);
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
								Texture2D treeTopTexture = this.GetTreeTopTexture(treeStyle3, 0, tileColor3);
								Vector2 vector = vector = new Vector2((float)(x * 16 - (int)unscaledPosition.X + 8), (float)(y * 16 - (int)unscaledPosition.Y + 16)) + zero;
								float num15 = 0f;
								if (!flag)
								{
									num15 = this.GetWindCycle(x, y, this._treeWindCounter);
								}
								vector.X += num15 * 2f;
								vector.Y += Math.Abs(num15) * 2f;
								Color color6 = Lighting.GetColor(x, y);
								if (tile.fullbrightBlock())
								{
									color6 = Color.White;
								}
								Main.spriteBatch.Draw(treeTopTexture, vector, new Rectangle?(new Rectangle(treeFrame * (topTextureFrameWidth3 + 2), 0, topTextureFrameWidth3, topTextureFrameHeight3)), color6, num15 * num3, new Vector2((float)(topTextureFrameWidth3 / 2), (float)topTextureFrameHeight3), 1f, 0, 0f);
								if (type == 634)
								{
									Texture2D value3 = TextureAssets.GlowMask[316].Value;
									Color white3 = Color.White;
									Main.spriteBatch.Draw(value3, vector, new Rectangle?(new Rectangle(treeFrame * (topTextureFrameWidth3 + 2), 0, topTextureFrameWidth3, topTextureFrameHeight3)), white3, num15 * num3, new Vector2((float)(topTextureFrameWidth3 / 2), (float)topTextureFrameHeight3), 1f, 0, 0f);
								}
							}
						}
						if (type == 323 && frameX >= 88 && frameX <= 132)
						{
							int num16 = 0;
							if (frameX != 110)
							{
								if (frameX == 132)
								{
									num16 = 2;
								}
							}
							else
							{
								num16 = 1;
							}
							int treeTextureIndex = 15;
							int num17 = 80;
							int num18 = 80;
							int num19 = 32;
							int num20 = 0;
							int palmTreeBiome = this.GetPalmTreeBiome(x, y);
							int y2 = palmTreeBiome * 82;
							if (palmTreeBiome >= 4 && palmTreeBiome <= 7)
							{
								treeTextureIndex = 21;
								num17 = 114;
								num18 = 98;
								y2 = (palmTreeBiome - 4) * 98;
								num19 = 48;
								num20 = 2;
							}
							if (Math.Abs(palmTreeBiome) >= 8)
							{
								y2 = 0;
								if (palmTreeBiome < 0)
								{
									num17 = 114;
									num18 = 98;
									num19 = 48;
									num20 = 2;
								}
								treeTextureIndex = Math.Abs(palmTreeBiome) - 8;
								treeTextureIndex *= -2;
								if (palmTreeBiome < 0)
								{
									treeTextureIndex--;
								}
							}
							int frameY2 = (int)(*Main.tile[x, y].frameY);
							byte tileColor4 = tile.color();
							Texture2D treeTopTexture2 = this.GetTreeTopTexture(treeTextureIndex, palmTreeBiome, tileColor4);
							Vector2 position3 = new Vector2((float)(x * 16 - (int)unscaledPosition.X - num19 + frameY2 + num17 / 2), (float)(y * 16 - (int)unscaledPosition.Y + 16 + num20)) + zero;
							float num21 = 0f;
							if (!flag)
							{
								num21 = this.GetWindCycle(x, y, this._treeWindCounter);
							}
							position3.X += num21 * 2f;
							position3.Y += Math.Abs(num21) * 2f;
							Color color7 = Lighting.GetColor(x, y);
							if (tile.fullbrightBlock())
							{
								color7 = Color.White;
							}
							Main.spriteBatch.Draw(treeTopTexture2, position3, new Rectangle?(new Rectangle(num16 * (num17 + 2), y2, num17, num18)), color7, num21 * num3, new Vector2((float)(num17 / 2), (float)num18), 1f, 0, 0f);
						}
					}
					catch
					{
					}
				}
				IL_A85:;
			}
		}

		// Token: 0x0600461C RID: 17948 RVA: 0x0062CE38 File Offset: 0x0062B038
		private Texture2D GetTreeTopTexture(int treeTextureIndex, int treeTextureStyle, byte tileColor)
		{
			Texture2D texture2D = this._paintSystem.TryGetTreeTopAndRequestIfNotReady(treeTextureIndex, treeTextureStyle, (int)tileColor);
			if (texture2D == null)
			{
				if (treeTextureIndex < 0 || treeTextureIndex >= 100)
				{
					treeTextureIndex = 0;
				}
				texture2D = TextureAssets.TreeTop[treeTextureIndex].Value;
			}
			return texture2D;
		}

		// Token: 0x0600461D RID: 17949 RVA: 0x0062CE74 File Offset: 0x0062B074
		private Texture2D GetTreeBranchTexture(int treeTextureIndex, int treeTextureStyle, byte tileColor)
		{
			Texture2D texture2D = this._paintSystem.TryGetTreeBranchAndRequestIfNotReady(treeTextureIndex, treeTextureStyle, (int)tileColor);
			if (texture2D == null)
			{
				if (treeTextureIndex < 0 || treeTextureIndex >= 100)
				{
					treeTextureIndex = 0;
				}
				texture2D = TextureAssets.TreeBranch[treeTextureIndex].Value;
			}
			return texture2D;
		}

		// Token: 0x0600461E RID: 17950 RVA: 0x0062CEB0 File Offset: 0x0062B0B0
		private unsafe void DrawGrass()
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
				if (!(tile == null) && tile.active() && TileDrawing.IsVisible(tile))
				{
					ushort type = *tile.type;
					short tileFrameX = *tile.frameX;
					short tileFrameY = *tile.frameY;
					int tileWidth;
					int tileHeight;
					int tileTop;
					int halfBrickHeight;
					int addFrX;
					int addFrY;
					SpriteEffects tileSpriteEffect;
					Texture2D glowTexture;
					Rectangle glowSourceRect;
					Color glowColor;
					this.GetTileDrawData(x, y, tile, type, ref tileFrameX, ref tileFrameY, out tileWidth, out tileHeight, out tileTop, out halfBrickHeight, out addFrX, out addFrY, out tileSpriteEffect, out glowTexture, out glowSourceRect, out glowColor);
					bool flag = this._rand.Next(4) == 0;
					Color tileLight = Lighting.GetColor(x, y);
					this.DrawAnimatedTile_AdjustForVisionChangers(x, y, tile, type, tileFrameX, tileFrameY, ref tileLight, flag);
					tileLight = this.DrawTiles_GetLightOverride(y, x, tile, type, tileFrameX, tileFrameY, tileLight);
					if (this._isActiveAndNotPaused && flag)
					{
						this.DrawTiles_EmitParticles(y, x, tile, type, tileFrameX, tileFrameY, tileLight);
					}
					if (type == 83 && this.IsAlchemyPlantHarvestable((int)(tileFrameX / 18)))
					{
						type = 84;
						Main.instance.LoadTiles((int)type);
					}
					Vector2 position = new Vector2((float)(x * 16 - (int)unscaledPosition.X + 8), (float)(y * 16 - (int)unscaledPosition.Y + 16)) + zero;
					double grassWindCounter = this._grassWindCounter;
					float num3 = this.GetWindCycle(x, y, this._grassWindCounter);
					if (!WallID.Sets.AllowsWind[(int)(*tile.wall)])
					{
						num3 = 0f;
					}
					if (!this.InAPlaceWithWind(x, y, 1, 1))
					{
						num3 = 0f;
					}
					num3 += this.GetWindGridPush(x, y, 20, 0.35f);
					position.X += num3 * 1f;
					position.Y += Math.Abs(num3) * 1f;
					Texture2D tileDrawTexture = this.GetTileDrawTexture(tile, x, y);
					if (tileDrawTexture != null)
					{
						Main.spriteBatch.Draw(tileDrawTexture, position, new Rectangle?(new Rectangle((int)tileFrameX + addFrX, (int)tileFrameY + addFrY, tileWidth, tileHeight - halfBrickHeight)), tileLight, num3 * 0.1f, new Vector2((float)(tileWidth / 2), (float)(16 - halfBrickHeight - tileTop)), 1f, tileSpriteEffect, 0f);
						if (glowTexture != null)
						{
							Main.spriteBatch.Draw(glowTexture, position, new Rectangle?(glowSourceRect), glowColor, num3 * 0.1f, new Vector2((float)(tileWidth / 2), (float)(16 - halfBrickHeight - tileTop)), 1f, tileSpriteEffect, 0f);
						}
					}
				}
			}
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x0062D170 File Offset: 0x0062B370
		private unsafe void DrawAnyDirectionalGrass()
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
				if (!(tile == null) && tile.active() && TileDrawing.IsVisible(tile))
				{
					ushort type = *tile.type;
					short tileFrameX = *tile.frameX;
					short tileFrameY = *tile.frameY;
					int tileWidth;
					int tileHeight;
					int tileTop;
					int halfBrickHeight;
					int addFrX;
					int addFrY;
					SpriteEffects tileSpriteEffect;
					Texture2D glowTexture;
					Rectangle rectangle;
					Color glowColor;
					this.GetTileDrawData(x, y, tile, type, ref tileFrameX, ref tileFrameY, out tileWidth, out tileHeight, out tileTop, out halfBrickHeight, out addFrX, out addFrY, out tileSpriteEffect, out glowTexture, out rectangle, out glowColor);
					bool flag = this._rand.Next(4) == 0;
					Color tileLight = Lighting.GetColor(x, y);
					this.DrawAnimatedTile_AdjustForVisionChangers(x, y, tile, type, tileFrameX, tileFrameY, ref tileLight, flag);
					tileLight = this.DrawTiles_GetLightOverride(y, x, tile, type, tileFrameX, tileFrameY, tileLight);
					if (this._isActiveAndNotPaused && flag)
					{
						this.DrawTiles_EmitParticles(y, x, tile, type, tileFrameX, tileFrameY, tileLight);
					}
					if (type == 83 && this.IsAlchemyPlantHarvestable((int)(tileFrameX / 18)))
					{
						type = 84;
						Main.instance.LoadTiles((int)type);
					}
					Vector2 position = new Vector2((float)(x * 16 - (int)unscaledPosition.X), (float)(y * 16 - (int)unscaledPosition.Y)) + zero;
					double grassWindCounter = this._grassWindCounter;
					float num3 = this.GetWindCycle(x, y, this._grassWindCounter);
					if (!WallID.Sets.AllowsWind[(int)(*tile.wall)])
					{
						num3 = 0f;
					}
					if (!this.InAPlaceWithWind(x, y, 1, 1))
					{
						num3 = 0f;
					}
					float pushX;
					float pushY;
					this.GetWindGridPush2Axis(x, y, 20, 0.35f, out pushX, out pushY);
					int num4 = 1;
					int num5 = 0;
					Vector2 origin;
					origin..ctor((float)(tileWidth / 2), (float)(16 - halfBrickHeight - tileTop));
					switch (tileFrameY / 54)
					{
					case 0:
						num4 = 1;
						num5 = 0;
						origin..ctor((float)(tileWidth / 2), (float)(16 - halfBrickHeight - tileTop));
						position.X += 8f;
						position.Y += 16f;
						position.X += num3;
						position.Y += Math.Abs(num3);
						break;
					case 1:
						num3 *= -1f;
						num4 = -1;
						num5 = 0;
						origin..ctor((float)(tileWidth / 2), (float)(-(float)tileTop));
						position.X += 8f;
						position.X += 0f - num3;
						position.Y += 0f - Math.Abs(num3);
						break;
					case 2:
						num4 = 0;
						num5 = 1;
						origin..ctor(2f, (float)((16 - halfBrickHeight - tileTop) / 2));
						position.Y += 8f;
						position.Y += num3;
						position.X += 0f - Math.Abs(num3);
						break;
					case 3:
						num3 *= -1f;
						num4 = 0;
						num5 = -1;
						origin..ctor(14f, (float)((16 - halfBrickHeight - tileTop) / 2));
						position.X += 16f;
						position.Y += 8f;
						position.Y += 0f - num3;
						position.X += Math.Abs(num3);
						break;
					}
					num3 += pushX * (float)num4 + pushY * (float)num5;
					Texture2D tileDrawTexture = this.GetTileDrawTexture(tile, x, y);
					if (tileDrawTexture != null)
					{
						Main.spriteBatch.Draw(tileDrawTexture, position, new Rectangle?(new Rectangle((int)tileFrameX + addFrX, (int)tileFrameY + addFrY, tileWidth, tileHeight - halfBrickHeight)), tileLight, num3 * 0.1f, origin, 1f, tileSpriteEffect, 0f);
						if (glowTexture != null)
						{
							Main.spriteBatch.Draw(glowTexture, position, new Rectangle?(new Rectangle((int)tileFrameX + addFrX, (int)tileFrameY + addFrY, tileWidth, tileHeight - halfBrickHeight)), glowColor, num3 * 0.1f, origin, 1f, tileSpriteEffect, 0f);
						}
					}
				}
			}
		}

		// Token: 0x06004620 RID: 17952 RVA: 0x0062D5C0 File Offset: 0x0062B7C0
		public void DrawAnimatedTile_AdjustForVisionChangers(int i, int j, Tile tileCache, ushort typeCache, short tileFrameX, short tileFrameY, ref Color tileLight, bool canDoDust)
		{
			if (this._localPlayer.dangerSense && TileDrawing.IsTileDangerous(i, j, this._localPlayer, tileCache, typeCache))
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
			if (this._localPlayer.findTreasure && Main.IsTileSpelunkable(i, j, typeCache, tileFrameX, tileFrameY))
			{
				if (tileLight.R < 200)
				{
					tileLight.R = 200;
				}
				if (tileLight.G < 170)
				{
					tileLight.G = 170;
				}
				if (this._isActiveAndNotPaused && this._rand.Next(60) == 0 && canDoDust)
				{
					int num2 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 204, 0f, 0f, 150, default(Color), 0.3f);
					this._dust[num2].fadeIn = 1f;
					this._dust[num2].velocity *= 0.1f;
					this._dust[num2].noLight = true;
				}
			}
			if (!this._localPlayer.biomeSight)
			{
				return;
			}
			Color sightColor = Color.White;
			if (Main.IsTileBiomeSightable(i, j, typeCache, tileFrameX, tileFrameY, ref sightColor))
			{
				if (tileLight.R < sightColor.R)
				{
					tileLight.R = sightColor.R;
				}
				if (tileLight.G < sightColor.G)
				{
					tileLight.G = sightColor.G;
				}
				if (tileLight.B < sightColor.B)
				{
					tileLight.B = sightColor.B;
				}
				if (this._isActiveAndNotPaused && canDoDust && this._rand.Next(480) == 0)
				{
					Color newColor = sightColor;
					int num3 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 267, 0f, 0f, 150, newColor, 0.3f);
					this._dust[num3].noGravity = true;
					this._dust[num3].fadeIn = 1f;
					this._dust[num3].velocity *= 0.1f;
					this._dust[num3].noLightEmittence = true;
				}
			}
		}

		/// <summary>
		/// Determines how much wind should affect a theoretical tile at the target location on the current update tick.
		/// </summary>
		/// <param name="i">The X coordinate of the theoretical target tile.</param>
		/// <param name="j">The Y coordinate of the theoretical target tile.</param>
		/// <param name="pushAnimationTimeTotal">The total amount of time, in ticks, that a wind push cycle for the theoretical target tile should last for.</param>
		/// <param name="pushForcePerFrame">The amount which wind should affect the theoretical target tile per frame.</param>
		/// <returns>
		/// The degree to which wind should affect the theoretical target tile, represented as a float.
		/// </returns>
		// Token: 0x06004621 RID: 17953 RVA: 0x0062D8F0 File Offset: 0x0062BAF0
		public float GetWindGridPush(int i, int j, int pushAnimationTimeTotal, float pushForcePerFrame)
		{
			int windTimeLeft;
			int directionX;
			int num;
			this._windGrid.GetWindTime(i, j, pushAnimationTimeTotal, out windTimeLeft, out directionX, out num);
			if (windTimeLeft >= pushAnimationTimeTotal / 2)
			{
				return (float)(pushAnimationTimeTotal - windTimeLeft) * pushForcePerFrame * (float)directionX;
			}
			return (float)windTimeLeft * pushForcePerFrame * (float)directionX;
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x0062D92C File Offset: 0x0062BB2C
		private void GetWindGridPush2Axis(int i, int j, int pushAnimationTimeTotal, float pushForcePerFrame, out float pushX, out float pushY)
		{
			int windTimeLeft;
			int directionX;
			int directionY;
			this._windGrid.GetWindTime(i, j, pushAnimationTimeTotal, out windTimeLeft, out directionX, out directionY);
			if (windTimeLeft >= pushAnimationTimeTotal / 2)
			{
				pushX = (float)(pushAnimationTimeTotal - windTimeLeft) * pushForcePerFrame * (float)directionX;
				pushY = (float)(pushAnimationTimeTotal - windTimeLeft) * pushForcePerFrame * (float)directionY;
				return;
			}
			pushX = (float)windTimeLeft * pushForcePerFrame * (float)directionX;
			pushY = (float)windTimeLeft * pushForcePerFrame * (float)directionY;
		}

		/// <summary>
		/// Determines how much wind should affect a theoretical tile at the target location on the current update tick.<br />
		/// More complex version of <see cref="M:Terraria.GameContent.Drawing.TileDrawing.GetWindGridPush(System.Int32,System.Int32,System.Int32,System.Single)" />.
		/// </summary>
		/// <param name="i">The X coordinate of the theoretical target tile.</param>
		/// <param name="j">The Y coordinate of the theoretical target tile.</param>
		/// <param name="pushAnimationTimeTotal">The total amount of time, in ticks, that a wind push cycle for the theoretical target tile should last for.</param>
		/// <param name="totalPushForce"></param>
		/// <param name="loops"></param>
		/// <param name="flipDirectionPerLoop"></param>
		/// <returns></returns>
		// Token: 0x06004623 RID: 17955 RVA: 0x0062D984 File Offset: 0x0062BB84
		public float GetWindGridPushComplex(int i, int j, int pushAnimationTimeTotal, float totalPushForce, int loops, bool flipDirectionPerLoop)
		{
			int windTimeLeft;
			int directionX;
			int num4;
			this._windGrid.GetWindTime(i, j, pushAnimationTimeTotal, out windTimeLeft, out directionX, out num4);
			float num5 = (float)windTimeLeft / (float)pushAnimationTimeTotal;
			int num2 = (int)(num5 * (float)loops);
			float num3 = num5 * (float)loops % 1f;
			float num6 = 1f / (float)loops;
			if (flipDirectionPerLoop && num2 % 2 == 1)
			{
				directionX *= -1;
			}
			if (num5 * (float)loops % 1f > 0.5f)
			{
				return (1f - num3) * totalPushForce * (float)directionX * (float)(loops - num2);
			}
			return num3 * totalPushForce * (float)directionX * (float)(loops - num2);
		}

		// Token: 0x06004624 RID: 17956 RVA: 0x0062DA08 File Offset: 0x0062BC08
		private unsafe void DrawMasterTrophies()
		{
			int num = 11;
			int num2 = this._specialsCount[num];
			for (int i = 0; i < num2; i++)
			{
				Point p = this._specialPositions[num][i];
				Tile tile = Main.tile[p.X, p.Y];
				if (tile != null && tile.active())
				{
					Texture2D value = TextureAssets.Extra[198].Value;
					int frameY = (int)(*tile.frameX / 54);
					bool flag = *tile.frameY / 72 != 0;
					int horizontalFrames = 1;
					int verticalFrames = 28;
					Rectangle rectangle = value.Frame(horizontalFrames, verticalFrames, 0, frameY, 0, 0);
					Vector2 origin = rectangle.Size() / 2f;
					Vector2 vector3 = p.ToWorldCoordinates(24f, 64f);
					float num3 = (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f / 5f));
					Vector2 vector2 = vector3 + new Vector2(0f, -40f) + new Vector2(0f, num3 * 4f);
					Color color = Lighting.GetColor(p.X, p.Y);
					SpriteEffects effects = flag ? 1 : 0;
					Main.spriteBatch.Draw(value, vector2 - Main.screenPosition, new Rectangle?(rectangle), color, 0f, origin, 1f, effects, 0f);
					float num4 = (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f / 2f)) * 0.3f + 0.7f;
					Color color2 = color;
					color2.A = 0;
					color2 = color2 * 0.1f * num4;
					for (float num5 = 0f; num5 < 1f; num5 += 0.16666667f)
					{
						Main.spriteBatch.Draw(value, vector2 - Main.screenPosition + (6.2831855f * num5).ToRotationVector2() * (6f + num3 * 2f), new Rectangle?(rectangle), color2, 0f, origin, 1f, effects, 0f);
					}
				}
			}
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x0062DC30 File Offset: 0x0062BE30
		private unsafe void DrawTeleportationPylons()
		{
			int num = 10;
			int num2 = this._specialsCount[num];
			for (int i = 0; i < num2; i++)
			{
				Point p = this._specialPositions[num][i];
				Tile tile = Main.tile[p.X, p.Y];
				if (!(tile == null) && tile.active())
				{
					Texture2D value = TextureAssets.Extra[181].Value;
					int num3 = (int)(*tile.frameX / 54);
					int num4 = 3;
					int horizontalFrames = num4 + 9;
					int verticalFrames = 8;
					int frameY = (Main.tileFrameCounter[597] + p.X + p.Y) % 64 / 8;
					Rectangle rectangle = value.Frame(horizontalFrames, verticalFrames, num4 + num3, frameY, 0, 0);
					Rectangle value2 = value.Frame(horizontalFrames, verticalFrames, 2, frameY, 0, 0);
					value.Frame(horizontalFrames, verticalFrames, 0, frameY, 0, 0);
					Vector2 origin = rectangle.Size() / 2f;
					Vector2 vector3 = p.ToWorldCoordinates(24f, 64f);
					float num5 = (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f / 5f));
					Vector2 vector2 = vector3 + new Vector2(0f, -40f) + new Vector2(0f, num5 * 4f);
					bool flag = this._rand.Next(4) == 0;
					if (this._isActiveAndNotPaused && flag && this._rand.Next(10) == 0)
					{
						Rectangle dustBox = Utils.CenteredRectangle(vector2, rectangle.Size());
						TeleportPylonsSystem.SpawnInWorldDust(num3, dustBox);
					}
					Color color = Lighting.GetColor(p.X, p.Y);
					color = Color.Lerp(color, Color.White, 0.8f);
					Main.spriteBatch.Draw(value, vector2 - Main.screenPosition, new Rectangle?(rectangle), color * 0.7f, 0f, origin, 1f, 0, 0f);
					float num6 = (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f / 1f)) * 0.2f + 0.8f;
					Color color2 = new Color(255, 255, 255, 0) * 0.1f * num6;
					for (float num7 = 0f; num7 < 1f; num7 += 0.16666667f)
					{
						Main.spriteBatch.Draw(value, vector2 - Main.screenPosition + (6.2831855f * num7).ToRotationVector2() * (6f + num5 * 2f), new Rectangle?(rectangle), color2, 0f, origin, 1f, 0, 0f);
					}
					int num8 = 0;
					bool actuallySelected;
					if (Main.InSmartCursorHighlightArea(p.X, p.Y, out actuallySelected))
					{
						num8 = 1;
						if (actuallySelected)
						{
							num8 = 2;
						}
					}
					if (num8 != 0)
					{
						int num9 = (int)((color.R + color.G + color.B) / 3);
						if (num9 > 10)
						{
							Color selectionGlowColor = Colors.GetSelectionGlowColor(num8 == 2, num9);
							Main.spriteBatch.Draw(value, vector2 - Main.screenPosition, new Rectangle?(value2), selectionGlowColor, 0f, origin, 1f, 0, 0f);
						}
					}
				}
			}
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x0062DF7C File Offset: 0x0062C17C
		private void DrawVoidLenses()
		{
			int num = 8;
			int num2 = this._specialsCount[num];
			this._voidLensData.Clear();
			for (int i = 0; i < num2; i++)
			{
				Point p = this._specialPositions[num][i];
				VoidLensHelper voidLensHelper = new VoidLensHelper(p.ToWorldCoordinates(8f, 8f), 1f);
				if (!Main.gamePaused)
				{
					voidLensHelper.Update();
				}
				int selectionMode = 0;
				bool actuallySelected;
				if (Main.InSmartCursorHighlightArea(p.X, p.Y, out actuallySelected))
				{
					selectionMode = 1;
					if (actuallySelected)
					{
						selectionMode = 2;
					}
				}
				voidLensHelper.DrawToDrawData(this._voidLensData, selectionMode);
			}
			foreach (DrawData voidLensDatum in this._voidLensData)
			{
				voidLensDatum.Draw(Main.spriteBatch);
			}
		}

		// Token: 0x06004627 RID: 17959 RVA: 0x0062E064 File Offset: 0x0062C264
		private unsafe void DrawMultiTileGrass()
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
					ushort num5 = *Main.tile[x, num3].type;
					if (num5 <= 238)
					{
						if (num5 <= 233)
						{
							if (num5 != 27)
							{
								if (num5 != 233)
								{
									goto IL_1D5;
								}
								sizeX = ((*Main.tile[x, num3].frameY != 0) ? 2 : 3);
								num4 = 2;
							}
							else
							{
								sizeX = 2;
								num4 = 5;
							}
						}
						else
						{
							if (num5 != 236 && num5 != 238)
							{
								goto IL_1D5;
							}
							num4 = (sizeX = 2);
						}
					}
					else
					{
						if (num5 > 493)
						{
							switch (num5)
							{
							case 519:
								sizeX = 1;
								num4 = this.ClimbCatTail(x, num3);
								num3 -= num4 - 1;
								goto IL_1FB;
							case 520:
							case 528:
							case 529:
								goto IL_1D5;
							case 521:
							case 522:
							case 523:
							case 524:
							case 525:
							case 526:
							case 527:
								goto IL_1A3;
							case 530:
								break;
							default:
								if (num5 != 651)
								{
									if (num5 != 652)
									{
										goto IL_1D5;
									}
									goto IL_1A3;
								}
								break;
							}
							sizeX = 3;
							num4 = 2;
							goto IL_1FB;
						}
						if (num5 != 485)
						{
							switch (num5)
							{
							case 489:
								sizeX = 2;
								num4 = 3;
								goto IL_1FB;
							case 490:
								break;
							case 491:
							case 492:
								goto IL_1D5;
							case 493:
								sizeX = 1;
								num4 = 2;
								goto IL_1FB;
							default:
								goto IL_1D5;
							}
						}
						IL_1A3:
						sizeX = 2;
						num4 = 2;
					}
					IL_1FB:
					this.DrawMultiTileGrassInWind(unscaledPosition, zero, x, num3, sizeX, num4);
					goto IL_20B;
					IL_1D5:
					if (TileID.Sets.MultiTileSway[(int)(*tile.TileType)])
					{
						TileObjectData tileData = TileObjectData.GetTileData(tile);
						sizeX = tileData.Width;
						num4 = tileData.Height;
						goto IL_1FB;
					}
					goto IL_1FB;
				}
				IL_20B:;
			}
		}

		// Token: 0x06004628 RID: 17960 RVA: 0x0062E28C File Offset: 0x0062C48C
		private unsafe int ClimbCatTail(int originx, int originy)
		{
			int num = 0;
			int num2 = originy;
			while (num2 > 10)
			{
				Tile tile = Main.tile[originx, num2];
				if (!tile.active() || *tile.type != 519)
				{
					break;
				}
				if (*tile.frameX >= 180)
				{
					num++;
					break;
				}
				num2--;
				num++;
			}
			return num;
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x0062E2E8 File Offset: 0x0062C4E8
		private unsafe void DrawMultiTileVines()
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
					ushort num3 = *Main.tile[x, y].type;
					if (num3 <= 271)
					{
						if (num3 <= 91)
						{
							if (num3 != 34)
							{
								if (num3 == 42)
								{
									goto IL_145;
								}
								if (num3 != 91)
								{
									goto IL_165;
								}
								sizeX = 1;
								sizeY = 3;
							}
							else
							{
								sizeX = 3;
								sizeY = 3;
							}
						}
						else
						{
							if (num3 == 95 || num3 == 126)
							{
								goto IL_155;
							}
							if (num3 - 270 > 1)
							{
								goto IL_165;
							}
							goto IL_145;
						}
					}
					else
					{
						if (num3 <= 465)
						{
							if (num3 == 444)
							{
								goto IL_155;
							}
							if (num3 == 454)
							{
								sizeX = 4;
								sizeY = 3;
								goto IL_18B;
							}
							if (num3 != 465)
							{
								goto IL_165;
							}
						}
						else if (num3 <= 581)
						{
							if (num3 != 572 && num3 != 581)
							{
								goto IL_165;
							}
							goto IL_145;
						}
						else if (num3 - 591 > 1)
						{
							if (num3 != 660)
							{
								goto IL_165;
							}
							goto IL_145;
						}
						sizeX = 2;
						sizeY = 3;
					}
					IL_18B:
					this.DrawMultiTileVinesInWind(unscaledPosition, zero, x, y, sizeX, sizeY);
					goto IL_19B;
					IL_145:
					sizeX = 1;
					sizeY = 2;
					goto IL_18B;
					IL_155:
					sizeX = 2;
					sizeY = 2;
					goto IL_18B;
					IL_165:
					if (TileID.Sets.MultiTileSway[(int)(*tile.TileType)])
					{
						TileObjectData tileData = TileObjectData.GetTileData(tile);
						sizeX = tileData.Width;
						sizeY = tileData.Height;
						goto IL_18B;
					}
					goto IL_18B;
				}
				IL_19B:;
			}
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x0062E4A0 File Offset: 0x0062C6A0
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

		// Token: 0x0600462B RID: 17963 RVA: 0x0062E504 File Offset: 0x0062C704
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

		// Token: 0x0600462C RID: 17964 RVA: 0x0062E568 File Offset: 0x0062C768
		private unsafe void DrawMultiTileGrassInWind(Vector2 screenPosition, Vector2 offSet, int topLeftX, int topLeftY, int sizeX, int sizeY)
		{
			float windCycle = this.GetWindCycle(topLeftX, topLeftY, this._sunflowerWindCounter);
			new Vector2((float)(sizeX * 16) * 0.5f, (float)(sizeY * 16));
			Vector2 vector = new Vector2((float)(topLeftX * 16 - (int)screenPosition.X) + (float)sizeX * 16f * 0.5f, (float)(topLeftY * 16 - (int)screenPosition.Y + 16 * sizeY)) + offSet;
			float num = 0.07f;
			int type = (int)(*Main.tile[topLeftX, topLeftY].type);
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
					ushort type2 = *tile.type;
					if ((int)type2 == type && TileDrawing.IsVisible(tile))
					{
						Math.Abs(((float)(i - topLeftX) + 0.5f) / (float)sizeX - 0.5f);
						short tileFrameX = *tile.frameX;
						short tileFrameY = *tile.frameY;
						float num2 = 1f - (float)(j - topLeftY + 1) / (float)sizeY;
						if (num2 == 0f)
						{
							num2 = 0.1f;
						}
						if (!flag)
						{
							num2 = 0f;
						}
						int tileWidth;
						int tileHeight;
						int tileTop;
						int halfBrickHeight;
						int addFrX;
						int addFrY;
						SpriteEffects tileSpriteEffect;
						Texture2D texture2D2;
						Rectangle rectangle;
						Color color2;
						this.GetTileDrawData(i, j, tile, type2, ref tileFrameX, ref tileFrameY, out tileWidth, out tileHeight, out tileTop, out halfBrickHeight, out addFrX, out addFrY, out tileSpriteEffect, out texture2D2, out rectangle, out color2);
						bool flag2 = this._rand.Next(4) == 0;
						Color tileLight = Lighting.GetColor(i, j);
						this.DrawAnimatedTile_AdjustForVisionChangers(i, j, tile, type2, tileFrameX, tileFrameY, ref tileLight, flag2);
						tileLight = this.DrawTiles_GetLightOverride(j, i, tile, type2, tileFrameX, tileFrameY, tileLight);
						if (this._isActiveAndNotPaused && flag2)
						{
							this.DrawTiles_EmitParticles(j, i, tile, type2, tileFrameX, tileFrameY, tileLight);
						}
						Vector2 vector2 = new Vector2((float)(i * 16 - (int)screenPosition.X), (float)(j * 16 - (int)screenPosition.Y + tileTop)) + offSet;
						if (*tile.type == 493 && *tile.frameY == 0)
						{
							if (Main.WindForVisuals >= 0f)
							{
								tileSpriteEffect ^= 1;
							}
							if (!tileSpriteEffect.HasFlag(1))
							{
								vector2.X -= 6f;
							}
							else
							{
								vector2.X += 6f;
							}
						}
						Vector2 vector3;
						vector3..ctor(windCycle * 1f, Math.Abs(windCycle) * 2f * num2);
						Vector2 origin = vector - vector2;
						Texture2D tileDrawTexture = this.GetTileDrawTexture(tile, i, j);
						if (tileDrawTexture != null)
						{
							Main.spriteBatch.Draw(tileDrawTexture, vector + new Vector2(0f, vector3.Y), new Rectangle?(new Rectangle((int)tileFrameX + addFrX, (int)tileFrameY + addFrY, tileWidth, tileHeight - halfBrickHeight)), tileLight, windCycle * num * num2, origin, 1f, tileSpriteEffect, 0f);
							if (texture2D != null)
							{
								Main.spriteBatch.Draw(texture2D, vector + new Vector2(0f, vector3.Y), new Rectangle?(new Rectangle((int)tileFrameX + addFrX, (int)tileFrameY + addFrY, tileWidth, tileHeight - halfBrickHeight)), color, windCycle * num * num2, origin, 1f, tileSpriteEffect, 0f);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600462D RID: 17965 RVA: 0x0062E928 File Offset: 0x0062CB28
		private unsafe void DrawVineStrip(Vector2 screenPosition, Vector2 offSet, int x, int startY)
		{
			int num = 0;
			int num2 = 0;
			Vector2 vector;
			vector..ctor((float)(x * 16 + 8), (float)(startY * 16 - 2));
			float amount = Math.Abs(Main.WindForVisuals) / 1.2f;
			amount = MathHelper.Lerp(0.2f, 1f, amount);
			float num3 = -0.08f * amount;
			float windCycle = this.GetWindCycle(x, startY, this._vineWindCounter);
			float num4 = 0f;
			float num5 = 0f;
			for (int i = startY; i < Main.maxTilesY - 10; i++)
			{
				Tile tile = Main.tile[x, i];
				if (!(tile == null))
				{
					ushort type = *tile.type;
					if (!tile.active() || !TileID.Sets.VineThreads[(int)type])
					{
						break;
					}
					num++;
					if (num2 >= 5)
					{
						num3 += 0.0075f * amount;
					}
					if (num2 >= 2)
					{
						num3 += 0.0025f;
					}
					if (Main.remixWorld)
					{
						if (WallID.Sets.AllowsWind[(int)(*tile.wall)] && (double)i > Main.worldSurface)
						{
							num2++;
						}
					}
					else if (WallID.Sets.AllowsWind[(int)(*tile.wall)] && (double)i < Main.worldSurface)
					{
						num2++;
					}
					float windGridPush = this.GetWindGridPush(x, i, 20, 0.01f);
					num4 = ((windGridPush != 0f || num5 == 0f) ? (num4 - windGridPush) : (num4 * -0.78f));
					num5 = windGridPush;
					short tileFrameX = *tile.frameX;
					short tileFrameY = *tile.frameY;
					Color color = Lighting.GetColor(x, i);
					int tileWidth;
					int tileHeight;
					int tileTop;
					int halfBrickHeight;
					int addFrX;
					int addFrY;
					SpriteEffects tileSpriteEffect;
					Texture2D glowTexture;
					Rectangle glowSourceRect;
					Color glowColor;
					this.GetTileDrawData(x, i, tile, type, ref tileFrameX, ref tileFrameY, out tileWidth, out tileHeight, out tileTop, out halfBrickHeight, out addFrX, out addFrY, out tileSpriteEffect, out glowTexture, out glowSourceRect, out glowColor);
					Vector2 position = new Vector2((float)(-(float)((int)screenPosition.X)), (float)(-(float)((int)screenPosition.Y))) + offSet + vector;
					if (tile.fullbrightBlock())
					{
						color = Color.White;
					}
					float num6 = (float)num2 * num3 * windCycle + num4;
					if (this._localPlayer.biomeSight)
					{
						Color sightColor = Color.White;
						if (Main.IsTileBiomeSightable(x, i, type, tileFrameX, tileFrameY, ref sightColor))
						{
							if (color.R < sightColor.R)
							{
								color.R = sightColor.R;
							}
							if (color.G < sightColor.G)
							{
								color.G = sightColor.G;
							}
							if (color.B < sightColor.B)
							{
								color.B = sightColor.B;
							}
							if (this._isActiveAndNotPaused && this._rand.Next(480) == 0)
							{
								Color newColor = sightColor;
								int num7 = Dust.NewDust(new Vector2((float)(x * 16), (float)(i * 16)), 16, 16, 267, 0f, 0f, 150, newColor, 0.3f);
								this._dust[num7].noGravity = true;
								this._dust[num7].fadeIn = 1f;
								this._dust[num7].velocity *= 0.1f;
								this._dust[num7].noLightEmittence = true;
							}
						}
					}
					Texture2D tileDrawTexture = this.GetTileDrawTexture(tile, x, i);
					if (tileDrawTexture == null)
					{
						break;
					}
					if (TileDrawing.IsVisible(tile))
					{
						Main.spriteBatch.Draw(tileDrawTexture, position, new Rectangle?(new Rectangle((int)tileFrameX + addFrX, (int)tileFrameY + addFrY, tileWidth, tileHeight - halfBrickHeight)), color, num6, new Vector2((float)(tileWidth / 2), (float)(halfBrickHeight - tileTop)), 1f, tileSpriteEffect, 0f);
						if (glowTexture != null)
						{
							Main.spriteBatch.Draw(glowTexture, position, new Rectangle?(glowSourceRect), glowColor, num6, new Vector2((float)(tileWidth / 2), (float)(halfBrickHeight - tileTop)), 1f, tileSpriteEffect, 0f);
						}
					}
					vector += (num6 + 1.5707964f).ToRotationVector2() * 16f;
				}
			}
		}

		// Token: 0x0600462E RID: 17966 RVA: 0x0062ED0C File Offset: 0x0062CF0C
		private unsafe void DrawRisingVineStrip(Vector2 screenPosition, Vector2 offSet, int x, int startY)
		{
			int num = 0;
			int num2 = 0;
			Vector2 vector;
			vector..ctor((float)(x * 16 + 8), (float)(startY * 16 + 16 + 2));
			float amount = Math.Abs(Main.WindForVisuals) / 1.2f;
			amount = MathHelper.Lerp(0.2f, 1f, amount);
			float num3 = -0.08f * amount;
			float windCycle = this.GetWindCycle(x, startY, this._vineWindCounter);
			float num4 = 0f;
			float num5 = 0f;
			for (int num6 = startY; num6 > 10; num6--)
			{
				Tile tile = Main.tile[x, num6];
				if (tile != null)
				{
					ushort type = *tile.type;
					if (!tile.active() || !TileID.Sets.ReverseVineThreads[(int)type])
					{
						break;
					}
					num++;
					if (num2 >= 5)
					{
						num3 += 0.0075f * amount;
					}
					if (num2 >= 2)
					{
						num3 += 0.0025f;
					}
					if (WallID.Sets.AllowsWind[(int)(*tile.wall)] && (double)num6 < Main.worldSurface)
					{
						num2++;
					}
					float windGridPush = this.GetWindGridPush(x, num6, 40, -0.004f);
					num4 = ((windGridPush != 0f || num5 == 0f) ? (num4 - windGridPush) : (num4 * -0.78f));
					num5 = windGridPush;
					short tileFrameX = *tile.frameX;
					short tileFrameY = *tile.frameY;
					Color color = Lighting.GetColor(x, num6);
					int tileWidth;
					int tileHeight;
					int tileTop;
					int halfBrickHeight;
					int addFrX;
					int addFrY;
					SpriteEffects tileSpriteEffect;
					Texture2D texture2D;
					Rectangle rectangle;
					Color color2;
					this.GetTileDrawData(x, num6, tile, type, ref tileFrameX, ref tileFrameY, out tileWidth, out tileHeight, out tileTop, out halfBrickHeight, out addFrX, out addFrY, out tileSpriteEffect, out texture2D, out rectangle, out color2);
					Vector2 position = new Vector2((float)(-(float)((int)screenPosition.X)), (float)(-(float)((int)screenPosition.Y))) + offSet + vector;
					float num7 = (float)num2 * (0f - num3) * windCycle + num4;
					Texture2D tileDrawTexture = this.GetTileDrawTexture(tile, x, num6);
					if (tileDrawTexture == null)
					{
						break;
					}
					if (TileDrawing.IsVisible(tile))
					{
						Main.spriteBatch.Draw(tileDrawTexture, position, new Rectangle?(new Rectangle((int)tileFrameX + addFrX, (int)tileFrameY + addFrY, tileWidth, tileHeight - halfBrickHeight)), color, num7, new Vector2((float)(tileWidth / 2), (float)(halfBrickHeight - tileTop + tileHeight)), 1f, tileSpriteEffect, 0f);
					}
					vector += (num7 - 1.5707964f).ToRotationVector2() * 16f;
				}
			}
		}

		// Token: 0x0600462F RID: 17967 RVA: 0x0062EF54 File Offset: 0x0062D154
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

		/// <summary>
		/// Determines how much wind should affect a multitile of the given size and top left target location on the current update tick.
		/// <para /> Similar to <see cref="M:Terraria.GameContent.Drawing.TileDrawing.GetWindGridPushComplex(System.Int32,System.Int32,System.Int32,System.Single,System.Int32,System.Boolean)" />, but for a multitile area instead of a single tile.
		/// </summary>
		// Token: 0x06004630 RID: 17968 RVA: 0x0062EFB4 File Offset: 0x0062D1B4
		public float GetHighestWindGridPushComplex(int topLeftX, int topLeftY, int sizeX, int sizeY, int totalPushTime, float pushForcePerFrame, int loops, bool swapLoopDir)
		{
			float result = 0f;
			int num = int.MaxValue;
			for (int i = 0; i < 1; i++)
			{
				for (int j = 0; j < sizeY; j++)
				{
					int windTimeLeft;
					int num2;
					int num3;
					this._windGrid.GetWindTime(topLeftX + i + sizeX / 2, topLeftY + j, totalPushTime, out windTimeLeft, out num2, out num3);
					float windGridPushComplex = this.GetWindGridPushComplex(topLeftX + i, topLeftY + j, totalPushTime, pushForcePerFrame, loops, swapLoopDir);
					if (windTimeLeft < num && windTimeLeft != 0)
					{
						result = windGridPushComplex;
						num = windTimeLeft;
					}
				}
			}
			return result;
		}

		// Token: 0x06004631 RID: 17969 RVA: 0x0062F02C File Offset: 0x0062D22C
		private unsafe void DrawMultiTileVinesInWind(Vector2 screenPosition, Vector2 offSet, int topLeftX, int topLeftY, int sizeX, int sizeY)
		{
			float windCycle = this.GetWindCycle(topLeftX, topLeftY, this._sunflowerWindCounter);
			float num = windCycle;
			int totalPushTime = 60;
			float pushForcePerFrame = 1.26f;
			float highestWindGridPushComplex = this.GetHighestWindGridPushComplex(topLeftX, topLeftY, sizeX, sizeY, totalPushTime, pushForcePerFrame, 3, true);
			windCycle += highestWindGridPushComplex;
			new Vector2((float)(sizeX * 16) * 0.5f, 0f);
			Vector2 vector = new Vector2((float)(topLeftX * 16 - (int)screenPosition.X) + (float)sizeX * 16f * 0.5f, (float)(topLeftY * 16 - (int)screenPosition.Y)) + offSet;
			float num2 = 0.07f;
			Tile tile = Main.tile[topLeftX, topLeftY];
			int type = (int)(*tile.type);
			Vector2 vector2;
			vector2..ctor(0f, -2f);
			vector += vector2;
			bool isBelowNonHammeredPlatform = true;
			for (int i = 0; i < sizeX; i++)
			{
				if (!WorldGen.IsBelowANonHammeredPlatform(topLeftX + i, topLeftY))
				{
					isBelowNonHammeredPlatform = false;
					break;
				}
			}
			if (isBelowNonHammeredPlatform)
			{
				vector.Y -= 8f;
				vector2.Y -= 8f;
			}
			Texture2D texture2D = null;
			Color color = Color.Transparent;
			float? num3 = null;
			float num4 = 1f;
			float num5 = -4f;
			bool flag2 = false;
			num2 = 0.15f;
			if (type <= 444)
			{
				int num8;
				if (type <= 95)
				{
					if (type != 34)
					{
						if (type != 42)
						{
							if (type != 95)
							{
								goto IL_5DC;
							}
							goto IL_580;
						}
						else
						{
							num3 = new float?(1f);
							num5 = 0f;
							num8 = (int)(*tile.frameY / 36);
							if (num8 <= 9)
							{
								if (num8 == 0)
								{
									num3 = null;
									num5 = -1f;
									goto IL_5DC;
								}
								if (num8 != 9)
								{
									goto IL_5DC;
								}
								num3 = new float?(0f);
								goto IL_5DC;
							}
							else
							{
								if (num8 == 12)
								{
									num3 = null;
									num5 = -1f;
									goto IL_5DC;
								}
								if (num8 == 14)
								{
									num3 = null;
									num5 = -1f;
									goto IL_5DC;
								}
								switch (num8)
								{
								case 28:
									num3 = null;
									num5 = -1f;
									goto IL_5DC;
								case 29:
								case 31:
								case 36:
								case 37:
									goto IL_5DC;
								case 30:
									num3 = new float?(0f);
									goto IL_5DC;
								case 32:
									num3 = new float?(0f);
									goto IL_5DC;
								case 33:
									num3 = new float?(0f);
									goto IL_5DC;
								case 34:
									num3 = null;
									num5 = -1f;
									goto IL_5DC;
								case 35:
									num3 = new float?(0f);
									goto IL_5DC;
								case 38:
									num3 = null;
									num5 = -1f;
									goto IL_5DC;
								case 39:
									num3 = null;
									num5 = -1f;
									flag2 = true;
									goto IL_5DC;
								case 40:
								case 41:
								case 42:
								case 43:
									num3 = new float?(0f);
									num3 = null;
									num5 = -1f;
									flag2 = true;
									goto IL_5DC;
								default:
									goto IL_5DC;
								}
							}
						}
					}
				}
				else if (type != 126)
				{
					if (type - 270 > 1 && type != 444)
					{
						goto IL_5DC;
					}
					goto IL_580;
				}
				num3 = new float?(1f);
				num5 = 0f;
				num8 = (int)(*tile.frameY / 54 + *tile.frameX / 108 * 37);
				switch (num8)
				{
				case 9:
					num3 = null;
					num5 = -1f;
					flag2 = true;
					num2 *= 0.3f;
					goto IL_5DC;
				case 10:
					goto IL_5DC;
				case 11:
					num2 *= 0.5f;
					goto IL_5DC;
				case 12:
					num3 = null;
					num5 = -1f;
					goto IL_5DC;
				default:
					switch (num8)
					{
					case 18:
						num3 = null;
						num5 = -1f;
						goto IL_5DC;
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
						goto IL_5DC;
					case 21:
						num3 = null;
						num5 = -1f;
						goto IL_5DC;
					case 23:
						num3 = new float?(0f);
						goto IL_5DC;
					case 25:
						num3 = null;
						num5 = -1f;
						flag2 = true;
						goto IL_5DC;
					case 32:
						num2 *= 0.5f;
						goto IL_5DC;
					case 33:
						num2 *= 0.5f;
						goto IL_5DC;
					case 35:
						num3 = new float?(0f);
						goto IL_5DC;
					case 36:
						num3 = null;
						num5 = -1f;
						flag2 = true;
						goto IL_5DC;
					case 37:
						num3 = null;
						num5 = -1f;
						flag2 = true;
						num2 *= 0.5f;
						goto IL_5DC;
					case 39:
						num3 = null;
						num5 = -1f;
						flag2 = true;
						goto IL_5DC;
					case 40:
					case 41:
					case 42:
					case 43:
						num3 = null;
						num5 = -2f;
						flag2 = true;
						num2 *= 0.5f;
						goto IL_5DC;
					case 44:
						num3 = null;
						num5 = -3f;
						goto IL_5DC;
					default:
						goto IL_5DC;
					}
					break;
				}
			}
			else if (type <= 581)
			{
				if (type != 454 && type != 572 && type != 581)
				{
					goto IL_5DC;
				}
			}
			else
			{
				if (type == 591)
				{
					num4 = 0.5f;
					num5 = -2f;
					goto IL_5DC;
				}
				if (type == 592)
				{
					num4 = 0.5f;
					num5 = -2f;
					texture2D = TextureAssets.GlowMask[294].Value;
					color..ctor(255, 255, 255, 0);
					goto IL_5DC;
				}
				if (type != 660)
				{
					goto IL_5DC;
				}
			}
			IL_580:
			num3 = new float?(1f);
			num5 = 0f;
			IL_5DC:
			ModTile tile3 = TileLoader.GetTile(type);
			if (tile3 != null)
			{
				tile3.AdjustMultiTileVineParameters(topLeftX, topLeftY, ref num3, ref num4, ref num5, ref flag2, ref num2, ref texture2D, ref color);
			}
			if (flag2)
			{
				vector += new Vector2(0f, 16f);
			}
			num2 *= -1f;
			if (!this.InAPlaceWithWind(topLeftX, topLeftY, sizeX, sizeY))
			{
				windCycle -= num;
			}
			ulong num6 = 0UL;
			for (int j = topLeftX; j < topLeftX + sizeX; j++)
			{
				for (int k = topLeftY; k < topLeftY + sizeY; k++)
				{
					Tile tile2 = Main.tile[j, k];
					ushort type2 = *tile2.type;
					if ((int)type2 == type && TileDrawing.IsVisible(tile2))
					{
						Math.Abs(((float)(j - topLeftX) + 0.5f) / (float)sizeX - 0.5f);
						short tileFrameX = *tile2.frameX;
						short tileFrameY = *tile2.frameY;
						float num7 = (float)(k - topLeftY + 1) / (float)sizeY;
						if (num7 == 0f)
						{
							num7 = 0.1f;
						}
						if (num3 != null)
						{
							num7 = num3.Value;
						}
						if (flag2 && k == topLeftY)
						{
							num7 = 0f;
						}
						int tileWidth;
						int tileHeight;
						int tileTop;
						int halfBrickHeight;
						int addFrX;
						int addFrY;
						SpriteEffects tileSpriteEffect;
						Texture2D texture2D2;
						Rectangle rectangle2;
						Color color3;
						this.GetTileDrawData(j, k, tile2, type2, ref tileFrameX, ref tileFrameY, out tileWidth, out tileHeight, out tileTop, out halfBrickHeight, out addFrX, out addFrY, out tileSpriteEffect, out texture2D2, out rectangle2, out color3);
						bool flag3 = this._rand.Next(4) == 0;
						Color tileLight = Lighting.GetColor(j, k);
						this.DrawAnimatedTile_AdjustForVisionChangers(j, k, tile2, type2, tileFrameX, tileFrameY, ref tileLight, flag3);
						tileLight = this.DrawTiles_GetLightOverride(k, j, tile2, type2, tileFrameX, tileFrameY, tileLight);
						if (this._isActiveAndNotPaused && flag3)
						{
							this.DrawTiles_EmitParticles(k, j, tile2, type2, tileFrameX, tileFrameY, tileLight);
						}
						Vector2 vector3 = new Vector2((float)(j * 16 - (int)screenPosition.X), (float)(k * 16 - (int)screenPosition.Y + tileTop)) + offSet;
						vector3 += vector2;
						Vector2 vector4;
						vector4..ctor(windCycle * num4, Math.Abs(windCycle) * num5 * num7);
						Vector2 vector5 = vector - vector3;
						Texture2D tileDrawTexture = this.GetTileDrawTexture(tile2, j, k);
						if (tileDrawTexture != null)
						{
							Vector2 vector6 = vector + new Vector2(0f, vector4.Y);
							Rectangle rectangle;
							rectangle..ctor((int)tileFrameX + addFrX, (int)tileFrameY + addFrY, tileWidth, tileHeight - halfBrickHeight);
							float rotation = windCycle * num2 * num7;
							if (type2 == 660 && k == topLeftY + sizeY - 1)
							{
								Texture2D value = TextureAssets.Extra[260].Value;
								float num9 = ((float)((j + k) % 200) * 0.11f + (float)Main.timeForVisualEffects / 360f) % 1f;
								Color white = Color.White;
								Main.spriteBatch.Draw(value, vector6, new Rectangle?(rectangle), white, rotation, vector5, 1f, tileSpriteEffect, 0f);
							}
							Main.spriteBatch.Draw(tileDrawTexture, vector6, new Rectangle?(rectangle), tileLight, rotation, vector5, 1f, tileSpriteEffect, 0f);
							if (type2 == 660 && k == topLeftY + sizeY - 1)
							{
								Texture2D value2 = TextureAssets.Extra[260].Value;
								Color color2 = Main.hslToRgb(((float)((j + k) % 200) * 0.11f + (float)Main.timeForVisualEffects / 360f) % 1f, 1f, 0.8f, byte.MaxValue);
								color2.A = 127;
								Rectangle value3 = rectangle;
								Vector2 position = vector6;
								Vector2 origin = vector5;
								Main.spriteBatch.Draw(value2, position, new Rectangle?(value3), color2, rotation, origin, 1f, tileSpriteEffect, 0f);
							}
							if (texture2D != null)
							{
								Main.spriteBatch.Draw(texture2D, vector6, new Rectangle?(rectangle), color, rotation, vector5, 1f, tileSpriteEffect, 0f);
							}
							TileDrawing.TileFlameData tileFlameData = this.GetTileFlameData(j, k, (int)type2, (int)tileFrameY);
							if (num6 == 0UL)
							{
								num6 = tileFlameData.flameSeed;
							}
							tileFlameData.flameSeed = num6;
							for (int l = 0; l < tileFlameData.flameCount; l++)
							{
								float x = (float)Utils.RandomInt(ref tileFlameData.flameSeed, tileFlameData.flameRangeXMin, tileFlameData.flameRangeXMax) * tileFlameData.flameRangeMultX;
								float y = (float)Utils.RandomInt(ref tileFlameData.flameSeed, tileFlameData.flameRangeYMin, tileFlameData.flameRangeYMax) * tileFlameData.flameRangeMultY;
								Main.spriteBatch.Draw(tileFlameData.flameTexture, vector6 + new Vector2(x, y), new Rectangle?(rectangle), tileFlameData.flameColor, rotation, vector5, 1f, tileSpriteEffect, 0f);
							}
						}
					}
				}
			}
		}

		// Token: 0x06004632 RID: 17970 RVA: 0x0062FAB8 File Offset: 0x0062DCB8
		private void EmitAlchemyHerbParticles(int j, int i, int style)
		{
			Color newColor;
			if (style == 0 && this._rand.Next(100) == 0)
			{
				Vector2 position = new Vector2((float)(i * 16), (float)(j * 16 - 4));
				int width = 16;
				int height = 16;
				int type = 19;
				float speedX = 0f;
				float speedY = 0f;
				int alpha = 160;
				newColor = default(Color);
				int num = Dust.NewDust(position, width, height, type, speedX, speedY, alpha, newColor, 0.1f);
				Dust dust = this._dust[num];
				dust.velocity.X = dust.velocity.X / 2f;
				Dust dust2 = this._dust[num];
				dust2.velocity.Y = dust2.velocity.Y / 2f;
				this._dust[num].noGravity = true;
				this._dust[num].fadeIn = 1f;
			}
			if (style == 1 && this._rand.Next(100) == 0)
			{
				Vector2 position2 = new Vector2((float)(i * 16), (float)(j * 16));
				int width2 = 16;
				int height2 = 16;
				int type2 = 41;
				float speedX2 = 0f;
				float speedY2 = 0f;
				int alpha2 = 250;
				newColor = default(Color);
				Dust.NewDust(position2, width2, height2, type2, speedX2, speedY2, alpha2, newColor, 0.8f);
			}
			if (style == 3)
			{
				if (this._rand.Next(200) == 0)
				{
					Vector2 position3 = new Vector2((float)(i * 16), (float)(j * 16));
					int width3 = 16;
					int height3 = 16;
					int type3 = 14;
					float speedX3 = 0f;
					float speedY3 = 0f;
					int alpha3 = 100;
					newColor = default(Color);
					int num2 = Dust.NewDust(position3, width3, height3, type3, speedX3, speedY3, alpha3, newColor, 0.2f);
					this._dust[num2].fadeIn = 1.2f;
				}
				if (this._rand.Next(75) == 0)
				{
					Vector2 position4 = new Vector2((float)(i * 16), (float)(j * 16));
					int width4 = 16;
					int height4 = 16;
					int type4 = 27;
					float speedX4 = 0f;
					float speedY4 = 0f;
					int alpha4 = 100;
					newColor = default(Color);
					int num3 = Dust.NewDust(position4, width4, height4, type4, speedX4, speedY4, alpha4, newColor, 1f);
					Dust dust3 = this._dust[num3];
					dust3.velocity.X = dust3.velocity.X / 2f;
					Dust dust4 = this._dust[num3];
					dust4.velocity.Y = dust4.velocity.Y / 2f;
				}
			}
			if (style == 4 && this._rand.Next(150) == 0)
			{
				Vector2 position5 = new Vector2((float)(i * 16), (float)(j * 16));
				int width5 = 16;
				int height5 = 8;
				int type5 = 16;
				float speedX5 = 0f;
				float speedY5 = 0f;
				int alpha5 = 0;
				newColor = default(Color);
				int num4 = Dust.NewDust(position5, width5, height5, type5, speedX5, speedY5, alpha5, newColor, 1f);
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
				Vector2 position6 = new Vector2((float)(i * 16), (float)(j * 16 - 6));
				int width6 = 16;
				int height6 = 16;
				int type6 = 6;
				float speedX6 = 0f;
				float speedY6 = 0f;
				int alpha6 = 0;
				newColor = default(Color);
				int num5 = Dust.NewDust(position6, width6, height6, type6, speedX6, speedY6, alpha6, newColor, 1.5f);
				Dust dust8 = this._dust[num5];
				dust8.velocity.Y = dust8.velocity.Y - 2f;
				this._dust[num5].noGravity = true;
			}
			if (style == 6 && this._rand.Next(30) == 0)
			{
				newColor..ctor(50, 255, 255, 255);
				int num6 = Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 43, 0f, 0f, 254, newColor, 0.5f);
				this._dust[num6].velocity *= 0f;
			}
		}

		// Token: 0x06004633 RID: 17971 RVA: 0x0062FE70 File Offset: 0x0062E070
		private bool IsAlchemyPlantHarvestable(int style)
		{
			return (style == 0 && Main.dayTime) || (style == 1 && !Main.dayTime) || (style == 3 && !Main.dayTime && (Main.bloodMoon || Main.moonPhase == 0)) || (style == 4 && (Main.raining || Main.cloudAlpha > 0f)) || (style == 5 && !Main.raining && Main.time > 40500.0);
		}

		/// <summary>
		/// The wind grid used to exert wind effects on tiles.
		/// </summary>
		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06004634 RID: 17972 RVA: 0x0062FEE8 File Offset: 0x0062E0E8
		public WindGrid Wind
		{
			get
			{
				return this._windGrid;
			}
		}

		/// <summary>
		/// Checks if a tile at the given coordinates counts towards tile coloring from the Dangersense buff.
		/// <br />Vanilla only uses Main.LocalPlayer for <paramref name="player" />
		/// </summary>
		// Token: 0x06004635 RID: 17973 RVA: 0x0062FEF0 File Offset: 0x0062E0F0
		public unsafe static bool IsTileDangerous(int tileX, int tileY, Player player)
		{
			Tile tile = Main.tile[tileX, tileY];
			return TileDrawing.IsTileDangerous(tileX, tileY, player, tile, *tile.type);
		}

		// Token: 0x06004636 RID: 17974 RVA: 0x0062FF1C File Offset: 0x0062E11C
		private unsafe void DrawCustom(bool solidLayer)
		{
			if (solidLayer)
			{
				Main.spriteBatch.Begin(0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
			}
			int index = solidLayer ? 14 : 13;
			int specialCount = this._specialsCount[index];
			for (int i = 0; i < specialCount; i++)
			{
				Point p = this._specialPositions[index][i];
				TileLoader.SpecialDraw((int)(*Main.tile[p.X, p.Y].TileType), p.X, p.Y, Main.spriteBatch);
			}
			if (solidLayer)
			{
				Main.spriteBatch.End();
			}
		}

		// Token: 0x04005B35 RID: 23349
		private const int MAX_SPECIALS = 9000;

		// Token: 0x04005B36 RID: 23350
		private const int MAX_SPECIALS_LEGACY = 1000;

		// Token: 0x04005B37 RID: 23351
		private const float FORCE_FOR_MIN_WIND = 0.08f;

		// Token: 0x04005B38 RID: 23352
		private const float FORCE_FOR_MAX_WIND = 1.2f;

		// Token: 0x04005B39 RID: 23353
		private int _leafFrequency = 100000;

		// Token: 0x04005B3A RID: 23354
		private int[] _specialsCount = new int[15];

		// Token: 0x04005B3B RID: 23355
		private Point[][] _specialPositions = new Point[15][];

		// Token: 0x04005B3C RID: 23356
		private Dictionary<Point, int> _displayDollTileEntityPositions = new Dictionary<Point, int>();

		// Token: 0x04005B3D RID: 23357
		private Dictionary<Point, int> _hatRackTileEntityPositions = new Dictionary<Point, int>();

		// Token: 0x04005B3E RID: 23358
		private Dictionary<Point, int> _trainingDummyTileEntityPositions = new Dictionary<Point, int>();

		// Token: 0x04005B3F RID: 23359
		private Dictionary<Point, int> _itemFrameTileEntityPositions = new Dictionary<Point, int>();

		// Token: 0x04005B40 RID: 23360
		private Dictionary<Point, int> _foodPlatterTileEntityPositions = new Dictionary<Point, int>();

		// Token: 0x04005B41 RID: 23361
		private Dictionary<Point, int> _weaponRackTileEntityPositions = new Dictionary<Point, int>();

		// Token: 0x04005B42 RID: 23362
		private Dictionary<Point, int> _chestPositions = new Dictionary<Point, int>();

		// Token: 0x04005B43 RID: 23363
		private int _specialTilesCount;

		// Token: 0x04005B44 RID: 23364
		private int[] _specialTileX = new int[1000];

		// Token: 0x04005B45 RID: 23365
		private int[] _specialTileY = new int[1000];

		// Token: 0x04005B46 RID: 23366
		private UnifiedRandom _rand;

		// Token: 0x04005B47 RID: 23367
		public double _treeWindCounter;

		// Token: 0x04005B48 RID: 23368
		public double _grassWindCounter;

		// Token: 0x04005B49 RID: 23369
		public double _sunflowerWindCounter;

		// Token: 0x04005B4A RID: 23370
		public double _vineWindCounter;

		// Token: 0x04005B4B RID: 23371
		private WindGrid _windGrid = new WindGrid();

		// Token: 0x04005B4C RID: 23372
		private bool _shouldShowInvisibleBlocks;

		// Token: 0x04005B4D RID: 23373
		private bool _shouldShowInvisibleBlocks_LastFrame;

		// Token: 0x04005B4E RID: 23374
		private List<Point> _vineRootsPositions = new List<Point>();

		// Token: 0x04005B4F RID: 23375
		private List<Point> _reverseVineRootsPositions = new List<Point>();

		// Token: 0x04005B50 RID: 23376
		private TilePaintSystemV2 _paintSystem;

		// Token: 0x04005B51 RID: 23377
		private Color _martianGlow = new Color(0, 0, 0, 0);

		// Token: 0x04005B52 RID: 23378
		private Color _meteorGlow = new Color(100, 100, 100, 0);

		// Token: 0x04005B53 RID: 23379
		private Color _lavaMossGlow = new Color(150, 100, 50, 0);

		// Token: 0x04005B54 RID: 23380
		private Color _kryptonMossGlow = new Color(0, 200, 0, 0);

		// Token: 0x04005B55 RID: 23381
		private Color _xenonMossGlow = new Color(0, 180, 250, 0);

		// Token: 0x04005B56 RID: 23382
		private Color _argonMossGlow = new Color(225, 0, 125, 0);

		// Token: 0x04005B57 RID: 23383
		private Color _violetMossGlow = new Color(150, 0, 250, 0);

		// Token: 0x04005B58 RID: 23384
		private bool _isActiveAndNotPaused;

		// Token: 0x04005B59 RID: 23385
		private Player _localPlayer = new Player();

		// Token: 0x04005B5A RID: 23386
		private Color _highQualityLightingRequirement;

		// Token: 0x04005B5B RID: 23387
		private Color _mediumQualityLightingRequirement;

		// Token: 0x04005B5C RID: 23388
		private static readonly Vector2 _zero;

		// Token: 0x04005B5D RID: 23389
		private ThreadLocal<TileDrawInfo> _currentTileDrawInfo = new ThreadLocal<TileDrawInfo>(() => new TileDrawInfo());

		// Token: 0x04005B5E RID: 23390
		private TileDrawInfo _currentTileDrawInfoNonThreaded = new TileDrawInfo();

		// Token: 0x04005B5F RID: 23391
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

		// Token: 0x04005B60 RID: 23392
		private List<DrawData> _voidLensData = new List<DrawData>();

		// Token: 0x02000CD6 RID: 3286
		public enum TileCounterType
		{
			// Token: 0x04007A22 RID: 31266
			Tree,
			// Token: 0x04007A23 RID: 31267
			DisplayDoll,
			// Token: 0x04007A24 RID: 31268
			HatRack,
			/// <summary> Tile will sway as if anchored below (1x1 tile). Affected by wind and player interaction. Used automatically by tiles in <see cref="F:Terraria.ID.TileID.Sets.SwaysInWindBasic" />. </summary>
			// Token: 0x04007A25 RID: 31269
			WindyGrass,
			/// <summary> Tile will sway as if anchored below. Affected by wind but not player interaction. Used by <see cref="F:Terraria.ID.TileID.PlantDetritus" />, <see cref="F:Terraria.ID.TileID.Sunflower" />. Tiles need to assign <see cref="F:Terraria.ID.TileID.Sets.MultiTileSway" /> and use as directed.</summary>
			// Token: 0x04007A26 RID: 31270
			MultiTileGrass,
			/// <summary> Tile will sway as if anchored above. Affected by wind and player interaction. Used by <see cref="F:Terraria.ID.TileID.Banners" />, <see cref="F:Terraria.ID.TileID.Chandeliers" />, <see cref="F:Terraria.ID.TileID.HangingLanterns" />, <see cref="F:Terraria.ID.TileID.FireflyinaBottle" />. Tiles need to assign <see cref="F:Terraria.ID.TileID.Sets.MultiTileSway" /> and use as directed. </summary>
			// Token: 0x04007A27 RID: 31271
			MultiTileVine,
			/// <summary> Tile chain will sway as if anchored above. Affected by wind and player interaction. Used by all varieties of <see cref="F:Terraria.ID.TileID.Vines" />. Tiles need to assign <see cref="F:Terraria.ID.TileID.Sets.VineThreads" /> and use as directed. </summary>
			// Token: 0x04007A28 RID: 31272
			Vine,
			// Token: 0x04007A29 RID: 31273
			BiomeGrass,
			// Token: 0x04007A2A RID: 31274
			VoidLens,
			/// <summary> Tile chain will sway as if anchored below. Affected by wind and player interaction. Used by <see cref="F:Terraria.ID.TileID.Seaweed" />, although seaweed grows below the wind limit so it isn't affected. Tiles need to assign <see cref="F:Terraria.ID.TileID.Sets.ReverseVineThreads" /> and use as directed. </summary>
			// Token: 0x04007A2B RID: 31275
			ReverseVine,
			// Token: 0x04007A2C RID: 31276
			TeleportationPylon,
			// Token: 0x04007A2D RID: 31277
			MasterTrophy,
			// Token: 0x04007A2E RID: 31278
			AnyDirectionalGrass,
			/// <summary> Will cause <see cref="M:Terraria.ModLoader.ModTile.SpecialDraw(System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteBatch)" /> to be called.
			/// <br /><br /> This option should be used with non-<see cref="F:Terraria.Main.tileSolid" /> tiles or solid tiles that set <see cref="F:Terraria.ID.TileID.Sets.DrawTileInSolidLayer" /> to false.</summary>
			// Token: 0x04007A2F RID: 31279
			CustomNonSolid,
			/// <summary> Will cause <see cref="M:Terraria.ModLoader.ModTile.SpecialDraw(System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteBatch)" /> to be called.
			/// <br /><br /> This option should be used with <see cref="F:Terraria.Main.tileSolid" /> tiles or non-solid tiles that set <see cref="F:Terraria.ID.TileID.Sets.DrawTileInSolidLayer" /> to true.</summary>
			// Token: 0x04007A30 RID: 31280
			CustomSolid,
			// Token: 0x04007A31 RID: 31281
			Count
		}

		/// <summary>
		/// Contains parameters for controlling how a flame overlay on a tile will be drawn. Used by <see cref="M:Terraria.ModLoader.ModTile.GetTileFlameData(System.Int32,System.Int32,Terraria.GameContent.Drawing.TileDrawing.TileFlameData@)" />.
		/// </summary>
		// Token: 0x02000CD7 RID: 3287
		public struct TileFlameData
		{
			// Token: 0x04007A32 RID: 31282
			public Texture2D flameTexture;

			// Token: 0x04007A33 RID: 31283
			public ulong flameSeed;

			// Token: 0x04007A34 RID: 31284
			public int flameCount;

			// Token: 0x04007A35 RID: 31285
			public Color flameColor;

			// Token: 0x04007A36 RID: 31286
			public int flameRangeXMin;

			// Token: 0x04007A37 RID: 31287
			public int flameRangeXMax;

			// Token: 0x04007A38 RID: 31288
			public int flameRangeYMin;

			// Token: 0x04007A39 RID: 31289
			public int flameRangeYMax;

			// Token: 0x04007A3A RID: 31290
			public float flameRangeMultX;

			// Token: 0x04007A3B RID: 31291
			public float flameRangeMultY;
		}

		// Token: 0x02000CD8 RID: 3288
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04007A3C RID: 31292
			public static WorldGen.GetTreeFoliageDataMethod <0>__GetCommonTreeFoliageData;

			// Token: 0x04007A3D RID: 31293
			public static WorldGen.GetTreeFoliageDataMethod <1>__GetGemTreeFoliageData;

			// Token: 0x04007A3E RID: 31294
			public static WorldGen.GetTreeFoliageDataMethod <2>__GetVanityTreeFoliageData;

			// Token: 0x04007A3F RID: 31295
			public static WorldGen.GetTreeFoliageDataMethod <3>__GetAshTreeFoliageData;
		}
	}
}
