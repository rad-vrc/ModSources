﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.Liquid;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.IO;

namespace Terraria
{
	// Token: 0x0200004D RID: 77
	public class WaterfallManager
	{
		// Token: 0x06000DC5 RID: 3525 RVA: 0x003F0EE5 File Offset: 0x003EF0E5
		public void BindTo(Preferences preferences)
		{
			preferences.OnLoad += this.Configuration_OnLoad;
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x003F0EF9 File Offset: 0x003EF0F9
		private void Configuration_OnLoad(Preferences preferences)
		{
			this.maxWaterfallCount = Math.Max(0, preferences.Get<int>("WaterfallDrawLimit", 1000));
			this.waterfalls = new WaterfallManager.WaterfallData[this.maxWaterfallCount];
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x003F0F28 File Offset: 0x003EF128
		public void LoadContent()
		{
			for (int i = 0; i < 26; i++)
			{
				this.waterfallTexture[i] = Main.Assets.Request<Texture2D>("Images/Waterfall_" + i, 2);
			}
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x003F0F68 File Offset: 0x003EF168
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

		// Token: 0x06000DC9 RID: 3529 RVA: 0x003F0FB4 File Offset: 0x003EF1B4
		public void FindWaterfalls(bool forced = false)
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
						tile = new Tile();
						Main.tile[i, j] = tile;
					}
					if (tile.active())
					{
						if (tile.halfBrick())
						{
							Tile tile2 = Main.tile[i, j - 1];
							if (tile2 == null)
							{
								tile2 = new Tile();
								Main.tile[i, j - 1] = tile2;
							}
							if (tile2.liquid < 16 || WorldGen.SolidTile(tile2))
							{
								Tile tile3 = Main.tile[i - 1, j];
								if (tile3 == null)
								{
									tile3 = new Tile();
									Main.tile[i - 1, j] = tile3;
								}
								Tile tile4 = Main.tile[i + 1, j];
								if (tile4 == null)
								{
									tile4 = new Tile();
									Main.tile[i + 1, j] = tile4;
								}
								if ((tile3.liquid > 160 || tile4.liquid > 160) && ((tile3.liquid == 0 && !WorldGen.SolidTile(tile3) && tile3.slope() == 0) || (tile4.liquid == 0 && !WorldGen.SolidTile(tile4) && tile4.slope() == 0)) && this.currentMax < this.qualityMax)
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
						if (tile.type == 196)
						{
							Tile tile5 = Main.tile[i, j + 1];
							if (tile5 == null)
							{
								tile5 = new Tile();
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
						if (tile.type == 460)
						{
							Tile tile6 = Main.tile[i, j + 1];
							if (tile6 == null)
							{
								tile6 = new Tile();
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

		// Token: 0x06000DCA RID: 3530 RVA: 0x003F14C4 File Offset: 0x003EF6C4
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

		// Token: 0x06000DCB RID: 3531 RVA: 0x003F15EC File Offset: 0x003EF7EC
		private void DrawWaterfall(int Style = 0, float Alpha = 1f)
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
				int num22;
				if (num12 == 1 || num12 == 14 || num12 == 25)
				{
					if (!Main.drewLava && this.waterfalls[i].stopAtStep != 0)
					{
						int num21 = 32 * this.slowFrame;
						goto IL_4BB;
					}
				}
				else
				{
					if (num12 != 11 && num12 != 22)
					{
						if (num12 == 0)
						{
							num12 = Style;
						}
						else if (num12 == 2 && Main.drewLava)
						{
							goto IL_1421;
						}
						int num21 = 32 * this.regularFrame;
						goto IL_4BB;
					}
					if (!Main.drewLava)
					{
						num22 = this.waterfallDist / 4;
						if (num12 == 22)
						{
							num22 = this.waterfallDist / 2;
						}
						if (this.waterfalls[i].stopAtStep > num22)
						{
							this.waterfalls[i].stopAtStep = num22;
						}
						if (this.waterfalls[i].stopAtStep != 0 && (float)(num14 + num22) >= Main.screenPosition.Y / 16f && (float)num13 >= Main.screenPosition.X / 16f - 20f && (float)num13 <= (Main.screenPosition.X + (float)Main.screenWidth) / 16f + 20f)
						{
							int num23;
							int num24;
							if (num13 % 2 == 0)
							{
								num23 = this.rainFrameForeground + 3;
								if (num23 > 7)
								{
									num23 -= 8;
								}
								num24 = this.rainFrameBackground + 2;
								if (num24 > 7)
								{
									num24 -= 8;
								}
								if (num12 == 22)
								{
									num23 = this.snowFrameForeground + 3;
									if (num23 > 7)
									{
										num23 -= 8;
									}
								}
							}
							else
							{
								num23 = this.rainFrameForeground;
								num24 = this.rainFrameBackground;
								if (num12 == 22)
								{
									num23 = this.snowFrameForeground;
								}
							}
							Rectangle value = new Rectangle(num24 * 18, 0, 16, 16);
							Rectangle value2 = new Rectangle(num23 * 18, 0, 16, 16);
							Vector2 origin = new Vector2(8f, 8f);
							Vector2 position;
							if (num14 % 2 == 0)
							{
								position = new Vector2((float)(num13 * 16 + 9), (float)(num14 * 16 + 8)) - Main.screenPosition;
							}
							else
							{
								position = new Vector2((float)(num13 * 16 + 8), (float)(num14 * 16 + 8)) - Main.screenPosition;
							}
							Tile tile = Main.tile[num13, num14 - 1];
							if (tile.active() && tile.bottomSlope())
							{
								position.Y -= 16f;
							}
							bool flag = false;
							float rotation = 0f;
							for (int j = 0; j < num22; j++)
							{
								Color color = Lighting.GetColor(num13, num14);
								float num25 = 0.6f;
								float num26 = 0.3f;
								if (j > num22 - 8)
								{
									float num27 = (float)(num22 - j) / 8f;
									num25 *= num27;
									num26 *= num27;
								}
								Color color2 = color * num25;
								Color color3 = color * num26;
								if (num12 == 22)
								{
									Main.spriteBatch.Draw(this.waterfallTexture[22].Value, position, new Rectangle?(value2), color2, 0f, origin, 1f, SpriteEffects.None, 0f);
								}
								else
								{
									Main.spriteBatch.Draw(this.waterfallTexture[12].Value, position, new Rectangle?(value), color3, rotation, origin, 1f, SpriteEffects.None, 0f);
									Main.spriteBatch.Draw(this.waterfallTexture[11].Value, position, new Rectangle?(value2), color2, rotation, origin, 1f, SpriteEffects.None, 0f);
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
								if (tile2.liquid > 0)
								{
									int num28 = (int)(16f * ((float)tile2.liquid / 255f)) & 254;
									if (num28 >= 15)
									{
										break;
									}
									value2.Height -= num28;
									value.Height -= num28;
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
						}
					}
				}
				IL_1421:
				i++;
				continue;
				IL_4BB:
				int num29 = 0;
				num22 = this.waterfallDist;
				Color color4 = Color.White;
				int num30 = 0;
				while (num30 < num22 && num29 < 2)
				{
					WaterfallManager.AddLight(num12, num13, num14);
					Tile tile3 = Main.tile[num13, num14];
					if (tile3 == null)
					{
						tile3 = new Tile();
						Main.tile[num13, num14] = tile3;
					}
					if (tile3.nactive() && Main.tileSolid[(int)tile3.type] && !Main.tileSolidTop[(int)tile3.type] && !TileID.Sets.Platforms[(int)tile3.type] && tile3.blockType() == 0)
					{
						break;
					}
					Tile tile4 = Main.tile[num13 - 1, num14];
					if (tile4 == null)
					{
						tile4 = new Tile();
						Main.tile[num13 - 1, num14] = tile4;
					}
					Tile tile5 = Main.tile[num13, num14 + 1];
					if (tile5 == null)
					{
						tile5 = new Tile();
						Main.tile[num13, num14 + 1] = tile5;
					}
					Tile tile6 = Main.tile[num13 + 1, num14];
					if (tile6 == null)
					{
						tile6 = new Tile();
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
					int num31 = 0;
					int num32 = num18;
					bool flag2 = false;
					int num33;
					int num34;
					if (tile5.topSlope() && !tile3.halfBrick() && tile5.type != 19)
					{
						flag2 = true;
						if (tile5.slope() == 1)
						{
							num31 = 1;
							num33 = 1;
							num17 = 1;
							num18 = num17;
						}
						else
						{
							num31 = -1;
							num33 = -1;
							num17 = -1;
							num18 = num17;
						}
						num34 = 1;
					}
					else if ((!WorldGen.SolidTile(tile5) && !tile5.bottomSlope() && !tile3.halfBrick()) || (!tile5.active() && !tile3.halfBrick()))
					{
						num29 = 0;
						num34 = 1;
						num33 = 0;
					}
					else if ((WorldGen.SolidTile(tile4) || tile4.topSlope() || tile4.liquid > 0) && !WorldGen.SolidTile(tile6) && tile6.liquid == 0)
					{
						if (num17 == -1)
						{
							num29++;
						}
						num33 = 1;
						num34 = 0;
						num17 = 1;
					}
					else if ((WorldGen.SolidTile(tile6) || tile6.topSlope() || tile6.liquid > 0) && !WorldGen.SolidTile(tile4) && tile4.liquid == 0)
					{
						if (num17 == 1)
						{
							num29++;
						}
						num33 = -1;
						num34 = 0;
						num17 = -1;
					}
					else if (((!WorldGen.SolidTile(tile6) && !tile3.topSlope()) || tile6.liquid == 0) && !WorldGen.SolidTile(tile4) && !tile3.topSlope() && tile4.liquid == 0)
					{
						num34 = 0;
						num33 = num17;
					}
					else
					{
						num29++;
						num34 = 0;
						num33 = 0;
					}
					if (num29 >= 2)
					{
						num17 *= -1;
						num33 *= -1;
					}
					int num35 = -1;
					if (num12 != 1 && num12 != 14 && num12 != 25)
					{
						if (tile5.active())
						{
							num35 = (int)tile5.type;
						}
						if (tile3.active())
						{
							num35 = (int)tile3.type;
						}
					}
					if (num35 != -1)
					{
						if (num35 == 160)
						{
							num12 = 2;
						}
						else if (num35 >= 262 && num35 <= 268)
						{
							num12 = 15 + num35 - 262;
						}
					}
					Color color5 = Lighting.GetColor(num13, num14);
					if (num30 > 50)
					{
						WaterfallManager.TrySparkling(num13, num14, num17, color5);
					}
					float alpha = WaterfallManager.GetAlpha(Alpha, num22, num12, num14, num30, tile3);
					color5 = WaterfallManager.StylizeColor(alpha, num22, num12, num14, num30, tile3, color5);
					if (num12 == 1)
					{
						float num36 = Math.Abs((float)(num13 * 16 + 8) - (Main.screenPosition.X + (float)(Main.screenWidth / 2)));
						float num37 = Math.Abs((float)(num14 * 16 + 8) - (Main.screenPosition.Y + (float)(Main.screenHeight / 2)));
						if (num36 < (float)(Main.screenWidth * 2) && num37 < (float)(Main.screenHeight * 2))
						{
							float num38 = (float)Math.Sqrt((double)(num36 * num36 + num37 * num37));
							float num39 = 1f - num38 / ((float)Main.screenWidth * 0.75f);
							if (num39 > 0f)
							{
								num6 += num39;
							}
						}
						if (num36 < num7)
						{
							num7 = num36;
							num9 = num13 * 16 + 8;
						}
						if (num37 < num8)
						{
							num8 = num36;
							num10 = num14 * 16 + 8;
						}
					}
					else if (num12 != 1 && num12 != 14 && num12 != 25 && num12 != 11 && num12 != 12 && num12 != 22)
					{
						float num40 = Math.Abs((float)(num13 * 16 + 8) - (Main.screenPosition.X + (float)(Main.screenWidth / 2)));
						float num41 = Math.Abs((float)(num14 * 16 + 8) - (Main.screenPosition.Y + (float)(Main.screenHeight / 2)));
						if (num40 < (float)(Main.screenWidth * 2) && num41 < (float)(Main.screenHeight * 2))
						{
							float num42 = (float)Math.Sqrt((double)(num40 * num40 + num41 * num41));
							float num43 = 1f - num42 / ((float)Main.screenWidth * 0.75f);
							if (num43 > 0f)
							{
								num += num43;
							}
						}
						if (num40 < num2)
						{
							num2 = num40;
							num4 = num13 * 16 + 8;
						}
						if (num41 < num3)
						{
							num3 = num40;
							num5 = num14 * 16 + 8;
						}
					}
					int num44 = (int)(tile3.liquid / 16);
					int num21;
					if (flag2 && num17 != num32)
					{
						int num45 = 2;
						if (num32 == 1)
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16 + 16 - num45)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44 - num45), color5, SpriteEffects.FlipHorizontally);
						}
						else
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + 16 - num45)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44 - num45), color5, SpriteEffects.None);
						}
					}
					if (num15 == 0 && num31 != 0 && num16 == 1 && num17 != num18)
					{
						num31 = 0;
						num17 = num18;
						color5 = Color.White;
						if (num17 == 1)
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16 + 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44), color5, SpriteEffects.FlipHorizontally);
						}
						else
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16 + 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44), color5, SpriteEffects.FlipHorizontally);
						}
					}
					if (num19 != 0 && num33 == 0 && num34 == 1)
					{
						if (num17 == 1)
						{
							if (num20 != num12)
							{
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11 + 8)) - Main.screenPosition, new Rectangle(num21, 0, 16, 16 - num44 - 8), color4, SpriteEffects.FlipHorizontally);
							}
							else
							{
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11 + 8)) - Main.screenPosition, new Rectangle(num21, 0, 16, 16 - num44 - 8), color5, SpriteEffects.FlipHorizontally);
							}
						}
						else
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11 + 8)) - Main.screenPosition, new Rectangle(num21, 0, 16, 16 - num44 - 8), color5, SpriteEffects.None);
						}
					}
					if (num11 == 8 && num16 == 1 && num19 == 0)
					{
						if (num18 == -1)
						{
							if (num20 != num12)
							{
								this.DrawWaterfall(num20, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 8), color4, SpriteEffects.None);
							}
							else
							{
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 8), color5, SpriteEffects.None);
							}
						}
						else if (num20 != num12)
						{
							this.DrawWaterfall(num20, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 8), color4, SpriteEffects.FlipHorizontally);
						}
						else
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 8), color5, SpriteEffects.FlipHorizontally);
						}
					}
					if (num31 != 0 && num15 == 0)
					{
						if (num32 == 1)
						{
							if (num20 != num12)
							{
								this.DrawWaterfall(num20, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44), color4, SpriteEffects.FlipHorizontally);
							}
							else
							{
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44), color5, SpriteEffects.FlipHorizontally);
							}
						}
						else if (num20 != num12)
						{
							this.DrawWaterfall(num20, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44), color4, SpriteEffects.None);
						}
						else
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44), color5, SpriteEffects.None);
						}
					}
					if (num34 == 1 && num31 == 0 && num19 == 0)
					{
						if (num17 == -1)
						{
							if (num16 == 0)
							{
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11)) - Main.screenPosition, new Rectangle(num21, 0, 16, 16 - num44), color5, SpriteEffects.None);
							}
							else if (num20 != num12)
							{
								this.DrawWaterfall(num20, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44), color4, SpriteEffects.None);
							}
							else
							{
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44), color5, SpriteEffects.None);
							}
						}
						else if (num16 == 0)
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11)) - Main.screenPosition, new Rectangle(num21, 0, 16, 16 - num44), color5, SpriteEffects.FlipHorizontally);
						}
						else if (num20 != num12)
						{
							this.DrawWaterfall(num20, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44), color4, SpriteEffects.FlipHorizontally);
						}
						else
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 - 16), (float)(num14 * 16)) - Main.screenPosition, new Rectangle(num21, 24, 32, 16 - num44), color5, SpriteEffects.FlipHorizontally);
						}
					}
					else if (num33 == 1)
					{
						if (Main.tile[num13, num14].liquid <= 0 || Main.tile[num13, num14].halfBrick())
						{
							if (num31 == 1)
							{
								for (int k = 0; k < 8; k++)
								{
									int num46 = k * 2;
									int num47 = 14 - k * 2;
									int num48 = num46;
									num11 = 8;
									if (num15 == 0 && k < 2)
									{
										num48 = 4;
									}
									this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 + num46), (float)(num14 * 16 + num11 + num48)) - Main.screenPosition, new Rectangle(16 + num21 + num47, 0, 2, 16 - num11), color5, SpriteEffects.FlipHorizontally);
								}
							}
							else
							{
								int height = 16;
								if (TileID.Sets.BlocksWaterDrawingBehindSelf[(int)Main.tile[num13, num14].type])
								{
									height = 8;
								}
								else if (TileID.Sets.BlocksWaterDrawingBehindSelf[(int)Main.tile[num13, num14 + 1].type])
								{
									height = 8;
								}
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11)) - Main.screenPosition, new Rectangle(16 + num21, 0, 16, height), color5, SpriteEffects.FlipHorizontally);
							}
						}
					}
					else if (num33 == -1)
					{
						if (Main.tile[num13, num14].liquid <= 0 || Main.tile[num13, num14].halfBrick())
						{
							if (num31 == -1)
							{
								for (int l = 0; l < 8; l++)
								{
									int num49 = l * 2;
									int num50 = l * 2;
									int num51 = 14 - l * 2;
									num11 = 8;
									if (num15 == 0 && l > 5)
									{
										num51 = 4;
									}
									this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16 + num49), (float)(num14 * 16 + num11 + num51)) - Main.screenPosition, new Rectangle(16 + num21 + num50, 0, 2, 16 - num11), color5, SpriteEffects.FlipHorizontally);
								}
							}
							else
							{
								int height2 = 16;
								if (TileID.Sets.BlocksWaterDrawingBehindSelf[(int)Main.tile[num13, num14].type])
								{
									height2 = 8;
								}
								else if (TileID.Sets.BlocksWaterDrawingBehindSelf[(int)Main.tile[num13, num14 + 1].type])
								{
									height2 = 8;
								}
								this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11)) - Main.screenPosition, new Rectangle(16 + num21, 0, 16, height2), color5, SpriteEffects.None);
							}
						}
					}
					else if (num33 == 0 && num34 == 0)
					{
						if (Main.tile[num13, num14].liquid <= 0 || Main.tile[num13, num14].halfBrick())
						{
							this.DrawWaterfall(num12, num13, num14, alpha, new Vector2((float)(num13 * 16), (float)(num14 * 16 + num11)) - Main.screenPosition, new Rectangle(16 + num21, 0, 16, 16), color5, SpriteEffects.None);
						}
						num30 = 1000;
					}
					if (tile3.liquid > 0 && !tile3.halfBrick())
					{
						num30 = 1000;
					}
					num16 = num34;
					num18 = num17;
					num15 = num33;
					num13 += num33;
					num14 += num34;
					num19 = num31;
					color4 = color5;
					if (num20 != num12)
					{
						num20 = num12;
					}
					if ((tile4.active() && (tile4.type == 189 || tile4.type == 196)) || (tile6.active() && (tile6.type == 189 || tile6.type == 196)) || (tile5.active() && (tile5.type == 189 || tile5.type == 196)))
					{
						num22 = (int)(40f * ((float)Main.maxTilesX / 4200f) * Main.gfxQuality);
					}
					num30++;
				}
				goto IL_1421;
			}
			Main.ambientWaterfallX = (float)num4;
			Main.ambientWaterfallY = (float)num5;
			Main.ambientWaterfallStrength = num;
			Main.ambientLavafallX = (float)num9;
			Main.ambientLavafallY = (float)num10;
			Main.ambientLavafallStrength = num6;
			Main.tileSolid[546] = true;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x003F2A68 File Offset: 0x003F0C68
		private void DrawWaterfall(int waterfallType, int x, int y, float opacity, Vector2 position, Rectangle sourceRect, Color color, SpriteEffects effects)
		{
			Texture2D value = this.waterfallTexture[waterfallType].Value;
			if (waterfallType == 25)
			{
				VertexColors colors;
				Lighting.GetCornerColors(x, y, out colors, 1f);
				LiquidRenderer.SetShimmerVertexColors(ref colors, opacity, x, y);
				Main.tileBatch.Draw(value, position + new Vector2(0f, 0f), new Rectangle?(sourceRect), colors, default(Vector2), 1f, effects);
				sourceRect.Y += 42;
				LiquidRenderer.SetShimmerVertexColors_Sparkle(ref colors, opacity, x, y, true);
				Main.tileBatch.Draw(value, position + new Vector2(0f, 0f), new Rectangle?(sourceRect), colors, default(Vector2), 1f, effects);
				return;
			}
			Main.spriteBatch.Draw(value, position, new Rectangle?(sourceRect), color, 0f, default(Vector2), 1f, effects, 0f);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x003F2B64 File Offset: 0x003F0D64
		private static Color StylizeColor(float alpha, int maxSteps, int waterfallType, int y, int s, Tile tileCache, Color aColor)
		{
			float num = (float)aColor.R * alpha;
			float num2 = (float)aColor.G * alpha;
			float num3 = (float)aColor.B * alpha;
			float num4 = (float)aColor.A * alpha;
			if (waterfallType == 1)
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
			else if (waterfallType == 2)
			{
				num = (float)Main.DiscoR * alpha;
				num2 = (float)Main.DiscoG * alpha;
				num3 = (float)Main.DiscoB * alpha;
			}
			else if (waterfallType >= 15 && waterfallType <= 21)
			{
				num = 255f * alpha;
				num2 = 255f * alpha;
				num3 = 255f * alpha;
			}
			aColor = new Color((int)num, (int)num2, (int)num3, (int)num4);
			return aColor;
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x003F2C30 File Offset: 0x003F0E30
		private static float GetAlpha(float Alpha, int maxSteps, int waterfallType, int y, int s, Tile tileCache)
		{
			float num;
			if (waterfallType == 1)
			{
				num = 1f;
			}
			else if (waterfallType == 14)
			{
				num = 0.8f;
			}
			else if (waterfallType == 25)
			{
				num = 0.75f;
			}
			else if (tileCache.wall == 0 && (double)y < Main.worldSurface)
			{
				num = Alpha;
			}
			else
			{
				num = 0.6f * Alpha;
			}
			if (s > maxSteps - 10)
			{
				num *= (float)(maxSteps - s) / 10f;
			}
			return num;
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x003F2C98 File Offset: 0x003F0E98
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

		// Token: 0x06000DD0 RID: 3536 RVA: 0x003F2D68 File Offset: 0x003F0F68
		private static void AddLight(int waterfallType, int x, int y)
		{
			float num2;
			float num3;
			float num4;
			if (waterfallType == 1)
			{
				float num = num2 = (0.55f + (float)(270 - (int)Main.mouseTextColor) / 900f) * 0.4f;
				num3 = num * 0.3f;
				num4 = num * 0.1f;
				Lighting.AddLight(x, y, num2, num3, num4);
				return;
			}
			if (waterfallType != 2)
			{
				switch (waterfallType)
				{
				case 15:
					num2 = 0f;
					num3 = 0f;
					num4 = 0.2f;
					Lighting.AddLight(x, y, num2, num3, num4);
					return;
				case 16:
					num2 = 0f;
					num3 = 0.2f;
					num4 = 0f;
					Lighting.AddLight(x, y, num2, num3, num4);
					return;
				case 17:
					num2 = 0f;
					num3 = 0f;
					num4 = 0.2f;
					Lighting.AddLight(x, y, num2, num3, num4);
					return;
				case 18:
					num2 = 0f;
					num3 = 0.2f;
					num4 = 0f;
					Lighting.AddLight(x, y, num2, num3, num4);
					return;
				case 19:
					num2 = 0.2f;
					num3 = 0f;
					num4 = 0f;
					Lighting.AddLight(x, y, num2, num3, num4);
					return;
				case 20:
					Lighting.AddLight(x, y, 0.2f, 0.2f, 0.2f);
					return;
				case 21:
					num2 = 0.2f;
					num3 = 0f;
					num4 = 0f;
					Lighting.AddLight(x, y, num2, num3, num4);
					return;
				case 22:
				case 23:
				case 24:
					break;
				case 25:
				{
					float num5 = 0.7f;
					float num6 = 0.7f;
					num5 += (float)(270 - (int)Main.mouseTextColor) / 900f;
					num6 += (float)(270 - (int)Main.mouseTextColor) / 125f;
					Lighting.AddLight(x, y, num5 * 0.6f, num6 * 0.25f, num5 * 0.9f);
					break;
				}
				default:
					return;
				}
				return;
			}
			num2 = (float)Main.DiscoR / 255f;
			num3 = (float)Main.DiscoG / 255f;
			num4 = (float)Main.DiscoB / 255f;
			num2 *= 0.2f;
			num3 *= 0.2f;
			num4 *= 0.2f;
			Lighting.AddLight(x, y, num2, num3, num4);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x003F2F54 File Offset: 0x003F1154
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
		}

		// Token: 0x04000DB3 RID: 3507
		private const int minWet = 160;

		// Token: 0x04000DB4 RID: 3508
		private const int maxWaterfallCountDefault = 1000;

		// Token: 0x04000DB5 RID: 3509
		private const int maxLength = 100;

		// Token: 0x04000DB6 RID: 3510
		private const int maxTypes = 26;

		// Token: 0x04000DB7 RID: 3511
		public int maxWaterfallCount = 1000;

		// Token: 0x04000DB8 RID: 3512
		private int qualityMax;

		// Token: 0x04000DB9 RID: 3513
		private int currentMax;

		// Token: 0x04000DBA RID: 3514
		private WaterfallManager.WaterfallData[] waterfalls = new WaterfallManager.WaterfallData[1000];

		// Token: 0x04000DBB RID: 3515
		private Asset<Texture2D>[] waterfallTexture = new Asset<Texture2D>[26];

		// Token: 0x04000DBC RID: 3516
		private int wFallFrCounter;

		// Token: 0x04000DBD RID: 3517
		private int regularFrame;

		// Token: 0x04000DBE RID: 3518
		private int wFallFrCounter2;

		// Token: 0x04000DBF RID: 3519
		private int slowFrame;

		// Token: 0x04000DC0 RID: 3520
		private int rainFrameCounter;

		// Token: 0x04000DC1 RID: 3521
		private int rainFrameForeground;

		// Token: 0x04000DC2 RID: 3522
		private int rainFrameBackground;

		// Token: 0x04000DC3 RID: 3523
		private int snowFrameCounter;

		// Token: 0x04000DC4 RID: 3524
		private int snowFrameForeground;

		// Token: 0x04000DC5 RID: 3525
		private int findWaterfallCount;

		// Token: 0x04000DC6 RID: 3526
		private int waterfallDist = 100;

		// Token: 0x020004E3 RID: 1251
		public struct WaterfallData
		{
			// Token: 0x0400571A RID: 22298
			public int x;

			// Token: 0x0400571B RID: 22299
			public int y;

			// Token: 0x0400571C RID: 22300
			public int type;

			// Token: 0x0400571D RID: 22301
			public int stopAtStep;
		}
	}
}
