using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Summons
{
	// Token: 0x0200018F RID: 399
	public class WallOfFleshSummon : ModItem
	{
		// Token: 0x06000841 RID: 2113 RVA: 0x00017610 File Offset: 0x00015810
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.BossSummons;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00017E9E File Offset: 0x0001609E
		public override void SetStaticDefaults()
		{
			ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 6;
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00017EBC File Offset: 0x000160BC
		public override void SetDefaults()
		{
			base.Item.width = 11;
			base.Item.height = 14;
			base.Item.maxStack = Item.CommonMaxStack;
			base.Item.useAnimation = 30;
			base.Item.useTime = 30;
			base.Item.useStyle = 4;
			base.Item.consumable = true;
			base.Item.SetShopValues(ItemRarityColor.Orange3, Item.sellPrice(0, 0, 0, 0));
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00017F3A File Offset: 0x0001613A
		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(113);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00017F46 File Offset: 0x00016146
		public override bool? UseItem(Player player)
		{
			NPC.SpawnWOF(player.Center);
			SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
			return new bool?(true);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00017771 File Offset: 0x00015971
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.BossSummons);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00017F70 File Offset: 0x00016170
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.BossSummons, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(5277, 1);
			itemRecipe.AddIngredient(154, 5);
			itemRecipe.AddTile(26);
			itemRecipe.Register();
		}
	}
}
