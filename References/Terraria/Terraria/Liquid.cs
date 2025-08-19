using System;
using System.Collections.Generic;
using Terraria.GameContent.NetModules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ObjectData;
using Terraria.WorldBuilding;

namespace Terraria
{
	// Token: 0x02000026 RID: 38
	public class Liquid
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x0001B7E4 File Offset: 0x000199E4
		public static void NetSendLiquid(int x, int y)
		{
			if (WorldGen.gen)
			{
				return;
			}
			HashSet<int> netChangeSet = Liquid._netChangeSet;
			lock (netChangeSet)
			{
				Liquid._netChangeSet.Add((x & 65535) << 16 | (y & 65535));
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0001B844 File Offset: 0x00019A44
		public static void tilesIgnoreWater(bool ignoreSolids)
		{
			Main.tileSolid[138] = !ignoreSolids;
			Main.tileSolid[484] = !ignoreSolids;
			Main.tileSolid[546] = !ignoreSolids;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0001B873 File Offset: 0x00019A73
		public static void worldGenTilesIgnoreWater(bool ignoreSolids)
		{
			Main.tileSolid[10] = !ignoreSolids;
			Main.tileSolid[192] = !ignoreSolids;
			Main.tileSolid[191] = !ignoreSolids;
			Main.tileSolid[190] = !ignoreSolids;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0001B8B0 File Offset: 0x00019AB0
		public static void ReInit()
		{
			Liquid.skipCount = 0;
			Liquid.stuckCount = 0;
			Liquid.stuckAmount = 0;
			Liquid.cycles = 10;
			Liquid.curMaxLiquid = Liquid.maxLiquid;
			Liquid.numLiquid = 0;
			Liquid.stuck = false;
			Liquid.quickFall = false;
			Liquid.quickSettle = false;
			Liquid.wetCounter = 0;
			Liquid.panicCounter = 0;
			Liquid.panicMode = false;
			Liquid.panicY = 0;
			if (Main.Setting_UseReducedMaxLiquids)
			{
				Liquid.curMaxLiquid = 5000;
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0001B924 File Offset: 0x00019B24
		public static void QuickWater(int verbose = 0, int minY = -1, int maxY = -1)
		{
			if (WorldGen.gen)
			{
				WorldGen.ShimmerRemoveWater();
				if (WorldGen.noTrapsWorldGen)
				{
					Main.tileSolid[138] = false;
				}
			}
			Main.tileSolid[379] = true;
			Liquid.tilesIgnoreWater(true);
			if (minY == -1)
			{
				minY = 3;
			}
			if (maxY == -1)
			{
				maxY = Main.maxTilesY - 3;
			}
			for (int i = maxY; i >= minY; i--)
			{
				Liquid.UpdateProgressDisplay(verbose, minY, maxY, i);
				for (int j = 4; j < Main.maxTilesX - 4; j++)
				{
					if (Main.tile[j, i].liquid != 0)
					{
						Liquid.SettleWaterAt(j, i);
					}
				}
			}
			Liquid.tilesIgnoreWater(false);
			if (WorldGen.gen)
			{
				WorldGen.ShimmerRemoveWater();
				if (WorldGen.noTrapsWorldGen)
				{
					Main.tileSolid[138] = true;
				}
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0001B9E0 File Offset: 0x00019BE0
		private static void SettleWaterAt(int originX, int originY)
		{
			Tile tile = Main.tile[originX, originY];
			Liquid.tilesIgnoreWater(true);
			if (tile.liquid == 0)
			{
				return;
			}
			int num = originX;
			int num2 = originY;
			bool tileAtXYHasLava = tile.lava();
			bool flag = tile.honey();
			bool flag2 = tile.shimmer();
			int num3 = (int)tile.liquid;
			byte b = tile.liquidType();
			tile.liquid = 0;
			bool flag3 = true;
			for (;;)
			{
				Tile tile2 = Main.tile[num, num2 + 1];
				bool flag4 = false;
				while (num2 < Main.maxTilesY - 5 && tile2.liquid == 0 && (!tile2.nactive() || !Main.tileSolid[(int)tile2.type] || Main.tileSolidTop[(int)tile2.type]))
				{
					num2++;
					flag4 = true;
					flag3 = false;
					tile2 = Main.tile[num, num2 + 1];
				}
				if (WorldGen.gen && flag4 && !flag && !flag2)
				{
					if (WorldGen.remixWorldGen)
					{
						if (num2 > GenVars.lavaLine && ((double)num2 < Main.rockLayer - 80.0 || num2 > Main.maxTilesY - 350))
						{
							if (WorldGen.oceanDepths(num, num2))
							{
								b = 0;
							}
							else
							{
								b = 1;
							}
						}
						else
						{
							b = 0;
						}
					}
					else if (num2 > GenVars.waterLine)
					{
						b = 1;
					}
				}
				int num4 = -1;
				int num5 = 0;
				int num6 = -1;
				int num7 = 0;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				for (;;)
				{
					if (Main.tile[num + num5 * num4, num2].liquid == 0)
					{
						num6 = num4;
						num7 = num5;
					}
					if (num4 == -1 && num + num5 * num4 < 5)
					{
						flag6 = true;
					}
					else if (num4 == 1 && num + num5 * num4 > Main.maxTilesX - 5)
					{
						flag5 = true;
					}
					tile2 = Main.tile[num + num5 * num4, num2 + 1];
					if (tile2.liquid != 0 && tile2.liquid != 255 && tile2.liquidType() == b)
					{
						int num8 = (int)(byte.MaxValue - tile2.liquid);
						if (num8 > num3)
						{
							num8 = num3;
						}
						Tile tile3 = tile2;
						tile3.liquid += (byte)num8;
						num3 -= num8;
						if (num3 == 0)
						{
							break;
						}
					}
					if (num2 < Main.maxTilesY - 5 && tile2.liquid == 0 && (!tile2.nactive() || !Main.tileSolid[(int)tile2.type] || Main.tileSolidTop[(int)tile2.type]))
					{
						goto IL_22E;
					}
					Tile tile4 = Main.tile[num + (num5 + 1) * num4, num2];
					if ((tile4.liquid != 0 && (!flag3 || num4 != 1)) || (tile4.nactive() && Main.tileSolid[(int)tile4.type] && !Main.tileSolidTop[(int)tile4.type]))
					{
						if (num4 == 1)
						{
							flag5 = true;
						}
						else
						{
							flag6 = true;
						}
					}
					if (flag6 && flag5)
					{
						break;
					}
					if (flag5)
					{
						num4 = -1;
						num5++;
					}
					else if (flag6)
					{
						if (num4 == 1)
						{
							num5++;
						}
						num4 = 1;
					}
					else
					{
						if (num4 == 1)
						{
							num5++;
						}
						num4 = -num4;
					}
				}
				IL_2D7:
				num += num7 * num6;
				if (num3 != 0 && flag7)
				{
					num2++;
					continue;
				}
				break;
				IL_22E:
				flag7 = true;
				goto IL_2D7;
			}
			Main.tile[num, num2].liquid = (byte)num3;
			Main.tile[num, num2].liquidType((int)b);
			if (Main.tile[num, num2].liquid > 0)
			{
				Liquid.AttemptToMoveLava(num, num2, tileAtXYHasLava);
				Liquid.AttemptToMoveHoney(num, num2, flag);
				Liquid.AttemptToMoveShimmer(num, num2, flag2);
			}
			Liquid.tilesIgnoreWater(false);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0001BD38 File Offset: 0x00019F38
		private static void AttemptToMoveHoney(int X, int Y, bool tileAtXYHasHoney)
		{
			if (Main.tile[X - 1, Y].liquid > 0 && Main.tile[X - 1, Y].honey() != tileAtXYHasHoney)
			{
				if (tileAtXYHasHoney)
				{
					Liquid.HoneyCheck(X, Y);
					return;
				}
				Liquid.HoneyCheck(X - 1, Y);
				return;
			}
			else if (Main.tile[X + 1, Y].liquid > 0 && Main.tile[X + 1, Y].honey() != tileAtXYHasHoney)
			{
				if (tileAtXYHasHoney)
				{
					Liquid.HoneyCheck(X, Y);
					return;
				}
				Liquid.HoneyCheck(X + 1, Y);
				return;
			}
			else
			{
				if (Main.tile[X, Y - 1].liquid <= 0 || Main.tile[X, Y - 1].honey() == tileAtXYHasHoney)
				{
					if (Main.tile[X, Y + 1].liquid > 0 && Main.tile[X, Y + 1].honey() != tileAtXYHasHoney)
					{
						if (tileAtXYHasHoney)
						{
							Liquid.HoneyCheck(X, Y);
							return;
						}
						Liquid.HoneyCheck(X, Y + 1);
					}
					return;
				}
				if (tileAtXYHasHoney)
				{
					Liquid.HoneyCheck(X, Y);
					return;
				}
				Liquid.HoneyCheck(X, Y - 1);
				return;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0001BE48 File Offset: 0x0001A048
		private static void AttemptToMoveLava(int X, int Y, bool tileAtXYHasLava)
		{
			if (Main.tile[X - 1, Y].liquid > 0 && Main.tile[X - 1, Y].lava() != tileAtXYHasLava)
			{
				if (tileAtXYHasLava)
				{
					Liquid.LavaCheck(X, Y);
					return;
				}
				Liquid.LavaCheck(X - 1, Y);
				return;
			}
			else if (Main.tile[X + 1, Y].liquid > 0 && Main.tile[X + 1, Y].lava() != tileAtXYHasLava)
			{
				if (tileAtXYHasLava)
				{
					Liquid.LavaCheck(X, Y);
					return;
				}
				Liquid.LavaCheck(X + 1, Y);
				return;
			}
			else
			{
				if (Main.tile[X, Y - 1].liquid <= 0 || Main.tile[X, Y - 1].lava() == tileAtXYHasLava)
				{
					if (Main.tile[X, Y + 1].liquid > 0 && Main.tile[X, Y + 1].lava() != tileAtXYHasLava)
					{
						if (tileAtXYHasLava)
						{
							Liquid.LavaCheck(X, Y);
							return;
						}
						Liquid.LavaCheck(X, Y + 1);
					}
					return;
				}
				if (tileAtXYHasLava)
				{
					Liquid.LavaCheck(X, Y);
					return;
				}
				Liquid.LavaCheck(X, Y - 1);
				return;
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0001BF58 File Offset: 0x0001A158
		private static void AttemptToMoveShimmer(int X, int Y, bool tileAtXYHasShimmer)
		{
			if (Main.tile[X - 1, Y].liquid > 0 && Main.tile[X - 1, Y].shimmer() != tileAtXYHasShimmer)
			{
				if (tileAtXYHasShimmer)
				{
					Liquid.ShimmerCheck(X, Y);
					return;
				}
				Liquid.ShimmerCheck(X - 1, Y);
				return;
			}
			else if (Main.tile[X + 1, Y].liquid > 0 && Main.tile[X + 1, Y].shimmer() != tileAtXYHasShimmer)
			{
				if (tileAtXYHasShimmer)
				{
					Liquid.ShimmerCheck(X, Y);
					return;
				}
				Liquid.ShimmerCheck(X + 1, Y);
				return;
			}
			else
			{
				if (Main.tile[X, Y - 1].liquid <= 0 || Main.tile[X, Y - 1].shimmer() == tileAtXYHasShimmer)
				{
					if (Main.tile[X, Y + 1].liquid > 0 && Main.tile[X, Y + 1].shimmer() != tileAtXYHasShimmer)
					{
						if (tileAtXYHasShimmer)
						{
							Liquid.ShimmerCheck(X, Y);
							return;
						}
						Liquid.ShimmerCheck(X, Y + 1);
					}
					return;
				}
				if (tileAtXYHasShimmer)
				{
					Liquid.ShimmerCheck(X, Y);
					return;
				}
				Liquid.ShimmerCheck(X, Y - 1);
				return;
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0001C068 File Offset: 0x0001A268
		private static void UpdateProgressDisplay(int verbose, int minY, int maxY, int y)
		{
			if (verbose > 0)
			{
				float num = (float)(maxY - y) / (float)(maxY - minY + 1);
				num /= (float)verbose;
				Main.statusText = string.Concat(new object[]
				{
					Lang.gen[27].Value,
					" ",
					(int)(num * 100f + 1f),
					"%"
				});
				return;
			}
			if (verbose < 0)
			{
				float num2 = (float)(maxY - y) / (float)(maxY - minY + 1);
				num2 /= (float)(-(float)verbose);
				Main.statusText = string.Concat(new object[]
				{
					Lang.gen[18].Value,
					" ",
					(int)(num2 * 100f + 1f),
					"%"
				});
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0001C130 File Offset: 0x0001A330
		public void Update()
		{
			Main.tileSolid[379] = true;
			Tile tile = Main.tile[this.x - 1, this.y];
			Tile tile2 = Main.tile[this.x + 1, this.y];
			Tile tile3 = Main.tile[this.x, this.y - 1];
			Tile tile4 = Main.tile[this.x, this.y + 1];
			Tile tile5 = Main.tile[this.x, this.y];
			if (tile5.nactive() && Main.tileSolid[(int)tile5.type] && !Main.tileSolidTop[(int)tile5.type])
			{
				ushort type = tile5.type;
				this.kill = 999;
				return;
			}
			byte liquid = tile5.liquid;
			if (this.y > Main.UnderworldLayer && tile5.liquidType() == 0 && tile5.liquid > 0)
			{
				byte b = 2;
				if (tile5.liquid < b)
				{
					b = tile5.liquid;
				}
				Tile tile6 = tile5;
				tile6.liquid -= b;
			}
			if (tile5.liquid == 0)
			{
				this.kill = 999;
				return;
			}
			if (tile5.lava())
			{
				Liquid.LavaCheck(this.x, this.y);
				if (!Liquid.quickFall)
				{
					if (this.delay < 5)
					{
						this.delay++;
						return;
					}
					this.delay = 0;
				}
			}
			else
			{
				if (tile.lava())
				{
					Liquid.AddWater(this.x - 1, this.y);
				}
				if (tile2.lava())
				{
					Liquid.AddWater(this.x + 1, this.y);
				}
				if (tile3.lava())
				{
					Liquid.AddWater(this.x, this.y - 1);
				}
				if (tile4.lava())
				{
					Liquid.AddWater(this.x, this.y + 1);
				}
				if (tile5.honey())
				{
					Liquid.HoneyCheck(this.x, this.y);
					if (!Liquid.quickFall)
					{
						if (this.delay < 10)
						{
							this.delay++;
							return;
						}
						this.delay = 0;
					}
				}
				else
				{
					if (tile.honey())
					{
						Liquid.AddWater(this.x - 1, this.y);
					}
					if (tile2.honey())
					{
						Liquid.AddWater(this.x + 1, this.y);
					}
					if (tile3.honey())
					{
						Liquid.AddWater(this.x, this.y - 1);
					}
					if (tile4.honey())
					{
						Liquid.AddWater(this.x, this.y + 1);
					}
					if (tile5.shimmer())
					{
						Liquid.ShimmerCheck(this.x, this.y);
					}
					else
					{
						if (tile.shimmer())
						{
							Liquid.AddWater(this.x - 1, this.y);
						}
						if (tile2.shimmer())
						{
							Liquid.AddWater(this.x + 1, this.y);
						}
						if (tile3.shimmer())
						{
							Liquid.AddWater(this.x, this.y - 1);
						}
						if (tile4.shimmer())
						{
							Liquid.AddWater(this.x, this.y + 1);
						}
					}
				}
			}
			if ((!tile4.nactive() || !Main.tileSolid[(int)tile4.type] || Main.tileSolidTop[(int)tile4.type]) && (tile4.liquid <= 0 || tile4.liquidType() == tile5.liquidType()) && tile4.liquid < 255)
			{
				bool flag = false;
				float num = (float)(byte.MaxValue - tile4.liquid);
				if (num > (float)tile5.liquid)
				{
					num = (float)tile5.liquid;
				}
				if (num == 1f && tile5.liquid == 255)
				{
					flag = true;
				}
				if (!flag)
				{
					Tile tile7 = tile5;
					tile7.liquid -= (byte)num;
				}
				Tile tile8 = tile4;
				tile8.liquid += (byte)num;
				tile4.liquidType((int)tile5.liquidType());
				Liquid.AddWater(this.x, this.y + 1);
				tile4.skipLiquid(true);
				tile5.skipLiquid(true);
				if (Liquid.quickSettle && tile5.liquid > 250)
				{
					tile5.liquid = byte.MaxValue;
				}
				else if (!flag)
				{
					Liquid.AddWater(this.x - 1, this.y);
					Liquid.AddWater(this.x + 1, this.y);
				}
			}
			if (tile5.liquid > 0)
			{
				bool flag2 = true;
				bool flag3 = true;
				bool flag4 = true;
				bool flag5 = true;
				if (tile.nactive() && Main.tileSolid[(int)tile.type] && !Main.tileSolidTop[(int)tile.type])
				{
					flag2 = false;
				}
				else if (tile.liquid > 0 && tile.liquidType() != tile5.liquidType())
				{
					flag2 = false;
				}
				else if (Main.tile[this.x - 2, this.y].nactive() && Main.tileSolid[(int)Main.tile[this.x - 2, this.y].type] && !Main.tileSolidTop[(int)Main.tile[this.x - 2, this.y].type])
				{
					flag4 = false;
				}
				else if (Main.tile[this.x - 2, this.y].liquid == 0)
				{
					flag4 = false;
				}
				else if (Main.tile[this.x - 2, this.y].liquid > 0 && Main.tile[this.x - 2, this.y].liquidType() != tile5.liquidType())
				{
					flag4 = false;
				}
				if (tile2.nactive() && Main.tileSolid[(int)tile2.type] && !Main.tileSolidTop[(int)tile2.type])
				{
					flag3 = false;
				}
				else if (tile2.liquid > 0 && tile2.liquidType() != tile5.liquidType())
				{
					flag3 = false;
				}
				else if (Main.tile[this.x + 2, this.y].nactive() && Main.tileSolid[(int)Main.tile[this.x + 2, this.y].type] && !Main.tileSolidTop[(int)Main.tile[this.x + 2, this.y].type])
				{
					flag5 = false;
				}
				else if (Main.tile[this.x + 2, this.y].liquid == 0)
				{
					flag5 = false;
				}
				else if (Main.tile[this.x + 2, this.y].liquid > 0 && Main.tile[this.x + 2, this.y].liquidType() != tile5.liquidType())
				{
					flag5 = false;
				}
				int num2 = 0;
				if (tile5.liquid < 3)
				{
					num2 = -1;
				}
				if (tile5.liquid > 250)
				{
					flag4 = false;
					flag5 = false;
				}
				if (flag2 && flag3)
				{
					if (flag4 && flag5)
					{
						bool flag6 = true;
						bool flag7 = true;
						if (Main.tile[this.x - 3, this.y].nactive() && Main.tileSolid[(int)Main.tile[this.x - 3, this.y].type] && !Main.tileSolidTop[(int)Main.tile[this.x - 3, this.y].type])
						{
							flag6 = false;
						}
						else if (Main.tile[this.x - 3, this.y].liquid == 0)
						{
							flag6 = false;
						}
						else if (Main.tile[this.x - 3, this.y].liquidType() != tile5.liquidType())
						{
							flag6 = false;
						}
						if (Main.tile[this.x + 3, this.y].nactive() && Main.tileSolid[(int)Main.tile[this.x + 3, this.y].type] && !Main.tileSolidTop[(int)Main.tile[this.x + 3, this.y].type])
						{
							flag7 = false;
						}
						else if (Main.tile[this.x + 3, this.y].liquid == 0)
						{
							flag7 = false;
						}
						else if (Main.tile[this.x + 3, this.y].liquidType() != tile5.liquidType())
						{
							flag7 = false;
						}
						if (flag6 && flag7)
						{
							float num = (float)((int)(tile.liquid + tile2.liquid + Main.tile[this.x - 2, this.y].liquid + Main.tile[this.x + 2, this.y].liquid + Main.tile[this.x - 3, this.y].liquid + Main.tile[this.x + 3, this.y].liquid + tile5.liquid) + num2);
							num = (float)Math.Round((double)(num / 7f));
							int num3 = 0;
							tile.liquidType((int)tile5.liquidType());
							if (tile.liquid != (byte)num)
							{
								tile.liquid = (byte)num;
								Liquid.AddWater(this.x - 1, this.y);
							}
							else
							{
								num3++;
							}
							tile2.liquidType((int)tile5.liquidType());
							if (tile2.liquid != (byte)num)
							{
								tile2.liquid = (byte)num;
								Liquid.AddWater(this.x + 1, this.y);
							}
							else
							{
								num3++;
							}
							Main.tile[this.x - 2, this.y].liquidType((int)tile5.liquidType());
							if (Main.tile[this.x - 2, this.y].liquid != (byte)num)
							{
								Main.tile[this.x - 2, this.y].liquid = (byte)num;
								Liquid.AddWater(this.x - 2, this.y);
							}
							else
							{
								num3++;
							}
							Main.tile[this.x + 2, this.y].liquidType((int)tile5.liquidType());
							if (Main.tile[this.x + 2, this.y].liquid != (byte)num)
							{
								Main.tile[this.x + 2, this.y].liquid = (byte)num;
								Liquid.AddWater(this.x + 2, this.y);
							}
							else
							{
								num3++;
							}
							Main.tile[this.x - 3, this.y].liquidType((int)tile5.liquidType());
							if (Main.tile[this.x - 3, this.y].liquid != (byte)num)
							{
								Main.tile[this.x - 3, this.y].liquid = (byte)num;
								Liquid.AddWater(this.x - 3, this.y);
							}
							else
							{
								num3++;
							}
							Main.tile[this.x + 3, this.y].liquidType((int)tile5.liquidType());
							if (Main.tile[this.x + 3, this.y].liquid != (byte)num)
							{
								Main.tile[this.x + 3, this.y].liquid = (byte)num;
								Liquid.AddWater(this.x + 3, this.y);
							}
							else
							{
								num3++;
							}
							if (tile.liquid != (byte)num || tile5.liquid != (byte)num)
							{
								Liquid.AddWater(this.x - 1, this.y);
							}
							if (tile2.liquid != (byte)num || tile5.liquid != (byte)num)
							{
								Liquid.AddWater(this.x + 1, this.y);
							}
							if (Main.tile[this.x - 2, this.y].liquid != (byte)num || tile5.liquid != (byte)num)
							{
								Liquid.AddWater(this.x - 2, this.y);
							}
							if (Main.tile[this.x + 2, this.y].liquid != (byte)num || tile5.liquid != (byte)num)
							{
								Liquid.AddWater(this.x + 2, this.y);
							}
							if (Main.tile[this.x - 3, this.y].liquid != (byte)num || tile5.liquid != (byte)num)
							{
								Liquid.AddWater(this.x - 3, this.y);
							}
							if (Main.tile[this.x + 3, this.y].liquid != (byte)num || tile5.liquid != (byte)num)
							{
								Liquid.AddWater(this.x + 3, this.y);
							}
							if (num3 != 6 || tile3.liquid <= 0)
							{
								tile5.liquid = (byte)num;
							}
						}
						else
						{
							int num4 = 0;
							float num = (float)((int)(tile.liquid + tile2.liquid + Main.tile[this.x - 2, this.y].liquid + Main.tile[this.x + 2, this.y].liquid + tile5.liquid) + num2);
							num = (float)Math.Round((double)(num / 5f));
							tile.liquidType((int)tile5.liquidType());
							if (tile.liquid != (byte)num)
							{
								tile.liquid = (byte)num;
								Liquid.AddWater(this.x - 1, this.y);
							}
							else
							{
								num4++;
							}
							tile2.liquidType((int)tile5.liquidType());
							if (tile2.liquid != (byte)num)
							{
								tile2.liquid = (byte)num;
								Liquid.AddWater(this.x + 1, this.y);
							}
							else
							{
								num4++;
							}
							Main.tile[this.x - 2, this.y].liquidType((int)tile5.liquidType());
							if (Main.tile[this.x - 2, this.y].liquid != (byte)num)
							{
								Main.tile[this.x - 2, this.y].liquid = (byte)num;
								Liquid.AddWater(this.x - 2, this.y);
							}
							else
							{
								num4++;
							}
							Main.tile[this.x + 2, this.y].liquidType((int)tile5.liquidType());
							if (Main.tile[this.x + 2, this.y].liquid != (byte)num)
							{
								Main.tile[this.x + 2, this.y].liquid = (byte)num;
								Liquid.AddWater(this.x + 2, this.y);
							}
							else
							{
								num4++;
							}
							if (tile.liquid != (byte)num || tile5.liquid != (byte)num)
							{
								Liquid.AddWater(this.x - 1, this.y);
							}
							if (tile2.liquid != (byte)num || tile5.liquid != (byte)num)
							{
								Liquid.AddWater(this.x + 1, this.y);
							}
							if (Main.tile[this.x - 2, this.y].liquid != (byte)num || tile5.liquid != (byte)num)
							{
								Liquid.AddWater(this.x - 2, this.y);
							}
							if (Main.tile[this.x + 2, this.y].liquid != (byte)num || tile5.liquid != (byte)num)
							{
								Liquid.AddWater(this.x + 2, this.y);
							}
							if (num4 != 4 || tile3.liquid <= 0)
							{
								tile5.liquid = (byte)num;
							}
						}
					}
					else if (flag4)
					{
						float num = (float)((int)(tile.liquid + tile2.liquid + Main.tile[this.x - 2, this.y].liquid + tile5.liquid) + num2);
						num = (float)Math.Round((double)(num / 4f));
						tile.liquidType((int)tile5.liquidType());
						if (tile.liquid != (byte)num || tile5.liquid != (byte)num)
						{
							tile.liquid = (byte)num;
							Liquid.AddWater(this.x - 1, this.y);
						}
						tile2.liquidType((int)tile5.liquidType());
						if (tile2.liquid != (byte)num || tile5.liquid != (byte)num)
						{
							tile2.liquid = (byte)num;
							Liquid.AddWater(this.x + 1, this.y);
						}
						Main.tile[this.x - 2, this.y].liquidType((int)tile5.liquidType());
						if (Main.tile[this.x - 2, this.y].liquid != (byte)num || tile5.liquid != (byte)num)
						{
							Main.tile[this.x - 2, this.y].liquid = (byte)num;
							Liquid.AddWater(this.x - 2, this.y);
						}
						tile5.liquid = (byte)num;
					}
					else if (flag5)
					{
						float num = (float)((int)(tile.liquid + tile2.liquid + Main.tile[this.x + 2, this.y].liquid + tile5.liquid) + num2);
						num = (float)Math.Round((double)(num / 4f));
						tile.liquidType((int)tile5.liquidType());
						if (tile.liquid != (byte)num || tile5.liquid != (byte)num)
						{
							tile.liquid = (byte)num;
							Liquid.AddWater(this.x - 1, this.y);
						}
						tile2.liquidType((int)tile5.liquidType());
						if (tile2.liquid != (byte)num || tile5.liquid != (byte)num)
						{
							tile2.liquid = (byte)num;
							Liquid.AddWater(this.x + 1, this.y);
						}
						Main.tile[this.x + 2, this.y].liquidType((int)tile5.liquidType());
						if (Main.tile[this.x + 2, this.y].liquid != (byte)num || tile5.liquid != (byte)num)
						{
							Main.tile[this.x + 2, this.y].liquid = (byte)num;
							Liquid.AddWater(this.x + 2, this.y);
						}
						tile5.liquid = (byte)num;
					}
					else
					{
						float num = (float)((int)(tile.liquid + tile2.liquid + tile5.liquid) + num2);
						num = (float)Math.Round((double)(num / 3f));
						if (num == 254f && WorldGen.genRand.Next(30) == 0)
						{
							num = 255f;
						}
						tile.liquidType((int)tile5.liquidType());
						if (tile.liquid != (byte)num)
						{
							tile.liquid = (byte)num;
							Liquid.AddWater(this.x - 1, this.y);
						}
						tile2.liquidType((int)tile5.liquidType());
						if (tile2.liquid != (byte)num)
						{
							tile2.liquid = (byte)num;
							Liquid.AddWater(this.x + 1, this.y);
						}
						tile5.liquid = (byte)num;
					}
				}
				else if (flag2)
				{
					float num = (float)((int)(tile.liquid + tile5.liquid) + num2);
					num = (float)Math.Round((double)(num / 2f));
					if (tile.liquid != (byte)num)
					{
						tile.liquid = (byte)num;
					}
					tile.liquidType((int)tile5.liquidType());
					if (tile5.liquid != (byte)num || tile.liquid != (byte)num)
					{
						Liquid.AddWater(this.x - 1, this.y);
					}
					tile5.liquid = (byte)num;
				}
				else if (flag3)
				{
					float num = (float)((int)(tile2.liquid + tile5.liquid) + num2);
					num = (float)Math.Round((double)(num / 2f));
					if (tile2.liquid != (byte)num)
					{
						tile2.liquid = (byte)num;
					}
					tile2.liquidType((int)tile5.liquidType());
					if (tile5.liquid != (byte)num || tile2.liquid != (byte)num)
					{
						Liquid.AddWater(this.x + 1, this.y);
					}
					tile5.liquid = (byte)num;
				}
			}
			if (tile5.liquid == liquid)
			{
				this.kill++;
				return;
			}
			if (tile5.liquid != 254 || liquid != 255)
			{
				Liquid.AddWater(this.x, this.y - 1);
				this.kill = 0;
				return;
			}
			if (Liquid.quickSettle)
			{
				tile5.liquid = byte.MaxValue;
				this.kill++;
				return;
			}
			this.kill++;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0001D638 File Offset: 0x0001B838
		public static void StartPanic()
		{
			if (!Liquid.panicMode)
			{
				GenVars.waterLine = Main.maxTilesY;
				Liquid.numLiquid = 0;
				LiquidBuffer.numLiquidBuffer = 0;
				Liquid.panicCounter = 0;
				Liquid.panicMode = true;
				Liquid.panicY = Main.maxTilesY - 3;
				if (Main.dedServ)
				{
					Console.WriteLine(Language.GetTextValue("Misc.ForceWaterSettling"));
				}
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0001D690 File Offset: 0x0001B890
		public static void UpdateLiquid()
		{
			int num = 8;
			Liquid.tilesIgnoreWater(true);
			if (Main.netMode == 2)
			{
				int num2 = 0;
				for (int i = 0; i < 15; i++)
				{
					if (Main.player[i].active)
					{
						num2++;
					}
				}
				Liquid.cycles = 10 + num2 / 3;
				Liquid.curMaxLiquid = Liquid.maxLiquid - num2 * 250;
				num = 10 + num2 / 3;
				if (Main.Setting_UseReducedMaxLiquids)
				{
					Liquid.curMaxLiquid = 5000;
				}
			}
			if (!WorldGen.gen)
			{
				if (!Liquid.panicMode)
				{
					if ((double)LiquidBuffer.numLiquidBuffer >= 45000.0)
					{
						Liquid.panicCounter++;
						if (Liquid.panicCounter > 3600)
						{
							Liquid.StartPanic();
						}
					}
					else
					{
						Liquid.panicCounter = 0;
					}
				}
				if (Liquid.panicMode)
				{
					int num3 = 0;
					while (Liquid.panicY >= 3 && num3 < 5)
					{
						num3++;
						Liquid.QuickWater(0, Liquid.panicY, Liquid.panicY);
						Liquid.panicY--;
						if (Liquid.panicY < 3)
						{
							Console.WriteLine(Language.GetTextValue("Misc.WaterSettled"));
							Liquid.panicCounter = 0;
							Liquid.panicMode = false;
							WorldGen.WaterCheck();
							if (Main.netMode == 2)
							{
								for (int j = 0; j < 255; j++)
								{
									for (int k = 0; k < Main.maxSectionsX; k++)
									{
										for (int l = 0; l < Main.maxSectionsY; l++)
										{
											Netplay.Clients[j].TileSections[k, l] = false;
										}
									}
								}
							}
						}
					}
					return;
				}
			}
			bool flag = Liquid.quickSettle;
			if (Main.Setting_UseReducedMaxLiquids)
			{
				flag |= (Liquid.numLiquid > 2000);
			}
			if (flag)
			{
				Liquid.quickFall = true;
			}
			else
			{
				Liquid.quickFall = false;
			}
			Liquid.wetCounter++;
			int num4 = Liquid.curMaxLiquid / Liquid.cycles;
			int num5 = num4 * (Liquid.wetCounter - 1);
			int num6 = num4 * Liquid.wetCounter;
			if (Liquid.wetCounter == Liquid.cycles)
			{
				num6 = Liquid.numLiquid;
			}
			if (num6 > Liquid.numLiquid)
			{
				num6 = Liquid.numLiquid;
				int netMode = Main.netMode;
				Liquid.wetCounter = Liquid.cycles;
			}
			if (Liquid.quickFall)
			{
				for (int m = num5; m < num6; m++)
				{
					Main.liquid[m].delay = 10;
					Main.liquid[m].Update();
					Main.tile[Main.liquid[m].x, Main.liquid[m].y].skipLiquid(false);
				}
			}
			else
			{
				for (int n = num5; n < num6; n++)
				{
					if (!Main.tile[Main.liquid[n].x, Main.liquid[n].y].skipLiquid())
					{
						Main.liquid[n].Update();
					}
					else
					{
						Main.tile[Main.liquid[n].x, Main.liquid[n].y].skipLiquid(false);
					}
				}
			}
			if (Liquid.wetCounter >= Liquid.cycles)
			{
				Liquid.wetCounter = 0;
				for (int num7 = Liquid.numLiquid - 1; num7 >= 0; num7--)
				{
					if (Main.liquid[num7].kill >= num)
					{
						if (Main.tile[Main.liquid[num7].x, Main.liquid[num7].y].liquid == 254)
						{
							Main.tile[Main.liquid[num7].x, Main.liquid[num7].y].liquid = byte.MaxValue;
						}
						Liquid.DelWater(num7);
					}
				}
				int num8 = Liquid.curMaxLiquid - (Liquid.curMaxLiquid - Liquid.numLiquid);
				if (num8 > LiquidBuffer.numLiquidBuffer)
				{
					num8 = LiquidBuffer.numLiquidBuffer;
				}
				for (int num9 = 0; num9 < num8; num9++)
				{
					Main.tile[Main.liquidBuffer[0].x, Main.liquidBuffer[0].y].checkingLiquid(false);
					Liquid.AddWater(Main.liquidBuffer[0].x, Main.liquidBuffer[0].y);
					LiquidBuffer.DelBuffer(0);
				}
				if (Liquid.numLiquid > 0 && Liquid.numLiquid > Liquid.stuckAmount - 50 && Liquid.numLiquid < Liquid.stuckAmount + 50)
				{
					Liquid.stuckCount++;
					if (Liquid.stuckCount >= 10000)
					{
						Liquid.stuck = true;
						for (int num10 = Liquid.numLiquid - 1; num10 >= 0; num10--)
						{
							Liquid.DelWater(num10);
						}
						Liquid.stuck = false;
						Liquid.stuckCount = 0;
					}
				}
				else
				{
					Liquid.stuckCount = 0;
					Liquid.stuckAmount = Liquid.numLiquid;
				}
			}
			if (!WorldGen.gen && Main.netMode == 2 && Liquid._netChangeSet.Count > 0)
			{
				Utils.Swap<HashSet<int>>(ref Liquid._netChangeSet, ref Liquid._swapNetChangeSet);
				NetLiquidModule.CreateAndBroadcastByChunk(Liquid._swapNetChangeSet);
				Liquid._swapNetChangeSet.Clear();
			}
			Liquid.tilesIgnoreWater(false);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0001DB5C File Offset: 0x0001BD5C
		public static void AddWater(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			if (Main.tile[x, y] == null)
			{
				return;
			}
			if (tile.checkingLiquid())
			{
				return;
			}
			if (x >= Main.maxTilesX - 5 || y >= Main.maxTilesY - 5)
			{
				return;
			}
			if (x < 5 || y < 5)
			{
				return;
			}
			if (tile.liquid == 0)
			{
				return;
			}
			if (tile.nactive() && Main.tileSolid[(int)tile.type] && tile.type != 546 && !Main.tileSolidTop[(int)tile.type])
			{
				return;
			}
			if (Liquid.numLiquid >= Liquid.curMaxLiquid - 1)
			{
				LiquidBuffer.AddBuffer(x, y);
				return;
			}
			tile.checkingLiquid(true);
			tile.skipLiquid(false);
			Main.liquid[Liquid.numLiquid].kill = 0;
			Main.liquid[Liquid.numLiquid].x = x;
			Main.liquid[Liquid.numLiquid].y = y;
			Main.liquid[Liquid.numLiquid].delay = 0;
			Liquid.numLiquid++;
			if (Main.netMode == 2)
			{
				Liquid.NetSendLiquid(x, y);
			}
			if (tile.active() && !WorldGen.gen)
			{
				bool flag = false;
				if (tile.lava())
				{
					if (TileObjectData.CheckLavaDeath(tile))
					{
						flag = true;
					}
				}
				else if (TileObjectData.CheckWaterDeath(tile))
				{
					flag = true;
				}
				if (flag)
				{
					WorldGen.KillTile(x, y, false, false, false);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 0f, 0, 0, 0);
					}
				}
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0001DCC8 File Offset: 0x0001BEC8
		private static bool UndergroundDesertCheck(int x, int y)
		{
			int num = 3;
			for (int i = x - num; i <= x + num; i++)
			{
				for (int j = y - num; j <= y + num; j++)
				{
					if (WorldGen.InWorld(i, j, 0) && (Main.tile[i, j].wall == 187 || Main.tile[i, j].wall == 216))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0001DD34 File Offset: 0x0001BF34
		public static void LiquidCheck(int x, int y, int thisLiquidType)
		{
			if (WorldGen.SolidTile(x, y, false))
			{
				return;
			}
			Tile tile = Main.tile[x - 1, y];
			Tile tile2 = Main.tile[x + 1, y];
			Tile tile3 = Main.tile[x, y - 1];
			Tile tile4 = Main.tile[x, y + 1];
			Tile tile5 = Main.tile[x, y];
			if ((tile.liquid > 0 && (int)tile.liquidType() != thisLiquidType) || (tile2.liquid > 0 && (int)tile2.liquidType() != thisLiquidType) || (tile3.liquid > 0 && (int)tile3.liquidType() != thisLiquidType))
			{
				int num = 0;
				if ((int)tile.liquidType() != thisLiquidType)
				{
					num += (int)tile.liquid;
					tile.liquid = 0;
				}
				if ((int)tile2.liquidType() != thisLiquidType)
				{
					num += (int)tile2.liquid;
					tile2.liquid = 0;
				}
				if ((int)tile3.liquidType() != thisLiquidType)
				{
					num += (int)tile3.liquid;
					tile3.liquid = 0;
				}
				int type = 56;
				int num2 = 0;
				bool waterNearby = tile.liquidType() == 0 || tile2.liquidType() == 0 || tile3.liquidType() == 0;
				bool lavaNearby = tile.lava() || tile2.lava() || tile3.lava();
				bool honeyNearby = tile.honey() || tile2.honey() || tile3.honey();
				bool shimmerNearby = tile.shimmer() || tile2.shimmer() || tile3.shimmer();
				Liquid.GetLiquidMergeTypes(thisLiquidType, out type, out num2, waterNearby, lavaNearby, honeyNearby, shimmerNearby);
				if (num >= 24 && num2 != thisLiquidType)
				{
					if (tile5.active() && Main.tileObsidianKill[(int)tile5.type])
					{
						WorldGen.KillTile(x, y, false, false, false);
						if (Main.netMode == 2)
						{
							NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 0f, 0, 0, 0);
						}
					}
					if (!tile5.active())
					{
						tile5.liquid = 0;
						switch (thisLiquidType)
						{
						case 1:
							tile5.lava(false);
							break;
						case 2:
							tile5.honey(false);
							break;
						case 3:
							tile5.shimmer(false);
							break;
						}
						TileChangeType liquidChangeType = WorldGen.GetLiquidChangeType(thisLiquidType, num2);
						if (!WorldGen.gen)
						{
							WorldGen.PlayLiquidChangeSound(liquidChangeType, x, y, 1);
						}
						WorldGen.PlaceTile(x, y, type, true, true, -1, 0);
						WorldGen.SquareTileFrame(x, y, true);
						if (Main.netMode == 2)
						{
							NetMessage.SendTileSquare(-1, x - 1, y - 1, 3, liquidChangeType);
							return;
						}
					}
				}
			}
			else if (tile4.liquid > 0 && (int)tile4.liquidType() != thisLiquidType)
			{
				bool flag = false;
				if (tile5.active() && TileID.Sets.IsAContainer[(int)tile5.type] && !TileID.Sets.IsAContainer[(int)tile4.type])
				{
					flag = true;
				}
				if (thisLiquidType != 0 && Main.tileCut[(int)tile4.type])
				{
					WorldGen.KillTile(x, y + 1, false, false, false);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)(y + 1), 0f, 0, 0, 0);
					}
				}
				else if (tile4.active() && Main.tileObsidianKill[(int)tile4.type])
				{
					WorldGen.KillTile(x, y + 1, false, false, false);
					if (Main.netMode == 2)
					{
						NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)(y + 1), 0f, 0, 0, 0);
					}
				}
				if (!tile4.active() || flag)
				{
					if (tile5.liquid < 24)
					{
						tile5.liquid = 0;
						tile5.liquidType(0);
						if (Main.netMode == 2)
						{
							NetMessage.SendTileSquare(-1, x - 1, y, 3, TileChangeType.None);
							return;
						}
					}
					else
					{
						int type2 = 56;
						int otherLiquidType = 0;
						bool waterNearby2 = tile4.liquidType() == 0;
						bool lavaNearby2 = tile4.lava();
						bool honeyNearby2 = tile4.honey();
						bool shimmerNearby2 = tile4.shimmer();
						Liquid.GetLiquidMergeTypes(thisLiquidType, out type2, out otherLiquidType, waterNearby2, lavaNearby2, honeyNearby2, shimmerNearby2);
						tile5.liquid = 0;
						switch (thisLiquidType)
						{
						case 1:
							tile5.lava(false);
							break;
						case 2:
							tile5.honey(false);
							break;
						case 3:
							tile5.shimmer(false);
							break;
						}
						tile4.liquid = 0;
						TileChangeType liquidChangeType2 = WorldGen.GetLiquidChangeType(thisLiquidType, otherLiquidType);
						if (!Main.gameMenu)
						{
							WorldGen.PlayLiquidChangeSound(liquidChangeType2, x, y, 1);
						}
						WorldGen.PlaceTile(x, y + 1, type2, true, true, -1, 0);
						WorldGen.SquareTileFrame(x, y + 1, true);
						if (Main.netMode == 2)
						{
							NetMessage.SendTileSquare(-1, x - 1, y, 3, liquidChangeType2);
						}
					}
				}
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0001E16C File Offset: 0x0001C36C
		public static void GetLiquidMergeTypes(int thisLiquidType, out int liquidMergeTileType, out int liquidMergeType, bool waterNearby, bool lavaNearby, bool honeyNearby, bool shimmerNearby)
		{
			liquidMergeTileType = 56;
			liquidMergeType = thisLiquidType;
			if (thisLiquidType != 0 && waterNearby)
			{
				switch (thisLiquidType)
				{
				case 1:
					liquidMergeTileType = 56;
					break;
				case 2:
					liquidMergeTileType = 229;
					break;
				case 3:
					liquidMergeTileType = 659;
					break;
				}
				liquidMergeType = 0;
			}
			if (thisLiquidType != 1 && lavaNearby)
			{
				switch (thisLiquidType)
				{
				case 0:
					liquidMergeTileType = 56;
					break;
				case 2:
					liquidMergeTileType = 230;
					break;
				case 3:
					liquidMergeTileType = 659;
					break;
				}
				liquidMergeType = 1;
			}
			if (thisLiquidType != 2 && honeyNearby)
			{
				switch (thisLiquidType)
				{
				case 0:
					liquidMergeTileType = 229;
					break;
				case 1:
					liquidMergeTileType = 230;
					break;
				case 3:
					liquidMergeTileType = 659;
					break;
				}
				liquidMergeType = 2;
			}
			if (thisLiquidType != 3 && shimmerNearby)
			{
				switch (thisLiquidType)
				{
				case 0:
					liquidMergeTileType = 659;
					break;
				case 1:
					liquidMergeTileType = 659;
					break;
				case 2:
					liquidMergeTileType = 659;
					break;
				}
				liquidMergeType = 3;
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0001E270 File Offset: 0x0001C470
		public static void LavaCheck(int x, int y)
		{
			if (!WorldGen.remixWorldGen && WorldGen.generatingWorld && Liquid.UndergroundDesertCheck(x, y))
			{
				for (int i = x - 3; i <= x + 3; i++)
				{
					for (int j = y - 3; j <= y + 3; j++)
					{
						Main.tile[i, j].lava(true);
					}
				}
			}
			Liquid.LiquidCheck(x, y, 1);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0001E2CE File Offset: 0x0001C4CE
		public static void HoneyCheck(int x, int y)
		{
			Liquid.LiquidCheck(x, y, 2);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0001E2D8 File Offset: 0x0001C4D8
		public static void ShimmerCheck(int x, int y)
		{
			Liquid.LiquidCheck(x, y, 3);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0001E2E4 File Offset: 0x0001C4E4
		public static void DelWater(int l)
		{
			int num = Main.liquid[l].x;
			int num2 = Main.liquid[l].y;
			Tile tile = Main.tile[num - 1, num2];
			Tile tile2 = Main.tile[num + 1, num2];
			Tile tile3 = Main.tile[num, num2 + 1];
			Tile tile4 = Main.tile[num, num2];
			byte b = 2;
			if (tile4.liquid < b)
			{
				tile4.liquid = 0;
				if (tile.liquid < b)
				{
					tile.liquid = 0;
				}
				else
				{
					Liquid.AddWater(num - 1, num2);
				}
				if (tile2.liquid < b)
				{
					tile2.liquid = 0;
				}
				else
				{
					Liquid.AddWater(num + 1, num2);
				}
			}
			else if (tile4.liquid < 20)
			{
				if ((tile.liquid < tile4.liquid && (!tile.nactive() || !Main.tileSolid[(int)tile.type] || Main.tileSolidTop[(int)tile.type])) || (tile2.liquid < tile4.liquid && (!tile2.nactive() || !Main.tileSolid[(int)tile2.type] || Main.tileSolidTop[(int)tile2.type])) || (tile3.liquid < 255 && (!tile3.nactive() || !Main.tileSolid[(int)tile3.type] || Main.tileSolidTop[(int)tile3.type])))
				{
					tile4.liquid = 0;
				}
			}
			else if (tile3.liquid < 255 && (!tile3.nactive() || !Main.tileSolid[(int)tile3.type] || Main.tileSolidTop[(int)tile3.type]) && !Liquid.stuck && (!Main.tile[num, num2].nactive() || !Main.tileSolid[(int)Main.tile[num, num2].type] || Main.tileSolidTop[(int)Main.tile[num, num2].type]))
			{
				Main.liquid[l].kill = 0;
				return;
			}
			if (tile4.liquid < 250 && Main.tile[num, num2 - 1].liquid > 0)
			{
				Liquid.AddWater(num, num2 - 1);
			}
			if (tile4.liquid == 0)
			{
				tile4.liquidType(0);
			}
			else
			{
				if (tile2.liquid > 0 && tile2.liquid < 250 && (!tile2.nactive() || !Main.tileSolid[(int)tile2.type] || Main.tileSolidTop[(int)tile2.type]) && tile4.liquid != tile2.liquid)
				{
					Liquid.AddWater(num + 1, num2);
				}
				if (tile.liquid > 0 && tile.liquid < 250 && (!tile.nactive() || !Main.tileSolid[(int)tile.type] || Main.tileSolidTop[(int)tile.type]) && tile4.liquid != tile.liquid)
				{
					Liquid.AddWater(num - 1, num2);
				}
				if (tile4.lava())
				{
					Liquid.LavaCheck(num, num2);
					for (int i = num - 1; i <= num + 1; i++)
					{
						for (int j = num2 - 1; j <= num2 + 1; j++)
						{
							Tile tile5 = Main.tile[i, j];
							if (tile5.active())
							{
								if (tile5.type == 2 || tile5.type == 23 || tile5.type == 109 || tile5.type == 199 || tile5.type == 477 || tile5.type == 492)
								{
									tile5.type = 0;
									WorldGen.SquareTileFrame(i, j, true);
									if (Main.netMode == 2)
									{
										NetMessage.SendTileSquare(-1, num, num2, 3, TileChangeType.None);
									}
								}
								else if (tile5.type == 60 || tile5.type == 70 || tile5.type == 661 || tile5.type == 662)
								{
									tile5.type = 59;
									WorldGen.SquareTileFrame(i, j, true);
									if (Main.netMode == 2)
									{
										NetMessage.SendTileSquare(-1, num, num2, 3, TileChangeType.None);
									}
								}
							}
						}
					}
				}
				else if (tile4.honey())
				{
					Liquid.HoneyCheck(num, num2);
				}
				else if (tile4.shimmer())
				{
					Liquid.ShimmerCheck(num, num2);
				}
			}
			if (Main.netMode == 2)
			{
				Liquid.NetSendLiquid(num, num2);
			}
			Liquid.numLiquid--;
			Main.tile[Main.liquid[l].x, Main.liquid[l].y].checkingLiquid(false);
			Main.liquid[l].x = Main.liquid[Liquid.numLiquid].x;
			Main.liquid[l].y = Main.liquid[Liquid.numLiquid].y;
			Main.liquid[l].kill = Main.liquid[Liquid.numLiquid].kill;
			if (Main.tileAlch[(int)tile4.type])
			{
				WorldGen.CheckAlch(num, num2);
				return;
			}
			if (tile4.type == 518)
			{
				if (Liquid.quickFall)
				{
					WorldGen.CheckLilyPad(num, num2);
					return;
				}
				if (Main.tile[num, num2 + 1].liquid < 255 || Main.tile[num, num2 - 1].liquid > 0)
				{
					WorldGen.SquareTileFrame(num, num2, true);
					return;
				}
				WorldGen.CheckLilyPad(num, num2);
			}
		}

		// Token: 0x0400013F RID: 319
		public const int maxLiquidBuffer = 50000;

		// Token: 0x04000140 RID: 320
		public static int maxLiquid = 25000;

		// Token: 0x04000141 RID: 321
		public static int skipCount;

		// Token: 0x04000142 RID: 322
		public static int stuckCount;

		// Token: 0x04000143 RID: 323
		public static int stuckAmount;

		// Token: 0x04000144 RID: 324
		public static int cycles = 10;

		// Token: 0x04000145 RID: 325
		public static int curMaxLiquid = 0;

		// Token: 0x04000146 RID: 326
		public static int numLiquid;

		// Token: 0x04000147 RID: 327
		public static bool stuck;

		// Token: 0x04000148 RID: 328
		public static bool quickFall;

		// Token: 0x04000149 RID: 329
		public static bool quickSettle;

		// Token: 0x0400014A RID: 330
		private static int wetCounter;

		// Token: 0x0400014B RID: 331
		public static int panicCounter;

		// Token: 0x0400014C RID: 332
		public static bool panicMode;

		// Token: 0x0400014D RID: 333
		public static int panicY;

		// Token: 0x0400014E RID: 334
		public int x;

		// Token: 0x0400014F RID: 335
		public int y;

		// Token: 0x04000150 RID: 336
		public int kill;

		// Token: 0x04000151 RID: 337
		public int delay;

		// Token: 0x04000152 RID: 338
		private static HashSet<int> _netChangeSet = new HashSet<int>();

		// Token: 0x04000153 RID: 339
		private static HashSet<int> _swapNetChangeSet = new HashSet<int>();
	}
}
