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
	// Token: 0x02000187 RID: 391
	public class TravelersMannequin : ModItem
	{
		// Token: 0x06000803 RID: 2051 RVA: 0x0001716C File Offset: 0x0001536C
		public override bool IsLoadingEnabled(Mod mod)
		{
			return !QoLCompendium.itemConfig.DisableModdedItems || QoLCompendium.itemConfig.TravelersMannequin;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x000086D7 File Offset: 0x000068D7
		public override void SetStaticDefaults()
		{
			base.Item.ResearchUnlockCount = 1;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00017188 File Offset: 0x00015388
		public override void SetDefaults()
		{
			base.Item.width = 15;
			base.Item.height = 23;
			base.Item.useStyle = 4;
			base.Item.UseSound = new SoundStyle?(SoundID.Item4);
			base.Item.useAnimation = 20;
			base.Item.useTime = 20;
			base.Item.shoot = ModContent.ProjectileType<NPCSpawner>();
			base.Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 0, 75, 0));
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00017210 File Offset: 0x00015410
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipChanges.ItemDisabledTooltip(base.Item, tooltips, QoLCompendium.itemConfig.TravelersMannequin);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00017228 File Offset: 0x00015428
		public override void AddRecipes()
		{
			Recipe itemRecipe = ModConditions.GetItemRecipe(() => QoLCompendium.itemConfig.TravelersMannequin, base.Type, 1, "Mods.QoLCompendium.ItemToggledConditions.ItemEnabled");
			itemRecipe.AddIngredient(225, 6);
			itemRecipe.AddIngredient(178, 2);
			itemRecipe.AddIngredient(320, 1);
			itemRecipe.AddTile(16);
			itemRecipe.Register();
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001729C File Offset: 0x0001549C
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 spawnPosition = player.Center - Vector2.UnitY * 800f;
			Projectile.NewProjectile(player.GetSource_ItemUse(base.Item, null), spawnPosition, Vector2.Zero, ModContent.ProjectileType<NPCSpawner>(), 0, 0f, Main.myPlayer, 368f, 0f, 0f);
			SoundEngine.PlaySound(SoundID.Roar, new Vector2?(player.position), null);
			return false;
		}
	}
}
