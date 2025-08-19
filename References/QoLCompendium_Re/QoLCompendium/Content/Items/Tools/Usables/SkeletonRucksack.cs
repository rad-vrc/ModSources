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

namespace QoLCompendium.Content.Items.Tools.Usables
{
	// Token: 0x02000183 RID: 387
	public class SkeletonRucksack : ModItem
	{
		// Token: 0x060007E3 RID: 2019 RVA: 0x00015E1E File Offset: 0x0001401E
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.SkeletonRucksack;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00015E38 File Offset: 0x00014038
		public override void SetDefaults()
		{
			base.Item.width = 13;
			base.Item.height = 17;
			base.Item.useStyle = 4;
			base.Item.UseSound = new SoundStyle?(SoundID.Item4);
			base.Item.useAnimation = 20;
			base.Item.useTime = 20;
			base.Item.shoot = ModContent.ProjectileType<NPCSpawner>();
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 0, 75, 0));
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00015EC0 File Offset: 0x000140C0
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.SkeletonRucksack);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00015ED8 File Offset: 0x000140D8
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.SkeletonRucksack, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(259, 8);
			itemRecipe.AddTile(86);
			itemRecipe.Register();
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00015F34 File Offset: 0x00014134
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 spawnPosition = player.Center - Vector2.UnitY * 800f;
			Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), spawnPosition, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0f, Main.myPlayer, 453f, 0f, 0f);
			SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
			return false;
		}
	}
}
