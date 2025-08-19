using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.Utilities;

namespace Terraria.GameContent.Liquid
{
	// Token: 0x020005E9 RID: 1513
	public class LiquidRenderer
	{
		// Token: 0x1400006C RID: 108
		// (add) Token: 0x06004351 RID: 17233 RVA: 0x005FD2D8 File Offset: 0x005FB4D8
		// (remove) Token: 0x06004352 RID: 17234 RVA: 0x005FD310 File Offset: 0x005FB510
		public event Action<Color[], Rectangle> WaveFilters;

		// Token: 0x06004353 RID: 17235 RVA: 0x005FD345 File Offset: 0x005FB545
		public static void LoadContent()
		{
			LiquidRenderer.Instance = new LiquidRenderer();
			LiquidRenderer.Instance.PrepareAssets();
		}

		// Token: 0x06004354 RID: 17236 RVA: 0x005FD35C File Offset: 0x005FB55C
		private void PrepareAssets()
		{
			if (!Main.dedServ)
			{
				for (int i = 0; i < this._liquidTextures.Length; i++)
				{
					this._liquidTextures[i] = Main.Assets.Request<Texture2D>("Images/Misc/water_" + i.ToString());
				}
			}
		}

		// Token: 0x06004355 RID: 17237 RVA: 0x005FD3A8 File Offset: 0x005FB5A8
		private unsafe void InternalPrepareDraw(Rectangle drawArea)
		{
			Rectangle rectangle;
			rectangle..ctor(drawArea.X - 2, drawArea.Y - 2, drawArea.Width + 4, drawArea.Height + 4);
			this._drawArea = drawArea;
			if (this._cache.Length < rectangle.Width * rectangle.Height + 1)
			{
				this._cache = new LiquidRenderer.LiquidCache[rectangle.Width * rectangle.Height + 1];
			}
			if (this._drawCache.Length < drawArea.Width * drawArea.Height + 1)
			{
				this._drawCache = new LiquidRenderer.LiquidDrawCache[drawArea.Width * drawArea.Height + 1];
			}
			if (this._drawCacheForShimmer.Length < drawArea.Width * drawArea.Height + 1)
			{
				this._drawCacheForShimmer = new LiquidRenderer.SpecialLiquidDrawCache[drawArea.Width * drawArea.Height + 1];
			}
			if (this._waveMask.Length < drawArea.Width * drawArea.Height)
			{
				this._waveMask = new Color[drawArea.Width * drawArea.Height];
			}
			Tile tile = default(Tile);
			fixed (LiquidRenderer.LiquidCache* ptr10 = &this._cache[1])
			{
				LiquidRenderer.LiquidCache* ptr = ptr10;
				int num = rectangle.Height * 2 + 2;
				LiquidRenderer.LiquidCache* ptr2 = ptr;
				for (int i = rectangle.X; i < rectangle.X + rectangle.Width; i++)
				{
					for (int j = rectangle.Y; j < rectangle.Y + rectangle.Height; j++)
					{
						tile = Main.tile[i, j];
						if (tile == null)
						{
							tile = default(Tile);
						}
						ptr2->LiquidLevel = (float)(*tile.liquid) / 255f;
						ptr2->IsHalfBrick = (tile.halfBrick() && ptr2[-1].HasLiquid && !TileID.Sets.Platforms[(int)(*tile.type)]);
						ptr2->IsSolid = WorldGen.SolidOrSlopedTile(tile);
						ptr2->HasLiquid = (*tile.liquid > 0);
						ptr2->VisibleLiquidLevel = 0f;
						ptr2->HasWall = (*tile.wall > 0);
						ptr2->Type = tile.liquidType();
						if (ptr2->IsHalfBrick && !ptr2->HasLiquid)
						{
							ptr2->Type = ptr2[-1].Type;
						}
						ptr2++;
					}
				}
				ptr2 = ptr;
				ptr2 += num;
				for (int k = 2; k < rectangle.Width - 2; k++)
				{
					for (int l = 2; l < rectangle.Height - 2; l++)
					{
						float num2 = 0f;
						if (ptr2->IsHalfBrick && ptr2[-1].HasLiquid)
						{
							num2 = 1f;
						}
						else if (!ptr2->HasLiquid)
						{
							LiquidRenderer.LiquidCache liquidCache = ptr2[-1];
							LiquidRenderer.LiquidCache liquidCache2 = ptr2[1];
							LiquidRenderer.LiquidCache liquidCache3 = ptr2[-rectangle.Height];
							LiquidRenderer.LiquidCache liquidCache4 = ptr2[rectangle.Height];
							if (liquidCache.HasLiquid && liquidCache2.HasLiquid && liquidCache.Type == liquidCache2.Type && !liquidCache.IsSolid && !liquidCache2.IsSolid)
							{
								num2 = liquidCache.LiquidLevel + liquidCache2.LiquidLevel;
								ptr2->Type = liquidCache.Type;
							}
							if (liquidCache3.HasLiquid && liquidCache4.HasLiquid && liquidCache3.Type == liquidCache4.Type && !liquidCache3.IsSolid && !liquidCache4.IsSolid)
							{
								num2 = Math.Max(num2, liquidCache3.LiquidLevel + liquidCache4.LiquidLevel);
								ptr2->Type = liquidCache3.Type;
							}
							num2 *= 0.5f;
						}
						else
						{
							num2 = ptr2->LiquidLevel;
						}
						ptr2->VisibleLiquidLevel = num2;
						ptr2->HasVisibleLiquid = (num2 != 0f);
						ptr2++;
					}
					ptr2 += 4;
				}
				ptr2 = ptr;
				for (int m = 0; m < rectangle.Width; m++)
				{
					for (int n = 0; n < rectangle.Height - 10; n++)
					{
						if (ptr2->HasVisibleLiquid && (!ptr2->IsSolid || ptr2->IsHalfBrick))
						{
							ptr2->Opacity = 1f;
							ptr2->VisibleType = ptr2->Type;
							float num3 = 1f / (float)(LiquidRenderer.WATERFALL_LENGTH[(int)ptr2->Type] + 1);
							float num4 = 1f;
							for (int num5 = 1; num5 <= LiquidRenderer.WATERFALL_LENGTH[(int)ptr2->Type]; num5++)
							{
								num4 -= num3;
								if (ptr2[num5].IsSolid)
								{
									break;
								}
								ptr2[num5].VisibleLiquidLevel = Math.Max(ptr2[num5].VisibleLiquidLevel, ptr2->VisibleLiquidLevel * num4);
								ptr2[num5].Opacity = num4;
								ptr2[num5].VisibleType = ptr2->Type;
							}
						}
						if (ptr2->IsSolid && !ptr2->IsHalfBrick)
						{
							ptr2->VisibleLiquidLevel = 1f;
							ptr2->HasVisibleLiquid = false;
						}
						else
						{
							ptr2->HasVisibleLiquid = (ptr2->VisibleLiquidLevel != 0f);
						}
						ptr2++;
					}
					ptr2 += 10;
				}
				ptr2 = ptr;
				ptr2 += num;
				for (int num6 = 2; num6 < rectangle.Width - 2; num6++)
				{
					for (int num7 = 2; num7 < rectangle.Height - 2; num7++)
					{
						if (!ptr2->HasVisibleLiquid)
						{
							ptr2->HasLeftEdge = false;
							ptr2->HasTopEdge = false;
							ptr2->HasRightEdge = false;
							ptr2->HasBottomEdge = false;
						}
						else
						{
							LiquidRenderer.LiquidCache liquidCache5 = ptr2[-1];
							LiquidRenderer.LiquidCache liquidCache6 = ptr2[1];
							LiquidRenderer.LiquidCache liquidCache7 = ptr2[-rectangle.Height];
							LiquidRenderer.LiquidCache liquidCache8 = ptr2[rectangle.Height];
							float num8 = 0f;
							float num9 = 1f;
							float num10 = 0f;
							float num11 = 1f;
							float visibleLiquidLevel = ptr2->VisibleLiquidLevel;
							if (!liquidCache5.HasVisibleLiquid)
							{
								num10 += liquidCache6.VisibleLiquidLevel * (1f - visibleLiquidLevel);
							}
							if (!liquidCache6.HasVisibleLiquid && !liquidCache6.IsSolid && !liquidCache6.IsHalfBrick)
							{
								num11 -= liquidCache5.VisibleLiquidLevel * (1f - visibleLiquidLevel);
							}
							if (!liquidCache7.HasVisibleLiquid && !liquidCache7.IsSolid && !liquidCache7.IsHalfBrick)
							{
								num8 += liquidCache8.VisibleLiquidLevel * (1f - visibleLiquidLevel);
							}
							if (!liquidCache8.HasVisibleLiquid && !liquidCache8.IsSolid && !liquidCache8.IsHalfBrick)
							{
								num9 -= liquidCache7.VisibleLiquidLevel * (1f - visibleLiquidLevel);
							}
							ptr2->LeftWall = num8;
							ptr2->RightWall = num9;
							ptr2->BottomWall = num11;
							ptr2->TopWall = num10;
							Point zero = Point.Zero;
							ptr2->HasTopEdge = ((!liquidCache5.HasVisibleLiquid && !liquidCache5.IsSolid) || num10 != 0f);
							ptr2->HasBottomEdge = ((!liquidCache6.HasVisibleLiquid && !liquidCache6.IsSolid) || num11 != 1f);
							ptr2->HasLeftEdge = ((!liquidCache7.HasVisibleLiquid && !liquidCache7.IsSolid) || num8 != 0f);
							ptr2->HasRightEdge = ((!liquidCache8.HasVisibleLiquid && !liquidCache8.IsSolid) || num9 != 1f);
							if (!ptr2->HasLeftEdge)
							{
								if (ptr2->HasRightEdge)
								{
									zero.X += 32;
								}
								else
								{
									zero.X += 16;
								}
							}
							if (ptr2->HasLeftEdge && ptr2->HasRightEdge)
							{
								zero.X = 16;
								zero.Y += 32;
								if (ptr2->HasTopEdge)
								{
									zero.Y = 16;
								}
							}
							else if (!ptr2->HasTopEdge)
							{
								if (!ptr2->HasLeftEdge && !ptr2->HasRightEdge)
								{
									zero.Y += 48;
								}
								else
								{
									zero.Y += 16;
								}
							}
							if (zero.Y == 16 && (ptr2->HasLeftEdge ^ ptr2->HasRightEdge) && (num7 + rectangle.Y) % 2 == 0)
							{
								zero.Y += 16;
							}
							ptr2->FrameOffset = zero;
						}
						ptr2++;
					}
					ptr2 += 4;
				}
				ptr2 = ptr;
				ptr2 += num;
				for (int num12 = 2; num12 < rectangle.Width - 2; num12++)
				{
					for (int num13 = 2; num13 < rectangle.Height - 2; num13++)
					{
						if (ptr2->HasVisibleLiquid)
						{
							LiquidRenderer.LiquidCache liquidCache9 = ptr2[-1];
							LiquidRenderer.LiquidCache liquidCache10 = ptr2[1];
							LiquidRenderer.LiquidCache liquidCache11 = ptr2[-rectangle.Height];
							LiquidRenderer.LiquidCache liquidCache12 = ptr2[rectangle.Height];
							ptr2->VisibleLeftWall = ptr2->LeftWall;
							ptr2->VisibleRightWall = ptr2->RightWall;
							ptr2->VisibleTopWall = ptr2->TopWall;
							ptr2->VisibleBottomWall = ptr2->BottomWall;
							if (liquidCache9.HasVisibleLiquid && liquidCache10.HasVisibleLiquid)
							{
								if (ptr2->HasLeftEdge)
								{
									ptr2->VisibleLeftWall = (ptr2->LeftWall * 2f + liquidCache9.LeftWall + liquidCache10.LeftWall) * 0.25f;
								}
								if (ptr2->HasRightEdge)
								{
									ptr2->VisibleRightWall = (ptr2->RightWall * 2f + liquidCache9.RightWall + liquidCache10.RightWall) * 0.25f;
								}
							}
							if (liquidCache11.HasVisibleLiquid && liquidCache12.HasVisibleLiquid)
							{
								if (ptr2->HasTopEdge)
								{
									ptr2->VisibleTopWall = (ptr2->TopWall * 2f + liquidCache11.TopWall + liquidCache12.TopWall) * 0.25f;
								}
								if (ptr2->HasBottomEdge)
								{
									ptr2->VisibleBottomWall = (ptr2->BottomWall * 2f + liquidCache11.BottomWall + liquidCache12.BottomWall) * 0.25f;
								}
							}
						}
						ptr2++;
					}
					ptr2 += 4;
				}
				ptr2 = ptr;
				ptr2 += num;
				for (int num14 = 2; num14 < rectangle.Width - 2; num14++)
				{
					for (int num15 = 2; num15 < rectangle.Height - 2; num15++)
					{
						if (ptr2->HasLiquid)
						{
							LiquidRenderer.LiquidCache liquidCache19 = ptr2[-1];
							LiquidRenderer.LiquidCache liquidCache13 = ptr2[1];
							LiquidRenderer.LiquidCache liquidCache14 = ptr2[-rectangle.Height];
							LiquidRenderer.LiquidCache liquidCache15 = ptr2[rectangle.Height];
							if (ptr2->HasTopEdge && !ptr2->HasBottomEdge && (ptr2->HasLeftEdge ^ ptr2->HasRightEdge))
							{
								if (ptr2->HasRightEdge)
								{
									ptr2->VisibleRightWall = liquidCache13.VisibleRightWall;
									ptr2->VisibleTopWall = liquidCache14.VisibleTopWall;
								}
								else
								{
									ptr2->VisibleLeftWall = liquidCache13.VisibleLeftWall;
									ptr2->VisibleTopWall = liquidCache15.VisibleTopWall;
								}
							}
							else if (liquidCache13.FrameOffset.X == 16 && liquidCache13.FrameOffset.Y == 32)
							{
								if (ptr2->VisibleLeftWall > 0.5f)
								{
									ptr2->VisibleLeftWall = 0f;
									ptr2->FrameOffset = new Point(0, 0);
								}
								else if (ptr2->VisibleRightWall < 0.5f)
								{
									ptr2->VisibleRightWall = 1f;
									ptr2->FrameOffset = new Point(32, 0);
								}
							}
						}
						ptr2++;
					}
					ptr2 += 4;
				}
				ptr2 = ptr;
				ptr2 += num;
				for (int num16 = 2; num16 < rectangle.Width - 2; num16++)
				{
					for (int num17 = 2; num17 < rectangle.Height - 2; num17++)
					{
						if (ptr2->HasLiquid)
						{
							LiquidRenderer.LiquidCache liquidCache16 = ptr2[-1];
							LiquidRenderer.LiquidCache liquidCache20 = ptr2[1];
							LiquidRenderer.LiquidCache liquidCache17 = ptr2[-rectangle.Height];
							LiquidRenderer.LiquidCache liquidCache18 = ptr2[rectangle.Height];
							if (!ptr2->HasBottomEdge && !ptr2->HasLeftEdge && !ptr2->HasTopEdge && !ptr2->HasRightEdge)
							{
								if (liquidCache17.HasTopEdge && liquidCache16.HasLeftEdge)
								{
									ptr2->FrameOffset.X = Math.Max(4, (int)(16f - liquidCache16.VisibleLeftWall * 16f)) - 4;
									ptr2->FrameOffset.Y = 48 + Math.Max(4, (int)(16f - liquidCache17.VisibleTopWall * 16f)) - 4;
									ptr2->VisibleLeftWall = 0f;
									ptr2->VisibleTopWall = 0f;
									ptr2->VisibleRightWall = 1f;
									ptr2->VisibleBottomWall = 1f;
								}
								else if (liquidCache18.HasTopEdge && liquidCache16.HasRightEdge)
								{
									ptr2->FrameOffset.X = 32 - Math.Min(16, (int)(liquidCache16.VisibleRightWall * 16f) - 4);
									ptr2->FrameOffset.Y = 48 + Math.Max(4, (int)(16f - liquidCache18.VisibleTopWall * 16f)) - 4;
									ptr2->VisibleLeftWall = 0f;
									ptr2->VisibleTopWall = 0f;
									ptr2->VisibleRightWall = 1f;
									ptr2->VisibleBottomWall = 1f;
								}
							}
						}
						ptr2++;
					}
					ptr2 += 4;
				}
				ptr2 = ptr;
				ptr2 += num;
				fixed (LiquidRenderer.LiquidDrawCache* ptr11 = &this._drawCache[0])
				{
					LiquidRenderer.LiquidDrawCache* ptr3 = ptr11;
					fixed (Color* ptr12 = &this._waveMask[0])
					{
						Color* ptr13 = ptr12;
						LiquidRenderer.LiquidDrawCache* ptr4 = ptr3;
						Color* ptr5 = ptr13;
						for (int num18 = 2; num18 < rectangle.Width - 2; num18++)
						{
							for (int num19 = 2; num19 < rectangle.Height - 2; num19++)
							{
								if (ptr2->HasVisibleLiquid)
								{
									float num20 = Math.Min(0.75f, ptr2->VisibleLeftWall);
									float num21 = Math.Max(0.25f, ptr2->VisibleRightWall);
									float num22 = Math.Min(0.75f, ptr2->VisibleTopWall);
									float num23 = Math.Max(0.25f, ptr2->VisibleBottomWall);
									if (ptr2->IsHalfBrick && ptr2->IsSolid && num23 > 0.5f)
									{
										num23 = 0.5f;
									}
									ptr4->IsVisible = (ptr2->HasWall || !ptr2->IsHalfBrick || !ptr2->HasLiquid || ptr2->LiquidLevel >= 1f);
									ptr4->SourceRectangle = new Rectangle((int)(16f - num21 * 16f) + ptr2->FrameOffset.X, (int)(16f - num23 * 16f) + ptr2->FrameOffset.Y, (int)Math.Ceiling((double)((num21 - num20) * 16f)), (int)Math.Ceiling((double)((num23 - num22) * 16f)));
									ptr4->IsSurfaceLiquid = (ptr2->FrameOffset.X == 16 && ptr2->FrameOffset.Y == 0 && (double)(num19 + rectangle.Y) > Main.worldSurface - 40.0);
									ptr4->Opacity = ptr2->Opacity;
									ptr4->LiquidOffset = new Vector2((float)Math.Floor((double)(num20 * 16f)), (float)Math.Floor((double)(num22 * 16f)));
									ptr4->Type = ptr2->VisibleType;
									ptr4->HasWall = ptr2->HasWall;
									byte b = LiquidRenderer.WAVE_MASK_STRENGTH[(int)ptr2->VisibleType];
									byte g = ptr5.R = (byte)(b >> 1);
									ptr5.G = g;
									ptr5.B = LiquidRenderer.VISCOSITY_MASK[(int)ptr2->VisibleType];
									ptr5.A = b;
									LiquidRenderer.LiquidCache* ptr6 = ptr2 - 1;
									if (num19 != 2 && !ptr6->HasVisibleLiquid && !ptr6->IsSolid && !ptr6->IsHalfBrick)
									{
										*(ptr5 - 1) = *ptr5;
									}
								}
								else
								{
									ptr4->IsVisible = false;
									int num24 = (!ptr2->IsSolid && !ptr2->IsHalfBrick) ? 4 : 3;
									byte b2 = LiquidRenderer.WAVE_MASK_STRENGTH[num24];
									byte g2 = ptr5.R = (byte)(b2 >> 1);
									ptr5.G = g2;
									ptr5.B = LiquidRenderer.VISCOSITY_MASK[num24];
									ptr5.A = b2;
								}
								ptr2++;
								ptr4++;
								ptr5++;
							}
							ptr2 += 4;
						}
					}
				}
				ptr2 = ptr;
				for (int num25 = rectangle.X; num25 < rectangle.X + rectangle.Width; num25++)
				{
					for (int num26 = rectangle.Y; num26 < rectangle.Y + rectangle.Height; num26++)
					{
						if (ptr2->VisibleType == 1 && ptr2->HasVisibleLiquid && Dust.lavaBubbles < 200)
						{
							if (this._random.Next(700) == 0)
							{
								Dust.NewDust(new Vector2((float)(num25 * 16), (float)(num26 * 16)), 16, 16, 35, 0f, 0f, 0, Color.White, 1f);
							}
							if (this._random.Next(350) == 0)
							{
								int num27 = Dust.NewDust(new Vector2((float)(num25 * 16), (float)(num26 * 16)), 16, 8, 35, 0f, 0f, 50, Color.White, 1.5f);
								Main.dust[num27].velocity *= 0.8f;
								Dust dust = Main.dust[num27];
								dust.velocity.X = dust.velocity.X * 2f;
								Dust dust2 = Main.dust[num27];
								dust2.velocity.Y = dust2.velocity.Y - (float)this._random.Next(1, 7) * 0.1f;
								if (this._random.Next(10) == 0)
								{
									Dust dust3 = Main.dust[num27];
									dust3.velocity.Y = dust3.velocity.Y * (float)this._random.Next(2, 5);
								}
								Main.dust[num27].noGravity = true;
							}
						}
						ptr2++;
					}
				}
				fixed (LiquidRenderer.LiquidDrawCache* ptr11 = &this._drawCache[0])
				{
					LiquidRenderer.LiquidDrawCache* ptr7 = ptr11;
					fixed (LiquidRenderer.SpecialLiquidDrawCache* ptr14 = &this._drawCacheForShimmer[0])
					{
						LiquidRenderer.SpecialLiquidDrawCache* ptr15 = ptr14;
						LiquidRenderer.LiquidDrawCache* ptr8 = ptr7;
						LiquidRenderer.SpecialLiquidDrawCache* ptr9 = ptr15;
						for (int num28 = 2; num28 < rectangle.Width - 2; num28++)
						{
							for (int num29 = 2; num29 < rectangle.Height - 2; num29++)
							{
								if (ptr8->IsVisible && ptr8->Type == 3)
								{
									ptr9->X = num28;
									ptr9->Y = num29;
									ptr9->IsVisible = ptr8->IsVisible;
									ptr9->HasWall = ptr8->HasWall;
									ptr9->IsSurfaceLiquid = ptr8->IsSurfaceLiquid;
									ptr9->LiquidOffset = ptr8->LiquidOffset;
									ptr9->Opacity = ptr8->Opacity;
									ptr9->SourceRectangle = ptr8->SourceRectangle;
									ptr9->Type = ptr8->Type;
									ptr8->IsVisible = false;
									ptr9++;
								}
								ptr8++;
							}
						}
						ptr9->IsVisible = false;
					}
				}
			}
			if (this.WaveFilters != null)
			{
				this.WaveFilters(this._waveMask, this.GetCachedDrawArea());
			}
		}

		// Token: 0x06004356 RID: 17238 RVA: 0x005FE8D8 File Offset: 0x005FCAD8
		public unsafe void DrawNormalLiquids(SpriteBatch spriteBatch, Vector2 drawOffset, int waterStyle, float globalAlpha, bool isBackgroundDraw)
		{
			Rectangle drawArea = this._drawArea;
			Main.tileBatch.Begin();
			fixed (LiquidRenderer.LiquidDrawCache* ptr3 = &this._drawCache[0])
			{
				LiquidRenderer.LiquidDrawCache* ptr2 = ptr3;
				for (int i = drawArea.X; i < drawArea.X + drawArea.Width; i++)
				{
					for (int j = drawArea.Y; j < drawArea.Y + drawArea.Height; j++)
					{
						if (ptr2->IsVisible)
						{
							Rectangle sourceRectangle = ptr2->SourceRectangle;
							if (ptr2->IsSurfaceLiquid)
							{
								sourceRectangle.Y = 1280;
							}
							else
							{
								sourceRectangle.Y += this._animationFrame * 80;
							}
							Vector2 liquidOffset = ptr2->LiquidOffset;
							float num = ptr2->Opacity * (isBackgroundDraw ? 1f : LiquidRenderer.DEFAULT_OPACITY[(int)ptr2->Type]);
							int num2 = (int)ptr2->Type;
							if (num2 != 0)
							{
								if (num2 == 2)
								{
									num2 = 11;
								}
							}
							else
							{
								num2 = waterStyle;
								num *= globalAlpha;
							}
							num = Math.Min(1f, num);
							VertexColors vertices;
							Lighting.GetCornerColors(i, j, out vertices, 1f);
							vertices.BottomLeftColor *= num;
							vertices.BottomRightColor *= num;
							vertices.TopLeftColor *= num;
							vertices.TopRightColor *= num;
							Main.DrawTileInWater(drawOffset, i, j);
							Main.tileBatch.Draw(this._liquidTextures[num2].Value, new Vector2((float)(i << 4), (float)(j << 4)) + drawOffset + liquidOffset, new Rectangle?(sourceRectangle), vertices, Vector2.Zero, 1f, 0);
						}
						ptr2++;
					}
				}
			}
			Main.tileBatch.End();
		}

		// Token: 0x06004357 RID: 17239 RVA: 0x005FEAC0 File Offset: 0x005FCCC0
		public unsafe void DrawShimmer(SpriteBatch spriteBatch, Vector2 drawOffset, bool isBackgroundDraw)
		{
			Rectangle drawArea = this._drawArea;
			Main.tileBatch.Begin();
			fixed (LiquidRenderer.SpecialLiquidDrawCache* ptr3 = &this._drawCacheForShimmer[0])
			{
				LiquidRenderer.SpecialLiquidDrawCache* ptr2 = ptr3;
				int num = this._drawCacheForShimmer.Length;
				int i = 0;
				while (i < num && ptr2->IsVisible)
				{
					Rectangle sourceRectangle = ptr2->SourceRectangle;
					if (ptr2->IsSurfaceLiquid)
					{
						sourceRectangle.Y = 1280;
					}
					else
					{
						sourceRectangle.Y += this._animationFrame * 80;
					}
					Vector2 liquidOffset = ptr2->LiquidOffset;
					float val = ptr2->Opacity * (isBackgroundDraw ? 1f : 0.75f);
					int num2 = 14;
					val = Math.Min(1f, val);
					int num3 = ptr2->X + drawArea.X - 2;
					int num4 = ptr2->Y + drawArea.Y - 2;
					VertexColors vertices;
					Lighting.GetCornerColors(num3, num4, out vertices, 1f);
					LiquidRenderer.SetShimmerVertexColors(ref vertices, val, num3, num4);
					Main.DrawTileInWater(drawOffset, num3, num4);
					Main.tileBatch.Draw(this._liquidTextures[num2].Value, new Vector2((float)(num3 << 4), (float)(num4 << 4)) + drawOffset + liquidOffset, new Rectangle?(sourceRectangle), vertices, Vector2.Zero, 1f, 0);
					sourceRectangle = ptr2->SourceRectangle;
					bool flag = sourceRectangle.X != 16 || sourceRectangle.Y % 80 != 48;
					if (flag || (num3 + num4) % 2 == 0)
					{
						sourceRectangle.X += 48;
						sourceRectangle.Y += 80 * this.GetShimmerFrame(flag, (float)num3, (float)num4);
						LiquidRenderer.SetShimmerVertexColors_Sparkle(ref vertices, ptr2->Opacity, num3, num4, flag);
						Main.tileBatch.Draw(this._liquidTextures[num2].Value, new Vector2((float)(num3 << 4), (float)(num4 << 4)) + drawOffset + liquidOffset, new Rectangle?(sourceRectangle), vertices, Vector2.Zero, 1f, 0);
					}
					ptr2++;
					i++;
				}
			}
			Main.tileBatch.End();
		}

		// Token: 0x06004358 RID: 17240 RVA: 0x005FECE4 File Offset: 0x005FCEE4
		public static VertexColors SetShimmerVertexColors_Sparkle(ref VertexColors colors, float opacity, int x, int y, bool top)
		{
			colors.BottomLeftColor = LiquidRenderer.GetShimmerGlitterColor(top, (float)x, (float)(y + 1));
			colors.BottomRightColor = LiquidRenderer.GetShimmerGlitterColor(top, (float)(x + 1), (float)(y + 1));
			colors.TopLeftColor = LiquidRenderer.GetShimmerGlitterColor(top, (float)x, (float)y);
			colors.TopRightColor = LiquidRenderer.GetShimmerGlitterColor(top, (float)(x + 1), (float)y);
			colors.BottomLeftColor *= opacity;
			colors.BottomRightColor *= opacity;
			colors.TopLeftColor *= opacity;
			colors.TopRightColor *= opacity;
			return colors;
		}

		// Token: 0x06004359 RID: 17241 RVA: 0x005FEDA0 File Offset: 0x005FCFA0
		public static void SetShimmerVertexColors(ref VertexColors colors, float opacity, int x, int y)
		{
			colors.BottomLeftColor = Color.White;
			colors.BottomRightColor = Color.White;
			colors.TopLeftColor = Color.White;
			colors.TopRightColor = Color.White;
			colors.BottomLeftColor *= opacity;
			colors.BottomRightColor *= opacity;
			colors.TopLeftColor *= opacity;
			colors.TopRightColor *= opacity;
			colors.BottomLeftColor = new Color(colors.BottomLeftColor.ToVector4() * LiquidRenderer.GetShimmerBaseColor((float)x, (float)(y + 1)));
			colors.BottomRightColor = new Color(colors.BottomRightColor.ToVector4() * LiquidRenderer.GetShimmerBaseColor((float)(x + 1), (float)(y + 1)));
			colors.TopLeftColor = new Color(colors.TopLeftColor.ToVector4() * LiquidRenderer.GetShimmerBaseColor((float)x, (float)y));
			colors.TopRightColor = new Color(colors.TopRightColor.ToVector4() * LiquidRenderer.GetShimmerBaseColor((float)(x + 1), (float)y));
		}

		// Token: 0x0600435A RID: 17242 RVA: 0x005FEECD File Offset: 0x005FD0CD
		public static float GetShimmerWave(ref float worldPositionX, ref float worldPositionY)
		{
			return (float)Math.Sin(((double)((worldPositionX + worldPositionY / 6f) / 10f) - Main.timeForVisualEffects / 360.0) * 6.2831854820251465);
		}

		// Token: 0x0600435B RID: 17243 RVA: 0x005FEF04 File Offset: 0x005FD104
		public static Color GetShimmerGlitterColor(bool top, float worldPositionX, float worldPositionY)
		{
			Color color = Main.hslToRgb((float)(((double)(worldPositionX + worldPositionY / 6f) + Main.timeForVisualEffects / 30.0) / 6.0) % 1f, 1f, 0.5f, byte.MaxValue);
			color.A = 0;
			return new Color(color.ToVector4() * LiquidRenderer.GetShimmerGlitterOpacity(top, worldPositionX, worldPositionY));
		}

		// Token: 0x0600435C RID: 17244 RVA: 0x005FEF74 File Offset: 0x005FD174
		public static float GetShimmerGlitterOpacity(bool top, float worldPositionX, float worldPositionY)
		{
			if (top)
			{
				return 0.5f;
			}
			float num3 = Utils.Remap((float)Math.Sin(((double)((worldPositionX + worldPositionY / 6f) / 10f) - Main.timeForVisualEffects / 360.0) * 6.2831854820251465), -0.5f, 1f, 0f, 0.35f, true);
			float num2 = (float)Math.Sin((double)(LiquidRenderer.SimpleWhiteNoise((uint)worldPositionX, (uint)worldPositionY) / 10f) + Main.timeForVisualEffects / 180.0);
			return Utils.Remap(num3 * num2, 0f, 0.5f, 0f, 1f, true);
		}

		// Token: 0x0600435D RID: 17245 RVA: 0x005FF019 File Offset: 0x005FD219
		private static uint SimpleWhiteNoise(uint x, uint y)
		{
			x = 36469U * (x & 65535U) + (x >> 16);
			y = 18012U * (y & 65535U) + (y >> 16);
			return (x << 16) + y;
		}

		// Token: 0x0600435E RID: 17246 RVA: 0x005FF04C File Offset: 0x005FD24C
		public int GetShimmerFrame(bool top, float worldPositionX, float worldPositionY)
		{
			worldPositionX += 0.5f;
			worldPositionY += 0.5f;
			double num = (double)((worldPositionX + worldPositionY / 6f) / 10f) - Main.timeForVisualEffects / 360.0;
			if (!top)
			{
				num += (double)(worldPositionX + worldPositionY);
			}
			return ((int)num % 16 + 16) % 16;
		}

		// Token: 0x0600435F RID: 17247 RVA: 0x005FF0A4 File Offset: 0x005FD2A4
		public static Vector4 GetShimmerBaseColor(float worldPositionX, float worldPositionY)
		{
			float shimmerWave = LiquidRenderer.GetShimmerWave(ref worldPositionX, ref worldPositionY);
			return Vector4.Lerp(new Vector4(0.64705884f, 0.50980395f, 0.93333334f, 1f), new Vector4(0.8039216f, 0.8039216f, 1f, 1f), 0.1f + shimmerWave * 0.4f);
		}

		// Token: 0x06004360 RID: 17248 RVA: 0x005FF100 File Offset: 0x005FD300
		public bool HasFullWater(int x, int y)
		{
			x -= this._drawArea.X;
			y -= this._drawArea.Y;
			int num = x * this._drawArea.Height + y;
			return num < 0 || num >= this._drawCache.Length || (this._drawCache[num].IsVisible && !this._drawCache[num].IsSurfaceLiquid);
		}

		// Token: 0x06004361 RID: 17249 RVA: 0x005FF178 File Offset: 0x005FD378
		public float GetVisibleLiquid(int x, int y)
		{
			x -= this._drawArea.X;
			y -= this._drawArea.Y;
			if (x < 0 || x >= this._drawArea.Width || y < 0 || y >= this._drawArea.Height)
			{
				return 0f;
			}
			int num = (x + 2) * (this._drawArea.Height + 4) + y + 2;
			if (!this._cache[num].HasVisibleLiquid)
			{
				return 0f;
			}
			return this._cache[num].VisibleLiquidLevel;
		}

		// Token: 0x06004362 RID: 17250 RVA: 0x005FF210 File Offset: 0x005FD410
		public void Update(GameTime gameTime)
		{
			if (!Main.gamePaused && Main.hasFocus)
			{
				float num = Main.windSpeedCurrent * 25f;
				num = ((num >= 0f) ? (num + 6f) : (num - 6f));
				this._frameState += num * (float)gameTime.ElapsedGameTime.TotalSeconds;
				if (this._frameState < 0f)
				{
					this._frameState += 16f;
				}
				this._frameState %= 16f;
				this._animationFrame = (int)this._frameState;
			}
		}

		// Token: 0x06004363 RID: 17251 RVA: 0x005FF2B2 File Offset: 0x005FD4B2
		public void PrepareDraw(Rectangle drawArea)
		{
			this.InternalPrepareDraw(drawArea);
		}

		// Token: 0x06004364 RID: 17252 RVA: 0x005FF2BC File Offset: 0x005FD4BC
		public void SetWaveMaskData(ref Texture2D texture)
		{
			try
			{
				if (texture == null || texture.Width < this._drawArea.Height || texture.Height < this._drawArea.Width)
				{
					Console.WriteLine("WaveMaskData texture recreated. {0}x{1}", this._drawArea.Height, this._drawArea.Width);
					if (texture != null)
					{
						try
						{
							texture.Dispose();
						}
						catch
						{
						}
					}
					texture = new Texture2D(Main.instance.GraphicsDevice, this._drawArea.Height, this._drawArea.Width, false, 0);
				}
				texture.SetData<Color>(0, new Rectangle?(new Rectangle(0, 0, this._drawArea.Height, this._drawArea.Width)), this._waveMask, 0, this._drawArea.Width * this._drawArea.Height);
			}
			catch
			{
				texture = new Texture2D(Main.instance.GraphicsDevice, this._drawArea.Height, this._drawArea.Width, false, 0);
				texture.SetData<Color>(0, new Rectangle?(new Rectangle(0, 0, this._drawArea.Height, this._drawArea.Width)), this._waveMask, 0, this._drawArea.Width * this._drawArea.Height);
			}
		}

		// Token: 0x06004365 RID: 17253 RVA: 0x005FF434 File Offset: 0x005FD634
		public Rectangle GetCachedDrawArea()
		{
			return this._drawArea;
		}

		// Token: 0x06004367 RID: 17255 RVA: 0x005FF4A8 File Offset: 0x005FD6A8
		// Note: this type is marked as 'beforefieldinit'.
		static LiquidRenderer()
		{
			byte[] array = new byte[5];
			array[1] = 200;
			array[2] = 240;
			LiquidRenderer.VISCOSITY_MASK = array;
		}

		// Token: 0x04005A0E RID: 23054
		private const int ANIMATION_FRAME_COUNT = 16;

		// Token: 0x04005A0F RID: 23055
		private const int CACHE_PADDING = 2;

		// Token: 0x04005A10 RID: 23056
		private const int CACHE_PADDING_2 = 4;

		// Token: 0x04005A11 RID: 23057
		private static readonly int[] WATERFALL_LENGTH = new int[]
		{
			10,
			3,
			2,
			10
		};

		// Token: 0x04005A12 RID: 23058
		private static readonly float[] DEFAULT_OPACITY = new float[]
		{
			0.6f,
			0.95f,
			0.95f,
			0.75f
		};

		// Token: 0x04005A13 RID: 23059
		private static readonly byte[] WAVE_MASK_STRENGTH = new byte[5];

		// Token: 0x04005A14 RID: 23060
		private static readonly byte[] VISCOSITY_MASK;

		// Token: 0x04005A15 RID: 23061
		public const float MIN_LIQUID_SIZE = 0.25f;

		// Token: 0x04005A16 RID: 23062
		public static LiquidRenderer Instance;

		// Token: 0x04005A17 RID: 23063
		public Asset<Texture2D>[] _liquidTextures = new Asset<Texture2D>[15];

		// Token: 0x04005A18 RID: 23064
		private LiquidRenderer.LiquidCache[] _cache = new LiquidRenderer.LiquidCache[1];

		// Token: 0x04005A19 RID: 23065
		private LiquidRenderer.LiquidDrawCache[] _drawCache = new LiquidRenderer.LiquidDrawCache[1];

		// Token: 0x04005A1A RID: 23066
		private LiquidRenderer.SpecialLiquidDrawCache[] _drawCacheForShimmer = new LiquidRenderer.SpecialLiquidDrawCache[1];

		// Token: 0x04005A1B RID: 23067
		private int _animationFrame;

		// Token: 0x04005A1C RID: 23068
		private Rectangle _drawArea = new Rectangle(0, 0, 1, 1);

		// Token: 0x04005A1D RID: 23069
		private readonly UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x04005A1E RID: 23070
		private Color[] _waveMask = new Color[1];

		// Token: 0x04005A1F RID: 23071
		private float _frameState;

		// Token: 0x02000C73 RID: 3187
		private struct LiquidCache
		{
			// Token: 0x040079B1 RID: 31153
			public float LiquidLevel;

			// Token: 0x040079B2 RID: 31154
			public float VisibleLiquidLevel;

			// Token: 0x040079B3 RID: 31155
			public float Opacity;

			// Token: 0x040079B4 RID: 31156
			public bool IsSolid;

			// Token: 0x040079B5 RID: 31157
			public bool IsHalfBrick;

			// Token: 0x040079B6 RID: 31158
			public bool HasLiquid;

			// Token: 0x040079B7 RID: 31159
			public bool HasVisibleLiquid;

			// Token: 0x040079B8 RID: 31160
			public bool HasWall;

			// Token: 0x040079B9 RID: 31161
			public Point FrameOffset;

			// Token: 0x040079BA RID: 31162
			public bool HasLeftEdge;

			// Token: 0x040079BB RID: 31163
			public bool HasRightEdge;

			// Token: 0x040079BC RID: 31164
			public bool HasTopEdge;

			// Token: 0x040079BD RID: 31165
			public bool HasBottomEdge;

			// Token: 0x040079BE RID: 31166
			public float LeftWall;

			// Token: 0x040079BF RID: 31167
			public float RightWall;

			// Token: 0x040079C0 RID: 31168
			public float BottomWall;

			// Token: 0x040079C1 RID: 31169
			public float TopWall;

			// Token: 0x040079C2 RID: 31170
			public float VisibleLeftWall;

			// Token: 0x040079C3 RID: 31171
			public float VisibleRightWall;

			// Token: 0x040079C4 RID: 31172
			public float VisibleBottomWall;

			// Token: 0x040079C5 RID: 31173
			public float VisibleTopWall;

			// Token: 0x040079C6 RID: 31174
			public byte Type;

			// Token: 0x040079C7 RID: 31175
			public byte VisibleType;
		}

		// Token: 0x02000C74 RID: 3188
		private struct LiquidDrawCache
		{
			// Token: 0x040079C8 RID: 31176
			public Rectangle SourceRectangle;

			// Token: 0x040079C9 RID: 31177
			public Vector2 LiquidOffset;

			// Token: 0x040079CA RID: 31178
			public bool IsVisible;

			// Token: 0x040079CB RID: 31179
			public float Opacity;

			// Token: 0x040079CC RID: 31180
			public byte Type;

			// Token: 0x040079CD RID: 31181
			public bool IsSurfaceLiquid;

			// Token: 0x040079CE RID: 31182
			public bool HasWall;
		}

		// Token: 0x02000C75 RID: 3189
		private struct SpecialLiquidDrawCache
		{
			// Token: 0x040079CF RID: 31183
			public int X;

			// Token: 0x040079D0 RID: 31184
			public int Y;

			// Token: 0x040079D1 RID: 31185
			public Rectangle SourceRectangle;

			// Token: 0x040079D2 RID: 31186
			public Vector2 LiquidOffset;

			// Token: 0x040079D3 RID: 31187
			public bool IsVisible;

			// Token: 0x040079D4 RID: 31188
			public float Opacity;

			// Token: 0x040079D5 RID: 31189
			public byte Type;

			// Token: 0x040079D6 RID: 31190
			public bool IsSurfaceLiquid;

			// Token: 0x040079D7 RID: 31191
			public bool HasWall;
		}
	}
}
