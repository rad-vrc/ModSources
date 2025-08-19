using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Utilities;

namespace Terraria
{
	// Token: 0x02000023 RID: 35
	public class HitTile
	{
		// Token: 0x0600017A RID: 378 RVA: 0x00012C70 File Offset: 0x00010E70
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

		// Token: 0x0600017B RID: 379 RVA: 0x00012CB0 File Offset: 0x00010EB0
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

		// Token: 0x0600017C RID: 380 RVA: 0x00012D00 File Offset: 0x00010F00
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

		// Token: 0x0600017D RID: 381 RVA: 0x00012D6C File Offset: 0x00010F6C
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

		// Token: 0x0600017E RID: 382 RVA: 0x00012DC4 File Offset: 0x00010FC4
		public void TryClearingAndPruning(int x, int y, int hitType)
		{
			int num = this.TryFinding(x, y, hitType);
			if (num != -1)
			{
				this.Clear(num);
				this.Prune();
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00012DEC File Offset: 0x00010FEC
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

		// Token: 0x06000180 RID: 384 RVA: 0x00012E6C File Offset: 0x0001106C
		public void UpdatePosition(int tileId, int x, int y)
		{
			if (tileId < 0 || tileId > 500)
			{
				return;
			}
			HitTile.HitTileObject hitTileObject = this.data[tileId];
			hitTileObject.X = x;
			hitTileObject.Y = y;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00012E90 File Offset: 0x00011090
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

		// Token: 0x06000182 RID: 386 RVA: 0x00012F20 File Offset: 0x00011120
		private void SortSlots(int tileId)
		{
			if (tileId == this.bufferLocation)
			{
				this.bufferLocation = this.order[500];
				if (tileId != this.bufferLocation)
				{
					this.data[this.bufferLocation].Clear();
				}
				for (int i = 500; i > 0; i--)
				{
					this.order[i] = this.order[i - 1];
				}
				this.order[0] = this.bufferLocation;
				return;
			}
			for (int i = 0; i <= 500; i++)
			{
				if (this.order[i] == tileId)
				{
					IL_AE:
					while (i > 1)
					{
						int num = this.order[i - 1];
						this.order[i - 1] = this.order[i];
						this.order[i] = num;
						i--;
					}
					this.order[1] = tileId;
					return;
				}
			}
			goto IL_AE;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00012FE8 File Offset: 0x000111E8
		public void Clear(int tileId)
		{
			if (tileId < 0 || tileId > 500)
			{
				return;
			}
			this.data[tileId].Clear();
			for (int i = 0; i < 500; i++)
			{
				if (this.order[i] == tileId)
				{
					IL_4D:
					while (i < 500)
					{
						this.order[i] = this.order[i + 1];
						i++;
					}
					this.order[500] = tileId;
					return;
				}
			}
			goto IL_4D;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00013058 File Offset: 0x00011258
		public void Prune()
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
						else if (tile.wall == 0)
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

		// Token: 0x06000185 RID: 389 RVA: 0x00013214 File Offset: 0x00011414
		public void DrawFreshAnimations(SpriteBatch spriteBatch)
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
			Vector2 zero = new Vector2((float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
			zero = Vector2.Zero;
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
								flag2 = (flag2 && tile.active() && Main.tileSolid[(int)Main.tile[x, y].type] && (!tile.invisibleBlock() || flag));
							}
							if (flag2 && num == 2)
							{
								flag2 = (flag2 && tile.wall != 0 && (!tile.invisibleWall() || flag));
							}
							if (flag2)
							{
								bool flag3 = false;
								bool flag4 = false;
								if (tile.type == 10)
								{
									flag3 = false;
								}
								else if (Main.tileSolid[(int)tile.type] && !Main.tileSolidTop[(int)tile.type])
								{
									flag3 = true;
								}
								else if (WorldGen.IsTreeType((int)tile.type))
								{
									flag4 = true;
									int num2 = (int)(tile.frameX / 22);
									int num3 = (int)(tile.frameY / 22);
									if (num3 < 9)
									{
										flag3 = (((num2 != 1 && num2 != 2) || num3 < 6 || num3 > 8) && (num2 != 3 || num3 > 2) && (num2 != 4 || num3 < 3 || num3 > 5) && (num2 != 5 || num3 < 6 || num3 > 8));
									}
								}
								else if (tile.type == 72)
								{
									flag4 = true;
									if (tile.frameX <= 34)
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
									Rectangle value = new Rectangle(this.data[j].crackStyle * 18, num4 * 18, 16, 16);
									value.Inflate(-2, -2);
									if (flag4)
									{
										value.X = (4 + this.data[j].crackStyle / 2) * 18;
									}
									int animationTimeElapsed = this.data[j].animationTimeElapsed;
									if ((float)animationTimeElapsed < 10f)
									{
										float num5 = (float)animationTimeElapsed / 10f;
										Color color = Lighting.GetColor(x, y);
										float rotation = 0f;
										Vector2 zero2 = Vector2.Zero;
										float num6 = 0.5f;
										float num7 = num5 % num6;
										num7 *= 1f / num6;
										if ((int)(num5 / num6) % 2 == 1)
										{
											num7 = 1f - num7;
										}
										Tile tileSafely = Framing.GetTileSafely(x, y);
										Tile tile2 = tileSafely;
										Texture2D texture2D = Main.instance.TilePaintSystem.TryGetTileAndRequestIfNotReady((int)tileSafely.type, 0, (int)tileSafely.color());
										if (texture2D != null)
										{
											Vector2 vector = new Vector2(8f);
											Vector2 value2 = new Vector2(1f);
											float scaleFactor = num7 * 0.2f + 1f;
											float num8 = 1f - num7;
											num8 = 1f;
											color *= num8 * num8 * 0.8f;
											Vector2 scale = scaleFactor * value2;
											Vector2 position = (new Vector2((float)(x * 16 - (int)Main.screenPosition.X), (float)(y * 16 - (int)Main.screenPosition.Y)) + zero + vector + zero2).Floor();
											spriteBatch.Draw(texture2D, position, new Rectangle?(new Rectangle((int)tile2.frameX, (int)tile2.frameY, 16, 16)), color, rotation, vector, scale, SpriteEffects.None, 0f);
											color.A = 180;
											spriteBatch.Draw(TextureAssets.TileCrack.Value, position, new Rectangle?(value), color, rotation, vector, scale, SpriteEffects.None, 0f);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0400010F RID: 271
		internal const int UNUSED = 0;

		// Token: 0x04000110 RID: 272
		internal const int TILE = 1;

		// Token: 0x04000111 RID: 273
		internal const int WALL = 2;

		// Token: 0x04000112 RID: 274
		internal const int MAX_HITTILES = 500;

		// Token: 0x04000113 RID: 275
		internal const int TIMETOLIVE = 60;

		// Token: 0x04000114 RID: 276
		private static UnifiedRandom rand;

		// Token: 0x04000115 RID: 277
		private static int lastCrack = -1;

		// Token: 0x04000116 RID: 278
		public HitTile.HitTileObject[] data;

		// Token: 0x04000117 RID: 279
		private int[] order;

		// Token: 0x04000118 RID: 280
		private int bufferLocation;

		// Token: 0x0200049F RID: 1183
		public class HitTileObject
		{
			// Token: 0x06002EB2 RID: 11954 RVA: 0x005C4B3C File Offset: 0x005C2D3C
			public HitTileObject()
			{
				this.Clear();
			}

			// Token: 0x06002EB3 RID: 11955 RVA: 0x005C4B4C File Offset: 0x005C2D4C
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

			// Token: 0x040055C7 RID: 21959
			public int X;

			// Token: 0x040055C8 RID: 21960
			public int Y;

			// Token: 0x040055C9 RID: 21961
			public int damage;

			// Token: 0x040055CA RID: 21962
			public int type;

			// Token: 0x040055CB RID: 21963
			public int timeToLive;

			// Token: 0x040055CC RID: 21964
			public int crackStyle;

			// Token: 0x040055CD RID: 21965
			public int animationTimeElapsed;

			// Token: 0x040055CE RID: 21966
			public Vector2 animationDirection;
		}
	}
}
