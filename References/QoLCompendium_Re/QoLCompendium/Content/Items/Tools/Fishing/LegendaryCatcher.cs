using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using QoLCompendium.Content.Projectiles.Fishing;
using QoLCompendium.Core;
using QoLCompendium.Core.Changes.TooltipChanges;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Items.Tools.Fishing
{
	// Token: 0x020001B3 RID: 435
	public class LegendaryCatcher : ModItem
	{
		// Token: 0x0600094D RID: 2381 RVA: 0x0001C1B0 File Offset: 0x0001A3B0
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.LegendaryCatcher;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0001C1CA File Offset: 0x0001A3CA
		public override void SetStaticDefaults()
		{
			ItemID.Sets.CanFishInLava[base.Item.type] = true;
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0001C1EC File Offset: 0x0001A3EC
		public override void SetDefaults()
		{
			base.Item.useStyle = 1;
			base.Item.useAnimation = 8;
			base.Item.useTime = 8;
			base.Item.width = 24;
			base.Item.height = 15;
			base.Item.UseSound = new SoundStyle?(SoundID.Item1);
			base.Item.fishingPole = 200;
			base.Item.shootSpeed = 15f;
			base.Item.shoot = ModContent.ProjectileType<LegendaryBobber>();
			base.Item.SetShopValues(ItemRarityColor.StrongRed10, Item.buyPrice(0, 10, 0, 0));
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0001C293 File Offset: 0x0001A493
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.LegendaryCatcher);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0001C2AB File Offset: 0x0001A4AB
		public override void HoldItem(Player player)
		{
			player.accFishingLine = true;
			if (player.ZoneRain)
			{
				player.fishingSkill += 100;
			}
			if (!Main.dayTime)
			{
				player.fishingSkill += 50;
			}
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0001C2E4 File Offset: 0x0001A4E4
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int castAmount = 50;
			if (player.ZoneRain)
			{
				castAmount += 20;
			}
			if (!Main.dayTime)
			{
				castAmount += 10;
			}
			float castShootSpeed = 75f;
			for (int i = 0; i < castAmount; i++)
			{
				float speedx = velocity.X + Main.rand.NextFloat(0f - castShootSpeed, castShootSpeed) * 0.05f;
				float speedy = velocity.Y + Main.rand.NextFloat(0f - castShootSpeed, castShootSpeed) * 0.05f;
				Projectile.NewProjectile(source, position.X, position.Y, speedx, speedy, type, 0, 0f, player.whoAmI, 0f, 0f, 0f);
			}
			return false;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0001C398 File Offset: 0x0001A598
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.LegendaryCatcher, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddRecipeGroup(RecipeGroupID.IronBar, 12);
			itemRecipe.AddIngredient(999, 6);
			itemRecipe.AddIngredient(74, 1);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}
	}
}
