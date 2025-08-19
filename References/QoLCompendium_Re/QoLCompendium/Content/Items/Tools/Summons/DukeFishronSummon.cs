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
	// Token: 0x0200018B RID: 395
	public class DukeFishronSummon : ModItem
	{
		// Token: 0x06000821 RID: 2081 RVA: 0x00017610 File Offset: 0x00015810
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.BossSummons;
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x000177F5 File Offset: 0x000159F5
		public override void SetStaticDefaults()
		{
			ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 15;
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00017814 File Offset: 0x00015A14
		public override void SetDefaults()
		{
			base.Item.width = 14;
			base.Item.height = 15;
			base.Item.maxStack = Item.CommonMaxStack;
			base.Item.useAnimation = 30;
			base.Item.useTime = 30;
			base.Item.useStyle = 4;
			base.Item.consumable = true;
			base.Item.shoot = ModContent.ProjectileType<NPCSpawner>();
			base.Item.SetShopValues(ItemRarityColor.Yellow8, Item.sellPrice(0, 0, 0, 0));
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x000178A2 File Offset: 0x00015AA2
		public override bool CanUseItem(Player player)
		{
			return Main.hardMode && !NPC.AnyNPCs(370);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000178BC File Offset: 0x00015ABC
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 spawnPosition = player.Center - Vector2.UnitY * 800f;
			Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), spawnPosition, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0f, player.whoAmI, 370f, 0f, 0f);
			SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
			return false;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00017771 File Offset: 0x00015971
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.BossSummons);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00017938 File Offset: 0x00015B38
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.BossSummons, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(2673, 1);
			itemRecipe.AddIngredient(356, 1);
			itemRecipe.AddTile(26);
			itemRecipe.Register();
		}
	}
}
