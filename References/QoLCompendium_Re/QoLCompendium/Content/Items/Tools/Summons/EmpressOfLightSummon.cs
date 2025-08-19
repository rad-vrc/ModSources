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

namespace QoLCompendium.Content.Items.Tools.Summons
{
	// Token: 0x0200018C RID: 396
	public class EmpressOfLightSummon : ModItem
	{
		// Token: 0x06000829 RID: 2089 RVA: 0x00017610 File Offset: 0x00015810
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.BossSummons;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x000177F5 File Offset: 0x000159F5
		public override void SetStaticDefaults()
		{
			ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 15;
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x000179A0 File Offset: 0x00015BA0
		public override void SetDefaults()
		{
			base.Item.width = 11;
			base.Item.height = 14;
			base.Item.maxStack = Item.CommonMaxStack;
			base.Item.useAnimation = 30;
			base.Item.useTime = 30;
			base.Item.useStyle = 4;
			base.Item.consumable = true;
			base.Item.shoot = ModContent.ProjectileType<NPCSpawner>();
			base.Item.SetShopValues(ItemRarityColor.Yellow8, Item.sellPrice(0, 0, 0, 0));
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00017A2E File Offset: 0x00015C2E
		public override bool CanUseItem(Player player)
		{
			return Main.hardMode && NPC.downedPlantBoss && !NPC.AnyNPCs(636);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00017A50 File Offset: 0x00015C50
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 spawnPosition = player.Center - Vector2.UnitY * 800f;
			Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), spawnPosition, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0f, player.whoAmI, 636f, 0f, 0f);
			SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
			return false;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00017771 File Offset: 0x00015971
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.BossSummons);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00017ACC File Offset: 0x00015CCC
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.BossSummons, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(502, 5);
			itemRecipe.AddIngredient(1508, 3);
			itemRecipe.AddTile(134);
			itemRecipe.Register();
		}
	}
}
