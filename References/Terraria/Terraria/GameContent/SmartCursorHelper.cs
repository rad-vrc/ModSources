using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Enums;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.ID;

namespace Terraria.GameContent
{
	// Token: 0x020001E8 RID: 488
	public class SmartCursorHelper
	{
		// Token: 0x06001C93 RID: 7315 RVA: 0x004F4D30 File Offset: 0x004F2F30
		public static void SmartCursorLookup(Player player)
		{
			Main.SmartCursorShowing = false;
			if (!Main.SmartCursorIsUsed)
			{
				return;
			}
			SmartCursorHelper.SmartCursorUsageInfo smartCursorUsageInfo = new SmartCursorHelper.SmartCursorUsageInfo
			{
				player = player,
				item = player.inventory[player.selectedItem],
				mouse = Main.MouseWorld,
				position = player.position,
				Center = player.Center
			};
			float gravDir = player.gravDir;
			int tileTargetX = Player.tileTargetX;
			int tileTargetY = Player.tileTargetY;
			int tileRangeX = Player.tileRangeX;
			int tileRangeY = Player.tileRangeY;
			smartCursorUsageInfo.screenTargetX = Utils.Clamp<int>(tileTargetX, 10, Main.maxTilesX - 10);
			smartCursorUsageInfo.screenTargetY = Utils.Clamp<int>(tileTargetY, 10, Main.maxTilesY - 10);
			if (Main.tile[smartCursorUsageInfo.screenTargetX, smartCursorUsageInfo.screenTargetY] == null)
			{
				return;
			}
			bool flag = SmartCursorHelper.IsHoveringOverAnInteractibleTileThatBlocksSmartCursor(smartCursorUsageInfo);
			SmartCursorHelper.TryFindingPaintInplayerInventory(smartCursorUsageInfo, out smartCursorUsageInfo.paintLookup, out smartCursorUsageInfo.paintCoatingLookup);
			int tileBoost = smartCursorUsageInfo.item.tileBoost;
			smartCursorUsageInfo.reachableStartX = (int)(player.position.X / 16f) - tileRangeX - tileBoost + 1;
			smartCursorUsageInfo.reachableEndX = (int)((player.position.X + (float)player.width) / 16f) + tileRangeX + tileBoost - 1;
			smartCursorUsageInfo.reachableStartY = (int)(player.position.Y / 16f) - tileRangeY - tileBoost + 1;
			smartCursorUsageInfo.reachableEndY = (int)((player.position.Y + (float)player.height) / 16f) + tileRangeY + tileBoost - 2;
			smartCursorUsageInfo.reachableStartX = Utils.Clamp<int>(smartCursorUsageInfo.reachableStartX, 10, Main.maxTilesX - 10);
			smartCursorUsageInfo.reachableEndX = Utils.Clamp<int>(smartCursorUsageInfo.reachableEndX, 10, Main.maxTilesX - 10);
			smartCursorUsageInfo.reachableStartY = Utils.Clamp<int>(smartCursorUsageInfo.reachableStartY, 10, Main.maxTilesY - 10);
			smartCursorUsageInfo.reachableEndY = Utils.Clamp<int>(smartCursorUsageInfo.reachableEndY, 10, Main.maxTilesY - 10);
			if (flag && smartCursorUsageInfo.screenTargetX >= smartCursorUsageInfo.reachableStartX && smartCursorUsageInfo.screenTargetX <= smartCursorUsageInfo.reachableEndX && smartCursorUsageInfo.screenTargetY >= smartCursorUsageInfo.reachableStartY && smartCursorUsageInfo.screenTargetY <= smartCursorUsageInfo.reachableEndY)
			{
				return;
			}
			SmartCursorHelper._grappleTargets.Clear();
			int[] grappling = player.grappling;
			int grapCount = player.grapCount;
			for (int i = 0; i < grapCount; i++)
			{
				Projectile projectile = Main.projectile[grappling[i]];
				int item = (int)projectile.Center.X / 16;
				int item2 = (int)projectile.Center.Y / 16;
				SmartCursorHelper._grappleTargets.Add(new Tuple<int, int>(item, item2));
			}
			int num = -1;
			int num2 = -1;
			if (!Player.SmartCursorSettings.SmartAxeAfterPickaxe)
			{
				SmartCursorHelper.Step_Axe(smartCursorUsageInfo, ref num, ref num2);
			}
			SmartCursorHelper.Step_ForceCursorToAnyMinableThing(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_Pickaxe_MineShinies(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_Pickaxe_MineSolids(player, smartCursorUsageInfo, SmartCursorHelper._grappleTargets, ref num, ref num2);
			if (Player.SmartCursorSettings.SmartAxeAfterPickaxe)
			{
				SmartCursorHelper.Step_Axe(smartCursorUsageInfo, ref num, ref num2);
			}
			SmartCursorHelper.Step_ColoredWrenches(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_MulticolorWrench(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_Hammers(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_ActuationRod(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_WireCutter(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_Platforms(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_MinecartTracks(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_Walls(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_PumpkinSeeds(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_GrassSeeds(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_Pigronata(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_Boulders(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_Torch(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_LawnMower(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_BlocksFilling(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_BlocksLines(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_PaintRoller(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_PaintBrush(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_PaintScrapper(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_Acorns(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_GemCorns(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_EmptyBuckets(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_Actuators(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_AlchemySeeds(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_PlanterBox(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_ClayPots(smartCursorUsageInfo, ref num, ref num2);
			SmartCursorHelper.Step_StaffOfRegrowth(smartCursorUsageInfo, ref num, ref num2);
			if (num != -1 && num2 != -1)
			{
				Main.SmartCursorX = (Player.tileTargetX = num);
				Main.SmartCursorY = (Player.tileTargetY = num2);
				Main.SmartCursorShowing = true;
			}
			SmartCursorHelper._grappleTargets.Clear();
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x004F5148 File Offset: 0x004F3348
		private static void TryFindingPaintInplayerInventory(SmartCursorHelper.SmartCursorUsageInfo providedInfo, out int paintLookup, out int coatingLookup)
		{
			Item[] inventory = providedInfo.player.inventory;
			paintLookup = 0;
			coatingLookup = 0;
			if (providedInfo.item.type != 1071 && providedInfo.item.type != 1543 && providedInfo.item.type != 1072 && providedInfo.item.type != 1544)
			{
				return;
			}
			Item item = providedInfo.player.FindPaintOrCoating();
			if (item == null)
			{
				return;
			}
			coatingLookup = (int)item.paintCoating;
			paintLookup = (int)item.paint;
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x004F51D8 File Offset: 0x004F33D8
		private static bool IsHoveringOverAnInteractibleTileThatBlocksSmartCursor(SmartCursorHelper.SmartCursorUsageInfo providedInfo)
		{
			bool result = false;
			if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].active())
			{
				ushort type = Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].type;
				if (type <= 209)
				{
					if (type <= 85)
					{
						if (type <= 29)
						{
							if (type <= 11)
							{
								if (type != 4 && type - 10 > 1)
								{
									return result;
								}
							}
							else if (type != 13 && type != 21 && type != 29)
							{
								return result;
							}
						}
						else if (type <= 50)
						{
							if (type != 33 && type - 49 > 1)
							{
								return result;
							}
						}
						else if (type != 55 && type != 79 && type != 85)
						{
							return result;
						}
					}
					else if (type <= 132)
					{
						if (type <= 97)
						{
							if (type != 88 && type != 97)
							{
								return result;
							}
						}
						else if (type != 104 && type != 125 && type != 132)
						{
							return result;
						}
					}
					else if (type <= 144)
					{
						if (type != 136 && type != 139 && type != 144)
						{
							return result;
						}
					}
					else if (type != 174 && type != 207 && type != 209)
					{
						return result;
					}
				}
				else if (type <= 389)
				{
					if (type <= 287)
					{
						if (type <= 216)
						{
							if (type != 212 && type != 216)
							{
								return result;
							}
						}
						else if (type != 219 && type != 237 && type != 287)
						{
							return result;
						}
					}
					else if (type <= 335)
					{
						if (type != 314)
						{
							if (type - 334 > 1)
							{
								return result;
							}
						}
						else
						{
							if (providedInfo.player.gravDir == 1f)
							{
								return true;
							}
							return result;
						}
					}
					else if (type != 338 && type != 354 && type - 386 > 3)
					{
						return result;
					}
				}
				else if (type <= 468)
				{
					if (type <= 425)
					{
						if (type != 411 && type != 425)
						{
							return result;
						}
					}
					else if (type != 441 && type - 463 > 1 && type - 467 > 1)
					{
						return result;
					}
				}
				else if (type <= 511)
				{
					if (type != 491 && type != 494 && type - 510 > 1)
					{
						return result;
					}
				}
				else if (type != 573 && type != 621 && type != 642)
				{
					return result;
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x004F5480 File Offset: 0x004F3680
		private static void Step_StaffOfRegrowth(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if ((providedInfo.item.type == 213 || providedInfo.item.type == 5295) && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
				{
					for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
					{
						Tile tile = Main.tile[i, j];
						bool flag = !Main.tile[i - 1, j].active() || !Main.tile[i, j + 1].active() || !Main.tile[i + 1, j].active() || !Main.tile[i, j - 1].active();
						bool flag2 = !Main.tile[i - 1, j - 1].active() || !Main.tile[i - 1, j + 1].active() || !Main.tile[i + 1, j + 1].active() || !Main.tile[i + 1, j - 1].active();
						if (tile.active() && !tile.inActive() && tile.type == 0 && (flag || (tile.type == 0 && flag2)))
						{
							SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
					{
						float num2 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num == -1f || num2 < num)
						{
							num = num2;
							tuple = SmartCursorHelper._targets[k];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x004F570C File Offset: 0x004F390C
		private static void Step_GrassSeeds(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (focusedX > -1 || focusedY > -1)
			{
				return;
			}
			int type = providedInfo.item.type;
			if (type < 0 || type >= (int)ItemID.Count || !ItemID.Sets.GrassSeeds[type])
			{
				return;
			}
			SmartCursorHelper._targets.Clear();
			for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
			{
				for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
				{
					Tile tile = Main.tile[i, j];
					bool flag = !Main.tile[i - 1, j].active() || !Main.tile[i, j + 1].active() || !Main.tile[i + 1, j].active() || !Main.tile[i, j - 1].active();
					bool flag2 = !Main.tile[i - 1, j - 1].active() || !Main.tile[i - 1, j + 1].active() || !Main.tile[i + 1, j + 1].active() || !Main.tile[i + 1, j - 1].active();
					if (tile.active() && !tile.inActive() && (flag || flag2))
					{
						bool flag3;
						if (type <= 195)
						{
							if (type == 59)
							{
								goto IL_172;
							}
							if (type - 194 > 1)
							{
								goto IL_165;
							}
							flag3 = (tile.type == 59);
						}
						else
						{
							if (type == 2171)
							{
								goto IL_172;
							}
							if (type != 5214)
							{
								goto IL_165;
							}
							flag3 = (tile.type == 57);
						}
						IL_1A5:
						if (flag3)
						{
							SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
							goto IL_1BA;
						}
						goto IL_1BA;
						IL_172:
						flag3 = (tile.type == 0 || tile.type == 59);
						goto IL_1A5;
						IL_165:
						flag3 = (tile.type == 0);
						goto IL_1A5;
					}
					IL_1BA:;
				}
			}
			if (SmartCursorHelper._targets.Count > 0)
			{
				float num = -1f;
				Tuple<int, int> tuple = SmartCursorHelper._targets[0];
				for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
				{
					float num2 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
					if (num == -1f || num2 < num)
					{
						num = num2;
						tuple = SmartCursorHelper._targets[k];
					}
				}
				if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
				{
					focusedX = tuple.Item1;
					focusedY = tuple.Item2;
				}
			}
			SmartCursorHelper._targets.Clear();
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x004F59F4 File Offset: 0x004F3BF4
		private static void Step_ClayPots(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.createTile == 78 && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				bool flag = false;
				if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].active())
				{
					flag = true;
				}
				if (!Collision.InTileBounds(providedInfo.screenTargetX, providedInfo.screenTargetY, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
				{
					flag = true;
				}
				if (!flag)
				{
					for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
					{
						for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
						{
							Tile tile = Main.tile[i, j];
							Tile tile2 = Main.tile[i, j + 1];
							if ((!tile.active() || Main.tileCut[(int)tile.type] || TileID.Sets.BreakableWhenPlacing[(int)tile.type]) && tile2.nactive() && !tile2.halfBrick() && tile2.slope() == 0 && Main.tileSolid[(int)tile2.type])
							{
								SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
							}
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
					{
						if (Collision.EmptyTile(SmartCursorHelper._targets[k].Item1, SmartCursorHelper._targets[k].Item2, true))
						{
							float num2 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
							if (num == -1f || num2 < num)
							{
								num = num2;
								tuple = SmartCursorHelper._targets[k];
							}
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY) && num != -1f)
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x004F5C6C File Offset: 0x004F3E6C
		private static void Step_PlanterBox(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.createTile == 380 && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				bool flag = false;
				if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].active() && Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].type == 380)
				{
					flag = true;
				}
				if (!flag)
				{
					for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
					{
						for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
						{
							Tile tile = Main.tile[i, j];
							if (tile.active() && tile.type == 380)
							{
								if (!Main.tile[i - 1, j].active() || Main.tileCut[(int)Main.tile[i - 1, j].type] || TileID.Sets.BreakableWhenPlacing[(int)Main.tile[i - 1, j].type])
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i - 1, j));
								}
								if (!Main.tile[i + 1, j].active() || Main.tileCut[(int)Main.tile[i + 1, j].type] || TileID.Sets.BreakableWhenPlacing[(int)Main.tile[i + 1, j].type])
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i + 1, j));
								}
							}
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
					{
						float num2 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num == -1f || num2 < num)
						{
							num = num2;
							tuple = SmartCursorHelper._targets[k];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY) && num != -1f)
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x004F5F14 File Offset: 0x004F4114
		private static void Step_AlchemySeeds(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.createTile == 82 && focusedX == -1 && focusedY == -1)
			{
				int placeStyle = providedInfo.item.placeStyle;
				SmartCursorHelper._targets.Clear();
				for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
				{
					for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
					{
						Tile tile = Main.tile[i, j];
						Tile tile2 = Main.tile[i, j + 1];
						bool flag = !tile.active() || TileID.Sets.BreakableWhenPlacing[(int)tile.type] || (Main.tileCut[(int)tile.type] && tile.type != 82 && tile.type != 83) || WorldGen.IsHarvestableHerbWithSeed((int)tile.type, (int)(tile.frameX / 18));
						bool flag2 = tile2.nactive() && !tile2.halfBrick() && tile2.slope() == 0;
						if (flag && flag2)
						{
							if (placeStyle == 0)
							{
								if (tile2.type != 78 && tile2.type != 380 && tile2.type != 2 && tile2.type != 477 && tile2.type != 109 && tile2.type != 492)
								{
									goto IL_357;
								}
								if (tile.liquid > 0)
								{
									goto IL_357;
								}
							}
							else if (placeStyle == 1)
							{
								if (tile2.type != 78 && tile2.type != 380 && tile2.type != 60)
								{
									goto IL_357;
								}
								if (tile.liquid > 0)
								{
									goto IL_357;
								}
							}
							else if (placeStyle == 2)
							{
								if (tile2.type != 78 && tile2.type != 380 && tile2.type != 0 && tile2.type != 59)
								{
									goto IL_357;
								}
								if (tile.liquid > 0)
								{
									goto IL_357;
								}
							}
							else if (placeStyle == 3)
							{
								if (tile2.type != 78 && tile2.type != 380 && tile2.type != 203 && tile2.type != 199 && tile2.type != 23 && tile2.type != 25)
								{
									goto IL_357;
								}
								if (tile.liquid > 0)
								{
									goto IL_357;
								}
							}
							else if (placeStyle == 4)
							{
								if (tile2.type != 78 && tile2.type != 380 && tile2.type != 53 && tile2.type != 116)
								{
									goto IL_357;
								}
								if (tile.liquid > 0 && tile.lava())
								{
									goto IL_357;
								}
							}
							else if (placeStyle == 5)
							{
								if (tile2.type != 78 && tile2.type != 380 && tile2.type != 57 && tile2.type != 633)
								{
									goto IL_357;
								}
								if (tile.liquid > 0 && !tile.lava())
								{
									goto IL_357;
								}
							}
							else if (placeStyle == 6 && ((tile2.type != 78 && tile2.type != 380 && tile2.type != 147 && tile2.type != 161 && tile2.type != 163 && tile2.type != 164 && tile2.type != 200) || (tile.liquid > 0 && tile.lava())))
							{
								goto IL_357;
							}
							SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
						}
						IL_357:;
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
					{
						float num2 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num == -1f || num2 < num)
						{
							num = num2;
							tuple = SmartCursorHelper._targets[k];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x004F6398 File Offset: 0x004F4598
		private static void Step_Actuators(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.type == 849 && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
				{
					for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
					{
						Tile tile = Main.tile[i, j];
						if ((tile.wire() || tile.wire2() || tile.wire3() || tile.wire4()) && !tile.actuator() && tile.active())
						{
							SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
					{
						float num2 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num == -1f || num2 < num)
						{
							num = num2;
							tuple = SmartCursorHelper._targets[k];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x004F6548 File Offset: 0x004F4748
		private static void Step_EmptyBuckets(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.type == 205 && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
				{
					for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
					{
						Tile tile = Main.tile[i, j];
						if (tile.liquid > 0)
						{
							int num = (int)tile.liquidType();
							int num2 = 0;
							for (int k = i - 1; k <= i + 1; k++)
							{
								for (int l = j - 1; l <= j + 1; l++)
								{
									if ((int)Main.tile[k, l].liquidType() == num)
									{
										num2 += (int)Main.tile[k, l].liquid;
									}
								}
							}
							if (num2 > 100)
							{
								SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
							}
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num3 = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int m = 0; m < SmartCursorHelper._targets.Count; m++)
					{
						float num4 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[m].Item1, (float)SmartCursorHelper._targets[m].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num3 == -1f || num4 < num3)
						{
							num3 = num4;
							tuple = SmartCursorHelper._targets[m];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x004F6748 File Offset: 0x004F4948
		private static void Step_PaintScrapper(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (ItemID.Sets.IsPaintScraper[providedInfo.item.type] && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
				{
					for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
					{
						Tile tile = Main.tile[i, j];
						bool flag = false;
						if (tile.active())
						{
							flag |= (tile.color() > 0);
							flag |= (tile.type == 184);
							flag |= tile.fullbrightBlock();
							flag |= tile.invisibleBlock();
						}
						if (tile.wall > 0)
						{
							flag |= (tile.wallColor() > 0);
							flag |= tile.fullbrightWall();
							flag |= tile.invisibleWall();
						}
						if (flag)
						{
							SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
					{
						float num2 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num == -1f || num2 < num)
						{
							num = num2;
							tuple = SmartCursorHelper._targets[k];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x004F693C File Offset: 0x004F4B3C
		private static void Step_PaintBrush(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if ((providedInfo.item.type == 1071 || providedInfo.item.type == 1543) && (providedInfo.paintLookup != 0 || providedInfo.paintCoatingLookup != 0) && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				int paintLookup = providedInfo.paintLookup;
				int paintCoatingLookup = providedInfo.paintCoatingLookup;
				if (paintLookup != 0 || paintCoatingLookup != 0)
				{
					for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
					{
						for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
						{
							Tile tile = Main.tile[i, j];
							if (tile.active() && (false | (paintLookup != 0 && (int)tile.color() != paintLookup) | (paintCoatingLookup == 1 && !tile.fullbrightBlock()) | (paintCoatingLookup == 2 && !tile.invisibleBlock())))
							{
								SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
							}
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
					{
						float num2 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num == -1f || num2 < num)
						{
							num = num2;
							tuple = SmartCursorHelper._targets[k];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x004F6B44 File Offset: 0x004F4D44
		private static void Step_PaintRoller(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if ((providedInfo.item.type == 1072 || providedInfo.item.type == 1544) && (providedInfo.paintLookup != 0 || providedInfo.paintCoatingLookup != 0) && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				int paintLookup = providedInfo.paintLookup;
				int paintCoatingLookup = providedInfo.paintCoatingLookup;
				for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
				{
					for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
					{
						Tile tile = Main.tile[i, j];
						if (tile.wall > 0 && (!tile.active() || !Main.tileSolid[(int)tile.type] || Main.tileSolidTop[(int)tile.type]) && (false | (paintLookup != 0 && (int)tile.wallColor() != paintLookup) | (paintCoatingLookup == 1 && !tile.fullbrightWall()) | (paintCoatingLookup == 2 && !tile.invisibleWall())))
						{
							SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
					{
						float num2 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num == -1f || num2 < num)
						{
							num = num2;
							tuple = SmartCursorHelper._targets[k];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x004F6D74 File Offset: 0x004F4F74
		private static void Step_BlocksLines(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			int type = providedInfo.item.type;
			if (type < 0 || type >= (int)ItemID.Count)
			{
				return;
			}
			if (Player.SmartCursorSettings.SmartBlocksEnabled && providedInfo.item.createTile > -1 && type != 213 && type != 5295 && !ItemID.Sets.GrassSeeds[type] && Main.tileSolid[providedInfo.item.createTile] && !Main.tileSolidTop[providedInfo.item.createTile] && !Main.tileFrameImportant[providedInfo.item.createTile] && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				bool flag = false;
				if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].active())
				{
					flag = true;
				}
				if (!Collision.InTileBounds(providedInfo.screenTargetX, providedInfo.screenTargetY, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
				{
					flag = true;
				}
				if (!flag)
				{
					for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
					{
						for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
						{
							Tile tile = Main.tile[i, j];
							if (!tile.active() || Main.tileCut[(int)tile.type] || TileID.Sets.BreakableWhenPlacing[(int)tile.type])
							{
								bool flag2 = false;
								if (Main.tile[i - 1, j].active() && Main.tileSolid[(int)Main.tile[i - 1, j].type] && !Main.tileSolidTop[(int)Main.tile[i - 1, j].type])
								{
									flag2 = true;
								}
								if (Main.tile[i + 1, j].active() && Main.tileSolid[(int)Main.tile[i + 1, j].type] && !Main.tileSolidTop[(int)Main.tile[i + 1, j].type])
								{
									flag2 = true;
								}
								if (Main.tile[i, j - 1].active() && Main.tileSolid[(int)Main.tile[i, j - 1].type] && !Main.tileSolidTop[(int)Main.tile[i, j - 1].type])
								{
									flag2 = true;
								}
								if (Main.tile[i, j + 1].active() && Main.tileSolid[(int)Main.tile[i, j + 1].type] && !Main.tileSolidTop[(int)Main.tile[i, j + 1].type])
								{
									flag2 = true;
								}
								if (flag2)
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
								}
							}
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
					{
						if (Collision.EmptyTile(SmartCursorHelper._targets[k].Item1, SmartCursorHelper._targets[k].Item2, false))
						{
							float num2 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
							if (num == -1f || num2 < num)
							{
								num = num2;
								tuple = SmartCursorHelper._targets[k];
							}
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY) && num != -1f)
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x004F717C File Offset: 0x004F537C
		private static void Step_Boulders(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.createTile > -1 && providedInfo.item.createTile < (int)TileID.Count && TileID.Sets.Boulders[providedInfo.item.createTile] && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
				{
					for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
					{
						Tile tile = Main.tile[i, j + 1];
						Tile tile2 = Main.tile[i + 1, j + 1];
						bool flag = true;
						if (!tile2.nactive() || !tile.nactive())
						{
							flag = false;
						}
						if (tile2.slope() > 0 || tile.slope() > 0 || tile2.halfBrick() || tile.halfBrick())
						{
							flag = false;
						}
						if ((!Main.tileSolid[(int)tile2.type] && !Main.tileTable[(int)tile2.type]) || (!Main.tileSolid[(int)tile.type] && !Main.tileTable[(int)tile.type]))
						{
							flag = false;
						}
						if (Main.tileNoAttach[(int)tile2.type] || Main.tileNoAttach[(int)tile.type])
						{
							flag = false;
						}
						for (int k = i; k <= i + 1; k++)
						{
							for (int l = j - 1; l <= j; l++)
							{
								Tile tile3 = Main.tile[k, l];
								if (tile3.active() && !Main.tileCut[(int)tile3.type])
								{
									flag = false;
								}
							}
						}
						int x = i * 16;
						int y = j * 16 - 16;
						int width = 32;
						int height = 32;
						Rectangle value = new Rectangle(x, y, width, height);
						for (int m = 0; m < 255; m++)
						{
							Player player = Main.player[m];
							if (player.active && !player.dead && player.Hitbox.Intersects(value))
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int n = 0; n < SmartCursorHelper._targets.Count; n++)
					{
						float num2 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[n].Item1, (float)SmartCursorHelper._targets[n].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num == -1f || num2 < num)
						{
							num = num2;
							tuple = SmartCursorHelper._targets[n];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x004F74A0 File Offset: 0x004F56A0
		private static void Step_Pigronata(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.createTile == 454 && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
				{
					int num = providedInfo.reachableStartY;
					while (num <= providedInfo.reachableEndY && (double)num <= Main.worldSurface - 2.0)
					{
						bool flag = true;
						for (int j = i - 2; j <= i + 1; j++)
						{
							for (int k = num - 1; k <= num + 2; k++)
							{
								Tile tile = Main.tile[j, k];
								if (k == num - 1)
								{
									if (!WorldGen.SolidTile(tile))
									{
										flag = false;
									}
								}
								else if (tile.active() && (!Main.tileCut[(int)tile.type] || tile.type == 454))
								{
									flag = false;
								}
							}
						}
						if (flag)
						{
							SmartCursorHelper._targets.Add(new Tuple<int, int>(i, num));
						}
						num++;
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num2 = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int l = 0; l < SmartCursorHelper._targets.Count; l++)
					{
						float num3 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[l].Item1, (float)SmartCursorHelper._targets[l].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num2 == -1f || num3 < num2)
						{
							num2 = num3;
							tuple = SmartCursorHelper._targets[l];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x004F76B0 File Offset: 0x004F58B0
		private static void Step_PumpkinSeeds(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.createTile == 254 && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
				{
					for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
					{
						Tile tile = Main.tile[i, j + 1];
						Tile tile2 = Main.tile[i + 1, j + 1];
						if ((double)j > Main.worldSurface - 2.0)
						{
							break;
						}
						bool flag = true;
						if (!tile2.active() || !tile.active())
						{
							flag = false;
						}
						if (tile2.slope() > 0 || tile.slope() > 0 || tile2.halfBrick() || tile.halfBrick())
						{
							flag = false;
						}
						if (tile2.type != 2 && tile2.type != 477 && tile2.type != 109 && tile2.type != 492)
						{
							flag = false;
						}
						if (tile.type != 2 && tile.type != 477 && tile.type != 109 && tile.type != 492)
						{
							flag = false;
						}
						for (int k = i; k <= i + 1; k++)
						{
							for (int l = j - 1; l <= j; l++)
							{
								Tile tile3 = Main.tile[k, l];
								if (tile3.active() && (tile3.type < 0 || tile3.type >= TileID.Count || Main.tileSolid[(int)tile3.type] || !WorldGen.CanCutTile(k, l, TileCuttingContext.TilePlacement)))
								{
									flag = false;
								}
							}
						}
						if (flag)
						{
							SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int m = 0; m < SmartCursorHelper._targets.Count; m++)
					{
						float num2 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[m].Item1, (float)SmartCursorHelper._targets[m].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num == -1f || num2 < num)
						{
							num = num2;
							tuple = SmartCursorHelper._targets[m];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x004F797C File Offset: 0x004F5B7C
		private static void Step_Walls(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			int width = providedInfo.player.width;
			int height = providedInfo.player.height;
			if (providedInfo.item.createWall > 0 && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
				{
					for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
					{
						Tile tile = Main.tile[i, j];
						if (tile.wall == 0 && (!tile.active() || !Main.tileSolid[(int)tile.type] || Main.tileSolidTop[(int)tile.type]) && Collision.CanHitWithCheck(providedInfo.position, width, height, new Vector2((float)i, (float)j) * 16f, 16, 16, new Utils.TileActionAttempt(DelegateMethods.NotDoorStand)))
						{
							bool flag = false;
							if (Main.tile[i - 1, j].active() || Main.tile[i - 1, j].wall > 0)
							{
								flag = true;
							}
							if (Main.tile[i + 1, j].active() || Main.tile[i + 1, j].wall > 0)
							{
								flag = true;
							}
							if (Main.tile[i, j - 1].active() || Main.tile[i, j - 1].wall > 0)
							{
								flag = true;
							}
							if (Main.tile[i, j + 1].active() || Main.tile[i, j + 1].wall > 0)
							{
								flag = true;
							}
							if (WorldGen.IsOpenDoorAnchorFrame(i, j))
							{
								flag = false;
							}
							if (flag)
							{
								SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
							}
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
					{
						float num2 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num == -1f || num2 < num)
						{
							num = num2;
							tuple = SmartCursorHelper._targets[k];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x004F7C58 File Offset: 0x004F5E58
		private static void Step_MinecartTracks(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if ((providedInfo.item.type == 2340 || providedInfo.item.type == 2739) && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				Vector2 value = (Main.MouseWorld - providedInfo.Center).SafeNormalize(Vector2.UnitY);
				float num = Vector2.Dot(value, -Vector2.UnitY);
				bool flag = num >= 0.5f;
				bool flag2 = num <= -0.5f;
				float num2 = Vector2.Dot(value, Vector2.UnitX);
				bool flag3 = num2 >= 0.5f;
				bool flag4 = num2 <= -0.5f;
				bool flag5 = flag && flag4;
				bool flag6 = flag && flag3;
				bool flag7 = flag2 && flag4;
				bool flag8 = flag2 && flag3;
				if (flag5)
				{
					flag4 = false;
				}
				if (flag6)
				{
					flag3 = false;
				}
				if (flag7)
				{
					flag4 = false;
				}
				if (flag8)
				{
					flag3 = false;
				}
				bool flag9 = false;
				if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].active() && Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].type == 314)
				{
					flag9 = true;
				}
				if (!flag9)
				{
					for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
					{
						for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
						{
							Tile tile = Main.tile[i, j];
							if (tile.active() && tile.type == 314)
							{
								bool flag10 = Main.tile[i + 1, j + 1].active() && Main.tile[i + 1, j + 1].type == 314;
								bool flag11 = Main.tile[i + 1, j - 1].active() && Main.tile[i + 1, j - 1].type == 314;
								bool flag12 = Main.tile[i - 1, j + 1].active() && Main.tile[i - 1, j + 1].type == 314;
								bool flag13 = Main.tile[i - 1, j - 1].active() && Main.tile[i - 1, j - 1].type == 314;
								if (flag5 && (!Main.tile[i - 1, j - 1].active() || Main.tileCut[(int)Main.tile[i - 1, j - 1].type] || TileID.Sets.BreakableWhenPlacing[(int)Main.tile[i - 1, j - 1].type]) && (flag10 || !flag11) && !flag12)
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i - 1, j - 1));
								}
								if (flag4 && (!Main.tile[i - 1, j].active() || Main.tileCut[(int)Main.tile[i - 1, j].type] || TileID.Sets.BreakableWhenPlacing[(int)Main.tile[i - 1, j].type]))
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i - 1, j));
								}
								if (flag7 && (!Main.tile[i - 1, j + 1].active() || Main.tileCut[(int)Main.tile[i - 1, j + 1].type] || TileID.Sets.BreakableWhenPlacing[(int)Main.tile[i - 1, j + 1].type]) && (flag11 || !flag10) && !flag13)
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i - 1, j + 1));
								}
								if (flag6 && (!Main.tile[i + 1, j - 1].active() || Main.tileCut[(int)Main.tile[i + 1, j - 1].type] || TileID.Sets.BreakableWhenPlacing[(int)Main.tile[i + 1, j - 1].type]) && (flag12 || !flag13) && !flag10)
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i + 1, j - 1));
								}
								if (flag3 && (!Main.tile[i + 1, j].active() || Main.tileCut[(int)Main.tile[i + 1, j].type] || TileID.Sets.BreakableWhenPlacing[(int)Main.tile[i + 1, j].type]))
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i + 1, j));
								}
								if (flag8 && (!Main.tile[i + 1, j + 1].active() || Main.tileCut[(int)Main.tile[i + 1, j + 1].type] || TileID.Sets.BreakableWhenPlacing[(int)Main.tile[i + 1, j + 1].type]) && (flag13 || !flag12) && !flag11)
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i + 1, j + 1));
								}
							}
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num3 = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
					{
						if ((!Main.tile[SmartCursorHelper._targets[k].Item1, SmartCursorHelper._targets[k].Item2 - 1].active() || Main.tile[SmartCursorHelper._targets[k].Item1, SmartCursorHelper._targets[k].Item2 - 1].type != 314) && (!Main.tile[SmartCursorHelper._targets[k].Item1, SmartCursorHelper._targets[k].Item2 + 1].active() || Main.tile[SmartCursorHelper._targets[k].Item1, SmartCursorHelper._targets[k].Item2 + 1].type != 314))
						{
							float num4 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
							if (num3 == -1f || num4 < num3)
							{
								num3 = num4;
								tuple = SmartCursorHelper._targets[k];
							}
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY) && num3 != -1f)
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
			if (providedInfo.item.type == 2492 && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				bool flag14 = false;
				if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].active() && Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].type == 314)
				{
					flag14 = true;
				}
				if (!flag14)
				{
					for (int l = providedInfo.reachableStartX; l <= providedInfo.reachableEndX; l++)
					{
						for (int m = providedInfo.reachableStartY; m <= providedInfo.reachableEndY; m++)
						{
							Tile tile2 = Main.tile[l, m];
							if (tile2.active() && tile2.type == 314)
							{
								if (!Main.tile[l - 1, m].active() || Main.tileCut[(int)Main.tile[l - 1, m].type] || TileID.Sets.BreakableWhenPlacing[(int)Main.tile[l - 1, m].type])
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(l - 1, m));
								}
								if (!Main.tile[l + 1, m].active() || Main.tileCut[(int)Main.tile[l + 1, m].type] || TileID.Sets.BreakableWhenPlacing[(int)Main.tile[l + 1, m].type])
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(l + 1, m));
								}
							}
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num5 = -1f;
					Tuple<int, int> tuple2 = SmartCursorHelper._targets[0];
					for (int n = 0; n < SmartCursorHelper._targets.Count; n++)
					{
						if ((!Main.tile[SmartCursorHelper._targets[n].Item1, SmartCursorHelper._targets[n].Item2 - 1].active() || Main.tile[SmartCursorHelper._targets[n].Item1, SmartCursorHelper._targets[n].Item2 - 1].type != 314) && (!Main.tile[SmartCursorHelper._targets[n].Item1, SmartCursorHelper._targets[n].Item2 + 1].active() || Main.tile[SmartCursorHelper._targets[n].Item1, SmartCursorHelper._targets[n].Item2 + 1].type != 314))
						{
							float num6 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[n].Item1, (float)SmartCursorHelper._targets[n].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
							if (num5 == -1f || num6 < num5)
							{
								num5 = num6;
								tuple2 = SmartCursorHelper._targets[n];
							}
						}
					}
					if (Collision.InTileBounds(tuple2.Item1, tuple2.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY) && num5 != -1f)
					{
						focusedX = tuple2.Item1;
						focusedY = tuple2.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x004F8744 File Offset: 0x004F6944
		private static void Step_Platforms(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.createTile >= 0 && TileID.Sets.Platforms[providedInfo.item.createTile] && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				bool flag = false;
				if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].active() && TileID.Sets.Platforms[(int)Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].type])
				{
					flag = true;
				}
				if (!flag)
				{
					for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
					{
						for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
						{
							Tile tile = Main.tile[i, j];
							if (tile.active() && TileID.Sets.Platforms[(int)tile.type])
							{
								byte b = tile.slope();
								if (b != 2 && !Main.tile[i - 1, j - 1].active())
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i - 1, j - 1));
								}
								if (!Main.tile[i - 1, j].active())
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i - 1, j));
								}
								if (b != 1 && !Main.tile[i - 1, j + 1].active())
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i - 1, j + 1));
								}
								if (b != 1 && !Main.tile[i + 1, j - 1].active())
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i + 1, j - 1));
								}
								if (!Main.tile[i + 1, j].active())
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i + 1, j));
								}
								if (b != 2 && !Main.tile[i + 1, j + 1].active())
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i + 1, j + 1));
								}
							}
							if (!tile.active())
							{
								int num = 0;
								int num2 = 1;
								Tile tile2 = Main.tile[i + num, j + num2];
								if (tile2.active() && Main.tileSolid[(int)tile2.type] && !Main.tileSolidTop[(int)tile2.type])
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
								}
								num = -1;
								num2 = 0;
								tile2 = Main.tile[i + num, j + num2];
								if (tile2.active() && Main.tileSolid[(int)tile2.type] && !Main.tileSolidTop[(int)tile2.type])
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
								}
								num = 1;
								num2 = 0;
								tile2 = Main.tile[i + num, j + num2];
								if (tile2.active() && Main.tileSolid[(int)tile2.type] && !Main.tileSolidTop[(int)tile2.type])
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
								}
							}
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num3 = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
					{
						float num4 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num3 == -1f || num4 < num3)
						{
							num3 = num4;
							tuple = SmartCursorHelper._targets[k];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x004F8B58 File Offset: 0x004F6D58
		private static void Step_WireCutter(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.type == 510 && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
				{
					for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
					{
						Tile tile = Main.tile[i, j];
						if (tile.wire() || tile.wire2() || tile.wire3() || tile.wire4() || tile.actuator())
						{
							SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
					{
						float num2 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num == -1f || num2 < num)
						{
							num = num2;
							tuple = SmartCursorHelper._targets[k];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x004F8D00 File Offset: 0x004F6F00
		private static void Step_ActuationRod(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			bool actuationRodLock = providedInfo.player.ActuationRodLock;
			bool actuationRodLockSetting = providedInfo.player.ActuationRodLockSetting;
			if (providedInfo.item.type == 3620 && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
				{
					for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
					{
						Tile tile = Main.tile[i, j];
						if (tile.active() && tile.actuator() && (!actuationRodLock || actuationRodLockSetting == tile.inActive()))
						{
							SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
					{
						float num2 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num == -1f || num2 < num)
						{
							num = num2;
							tuple = SmartCursorHelper._targets[k];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x004F8EBC File Offset: 0x004F70BC
		private static void Step_Hammers(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			int width = providedInfo.player.width;
			int height = providedInfo.player.height;
			if (providedInfo.item.hammer > 0 && focusedX == -1 && focusedY == -1)
			{
				Vector2 vector = providedInfo.mouse - providedInfo.Center;
				int num = Math.Sign(vector.X);
				int num2 = Math.Sign(vector.Y);
				if (Math.Abs(vector.X) > Math.Abs(vector.Y) * 3f)
				{
					num2 = 0;
					providedInfo.mouse.Y = providedInfo.Center.Y;
				}
				if (Math.Abs(vector.Y) > Math.Abs(vector.X) * 3f)
				{
					num = 0;
					providedInfo.mouse.X = providedInfo.Center.X;
				}
				int num3 = (int)providedInfo.Center.X / 16;
				int num4 = (int)providedInfo.Center.Y / 16;
				SmartCursorHelper._points.Clear();
				SmartCursorHelper._endpoints.Clear();
				int num5 = 1;
				if (num2 == -1 && num != 0)
				{
					num5 = -1;
				}
				int num6 = (int)((providedInfo.position.X + (float)(width / 2) + (float)((width / 2 - 1) * num)) / 16f);
				int num7 = (int)(((double)providedInfo.position.Y + 0.1) / 16.0);
				if (num5 == -1)
				{
					num7 = (int)((providedInfo.position.Y + (float)height - 1f) / 16f);
				}
				int num8 = width / 16 + ((width % 16 == 0) ? 0 : 1);
				int num9 = height / 16 + ((height % 16 == 0) ? 0 : 1);
				if (num != 0)
				{
					for (int i = 0; i < num9; i++)
					{
						if (Main.tile[num6, num7 + i * num5] != null)
						{
							SmartCursorHelper._points.Add(new Tuple<int, int>(num6, num7 + i * num5));
						}
					}
				}
				if (num2 != 0)
				{
					for (int j = 0; j < num8; j++)
					{
						if (Main.tile[(int)(providedInfo.position.X / 16f) + j, num7] != null)
						{
							SmartCursorHelper._points.Add(new Tuple<int, int>((int)(providedInfo.position.X / 16f) + j, num7));
						}
					}
				}
				int num10 = (int)((providedInfo.mouse.X + (float)((width / 2 - 1) * num)) / 16f);
				int num11 = (int)(((double)providedInfo.mouse.Y + 0.1 - (double)(height / 2 + 1)) / 16.0);
				if (num5 == -1)
				{
					num11 = (int)((providedInfo.mouse.Y + (float)(height / 2) - 1f) / 16f);
				}
				if (providedInfo.player.gravDir == -1f && num2 == 0)
				{
					num11++;
				}
				if (num11 < 10)
				{
					num11 = 10;
				}
				if (num11 > Main.maxTilesY - 10)
				{
					num11 = Main.maxTilesY - 10;
				}
				int num12 = width / 16 + ((width % 16 == 0) ? 0 : 1);
				int num13 = height / 16 + ((height % 16 == 0) ? 0 : 1);
				if (num != 0)
				{
					for (int k = 0; k < num13; k++)
					{
						if (Main.tile[num10, num11 + k * num5] != null)
						{
							SmartCursorHelper._endpoints.Add(new Tuple<int, int>(num10, num11 + k * num5));
						}
					}
				}
				if (num2 != 0)
				{
					for (int l = 0; l < num12; l++)
					{
						if (Main.tile[(int)((providedInfo.mouse.X - (float)(width / 2)) / 16f) + l, num11] != null)
						{
							SmartCursorHelper._endpoints.Add(new Tuple<int, int>((int)((providedInfo.mouse.X - (float)(width / 2)) / 16f) + l, num11));
						}
					}
				}
				SmartCursorHelper._targets.Clear();
				while (SmartCursorHelper._points.Count > 0)
				{
					Tuple<int, int> tuple = SmartCursorHelper._points[0];
					Tuple<int, int> tuple2 = SmartCursorHelper._endpoints[0];
					Tuple<int, int> tuple3 = Collision.TupleHitLineWall(tuple.Item1, tuple.Item2, tuple2.Item1, tuple2.Item2);
					if (tuple3.Item1 == -1 || tuple3.Item2 == -1)
					{
						SmartCursorHelper._points.Remove(tuple);
						SmartCursorHelper._endpoints.Remove(tuple2);
					}
					else
					{
						if (tuple3.Item1 != tuple2.Item1 || tuple3.Item2 != tuple2.Item2)
						{
							SmartCursorHelper._targets.Add(tuple3);
						}
						Main.tile[tuple3.Item1, tuple3.Item2];
						if (Collision.HitWallSubstep(tuple3.Item1, tuple3.Item2))
						{
							SmartCursorHelper._targets.Add(tuple3);
						}
						SmartCursorHelper._points.Remove(tuple);
						SmartCursorHelper._endpoints.Remove(tuple2);
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num14 = -1f;
					Tuple<int, int> tuple4 = null;
					for (int m = 0; m < SmartCursorHelper._targets.Count; m++)
					{
						if (!Main.tile[SmartCursorHelper._targets[m].Item1, SmartCursorHelper._targets[m].Item2].active() || Main.tile[SmartCursorHelper._targets[m].Item1, SmartCursorHelper._targets[m].Item2].type != 26)
						{
							float num15 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[m].Item1, (float)SmartCursorHelper._targets[m].Item2) * 16f + Vector2.One * 8f, providedInfo.Center);
							if (num14 == -1f || num15 < num14)
							{
								num14 = num15;
								tuple4 = SmartCursorHelper._targets[m];
							}
						}
					}
					if (tuple4 != null && Collision.InTileBounds(tuple4.Item1, tuple4.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						providedInfo.player.poundRelease = false;
						focusedX = tuple4.Item1;
						focusedY = tuple4.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
				SmartCursorHelper._points.Clear();
				SmartCursorHelper._endpoints.Clear();
			}
			if (providedInfo.item.hammer > 0 && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				for (int n = providedInfo.reachableStartX; n <= providedInfo.reachableEndX; n++)
				{
					for (int num16 = providedInfo.reachableStartY; num16 <= providedInfo.reachableEndY; num16++)
					{
						if (Main.tile[n, num16].wall > 0 && Collision.HitWallSubstep(n, num16))
						{
							SmartCursorHelper._targets.Add(new Tuple<int, int>(n, num16));
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num17 = -1f;
					Tuple<int, int> tuple5 = null;
					for (int num18 = 0; num18 < SmartCursorHelper._targets.Count; num18++)
					{
						if (!Main.tile[SmartCursorHelper._targets[num18].Item1, SmartCursorHelper._targets[num18].Item2].active() || Main.tile[SmartCursorHelper._targets[num18].Item1, SmartCursorHelper._targets[num18].Item2].type != 26)
						{
							float num19 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[num18].Item1, (float)SmartCursorHelper._targets[num18].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
							if (num17 == -1f || num19 < num17)
							{
								num17 = num19;
								tuple5 = SmartCursorHelper._targets[num18];
							}
						}
					}
					if (tuple5 != null && Collision.InTileBounds(tuple5.Item1, tuple5.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						providedInfo.player.poundRelease = false;
						focusedX = tuple5.Item1;
						focusedY = tuple5.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x004F9710 File Offset: 0x004F7910
		private static void Step_MulticolorWrench(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.type == 3625 && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				WiresUI.Settings.MultiToolMode multiToolMode = WiresUI.Settings.ToolMode;
				WiresUI.Settings.MultiToolMode multiToolMode2 = (WiresUI.Settings.MultiToolMode)0;
				if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].wire())
				{
					multiToolMode2 |= WiresUI.Settings.MultiToolMode.Red;
				}
				if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].wire2())
				{
					multiToolMode2 |= WiresUI.Settings.MultiToolMode.Blue;
				}
				if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].wire3())
				{
					multiToolMode2 |= WiresUI.Settings.MultiToolMode.Green;
				}
				if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].wire4())
				{
					multiToolMode2 |= WiresUI.Settings.MultiToolMode.Yellow;
				}
				multiToolMode &= ~WiresUI.Settings.MultiToolMode.Cutter;
				bool flag = multiToolMode == multiToolMode2;
				multiToolMode = WiresUI.Settings.ToolMode;
				if (!flag)
				{
					bool flag2 = multiToolMode.HasFlag(WiresUI.Settings.MultiToolMode.Red);
					bool flag3 = multiToolMode.HasFlag(WiresUI.Settings.MultiToolMode.Blue);
					bool flag4 = multiToolMode.HasFlag(WiresUI.Settings.MultiToolMode.Green);
					bool flag5 = multiToolMode.HasFlag(WiresUI.Settings.MultiToolMode.Yellow);
					bool flag6 = multiToolMode.HasFlag(WiresUI.Settings.MultiToolMode.Cutter);
					for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
					{
						for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
						{
							Tile tile = Main.tile[i, j];
							if (flag6)
							{
								if ((tile.wire() && flag2) || (tile.wire2() && flag3) || (tile.wire3() && flag4) || (tile.wire4() && flag5))
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
								}
							}
							else if ((tile.wire() && flag2) || (tile.wire2() && flag3) || (tile.wire3() && flag4) || (tile.wire4() && flag5))
							{
								if (flag2)
								{
									if (!Main.tile[i - 1, j].wire())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i - 1, j));
									}
									if (!Main.tile[i + 1, j].wire())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i + 1, j));
									}
									if (!Main.tile[i, j - 1].wire())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j - 1));
									}
									if (!Main.tile[i, j + 1].wire())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j + 1));
									}
								}
								if (flag3)
								{
									if (!Main.tile[i - 1, j].wire2())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i - 1, j));
									}
									if (!Main.tile[i + 1, j].wire2())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i + 1, j));
									}
									if (!Main.tile[i, j - 1].wire2())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j - 1));
									}
									if (!Main.tile[i, j + 1].wire2())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j + 1));
									}
								}
								if (flag4)
								{
									if (!Main.tile[i - 1, j].wire3())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i - 1, j));
									}
									if (!Main.tile[i + 1, j].wire3())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i + 1, j));
									}
									if (!Main.tile[i, j - 1].wire3())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j - 1));
									}
									if (!Main.tile[i, j + 1].wire3())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j + 1));
									}
								}
								if (flag5)
								{
									if (!Main.tile[i - 1, j].wire4())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i - 1, j));
									}
									if (!Main.tile[i + 1, j].wire4())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i + 1, j));
									}
									if (!Main.tile[i, j - 1].wire4())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j - 1));
									}
									if (!Main.tile[i, j + 1].wire4())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j + 1));
									}
								}
							}
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
					{
						float num2 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num == -1f || num2 < num)
						{
							num = num2;
							tuple = SmartCursorHelper._targets[k];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x004F9CF0 File Offset: 0x004F7EF0
		private static void Step_ColoredWrenches(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if ((providedInfo.item.type == 509 || providedInfo.item.type == 850 || providedInfo.item.type == 851 || providedInfo.item.type == 3612) && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				int num = 0;
				if (providedInfo.item.type == 509)
				{
					num = 1;
				}
				if (providedInfo.item.type == 850)
				{
					num = 2;
				}
				if (providedInfo.item.type == 851)
				{
					num = 3;
				}
				if (providedInfo.item.type == 3612)
				{
					num = 4;
				}
				bool flag = false;
				if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].wire() && num == 1)
				{
					flag = true;
				}
				if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].wire2() && num == 2)
				{
					flag = true;
				}
				if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].wire3() && num == 3)
				{
					flag = true;
				}
				if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].wire4() && num == 4)
				{
					flag = true;
				}
				if (!flag)
				{
					for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
					{
						for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
						{
							Tile tile = Main.tile[i, j];
							if ((tile.wire() && num == 1) || (tile.wire2() && num == 2) || (tile.wire3() && num == 3) || (tile.wire4() && num == 4))
							{
								if (num == 1)
								{
									if (!Main.tile[i - 1, j].wire())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i - 1, j));
									}
									if (!Main.tile[i + 1, j].wire())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i + 1, j));
									}
									if (!Main.tile[i, j - 1].wire())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j - 1));
									}
									if (!Main.tile[i, j + 1].wire())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j + 1));
									}
								}
								if (num == 2)
								{
									if (!Main.tile[i - 1, j].wire2())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i - 1, j));
									}
									if (!Main.tile[i + 1, j].wire2())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i + 1, j));
									}
									if (!Main.tile[i, j - 1].wire2())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j - 1));
									}
									if (!Main.tile[i, j + 1].wire2())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j + 1));
									}
								}
								if (num == 3)
								{
									if (!Main.tile[i - 1, j].wire3())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i - 1, j));
									}
									if (!Main.tile[i + 1, j].wire3())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i + 1, j));
									}
									if (!Main.tile[i, j - 1].wire3())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j - 1));
									}
									if (!Main.tile[i, j + 1].wire3())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j + 1));
									}
								}
								if (num == 4)
								{
									if (!Main.tile[i - 1, j].wire4())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i - 1, j));
									}
									if (!Main.tile[i + 1, j].wire4())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i + 1, j));
									}
									if (!Main.tile[i, j - 1].wire4())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j - 1));
									}
									if (!Main.tile[i, j + 1].wire4())
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j + 1));
									}
								}
							}
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num2 = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
					{
						float num3 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num2 == -1f || num3 < num2)
						{
							num2 = num3;
							tuple = SmartCursorHelper._targets[k];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x004FA264 File Offset: 0x004F8464
		private static void Step_Acorns(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.type == 27 && focusedX == -1 && focusedY == -1 && providedInfo.reachableStartY > 20)
			{
				SmartCursorHelper._targets.Clear();
				for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
				{
					for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
					{
						Tile tile = Main.tile[i, j];
						Tile tile2 = Main.tile[i, j - 1];
						Tile tile3 = Main.tile[i, j + 1];
						Tile tile4 = Main.tile[i - 1, j];
						Tile tile5 = Main.tile[i + 1, j];
						Tile tile6 = Main.tile[i - 2, j];
						Tile tile7 = Main.tile[i + 2, j];
						Tile tile8 = Main.tile[i - 3, j];
						Tile tile9 = Main.tile[i + 3, j];
						if ((!tile.active() || Main.tileCut[(int)tile.type] || TileID.Sets.BreakableWhenPlacing[(int)tile.type]) && (!tile2.active() || Main.tileCut[(int)tile2.type] || TileID.Sets.BreakableWhenPlacing[(int)tile2.type]) && (!tile4.active() || !TileID.Sets.CommonSapling[(int)tile4.type]) && (!tile5.active() || !TileID.Sets.CommonSapling[(int)tile5.type]) && (!tile6.active() || !TileID.Sets.CommonSapling[(int)tile6.type]) && (!tile7.active() || !TileID.Sets.CommonSapling[(int)tile7.type]) && (!tile8.active() || !TileID.Sets.CommonSapling[(int)tile8.type]) && (!tile9.active() || !TileID.Sets.CommonSapling[(int)tile9.type]) && tile3.active() && WorldGen.SolidTile2(tile3))
						{
							ushort type = tile3.type;
							if (type <= 116)
							{
								if (type <= 53)
								{
									if (type != 2 && type != 23 && type != 53)
									{
										goto IL_30F;
									}
								}
								else if (type <= 109)
								{
									if (type != 60)
									{
										if (type != 109)
										{
											goto IL_30F;
										}
									}
									else
									{
										if (WorldGen.EmptyTileCheck(i - 2, i + 2, j - 20, j, 20))
										{
											SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
											goto IL_30F;
										}
										goto IL_30F;
									}
								}
								else if (type != 112 && type != 116)
								{
									goto IL_30F;
								}
							}
							else if (type <= 234)
							{
								if (type != 147 && type != 199 && type != 234)
								{
									goto IL_30F;
								}
							}
							else if (type <= 492)
							{
								if (type != 477 && type != 492)
								{
									goto IL_30F;
								}
							}
							else if (type != 633 && type - 661 > 1)
							{
								goto IL_30F;
							}
							if (tile4.liquid == 0 && tile.liquid == 0 && tile5.liquid == 0 && WorldGen.EmptyTileCheck(i - 2, i + 2, j - 20, j, 20))
							{
								SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
							}
						}
						IL_30F:;
					}
				}
				SmartCursorHelper._toRemove.Clear();
				for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
				{
					bool flag = false;
					for (int l = -1; l < 2; l += 2)
					{
						Tile tile10 = Main.tile[SmartCursorHelper._targets[k].Item1 + l, SmartCursorHelper._targets[k].Item2 + 1];
						if (tile10.active())
						{
							ushort type = tile10.type;
							if (type <= 116)
							{
								if (type <= 53)
								{
									if (type != 2 && type != 23 && type != 53)
									{
										goto IL_435;
									}
								}
								else if (type <= 109)
								{
									if (type != 60 && type != 109)
									{
										goto IL_435;
									}
								}
								else if (type != 112 && type != 116)
								{
									goto IL_435;
								}
							}
							else if (type <= 234)
							{
								if (type != 147 && type != 199 && type != 234)
								{
									goto IL_435;
								}
							}
							else if (type <= 492)
							{
								if (type != 477 && type != 492)
								{
									goto IL_435;
								}
							}
							else if (type != 633 && type - 661 > 1)
							{
								goto IL_435;
							}
							flag = true;
						}
						IL_435:;
					}
					if (!flag)
					{
						SmartCursorHelper._toRemove.Add(SmartCursorHelper._targets[k]);
					}
				}
				for (int m = 0; m < SmartCursorHelper._toRemove.Count; m++)
				{
					SmartCursorHelper._targets.Remove(SmartCursorHelper._toRemove[m]);
				}
				SmartCursorHelper._toRemove.Clear();
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int n = 0; n < SmartCursorHelper._targets.Count; n++)
					{
						float num2 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[n].Item1, (float)SmartCursorHelper._targets[n].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num == -1f || num2 < num)
						{
							num = num2;
							tuple = SmartCursorHelper._targets[n];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x004FA820 File Offset: 0x004F8A20
		private static void Step_GemCorns(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			WorldGen.GrowTreeSettings growTreeSettings;
			if (!WorldGen.GrowTreeSettings.Profiles.TryGetFromItemId(providedInfo.item.type, out growTreeSettings))
			{
				return;
			}
			if (focusedX == -1 && focusedY == -1 && providedInfo.reachableStartY > 20)
			{
				SmartCursorHelper._targets.Clear();
				for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
				{
					for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
					{
						Tile tile = Main.tile[i, j];
						Tile tile2 = Main.tile[i, j - 1];
						Tile tile3 = Main.tile[i, j + 1];
						Tile tile4 = Main.tile[i - 1, j];
						Tile tile5 = Main.tile[i + 1, j];
						Tile tile6 = Main.tile[i - 2, j];
						Tile tile7 = Main.tile[i + 2, j];
						Tile tile8 = Main.tile[i - 3, j];
						Tile tile9 = Main.tile[i + 3, j];
						if (growTreeSettings.GroundTest((int)tile3.type) && (!tile.active() || Main.tileCut[(int)tile.type] || TileID.Sets.BreakableWhenPlacing[(int)tile.type]) && (!tile2.active() || Main.tileCut[(int)tile2.type] || TileID.Sets.BreakableWhenPlacing[(int)tile2.type]) && (!tile4.active() || !TileID.Sets.CommonSapling[(int)tile4.type]) && (!tile5.active() || !TileID.Sets.CommonSapling[(int)tile5.type]) && (!tile6.active() || !TileID.Sets.CommonSapling[(int)tile6.type]) && (!tile7.active() || !TileID.Sets.CommonSapling[(int)tile7.type]) && (!tile8.active() || !TileID.Sets.CommonSapling[(int)tile8.type]) && (!tile9.active() || !TileID.Sets.CommonSapling[(int)tile9.type]) && tile3.active() && WorldGen.SolidTile2(tile3) && tile4.liquid == 0 && tile.liquid == 0 && tile5.liquid == 0 && WorldGen.EmptyTileCheck(i - 2, i + 2, j - growTreeSettings.TreeHeightMax, j, (int)growTreeSettings.SaplingTileType))
						{
							SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
						}
					}
				}
				SmartCursorHelper._toRemove.Clear();
				for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
				{
					bool flag = false;
					for (int l = -1; l < 2; l += 2)
					{
						Tile tile10 = Main.tile[SmartCursorHelper._targets[k].Item1 + l, SmartCursorHelper._targets[k].Item2 + 1];
						if (tile10.active() && growTreeSettings.GroundTest((int)tile10.type))
						{
							flag = true;
						}
					}
					if (!flag)
					{
						SmartCursorHelper._toRemove.Add(SmartCursorHelper._targets[k]);
					}
				}
				for (int m = 0; m < SmartCursorHelper._toRemove.Count; m++)
				{
					SmartCursorHelper._targets.Remove(SmartCursorHelper._toRemove[m]);
				}
				SmartCursorHelper._toRemove.Clear();
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int n = 0; n < SmartCursorHelper._targets.Count; n++)
					{
						float num2 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[n].Item1, (float)SmartCursorHelper._targets[n].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num == -1f || num2 < num)
						{
							num = num2;
							tuple = SmartCursorHelper._targets[n];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						focusedX = tuple.Item1;
						focusedY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x004FAC6C File Offset: 0x004F8E6C
		private static void Step_ForceCursorToAnyMinableThing(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int fX, ref int fY)
		{
			int reachableStartX = providedInfo.reachableStartX;
			int reachableStartY = providedInfo.reachableStartY;
			int reachableEndX = providedInfo.reachableEndX;
			int reachableEndY = providedInfo.reachableEndY;
			int screenTargetX = providedInfo.screenTargetX;
			int screenTargetY = providedInfo.screenTargetY;
			Vector2 mouse = providedInfo.mouse;
			Item item = providedInfo.item;
			if (fX != -1 || fY != -1)
			{
				return;
			}
			if (PlayerInput.UsingGamepad)
			{
				return;
			}
			Point point = mouse.ToTileCoordinates();
			int x = point.X;
			int y = point.Y;
			if (!Collision.InTileBounds(x, y, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
			{
				return;
			}
			Tile tile = Main.tile[x, y];
			bool flag = tile.active() && WorldGen.CanKillTile(x, y) && (!Main.tileSolid[(int)tile.type] || Main.tileSolidTop[(int)tile.type]);
			if (flag && Main.tileAxe[(int)tile.type] && item.axe < 1)
			{
				flag = false;
			}
			if (flag && Main.tileHammer[(int)tile.type] && item.hammer < 1)
			{
				flag = false;
			}
			if (flag && !Main.tileHammer[(int)tile.type] && !Main.tileAxe[(int)tile.type] && item.pick < 1)
			{
				flag = false;
			}
			if (!flag)
			{
				return;
			}
			fX = x;
			fY = y;
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x004FADB0 File Offset: 0x004F8FB0
		private static void Step_Pickaxe_MineShinies(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int fX, ref int fY)
		{
			int reachableStartX = providedInfo.reachableStartX;
			int reachableStartY = providedInfo.reachableStartY;
			int reachableEndX = providedInfo.reachableEndX;
			int reachableEndY = providedInfo.reachableEndY;
			int screenTargetX = providedInfo.screenTargetX;
			int screenTargetY = providedInfo.screenTargetY;
			Item item = providedInfo.item;
			Vector2 mouse = providedInfo.mouse;
			if (item.pick <= 0 || fX != -1 || fY != -1)
			{
				return;
			}
			SmartCursorHelper._targets.Clear();
			if (item.type != 1333 && item.type != 523)
			{
				bool flag = item.type != 4384;
			}
			int num = 0;
			for (int i = reachableStartX; i <= reachableEndX; i++)
			{
				for (int j = reachableStartY; j <= reachableEndY; j++)
				{
					Tile tile = Main.tile[i, j];
					Main.tile[i - 1, j];
					Main.tile[i + 1, j];
					Main.tile[i, j + 1];
					if (tile.active())
					{
						int num2 = TileID.Sets.SmartCursorPickaxePriorityOverride[(int)tile.type];
						if (num2 > 0)
						{
							if (num < num2)
							{
								num = num2;
							}
							SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
						}
					}
				}
			}
			SmartCursorHelper._targets2.Clear();
			foreach (Tuple<int, int> tuple in SmartCursorHelper._targets2)
			{
				Tile tile2 = Main.tile[tuple.Item1, tuple.Item2];
				if (TileID.Sets.SmartCursorPickaxePriorityOverride[(int)tile2.type] < num)
				{
					SmartCursorHelper._targets2.Add(tuple);
				}
			}
			foreach (Tuple<int, int> item2 in SmartCursorHelper._targets2)
			{
				SmartCursorHelper._targets.Remove(item2);
			}
			if (SmartCursorHelper._targets.Count > 0)
			{
				float num3 = -1f;
				Tuple<int, int> tuple2 = SmartCursorHelper._targets[0];
				for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
				{
					float num4 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, mouse);
					if (num3 == -1f || num4 < num3)
					{
						num3 = num4;
						tuple2 = SmartCursorHelper._targets[k];
					}
				}
				if (Collision.InTileBounds(tuple2.Item1, tuple2.Item2, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
				{
					fX = tuple2.Item1;
					fY = tuple2.Item2;
				}
			}
			SmartCursorHelper._targets.Clear();
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x004FB0A8 File Offset: 0x004F92A8
		private static void Step_Pickaxe_MineSolids(Player player, SmartCursorHelper.SmartCursorUsageInfo providedInfo, List<Tuple<int, int>> grappleTargets, ref int focusedX, ref int focusedY)
		{
			int width = player.width;
			int height = player.height;
			int direction = player.direction;
			Vector2 center = player.Center;
			Vector2 position = player.position;
			float gravDir = player.gravDir;
			int whoAmI = player.whoAmI;
			if (providedInfo.item.pick > 0 && focusedX == -1 && focusedY == -1)
			{
				if (PlayerInput.UsingGamepad)
				{
					Vector2 navigatorDirections = PlayerInput.Triggers.Current.GetNavigatorDirections();
					Vector2 gamepadThumbstickLeft = PlayerInput.GamepadThumbstickLeft;
					Vector2 gamepadThumbstickRight = PlayerInput.GamepadThumbstickRight;
					if (navigatorDirections == Vector2.Zero && gamepadThumbstickLeft.Length() < 0.05f && gamepadThumbstickRight.Length() < 0.05f)
					{
						providedInfo.mouse = center + new Vector2((float)(direction * 1000), 0f);
					}
				}
				Vector2 vector = providedInfo.mouse - center;
				int num = Math.Sign(vector.X);
				int num2 = Math.Sign(vector.Y);
				if (Math.Abs(vector.X) > Math.Abs(vector.Y) * 3f)
				{
					num2 = 0;
					providedInfo.mouse.Y = center.Y;
				}
				if (Math.Abs(vector.Y) > Math.Abs(vector.X) * 3f)
				{
					num = 0;
					providedInfo.mouse.X = center.X;
				}
				int num3 = (int)center.X / 16;
				int num4 = (int)center.Y / 16;
				SmartCursorHelper._points.Clear();
				SmartCursorHelper._endpoints.Clear();
				int num5 = 1;
				if (num2 == -1 && num != 0)
				{
					num5 = -1;
				}
				int num6 = (int)((position.X + (float)(width / 2) + (float)((width / 2 - 1) * num)) / 16f);
				int num7 = (int)(((double)position.Y + 0.1) / 16.0);
				if (num5 == -1)
				{
					num7 = (int)((position.Y + (float)height - 1f) / 16f);
				}
				int num8 = width / 16 + ((width % 16 == 0) ? 0 : 1);
				int num9 = height / 16 + ((height % 16 == 0) ? 0 : 1);
				if (num != 0)
				{
					for (int i = 0; i < num9; i++)
					{
						if (Main.tile[num6, num7 + i * num5] != null)
						{
							SmartCursorHelper._points.Add(new Tuple<int, int>(num6, num7 + i * num5));
						}
					}
				}
				if (num2 != 0)
				{
					for (int j = 0; j < num8; j++)
					{
						if (Main.tile[(int)(position.X / 16f) + j, num7] != null)
						{
							SmartCursorHelper._points.Add(new Tuple<int, int>((int)(position.X / 16f) + j, num7));
						}
					}
				}
				int num10 = (int)((providedInfo.mouse.X + (float)((width / 2 - 1) * num)) / 16f);
				int num11 = (int)(((double)providedInfo.mouse.Y + 0.1 - (double)(height / 2 + 1)) / 16.0);
				if (num5 == -1)
				{
					num11 = (int)((providedInfo.mouse.Y + (float)(height / 2) - 1f) / 16f);
				}
				if (gravDir == -1f && num2 == 0)
				{
					num11++;
				}
				if (num11 < 10)
				{
					num11 = 10;
				}
				if (num11 > Main.maxTilesY - 10)
				{
					num11 = Main.maxTilesY - 10;
				}
				int num12 = width / 16 + ((width % 16 == 0) ? 0 : 1);
				int num13 = height / 16 + ((height % 16 == 0) ? 0 : 1);
				if (WorldGen.InWorld(num10, num11, 40))
				{
					if (num != 0)
					{
						for (int k = 0; k < num13; k++)
						{
							if (Main.tile[num10, num11 + k * num5] != null)
							{
								SmartCursorHelper._endpoints.Add(new Tuple<int, int>(num10, num11 + k * num5));
							}
						}
					}
					if (num2 != 0)
					{
						for (int l = 0; l < num12; l++)
						{
							if (Main.tile[(int)((providedInfo.mouse.X - (float)(width / 2)) / 16f) + l, num11] != null)
							{
								SmartCursorHelper._endpoints.Add(new Tuple<int, int>((int)((providedInfo.mouse.X - (float)(width / 2)) / 16f) + l, num11));
							}
						}
					}
				}
				SmartCursorHelper._targets.Clear();
				while (SmartCursorHelper._points.Count > 0 && SmartCursorHelper._endpoints.Count > 0)
				{
					Tuple<int, int> tuple = SmartCursorHelper._points[0];
					Tuple<int, int> tuple2 = SmartCursorHelper._endpoints[0];
					Tuple<int, int> tuple3;
					if (!Collision.TupleHitLine(tuple.Item1, tuple.Item2, tuple2.Item1, tuple2.Item2, num * (int)gravDir, -num2 * (int)gravDir, grappleTargets, out tuple3))
					{
						SmartCursorHelper._points.Remove(tuple);
						SmartCursorHelper._endpoints.Remove(tuple2);
					}
					else
					{
						if (tuple3.Item1 != tuple2.Item1 || tuple3.Item2 != tuple2.Item2)
						{
							SmartCursorHelper._targets.Add(tuple3);
						}
						Tile tile = Main.tile[tuple3.Item1, tuple3.Item2];
						if (!tile.inActive() && tile.active() && Main.tileSolid[(int)tile.type] && !Main.tileSolidTop[(int)tile.type] && !grappleTargets.Contains(tuple3))
						{
							SmartCursorHelper._targets.Add(tuple3);
						}
						SmartCursorHelper._points.Remove(tuple);
						SmartCursorHelper._endpoints.Remove(tuple2);
					}
				}
				SmartCursorHelper._toRemove.Clear();
				for (int m = 0; m < SmartCursorHelper._targets.Count; m++)
				{
					if (!WorldGen.CanKillTile(SmartCursorHelper._targets[m].Item1, SmartCursorHelper._targets[m].Item2))
					{
						SmartCursorHelper._toRemove.Add(SmartCursorHelper._targets[m]);
					}
				}
				for (int n = 0; n < SmartCursorHelper._toRemove.Count; n++)
				{
					SmartCursorHelper._targets.Remove(SmartCursorHelper._toRemove[n]);
				}
				SmartCursorHelper._toRemove.Clear();
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num14 = -1f;
					Tuple<int, int> tuple4 = SmartCursorHelper._targets[0];
					Vector2 value = center;
					if (Main.netMode == 1)
					{
						int num15 = 0;
						int num16 = 0;
						int num17 = 0;
						for (int num18 = 0; num18 < whoAmI; num18++)
						{
							Player player2 = Main.player[num18];
							if (player2.active && !player2.dead && player2.HeldItem.pick > 0 && player2.itemAnimation > 0)
							{
								if (player.Distance(player2.Center) <= 8f)
								{
									num15++;
								}
								if (player.Distance(player2.Center) <= 80f && Math.Abs(player2.Center.Y - center.Y) <= 12f)
								{
									num16++;
								}
							}
						}
						for (int num19 = whoAmI + 1; num19 < 255; num19++)
						{
							Player player3 = Main.player[num19];
							if (player3.active && !player3.dead && player3.HeldItem.pick > 0 && player3.itemAnimation > 0 && player.Distance(player3.Center) <= 8f)
							{
								num17++;
							}
						}
						if (num15 > 0)
						{
							if (num15 % 2 == 1)
							{
								value.X += 12f;
							}
							else
							{
								value.X -= 12f;
							}
							if (num16 % 2 == 1)
							{
								value.Y -= 12f;
							}
						}
						if (num17 > 0 && num15 == 0)
						{
							if (num17 % 2 == 1)
							{
								value.X -= 12f;
							}
							else
							{
								value.X += 12f;
							}
						}
					}
					for (int num20 = 0; num20 < SmartCursorHelper._targets.Count; num20++)
					{
						float num21 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[num20].Item1, (float)SmartCursorHelper._targets[num20].Item2) * 16f + Vector2.One * 8f, value);
						if (num14 == -1f || num21 < num14)
						{
							num14 = num21;
							tuple4 = SmartCursorHelper._targets[num20];
						}
					}
					if (Collision.InTileBounds(tuple4.Item1, tuple4.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
					{
						focusedX = tuple4.Item1;
						focusedY = tuple4.Item2;
					}
				}
				SmartCursorHelper._points.Clear();
				SmartCursorHelper._endpoints.Clear();
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x004FB954 File Offset: 0x004F9B54
		private static void Step_Axe(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int fX, ref int fY)
		{
			int reachableStartX = providedInfo.reachableStartX;
			int reachableStartY = providedInfo.reachableStartY;
			int reachableEndX = providedInfo.reachableEndX;
			int reachableEndY = providedInfo.reachableEndY;
			int screenTargetX = providedInfo.screenTargetX;
			int screenTargetY = providedInfo.screenTargetY;
			if (providedInfo.item.axe > 0 && fX == -1 && fY == -1)
			{
				float num = -1f;
				for (int i = reachableStartX; i <= reachableEndX; i++)
				{
					for (int j = reachableStartY; j <= reachableEndY; j++)
					{
						if (Main.tile[i, j].active())
						{
							Tile tile = Main.tile[i, j];
							if (Main.tileAxe[(int)tile.type] && !TileID.Sets.IgnoreSmartCursorPriorityAxe[(int)tile.type])
							{
								int num2 = i;
								int num3 = j;
								int type = (int)tile.type;
								if (TileID.Sets.IsATreeTrunk[type])
								{
									if (Collision.InTileBounds(num2 + 1, num3, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
									{
										if (Main.tile[num2, num3].frameY >= 198 && Main.tile[num2, num3].frameX == 44)
										{
											num2++;
										}
										if (Main.tile[num2, num3].frameX == 66 && Main.tile[num2, num3].frameY <= 44)
										{
											num2++;
										}
										if (Main.tile[num2, num3].frameX == 44 && Main.tile[num2, num3].frameY >= 132 && Main.tile[num2, num3].frameY <= 176)
										{
											num2++;
										}
									}
									if (Collision.InTileBounds(num2 - 1, num3, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
									{
										if (Main.tile[num2, num3].frameY >= 198 && Main.tile[num2, num3].frameX == 66)
										{
											num2--;
										}
										if (Main.tile[num2, num3].frameX == 88 && Main.tile[num2, num3].frameY >= 66 && Main.tile[num2, num3].frameY <= 110)
										{
											num2--;
										}
										if (Main.tile[num2, num3].frameX == 22 && Main.tile[num2, num3].frameY >= 132 && Main.tile[num2, num3].frameY <= 176)
										{
											num2--;
										}
									}
									while (Main.tile[num2, num3].active() && (int)Main.tile[num2, num3].type == type && (int)Main.tile[num2, num3 + 1].type == type && Collision.InTileBounds(num2, num3 + 1, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
									{
										num3++;
									}
								}
								if (tile.type == 80)
								{
									if (Collision.InTileBounds(num2 + 1, num3, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
									{
										if (Main.tile[num2, num3].frameX == 54)
										{
											num2++;
										}
										if (Main.tile[num2, num3].frameX == 108 && Main.tile[num2, num3].frameY == 36)
										{
											num2++;
										}
									}
									if (Collision.InTileBounds(num2 - 1, num3, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
									{
										if (Main.tile[num2, num3].frameX == 36)
										{
											num2--;
										}
										if (Main.tile[num2, num3].frameX == 108 && Main.tile[num2, num3].frameY == 18)
										{
											num2--;
										}
									}
									while (Main.tile[num2, num3].active() && Main.tile[num2, num3].type == 80 && Main.tile[num2, num3 + 1].type == 80 && Collision.InTileBounds(num2, num3 + 1, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
									{
										num3++;
									}
								}
								if (tile.type != 323)
								{
									if (tile.type != 72)
									{
										goto IL_4C4;
									}
								}
								while (Main.tile[num2, num3].active() && ((Main.tile[num2, num3].type == 323 && Main.tile[num2, num3 + 1].type == 323) || (Main.tile[num2, num3].type == 72 && Main.tile[num2, num3 + 1].type == 72)) && Collision.InTileBounds(num2, num3 + 1, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
								{
									num3++;
								}
								IL_4C4:
								float num4 = Vector2.Distance(new Vector2((float)num2, (float)num3) * 16f + Vector2.One * 8f, providedInfo.mouse);
								if (num == -1f || num4 < num)
								{
									num = num4;
									fX = num2;
									fY = num3;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x004FBE94 File Offset: 0x004FA094
		private static void Step_BlocksFilling(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int fX, ref int fY)
		{
			if (!Player.SmartCursorSettings.SmartBlocksEnabled)
			{
				return;
			}
			int type = providedInfo.item.type;
			if (type < 0 || type >= (int)ItemID.Count)
			{
				return;
			}
			int reachableStartX = providedInfo.reachableStartX;
			int reachableStartY = providedInfo.reachableStartY;
			int reachableEndX = providedInfo.reachableEndX;
			int reachableEndY = providedInfo.reachableEndY;
			int screenTargetX = providedInfo.screenTargetX;
			int screenTargetY = providedInfo.screenTargetY;
			if (!Player.SmartCursorSettings.SmartBlocksEnabled && providedInfo.item.createTile > -1 && type != 213 && type != 5295 && !ItemID.Sets.GrassSeeds[type] && Main.tileSolid[providedInfo.item.createTile] && !Main.tileSolidTop[providedInfo.item.createTile] && !Main.tileFrameImportant[providedInfo.item.createTile] && fX == -1 && fY == -1)
			{
				SmartCursorHelper._targets.Clear();
				bool flag = false;
				if (Main.tile[screenTargetX, screenTargetY].active())
				{
					flag = true;
				}
				if (!Collision.InTileBounds(screenTargetX, screenTargetY, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
				{
					flag = true;
				}
				if (!flag)
				{
					for (int i = reachableStartX; i <= reachableEndX; i++)
					{
						for (int j = reachableStartY; j <= reachableEndY; j++)
						{
							Tile tile = Main.tile[i, j];
							if (!tile.active() || Main.tileCut[(int)tile.type] || TileID.Sets.BreakableWhenPlacing[(int)tile.type])
							{
								int num = 0;
								if (Main.tile[i - 1, j].active() && Main.tileSolid[(int)Main.tile[i - 1, j].type] && !Main.tileSolidTop[(int)Main.tile[i - 1, j].type])
								{
									num++;
								}
								if (Main.tile[i + 1, j].active() && Main.tileSolid[(int)Main.tile[i + 1, j].type] && !Main.tileSolidTop[(int)Main.tile[i + 1, j].type])
								{
									num++;
								}
								if (Main.tile[i, j - 1].active() && Main.tileSolid[(int)Main.tile[i, j - 1].type] && !Main.tileSolidTop[(int)Main.tile[i, j - 1].type])
								{
									num++;
								}
								if (Main.tile[i, j + 1].active() && Main.tileSolid[(int)Main.tile[i, j + 1].type] && !Main.tileSolidTop[(int)Main.tile[i, j + 1].type])
								{
									num++;
								}
								if (num >= 2)
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
								}
							}
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num2 = -1f;
					float num3 = float.PositiveInfinity;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
					{
						if (Collision.EmptyTile(SmartCursorHelper._targets[k].Item1, SmartCursorHelper._targets[k].Item2, true))
						{
							Vector2 vector = new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f - providedInfo.mouse;
							bool flag2 = false;
							float num4 = Math.Abs(vector.X);
							float num5 = vector.Length();
							if (num4 < num3)
							{
								flag2 = true;
							}
							if (num4 == num3 && (num2 == -1f || num5 < num2))
							{
								flag2 = true;
							}
							if (flag2)
							{
								num2 = num5;
								num3 = num4;
								tuple = SmartCursorHelper._targets[k];
							}
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, reachableStartX, reachableStartY, reachableEndX, reachableEndY) && num2 != -1f)
					{
						fX = tuple.Item1;
						fY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x004FC2FC File Offset: 0x004FA4FC
		private static void Step_Torch(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int fX, ref int fY)
		{
			int reachableStartX = providedInfo.reachableStartX;
			int reachableStartY = providedInfo.reachableStartY;
			int reachableEndX = providedInfo.reachableEndX;
			int reachableEndY = providedInfo.reachableEndY;
			int screenTargetX = providedInfo.screenTargetX;
			int screenTargetY = providedInfo.screenTargetY;
			if (providedInfo.item.createTile == 4 && fX == -1 && fY == -1)
			{
				SmartCursorHelper._targets.Clear();
				bool flag = providedInfo.item.type != 1333 && providedInfo.item.type != 523 && providedInfo.item.type != 4384;
				for (int i = reachableStartX; i <= reachableEndX; i++)
				{
					for (int j = reachableStartY; j <= reachableEndY; j++)
					{
						Tile tile = Main.tile[i, j];
						Tile tile2 = Main.tile[i - 1, j];
						Tile tile3 = Main.tile[i + 1, j];
						Tile tile4 = Main.tile[i, j + 1];
						if (!tile.active() || TileID.Sets.BreakableWhenPlacing[(int)tile.type] || (Main.tileCut[(int)tile.type] && tile.type != 82 && tile.type != 83))
						{
							bool flag2 = false;
							for (int k = i - 8; k <= i + 8; k++)
							{
								for (int l = j - 8; l <= j + 8; l++)
								{
									if (Main.tile[k, l] != null && Main.tile[k, l].type == 4)
									{
										flag2 = true;
										break;
									}
								}
								if (flag2)
								{
									break;
								}
							}
							if (!flag2 && (!flag || tile.liquid <= 0) && (tile.wall > 0 || (tile2.active() && (tile2.slope() == 0 || tile2.slope() % 2 != 1) && ((Main.tileSolid[(int)tile2.type] && !Main.tileNoAttach[(int)tile2.type] && !Main.tileSolidTop[(int)tile2.type] && !TileID.Sets.NotReallySolid[(int)tile2.type]) || TileID.Sets.IsBeam[(int)tile2.type] || (WorldGen.IsTreeType((int)tile2.type) && WorldGen.IsTreeType((int)Main.tile[i - 1, j - 1].type) && WorldGen.IsTreeType((int)Main.tile[i - 1, j + 1].type)))) || (tile3.active() && (tile3.slope() == 0 || tile3.slope() % 2 != 0) && ((Main.tileSolid[(int)tile3.type] && !Main.tileNoAttach[(int)tile3.type] && !Main.tileSolidTop[(int)tile3.type] && !TileID.Sets.NotReallySolid[(int)tile3.type]) || TileID.Sets.IsBeam[(int)tile3.type] || (WorldGen.IsTreeType((int)tile3.type) && WorldGen.IsTreeType((int)Main.tile[i + 1, j - 1].type) && WorldGen.IsTreeType((int)Main.tile[i + 1, j + 1].type)))) || (tile4.active() && Main.tileSolid[(int)tile4.type] && !Main.tileNoAttach[(int)tile4.type] && (!Main.tileSolidTop[(int)tile4.type] || (TileID.Sets.Platforms[(int)tile4.type] && tile4.slope() == 0)) && !TileID.Sets.NotReallySolid[(int)tile4.type] && !tile4.halfBrick() && tile4.slope() == 0)) && tile.type != 4)
							{
								SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
							}
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int m = 0; m < SmartCursorHelper._targets.Count; m++)
					{
						float num2 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[m].Item1, (float)SmartCursorHelper._targets[m].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num == -1f || num2 < num)
						{
							num = num2;
							tuple = SmartCursorHelper._targets[m];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
					{
						fX = tuple.Item1;
						fY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x004FC7D0 File Offset: 0x004FA9D0
		private static void Step_LawnMower(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int fX, ref int fY)
		{
			int reachableStartX = providedInfo.reachableStartX;
			int reachableStartY = providedInfo.reachableStartY;
			int reachableEndX = providedInfo.reachableEndX;
			int reachableEndY = providedInfo.reachableEndY;
			int screenTargetX = providedInfo.screenTargetX;
			int screenTargetY = providedInfo.screenTargetY;
			if (providedInfo.item.type == 4049 && fX == -1 && fY == -1)
			{
				SmartCursorHelper._targets.Clear();
				for (int i = reachableStartX; i <= reachableEndX; i++)
				{
					for (int j = reachableStartY; j <= reachableEndY; j++)
					{
						Tile tile = Main.tile[i, j];
						if (tile.active() && (tile.type == 2 || tile.type == 109))
						{
							SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
						}
					}
				}
				if (SmartCursorHelper._targets.Count > 0)
				{
					float num = -1f;
					Tuple<int, int> tuple = SmartCursorHelper._targets[0];
					for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
					{
						float num2 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num == -1f || num2 < num)
						{
							num = num2;
							tuple = SmartCursorHelper._targets[k];
						}
					}
					if (Collision.InTileBounds(tuple.Item1, tuple.Item2, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
					{
						fX = tuple.Item1;
						fY = tuple.Item2;
					}
				}
				SmartCursorHelper._targets.Clear();
			}
		}

		// Token: 0x040043B9 RID: 17337
		private static List<Tuple<int, int>> _targets = new List<Tuple<int, int>>();

		// Token: 0x040043BA RID: 17338
		private static List<Tuple<int, int>> _grappleTargets = new List<Tuple<int, int>>();

		// Token: 0x040043BB RID: 17339
		private static List<Tuple<int, int>> _points = new List<Tuple<int, int>>();

		// Token: 0x040043BC RID: 17340
		private static List<Tuple<int, int>> _endpoints = new List<Tuple<int, int>>();

		// Token: 0x040043BD RID: 17341
		private static List<Tuple<int, int>> _toRemove = new List<Tuple<int, int>>();

		// Token: 0x040043BE RID: 17342
		private static List<Tuple<int, int>> _targets2 = new List<Tuple<int, int>>();

		// Token: 0x02000607 RID: 1543
		private class SmartCursorUsageInfo
		{
			// Token: 0x0400603D RID: 24637
			public Player player;

			// Token: 0x0400603E RID: 24638
			public Item item;

			// Token: 0x0400603F RID: 24639
			public Vector2 mouse;

			// Token: 0x04006040 RID: 24640
			public Vector2 position;

			// Token: 0x04006041 RID: 24641
			public Vector2 Center;

			// Token: 0x04006042 RID: 24642
			public int screenTargetX;

			// Token: 0x04006043 RID: 24643
			public int screenTargetY;

			// Token: 0x04006044 RID: 24644
			public int reachableStartX;

			// Token: 0x04006045 RID: 24645
			public int reachableEndX;

			// Token: 0x04006046 RID: 24646
			public int reachableStartY;

			// Token: 0x04006047 RID: 24647
			public int reachableEndY;

			// Token: 0x04006048 RID: 24648
			public int paintLookup;

			// Token: 0x04006049 RID: 24649
			public int paintCoatingLookup;
		}
	}
}
