using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using QoLCompendium.Content.NPCs;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Usables
{
	// Token: 0x02000186 RID: 390
	public class SuperDummy : ModItem
	{
		// Token: 0x060007FB RID: 2043 RVA: 0x00016EE9 File Offset: 0x000150E9
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.SuperDummy;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00016F04 File Offset: 0x00015104
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 30;
			base.Item.useTime = 15;
			base.Item.useAnimation = 15;
			base.Item.useStyle = 1;
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 0, 0, 0));
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00016F66 File Offset: 0x00015166
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.SuperDummy);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00002430 File Offset: 0x00000630
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00016F80 File Offset: 0x00015180
		public override bool? UseItem(Player player)
		{
			if (player.whoAmI == Main.myPlayer)
			{
				if (player.altFunctionUse == 2)
				{
					if (Main.netMode == 0)
					{
						for (int i = 0; i < Main.maxNPCs; i++)
						{
							if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<SuperDummy>())
							{
								NPC npc = Main.npc[i];
								npc.life = 0;
								npc.HitEffect(0, 10.0, null);
								Main.npc[i].SimpleStrikeNPC(int.MaxValue, 0, false, 0f, null, false, 0f, true);
							}
						}
					}
					else if (Main.netMode == 1)
					{
						ModPacket packet = base.Mod.GetPacket(256);
						packet.Write(5);
						packet.Send(-1, -1);
					}
				}
				else if (NPC.CountNPCS(ModContent.NPCType<SuperDummy>()) < 100)
				{
					Vector2 pos;
					pos..ctor((float)((int)Main.MouseWorld.X), (float)((int)Main.MouseWorld.Y));
					int dummy = NPC.NewNPC(NPC.GetBossSpawnSource(Main.myPlayer), (int)pos.X, (int)pos.Y, ModContent.NPCType<SuperDummy>(), 0, 0f, 0f, 0f, 0f, 255);
					if (dummy != Main.maxNPCs && Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, dummy, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
			return new bool?(true);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x000170FC File Offset: 0x000152FC
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.SuperDummy, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(3202, 1);
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 5);
			itemRecipe.AddIngredient(75, 1);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
