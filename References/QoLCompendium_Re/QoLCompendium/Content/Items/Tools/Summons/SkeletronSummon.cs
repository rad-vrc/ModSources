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
	// Token: 0x0200018E RID: 398
	public class SkeletronSummon : ModItem
	{
		// Token: 0x06000839 RID: 2105 RVA: 0x00017610 File Offset: 0x00015810
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.BossSummons;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00017D03 File Offset: 0x00015F03
		public override void SetStaticDefaults()
		{
			ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 5;
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00017D20 File Offset: 0x00015F20
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
			base.Item.SetShopValues(ItemRarityColor.Orange3, Item.sellPrice(0, 0, 0, 0));
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00017DAE File Offset: 0x00015FAE
		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(35);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00017DBC File Offset: 0x00015FBC
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 spawnPosition = player.Center - Vector2.UnitY * 800f;
			Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), spawnPosition, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0f, player.whoAmI, 35f, 0f, 0f);
			SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
			return false;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00017771 File Offset: 0x00015971
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.BossSummons);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00017E38 File Offset: 0x00016038
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.BossSummons, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(225, 5);
			itemRecipe.AddIngredient(1115, 1);
			itemRecipe.AddTile(26);
			itemRecipe.Register();
		}
	}
}
