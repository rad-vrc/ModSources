using System;
using System.Collections.Generic;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Usables
{
	// Token: 0x02000188 RID: 392
	public class UltimateChecklist : ModItem
	{
		// Token: 0x0600080A RID: 2058 RVA: 0x00017314 File Offset: 0x00015514
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.UltimateChecklist;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00017330 File Offset: 0x00015530
		public override void SetDefaults()
		{
			base.Item.width = 12;
			base.Item.height = 15;
			base.Item.useStyle = 4;
			base.Item.UseSound = new SoundStyle?(SoundID.Item4);
			base.Item.useAnimation = 20;
			base.Item.useTime = 20;
			base.Item.SetShopValues(ItemRarityColor.TrashMinus1, Item.buyPrice(0, 0, 0, 0));
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00002430 File Offset: 0x00000630
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x000173A8 File Offset: 0x000155A8
		public override bool? UseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				for (int i = 0; i < NPCLoader.NPCCount; i++)
				{
					string persistentId = ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[i];
					Main.BestiaryTracker.Kills.SetKillCountDirectly(persistentId, 50);
					Main.BestiaryTracker.Sights.SetWasSeenDirectly(persistentId);
				}
			}
			else
			{
				for (int j = 0; j < ItemLoader.ItemCount; j++)
				{
					CreativeUI.ResearchItem(j);
				}
			}
			return new bool?(true);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001741B File Offset: 0x0001561B
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.UltimateChecklist);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00017434 File Offset: 0x00015634
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.UltimateChecklist, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(3467, 12);
			itemRecipe.AddIngredient(225, 8);
			itemRecipe.AddIngredient(1050, 1);
			itemRecipe.AddTile(412);
			itemRecipe.Register();
		}
	}
}
