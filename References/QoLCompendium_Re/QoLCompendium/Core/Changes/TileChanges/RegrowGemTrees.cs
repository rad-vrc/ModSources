using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace QoLCompendium.Core.Changes.TileChanges
{
	// Token: 0x0200021F RID: 543
	public class RegrowGemTrees : ModSystem
	{
		// Token: 0x06000D2E RID: 3374 RVA: 0x00066F81 File Offset: 0x00065181
		public override void Load()
		{
			On_Player.ItemCheck_UseMiningTools_ActuallyUseMiningTool += new On_Player.hook_ItemCheck_UseMiningTools_ActuallyUseMiningTool(this.OnPlayerMiningToolUse);
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00066F94 File Offset: 0x00065194
		private unsafe void OnPlayerMiningToolUse(On_Player.orig_ItemCheck_UseMiningTools_ActuallyUseMiningTool orig, Player self, Item sItem, out bool canHitWalls, int x, int y)
		{
			if (!QoLCompendium.mainConfig.RegrowthAutoReplant)
			{
				orig.Invoke(self, sItem, ref canHitWalls, x, y);
				return;
			}
			Tile tile = Main.tile[x, y];
			int cachedTileType = (int)(*tile.TileType);
			int treeX;
			int treeY;
			WorldGen.GetTreeBottom(x, y, out treeX, out treeY);
			orig.Invoke(self, sItem, ref canHitWalls, x, y);
			if (x != treeX || y != treeY - 1)
			{
				return;
			}
			if (*tile.TileType != 0)
			{
				return;
			}
			Player player = RegrowGemTrees.GetPlayerForTile(x, y);
			if (RegrowGemTrees.ShouldReplantGemcorn(cachedTileType, player))
			{
				RegrowGemTrees.GetSeedItemFromGemTile(cachedTileType);
				int gemStyle = RegrowGemTrees.GetGemTreeStyleFromTile(cachedTileType);
				RegrowGemTrees.TryReplantingGemTree(self, x, y, gemStyle);
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00067038 File Offset: 0x00065238
		private static void TryReplantingGemTree(Player player, int x, int y, int gemStyle = 0)
		{
			int type = 590;
			int style = gemStyle;
			PlantLoader.CheckAndInjectModSapling(x, y, ref type, ref style);
			TileObject objectData;
			if (!TileObject.CanPlace(Player.tileTargetX, Player.tileTargetY, type, style, player.direction, out objectData, false, null, false))
			{
				return;
			}
			bool flag = TileObject.Place(objectData);
			WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, true);
			if (flag)
			{
				TileObjectData.CallPostPlacementPlayerHook(Player.tileTargetX, Player.tileTargetY, type, style, player.direction, objectData.alternate, objectData);
				if (Main.netMode == 1)
				{
					NetMessage.SendObjectPlacement(-1, Player.tileTargetX, Player.tileTargetY, objectData.type, objectData.style, objectData.alternate, objectData.random, player.direction);
				}
			}
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x000670EC File Offset: 0x000652EC
		public static bool ShouldReplantGemcorn(int tileType, Player player)
		{
			return player != null && QoLCompendium.mainConfig.RegrowthAutoReplant && player.inventory[player.selectedItem].type == 5295 && TileID.Sets.CountsAsGemTree[tileType];
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00067128 File Offset: 0x00065328
		private static Player GetPlayerForTile(int x, int y)
		{
			return Main.player[(int)Player.FindClosest(new Vector2((float)x, (float)y) * 16f, 16, 16)];
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0006714C File Offset: 0x0006534C
		private static int GetGemTreeStyleFromTile(int tileType)
		{
			switch (tileType)
			{
			case 583:
				return 0;
			case 584:
				return 1;
			case 585:
				return 2;
			case 586:
				return 3;
			case 587:
				return 4;
			case 588:
				return 5;
			case 589:
				return 6;
			default:
				return -1;
			}
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00067188 File Offset: 0x00065388
		public static int GetSeedItemFromGemTile(int tileType)
		{
			switch (tileType)
			{
			case 583:
				return 4851;
			case 584:
				return 4852;
			case 585:
				return 4853;
			case 586:
				return 4854;
			case 587:
				return 4855;
			case 588:
				return 4856;
			case 589:
				return 4857;
			default:
				return -1;
			}
		}

		// Token: 0x02000545 RID: 1349
		public enum GemTreeStyle
		{
			// Token: 0x04000EE5 RID: 3813
			Topaz,
			// Token: 0x04000EE6 RID: 3814
			Amethyst,
			// Token: 0x04000EE7 RID: 3815
			Sapphire,
			// Token: 0x04000EE8 RID: 3816
			Emerald,
			// Token: 0x04000EE9 RID: 3817
			Ruby,
			// Token: 0x04000EEA RID: 3818
			Diamond,
			// Token: 0x04000EEB RID: 3819
			Amber
		}
	}
}
