using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Mirrors
{
	// Token: 0x020001A8 RID: 424
	public class WormholeMirror : ModItem
	{
		// Token: 0x0600090B RID: 2315 RVA: 0x0001A84F File Offset: 0x00018A4F
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.Mirrors;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0001A869 File Offset: 0x00018A69
		public override void SetDefaults()
		{
			base.Item.CloneDefaults(50);
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 2, 0, 0));
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0001A88D File Offset: 0x00018A8D
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.Mirrors);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0000404D File Offset: 0x0000224D
		public override bool CanUseItem(Player player)
		{
			return false;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0001B7EC File Offset: 0x000199EC
		public override void Load()
		{
			On_Player.hook_HasUnityPotion hook_HasUnityPotion;
			if ((hook_HasUnityPotion = WormholeMirror.<>O.<0>__Player_HasUnityPotion) == null)
			{
				hook_HasUnityPotion = (WormholeMirror.<>O.<0>__Player_HasUnityPotion = new On_Player.hook_HasUnityPotion(WormholeMirror.Player_HasUnityPotion));
			}
			On_Player.HasUnityPotion += hook_HasUnityPotion;
			On_Player.hook_TakeUnityPotion hook_TakeUnityPotion;
			if ((hook_TakeUnityPotion = WormholeMirror.<>O.<1>__Player_TakeUnityPotion) == null)
			{
				hook_TakeUnityPotion = (WormholeMirror.<>O.<1>__Player_TakeUnityPotion = new On_Player.hook_TakeUnityPotion(WormholeMirror.Player_TakeUnityPotion));
			}
			On_Player.TakeUnityPotion += hook_TakeUnityPotion;
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0001B839 File Offset: 0x00019A39
		private static void Player_TakeUnityPotion(On_Player.orig_TakeUnityPotion orig, Player self)
		{
			if (self.HasItem(ModContent.ItemType<WormholeMirror>()) || self.HasItem(ModContent.ItemType<MosaicMirror>()))
			{
				return;
			}
			orig.Invoke(self);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0001B85D File Offset: 0x00019A5D
		private static bool Player_HasUnityPotion(On_Player.orig_HasUnityPotion orig, Player self)
		{
			return self.HasItem(ModContent.ItemType<WormholeMirror>()) || self.HasItem(ModContent.ItemType<MosaicMirror>()) || orig.Invoke(self);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0001B884 File Offset: 0x00019A84
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.Mirrors, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(170, 10);
			itemRecipe.AddRecipeGroup("QoLCompendium:GoldBars", 8);
			itemRecipe.AddIngredient(2997, 3);
			itemRecipe.AddTile(17);
			itemRecipe.Register();
		}

		// Token: 0x020004E2 RID: 1250
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000A28 RID: 2600
			public static On_Player.hook_HasUnityPotion <0>__Player_HasUnityPotion;

			// Token: 0x04000A29 RID: 2601
			public static On_Player.hook_TakeUnityPotion <1>__Player_TakeUnityPotion;
		}
	}
}
