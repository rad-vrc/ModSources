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
	// Token: 0x02000192 RID: 402
	public class LeviathanAnahitaSummon : ModItem
	{
		// Token: 0x06000859 RID: 2137 RVA: 0x00017610 File Offset: 0x00015810
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.BossSummons;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00017B35 File Offset: 0x00015D35
		public override void SetStaticDefaults()
		{
			ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 13;
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x000183D4 File Offset: 0x000165D4
		public override void SetDefaults()
		{
			base.Item.width = 11;
			base.Item.height = 11;
			base.Item.maxStack = Item.CommonMaxStack;
			base.Item.useAnimation = 30;
			base.Item.useTime = 30;
			base.Item.useStyle = 4;
			base.Item.consumable = true;
			base.Item.shoot = ModContent.ProjectileType<NPCSpawner>();
			base.Item.SetShopValues(ItemRarityColor.Pink5, Item.sellPrice(0, 0, 0, 0));
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00018462 File Offset: 0x00016662
		public override bool CanUseItem(Player player)
		{
			return ModConditions.calamityLoaded && NPC.downedPlantBoss && player.ZoneBeach && !NPC.AnyNPCs(Common.GetModNPC(ModConditions.calamityMod, "Anahita"));
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00018494 File Offset: 0x00016694
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 spawnPosition = player.Center - Vector2.UnitY * 800f;
			Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), spawnPosition, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0f, player.whoAmI, (float)Common.GetModNPC(ModConditions.calamityMod, "Anahita"), 0f, 0f);
			SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
			return false;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001811C File Offset: 0x0001631C
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.CrossModItems);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00018518 File Offset: 0x00016718
		public override void AddRecipes()
		{
			if (!ModConditions.calamityLoaded)
			{
				return;
			}
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.CrossModItems, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(4412, 3);
			itemRecipe.AddIngredient(1508, 5);
			itemRecipe.AddTile(134);
			itemRecipe.Register();
		}
	}
}
