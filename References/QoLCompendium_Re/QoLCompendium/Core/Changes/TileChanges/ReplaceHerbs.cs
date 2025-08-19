using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.TileChanges
{
	// Token: 0x02000220 RID: 544
	public class ReplaceHerbs : ModPlayer
	{
		// Token: 0x06000D36 RID: 3382 RVA: 0x000671EC File Offset: 0x000653EC
		public override void PostItemCheck()
		{
			if (QoLCompendium.mainConfig.RegrowthAutoReplant && base.Player.controlUseItem && (base.Player.HeldItem.type == 213 || base.Player.HeldItem.type == 5295 || (Main.mouseItem != null && (Main.mouseItem.type == 213 || Main.mouseItem.type == 5295))))
			{
				ReplaceHerbs.GetHerbDrops(Main.tile[Player.tileTargetX, Player.tileTargetY]);
			}
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00067284 File Offset: 0x00065484
		public unsafe static void GetHerbDrops(Tile tile)
		{
			if (!tile.HasTile || *tile.TileType == 82)
			{
				return;
			}
			if (*tile.TileType == 83 || *tile.TileType == 84)
			{
				int herbStyle = (int)(*tile.TileFrameX / 18);
				if (herbStyle == 0)
				{
					ReplaceHerbs.DropItems(tile, 313, 307);
					ReplaceHerbs.ResetTileFrame(tile, herbStyle);
				}
				if (herbStyle == 1)
				{
					ReplaceHerbs.DropItems(tile, 314, 308);
					ReplaceHerbs.ResetTileFrame(tile, herbStyle);
				}
				if (herbStyle == 2)
				{
					ReplaceHerbs.DropItems(tile, 315, 309);
					ReplaceHerbs.ResetTileFrame(tile, herbStyle);
				}
				if (herbStyle == 3)
				{
					ReplaceHerbs.DropItems(tile, 316, 310);
					ReplaceHerbs.ResetTileFrame(tile, herbStyle);
				}
				if (herbStyle == 4)
				{
					ReplaceHerbs.DropItems(tile, 317, 311);
					ReplaceHerbs.ResetTileFrame(tile, herbStyle);
				}
				if (herbStyle == 5)
				{
					ReplaceHerbs.DropItems(tile, 318, 312);
					ReplaceHerbs.ResetTileFrame(tile, herbStyle);
				}
				if (herbStyle == 6)
				{
					ReplaceHerbs.DropItems(tile, 2358, 2357);
					ReplaceHerbs.ResetTileFrame(tile, herbStyle);
				}
			}
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0006738C File Offset: 0x0006558C
		public unsafe static void DropItems(Tile tile, int herbID, int seedID)
		{
			int herbDropCount = 0;
			int seedDropCount = 0;
			if (*tile.TileType == 83)
			{
				herbDropCount = 1;
				seedDropCount = Main.rand.Next(1, 3);
			}
			if (*tile.TileType == 84)
			{
				herbDropCount = Main.rand.Next(1, 3);
				seedDropCount = Main.rand.Next(1, 6);
			}
			Item.NewItem(new EntitySource_TileBreak(Player.tileTargetX, Player.tileTargetY, null), new Vector2((float)(Player.tileTargetX * 16), (float)(Player.tileTargetY * 16)), 8, 8, herbID, herbDropCount, false, 0, false, false);
			Item.NewItem(new EntitySource_TileBreak(Player.tileTargetX, Player.tileTargetY, null), new Vector2((float)(Player.tileTargetX * 16), (float)(Player.tileTargetY * 16)), 8, 8, seedID, seedDropCount, false, 0, false, false);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00067448 File Offset: 0x00065648
		public unsafe static void ResetTileFrame(Tile tile, int style)
		{
			*tile.TileType = 82;
			*tile.TileFrameX = (short)(18 * style);
			NetMessage.SendData(17, -1, -1, null, 1, (float)Player.tileTargetX, (float)Player.tileTargetY, 82f, style, 0, 0);
		}
	}
}
