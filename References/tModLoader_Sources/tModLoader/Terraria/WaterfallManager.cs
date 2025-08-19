using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.Liquid;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;

namespace Terraria
{
	// Token: 0x02000069 RID: 105
	public class WaterfallManager
	{
		// Token: 0x060010EB RID: 4331 RVA: 0x00403E8C File Offset: 0x0040208C
		public void BindTo(Preferences preferences)
		{
			preferences.OnLoad += this.Configuration_OnLoad;
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00403EA0 File Offset: 0x004020A0
		private void Configuration_OnLoad(Preferences preferences)
		{
			this.maxWaterfallCount = Math.Max(0, preferences.Get<int>("WaterfallDrawLimit", 1000));
			this.waterfalls = new WaterfallManager.WaterfallData[this.maxWaterfallCount];
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00403ED0 File Offset: 0x004020D0
		public void LoadContent()
		{
			for (int i = 0; i < 26; i++)
			{
				this.waterfallTexture[i] = Main.Assets.Request<Texture2D>("Images/Waterfall_" + i.ToString(), 2);
			}
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00403F10 File Offset: 0x00402110
		public bool CheckForWaterfall(int i, int j)
		{
			for (int k = 0; k < this.currentMax; k++)
			{
				if (this.waterfalls[k].x == i && this.waterfalls[k].y == j)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00403F5C File Offset: 0x0040215C
		public unsafe void FindWaterfalls(bool forced = false)
		{
			this.findWaterfallCount++;
			if (this.findWaterfallCount < 30 && !forced)
			{
				return;
			}
			this.findWaterfallCount = 0;
			this.waterfallDist = (int)(75f * Main.gfxQuality) + 25;
			this.qualityMax = (int)((float)this.maxWaterfallCount * Main.gfxQuality);
			this.currentMax = 0;
			int num = (int)(Main.screenPosition.X / 16f - 1f);
			int num2 = (int)((Main.screenPosition.X + (float)Main.screenWidth) / 16f) + 2;
			int num3 = (int)(Main.screenPosition.Y / 16f - 1f);
			int num4 = (int)((Main.screenPosition.Y + (float)Main.screenHeight) / 16f) + 2;
			num -= this.waterfallDist;
			num2 += this.waterfallDist;
			num3 -= this.waterfallDist;
			num4 += 20;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > Main.maxTilesX)
			{
				num2 = Main.maxTilesX;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num4 > Main.maxTilesY)
			{
				num4 = Main.maxTilesY;
			}
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile == null)
					{
						tile = default(Tile);
						Main.tile[i, j] = tile;
					}
					if (tile.active())
					{
						if (tile.halfBrick())
						{
							Tile tile2 = Main.tile[i, j - 1];
							if (tile2 == null)
							{
								tile2 = default(Tile);
								Main.tile[i, j - 1] = tile2;
							}
							if (*tile2.liquid < 16 || WorldGen.SolidTile(tile2))
							{
								Tile tile3 = Main.tile[i - 1, j];
								if (tile3 == null)
								{
									tile3 = default(Tile);
									Main.tile[i - 1, j] = tile3;
								}
								Tile tile4 = Main.tile[i + 1, j];
								if (tile4 == null)
								{
									tile4 = default(Tile);
									Main.tile[i + 1, j] = tile4;
								}
								if ((*tile3.liquid > 160 || *tile4.liquid > 160) && ((*tile3.liquid == 0 && !WorldGen.SolidTile(tile3) && tile3.slope() == 0) || (*tile4.liquid == 0 && !WorldGen.SolidTile(tile4) && tile4.slope() == 0)) && this.currentMax < this.qualityMax)
								{
									this.waterfalls[this.currentMax].type = 0;
									if (tile2.lava() || tile4.lava() || tile3.lava())
									{
										this.waterfalls[this.currentMax].type = 1;
									}
									else if (tile2.honey() || tile4.honey() || tile3.honey())
									{
										this.waterfalls[this.currentMax].type = 14;
									}
									else if (tile2.shimmer() || tile4.shimmer() || tile3.shimmer())
									{
										this.waterfalls[this.currentMax].type = 25;
									}
									else
									{
										this.waterfalls[this.currentMax].type = 0;
									}
									this.waterfalls[this.currentMax].x = i;
									this.waterfalls[this.currentMax].y = j;
									this.currentMax++;
								}
							}
						}
						if (*tile.type == 196)
						{
							Tile tile5 = Main.tile[i, j + 1];
							if (tile5 == null)
							{
								tile5 = default(Tile);
								Main.tile[i, j + 1] = tile5;
							}
							if (!WorldGen.SolidTile(tile5) && tile5.slope() == 0 && this.currentMax < this.qualityMax)
							{
								this.waterfalls[this.currentMax].type = 11;
								this.waterfalls[this.currentMax].x = i;
								this.waterfalls[this.currentMax].y = j + 1;
								this.currentMax++;
							}
						}
						if (*tile.type == 460)
						{
							Tile tile6 = Main.tile[i, j + 1];
							if (tile6 == null)
							{
								tile6 = default(Tile);
								Main.tile[i, j + 1] = tile6;
							}
							if (!WorldGen.SolidTile(tile6) && tile6.slope() == 0 && this.currentMax < this.qualityMax)
							{
								this.waterfalls[this.currentMax].type = 22;
								this.waterfalls[this.currentMax].x = i;
								this.waterfalls[this.currentMax].y = j + 1;
								this.currentMax++;
							}
						}
					}
				}
			}
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0040449C File Offset: 0x0040269C
		public void UpdateFrame()
		{
			this.wFallFrCounter++;
			if (this.wFallFrCounter > 2)
			{
				this.wFallFrCounter = 0;
				this.regularFrame++;
				if (this.regularFrame > 15)
				{
					this.regularFrame = 0;
				}
			}
			this.wFallFrCounter2++;
			if (this.wFallFrCounter2 > 6)
			{
				this.wFallFrCounter2 = 0;
				this.slowFrame++;
				if (this.slowFrame > 15)
				{
					this.slowFrame = 0;
				}
			}
			this.rainFrameCounter++;
			if (this.rainFrameCounter > 0)
			{
				this.rainFrameForeground++;
				if (this.rainFrameForeground > 7)
				{
					this.rainFrameForeground -= 8;
				}
				if (this.rainFrameCounter > 2)
				{
					this.rainFrameCounter = 0;
					this.rainFrameBackground--;
					if (this.rainFrameBackground < 0)
					{
						this.rainFrameBackground = 7;
					}
				}
			}
			int num = this.snowFrameCounter + 1;
			this.snowFrameCounter = num;
			if (num > 3)
			{
				this.snowFrameCounter = 0;
				num = this.snowFrameForeground + 1;
				this.snowFrameForeground = num;
				if (num > 7)
				{
					this.snowFrameForeground = 0;
				}
			}
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x004045C4 File Offset: 0x004027C4
		internal unsafe void DrawWaterfall(int Style = 0, float Alpha = 1f)
		{
			Main.tileSolid[546] = false;
			float num = 0f;
			float num2 = 99999f;
			float num3 = 99999f;
			int num4 = -1;
			int num5 = -1;
			float num6 = 0f;
			float num7 = 99999f;
			float num8 = 99999f;
			int num9 = -1;
			int num10 = -1;
			int i = 0;
			while (i < this.currentMax)
			{
				int num11 = 0;
				int num12 = this.waterfalls[i].type;
				int num13 = this.waterfalls[i].x;
				int num14 = this.waterfalls[i].y;
				int num15 = 0;
				int num16 = 0;
				int num17 = 0;
				int num18 = 0;
				int num19 = 0;
				int num20 = 0;
				int num21;
				if (num12 != 1 && num12 != 14 && num12 != 25)
				{
					if (num12 <= 2)
					{
						if (num12 != 0)
						{
							if (num12 == 2)
							{
								if (Main.drewLava)
								{
									goto IL_149D;
								}
							}
						}
						else
						{
							num12 = Style;
						}
					}
					else if (num12 == 11 || num12 == 22)
					{
						if (Main.drewLava)
						{
							goto IL_149D;
						}
						num21 = this.waterfallDist / 4;
						if (num12 == 22)
						{
							num21 = this.waterfallDist / 2;
						}
						if (this.waterfalls[i].stopAtStep > num21)
						{
							this.waterfalls[i].stopAtStep = num21;
						}
						if (this.waterfalls[i].stopAtStep != 0 && (float)(num14 + num21) >= Main.screenPosition.Y / 16f && (float)num13 >= Main.screenPosition.X / 16f - 20f && (float)num13 <= (Main.screenPosition.X + (float)Main.screenWidth) / 16f + 20f)
						{
							int num22;
							int num23;
							if (num13 % 2 == 0)
							{
								num22 = this.rainFrameForeground + 3;
								if (num22 > 7)
								{
									num22 -= 8;
								}
								num23 = this.rainFrameBackground + 2;
								if (num23 > 7)
								{
									num23 -= 8;
								}
								if (num12 == 22)
								{
									num22 = this.snowFrameForeground + 3;
									if (num22 > 7)
									{
										num22 -= 8;
									}
								}
							}
							else
							{
								num22 = this.rainFrameForeground;
								num23 = this.rainFrameBackground;
								if (num12 == 22)
								{
									num22 = this.snowFrameForeground;
								}
							}
							Rectangle value;
							value..ctor(num23 * 18, 0, 16, 16);
							Rectangle value2;
							value2..ctor(num22 * 18, 0, 16, 16);
							Vector2 origin;
							origin..ctor(8f, 8f);
							Vector2 position = (num14 % 2 != 0) ? (new Vector2((float)(num13 * 16 + 8), (float)(num14 * 16 + 8)) - Main.screenPosition) : (new Vector2((float)(num13 * 16 + 9), (float)(num14 * 16 + 8)) - Main.screenPosition);
							Tile tile = Main.tile[num13, num14 - 1];
							if (tile.active() && tile.bottomSlope())
							{
								position.Y -= 16f;
							}
							bool flag = false;
							float rotation = 0f;
							for (int j = 0; j < num21; j++)
							{
								Color color6 = Lighting.GetColor(num13, num14);
								float num24 = 0.6f;
								float num25 = 0.3f;
								if (j > num21 - 8)
								{
									float num26 = (float)(num21 - j) / 8f;
									num24 *= num26;
									num25 *= num26;
								}
								Color color2 = color6 * num24;
								Color color3 = color6 * num25;
								if (num12 == 22)
								{
									Main.spriteBatch.Draw(this.waterfallTexture[22].Value, position, new Rectangle?(value2), color2, 0f, origin, 1f, 0, 0f);
								}
								else
								{
									Main.spriteBatch.Draw(this.waterfallTexture[12].Value, position, new Rectangle?(value), color3, rotation, origin, 1f, 0, 0f);
									Main.spriteBatch.Draw(this.waterfallTexture[11].Value, position, new Rectangle?(value2), color2, rotation, origin, 1f, 0, 0f);
								}
								if (flag)
								{
									break;
								}
								num14++;
								Tile tile2 = Main.tile[num13, num14];
								if (WorldGen.SolidTile(tile2))
								{
									flag = true;
								}
								if (*tile2.liquid > 0)
								{
									int num27 = (int)(16f * ((float)(*tile2.liquid) / 255f)) & 254;
									if (num27 >= 15)
									{
										break;
									}
									value2.Height -= num27;
									value.Height -= num27;
								}
								if (num14 % 2 == 0)
								{
									position.X += 1f;
								}
								else
								{
									position.X -= 1f;
								}
								position.Y += 16f;
							}
							this.waterfalls[i].stopAtStep = 0;
							goto IL_149D;
						}
						goto IL_149D;
					}
					int num28 = 32 * this.regularFrame;
					goto IL_4CB;
				}
				if (!Main.drewLava && this.waterfalls[i].stopAtStep != 0)
				{
					int num28 = 32 * this.slowFrame;
					goto IL_4CB;
				}
				IL_149D:
				i++;
				continue;
				IL_4CB:
				int num29 = 0;
				num21 = this.waterfallDist;
				Color color4 = Color.White;
				int k = 0;
				while (k < num21 && num29 < 2)
				{
					WaterfallManager.AddLight(num12, num13, num14);
					Tile tile3 = Main.tile[num13, num14];
					if (tile3 == null)
					{
						tile3 = default(Tile);
						Main.tile[num13, num14] = tile3;
					}
					if (tile3.nactive() && Main.tileSolid[(int)(*tile3.type)] && !Main.tileSolidTop[(int)(*tile3.type)] && !TileID.Sets.Platforms[(int)(*tile3.type)] && tile3.blockType() == 0)
					{
						break;
					}
					Tile tile4 = Main.tile[num13 - 1, num14];
					if (tile4 == null)
					{
						tile4 = default(Tile);
						Main.tile[num13 - 1, num14] = tile4;
					}
					Tile tile5 = Main.tile[num13, num14 + 1];
					if (tile5 == null)
					{
						tile5 = default(Tile);
						Main.tile[num13, num14 + 1] = tile5;
					}
					Tile tile6 = Main.tile[num13 + 1, num14];
					if (tile6 == null)
					{
						tile6 = default(Tile);
						Main.tile[num13 + 1, num14] = tile6;
					}
					if (WorldGen.SolidTile(tile5) && !tile3.halfBrick())
					{
						num11 = 8;
					}
					else if (num16 != 0)
					{
						num11 = 0;
					}
					int num30 = 0;
					int num31 = num18;
					bool flag2 = false;
					int num32;
					int num33;
					if (tile5.topSlope() && !tile3.halfBrick() && *tile5.type != 19)
					{
						flag2 = true;
						if (tile5.slope() == 1)
						{
							num30 = 1;
							num32 = 1;
							num17 = 1;
							num18 = num17;
						}
						else
						{
							num30 = -1;
							num32 = -1;
							num17 = -1;
							num18 = num17;
						}
						num33 = 1;
					}
					else if ((!WorldGen.SolidTile(tile5) && !tile5.bottomSlope() && !tile3.halfBrick()) || (!tile5.active() && !tile3.halfBrick()))
					{
						num29 = 0;
						num33 = 1;
						num32 = 0;
					}
					else if ((WorldGen.SolidTile(tile4) || tile4.topSlope() || *tile4.liquid > 0) && !WorldGen.SolidTile(tile6) && *tile6.liquid == 0)
					{
						if (num17 == -1)
						{
							num29++;
						}
						num32 = 1;
						num33 = 0;
						num17 = 1;
					}
					else if ((WorldGen.SolidTile(tile6) || tile6.topSlope() || *tile6.liquid > 0) && !WorldGen.SolidTile(tile4) && *tile4.liquid == 0)
					{
						if (num17 == 1)
						{
							num29++;
						}
						num32 = -1;
						num33 = 0;
						num17 = -1;
					}
					else if (((!WorldGen.SolidTile(tile6) && !tile3.topSlope()) || *tile6.liquid == 0) && !WorldGen.SolidTile(tile4) && !tile3.topSlope() && *tile4.liquid == 0)
					{
						num33 = 0;
						num32 = num17;
					}
					else
					{
						num29++;
						num33 = 0;
						num32 = 0;
					}
					if (num29 >= 2)
					{
						num17 *= -1;
						num32 *= -1;
					}
					int num34 = -1;
					if (num12 != 1 && num12 != 14 && num12 != 25)
					{
						if (tile5.active())
						{
							num34 = (int)(*tile5.type);
						}
						if (tile3.active())
						{
							num34 = (int)(*tile3.type);
						}
					}
					if (num34 != 160)
					{
						if (num34 - 262 <= 6)
						{
							num12 = 15 + num34 - 262;
						}
					}
					else
					{
						num12 = 2;
					}
					if (num34 != -1)
					{
						TileLoader.ChangeWaterfallStyle(num34, ref num12);
					}
					Color color5 = Lighting.GetColor(num13, num14);
					if (k > 50)
					{
						WaterfallManager.TrySparkling(num13, num14, num17, color5);
					}
					float alpha = WaterfallManager.GetAlpha(Alpha, num21, num12, num14, k, tile3);
					color5 = WaterfallManager.StylizeColor(alpha, num21, num12, num14, k, tile3, color5);
					if (num12 == 1)
					{
						float num35 = Math.Abs((float)(num13 * 16 + 8) - (Main.screenPosition.X + (float)(Main.screenWidth / 2)));
						float num36 = Math.Abs((float)(num14 * 16 + 8) - (Main.screenPosition.Y + (float)(Main.screenHeight / 2)));
						if (num35 < (float)(Main.screenWidth * 2) && num36 < (float)(Main.screenHeight * 2))
						{
							float num37 = (float)Math.Sqrt((double)(num35 * num35 + num36 * num36));
							float num38 = 1f - num37 / ((float)Main.screenWidth * 0.75f);
							if (num38 > 0f)
							{
								num6 += num38;
							}
						}
						if (num35 < num7)
						{
							num7 = num35;
							num9 = num13 * 16 + 8;
						}
						if (num36 < num8)
						{
							num8 = num35;
							num10 = num14 * 16 + 8;
						}
					}
					else if (num12 != 1 && num12 != 14 && num12 != 25 && num12 != 11 && num12 != 12 && num12 != 22)
					{
						float num39 = Math.Abs((float)(num13 * 16 + 8) - (Main.screenPosition.X + (float)(Main.screenWidth / 2)));
						float num40 = Math.Abs((float)(num14 * 16 + 8) - (Main.screenPosition.Y + (float)(Main.screenHeight / 2)));
						if (num39 < (float)(Main.screenWidth * 2) && num40 < (float)(Main.screenHeight * 2))
						{
							float num41 = (float)Math.Sqrt((double)(num39 * num39 + num40 * num40));
							float num42 = 1f - num41 / ((float)Main.screenWidth * 0.75f);
							if (num42 > 0f)
							{
								num += num42;
							}
						}
						if (num39 < num2)
						{
							num2 = num39;
							num4 = num13 * 16 + 8;
						}
						if (num40 < num3)
						{
							num3 = num39;
							num5 = num14 * 16 + 8;
						}
					}
					int num43 = (int)(*tile3.liquid / 16);
					int num28;
					if (flag2 && num17 != num31)
					{
						int num44 = 2;
						if (num31 == 1)
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16 + 16 - num44)) - Main.screenPosition, new Rectangle(num28, 24, 32, 16 - num43 - num44), color5, 1);
						}
						else
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + 16 - num44)) - Main.screenPosition, new Rectangle(num28, 24, 32, 16 - num43 - num44), color5, 0);
						}
					}
					if (num15 == 0 && num30 != 0 && num16 == 1 && num17 != num18)
					{
						num30 = 0;
						num17 = num18;
						color5 = Color.White;
						if (num17 == 1)
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16 + 16)) - Main.screenPosition, new Rectangle(num28, 24, 32, 16 - num43), color5, 1);
						}
						else
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16 + 16)) - Main.screenPosition, new Rectangle(num28, 24, 32, 16 - num43), color5, 1);
						}
					}
					if (num19 != 0 && num32 == 0 && num33 == 1)
					{
						if (num17 == 1)
						{
							if (num20 != num12)
							{
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11 + 8)) - Main.screenPosition, new Rectangle(num28, 0, 16, 16 - num43 - 8), color4, 1);
							}
							else
							{
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11 + 8)) - Main.screenPosition, new Rectangle(num28, 0, 16, 16 - num43 - 8), color5, 1);
							}
						}
						else
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11 + 8)) - Main.screenPosition, new Rectangle(num28, 0, 16, 16 - num43 - 8), color5, 0);
						}
					}
					if (num11 == 8 && num16 == 1 && num19 == 0)
					{
						if (num18 == -1)
						{
							if (num20 != num12)
							{
								this.DrawWaterfall(num20, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num28, 24, 32, 8), color4, 0);
							}
							else
							{
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num28, 24, 32, 8), color5, 0);
							}
						}
						else if (num20 != num12)
						{
							this.DrawWaterfall(num20, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num28, 24, 32, 8), color4, 1);
						}
						else
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num28, 24, 32, 8), color5, 1);
						}
					}
					if (num30 != 0 && num15 == 0)
					{
						if (num31 == 1)
						{
							if (num20 != num12)
							{
								this.DrawWaterfall(num20, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num28, 24, 32, 16 - num43), color4, 1);
							}
							else
							{
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num28, 24, 32, 16 - num43), color5, 1);
							}
						}
						else if (num20 != num12)
						{
							this.DrawWaterfall(num20, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num28, 24, 32, 16 - num43), color4, 0);
						}
						else
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num28, 24, 32, 16 - num43), color5, 0);
						}
					}
					if (num33 == 1 && num30 == 0 && num19 == 0)
					{
						if (num17 == -1)
						{
							if (num16 == 0)
							{
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11)) - Main.screenPosition, new Rectangle(num28, 0, 16, 16 - num43), color5, 0);
							}
							else if (num20 != num12)
							{
								this.DrawWaterfall(num20, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num28, 24, 32, 16 - num43), color4, 0);
							}
							else
							{
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num28, 24, 32, 16 - num43), color5, 0);
							}
						}
						else if (num16 == 0)
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11)) - Main.screenPosition, new Rectangle(num28, 0, 16, 16 - num43), color5, 1);
						}
						else if (num20 != num12)
						{
							this.DrawWaterfall(num20, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num28, 24, 32, 16 - num43), color4, 1);
						}
						else
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num28, 24, 32, 16 - num43), color5, 1);
						}
					}
					else
					{
						switch (num32)
						{
						case -1:
							if (*Main.tile[num13, num14].liquid <= 0 || Main.tile[num13, num14].halfBrick())
							{
								if (num30 == -1)
								{
									for (int l = 0; l < 8; l++)
									{
										int num45 = l * 2;
										int num46 = l * 2;
										int num47 = 14 - l * 2;
										num11 = 8;
										if (num15 == 0 && l > 5)
										{
											num47 = 4;
										}
										this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 + num45), (float)(num14 * 16 + num11 + num47)) - Main.screenPosition, new Rectangle(16 + num28 + num46, 0, 2, 16 - num11), color5, 1);
									}
								}
								else
								{
									int height = 16;
									if (TileID.Sets.BlocksWaterDrawingBehindSelf[(int)(*Main.tile[num13, num14].type)])
									{
										height = 8;
									}
									else if (TileID.Sets.BlocksWaterDrawingBehindSelf[(int)(*Main.tile[num13, num14 + 1].type)])
									{
										height = 8;
									}
									this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11)) - Main.screenPosition, new Rectangle(16 + num28, 0, 16, height), color5, 0);
								}
							}
							break;
						case 0:
							if (num33 == 0)
							{
								if (*Main.tile[num13, num14].liquid <= 0 || Main.tile[num13, num14].halfBrick())
								{
									this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11)) - Main.screenPosition, new Rectangle(16 + num28, 0, 16, 16), color5, 0);
								}
								k = 1000;
							}
							break;
						case 1:
							if (*Main.tile[num13, num14].liquid <= 0 || Main.tile[num13, num14].halfBrick())
							{
								if (num30 == 1)
								{
									for (int m = 0; m < 8; m++)
									{
										int num48 = m * 2;
										int num49 = 14 - m * 2;
										int num50 = num48;
										num11 = 8;
										if (num15 == 0 && m < 2)
										{
											num50 = 4;
										}
										this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 + num48), (float)(num14 * 16 + num11 + num50)) - Main.screenPosition, new Rectangle(16 + num28 + num49, 0, 2, 16 - num11), color5, 1);
									}
								}
								else
								{
									int height2 = 16;
									if (TileID.Sets.BlocksWaterDrawingBehindSelf[(int)(*Main.tile[num13, num14].type)])
									{
										height2 = 8;
									}
									else if (TileID.Sets.BlocksWaterDrawingBehindSelf[(int)(*Main.tile[num13, num14 + 1].type)])
									{
										height2 = 8;
									}
									this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11)) - Main.screenPosition, new Rectangle(16 + num28, 0, 16, height2), color5, 1);
								}
							}
							break;
						}
					}
					if (*tile3.liquid > 0 && !tile3.halfBrick())
					{
						k = 1000;
					}
					num16 = num33;
					num18 = num17;
					num15 = num32;
					num13 += num32;
					num14 += num33;
					num19 = num30;
					color4 = color5;
					if (num20 != num12)
					{
						num20 = num12;
					}
					if ((tile4.active() && (*tile4.type == 189 || *tile4.type == 196)) || (tile6.active() && (*tile6.type == 189 || *tile6.type == 196)) || (tile5.active() && (*tile5.type == 189 || *tile5.type == 196)))
					{
						num21 = (int)(40f * ((float)Main.maxTilesX / 4200f) * Main.gfxQuality);
					}
					k++;
				}
				goto IL_149D;
			}
			Main.ambientWaterfallX = (float)num4;
			Main.ambientWaterfallY = (float)num5;
			Main.ambientWaterfallStrength = num;
			Main.ambientLavafallX = (float)num9;
			Main.ambientLavafallY = (float)num10;
			Main.ambientLavafallStrength = num6;
			Main.tileSolid[546] = true;
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00405ABC File Offset: 0x00403CBC
		private void DrawWaterfall(int waterfallType, int x, int y, float opacity, Vector2 position, Rectangle sourceRect, Color color, SpriteEffects effects)
		{
			Texture2D value = this.waterfallTexture[waterfallType].Value;
			if (waterfallType == 25)
			{
				VertexColors vertices;
				Lighting.GetCornerColors(x, y, out vertices, 1f);
				LiquidRenderer.SetShimmerVertexColors(ref vertices, opacity, x, y);
				Main.tileBatch.Draw(value, position + new Vector2(0f, 0f), new Rectangle?(sourceRect), vertices, default(Vector2), 1f, effects);
				sourceRect.Y += 42;
				LiquidRenderer.SetShimmerVertexColors_Sparkle(ref vertices, opacity, x, y, true);
				Main.tileBatch.Draw(value, position + new Vector2(0f, 0f), new Rectangle?(sourceRect), vertices, default(Vector2), 1f, effects);
				return;
			}
			Main.spriteBatch.Draw(value, position, new Rectangle?(sourceRect), color, 0f, default(Vector2), 1f, effects, 0f);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00405BB8 File Offset: 0x00403DB8
		private static Color StylizeColor(float alpha, int maxSteps, int waterfallType, int y, int s, Tile tileCache, Color aColor)
		{
			float num = (float)aColor.R * alpha;
			float num2 = (float)aColor.G * alpha;
			float num3 = (float)aColor.B * alpha;
			float num4 = (float)aColor.A * alpha;
			if (waterfallType != 1)
			{
				if (waterfallType != 2)
				{
					if (waterfallType - 15 <= 6)
					{
						num = 255f * alpha;
						num2 = 255f * alpha;
						num3 = 255f * alpha;
					}
				}
				else
				{
					num = (float)Main.DiscoR * alpha;
					num2 = (float)Main.DiscoG * alpha;
					num3 = (float)Main.DiscoB * alpha;
				}
			}
			else
			{
				if (num < 190f * alpha)
				{
					num = 190f * alpha;
				}
				if (num2 < 190f * alpha)
				{
					num2 = 190f * alpha;
				}
				if (num3 < 190f * alpha)
				{
					num3 = 190f * alpha;
				}
			}
			if (waterfallType >= 26)
			{
				LoaderManager.Get<WaterFallStylesLoader>().Get(waterfallType).ColorMultiplier(ref num, ref num2, ref num3, alpha);
			}
			aColor..ctor((int)num, (int)num2, (int)num3, (int)num4);
			return aColor;
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00405C9C File Offset: 0x00403E9C
		private unsafe static float GetAlpha(float Alpha, int maxSteps, int waterfallType, int y, int s, Tile tileCache)
		{
			float num;
			if (waterfallType != 1)
			{
				if (waterfallType != 14)
				{
					if (waterfallType != 25)
					{
						num = ((*tileCache.wall != 0 || (double)y >= Main.worldSurface) ? (0.6f * Alpha) : Alpha);
					}
					else
					{
						num = 0.75f;
					}
				}
				else
				{
					num = 0.8f;
				}
			}
			else
			{
				num = 1f;
			}
			if (s > maxSteps - 10)
			{
				num *= (float)(maxSteps - s) / 10f;
			}
			return num;
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00405D08 File Offset: 0x00403F08
		private static void TrySparkling(int x, int y, int direction, Color aColor2)
		{
			if (aColor2.R > 20 || aColor2.B > 20 || aColor2.G > 20)
			{
				float num = (float)aColor2.R;
				if ((float)aColor2.G > num)
				{
					num = (float)aColor2.G;
				}
				if ((float)aColor2.B > num)
				{
					num = (float)aColor2.B;
				}
				if ((float)Main.rand.Next(20000) < num / 30f)
				{
					int num2 = Dust.NewDust(new Vector2((float)(x * 16 - direction * 7), (float)(y * 16 + 6)), 10, 8, 43, 0f, 0f, 254, Color.White, 0.5f);
					Main.dust[num2].velocity *= 0f;
				}
			}
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00405DD8 File Offset: 0x00403FD8
		private static void AddLight(int waterfallType, int x, int y)
		{
			if (waterfallType >= 26)
			{
				LoaderManager.Get<WaterFallStylesLoader>().Get(waterfallType).AddLight(x, y);
			}
			if (waterfallType == 1)
			{
				float r;
				float num3 = r = (0.55f + (float)(270 - (int)Main.mouseTextColor) / 900f) * 0.4f;
				float g = num3 * 0.3f;
				float b = num3 * 0.1f;
				Lighting.AddLight(x, y, r, g, b);
				return;
			}
			if (waterfallType != 2)
			{
				switch (waterfallType)
				{
				case 15:
				{
					float r2 = 0f;
					float g2 = 0f;
					float b2 = 0.2f;
					Lighting.AddLight(x, y, r2, g2, b2);
					return;
				}
				case 16:
				{
					float r3 = 0f;
					float g3 = 0.2f;
					float b3 = 0f;
					Lighting.AddLight(x, y, r3, g3, b3);
					return;
				}
				case 17:
				{
					float r4 = 0f;
					float g4 = 0f;
					float b4 = 0.2f;
					Lighting.AddLight(x, y, r4, g4, b4);
					return;
				}
				case 18:
				{
					float r5 = 0f;
					float g5 = 0.2f;
					float b5 = 0f;
					Lighting.AddLight(x, y, r5, g5, b5);
					return;
				}
				case 19:
				{
					float r6 = 0.2f;
					float g6 = 0f;
					float b6 = 0f;
					Lighting.AddLight(x, y, r6, g6, b6);
					return;
				}
				case 20:
					Lighting.AddLight(x, y, 0.2f, 0.2f, 0.2f);
					return;
				case 21:
				{
					float r7 = 0.2f;
					float g7 = 0f;
					float b7 = 0f;
					Lighting.AddLight(x, y, r7, g7, b7);
					return;
				}
				case 22:
				case 23:
				case 24:
					break;
				case 25:
				{
					float num = 0.7f;
					float num2 = 0.7f;
					num += (float)(270 - (int)Main.mouseTextColor) / 900f;
					num2 += (float)(270 - (int)Main.mouseTextColor) / 125f;
					Lighting.AddLight(x, y, num * 0.6f, num2 * 0.25f, num * 0.9f);
					break;
				}
				default:
					return;
				}
				return;
			}
			float r8 = (float)Main.DiscoR / 255f;
			float g8 = (float)Main.DiscoG / 255f;
			float b8 = (float)Main.DiscoB / 255f;
			r8 *= 0.2f;
			g8 *= 0.2f;
			b8 *= 0.2f;
			Lighting.AddLight(x, y, r8, g8, b8);
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0040600C File Offset: 0x0040420C
		public void Draw(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < this.currentMax; i++)
			{
				this.waterfalls[i].stopAtStep = this.waterfallDist;
			}
			Main.drewLava = false;
			if (Main.liquidAlpha[0] > 0f)
			{
				this.DrawWaterfall(0, Main.liquidAlpha[0]);
			}
			if (Main.liquidAlpha[2] > 0f)
			{
				this.DrawWaterfall(3, Main.liquidAlpha[2]);
			}
			if (Main.liquidAlpha[3] > 0f)
			{
				this.DrawWaterfall(4, Main.liquidAlpha[3]);
			}
			if (Main.liquidAlpha[4] > 0f)
			{
				this.DrawWaterfall(5, Main.liquidAlpha[4]);
			}
			if (Main.liquidAlpha[5] > 0f)
			{
				this.DrawWaterfall(6, Main.liquidAlpha[5]);
			}
			if (Main.liquidAlpha[6] > 0f)
			{
				this.DrawWaterfall(7, Main.liquidAlpha[6]);
			}
			if (Main.liquidAlpha[7] > 0f)
			{
				this.DrawWaterfall(8, Main.liquidAlpha[7]);
			}
			if (Main.liquidAlpha[8] > 0f)
			{
				this.DrawWaterfall(9, Main.liquidAlpha[8]);
			}
			if (Main.liquidAlpha[9] > 0f)
			{
				this.DrawWaterfall(10, Main.liquidAlpha[9]);
			}
			if (Main.liquidAlpha[10] > 0f)
			{
				this.DrawWaterfall(13, Main.liquidAlpha[10]);
			}
			if (Main.liquidAlpha[12] > 0f)
			{
				this.DrawWaterfall(23, Main.liquidAlpha[12]);
			}
			if (Main.liquidAlpha[13] > 0f)
			{
				this.DrawWaterfall(24, Main.liquidAlpha[13]);
			}
			LoaderManager.Get<WaterStylesLoader>().DrawWaterfall(this);
		}

		// Token: 0x04000EFE RID: 3838
		private const int minWet = 160;

		// Token: 0x04000EFF RID: 3839
		private const int maxWaterfallCountDefault = 1000;

		// Token: 0x04000F00 RID: 3840
		private const int maxLength = 100;

		// Token: 0x04000F01 RID: 3841
		internal const int maxTypes = 26;

		// Token: 0x04000F02 RID: 3842
		public int maxWaterfallCount = 1000;

		// Token: 0x04000F03 RID: 3843
		private int qualityMax;

		// Token: 0x04000F04 RID: 3844
		private int currentMax;

		// Token: 0x04000F05 RID: 3845
		private WaterfallManager.WaterfallData[] waterfalls = new WaterfallManager.WaterfallData[1000];

		// Token: 0x04000F06 RID: 3846
		internal Asset<Texture2D>[] waterfallTexture = new Asset<Texture2D>[26];

		// Token: 0x04000F07 RID: 3847
		private int wFallFrCounter;

		// Token: 0x04000F08 RID: 3848
		private int regularFrame;

		// Token: 0x04000F09 RID: 3849
		private int wFallFrCounter2;

		// Token: 0x04000F0A RID: 3850
		private int slowFrame;

		// Token: 0x04000F0B RID: 3851
		private int rainFrameCounter;

		// Token: 0x04000F0C RID: 3852
		private int rainFrameForeground;

		// Token: 0x04000F0D RID: 3853
		private int rainFrameBackground;

		// Token: 0x04000F0E RID: 3854
		private int snowFrameCounter;

		// Token: 0x04000F0F RID: 3855
		private int snowFrameForeground;

		// Token: 0x04000F10 RID: 3856
		private int findWaterfallCount;

		// Token: 0x04000F11 RID: 3857
		private int waterfallDist = 100;

		// Token: 0x020007FB RID: 2043
		public struct WaterfallData
		{
			// Token: 0x040067C2 RID: 26562
			public int x;

			// Token: 0x040067C3 RID: 26563
			public int y;

			// Token: 0x040067C4 RID: 26564
			public int type;

			// Token: 0x040067C5 RID: 26565
			public int stopAtStep;
		}
	}
}
