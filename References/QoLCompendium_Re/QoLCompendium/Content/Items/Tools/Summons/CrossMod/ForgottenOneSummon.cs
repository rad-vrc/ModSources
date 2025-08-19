using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using QoLCompendium.Content.Projectiles.Other;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Summons.CrossMod
{
	// Token: 0x02000190 RID: 400
	public class ForgottenOneSummon : ModItem
	{
		// Token: 0x06000849 RID: 2121 RVA: 0x00017610 File Offset: 0x00015810
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.BossSummons;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00017B35 File Offset: 0x00015D35
		public override void SetStaticDefaults()
		{
			ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 13;
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00017FD8 File Offset: 0x000161D8
		public override void SetDefaults()
		{
			base.Item.width = 11;
			base.Item.height = 12;
			base.Item.maxStack = Item.CommonMaxStack;
			base.Item.useAnimation = 30;
			base.Item.useTime = 30;
			base.Item.useStyle = 4;
			base.Item.consumable = true;
			base.Item.shoot = ModContent.ProjectileType<NPCSpawner>();
			base.Item.SetShopValues(ItemRarityColor.Pink5, Item.sellPrice(0, 0, 0, 0));
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00018066 File Offset: 0x00016266
		public override bool CanUseItem(Player player)
		{
			return ModConditions.thoriumLoaded && NPC.downedPlantBoss && player.ZoneBeach && !NPC.AnyNPCs(Common.GetModNPC(ModConditions.thoriumMod, "ForgottenOne"));
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00018098 File Offset: 0x00016298
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 spawnPosition = player.Center - Vector2.UnitY * 800f;
			Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), spawnPosition, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0f, player.whoAmI, (float)Common.GetModNPC(ModConditions.thoriumMod, "ForgottenOne"), 0f, 0f);
			SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
			return false;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001811C File Offset: 0x0001631C
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.CrossModItems);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00018134 File Offset: 0x00016334
		public override void AddRecipes()
		{
			if (!ModConditions.thoriumLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.CrossModItems, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.thoriumMod, "MarineBlock"), 12);
			itemRecipe.AddIngredient(Common.GetModItem(ModConditions.thoriumMod, "MossyMarineBlock"), 12);
			itemRecipe.AddIngredient(1508, 5);
			itemRecipe.AddTile(134);
			itemRecipe.Register();
		}
	}
}
