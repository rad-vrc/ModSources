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
	// Token: 0x0200018D RID: 397
	public class PlanteraSummon : ModItem
	{
		// Token: 0x06000831 RID: 2097 RVA: 0x00017610 File Offset: 0x00015810
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.BossSummons;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00017B35 File Offset: 0x00015D35
		public override void SetStaticDefaults()
		{
			ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 13;
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00017B54 File Offset: 0x00015D54
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
			base.Item.SetShopValues(ItemRarityColor.LightPurple6, Item.sellPrice(0, 0, 0, 0));
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00017BE2 File Offset: 0x00015DE2
		public override bool CanUseItem(Player player)
		{
			return Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && !NPC.AnyNPCs(262);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00017C10 File Offset: 0x00015E10
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 spawnPosition = player.Center - Vector2.UnitY * 800f;
			Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), spawnPosition, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0f, player.whoAmI, 262f, 0f, 0f);
			SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
			return false;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00017771 File Offset: 0x00015971
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.BossSummons);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00017C8C File Offset: 0x00015E8C
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.BossSummons, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(176, 10);
			itemRecipe.AddIngredient(314, 3);
			itemRecipe.AddIngredient(947, 5);
			itemRecipe.AddTile(134);
			itemRecipe.Register();
		}
	}
}
