using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace Terraria
{
	// Token: 0x02000031 RID: 49
	public class HitTile
	{
		// Token: 0x06000234 RID: 564 RVA: 0x00026A04 File Offset: 0x00024C04
		public static void ClearAllTilesAtThisLocation(int x, int y)
		{
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					Main.player[i].hitTile.ClearThisTile(x, y);
				}
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00026A44 File Offset: 0x00024C44
		public void ClearThisTile(int x, int y)
		{
			for (int i = 0; i <= 500; i++)
			{
				int num = this.order[i];
				HitTile.HitTileObject hitTileObject = this.data[num];
				if (hitTileObject.X == x && hitTileObject.Y == y)
				{
					this.Clear(i);
					this.Prune();
				}
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00026A94 File Offset: 0x00024C94
		public HitTile()
		{
			HitTile.rand = new UnifiedRandom();
			this.data = new HitTile.HitTileObject[501];
			this.order = new int[501];
			for (int i = 0; i <= 500; i++)
			{
				this.data[i] = new HitTile.HitTileObject();
				this.order[i] = i;
			}
			this.bufferLocation = 0;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00026B00 File Offset: 0x00024D00
		public int TryFinding(int x, int y, int hitType)
		{
			for (int i = 0; i <= 500; i++)
			{
				int num = this.order[i];
				HitTile.HitTileObject hitTileObject = this.data[num];
				if (hitTileObject.type == hitType)
				{
					if (hitTileObject.X == x && hitTileObject.Y == y)
					{
						return num;
					}
				}
				else if (i != 0 && hitTileObject.type == 0)
				{
					break;
				}
			}
			return -1;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00026B58 File Offset: 0x00024D58
		public void TryClearingAndPruning(int x, int y, int hitType)
		{
			int num = this.TryFinding(x, y, hitType);
			if (num != -1)
			{
				this.Clear(num);
				this.Prune();
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00026B80 File Offset: 0x00024D80
		public int HitObject(int x, int y, int hitType)
		{
			HitTile.HitTileObject hitTileObject;
			for (int i = 0; i <= 500; i++)
			{
				int num = this.order[i];
				hitTileObject = this.data[num];
				if (hitTileObject.type == hitType)
				{
					if (hitTileObject.X == x && hitTileObject.Y == y)
					{
						return num;
					}
				}
				else if (i != 0 && hitTileObject.type == 0)
				{
					break;
				}
			}
			hitTileObject = this.data[this.bufferLocation];
			hitTileObject.X = x;
			hitTileObject.Y = y;
			hitTileObject.type = hitType;
			return this.bufferLocation;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00026C00 File Offset: 0x00024E00
		public void UpdatePosition(int tileId, int x, int y)
		{
			if (tileId >= 0 && tileId <= 500)
			{
				HitTile.HitTileObject hitTileObject = this.data[tileId];
				hitTileObject.X = x;
				hitTileObject.Y = y;
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00026C24 File Offset: 0x00024E24
		public int AddDamage(int tileId, int damageAmount, bool updateAmount = true)
		{
			if (tileId < 0 || tileId > 500)
			{
				return 0;
			}
			if (tileId == this.bufferLocation && damageAmount == 0)
			{
				return 0;
			}
			HitTile.HitTileObject hitTileObject = this.data[tileId];
			if (!updateAmount)
			{
				return hitTileObject.damage + damageAmount;
			}
			hitTileObject.damage += damageAmount;
			hitTileObject.timeToLive = 60;
			hitTileObject.animationTimeElapsed = 0;
			hitTileObject.animationDirection = (Main.rand.NextFloat() * 6.2831855f).ToRotationVector2() * 2f;
			this.SortSlots(tileId);
			return hitTileObject.damage;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00026CB4 File Offset: 0x00024EB4
		private void SortSlots(int tileId)
		{
			if (tileId == this.bufferLocation)
			{
				this.bufferLocation = this.order[500];
				if (tileId != this.bufferLocation)
				{
					this.data[this.bufferLocation].Clear();
				}
				for (int num = 500; num > 0; num--)
				{
					this.order[num] = this.order[num - 1];
				}
				this.order[0] = this.bufferLocation;
				return;
			}
			int num2;
			for (num2 = 0; num2 <= 500; num2++)
			{
				if (this.order[num2] == tileId)
				{
					break;
				}
			}
			while (num2 > 1)
			{
				int num3 = this.order[num2 - 1];
				this.order[num2 - 1] = this.order[num2];
				this.order[num2] = num3;
				num2--;
			}
			this.order[1] = tileId;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00026D7C File Offset: 0x00024F7C
		public void Clear(int tileId)
		{
			if (tileId >= 0 && tileId <= 500)
			{
				this.data[tileId].Clear();
				int i;
				for (i = 0; i < 500; i++)
				{
					if (this.order[i] == tileId)
					{
						break;
					}
				}
				while (i < 500)
				{
					this.order[i] = this.order[i + 1];
					i++;
				}
				this.order[500] = tileId;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00026DEC File Offset: 0x00024FEC
		public unsafe void Prune()
		{
			bool flag = false;
			for (int i = 0; i <= 500; i++)
			{
				HitTile.HitTileObject hitTileObject = this.data[i];
				if (hitTileObject.type != 0)
				{
					Tile tile = Main.tile[hitTileObject.X, hitTileObject.Y];
					if (hitTileObject.timeToLive <= 1)
					{
						hitTileObject.Clear();
						flag = true;
					}
					else
					{
						hitTileObject.timeToLive--;
						if ((double)hitTileObject.timeToLive < 12.0)
						{
							hitTileObject.damage -= 10;
						}
						else if ((double)hitTileObject.timeToLive < 24.0)
						{
							hitTileObject.damage -= 7;
						}
						else if ((double)hitTileObject.timeToLive < 36.0)
						{
							hitTileObject.damage -= 5;
						}
						else if ((double)hitTileObject.timeToLive < 48.0)
						{
							hitTileObject.damage -= 2;
						}
						if (hitTileObject.damage < 0)
						{
							hitTileObject.Clear();
							flag = true;
						}
						else if (hitTileObject.type == 1)
						{
							if (!tile.active())
							{
								hitTileObject.Clear();
								flag = true;
							}
						}
						else if (*tile.wall == 0)
						{
							hitTileObject.Clear();
							flag = true;
						}
					}
				}
			}
			if (!flag)
			{
				return;
			}
			int num = 1;
			while (flag)
			{
				flag = false;
				for (int j = num; j < 500; j++)
				{
					if (this.data[this.order[j]].type == 0 && this.data[this.order[j + 1]].type != 0)
					{
						int num2 = this.order[j];
						this.order[j] = this.order[j + 1];
						this.order[j + 1] = num2;
						flag = true;
					}
				}
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00026FA8 File Offset: 0x000251A8
		public unsafe void DrawFreshAnimations(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < this.data.Length; i++)
			{
				this.data[i].animationTimeElapsed++;
			}
			if (!Main.SettingsEnabled_MinersWobble)
			{
				return;
			}
			int num = 1;
			Vector2 vector;
			vector..ctor((float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				vector = Vector2.Zero;
			}
			vector = Vector2.Zero;
			bool flag = Main.ShouldShowInvisibleWalls();
			for (int j = 0; j < this.data.Length; j++)
			{
				if (this.data[j].type == num)
				{
					int damage = this.data[j].damage;
					if (damage >= 20)
					{
						int x = this.data[j].X;
						int y = this.data[j].Y;
						if (WorldGen.InWorld(x, y, 0))
						{
							Tile tile = Main.tile[x, y];
							bool flag2 = tile != null;
							if (flag2 && num == 1)
							{
								flag2 = (flag2 && tile.active() && Main.tileSolid[(int)(*Main.tile[x, y].type)] && (!tile.invisibleBlock() || flag));
							}
							if (flag2 && num == 2)
							{
								flag2 = (flag2 && *tile.wall != 0 && (!tile.invisibleWall() || flag));
							}
							if (flag2)
							{
								bool flag3 = false;
								bool flag4 = false;
								if (TileLoader.IsClosedDoor(tile))
								{
									flag3 = false;
								}
								else if (Main.tileSolid[(int)(*tile.type)] && !Main.tileSolidTop[(int)(*tile.type)])
								{
									flag3 = true;
								}
								else if (WorldGen.IsTreeType((int)(*tile.type)))
								{
									flag4 = true;
									int num2 = (int)(*tile.frameX / 22);
									int num3 = (int)(*tile.frameY / 22);
									if (num3 < 9)
									{
										flag3 = (((num2 != 1 && num2 != 2) || num3 < 6 || num3 > 8) && (num2 != 3 || num3 > 2) && (num2 != 4 || num3 < 3 || num3 > 5) && (num2 != 5 || num3 < 6 || num3 > 8));
									}
								}
								else if (*tile.type == 72)
								{
									flag4 = true;
									if (*tile.frameX <= 34)
									{
										flag3 = true;
									}
								}
								if (flag3 && tile.slope() == 0 && !tile.halfBrick())
								{
									int num4 = 0;
									if (damage >= 80)
									{
										num4 = 3;
									}
									else if (damage >= 60)
									{
										num4 = 2;
									}
									else if (damage >= 40)
									{
										num4 = 1;
									}
									else if (damage >= 20)
									{
										num4 = 0;
									}
									Rectangle value;
									value..ctor(this.data[j].crackStyle * 18, num4 * 18, 16, 16);
									value.Inflate(-2, -2);
									if (flag4)
									{
										value.X = (4 + this.data[j].crackStyle / 2) * 18;
									}
									int animationTimeElapsed = this.data[j].animationTimeElapsed;
									if ((float)animationTimeElapsed < 10f)
									{
										float num8 = (float)animationTimeElapsed / 10f;
										Color color = Lighting.GetColor(x, y);
										float rotation = 0f;
										Vector2 zero = Vector2.Zero;
										float num5 = 0.5f;
										float num6 = num8 % num5;
										num6 *= 1f / num5;
										if ((int)(num8 / num5) % 2 == 1)
										{
											num6 = 1f - num6;
										}
										Tile tileSafely = Framing.GetTileSafely(x, y);
										Tile tile2 = tileSafely;
										Texture2D texture2D = Main.instance.TilePaintSystem.TryGetTileAndRequestIfNotReady((int)(*tileSafely.type), 0, (int)tileSafely.color());
										if (texture2D != null)
										{
											Vector2 vector2;
											vector2..ctor(8f);
											Vector2 vector3;
											vector3..ctor(1f);
											float num9 = num6 * 0.2f + 1f;
											float num7 = 1f - num6;
											num7 = 1f;
											color *= num7 * num7 * 0.8f;
											Vector2 scale = num9 * vector3;
											Vector2 position = (new Vector2((float)(x * 16 - (int)Main.screenPosition.X), (float)(y * 16 - (int)Main.screenPosition.Y)) + vector + vector2 + zero).Floor();
											spriteBatch.Draw(texture2D, position, new Rectangle?(new Rectangle((int)(*tile2.frameX), (int)(*tile2.frameY), 16, 16)), color, rotation, vector2, scale, 0, 0f);
											color.A = 180;
											spriteBatch.Draw(TextureAssets.TileCrack.Value, position, new Rectangle?(value), color, rotation, vector2, scale, 0, 0f);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x040001F8 RID: 504
		internal const int UNUSED = 0;

		// Token: 0x040001F9 RID: 505
		internal const int TILE = 1;

		// Token: 0x040001FA RID: 506
		internal const int WALL = 2;

		// Token: 0x040001FB RID: 507
		internal const int MAX_HITTILES = 500;

		// Token: 0x040001FC RID: 508
		internal const int TIMETOLIVE = 60;

		// Token: 0x040001FD RID: 509
		private static UnifiedRandom rand;

		// Token: 0x040001FE RID: 510
		private static int lastCrack = -1;

		// Token: 0x040001FF RID: 511
		public HitTile.HitTileObject[] data;

		// Token: 0x04000200 RID: 512
		private int[] order;

		// Token: 0x04000201 RID: 513
		private int bufferLocation;

		// Token: 0x02000793 RID: 1939
		public class HitTileObject
		{
			// Token: 0x06004E8E RID: 20110 RVA: 0x00675103 File Offset: 0x00673303
			public HitTileObject()
			{
				this.Clear();
			}

			// Token: 0x06004E8F RID: 20111 RVA: 0x00675114 File Offset: 0x00673314
			public void Clear()
			{
				this.X = 0;
				this.Y = 0;
				this.damage = 0;
				this.type = 0;
				this.timeToLive = 0;
				if (HitTile.rand == null)
				{
					HitTile.rand = new UnifiedRandom((int)DateTime.Now.Ticks);
				}
				this.crackStyle = HitTile.rand.Next(4);
				while (this.crackStyle == HitTile.lastCrack)
				{
					this.crackStyle = HitTile.rand.Next(4);
				}
				HitTile.lastCrack = this.crackStyle;
			}

			// Token: 0x040065AB RID: 26027
			public int X;

			// Token: 0x040065AC RID: 26028
			public int Y;

			// Token: 0x040065AD RID: 26029
			public int damage;

			// Token: 0x040065AE RID: 26030
			public int type;

			// Token: 0x040065AF RID: 26031
			public int timeToLive;

			// Token: 0x040065B0 RID: 26032
			public int crackStyle;

			// Token: 0x040065B1 RID: 26033
			public int animationTimeElapsed;

			// Token: 0x040065B2 RID: 26034
			public Vector2 animationDirection;
		}
	}
}
