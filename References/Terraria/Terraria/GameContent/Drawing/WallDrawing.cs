using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics;
using Terraria.ID;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x020002B3 RID: 691
	public class WallDrawing
	{
		// Token: 0x0600220F RID: 8719 RVA: 0x00540998 File Offset: 0x0053EB98
		public void LerpVertexColorsWithColor(ref VertexColors colors, Color lerpColor, float percent)
		{
			colors.TopLeftColor = Color.Lerp(colors.TopLeftColor, lerpColor, percent);
			colors.TopRightColor = Color.Lerp(colors.TopRightColor, lerpColor, percent);
			colors.BottomLeftColor = Color.Lerp(colors.BottomLeftColor, lerpColor, percent);
			colors.BottomRightColor = Color.Lerp(colors.BottomRightColor, lerpColor, percent);
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x005409F1 File Offset: 0x0053EBF1
		public WallDrawing(TilePaintSystemV2 paintSystem)
		{
			this._paintSystem = paintSystem;
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x00540A00 File Offset: 0x0053EC00
		public void Update()
		{
			if (Main.dedServ)
			{
				return;
			}
			this._shouldShowInvisibleWalls = Main.ShouldShowInvisibleWalls();
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x00540A18 File Offset: 0x0053EC18
		public void DrawWalls()
		{
			float gfxQuality = Main.gfxQuality;
			int offScreenRange = Main.offScreenRange;
			bool drawToScreen = Main.drawToScreen;
			Vector2 screenPosition = Main.screenPosition;
			int screenWidth = Main.screenWidth;
			int screenHeight = Main.screenHeight;
			int maxTilesX = Main.maxTilesX;
			int maxTilesY = Main.maxTilesY;
			int[] wallBlend = Main.wallBlend;
			SpriteBatch spriteBatch = Main.spriteBatch;
			TileBatch tileBatch = Main.tileBatch;
			this._tileArray = Main.tile;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			int num = (int)(120f * (1f - gfxQuality) + 40f * gfxQuality);
			int num2 = (int)((float)num * 0.4f);
			int num3 = (int)((float)num * 0.35f);
			int num4 = (int)((float)num * 0.3f);
			Vector2 zero = new Vector2((float)offScreenRange, (float)offScreenRange);
			if (drawToScreen)
			{
				zero = Vector2.Zero;
			}
			int num5 = (int)((screenPosition.X - zero.X) / 16f - 1f);
			int num6 = (int)((screenPosition.X + (float)screenWidth + zero.X) / 16f) + 2;
			int num7 = (int)((screenPosition.Y - zero.Y) / 16f - 1f);
			int num8 = (int)((screenPosition.Y + (float)screenHeight + zero.Y) / 16f) + 5;
			int num9 = offScreenRange / 16;
			int num10 = offScreenRange / 16;
			if (num5 - num9 < 4)
			{
				num5 = num9 + 4;
			}
			if (num6 + num9 > maxTilesX - 4)
			{
				num6 = maxTilesX - num9 - 4;
			}
			if (num7 - num10 < 4)
			{
				num7 = num10 + 4;
			}
			if (num8 + num10 > maxTilesY - 4)
			{
				num8 = maxTilesY - num10 - 4;
			}
			VertexColors colors = default(VertexColors);
			Rectangle value = new Rectangle(0, 0, 32, 32);
			int underworldLayer = Main.UnderworldLayer;
			Point screenOverdrawOffset = Main.GetScreenOverdrawOffset();
			for (int i = num7 - num10 + screenOverdrawOffset.Y; i < num8 + num10 - screenOverdrawOffset.Y; i++)
			{
				for (int j = num5 - num9 + screenOverdrawOffset.X; j < num6 + num9 - screenOverdrawOffset.X; j++)
				{
					Tile tile = this._tileArray[j, i];
					if (tile == null)
					{
						tile = new Tile();
						this._tileArray[j, i] = tile;
					}
					ushort wall = tile.wall;
					if (wall > 0 && !this.FullTile(j, i) && (wall != 318 || this._shouldShowInvisibleWalls) && (!tile.invisibleWall() || this._shouldShowInvisibleWalls))
					{
						Color color = Lighting.GetColor(j, i);
						if (tile.fullbrightWall())
						{
							color = Color.White;
						}
						if (wall == 318)
						{
							color = Color.White;
						}
						if (color.R != 0 || color.G != 0 || color.B != 0 || i >= underworldLayer)
						{
							Main.instance.LoadWall((int)wall);
							value.X = tile.wallFrameX();
							value.Y = tile.wallFrameY() + (int)(Main.wallFrame[(int)wall] * 180);
							ushort wall2 = tile.wall;
							if (wall2 - 242 <= 1)
							{
								int num11 = 20;
								int num12 = ((int)Main.wallFrameCounter[(int)wall] + j * 11 + i * 27) % (num11 * 8);
								value.Y = tile.wallFrameY() + 180 * (num12 / num11);
							}
							if (Lighting.NotRetro && !Main.wallLight[(int)wall] && tile.wall != 241 && (tile.wall < 88 || tile.wall > 93) && !WorldGen.SolidTile(tile))
							{
								Texture2D tileDrawTexture = this.GetTileDrawTexture(tile, j, i);
								if (tile.wall == 346)
								{
									Color color2 = new Color((int)((byte)Main.DiscoR), (int)((byte)Main.DiscoG), (int)((byte)Main.DiscoB));
									colors.BottomLeftColor = color2;
									colors.BottomRightColor = color2;
									colors.TopLeftColor = color2;
									colors.TopRightColor = color2;
								}
								else if (tile.wall == 44)
								{
									Color color3 = new Color((int)((byte)Main.DiscoR), (int)((byte)Main.DiscoG), (int)((byte)Main.DiscoB));
									colors.BottomLeftColor = color3;
									colors.BottomRightColor = color3;
									colors.TopLeftColor = color3;
									colors.TopRightColor = color3;
								}
								else
								{
									Lighting.GetCornerColors(j, i, out colors, 1f);
									wall2 = tile.wall;
									if (wall2 - 341 <= 4)
									{
										this.LerpVertexColorsWithColor(ref colors, Color.White, 0.5f);
									}
									if (tile.fullbrightWall())
									{
										colors = WallDrawing._glowPaintColors;
									}
								}
								tileBatch.Draw(tileDrawTexture, new Vector2((float)(j * 16 - (int)screenPosition.X - 8), (float)(i * 16 - (int)screenPosition.Y - 8)) + zero, new Rectangle?(value), colors, Vector2.Zero, 1f, SpriteEffects.None);
							}
							else
							{
								Color color4 = color;
								if (wall == 44 || wall == 346)
								{
									color4 = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
								}
								if (wall - 341 <= 4)
								{
									color4 = Color.Lerp(color4, Color.White, 0.5f);
								}
								Texture2D tileDrawTexture2 = this.GetTileDrawTexture(tile, j, i);
								spriteBatch.Draw(tileDrawTexture2, new Vector2((float)(j * 16 - (int)screenPosition.X - 8), (float)(i * 16 - (int)screenPosition.Y - 8)) + zero, new Rectangle?(value), color4, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
							}
							if ((int)color.R > num2 || (int)color.G > num3 || (int)color.B > num4)
							{
								bool flag = this._tileArray[j - 1, i].wall > 0 && wallBlend[(int)this._tileArray[j - 1, i].wall] != wallBlend[(int)tile.wall];
								bool flag2 = this._tileArray[j + 1, i].wall > 0 && wallBlend[(int)this._tileArray[j + 1, i].wall] != wallBlend[(int)tile.wall];
								bool flag3 = this._tileArray[j, i - 1].wall > 0 && wallBlend[(int)this._tileArray[j, i - 1].wall] != wallBlend[(int)tile.wall];
								bool flag4 = this._tileArray[j, i + 1].wall > 0 && wallBlend[(int)this._tileArray[j, i + 1].wall] != wallBlend[(int)tile.wall];
								if (flag)
								{
									spriteBatch.Draw(TextureAssets.WallOutline.Value, new Vector2((float)(j * 16 - (int)screenPosition.X), (float)(i * 16 - (int)screenPosition.Y)) + zero, new Rectangle?(new Rectangle(0, 0, 2, 16)), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
								}
								if (flag2)
								{
									spriteBatch.Draw(TextureAssets.WallOutline.Value, new Vector2((float)(j * 16 - (int)screenPosition.X + 14), (float)(i * 16 - (int)screenPosition.Y)) + zero, new Rectangle?(new Rectangle(14, 0, 2, 16)), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
								}
								if (flag3)
								{
									spriteBatch.Draw(TextureAssets.WallOutline.Value, new Vector2((float)(j * 16 - (int)screenPosition.X), (float)(i * 16 - (int)screenPosition.Y)) + zero, new Rectangle?(new Rectangle(0, 0, 16, 2)), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
								}
								if (flag4)
								{
									spriteBatch.Draw(TextureAssets.WallOutline.Value, new Vector2((float)(j * 16 - (int)screenPosition.X), (float)(i * 16 - (int)screenPosition.Y + 14)) + zero, new Rectangle?(new Rectangle(0, 14, 16, 2)), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
								}
							}
						}
					}
				}
			}
			Main.instance.DrawTileCracks(2, Main.LocalPlayer.hitReplace);
			Main.instance.DrawTileCracks(2, Main.LocalPlayer.hitTile);
			TimeLogger.DrawTime(2, stopwatch.Elapsed.TotalMilliseconds);
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x00541298 File Offset: 0x0053F498
		private Texture2D GetTileDrawTexture(Tile tile, int tileX, int tileY)
		{
			Texture2D result = TextureAssets.Wall[(int)tile.wall].Value;
			int wall = (int)tile.wall;
			Texture2D texture2D = this._paintSystem.TryGetWallAndRequestIfNotReady(wall, (int)tile.wallColor());
			if (texture2D != null)
			{
				result = texture2D;
			}
			return result;
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x005412D8 File Offset: 0x0053F4D8
		protected bool FullTile(int x, int y)
		{
			if (this._tileArray[x - 1, y] == null || this._tileArray[x - 1, y].blockType() != 0 || this._tileArray[x + 1, y] == null || this._tileArray[x + 1, y].blockType() != 0)
			{
				return false;
			}
			Tile tile = this._tileArray[x, y];
			if (tile == null)
			{
				return false;
			}
			if (tile.active())
			{
				if ((int)tile.type < TileID.Sets.DrawsWalls.Length && TileID.Sets.DrawsWalls[(int)tile.type])
				{
					return false;
				}
				if (tile.invisibleBlock() && !this._shouldShowInvisibleWalls)
				{
					return false;
				}
				if (Main.tileSolid[(int)tile.type] && !Main.tileSolidTop[(int)tile.type])
				{
					int frameX = (int)tile.frameX;
					int frameY = (int)tile.frameY;
					if (Main.tileLargeFrames[(int)tile.type] > 0)
					{
						if (frameY == 18 || frameY == 108)
						{
							if (frameX >= 18 && frameX <= 54)
							{
								return true;
							}
							if (frameX >= 108 && frameX <= 144)
							{
								return true;
							}
						}
					}
					else if (frameY == 18)
					{
						if (frameX >= 18 && frameX <= 54)
						{
							return true;
						}
						if (frameX >= 108 && frameX <= 144)
						{
							return true;
						}
					}
					else if (frameY >= 90 && frameY <= 196)
					{
						if (frameX <= 70)
						{
							return true;
						}
						if (frameX >= 144 && frameX <= 232)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x040047A8 RID: 18344
		private static VertexColors _glowPaintColors = new VertexColors(Color.White);

		// Token: 0x040047A9 RID: 18345
		private Tile[,] _tileArray;

		// Token: 0x040047AA RID: 18346
		private TilePaintSystemV2 _paintSystem;

		// Token: 0x040047AB RID: 18347
		private bool _shouldShowInvisibleWalls;
	}
}
