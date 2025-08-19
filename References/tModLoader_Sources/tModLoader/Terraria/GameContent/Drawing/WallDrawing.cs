using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x0200063D RID: 1597
	public class WallDrawing
	{
		// Token: 0x06004637 RID: 17975 RVA: 0x0062FFC4 File Offset: 0x0062E1C4
		public void LerpVertexColorsWithColor(ref VertexColors colors, Color lerpColor, float percent)
		{
			colors.TopLeftColor = Color.Lerp(colors.TopLeftColor, lerpColor, percent);
			colors.TopRightColor = Color.Lerp(colors.TopRightColor, lerpColor, percent);
			colors.BottomLeftColor = Color.Lerp(colors.BottomLeftColor, lerpColor, percent);
			colors.BottomRightColor = Color.Lerp(colors.BottomRightColor, lerpColor, percent);
		}

		// Token: 0x06004638 RID: 17976 RVA: 0x0063001D File Offset: 0x0062E21D
		public WallDrawing(TilePaintSystemV2 paintSystem)
		{
			this._paintSystem = paintSystem;
		}

		// Token: 0x06004639 RID: 17977 RVA: 0x0063002C File Offset: 0x0062E22C
		public void Update()
		{
			if (!Main.dedServ)
			{
				this._shouldShowInvisibleWalls = Main.ShouldShowInvisibleWalls();
			}
		}

		// Token: 0x0600463A RID: 17978 RVA: 0x00630040 File Offset: 0x0062E240
		public unsafe void DrawWalls()
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
			int num13 = (int)(120f * (1f - gfxQuality) + 40f * gfxQuality);
			int num2 = (int)((float)num13 * 0.4f);
			int num3 = (int)((float)num13 * 0.35f);
			int num4 = (int)((float)num13 * 0.3f);
			Vector2 vector;
			vector..ctor((float)offScreenRange, (float)offScreenRange);
			if (drawToScreen)
			{
				vector = Vector2.Zero;
			}
			int num5 = (int)((screenPosition.X - vector.X) / 16f - 1f);
			int num6 = (int)((screenPosition.X + (float)screenWidth + vector.X) / 16f) + 2;
			int num7 = (int)((screenPosition.Y - vector.Y) / 16f - 1f);
			int num8 = (int)((screenPosition.Y + (float)screenHeight + vector.Y) / 16f) + 5;
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
			VertexColors vertices = default(VertexColors);
			Rectangle value;
			value..ctor(0, 0, 32, 32);
			int underworldLayer = Main.UnderworldLayer;
			Point screenOverdrawOffset = Main.GetScreenOverdrawOffset();
			for (int i = num5 - num9 + screenOverdrawOffset.X; i < num6 + num9 - screenOverdrawOffset.X; i++)
			{
				for (int j = num7 - num10 + screenOverdrawOffset.Y; j < num8 + num10 - screenOverdrawOffset.Y; j++)
				{
					Tile tile = this._tileArray[i, j];
					if (tile == null)
					{
						tile = default(Tile);
						this._tileArray[i, j] = tile;
					}
					ushort wall = *tile.wall;
					if (wall > 0 && !this.FullTile(i, j) && (wall != 318 || this._shouldShowInvisibleWalls) && (!tile.invisibleWall() || this._shouldShowInvisibleWalls))
					{
						if (WallLoader.PreDraw(i, j, (int)wall, spriteBatch))
						{
							Color color = Lighting.GetColor(i, j);
							if (tile.fullbrightWall())
							{
								color = Color.White;
							}
							if (wall == 318)
							{
								color = Color.White;
							}
							if (color.R == 0 && color.G == 0 && color.B == 0 && j < underworldLayer)
							{
								goto IL_859;
							}
							Main.instance.LoadWall((int)wall);
							value.X = tile.wallFrameX();
							value.Y = tile.wallFrameY() + (int)(Main.wallFrame[(int)wall] * 180);
							if (*tile.wall - 242 <= 1)
							{
								int num11 = 20;
								int num12 = ((int)Main.wallFrameCounter[(int)wall] + i * 11 + j * 27) % (num11 * 8);
								value.Y = tile.wallFrameY() + 180 * (num12 / num11);
							}
							if (Lighting.NotRetro && !Main.wallLight[(int)wall] && *tile.wall != 241 && (*tile.wall < 88 || *tile.wall > 93) && !WorldGen.SolidTile(tile))
							{
								Texture2D tileDrawTexture = this.GetTileDrawTexture(tile, i, j);
								if (*tile.wall == 346)
								{
									vertices.TopRightColor = (vertices.TopLeftColor = (vertices.BottomRightColor = (vertices.BottomLeftColor = new Color((int)((byte)Main.DiscoR), (int)((byte)Main.DiscoG), (int)((byte)Main.DiscoB)))));
								}
								else if (*tile.wall == 44)
								{
									vertices.TopRightColor = (vertices.TopLeftColor = (vertices.BottomRightColor = (vertices.BottomLeftColor = new Color((int)((byte)Main.DiscoR), (int)((byte)Main.DiscoG), (int)((byte)Main.DiscoB)))));
								}
								else
								{
									Lighting.GetCornerColors(i, j, out vertices, 1f);
									if (*tile.wall - 341 <= 4)
									{
										this.LerpVertexColorsWithColor(ref vertices, Color.White, 0.5f);
									}
									if (tile.fullbrightWall())
									{
										vertices = WallDrawing._glowPaintColors;
									}
								}
								tileBatch.Draw(tileDrawTexture, new Vector2((float)(i * 16 - (int)screenPosition.X - 8), (float)(j * 16 - (int)screenPosition.Y - 8)) + vector, new Rectangle?(value), vertices, Vector2.Zero, 1f, 0);
							}
							else
							{
								Color color2 = color;
								if (wall == 44 || wall == 346)
								{
									color2..ctor(Main.DiscoR, Main.DiscoG, Main.DiscoB);
								}
								if (wall - 341 <= 4)
								{
									color2 = Color.Lerp(color2, Color.White, 0.5f);
								}
								Texture2D tileDrawTexture2 = this.GetTileDrawTexture(tile, i, j);
								spriteBatch.Draw(tileDrawTexture2, new Vector2((float)(i * 16 - (int)screenPosition.X - 8), (float)(j * 16 - (int)screenPosition.Y - 8)) + vector, new Rectangle?(value), color2, 0f, Vector2.Zero, 1f, 0, 0f);
							}
							if ((int)color.R > num2 || (int)color.G > num3 || (int)color.B > num4)
							{
								bool flag4 = *this._tileArray[i - 1, j].wall > 0 && wallBlend[(int)(*this._tileArray[i - 1, j].wall)] != wallBlend[(int)(*tile.wall)];
								bool flag = *this._tileArray[i + 1, j].wall > 0 && wallBlend[(int)(*this._tileArray[i + 1, j].wall)] != wallBlend[(int)(*tile.wall)];
								bool flag2 = *this._tileArray[i, j - 1].wall > 0 && wallBlend[(int)(*this._tileArray[i, j - 1].wall)] != wallBlend[(int)(*tile.wall)];
								bool flag3 = *this._tileArray[i, j + 1].wall > 0 && wallBlend[(int)(*this._tileArray[i, j + 1].wall)] != wallBlend[(int)(*tile.wall)];
								if (flag4)
								{
									spriteBatch.Draw(TextureAssets.WallOutline.Value, new Vector2((float)(i * 16 - (int)screenPosition.X), (float)(j * 16 - (int)screenPosition.Y)) + vector, new Rectangle?(new Rectangle(0, 0, 2, 16)), color, 0f, Vector2.Zero, 1f, 0, 0f);
								}
								if (flag)
								{
									spriteBatch.Draw(TextureAssets.WallOutline.Value, new Vector2((float)(i * 16 - (int)screenPosition.X + 14), (float)(j * 16 - (int)screenPosition.Y)) + vector, new Rectangle?(new Rectangle(14, 0, 2, 16)), color, 0f, Vector2.Zero, 1f, 0, 0f);
								}
								if (flag2)
								{
									spriteBatch.Draw(TextureAssets.WallOutline.Value, new Vector2((float)(i * 16 - (int)screenPosition.X), (float)(j * 16 - (int)screenPosition.Y)) + vector, new Rectangle?(new Rectangle(0, 0, 16, 2)), color, 0f, Vector2.Zero, 1f, 0, 0f);
								}
								if (flag3)
								{
									spriteBatch.Draw(TextureAssets.WallOutline.Value, new Vector2((float)(i * 16 - (int)screenPosition.X), (float)(j * 16 - (int)screenPosition.Y + 14)) + vector, new Rectangle?(new Rectangle(0, 14, 16, 2)), color, 0f, Vector2.Zero, 1f, 0, 0f);
								}
							}
						}
						WallLoader.PostDraw(i, j, (int)wall, spriteBatch);
					}
					IL_859:;
				}
			}
			Main.instance.DrawTileCracks(2, Main.LocalPlayer.hitReplace);
			Main.instance.DrawTileCracks(2, Main.LocalPlayer.hitTile);
			TimeLogger.DrawTime(2, stopwatch.Elapsed.TotalMilliseconds);
		}

		// Token: 0x0600463B RID: 17979 RVA: 0x0063091C File Offset: 0x0062EB1C
		private unsafe Texture2D GetTileDrawTexture(Tile tile, int tileX, int tileY)
		{
			Texture2D result = TextureAssets.Wall[(int)(*tile.wall)].Value;
			int wall = (int)(*tile.wall);
			Texture2D texture2D = this._paintSystem.TryGetWallAndRequestIfNotReady(wall, (int)tile.wallColor());
			if (texture2D != null)
			{
				result = texture2D;
			}
			return result;
		}

		// Token: 0x0600463C RID: 17980 RVA: 0x00630960 File Offset: 0x0062EB60
		protected unsafe bool FullTile(int x, int y)
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
				if ((int)(*tile.type) < TileID.Sets.DrawsWalls.Length && TileID.Sets.DrawsWalls[(int)(*tile.type)])
				{
					return false;
				}
				if (tile.invisibleBlock() && !this._shouldShowInvisibleWalls)
				{
					return false;
				}
				if (Main.tileSolid[(int)(*tile.type)] && !Main.tileSolidTop[(int)(*tile.type)])
				{
					int frameX = (int)(*tile.frameX);
					int frameY = (int)(*tile.frameY);
					if (Main.tileLargeFrames[(int)(*tile.type)] > 0)
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

		// Token: 0x04005B61 RID: 23393
		private static VertexColors _glowPaintColors = new VertexColors(Color.White);

		// Token: 0x04005B62 RID: 23394
		private Tilemap _tileArray;

		// Token: 0x04005B63 RID: 23395
		private TilePaintSystemV2 _paintSystem;

		// Token: 0x04005B64 RID: 23396
		private bool _shouldShowInvisibleWalls;
	}
}
