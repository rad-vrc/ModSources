using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Usables
{
	// Token: 0x0200017C RID: 380
	public class GoldenLockpick : ModItem
	{
		// Token: 0x060007B0 RID: 1968 RVA: 0x000151E4 File Offset: 0x000133E4
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.GoldenLockpick;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x000151FE File Offset: 0x000133FE
		public override void SetDefaults()
		{
			base.Item.CloneDefaults(327);
			base.Item.maxStack = 1;
			base.Item.SetShopValues(ItemRarityColor.White0, Item.buyPrice(0, 1, 75, 0));
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00015232 File Offset: 0x00013432
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.GoldenLockpick);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001524A File Offset: 0x0001344A
		public override void UpdateInventory(Player player)
		{
			player.GetModPlayer<QoLCPlayer>().HasGoldenLockpick = true;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00015258 File Offset: 0x00013458
		public static bool UseKey(Item[] inv, int slot, Player player, QoLCPlayer qPlayer)
		{
			if (inv[slot].type == 3085 && qPlayer.HasGoldenLockpick)
			{
				if (ItemLoader.ConsumeItem(inv[slot], player))
				{
					inv[slot].stack--;
				}
				if (inv[slot].stack < 0)
				{
					inv[slot].SetDefaults(0);
				}
				SoundEngine.PlaySound(SoundID.Unlock, null, null);
				Main.stackSplit = 30;
				Main.mouseRightRelease = false;
				player.OpenLockBox(inv[slot].type);
				Recipe.FindRecipes(false);
				return true;
			}
			return false;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x000152E4 File Offset: 0x000134E4
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.GoldenLockpick, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(327, 1);
			itemRecipe.AddIngredient(154, 25);
			itemRecipe.AddTile(283);
			itemRecipe.Register();
		}
	}
}
