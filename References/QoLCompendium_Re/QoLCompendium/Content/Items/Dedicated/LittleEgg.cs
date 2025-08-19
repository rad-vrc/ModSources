using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using QoLCompendium.Content.Buffs;
using QoLCompendium.Content.Projectiles.Dedicated;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Dedicated
{
	// Token: 0x020001F4 RID: 500
	public class LittleEgg : ModItem
	{
		// Token: 0x06000B17 RID: 2839 RVA: 0x00020AFF File Offset: 0x0001ECFF
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.DedicatedItems;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00020EE4 File Offset: 0x0001F0E4
		public override void SetDefaults()
		{
			base.Item.CloneDefaults(2420);
			base.Item.shoot = ModContent.ProjectileType<LittleYagi>();
			base.Item.buffType = ModContent.BuffType<LittleYagiBuff>();
			base.Item.SetShopValues(ItemRarityColor.StrongRed10, Item.buyPrice(0, 5, 0, 0));
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00020CA7 File Offset: 0x0001EEA7
		public override bool? UseItem(Player player)
		{
			if (player.whoAmI == Main.myPlayer)
			{
				player.AddBuff(base.Item.buffType, 3600, true, false);
			}
			return new bool?(true);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00020F37 File Offset: 0x0001F137
		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(ModContent.BuffType<LittleYagiBuff>()) && base.CanUseItem(player);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00020F50 File Offset: 0x0001F150
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine dedicated = new TooltipLine(base.Mod, "Dedicated", Language.GetTextValue("Mods.QoLCompendium.DedicatedTooltips.Jay"))
			{
				OverrideColor = new Color?(Common.ColorSwap(Color.LightSeaGreen, Color.Aquamarine, 3f))
			};
			tooltips.Add(dedicated);
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.DedicatedItems);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00020FB4 File Offset: 0x0001F1B4
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.DedicatedItems, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(1809, 1);
			itemRecipe.AddIngredient(66, 10);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
