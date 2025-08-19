using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.Enums;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terraria.GameContent
{
	// Token: 0x020004B6 RID: 1206
	public class SmartCursorHelper
	{
		// Token: 0x060039E3 RID: 14819 RVA: 0x0059C818 File Offset: 0x0059AA18
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
			if (!flag || smartCursorUsageInfo.screenTargetX < smartCursorUsageInfo.reachableStartX || smartCursorUsageInfo.screenTargetX > smartCursorUsageInfo.reachableEndX || smartCursorUsageInfo.screenTargetY < smartCursorUsageInfo.reachableStartY || smartCursorUsageInfo.screenTargetY > smartCursorUsageInfo.reachableEndY)
			{
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
				int fX = -1;
				int fY = -1;
				if (!Player.SmartCursorSettings.SmartAxeAfterPickaxe)
				{
					SmartCursorHelper.Step_Axe(smartCursorUsageInfo, ref fX, ref fY);
				}
				SmartCursorHelper.Step_ForceCursorToAnyMinableThing(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_Pickaxe_MineShinies(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_Pickaxe_MineSolids(player, smartCursorUsageInfo, SmartCursorHelper._grappleTargets, ref fX, ref fY);
				if (Player.SmartCursorSettings.SmartAxeAfterPickaxe)
				{
					SmartCursorHelper.Step_Axe(smartCursorUsageInfo, ref fX, ref fY);
				}
				SmartCursorHelper.Step_ColoredWrenches(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_MulticolorWrench(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_Hammers(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_ActuationRod(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_WireCutter(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_Platforms(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_MinecartTracks(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_Walls(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_PumpkinSeeds(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_GrassSeeds(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_Pigronata(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_Boulders(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_Torch(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_LawnMower(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_BlocksFilling(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_BlocksLines(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_PaintRoller(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_PaintBrush(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_PaintScrapper(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_Acorns(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_GemCorns(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_EmptyBuckets(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_Actuators(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_AlchemySeeds(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_PlanterBox(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_ClayPots(smartCursorUsageInfo, ref fX, ref fY);
				SmartCursorHelper.Step_StaffOfRegrowth(smartCursorUsageInfo, ref fX, ref fY);
				if (fX != -1 && fY != -1)
				{
					Main.SmartCursorX = (Player.tileTargetX = fX);
					Main.SmartCursorY = (Player.tileTargetY = fY);
					Main.SmartCursorShowing = true;
				}
				SmartCursorHelper._grappleTargets.Clear();
			}
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x0059CC38 File Offset: 0x0059AE38
		private static void TryFindingPaintInplayerInventory(SmartCursorHelper.SmartCursorUsageInfo providedInfo, out int paintLookup, out int coatingLookup)
		{
			Item[] inventory = providedInfo.player.inventory;
			paintLookup = 0;
			coatingLookup = 0;
			if (providedInfo.item.type == 1071 || providedInfo.item.type == 1543 || providedInfo.item.type == 1072 || providedInfo.item.type == 1544)
			{
				Item item = providedInfo.player.FindPaintOrCoating();
				if (item != null)
				{
					coatingLookup = (int)item.paintCoating;
					paintLookup = (int)item.paint;
				}
			}
		}

		// Token: 0x060039E5 RID: 14821 RVA: 0x0059CCC0 File Offset: 0x0059AEC0
		private unsafe static bool IsHoveringOverAnInteractibleTileThatBlocksSmartCursor(SmartCursorHelper.SmartCursorUsageInfo providedInfo)
		{
			bool result = false;
			if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].active())
			{
				if (*Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].type != 314)
				{
					result = TileID.Sets.DisableSmartCursor[(int)(*Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].type)];
				}
				else if (providedInfo.player.gravDir == 1f)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060039E6 RID: 14822 RVA: 0x0059CD54 File Offset: 0x0059AF54
		private unsafe static void Step_StaffOfRegrowth(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if ((providedInfo.item.type != 213 && providedInfo.item.type != 5295) || focusedX != -1 || focusedY != -1)
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
					if (tile.active() && !tile.inActive() && *tile.type == 0 && (flag || (*tile.type == 0 && flag2)))
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

		// Token: 0x060039E7 RID: 14823 RVA: 0x0059CFF8 File Offset: 0x0059B1F8
		private unsafe static void Step_GrassSeeds(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (focusedX > -1 || focusedY > -1)
			{
				return;
			}
			int type = providedInfo.item.type;
			if (type < 0 || !ItemID.Sets.GrassSeeds[type])
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
								goto IL_18D;
							}
							if (type - 194 > 1)
							{
								goto IL_17E;
							}
							flag3 = (*tile.type == 59);
						}
						else
						{
							if (type == 2171)
							{
								goto IL_18D;
							}
							if (type != 5214)
							{
								goto IL_17E;
							}
							flag3 = (*tile.type == 57);
						}
						IL_1C8:
						if (flag3)
						{
							SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
							goto IL_1DD;
						}
						goto IL_1DD;
						IL_18D:
						flag3 = (*tile.type == 0 || *tile.type == 59);
						goto IL_1C8;
						IL_17E:
						flag3 = (*tile.type == 0);
						goto IL_1C8;
					}
					IL_1DD:;
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

		// Token: 0x060039E8 RID: 14824 RVA: 0x0059D300 File Offset: 0x0059B500
		private unsafe static void Step_ClayPots(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.createTile != 78 || focusedX != -1 || focusedY != -1)
			{
				return;
			}
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
						if ((!tile.active() || Main.tileCut[(int)(*tile.type)] || TileID.Sets.BreakableWhenPlacing[(int)(*tile.type)]) && tile2.nactive() && !tile2.halfBrick() && tile2.slope() == 0 && Main.tileSolid[(int)(*tile2.type)])
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

		// Token: 0x060039E9 RID: 14825 RVA: 0x0059D578 File Offset: 0x0059B778
		private unsafe static void Step_PlanterBox(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.createTile != 380 || focusedX != -1 || focusedY != -1)
			{
				return;
			}
			SmartCursorHelper._targets.Clear();
			bool flag = false;
			if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].active() && *Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].type == 380)
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
						if (tile.active() && *tile.type == 380)
						{
							if (!Main.tile[i - 1, j].active() || Main.tileCut[(int)(*Main.tile[i - 1, j].type)] || TileID.Sets.BreakableWhenPlacing[(int)(*Main.tile[i - 1, j].type)])
							{
								SmartCursorHelper._targets.Add(new Tuple<int, int>(i - 1, j));
							}
							if (!Main.tile[i + 1, j].active() || Main.tileCut[(int)(*Main.tile[i + 1, j].type)] || TileID.Sets.BreakableWhenPlacing[(int)(*Main.tile[i + 1, j].type)])
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

		// Token: 0x060039EA RID: 14826 RVA: 0x0059D83C File Offset: 0x0059BA3C
		private unsafe static void Step_AlchemySeeds(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.createTile != 82 || focusedX != -1 || focusedY != -1)
			{
				return;
			}
			int placeStyle = providedInfo.item.placeStyle;
			SmartCursorHelper._targets.Clear();
			for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
			{
				for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
				{
					Tile tile = Main.tile[i, j];
					Tile tile2 = Main.tile[i, j + 1];
					bool flag2 = !tile.active() || TileID.Sets.BreakableWhenPlacing[(int)(*tile.type)] || (Main.tileCut[(int)(*tile.type)] && *tile.type != 82 && *tile.type != 83) || WorldGen.IsHarvestableHerbWithSeed((int)(*tile.type), (int)(*tile.frameX / 18));
					bool flag = tile2.nactive() && !tile2.halfBrick() && tile2.slope() == 0;
					if (flag2 && flag)
					{
						switch (placeStyle)
						{
						case 0:
							if (*tile2.type != 78 && *tile2.type != 380 && *tile2.type != 2 && *tile2.type != 477 && *tile2.type != 109 && *tile2.type != 492)
							{
								goto IL_39B;
							}
							if (*tile.liquid > 0)
							{
								goto IL_39B;
							}
							break;
						case 1:
							if (*tile2.type != 78 && *tile2.type != 380 && *tile2.type != 60)
							{
								goto IL_39B;
							}
							if (*tile.liquid > 0)
							{
								goto IL_39B;
							}
							break;
						case 2:
							if (*tile2.type != 78 && *tile2.type != 380 && *tile2.type != 0 && *tile2.type != 59)
							{
								goto IL_39B;
							}
							if (*tile.liquid > 0)
							{
								goto IL_39B;
							}
							break;
						case 3:
							if (*tile2.type != 78 && *tile2.type != 380 && *tile2.type != 203 && *tile2.type != 199 && *tile2.type != 23 && *tile2.type != 25)
							{
								goto IL_39B;
							}
							if (*tile.liquid > 0)
							{
								goto IL_39B;
							}
							break;
						case 4:
							if (*tile2.type != 78 && *tile2.type != 380 && *tile2.type != 53 && *tile2.type != 116)
							{
								goto IL_39B;
							}
							if (*tile.liquid > 0 && tile.lava())
							{
								goto IL_39B;
							}
							break;
						case 5:
							if (*tile2.type != 78 && *tile2.type != 380 && *tile2.type != 57 && *tile2.type != 633)
							{
								goto IL_39B;
							}
							if (*tile.liquid > 0 && !tile.lava())
							{
								goto IL_39B;
							}
							break;
						case 6:
							if ((*tile2.type != 78 && *tile2.type != 380 && *tile2.type != 147 && *tile2.type != 161 && *tile2.type != 163 && *tile2.type != 164 && *tile2.type != 200) || (*tile.liquid > 0 && tile.lava()))
							{
								goto IL_39B;
							}
							break;
						}
						SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
					}
					IL_39B:;
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

		// Token: 0x060039EB RID: 14827 RVA: 0x0059DD04 File Offset: 0x0059BF04
		private static void Step_Actuators(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.type != 849 || focusedX != -1 || focusedY != -1)
			{
				return;
			}
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

		// Token: 0x060039EC RID: 14828 RVA: 0x0059DEB4 File Offset: 0x0059C0B4
		private unsafe static void Step_EmptyBuckets(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.type != 205 || focusedX != -1 || focusedY != -1)
			{
				return;
			}
			SmartCursorHelper._targets.Clear();
			for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
			{
				for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
				{
					Tile tile = Main.tile[i, j];
					if (*tile.liquid > 0)
					{
						int num = (int)tile.liquidType();
						int num2 = 0;
						for (int k = i - 1; k <= i + 1; k++)
						{
							for (int l = j - 1; l <= j + 1; l++)
							{
								if ((int)Main.tile[k, l].liquidType() == num)
								{
									num2 += (int)(*Main.tile[k, l].liquid);
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

		// Token: 0x060039ED RID: 14829 RVA: 0x0059E0BC File Offset: 0x0059C2BC
		private unsafe static void Step_PaintScrapper(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (!ItemID.Sets.IsPaintScraper[providedInfo.item.type] || focusedX != -1 || focusedY != -1)
			{
				return;
			}
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
						flag |= (*tile.type == 184);
						flag |= tile.fullbrightBlock();
						flag |= tile.invisibleBlock();
					}
					if (*tile.wall > 0)
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

		// Token: 0x060039EE RID: 14830 RVA: 0x0059E2B4 File Offset: 0x0059C4B4
		private static void Step_PaintBrush(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if ((providedInfo.item.type != 1071 && providedInfo.item.type != 1543) || (providedInfo.paintLookup == 0 && providedInfo.paintCoatingLookup == 0) || focusedX != -1 || focusedY != -1)
			{
				return;
			}
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
						if (tile.active() && ((paintLookup != 0 && (int)tile.color() != paintLookup) | (paintCoatingLookup == 1 && !tile.fullbrightBlock()) | (paintCoatingLookup == 2 && !tile.invisibleBlock())))
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

		// Token: 0x060039EF RID: 14831 RVA: 0x0059E4B0 File Offset: 0x0059C6B0
		private unsafe static void Step_PaintRoller(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if ((providedInfo.item.type != 1072 && providedInfo.item.type != 1544) || (providedInfo.paintLookup == 0 && providedInfo.paintCoatingLookup == 0) || focusedX != -1 || focusedY != -1)
			{
				return;
			}
			SmartCursorHelper._targets.Clear();
			int paintLookup = providedInfo.paintLookup;
			int paintCoatingLookup = providedInfo.paintCoatingLookup;
			for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
			{
				for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
				{
					Tile tile = Main.tile[i, j];
					if (*tile.wall > 0 && (!tile.active() || !Main.tileSolid[(int)(*tile.type)] || Main.tileSolidTop[(int)(*tile.type)]) && ((paintLookup != 0 && (int)tile.wallColor() != paintLookup) | (paintCoatingLookup == 1 && !tile.fullbrightWall()) | (paintCoatingLookup == 2 && !tile.invisibleWall())))
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

		// Token: 0x060039F0 RID: 14832 RVA: 0x0059E6D8 File Offset: 0x0059C8D8
		private unsafe static void Step_BlocksLines(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			int type = providedInfo.item.type;
			if (type < 0 || !Player.SmartCursorSettings.SmartBlocksEnabled || providedInfo.item.createTile <= -1 || type == 213 || type == 5295 || ItemID.Sets.GrassSeeds[type] || !Main.tileSolid[providedInfo.item.createTile] || Main.tileSolidTop[providedInfo.item.createTile] || Main.tileFrameImportant[providedInfo.item.createTile] || focusedX != -1 || focusedY != -1)
			{
				return;
			}
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
						if (!tile.active() || Main.tileCut[(int)(*tile.type)] || TileID.Sets.BreakableWhenPlacing[(int)(*tile.type)])
						{
							bool flag2 = false;
							if (Main.tile[i - 1, j].active() && Main.tileSolid[(int)(*Main.tile[i - 1, j].type)] && !Main.tileSolidTop[(int)(*Main.tile[i - 1, j].type)])
							{
								flag2 = true;
							}
							if (Main.tile[i + 1, j].active() && Main.tileSolid[(int)(*Main.tile[i + 1, j].type)] && !Main.tileSolidTop[(int)(*Main.tile[i + 1, j].type)])
							{
								flag2 = true;
							}
							if (Main.tile[i, j - 1].active() && Main.tileSolid[(int)(*Main.tile[i, j - 1].type)] && !Main.tileSolidTop[(int)(*Main.tile[i, j - 1].type)])
							{
								flag2 = true;
							}
							if (Main.tile[i, j + 1].active() && Main.tileSolid[(int)(*Main.tile[i, j + 1].type)] && !Main.tileSolidTop[(int)(*Main.tile[i, j + 1].type)])
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

		// Token: 0x060039F1 RID: 14833 RVA: 0x0059EAFC File Offset: 0x0059CCFC
		private unsafe static void Step_Boulders(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.createTile <= -1 || !TileID.Sets.Boulders[providedInfo.item.createTile] || focusedX != -1 || focusedY != -1)
			{
				return;
			}
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
					if ((!Main.tileSolid[(int)(*tile2.type)] && !Main.tileTable[(int)(*tile2.type)]) || (!Main.tileSolid[(int)(*tile.type)] && !Main.tileTable[(int)(*tile.type)]))
					{
						flag = false;
					}
					if (Main.tileNoAttach[(int)(*tile2.type)] || Main.tileNoAttach[(int)(*tile.type)])
					{
						flag = false;
					}
					for (int k = i; k <= i + 1; k++)
					{
						for (int l = j - 1; l <= j; l++)
						{
							Tile tile3 = Main.tile[k, l];
							if (tile3.active() && !Main.tileCut[(int)(*tile3.type)])
							{
								flag = false;
							}
						}
					}
					int x = i * 16;
					int y = j * 16 - 16;
					int width = 32;
					int height = 32;
					Rectangle value;
					value..ctor(x, y, width, height);
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

		// Token: 0x060039F2 RID: 14834 RVA: 0x0059EE10 File Offset: 0x0059D010
		private unsafe static void Step_Pigronata(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.createTile != 454 || focusedX != -1 || focusedY != -1)
			{
				return;
			}
			SmartCursorHelper._targets.Clear();
			for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
			{
				int j = providedInfo.reachableStartY;
				while (j <= providedInfo.reachableEndY && (double)j <= Main.worldSurface - 2.0)
				{
					bool flag = true;
					for (int k = i - 2; k <= i + 1; k++)
					{
						for (int l = j - 1; l <= j + 2; l++)
						{
							Tile tile = Main.tile[k, l];
							if (l == j - 1)
							{
								if (!WorldGen.SolidTile(tile))
								{
									flag = false;
								}
							}
							else if (tile.active() && (!Main.tileCut[(int)(*tile.type)] || *tile.type == 454))
							{
								flag = false;
							}
						}
					}
					if (flag)
					{
						SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
					}
					j++;
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

		// Token: 0x060039F3 RID: 14835 RVA: 0x0059F014 File Offset: 0x0059D214
		private unsafe static void Step_PumpkinSeeds(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.createTile != 254 || focusedX != -1 || focusedY != -1)
			{
				return;
			}
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
					if (*tile2.type != 2 && *tile2.type != 477 && *tile2.type != 109 && *tile2.type != 492)
					{
						flag = false;
					}
					if (*tile.type != 2 && *tile.type != 477 && *tile.type != 109 && *tile.type != 492)
					{
						flag = false;
					}
					for (int k = i; k <= i + 1; k++)
					{
						for (int l = j - 1; l <= j; l++)
						{
							Tile tile3 = Main.tile[k, l];
							if (tile3.active() && (*tile3.type < 0 || Main.tileSolid[(int)(*tile3.type)] || !WorldGen.CanCutTile(k, l, TileCuttingContext.TilePlacement)))
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

		// Token: 0x060039F4 RID: 14836 RVA: 0x0059F2E4 File Offset: 0x0059D4E4
		private unsafe static void Step_Walls(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			int width = providedInfo.player.width;
			int height = providedInfo.player.height;
			if (providedInfo.item.createWall <= 0 || focusedX != -1 || focusedY != -1)
			{
				return;
			}
			SmartCursorHelper._targets.Clear();
			for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
			{
				for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
				{
					Tile tile = Main.tile[i, j];
					if (*tile.wall == 0 && (!tile.active() || !Main.tileSolid[(int)(*tile.type)] || Main.tileSolidTop[(int)(*tile.type)]))
					{
						Vector2 position = providedInfo.position;
						int width2 = width;
						int height2 = height;
						Vector2 position2 = new Vector2((float)i, (float)j) * 16f;
						int width3 = 16;
						int height3 = 16;
						Utils.TileActionAttempt check;
						if ((check = SmartCursorHelper.<>O.<0>__NotDoorStand) == null)
						{
							check = (SmartCursorHelper.<>O.<0>__NotDoorStand = new Utils.TileActionAttempt(DelegateMethods.NotDoorStand));
						}
						if (Collision.CanHitWithCheck(position, width2, height2, position2, width3, height3, check))
						{
							bool flag = false;
							if (Main.tile[i - 1, j].active() || *Main.tile[i - 1, j].wall > 0)
							{
								flag = true;
							}
							if (Main.tile[i + 1, j].active() || *Main.tile[i + 1, j].wall > 0)
							{
								flag = true;
							}
							if (Main.tile[i, j - 1].active() || *Main.tile[i, j - 1].wall > 0)
							{
								flag = true;
							}
							if (Main.tile[i, j + 1].active() || *Main.tile[i, j + 1].wall > 0)
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

		// Token: 0x060039F5 RID: 14837 RVA: 0x0059F5F0 File Offset: 0x0059D7F0
		private unsafe static void Step_MinecartTracks(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if ((providedInfo.item.type == 2340 || providedInfo.item.type == 2739) && focusedX == -1 && focusedY == -1)
			{
				SmartCursorHelper._targets.Clear();
				Vector2 vector = (Main.MouseWorld - providedInfo.Center).SafeNormalize(Vector2.UnitY);
				float num7 = Vector2.Dot(vector, -Vector2.UnitY);
				bool flag = num7 >= 0.5f;
				bool flag2 = num7 <= -0.5f;
				float num8 = Vector2.Dot(vector, Vector2.UnitX);
				bool flag3 = num8 >= 0.5f;
				bool flag4 = num8 <= -0.5f;
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
				if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].active() && *Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].type == 314)
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
							if (tile.active() && *tile.type == 314)
							{
								bool flag10 = Main.tile[i + 1, j + 1].active() && *Main.tile[i + 1, j + 1].type == 314;
								bool flag11 = Main.tile[i + 1, j - 1].active() && *Main.tile[i + 1, j - 1].type == 314;
								bool flag12 = Main.tile[i - 1, j + 1].active() && *Main.tile[i - 1, j + 1].type == 314;
								bool flag13 = Main.tile[i - 1, j - 1].active() && *Main.tile[i - 1, j - 1].type == 314;
								if (flag5 && (!Main.tile[i - 1, j - 1].active() || Main.tileCut[(int)(*Main.tile[i - 1, j - 1].type)] || TileID.Sets.BreakableWhenPlacing[(int)(*Main.tile[i - 1, j - 1].type)]) && (flag10 || !flag11) && !flag12)
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i - 1, j - 1));
								}
								if (flag4 && (!Main.tile[i - 1, j].active() || Main.tileCut[(int)(*Main.tile[i - 1, j].type)] || TileID.Sets.BreakableWhenPlacing[(int)(*Main.tile[i - 1, j].type)]))
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i - 1, j));
								}
								if (flag7 && (!Main.tile[i - 1, j + 1].active() || Main.tileCut[(int)(*Main.tile[i - 1, j + 1].type)] || TileID.Sets.BreakableWhenPlacing[(int)(*Main.tile[i - 1, j + 1].type)]) && (flag11 || !flag10) && !flag13)
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i - 1, j + 1));
								}
								if (flag6 && (!Main.tile[i + 1, j - 1].active() || Main.tileCut[(int)(*Main.tile[i + 1, j - 1].type)] || TileID.Sets.BreakableWhenPlacing[(int)(*Main.tile[i + 1, j - 1].type)]) && (flag12 || !flag13) && !flag10)
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i + 1, j - 1));
								}
								if (flag3 && (!Main.tile[i + 1, j].active() || Main.tileCut[(int)(*Main.tile[i + 1, j].type)] || TileID.Sets.BreakableWhenPlacing[(int)(*Main.tile[i + 1, j].type)]))
								{
									SmartCursorHelper._targets.Add(new Tuple<int, int>(i + 1, j));
								}
								if (flag8 && (!Main.tile[i + 1, j + 1].active() || Main.tileCut[(int)(*Main.tile[i + 1, j + 1].type)] || TileID.Sets.BreakableWhenPlacing[(int)(*Main.tile[i + 1, j + 1].type)]) && (flag13 || !flag12) && !flag11)
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
						if ((!Main.tile[SmartCursorHelper._targets[k].Item1, SmartCursorHelper._targets[k].Item2 - 1].active() || *Main.tile[SmartCursorHelper._targets[k].Item1, SmartCursorHelper._targets[k].Item2 - 1].type != 314) && (!Main.tile[SmartCursorHelper._targets[k].Item1, SmartCursorHelper._targets[k].Item2 + 1].active() || *Main.tile[SmartCursorHelper._targets[k].Item1, SmartCursorHelper._targets[k].Item2 + 1].type != 314))
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
			if (providedInfo.item.type != 2492 || focusedX != -1 || focusedY != -1)
			{
				return;
			}
			SmartCursorHelper._targets.Clear();
			bool flag14 = false;
			if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].active() && *Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].type == 314)
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
						if (tile2.active() && *tile2.type == 314)
						{
							if (!Main.tile[l - 1, m].active() || Main.tileCut[(int)(*Main.tile[l - 1, m].type)] || TileID.Sets.BreakableWhenPlacing[(int)(*Main.tile[l - 1, m].type)])
							{
								SmartCursorHelper._targets.Add(new Tuple<int, int>(l - 1, m));
							}
							if (!Main.tile[l + 1, m].active() || Main.tileCut[(int)(*Main.tile[l + 1, m].type)] || TileID.Sets.BreakableWhenPlacing[(int)(*Main.tile[l + 1, m].type)])
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
					if ((!Main.tile[SmartCursorHelper._targets[n].Item1, SmartCursorHelper._targets[n].Item2 - 1].active() || *Main.tile[SmartCursorHelper._targets[n].Item1, SmartCursorHelper._targets[n].Item2 - 1].type != 314) && (!Main.tile[SmartCursorHelper._targets[n].Item1, SmartCursorHelper._targets[n].Item2 + 1].active() || *Main.tile[SmartCursorHelper._targets[n].Item1, SmartCursorHelper._targets[n].Item2 + 1].type != 314))
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

		// Token: 0x060039F6 RID: 14838 RVA: 0x005A01A4 File Offset: 0x0059E3A4
		private unsafe static void Step_Platforms(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.createTile < 0 || !TileID.Sets.Platforms[providedInfo.item.createTile] || focusedX != -1 || focusedY != -1)
			{
				return;
			}
			SmartCursorHelper._targets.Clear();
			bool flag = false;
			if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].active() && TileID.Sets.Platforms[(int)(*Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].type)])
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
						if (tile.active() && TileID.Sets.Platforms[(int)(*tile.type)])
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
							int num2 = 0;
							int num3 = 1;
							Tile tile2 = Main.tile[i + num2, j + num3];
							if (tile2.active() && Main.tileSolid[(int)(*tile2.type)] && !Main.tileSolidTop[(int)(*tile2.type)])
							{
								SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
							}
							num2 = -1;
							num3 = 0;
							tile2 = Main.tile[i + num2, j + num3];
							if (tile2.active() && Main.tileSolid[(int)(*tile2.type)] && !Main.tileSolidTop[(int)(*tile2.type)])
							{
								SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
							}
							num2 = 1;
							num3 = 0;
							tile2 = Main.tile[i + num2, j + num3];
							if (tile2.active() && Main.tileSolid[(int)(*tile2.type)] && !Main.tileSolidTop[(int)(*tile2.type)])
							{
								SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
							}
						}
					}
				}
			}
			if (SmartCursorHelper._targets.Count > 0)
			{
				float num4 = -1f;
				Tuple<int, int> tuple = SmartCursorHelper._targets[0];
				for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
				{
					float num5 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
					if (num4 == -1f || num5 < num4)
					{
						num4 = num5;
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

		// Token: 0x060039F7 RID: 14839 RVA: 0x005A05D4 File Offset: 0x0059E7D4
		private static void Step_WireCutter(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.type != 510 || focusedX != -1 || focusedY != -1)
			{
				return;
			}
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

		// Token: 0x060039F8 RID: 14840 RVA: 0x005A077C File Offset: 0x0059E97C
		private static void Step_ActuationRod(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			bool actuationRodLock = providedInfo.player.ActuationRodLock;
			bool actuationRodLockSetting = providedInfo.player.ActuationRodLockSetting;
			if (providedInfo.item.type != 3620 || focusedX != -1 || focusedY != -1)
			{
				return;
			}
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

		// Token: 0x060039F9 RID: 14841 RVA: 0x005A0930 File Offset: 0x0059EB30
		private unsafe static void Step_Hammers(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
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
				int num18 = (int)providedInfo.Center.X / 16;
				int num19 = (int)providedInfo.Center.Y / 16;
				SmartCursorHelper._points.Clear();
				SmartCursorHelper._endpoints.Clear();
				int num3 = 1;
				if (num2 == -1 && num != 0)
				{
					num3 = -1;
				}
				int num4 = (int)((providedInfo.position.X + (float)(width / 2) + (float)((width / 2 - 1) * num)) / 16f);
				int num5 = (int)(((double)providedInfo.position.Y + 0.1) / 16.0);
				if (num3 == -1)
				{
					num5 = (int)((providedInfo.position.Y + (float)height - 1f) / 16f);
				}
				int num6 = width / 16 + ((width % 16 != 0) ? 1 : 0);
				int num7 = height / 16 + ((height % 16 != 0) ? 1 : 0);
				if (num != 0)
				{
					for (int i = 0; i < num7; i++)
					{
						if (Main.tile[num4, num5 + i * num3] != null)
						{
							SmartCursorHelper._points.Add(new Tuple<int, int>(num4, num5 + i * num3));
						}
					}
				}
				if (num2 != 0)
				{
					for (int j = 0; j < num6; j++)
					{
						if (Main.tile[(int)(providedInfo.position.X / 16f) + j, num5] != null)
						{
							SmartCursorHelper._points.Add(new Tuple<int, int>((int)(providedInfo.position.X / 16f) + j, num5));
						}
					}
				}
				int num8 = (int)((providedInfo.mouse.X + (float)((width / 2 - 1) * num)) / 16f);
				int num9 = (int)(((double)providedInfo.mouse.Y + 0.1 - (double)(height / 2 + 1)) / 16.0);
				if (num3 == -1)
				{
					num9 = (int)((providedInfo.mouse.Y + (float)(height / 2) - 1f) / 16f);
				}
				if (providedInfo.player.gravDir == -1f && num2 == 0)
				{
					num9++;
				}
				if (num9 < 10)
				{
					num9 = 10;
				}
				if (num9 > Main.maxTilesY - 10)
				{
					num9 = Main.maxTilesY - 10;
				}
				int num10 = width / 16 + ((width % 16 != 0) ? 1 : 0);
				int num11 = height / 16 + ((height % 16 != 0) ? 1 : 0);
				if (num != 0)
				{
					for (int k = 0; k < num11; k++)
					{
						if (Main.tile[num8, num9 + k * num3] != null)
						{
							SmartCursorHelper._endpoints.Add(new Tuple<int, int>(num8, num9 + k * num3));
						}
					}
				}
				if (num2 != 0)
				{
					for (int l = 0; l < num10; l++)
					{
						if (Main.tile[(int)((providedInfo.mouse.X - (float)(width / 2)) / 16f) + l, num9] != null)
						{
							SmartCursorHelper._endpoints.Add(new Tuple<int, int>((int)((providedInfo.mouse.X - (float)(width / 2)) / 16f) + l, num9));
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
						Tile tile = Main.tile[tuple3.Item1, tuple3.Item2];
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
					float num12 = -1f;
					Tuple<int, int> tuple4 = null;
					for (int m = 0; m < SmartCursorHelper._targets.Count; m++)
					{
						if (!Main.tile[SmartCursorHelper._targets[m].Item1, SmartCursorHelper._targets[m].Item2].active() || *Main.tile[SmartCursorHelper._targets[m].Item1, SmartCursorHelper._targets[m].Item2].type != 26)
						{
							float num13 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[m].Item1, (float)SmartCursorHelper._targets[m].Item2) * 16f + Vector2.One * 8f, providedInfo.Center);
							if (num12 == -1f || num13 < num12)
							{
								num12 = num13;
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
			if (providedInfo.item.hammer <= 0 || focusedX != -1 || focusedY != -1)
			{
				return;
			}
			SmartCursorHelper._targets.Clear();
			for (int n = providedInfo.reachableStartX; n <= providedInfo.reachableEndX; n++)
			{
				for (int num14 = providedInfo.reachableStartY; num14 <= providedInfo.reachableEndY; num14++)
				{
					if (*Main.tile[n, num14].wall > 0 && Collision.HitWallSubstep(n, num14))
					{
						SmartCursorHelper._targets.Add(new Tuple<int, int>(n, num14));
					}
				}
			}
			if (SmartCursorHelper._targets.Count > 0)
			{
				float num15 = -1f;
				Tuple<int, int> tuple5 = null;
				for (int num16 = 0; num16 < SmartCursorHelper._targets.Count; num16++)
				{
					if (!Main.tile[SmartCursorHelper._targets[num16].Item1, SmartCursorHelper._targets[num16].Item2].active() || *Main.tile[SmartCursorHelper._targets[num16].Item1, SmartCursorHelper._targets[num16].Item2].type != 26)
					{
						float num17 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[num16].Item1, (float)SmartCursorHelper._targets[num16].Item2) * 16f + Vector2.One * 8f, providedInfo.mouse);
						if (num15 == -1f || num17 < num15)
						{
							num15 = num17;
							tuple5 = SmartCursorHelper._targets[num16];
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

		// Token: 0x060039FA RID: 14842 RVA: 0x005A11A0 File Offset: 0x0059F3A0
		private static void Step_MulticolorWrench(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.type != 3625 || focusedX != -1 || focusedY != -1)
			{
				return;
			}
			SmartCursorHelper._targets.Clear();
			WiresUI.Settings.MultiToolMode toolMode = WiresUI.Settings.ToolMode;
			WiresUI.Settings.MultiToolMode multiToolMode = (WiresUI.Settings.MultiToolMode)0;
			if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].wire())
			{
				multiToolMode |= WiresUI.Settings.MultiToolMode.Red;
			}
			if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].wire2())
			{
				multiToolMode |= WiresUI.Settings.MultiToolMode.Blue;
			}
			if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].wire3())
			{
				multiToolMode |= WiresUI.Settings.MultiToolMode.Green;
			}
			if (Main.tile[providedInfo.screenTargetX, providedInfo.screenTargetY].wire4())
			{
				multiToolMode |= WiresUI.Settings.MultiToolMode.Yellow;
			}
			toolMode &= ~WiresUI.Settings.MultiToolMode.Cutter;
			bool flag6 = toolMode == multiToolMode;
			toolMode = WiresUI.Settings.ToolMode;
			if (!flag6)
			{
				bool flag = toolMode.HasFlag(WiresUI.Settings.MultiToolMode.Red);
				bool flag2 = toolMode.HasFlag(WiresUI.Settings.MultiToolMode.Blue);
				bool flag3 = toolMode.HasFlag(WiresUI.Settings.MultiToolMode.Green);
				bool flag4 = toolMode.HasFlag(WiresUI.Settings.MultiToolMode.Yellow);
				bool flag5 = toolMode.HasFlag(WiresUI.Settings.MultiToolMode.Cutter);
				for (int i = providedInfo.reachableStartX; i <= providedInfo.reachableEndX; i++)
				{
					for (int j = providedInfo.reachableStartY; j <= providedInfo.reachableEndY; j++)
					{
						Tile tile = Main.tile[i, j];
						if (flag5)
						{
							if ((tile.wire() && flag) || (tile.wire2() && flag2) || (tile.wire3() && flag3) || (tile.wire4() && flag4))
							{
								SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
							}
						}
						else if ((tile.wire() && flag) || (tile.wire2() && flag2) || (tile.wire3() && flag3) || (tile.wire4() && flag4))
						{
							if (flag)
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
							if (flag2)
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
							if (flag3)
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
							if (flag4)
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

		// Token: 0x060039FB RID: 14843 RVA: 0x005A17B8 File Offset: 0x0059F9B8
		private static void Step_ColoredWrenches(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if ((providedInfo.item.type != 509 && providedInfo.item.type != 850 && providedInfo.item.type != 851 && providedInfo.item.type != 3612) || focusedX != -1 || focusedY != -1)
			{
				return;
			}
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

		// Token: 0x060039FC RID: 14844 RVA: 0x005A1D84 File Offset: 0x0059FF84
		private unsafe static void Step_Acorns(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			if (providedInfo.item.type != 27 || focusedX != -1 || focusedY != -1 || providedInfo.reachableStartY <= 20)
			{
				return;
			}
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
					if ((!tile.active() || Main.tileCut[(int)(*tile.type)] || TileID.Sets.BreakableWhenPlacing[(int)(*tile.type)]) && (!tile2.active() || Main.tileCut[(int)(*tile2.type)] || TileID.Sets.BreakableWhenPlacing[(int)(*tile2.type)]) && (!tile4.active() || !TileID.Sets.CommonSapling[(int)(*tile4.type)]) && (!tile5.active() || !TileID.Sets.CommonSapling[(int)(*tile5.type)]) && (!tile6.active() || !TileID.Sets.CommonSapling[(int)(*tile6.type)]) && (!tile7.active() || !TileID.Sets.CommonSapling[(int)(*tile7.type)]) && (!tile8.active() || !TileID.Sets.CommonSapling[(int)(*tile8.type)]) && (!tile9.active() || !TileID.Sets.CommonSapling[(int)(*tile9.type)]) && tile3.active() && WorldGen.SolidTile2(tile3))
					{
						bool tileTypeValid = false;
						ushort num3 = *tile3.type;
						if (num3 <= 116)
						{
							if (num3 <= 53)
							{
								if (num3 == 2 || num3 == 23 || num3 == 53)
								{
									goto IL_2D6;
								}
							}
							else if (num3 <= 109)
							{
								if (num3 != 60)
								{
									if (num3 == 109)
									{
										goto IL_2D6;
									}
								}
								else
								{
									if (WorldGen.EmptyTileCheck(i - 2, i + 2, j - 20, j, 20))
									{
										SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
										goto IL_345;
									}
									goto IL_345;
								}
							}
							else if (num3 == 112 || num3 == 116)
							{
								goto IL_2D6;
							}
						}
						else if (num3 <= 234)
						{
							if (num3 == 147 || num3 == 199 || num3 == 234)
							{
								goto IL_2D6;
							}
						}
						else if (num3 <= 492)
						{
							if (num3 == 477 || num3 == 492)
							{
								goto IL_2D6;
							}
						}
						else if (num3 == 633 || num3 - 661 <= 1)
						{
							goto IL_2D6;
						}
						IL_2D9:
						if (!tileTypeValid)
						{
							tileTypeValid = (TileLoader.CanGrowModTree((int)(*tile3.type)) || TileLoader.CanGrowModPalmTree((int)(*tile3.type)));
						}
						if (tileTypeValid && *tile4.liquid == 0 && *tile.liquid == 0 && *tile5.liquid == 0 && WorldGen.EmptyTileCheck(i - 2, i + 2, j - 20, j, 20))
						{
							SmartCursorHelper._targets.Add(new Tuple<int, int>(i, j));
							goto IL_345;
						}
						goto IL_345;
						IL_2D6:
						tileTypeValid = true;
						goto IL_2D9;
					}
					IL_345:;
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
						if (TileLoader.CanGrowModTree((int)(*tile10.type)) || TileLoader.CanGrowModPalmTree((int)(*tile10.type)))
						{
							flag = true;
						}
						else
						{
							ushort num3 = *tile10.type;
							if (num3 <= 116)
							{
								if (num3 <= 53)
								{
									if (num3 != 2 && num3 != 23 && num3 != 53)
									{
										goto IL_492;
									}
								}
								else if (num3 <= 109)
								{
									if (num3 != 60 && num3 != 109)
									{
										goto IL_492;
									}
								}
								else if (num3 != 112 && num3 != 116)
								{
									goto IL_492;
								}
							}
							else if (num3 <= 234)
							{
								if (num3 != 147 && num3 != 199 && num3 != 234)
								{
									goto IL_492;
								}
							}
							else if (num3 <= 492)
							{
								if (num3 != 477 && num3 != 492)
								{
									goto IL_492;
								}
							}
							else if (num3 != 633 && num3 - 661 > 1)
							{
								goto IL_492;
							}
							flag = true;
						}
					}
					IL_492:;
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

		// Token: 0x060039FD RID: 14845 RVA: 0x005A239C File Offset: 0x005A059C
		private unsafe static void Step_GemCorns(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int focusedX, ref int focusedY)
		{
			WorldGen.GrowTreeSettings profile;
			if (!WorldGen.GrowTreeSettings.Profiles.TryGetFromItemId(providedInfo.item.type, out profile) || focusedX != -1 || focusedY != -1 || providedInfo.reachableStartY <= 20)
			{
				return;
			}
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
					if (profile.GroundTest((int)(*tile3.type)) && (!tile.active() || Main.tileCut[(int)(*tile.type)] || TileID.Sets.BreakableWhenPlacing[(int)(*tile.type)]) && (!tile2.active() || Main.tileCut[(int)(*tile2.type)] || TileID.Sets.BreakableWhenPlacing[(int)(*tile2.type)]) && (!tile4.active() || !TileID.Sets.CommonSapling[(int)(*tile4.type)]) && (!tile5.active() || !TileID.Sets.CommonSapling[(int)(*tile5.type)]) && (!tile6.active() || !TileID.Sets.CommonSapling[(int)(*tile6.type)]) && (!tile7.active() || !TileID.Sets.CommonSapling[(int)(*tile7.type)]) && (!tile8.active() || !TileID.Sets.CommonSapling[(int)(*tile8.type)]) && (!tile9.active() || !TileID.Sets.CommonSapling[(int)(*tile9.type)]) && tile3.active() && WorldGen.SolidTile2(tile3) && *tile4.liquid == 0 && *tile.liquid == 0 && *tile5.liquid == 0 && WorldGen.EmptyTileCheck(i - 2, i + 2, j - profile.TreeHeightMax, j, (int)profile.SaplingTileType))
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
					if (tile10.active() && profile.GroundTest((int)(*tile10.type)))
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

		// Token: 0x060039FE RID: 14846 RVA: 0x005A27F4 File Offset: 0x005A09F4
		private unsafe static void Step_ForceCursorToAnyMinableThing(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int fX, ref int fY)
		{
			int reachableStartX = providedInfo.reachableStartX;
			int reachableStartY = providedInfo.reachableStartY;
			int reachableEndX = providedInfo.reachableEndX;
			int reachableEndY = providedInfo.reachableEndY;
			int screenTargetX = providedInfo.screenTargetX;
			int screenTargetY = providedInfo.screenTargetY;
			Vector2 mouse = providedInfo.mouse;
			Item item = providedInfo.item;
			if (fX != -1 || fY != -1 || PlayerInput.UsingGamepad)
			{
				return;
			}
			Point point = mouse.ToTileCoordinates();
			int x = point.X;
			int y = point.Y;
			if (Collision.InTileBounds(x, y, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
			{
				Tile tile = Main.tile[x, y];
				bool flag = tile.active() && WorldGen.CanKillTile(x, y) && (!Main.tileSolid[(int)(*tile.type)] || Main.tileSolidTop[(int)(*tile.type)]);
				if (flag && Main.tileAxe[(int)(*tile.type)] && item.axe < 1)
				{
					flag = false;
				}
				if (flag && Main.tileHammer[(int)(*tile.type)] && item.hammer < 1)
				{
					flag = false;
				}
				if (flag && !Main.tileHammer[(int)(*tile.type)] && !Main.tileAxe[(int)(*tile.type)] && item.pick < 1)
				{
					flag = false;
				}
				if (flag)
				{
					fX = x;
					fY = y;
				}
			}
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x005A2940 File Offset: 0x005A0B40
		private unsafe static void Step_Pickaxe_MineShinies(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int fX, ref int fY)
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
				int type = item.type;
			}
			int num = 0;
			for (int i = reachableStartX; i <= reachableEndX; i++)
			{
				for (int j = reachableStartY; j <= reachableEndY; j++)
				{
					Tile tile = Main.tile[i, j];
					Tile tile3 = Main.tile[i - 1, j];
					Tile tile4 = Main.tile[i + 1, j];
					Tile tile5 = Main.tile[i, j + 1];
					if (tile.active())
					{
						int num2 = TileID.Sets.SmartCursorPickaxePriorityOverride[(int)(*tile.type)];
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
			foreach (Tuple<int, int> item2 in SmartCursorHelper._targets2)
			{
				Tile tile2 = Main.tile[item2.Item1, item2.Item2];
				if (TileID.Sets.SmartCursorPickaxePriorityOverride[(int)(*tile2.type)] < num)
				{
					SmartCursorHelper._targets2.Add(item2);
				}
			}
			foreach (Tuple<int, int> item3 in SmartCursorHelper._targets2)
			{
				SmartCursorHelper._targets.Remove(item3);
			}
			if (SmartCursorHelper._targets.Count > 0)
			{
				float num3 = -1f;
				Tuple<int, int> tuple = SmartCursorHelper._targets[0];
				for (int k = 0; k < SmartCursorHelper._targets.Count; k++)
				{
					float num4 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[k].Item1, (float)SmartCursorHelper._targets[k].Item2) * 16f + Vector2.One * 8f, mouse);
					if (num3 == -1f || num4 < num3)
					{
						num3 = num4;
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

		// Token: 0x06003A00 RID: 14848 RVA: 0x005A2C2C File Offset: 0x005A0E2C
		private unsafe static void Step_Pickaxe_MineSolids(Player player, SmartCursorHelper.SmartCursorUsageInfo providedInfo, List<Tuple<int, int>> grappleTargets, ref int focusedX, ref int focusedY)
		{
			int width = player.width;
			int height = player.height;
			int direction = player.direction;
			Vector2 center = player.Center;
			Vector2 position = player.position;
			float gravDir = player.gravDir;
			int whoAmI = player.whoAmI;
			if (providedInfo.item.pick <= 0 || focusedX != -1 || focusedY != -1)
			{
				return;
			}
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
			int num20 = (int)center.X / 16;
			int num21 = (int)center.Y / 16;
			SmartCursorHelper._points.Clear();
			SmartCursorHelper._endpoints.Clear();
			int num3 = 1;
			if (num2 == -1 && num != 0)
			{
				num3 = -1;
			}
			int num4 = (int)((position.X + (float)(width / 2) + (float)((width / 2 - 1) * num)) / 16f);
			int num5 = (int)(((double)position.Y + 0.1) / 16.0);
			if (num3 == -1)
			{
				num5 = (int)((position.Y + (float)height - 1f) / 16f);
			}
			int num6 = width / 16 + ((width % 16 != 0) ? 1 : 0);
			int num7 = height / 16 + ((height % 16 != 0) ? 1 : 0);
			if (num != 0)
			{
				for (int i = 0; i < num7; i++)
				{
					if (Main.tile[num4, num5 + i * num3] != null)
					{
						SmartCursorHelper._points.Add(new Tuple<int, int>(num4, num5 + i * num3));
					}
				}
			}
			if (num2 != 0)
			{
				for (int j = 0; j < num6; j++)
				{
					if (Main.tile[(int)(position.X / 16f) + j, num5] != null)
					{
						SmartCursorHelper._points.Add(new Tuple<int, int>((int)(position.X / 16f) + j, num5));
					}
				}
			}
			int num8 = (int)((providedInfo.mouse.X + (float)((width / 2 - 1) * num)) / 16f);
			int num9 = (int)(((double)providedInfo.mouse.Y + 0.1 - (double)(height / 2 + 1)) / 16.0);
			if (num3 == -1)
			{
				num9 = (int)((providedInfo.mouse.Y + (float)(height / 2) - 1f) / 16f);
			}
			if (gravDir == -1f && num2 == 0)
			{
				num9++;
			}
			if (num9 < 10)
			{
				num9 = 10;
			}
			if (num9 > Main.maxTilesY - 10)
			{
				num9 = Main.maxTilesY - 10;
			}
			int num10 = width / 16 + ((width % 16 != 0) ? 1 : 0);
			int num11 = height / 16 + ((height % 16 != 0) ? 1 : 0);
			if (WorldGen.InWorld(num8, num9, 40))
			{
				if (num != 0)
				{
					for (int k = 0; k < num11; k++)
					{
						if (Main.tile[num8, num9 + k * num3] != null)
						{
							SmartCursorHelper._endpoints.Add(new Tuple<int, int>(num8, num9 + k * num3));
						}
					}
				}
				if (num2 != 0)
				{
					for (int l = 0; l < num10; l++)
					{
						if (Main.tile[(int)((providedInfo.mouse.X - (float)(width / 2)) / 16f) + l, num9] != null)
						{
							SmartCursorHelper._endpoints.Add(new Tuple<int, int>((int)((providedInfo.mouse.X - (float)(width / 2)) / 16f) + l, num9));
						}
					}
				}
			}
			SmartCursorHelper._targets.Clear();
			while (SmartCursorHelper._points.Count > 0 && SmartCursorHelper._endpoints.Count > 0)
			{
				Tuple<int, int> tuple = SmartCursorHelper._points[0];
				Tuple<int, int> tuple2 = SmartCursorHelper._endpoints[0];
				Tuple<int, int> col;
				if (!Collision.TupleHitLine(tuple.Item1, tuple.Item2, tuple2.Item1, tuple2.Item2, num * (int)gravDir, -num2 * (int)gravDir, grappleTargets, out col))
				{
					SmartCursorHelper._points.Remove(tuple);
					SmartCursorHelper._endpoints.Remove(tuple2);
				}
				else
				{
					if (col.Item1 != tuple2.Item1 || col.Item2 != tuple2.Item2)
					{
						SmartCursorHelper._targets.Add(col);
					}
					Tile tile = Main.tile[col.Item1, col.Item2];
					if (!tile.inActive() && tile.active() && Main.tileSolid[(int)(*tile.type)] && !Main.tileSolidTop[(int)(*tile.type)] && !grappleTargets.Contains(col))
					{
						SmartCursorHelper._targets.Add(col);
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
				float num12 = -1f;
				Tuple<int, int> tuple3 = SmartCursorHelper._targets[0];
				Vector2 value = center;
				if (Main.netMode == 1)
				{
					int num13 = 0;
					int num14 = 0;
					int num15 = 0;
					for (int num16 = 0; num16 < whoAmI; num16++)
					{
						Player player2 = Main.player[num16];
						if (player2.active && !player2.dead && player2.HeldItem.pick > 0 && player2.itemAnimation > 0)
						{
							if (player.Distance(player2.Center) <= 8f)
							{
								num13++;
							}
							if (player.Distance(player2.Center) <= 80f && Math.Abs(player2.Center.Y - center.Y) <= 12f)
							{
								num14++;
							}
						}
					}
					for (int num17 = whoAmI + 1; num17 < 255; num17++)
					{
						Player player3 = Main.player[num17];
						if (player3.active && !player3.dead && player3.HeldItem.pick > 0 && player3.itemAnimation > 0 && player.Distance(player3.Center) <= 8f)
						{
							num15++;
						}
					}
					if (num13 > 0)
					{
						if (num13 % 2 == 1)
						{
							value.X += 12f;
						}
						else
						{
							value.X -= 12f;
						}
						if (num14 % 2 == 1)
						{
							value.Y -= 12f;
						}
					}
					if (num15 > 0 && num13 == 0)
					{
						if (num15 % 2 == 1)
						{
							value.X -= 12f;
						}
						else
						{
							value.X += 12f;
						}
					}
				}
				for (int num18 = 0; num18 < SmartCursorHelper._targets.Count; num18++)
				{
					float num19 = Vector2.Distance(new Vector2((float)SmartCursorHelper._targets[num18].Item1, (float)SmartCursorHelper._targets[num18].Item2) * 16f + Vector2.One * 8f, value);
					if (num12 == -1f || num19 < num12)
					{
						num12 = num19;
						tuple3 = SmartCursorHelper._targets[num18];
					}
				}
				if (Collision.InTileBounds(tuple3.Item1, tuple3.Item2, providedInfo.reachableStartX, providedInfo.reachableStartY, providedInfo.reachableEndX, providedInfo.reachableEndY))
				{
					focusedX = tuple3.Item1;
					focusedY = tuple3.Item2;
				}
			}
			SmartCursorHelper._points.Clear();
			SmartCursorHelper._endpoints.Clear();
			SmartCursorHelper._targets.Clear();
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x005A34DC File Offset: 0x005A16DC
		private unsafe static void Step_Axe(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int fX, ref int fY)
		{
			int reachableStartX = providedInfo.reachableStartX;
			int reachableStartY = providedInfo.reachableStartY;
			int reachableEndX = providedInfo.reachableEndX;
			int reachableEndY = providedInfo.reachableEndY;
			int screenTargetX = providedInfo.screenTargetX;
			int screenTargetY = providedInfo.screenTargetY;
			if (providedInfo.item.axe <= 0 || fX != -1 || fY != -1)
			{
				return;
			}
			float num = -1f;
			for (int i = reachableStartX; i <= reachableEndX; i++)
			{
				for (int j = reachableStartY; j <= reachableEndY; j++)
				{
					if (Main.tile[i, j].active())
					{
						Tile tile = Main.tile[i, j];
						if (Main.tileAxe[(int)(*tile.type)] && !TileID.Sets.IgnoreSmartCursorPriorityAxe[(int)(*tile.type)])
						{
							int num2 = i;
							int k = j;
							int type = (int)(*tile.type);
							if (TileID.Sets.IsATreeTrunk[type])
							{
								if (Collision.InTileBounds(num2 + 1, k, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
								{
									if (*Main.tile[num2, k].frameY >= 198 && *Main.tile[num2, k].frameX == 44)
									{
										num2++;
									}
									if (*Main.tile[num2, k].frameX == 66 && *Main.tile[num2, k].frameY <= 44)
									{
										num2++;
									}
									if (*Main.tile[num2, k].frameX == 44 && *Main.tile[num2, k].frameY >= 132 && *Main.tile[num2, k].frameY <= 176)
									{
										num2++;
									}
								}
								if (Collision.InTileBounds(num2 - 1, k, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
								{
									if (*Main.tile[num2, k].frameY >= 198 && *Main.tile[num2, k].frameX == 66)
									{
										num2--;
									}
									if (*Main.tile[num2, k].frameX == 88 && *Main.tile[num2, k].frameY >= 66 && *Main.tile[num2, k].frameY <= 110)
									{
										num2--;
									}
									if (*Main.tile[num2, k].frameX == 22 && *Main.tile[num2, k].frameY >= 132 && *Main.tile[num2, k].frameY <= 176)
									{
										num2--;
									}
								}
								while (Main.tile[num2, k].active() && (int)(*Main.tile[num2, k].type) == type && (int)(*Main.tile[num2, k + 1].type) == type && Collision.InTileBounds(num2, k + 1, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
								{
									k++;
								}
							}
							if (*tile.type == 80)
							{
								if (Collision.InTileBounds(num2 + 1, k, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
								{
									if (*Main.tile[num2, k].frameX == 54)
									{
										num2++;
									}
									if (*Main.tile[num2, k].frameX == 108 && *Main.tile[num2, k].frameY == 36)
									{
										num2++;
									}
								}
								if (Collision.InTileBounds(num2 - 1, k, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
								{
									if (*Main.tile[num2, k].frameX == 36)
									{
										num2--;
									}
									if (*Main.tile[num2, k].frameX == 108 && *Main.tile[num2, k].frameY == 18)
									{
										num2--;
									}
								}
								while (Main.tile[num2, k].active() && *Main.tile[num2, k].type == 80 && *Main.tile[num2, k + 1].type == 80 && Collision.InTileBounds(num2, k + 1, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
								{
									k++;
								}
							}
							if (*tile.type != 323)
							{
								if (*tile.type != 72)
								{
									goto IL_566;
								}
							}
							while (Main.tile[num2, k].active() && ((*Main.tile[num2, k].type == 323 && *Main.tile[num2, k + 1].type == 323) || (*Main.tile[num2, k].type == 72 && *Main.tile[num2, k + 1].type == 72)) && Collision.InTileBounds(num2, k + 1, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
							{
								k++;
							}
							IL_566:
							float num3 = Vector2.Distance(new Vector2((float)num2, (float)k) * 16f + Vector2.One * 8f, providedInfo.mouse);
							if (num == -1f || num3 < num)
							{
								num = num3;
								fX = num2;
								fY = k;
							}
						}
					}
				}
			}
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x005A3ABC File Offset: 0x005A1CBC
		private unsafe static void Step_BlocksFilling(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int fX, ref int fY)
		{
			if (!Player.SmartCursorSettings.SmartBlocksEnabled)
			{
				return;
			}
			int type = providedInfo.item.type;
			if (type < 0)
			{
				return;
			}
			int reachableStartX = providedInfo.reachableStartX;
			int reachableStartY = providedInfo.reachableStartY;
			int reachableEndX = providedInfo.reachableEndX;
			int reachableEndY = providedInfo.reachableEndY;
			int screenTargetX = providedInfo.screenTargetX;
			int screenTargetY = providedInfo.screenTargetY;
			if (Player.SmartCursorSettings.SmartBlocksEnabled || providedInfo.item.createTile <= -1 || type == 213 || type == 5295 || ItemID.Sets.GrassSeeds[type] || !Main.tileSolid[providedInfo.item.createTile] || Main.tileSolidTop[providedInfo.item.createTile] || Main.tileFrameImportant[providedInfo.item.createTile] || fX != -1 || fY != -1)
			{
				return;
			}
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
						if (!tile.active() || Main.tileCut[(int)(*tile.type)] || TileID.Sets.BreakableWhenPlacing[(int)(*tile.type)])
						{
							int num = 0;
							if (Main.tile[i - 1, j].active() && Main.tileSolid[(int)(*Main.tile[i - 1, j].type)] && !Main.tileSolidTop[(int)(*Main.tile[i - 1, j].type)])
							{
								num++;
							}
							if (Main.tile[i + 1, j].active() && Main.tileSolid[(int)(*Main.tile[i + 1, j].type)] && !Main.tileSolidTop[(int)(*Main.tile[i + 1, j].type)])
							{
								num++;
							}
							if (Main.tile[i, j - 1].active() && Main.tileSolid[(int)(*Main.tile[i, j - 1].type)] && !Main.tileSolidTop[(int)(*Main.tile[i, j - 1].type)])
							{
								num++;
							}
							if (Main.tile[i, j + 1].active() && Main.tileSolid[(int)(*Main.tile[i, j + 1].type)] && !Main.tileSolidTop[(int)(*Main.tile[i, j + 1].type)])
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

		// Token: 0x06003A03 RID: 14851 RVA: 0x005A3F3C File Offset: 0x005A213C
		private unsafe static void Step_Torch(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int fX, ref int fY)
		{
			int reachableStartX = providedInfo.reachableStartX;
			int reachableStartY = providedInfo.reachableStartY;
			int reachableEndX = providedInfo.reachableEndX;
			int reachableEndY = providedInfo.reachableEndY;
			int screenTargetX = providedInfo.screenTargetX;
			int screenTargetY = providedInfo.screenTargetY;
			if (providedInfo.item.createTile < 0 || !TileID.Sets.Torch[providedInfo.item.createTile] || fX != -1 || fY != -1)
			{
				return;
			}
			SmartCursorHelper._targets.Clear();
			bool flag = !ItemID.Sets.WaterTorches[providedInfo.player.BiomeTorchHoldStyle(providedInfo.item.type)];
			for (int i = reachableStartX; i <= reachableEndX; i++)
			{
				for (int j = reachableStartY; j <= reachableEndY; j++)
				{
					Tile tile = Main.tile[i, j];
					Tile tile2 = Main.tile[i - 1, j];
					Tile tile3 = Main.tile[i + 1, j];
					Tile tile4 = Main.tile[i, j + 1];
					if (!tile.active() || TileID.Sets.BreakableWhenPlacing[(int)(*tile.type)] || (Main.tileCut[(int)(*tile.type)] && *tile.type != 82 && *tile.type != 83))
					{
						bool flag2 = false;
						for (int k = i - 8; k <= i + 8; k++)
						{
							for (int l = j - 8; l <= j + 8; l++)
							{
								if (Main.tile[k, l] != null && TileID.Sets.Torch[(int)(*Main.tile[k, l].type)])
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
						if (!flag2 && (!flag || *tile.liquid <= 0) && (*tile.wall > 0 || (tile2.active() && (tile2.slope() == 0 || tile2.slope() % 2 != 1) && ((Main.tileSolid[(int)(*tile2.type)] && !Main.tileNoAttach[(int)(*tile2.type)] && !Main.tileSolidTop[(int)(*tile2.type)] && !TileID.Sets.NotReallySolid[(int)(*tile2.type)]) || TileID.Sets.IsBeam[(int)(*tile2.type)] || (WorldGen.IsTreeType((int)(*tile2.type)) && WorldGen.IsTreeType((int)(*Main.tile[i - 1, j - 1].type)) && WorldGen.IsTreeType((int)(*Main.tile[i - 1, j + 1].type))))) || (tile3.active() && (tile3.slope() == 0 || tile3.slope() % 2 != 0) && ((Main.tileSolid[(int)(*tile3.type)] && !Main.tileNoAttach[(int)(*tile3.type)] && !Main.tileSolidTop[(int)(*tile3.type)] && !TileID.Sets.NotReallySolid[(int)(*tile3.type)]) || TileID.Sets.IsBeam[(int)(*tile3.type)] || (WorldGen.IsTreeType((int)(*tile3.type)) && WorldGen.IsTreeType((int)(*Main.tile[i + 1, j - 1].type)) && WorldGen.IsTreeType((int)(*Main.tile[i + 1, j + 1].type))))) || (tile4.active() && Main.tileSolid[(int)(*tile4.type)] && !Main.tileNoAttach[(int)(*tile4.type)] && (!Main.tileSolidTop[(int)(*tile4.type)] || (TileID.Sets.Platforms[(int)(*tile4.type)] && tile4.slope() == 0)) && !TileID.Sets.NotReallySolid[(int)(*tile4.type)] && !tile4.halfBrick() && tile4.slope() == 0)) && !TileID.Sets.Torch[(int)(*tile.type)])
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

		// Token: 0x06003A04 RID: 14852 RVA: 0x005A4440 File Offset: 0x005A2640
		private unsafe static void Step_LawnMower(SmartCursorHelper.SmartCursorUsageInfo providedInfo, ref int fX, ref int fY)
		{
			int reachableStartX = providedInfo.reachableStartX;
			int reachableStartY = providedInfo.reachableStartY;
			int reachableEndX = providedInfo.reachableEndX;
			int reachableEndY = providedInfo.reachableEndY;
			int screenTargetX = providedInfo.screenTargetX;
			int screenTargetY = providedInfo.screenTargetY;
			if (providedInfo.item.type != 4049 || fX != -1 || fY != -1)
			{
				return;
			}
			SmartCursorHelper._targets.Clear();
			for (int i = reachableStartX; i <= reachableEndX; i++)
			{
				for (int j = reachableStartY; j <= reachableEndY; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile.active() && (*tile.type == 2 || *tile.type == 109))
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

		// Token: 0x04005291 RID: 21137
		private static List<Tuple<int, int>> _targets = new List<Tuple<int, int>>();

		// Token: 0x04005292 RID: 21138
		private static List<Tuple<int, int>> _grappleTargets = new List<Tuple<int, int>>();

		// Token: 0x04005293 RID: 21139
		private static List<Tuple<int, int>> _points = new List<Tuple<int, int>>();

		// Token: 0x04005294 RID: 21140
		private static List<Tuple<int, int>> _endpoints = new List<Tuple<int, int>>();

		// Token: 0x04005295 RID: 21141
		private static List<Tuple<int, int>> _toRemove = new List<Tuple<int, int>>();

		// Token: 0x04005296 RID: 21142
		private static List<Tuple<int, int>> _targets2 = new List<Tuple<int, int>>();

		// Token: 0x02000BBB RID: 3003
		private class SmartCursorUsageInfo
		{
			// Token: 0x040076F7 RID: 30455
			public Player player;

			// Token: 0x040076F8 RID: 30456
			public Item item;

			// Token: 0x040076F9 RID: 30457
			public Vector2 mouse;

			// Token: 0x040076FA RID: 30458
			public Vector2 position;

			// Token: 0x040076FB RID: 30459
			public Vector2 Center;

			// Token: 0x040076FC RID: 30460
			public int screenTargetX;

			// Token: 0x040076FD RID: 30461
			public int screenTargetY;

			// Token: 0x040076FE RID: 30462
			public int reachableStartX;

			// Token: 0x040076FF RID: 30463
			public int reachableEndX;

			// Token: 0x04007700 RID: 30464
			public int reachableStartY;

			// Token: 0x04007701 RID: 30465
			public int reachableEndY;

			// Token: 0x04007702 RID: 30466
			public int paintLookup;

			// Token: 0x04007703 RID: 30467
			public int paintCoatingLookup;
		}

		// Token: 0x02000BBC RID: 3004
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04007704 RID: 30468
			public static Utils.TileActionAttempt <0>__NotDoorStand;
		}
	}
}
