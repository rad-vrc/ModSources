using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.ModChanges
{
	// Token: 0x02000252 RID: 594
	public class ModdedReplaceHerbs : ModPlayer
	{
		// Token: 0x06000DF0 RID: 3568 RVA: 0x0006FB68 File Offset: 0x0006DD68
		public override void PostItemCheck()
		{
			if (QoLCompendium.mainConfig.RegrowthAutoReplant && base.Player.controlUseItem && (base.Player.HeldItem.type == 213 || base.Player.HeldItem.type == 5295 || (Main.mouseItem != null && (Main.mouseItem.type == 213 || Main.mouseItem.type == 5295))))
			{
				ModdedReplaceHerbs.GetHerbDrops(Main.tile[Player.tileTargetX, Player.tileTargetY]);
			}
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0006FC00 File Offset: 0x0006DE00
		public unsafe static void GetHerbDrops(Tile tile)
		{
			if (!tile.HasTile)
			{
				return;
			}
			int stage = (int)(*tile.TileFrameX / 18);
			if (ModConditions.depthsLoaded && (int)(*tile.TileType) == Common.GetModTile(ModConditions.depthsMod, "ShadowShrub"))
			{
				ModdedReplaceHerbs.DropItems(stage, Common.GetModItem(ModConditions.depthsMod, "ShadowShrub"), Common.GetModItem(ModConditions.depthsMod, "ShadowShrubSeeds"));
				ModdedReplaceHerbs.ResetTileFrame(tile);
			}
			if (ModConditions.redemptionLoaded && (int)(*tile.TileType) == Common.GetModTile(ModConditions.redemptionMod, "NightshadeTile"))
			{
				ModdedReplaceHerbs.DropItems(stage, Common.GetModItem(ModConditions.redemptionMod, "Nightshade"), Common.GetModItem(ModConditions.redemptionMod, "NightshadeSeeds"));
				ModdedReplaceHerbs.ResetTileFrame(tile);
			}
			if (ModConditions.shadowsOfAbaddonLoaded)
			{
				if ((int)(*tile.TileType) == Common.GetModTile(ModConditions.shadowsOfAbaddonMod, "Welkinbell"))
				{
					ModdedReplaceHerbs.DropItems(stage, Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "Welkinbell"), Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "WelkinbellSeeds"));
					ModdedReplaceHerbs.ResetTileFrame(tile);
				}
				if ((int)(*tile.TileType) == Common.GetModTile(ModConditions.shadowsOfAbaddonMod, "Illumifern"))
				{
					ModdedReplaceHerbs.DropItems(stage, Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "Illumifern"), Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "IllumifernSeeds"));
					ModdedReplaceHerbs.ResetTileFrame(tile);
				}
				if ((int)(*tile.TileType) == Common.GetModTile(ModConditions.shadowsOfAbaddonMod, "Enduflora"))
				{
					ModdedReplaceHerbs.DropItems(stage, Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "Enduflora"), Common.GetModItem(ModConditions.shadowsOfAbaddonMod, "EndufloraSeeds"));
					ModdedReplaceHerbs.ResetTileFrame(tile);
				}
			}
			if (ModConditions.spiritLoaded)
			{
				if ((int)(*tile.TileType) == Common.GetModTile(ModConditions.spiritMod, "Cloudstalk"))
				{
					ModdedReplaceHerbs.DropItems(stage, Common.GetModItem(ModConditions.spiritMod, "CloudstalkItem"), Common.GetModItem(ModConditions.spiritMod, "CloudstalkSeed"));
					ModdedReplaceHerbs.ResetTileFrame(tile);
				}
				if ((int)(*tile.TileType) == Common.GetModTile(ModConditions.spiritMod, "SoulBloomTile"))
				{
					ModdedReplaceHerbs.DropItems(stage, Common.GetModItem(ModConditions.spiritMod, "SoulBloom"), Common.GetModItem(ModConditions.spiritMod, "SoulSeeds"));
					ModdedReplaceHerbs.ResetTileFrame(tile);
				}
			}
			if (ModConditions.thoriumLoaded && (int)(*tile.TileType) == Common.GetModTile(ModConditions.thoriumMod, "MarineKelp2"))
			{
				ModdedReplaceHerbs.DropItems(stage, Common.GetModItem(ModConditions.thoriumMod, "MarineKelp"), Common.GetModItem(ModConditions.thoriumMod, "MarineKelpSeeds"));
				ModdedReplaceHerbs.ResetTileFrame(tile);
			}
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0006FE64 File Offset: 0x0006E064
		public static void DropItems(int stage, int herbID, int seedID)
		{
			if (stage < 1)
			{
				return;
			}
			int herbDropCount = 0;
			int seedDropCount = 0;
			if (stage == 1)
			{
				herbDropCount = 1;
				seedDropCount = Main.rand.Next(1, 3);
			}
			if (stage == 2)
			{
				herbDropCount = Main.rand.Next(1, 3);
				seedDropCount = Main.rand.Next(1, 6);
			}
			Item.NewItem(new EntitySource_TileBreak(Player.tileTargetX, Player.tileTargetY, null), new Vector2((float)(Player.tileTargetX * 16), (float)(Player.tileTargetY * 16)), 8, 8, herbID, herbDropCount, false, 0, false, false);
			Item.NewItem(new EntitySource_TileBreak(Player.tileTargetX, Player.tileTargetY, null), new Vector2((float)(Player.tileTargetX * 16), (float)(Player.tileTargetY * 16)), 8, 8, seedID, seedDropCount, false, 0, false, false);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0006FF18 File Offset: 0x0006E118
		public unsafe static void ResetTileFrame(Tile tile)
		{
			*tile.TileFrameX = 0;
			NetMessage.SendData(17, -1, -1, null, 1, (float)Player.tileTargetX, (float)Player.tileTargetY, (float)(*tile.TileType), 0, 0, 0);
		}

		// Token: 0x040005B2 RID: 1458
		public const int GrowthSize = 18;
	}
}
